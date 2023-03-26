using WebApi.Modes;

namespace WebApi.Models
{
    public class OrderMessageDTO
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public string OrderID { get; set; }
        public AccountResponse account { get; set; }
    }
}
