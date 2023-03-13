using WebApi.Models;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Cart;

namespace WebApi.Repositorys.IRepositorys
{
    public interface ICartRepository
    {
        Task<CartDTO> GetCartByAccountIdAsync(string accountId);
        Task<Cart> GetCartAsync(string id);
        Task<APIResponse> AddItemToCartAsync(AddCartRequestDTO addCartDTO);
        Task<APIResponse> RemoveItemToCartAsync(AddCartRequestDTO addCartDTO);
    }
}
