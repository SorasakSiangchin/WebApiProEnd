using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class CategoryProductRepository : ICategoryProductRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ICollection<CategoryProduct>> GetAllAsync()
        {
            return await _db.CategoryProducts.ToListAsync();
        }
    }
}
