using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Modes;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class ImageProductRepository : IImageProductRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUploadFileRepository _uploadFile;

        public ImageProductRepository(ApplicationDbContext db , IUploadFileRepository uploadFile)
        {
            _db = db;
            _uploadFile = uploadFile;
        }

        public async Task CreactAsync(ImageProduct imageProduct, List<string> imageName)
        {
            for (var i = 0; i < imageName.Count(); i++)
            {
                imageProduct.Id = GenerateID() ;
                imageProduct.ImageUrl = imageName[i];
                await _db.ImageProducts.AddAsync(imageProduct);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteImage(string fileName)
        {
            await _uploadFile.DeleteFile(fileName, "imageProduct");
        }

        public async Task<ICollection<ImageProduct>> GetAllAsync(string productID) => await _db.ImageProducts.OrderBy(e => e.Id).Where(x => x.ProductID.Equals(productID)).ToListAsync();
        

        public async Task<ImageProduct> GetAsync(string id, bool tracked = true)
        {
            IQueryable<ImageProduct> query = _db.ImageProducts;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task RemoveAsync(ImageProduct imageProduct)
        {
            _db.Remove(imageProduct);
            await _db.SaveChangesAsync();
        }

        public async Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = new List<string>();
            if (_uploadFile.IsUpload(formFiles))
            {
                errorMessage = _uploadFile.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFile.UploadFile(formFiles , "imageProduct"));
                }
            }
            return (errorMessage, imageName);
        }
        private string GenerateID() => Guid.NewGuid().ToString("N"); 
        
    }
}
