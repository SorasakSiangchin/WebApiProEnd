using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys
{
    public class DetailProductRepository : IDetailProductRepository
    {
        private readonly ApplicationDbContext _db;

        public DetailProductRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreactAsync(DetailProduct entity) => await _db.AddAsync(entity);


        public async Task<DetailProduct> GetAsync(int? id, bool tracked = true)
        {
            IQueryable<DetailProduct> query = _db.DetailProducts;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<DetailProduct> GetByIdProductAsync(string idProduct, bool tracked = true)
        {
            IQueryable<DetailProduct> query = _db.DetailProducts;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.Where(e => e.ProductID == idProduct).FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(DetailProduct entity) => _db.Remove(entity);
        

        public async Task SaveAsync() => await _db.SaveChangesAsync();
        

        public async Task UpdateAsync(DetailProduct entity) => _db.Update(entity);
        
    }
}
