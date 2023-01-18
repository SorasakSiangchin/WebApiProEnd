using System.Reflection;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApi.Modes.DTOS.Product
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? Color { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int WeightUnitID { get; set; }
        public int CategoryProductID { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
       
        public static ValueTask<ProductDTO?> BindAsync(HttpContext context,
                                                  ParameterInfo parameter)
        {
            var file = context.Request.Form.Files;
            DateTime.TryParse(context.Request.Form["LastUpdate"], out DateTime lastUpdate);
            return ValueTask.FromResult<ProductDTO?>(new ProductDTO
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
                FormFiles = file,
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
