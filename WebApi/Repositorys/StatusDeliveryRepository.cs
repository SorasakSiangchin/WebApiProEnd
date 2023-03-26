using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class StatusDeliveryRepository : IStatusDeliveryRepository
    {
        private readonly ApplicationDbContext _db;

        public StatusDeliveryRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(StatusDelivery statusDelivery)
        {
           await _db.AddAsync(statusDelivery);
        }

        public async Task<ICollection<StatusDelivery>> GetAllAsync()
        {
          return  await _db.StatusDeliverys.ToListAsync();
        }

        public async Task<StatusDelivery> GetAsync(int? id, bool tracked = true)
        {
            IQueryable<StatusDelivery> query = _db.StatusDeliverys;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(StatusDelivery statusDelivery)
        {
             _db.Remove(statusDelivery);
        }

        public async Task SaveAsync()
        {
           await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(StatusDelivery statusDelivery)
        {
             _db.Update(statusDelivery);
        }
    }
}
