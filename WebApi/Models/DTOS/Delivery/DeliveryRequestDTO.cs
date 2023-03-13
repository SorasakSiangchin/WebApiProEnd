using System.Reflection;
using WebApi.Modes.DTOS.Product;

namespace WebApi.Models.DTOS.Delivery
{
    public class DeliveryRequestDTO
    {
        public int Id { get; set; }
        public DateTime TimeArrive { get; set; }
        public string ShippingServiceName { get; set; }
        public string OrderID { get; set; }
        public int StatusDeliveryID { get; set; }

        public static ValueTask<DeliveryRequestDTO?> BindAsync(HttpContext context,
                                                 ParameterInfo parameter)
        {
            DateTime.TryParse(context.Request.Form["TimeArrive"], out DateTime date);
            return ValueTask.FromResult<DeliveryRequestDTO?>(new DeliveryRequestDTO
            {
                Id = SetIntData("Id", context),
                TimeArrive = date,
                OrderID = SetStringData("OrderID", context),
                ShippingServiceName = SetStringData("ShippingServiceName", context),
                StatusDeliveryID = SetIntData("StatusDeliveryID", context)
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
    }
}
