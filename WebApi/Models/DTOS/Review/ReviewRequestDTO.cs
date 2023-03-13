using System.Reflection;

namespace WebApi.Models.DTOS.Review
{
    public class ReviewRequestDTO
    {
        public string? Information { get; set; }
        public IFormFileCollection? VideoFiles { get; set; }
        public IFormFileCollection? ImageFiles { get; set; }
        public double Score { set; get; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public int OrderItemID { get; set; }

        public static ValueTask<ReviewRequestDTO?> BindAsync(HttpContext context,
                                                  ParameterInfo parameter)
        {
            DateTime.TryParse(context.Request.Form["Created"], out DateTime created);
            return ValueTask.FromResult<ReviewRequestDTO?>(new ReviewRequestDTO
            {
                Information = SetStringData("Information", context),
                VideoFiles = context.Request.Form.Files,
                ImageFiles = context.Request.Form.Files,
                AccountID = SetStringData("AccountID", context),
                OrderItemID = SetIntData("OrderItemID", context),
                Score = SetDoubleData("Score", context),
                Created = created
            });
        }

        private static string SetStringData(string name, HttpContext context) => context.Request.Form[name];

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
