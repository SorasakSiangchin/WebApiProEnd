using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class WeightUnitRepository : IWeightUnitRepository
    {
        private readonly ApplicationDbContext _db;

        public WeightUnitRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<ICollection<WeightUnit>> GetAllAsync() =>  await _db.WeightUnits.ToListAsync();
       
    }
}
