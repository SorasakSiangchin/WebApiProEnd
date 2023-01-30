using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Order;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepo;
        private readonly ICartRepository _cartRepo;

        public OrderRepository(ApplicationDbContext db, IProductRepository productRepo , ICartRepository cartRepo)
        {
            _db = db;
            _productRepo = productRepo;
            _cartRepo = cartRepo;
        }

        public async Task<string> CreactAsync(CreateOrderDto createOrder)
        {
            //var items = new List<OrderItem>();
            foreach (var item in createOrder.OrderItems)
            {
                var product = await _productRepo.GetAsync(item.ItemOrdered.ProductID);
                product.Stock -= item.Amount;
            }
            var subtotal = createOrder.OrderItems.Sum(item => item.Price * item.Amount);
            var deliveryFee = subtotal > 10000 ? 0 : 500;
            Order order = new()
            {
                Id = GenerateID(),
                AddressID = createOrder.AddressID,
                CustomerStatus = false,
                Subtotal = subtotal,
                DeliveryFee = deliveryFee,
                OrderItems = createOrder.OrderItems,
                SellerStatus = false,
                OrderCancel = false,
            };
            await _db.AddAsync(order);
            await _db.SaveChangesAsync();
            return order.Id;
        }

        public async Task<ICollection<OrderDTO>> GetAllAsync(string accountId)
        {
            var orders = await _db.Orders.Include(e => e.Address)
                 .ProjectOrderToOrderDto()
                 .Where(x => x.Address.AccountID == accountId)
                 .ToListAsync();
            return orders;
        }

        public async Task<OrderDTO> GetAsync(string Id, string accountId)
        {
            var order = await _db.Orders.Include(e => e.Address)
            .ProjectOrderToOrderDto()
            .Where(x => x.Address.AccountID == accountId && x.Id == Id)
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
    }
}
