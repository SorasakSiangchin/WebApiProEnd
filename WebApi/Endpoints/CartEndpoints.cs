using System.Net;
using WebApi.Models;
using WebApi.Modes.DTOS.Cart;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class CartEndpoints
    {
        public static void ConfigureCartEndpoints(this WebApplication app)
        {
            app.MapGet("/cart/{accountId}", GetCartByAccount).WithName("GetCartByAccount").Produces<APIResponse>(200);
            app.MapPost("/cart/addItem", AddItemToCart).WithName("AddItemToCart").Accepts<AddCartRequestDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapPost("/cart/removeItem", RemoveItemToCart).WithName("RemoveItemToCart").Accepts<AddCartRequestDTO>("application/json").Produces<APIResponse>(200).Produces(400);
        }

        private async static Task<IResult> GetCartByAccount(ICartRepository _cartRepo , string accountId)
        {
            APIResponse response = new();
            response.Result = await _cartRepo.GetCartByAccountIdAsync(accountId);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> AddItemToCart(ICartRepository _cartRepo , AddCartRequestDTO model)
        {
            var cart = await _cartRepo.AddItemToCartAsync(model);
            return Results.CreatedAtRoute("GetCartByAccount", cart.Result , cart);
        }

        private async static Task<IResult> RemoveItemToCart(ICartRepository _cartRepo, AddCartRequestDTO model) => Results.Ok(await _cartRepo.RemoveItemToCartAsync(model));
    }
}
