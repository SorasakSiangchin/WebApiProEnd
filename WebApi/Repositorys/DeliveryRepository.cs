using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly ApplicationDbContext _db;

        public DeliveryRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task CreateAsync(Delivery delivery) => await _db.AddAsync(delivery);

        public async Task UpdateAsync(Delivery delivery) =>  _db.Update(delivery);

        public async Task<DeliveryDTO> GetByOrderId(string orderId)
        {
          return await _db.Deliverys
                .Include(e => e.StatusDelivery)
                .Include(e => e.Order)
                .Where(e => e.OrderID.Equals(orderId))
                .ProjectDeliveryToDeliveryDto(_db)
                .FirstOrDefaultAsync();
        }

        public async Task SaveAsync() => await _db.SaveChangesAsync();

        public async Task<DeliveryDTO> GetAsync(int id, bool tracked = true)
        {
            IQueryable<Delivery> query = _db.Deliverys;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query
                .Include(e => e.StatusDelivery)
                .Include(e => e.Order)
                .ProjectDeliveryToDeliveryDto(_db)
                .FirstOrDefaultAsync(e => e.Id.Equals(id));
        }
    }
}
