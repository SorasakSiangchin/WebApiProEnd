using WebApiProjectEnd.Modes;

namespace WebApi.Repositorys.IRepositorys
{
    public interface ICategoryProductRepository
    {
        Task<ICollection<CategoryProduct>> GetAllAsync();
    }
}
