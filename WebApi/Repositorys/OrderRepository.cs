using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.DTOS.Order;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepo;

        public OrderRepository(ApplicationDbContext db, IProductRepository productRepo)
        {
            _db = db;
            _productRepo = productRepo;
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
        private string GenerateID() => Guid.NewGuid().ToString("N");
    }
}
