using Microsoft.EntityFrameworkCore;
using Stripe;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Order;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Repositorys.IRepositorys;
using Order = WebApi.Models.OrderAggregate.Order;
using OrderItem = WebApi.Models.OrderAggregate.OrderItem;
using PaymentMethod = WebApi.Models.OrderAggregate.PaymentMethod;

namespace WebApi.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IConfiguration _config;

        public OrderRepository(
            ApplicationDbContext db,
            IProductRepository productRepo,
            ICartRepository cartRepo,
            IConfiguration config 
        )
        {
            _db = db;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
            _config = config;
        }


        //การสั่งจองกับการสั่งซื้อจะใช้ method เดียวกัน
        public async Task CreateAsync(CreateOrderDTO createOrder)
        {
            List<OrderItem> orderItems = new();
            if (createOrder.AccountIdFromProduct?.Count() > 0)
            {
                foreach (var accountId in createOrder.AccountIdFromProduct)
                {
                    Order order = new()
                    {
                        Id = GenerateID(),
                        Created = DateTime.Now,
                        AddressID = createOrder.AddressID,
                        CustomerStatus = false,
                        PaymentMethod = createOrder.PaymentMethod,
                        Subtotal = 0,
                        DeliveryFee = 0,
                        SellerStatus = false,
                        OrderCancel = false,
                        OrderUsage = createOrder.OrderUsage,
                        //OrderStatus = createOrder.PaymentMethod == PaymentMethod.CreditCard ? OrderStatus.SuccessfulPayment : OrderStatus.WaitingForPayment
                    };

                    foreach (var item in createOrder.OrderItems)
                    {
                        var product = await _productRepo.GetAsync(item.ItemOrdered.ProductID);
                        product.Stock -= item.Amount;
                        if (product.AccountID == accountId) orderItems.Add(new OrderItem
                        {
                            Amount = item.Amount,
                            OrderID = order.Id,
                            ItemOrdered = item.ItemOrdered,
                            Price = item.Price,

                        });
                    }
                    var subtotal = orderItems.Sum(item => item.Price * item.Amount);
                    var deliveryFee = subtotal > 10000 ? 0 : 500;
                    order.Subtotal = subtotal;
                    order.DeliveryFee = deliveryFee;
                    if (createOrder.PaymentMethod == PaymentMethod.CreditCard)
                    {
                        var intent = await CreatePaymentIntent(order);
                        if (!string.IsNullOrEmpty(intent.Id))
                        {
                            order.PaymentIntentId = intent.Id; // เอาใบส่งของใส่ในใบสั่งซื้อ
                            order.ClientSecret = intent.ClientSecret; // เอารหัสลับใส่ในใบสั่งซื้อ
                        };
                    };
                    await _db.AddAsync(order);
                    await _db.AddRangeAsync(orderItems);
                    for (int i = 0; i < orderItems.Count; i++) orderItems.Remove(orderItems[i]);
                };
            }

            if (!string.IsNullOrEmpty(createOrder.CartID))
                _db.Remove(await _cartRepo.GetCartAsync(createOrder.CartID));

            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<OrderDTO>> GetByAccountIdAsync(string accountId)
        {
            var orders = await _db.Orders
                 .Include(e => e.Address)
                 .ThenInclude(e => e.AddressInformations)
                 .ProjectOrderToOrderDTO(_db)
                 .Where(x => x.Address.AccountID == accountId)
                 .ToListAsync();
            return orders;
        }

        public async Task<OrderDTO> GetAsync(string Id)
        {
            var order = await _db.Orders.Include(e => e.Address)
            .ProjectOrderToOrderDTO(_db)
            .Where(x => x.Id == Id)
            .FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<string>> GetAccountIdAsync(int[] cartItemId, string cartId)
        {
            var accountIds = new List<string>();
            var cartItems = new List<CartItem>();
            var carts = await _cartRepo.GetCartAsync(cartId);

            if (carts != null)
            {
                foreach (var itemId in cartItemId)
                {
                    var cartItem = carts.Items.Find(x => x.Id == itemId);
                    if (cartItem != null) cartItems.Add(cartItem);
                }
                foreach (var item in cartItems)
                {
                    var product = await _productRepo.GetAsync(item.ProductId);
                    var result = accountIds.Find(x => x == product.AccountID);
                    if (result == null) accountIds.Add(product.AccountID);
                }
            }
            return accountIds;
        }

        private string GenerateID() => Guid.NewGuid().ToString("N");

        public async Task UpdateAsync(Order order)
        {
            _db.Update(order);
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<OrderDTO>> GetAllAsync(OrderParams orderParams)
        {
            var orders = await _db.Orders
              .Include(e => e.Address)
              .ThenInclude(e => e.AddressInformations)
              .Search(orderParams.Id)
              .ByAccountID(orderParams.AccountId)
              .FilterCancel(orderParams.OrderCancel)
              .FilterStatus(orderParams.OrderStatus)
              .FilterOrderUsage(orderParams.OrderUsage)
              .HaveEvidence(orderParams.HaveEvidence)
              .ProjectOrderToOrderDTO(_db)
              .ToListAsync();

            if (!string.IsNullOrEmpty(orderParams.SellerId))
            {
                List<OrderDTO> ordersDTO = new();
                foreach (var order in orders)
                {
                    var items = order.OrderItems.Where(e => e.AccountID == orderParams.SellerId);
                    if (items.Count() > 0) ordersDTO.Add(order);
                }
                return ordersDTO;
            };

            return orders;
        }

        private async Task<PaymentIntent> CreatePaymentIntent(Order order)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];
            var service = new PaymentIntentService();
            var intent = new PaymentIntent();

            //สร้างรายการใหม่
            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (order.Subtotal + order.DeliveryFee) * 100, // ยอดเงินเท่าไร
                    Currency = "THB", // สกุลเงิน
                    PaymentMethodTypes = new List<string> { "card" } // วิธีการจ่าย
                };
                intent = await service.CreateAsync(options); // รหัสใบส่งของ
            };

            return intent; // ส่งใบส่งของออกไป
        }

    }
}
