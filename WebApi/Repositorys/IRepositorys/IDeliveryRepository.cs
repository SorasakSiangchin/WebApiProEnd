using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IDeliveryRepository
    {
        Task<DeliveryDTO> GetByOrderId(string orderId);
        Task<DeliveryDTO> GetAsync(int id, bool tracked = true);
        Task CreactAsync(Delivery delivery);
        Task UpdateAsync(Delivery delivery);
        Task SaveAsync();
    }
}
