namespace WebApi.Models.DTOS.OrderMessage
{
    public class OrderMessageRequest
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public string OrderID { get; set; }
    }
}
