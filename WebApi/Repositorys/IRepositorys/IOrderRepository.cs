using WebApi.Models;
using WebApi.Modes.DTOS.Order;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDTO>> GetAllAsync(string accountId);
        Task<List<string>> GetAccountIdAsync(int[] cartItemId  , string cartId);
        Task<OrderDTO> GetAsync(string Id, string accountId);
        Task CreactAsync(CreateOrderDto createOrder);
    }
}
