using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IRoleRepository
    {
        Task<ICollection<Role>> GetAllAsync();
    }
}
