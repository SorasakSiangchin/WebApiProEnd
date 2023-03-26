using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IStatusDeliveryRepository : IRepository<StatusDelivery>
    {
        Task<ICollection<StatusDelivery>> GetAllAsync();
        Task<StatusDelivery> GetAsync(int? id, bool tracked = true);
    }
}
