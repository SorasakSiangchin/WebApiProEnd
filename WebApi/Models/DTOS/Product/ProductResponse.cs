using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models;
using WebApi.Settings;
using WebApiProjectEnd.Modes;

namespace WebApi.Modes
{
    public class ProductResponse 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? Color { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? LastUpdate { get; set; }
        public int WeightUnitID { get; set; }
        public string AccountID { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public CategoryProduct CategoryProduct { get; set; }
        static public ProductResponse FromProduct(Product product)
        {
            return new ProductResponse
            {
                Id= product.Id,
                Name= product.Name,
                Price= product.Price,
                Stock= product.Stock,   
                Color= product.Color,
                Weight= product.Weight,
                Description= product.Description,
                ImageUrl= !string.IsNullOrEmpty(product.ImageUrl) ? $"{applicationUrl.Url}/product/{product.ImageUrl}"  : "",
                Created= product.Created,
                LastUpdate= product.LastUpdate,
                WeightUnitID= product.WeightUnitID,
                WeightUnit = product.WeightUnit,
                CategoryProduct= product.CategoryProduct,
                AccountID = product.AccountID,
            };
        }
    }
}
