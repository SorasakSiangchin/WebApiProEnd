using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;
using WebApi.Models;
using WebApi.Models.DTOS.DetailProduct;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApi.Modes.DTOS.Product
{
    public class ProductRequest
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? Color { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public string AccountID { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int WeightUnitID { get; set; }
        public int CategoryProductID { get; set; }
        public int LevelProductID { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
        public DetailProductDTO DetailProduct { get; set; }

        public static ValueTask<ProductRequest?> BindAsync(HttpContext context,
                                                  ParameterInfo parameter)
        {
            var file = context.Request.Form.Files;
            DateTime.TryParse(context.Request.Form["LastUpdate"], out DateTime lastUpdate);
            return ValueTask.FromResult<ProductRequest?>(new ProductRequest
            {
                Id = SetStringData("Id" , context),
                Name = SetStringData("Name", context),
                Price = SetIntData("Price" , context),
                Stock = SetIntData("Stock", context),
                Color = SetStringData("Color", context),
                Weight = SetDoubleData("Weight", context),
                Description = SetStringData("Description", context),
                LastUpdate = lastUpdate ,
                WeightUnitID = SetIntData("WeightUnitID", context),
                CategoryProductID = SetIntData("CategoryProductID", context),
                AccountID = SetStringData("AccountID", context),
                LevelProductID = SetIntData("LevelProductID", context),
                FormFiles = file
            }); 
        }

        private static string SetStringData(string name, HttpContext context)
        {
            return context.Request.Form[name];
        }
        private static int SetIntData(string name, HttpContext context)
        {

            int.TryParse(context.Request.Form[name], out int result);
            return result;
        }
        private static double SetDoubleData(string name, HttpContext context)
        {

            double.TryParse(context.Request.Form[name], out double result);
            return result;
        }
    }
}
