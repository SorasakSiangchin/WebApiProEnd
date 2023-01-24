using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface ICategoryProductRepository
    {
        Task<ICollection<CategoryProduct>> GetAllAsync();
    }
}
