using WebApi.Models;
using WebApi.Modes.DTOS.Order;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDTO>> GetAllAsync(string accountId);
        Task<OrderDTO> GetAsync(string Id, string accountId);
        Task<string> CreactAsync(CreateOrderDto createOrder);
    }
}
