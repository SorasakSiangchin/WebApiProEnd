using WebApi.Models;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Modes;

namespace WebApiProjectEnd.Repositorys.IRepositorys
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetAsyncByNameAsync(string name);
        Task<ICollection<Product>> GetProductByAccountIdAsync(string accountId);
        Task<ICollection<Product>> GetRareAsync();
        Task<ICollection<Product>> GetRecommendAsync(int num);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
