using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.DTOS.Order;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IOrderRepository
    {
        Task<ICollection<OrderDTO>> GetByAccountIdAsync(string accountId);
        Task<ICollection<OrderDTO>> GetAllAsync(OrderParams orderParams);
        Task<List<string>> GetAccountIdAsync(int[] cartItemId  , string cartId);
        Task<OrderDTO> GetAsync(string Id);
        Task CreactAsync(CreateOrderDTO createOrder);
        Task UpdateAsync(Order order);
    }
}
