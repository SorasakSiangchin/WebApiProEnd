﻿using Microsoft.EntityFrameworkCore;
using WebApi.Modes.CartAggregate;
using WebApi.Modes.DTOS.Cart;
using WebApi.Settings;
using WebApiProjectEnd.Modes;

namespace WebApi.Extenstions
{
    public static class CartExtensions
    {
        public static CartDTO MapBasketToDto(this Cart cart)
        {
            return  new CartDTO
            {
                Id = cart.Id,
                AccountID = cart.AccountID,
                Items = cart.Items.Select(item => new CartItemDTO
                {
                    Amount = item.Amount,
                    ImageUrl = !string.IsNullOrEmpty(item.Product.ImageUrl) ? $"{applicationUrl.Url}/product/{item.Product.ImageUrl}" : ""  ,
                    Name = item.Product.Name,
                    CategoryProductName = item.Product.CategoryProduct.Name ,
                    Price = item.Product.Price,
                    Stock = item.Product.Stock,
                    ProductId = item.ProductId
                }).ToList()
            };
        }

        public static IQueryable<Cart> RetrieveBasketWithItems(this IQueryable<Cart> query, string accountId)
        {
            return query.Include(i => i.Items).ThenInclude(p => p.Product).Where(b => b.AccountID == accountId);
        }
    }
}
