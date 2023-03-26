

using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IImageProductRepository 
    {
        Task CreateAsync(ImageProduct imageProduct , List<string> imageName);
        Task<ICollection<ImageProduct>> GetAllAsync(string productID);
        Task<ImageProduct> GetAsync(string id, bool tracked = true);
        Task<(string errorMessage, List<string> imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
        Task RemoveAsync(ImageProduct imageProduct);
    }
}
