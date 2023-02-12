using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface ILevelProductRepository
    {
        Task<ICollection<LevelProduct>> GetAllAsync();
    }
}
