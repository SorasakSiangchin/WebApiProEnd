using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IStatusDeliveryRepository
    {
        Task<ICollection<StatusDelivery>> GetAllAsync();

        Task<StatusDelivery> GetAsync(int? id, bool tracked = true);
        Task CreactAsync(StatusDelivery statusDelivery);
        Task UpdateAsync(StatusDelivery statusDelivery);
        Task RemoveAsync(StatusDelivery statusDelivery);
        Task SaveAsync();
    }
}
