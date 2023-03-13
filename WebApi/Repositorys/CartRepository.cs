using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Net;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Cart;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _db;

        public CartRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<APIResponse> AddItemToCartAsync(AddCartRequestDTO addCartDTO)
        {
            APIResponse response = new() { IsSuccess = false ,  StatusCode = HttpStatusCode.BadRequest };

            var cart = await RetrieveCart(addCartDTO.accountId);
            if (cart == null) cart = await CreateCart(addCartDTO.accountId);

            var product = await _db.Products.Include(e => e.CategoryProduct).SingleOrDefaultAsync(e => e.Id == addCartDTO.productId);
            if (product == null) {
                response.ErrorMessages.Add("ไม่พบสินค้า");
                return response;
            } ;

            cart.AddItem(product, addCartDTO.amount);
            var result = await _db.SaveChangesAsync() > 0;
            if (result)
            {
                response.Result = cart.MapBasketToDto();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            response.ErrorMessages.Add("พบปัญหาในการบันทึกรายการลงตะกร้า");
            return response ;
        }

        public async Task<CartDTO> GetCartByAccountIdAsync(string accountId)
        {
            var cart = await RetrieveCart(accountId);
            if (cart == null) return null;
            return cart.MapBasketToDto();
        }

        public async Task<APIResponse> RemoveItemToCartAsync(AddCartRequestDTO addCartDTO)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var cart = await RetrieveCart(addCartDTO.accountId);
            if (cart == null)
            {
                response.ErrorMessages.Add("ไม่ผู้ใช้งาน");
                return response;
            }
            cart.RemoveItem(addCartDTO.productId, addCartDTO.amount);
            var result = await _db.SaveChangesAsync() > 0;
            if (result)
            {
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.OK;
                return response;
            }
            response.ErrorMessages.Add("พบปัญหาในการลบรายการออกจากตะกร้า");
            return response;
        }   

        private string GenerateID() => Guid.NewGuid().ToString("N");

        private async Task<Cart> CreateCart(string accountId)
        {
            Cart cart = new();
            if (await _db.Carts.SingleOrDefaultAsync(e => e.AccountID == accountId) == null) cart = new Cart { Id = GenerateID(), AccountID = accountId };
            await _db.Carts.AddAsync(cart);
            return cart;
        }

        private async Task<Cart> RetrieveCart(string accountId)
        {
            var cart = await _db.Carts
                   .Include(i => i.Items)
                   .ThenInclude(p => p.Product)
                   .ThenInclude(p => p.CategoryProduct)
                   .SingleOrDefaultAsync(x => x.AccountID == accountId);
            return cart;
        }

        public async Task<Cart> GetCartAsync(string id)
        {
            var cart = await _db.Carts
                .Include(i => i.Items)
                .ThenInclude(p => p.Product)
                .ThenInclude(p => p.CategoryProduct)
                .SingleOrDefaultAsync(x => x.Id == id);
            if (cart == null) return null;
            return cart;
        }

    }
}
