using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApiProjectEnd.Repositorys
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUploadFileRepository _uploadFile;

        public ProductRepository(ApplicationDbContext db , IUploadFileRepository uploadFile)
        {
            _db = db;
            _uploadFile = uploadFile;
        }

        public async Task CreactAsync(Product product)
        {
            product.Id = GenerateID();
            await _db.Products.AddAsync(product);   
        }

        public  async Task<ICollection<Product>> GetAllAsync(ProductParams? productParams)
        {
            return await _db.Products.Include(e => e.CategoryProduct)
                .RangePrice(productParams.RangePriceStart , productParams.RangePriceEnd)
                .Filter(productParams.Category)
                .Search(productParams.SearchTerm)
                .Include(e => e.CategoryProduct)
                .Include(e => e.WeightUnit)
                .Include(e => e.LevelProduct)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<Product> GetAsync(string id , bool tracked = true)
        {
            IQueryable<Product> query = _db.Products
                .Include(e => e.CategoryProduct)
                .Include(e => e.WeightUnit)
                .Include(e => e.LevelProduct);
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public  async Task<ICollection<Product>> GetAsyncByName(string name)
        {
            return await _db.Products.Where(e => e.Name.Contains(name)).ToListAsync();
        }

        public async Task RemoveAsync(Product product)
        {
            _db.Products.Remove(product);
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Update(product);
        }
        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = string.Empty;
            if (_uploadFile.IsUpload(formFiles))
            {
                errorMessage = _uploadFile.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFile.UploadFile(formFiles, "product"))[0];
                }
            }
            return (errorMessage, imageName);
        }

        private string GenerateID() => Guid.NewGuid().ToString("N"); 

        public async Task DeleteImage(string fileName)
        {
            await _uploadFile.DeleteFile(fileName, "product");
        }

        public async Task<ICollection<Product>> GetRareAsync()
        {
            return await _db.Products.Include(e => e.CategoryProduct).Where(e => e.CategoryProductID.Equals(999)).ToListAsync();
        }
    }
}
