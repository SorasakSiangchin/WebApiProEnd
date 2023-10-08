using WebApi.Models;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes;

namespace WebApiProjectEnd.Repositorys.IRepositorys
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<ProductDTO>> GetAllAsync(ProductParams? productParams);
        Task<ProductDTO> GetAsync(string id, bool tracked = true);
        Task<ICollection<ProductDTO>> GetAsyncByNameAsync(string name);
        Task<ICollection<ProductDTO>> GetProductByAccountIdAsync();
        Task<ICollection<ProductDTO>> GetRareAsync();
        Task<ICollection<ProductDTO>> GetRecommendAsync(int num);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
    }
}
