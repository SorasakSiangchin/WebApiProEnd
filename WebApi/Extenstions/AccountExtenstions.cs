using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes;
using WebApi.Modes.DTOS.Order;
using WebApi.Modes.DTOS.Product;
using WebApi.Settings;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApi.Extenstions
{
    public static class AccountExtenstions
    {
        public static IQueryable<Account> SearchName(this IQueryable<Account> query, string searchName)
        {
            if (string.IsNullOrEmpty(searchName)) return query;
            var lowerCaseSearchName = searchName.Trim().ToLower();
            return query.Where(p => p.FirstName.ToLower().Contains(lowerCaseSearchName) || p.LastName.ToLower().Contains(lowerCaseSearchName));
        }

        public static IQueryable<Account> SearchEmail(this IQueryable<Account> query, string searchEmail)
        {
            if (string.IsNullOrEmpty(searchEmail)) return query;
            var lowerCaseSearchNEmail = searchEmail.Trim().ToLower();
            return query.Where(p => p.Email.ToLower().Contains(lowerCaseSearchNEmail));
        }

        public static IQueryable<Account> SearchPhoneNumber(this IQueryable<Account> query, string searchPhoneNumber)
        {
            if (string.IsNullOrEmpty(searchPhoneNumber)) return query;
            return query.Where(p => p.PhoneNumber.Contains(searchPhoneNumber));
        }

        public static IQueryable<Account> FilterStatus(this IQueryable<Account> query, string status)
        {
            if (string.IsNullOrEmpty(status)) return query;
            return query.Where(p => p.Status.Equals(Convert.ToBoolean(status.ToLower())));
        }

        public static IQueryable<AccountDTO> ProjectAccountToAccountDTO(this IQueryable<Account> query, ApplicationDbContext db)
        {
            return query
                .Select(account => new AccountDTO
                {
                    Id = account.Id,
                    Email = account.Email,
                    FirstName = account.FirstName,
                    ImageUrl = CheckImageUrl(account.ImageUrl, account.LoginBy),
                    LastName = account.LastName,
                    Password = account.Password,
                    PhoneNumber = account.PhoneNumber,
                    Role = account.Role,
                    RoleID = account.RoleID,
                    Status = account.Status,
                    LoginBy = account.LoginBy,
                    Products = db.Products
                    .AsNoTracking()
                    .Where(e => e.AccountID == account.Id)
                    .Include(e => e.CategoryProduct)
                    .Include(e => e.LevelProduct)
                    .Include(e => e.WeightUnit)
                    .Select(product => FromProduct(product))
                    .ToList(),
                }).AsNoTracking();
        }

        private static string CheckImageUrl(string? ImageUrl, string? loginBy)
        {
            if (string.IsNullOrEmpty(loginBy))
            {
                if (!string.IsNullOrEmpty(ImageUrl)) return $"{ApplicationUrl.Url}/account/{ImageUrl}";
                else return "";
            }
            else
            {
                if (!string.IsNullOrEmpty(ImageUrl)) return ImageUrl;
                else return "";
            }

        }

        private static Product FromProduct(Product product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                Color = product.Color,
                Weight = product.Weight,
                Description = product.Description,
                ImageUrl = !string.IsNullOrEmpty(product.ImageUrl) ? $"{ApplicationUrl.Url}/product/{product.ImageUrl}" : "",
                Created = product.Created,
                LastUpdate = product.LastUpdate,
                WeightUnitID = product.WeightUnitID,
                WeightUnit = product.WeightUnit,
                CategoryProduct = product.CategoryProduct,
                CategoryProductID = product.CategoryProductID,
                AccountID = product.AccountID,
                LevelProductID = product.LevelProductID,
                LevelProduct = product.LevelProduct,
            };
        }
    }
}
