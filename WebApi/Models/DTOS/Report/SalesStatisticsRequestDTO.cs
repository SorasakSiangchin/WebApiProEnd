using System.Reflection;
using WebApi.Models.DTOS.Repost;

namespace WebApi.Models.DTOS.Report
{
    public class SalesStatisticsRequestDTO
    {
        public string AccountId { get; set; }
        public DateTime? Year { get; set; } = null;

        public static ValueTask<SalesStatisticsRequestDTO?> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
        {
            DateTime.TryParse(context.Request.Form["Year"], out DateTime year);
            return ValueTask.FromResult<SalesStatisticsRequestDTO?>(new SalesStatisticsRequestDTO
            {
                AccountId = context.Request.Form["AccountId"],
                Year = year
            }); ; ;
        }
    }
}
