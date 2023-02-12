using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class LevelProductRepository : ILevelProductRepository
    {
        private readonly ApplicationDbContext _db;

        public LevelProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ICollection<LevelProduct>> GetAllAsync() => await _db.LevelProducts.ToListAsync();
    }
}
