using WebApi.Models;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Cart;
using WebApiProjectEnd.Modes;

namespace WebApi.Repositorys.IRepositorys
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartAsync(string accountId);
        Task<APIResponse> AddItemToCartAsync(AddCartRequestDTO addCartDTO);
        Task<APIResponse> RemoveItemToCartAsync(AddCartRequestDTO addCartDTO);
    }
}
