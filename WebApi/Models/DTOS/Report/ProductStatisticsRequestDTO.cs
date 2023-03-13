using System.Reflection;

namespace WebApi.Models.DTOS.Repost
{
    public class ProductStatisticsRequestDTO
    {
        public string AccountId { get; set; }
        public DateTime? DateStart { get; set; } = null;
        public DateTime? DateEnd { get; set; } = null;
        public int Number { get; set; } 

        public static ValueTask<ProductStatisticsRequestDTO?> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
        {
            DateTime.TryParse(context.Request.Form["DateStart"], out DateTime dateStart);
            DateTime.TryParse(context.Request.Form["DateEnd"], out DateTime dateEnd);
            int.TryParse(context.Request.Form["Number"], out int number);
            return ValueTask.FromResult<ProductStatisticsRequestDTO?>(new ProductStatisticsRequestDTO
            {
                AccountId = context.Request.Form["AccountId"],
                DateEnd = dateEnd,
                DateStart = dateStart,
                Number = number
            }); ;
        }
    }
}
