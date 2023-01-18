using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync(ProductParams? productParams);
        Task<T> GetAsync(string id, bool tracked = true);
        Task CreactAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
