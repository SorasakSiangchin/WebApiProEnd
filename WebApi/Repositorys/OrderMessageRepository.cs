using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class OrderMessageRepository : IOrderMessageRepository
    {
        private readonly ApplicationDbContext _db;

        public OrderMessageRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(OrderMessage orderMessage)
        {
            orderMessage.Id = GenerateID();
            await _db.AddAsync(orderMessage);
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        private string GenerateID() => Guid.NewGuid().ToString("N");
    }
}
