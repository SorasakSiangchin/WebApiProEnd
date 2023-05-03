using System.Reflection;

namespace WebApi.Models.DTOS.Repost
{
    public class ProductStatisticsRequestDTO
    {
        public string AccountId { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public int Number { get; set; }

        public static ValueTask<ProductStatisticsRequestDTO?> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
        {
            int.TryParse(context.Request.Form["Number"], out int number);
            return ValueTask.FromResult<ProductStatisticsRequestDTO?>(new ProductStatisticsRequestDTO
            {
                AccountId = context.Request.Form["AccountId"],
                DateEnd = context.Request.Form["DateEnd"],
                DateStart = context.Request.Form["DateStart"],
                Number = number
            }); ;
        }
    }
}
