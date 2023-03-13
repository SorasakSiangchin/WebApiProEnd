using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.DTOS.Product;
using WebApi.Models.OrderAggregate;
using WebApi.Modes;
using WebApi.Modes.DTOS.Order;
using WebApi.Modes.DTOS.Product;
using WebApi.Settings;

namespace WebApi.Extenstions
{
    public static class ProductExtenstions
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> query, string category)
        {
            var categoryList = new List<string>();

            if (!string.IsNullOrEmpty(category)) categoryList.AddRange(category.ToLower().Split(",").ToList());
            //กระบวนการวนลูปอยู่ภายใน (ทำอยู่ข้างใน)
            //รูปแบบมันจะดูยาก
            query = query.Where(p => categoryList.Count == 0 || categoryList.Contains(p.CategoryProduct.Name.ToLower()));
            return query;
        }

        public static IQueryable<Product> RangePrice(this IQueryable<Product> query, int RangeStart, int RangeEnd)
        {
            if (RangeStart == 0 && RangeEnd == 0) return query;
            query = query.Where(p => p.Price >= RangeStart && p.Price <= RangeEnd);
            return query;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;
            // Trim เป็นการตัดช่องว่างทิ้ง
            // ToLower เป็นตัวเล็ก
            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();

            return query.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Product> ByAccountID(this IQueryable<Product> query, string accountId)
        {
            if (string.IsNullOrEmpty(accountId)) return query;

            return query.Where(p => p.AccountID.Equals(accountId));
        }

        public static IQueryable<ProductDTO> ProjectProductToProductDTO(this IQueryable<Product> query, ApplicationDbContext db)
        {
            return query
                .Select(product => new ProductDTO
                {
                    Id = product.Id,
                    AccountID = product.AccountID,
                    CategoryProduct = product.CategoryProduct,
                    CategoryProductID = product.CategoryProductID,
                    Color = product.Color,
                    Created = product.Created,
                    Description = product.Description,
                    ImageUrl = !string.IsNullOrEmpty(product.ImageUrl) ? $"{ApplicationUrl.Url}/product/{product.ImageUrl}" : "",
                    LastUpdate = product.LastUpdate,
                    LevelProduct = product.LevelProduct,
                    LevelProductID = product.LevelProductID,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock,
                    Weight = product.Weight,
                    WeightUnit = product.WeightUnit,
                    WeightUnitID = product.WeightUnitID ,
                    ImageProducts = db.ImageProducts.Where(e => e.ProductID == product.Id).Select(ImageProductResponse.FromImageProduct).ToList(),
                }).AsNoTracking();
        }
    }
}
