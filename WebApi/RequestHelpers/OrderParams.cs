namespace WebApi.RequestHelpers
{
    public class OrderParams : PaginationParams
    {
        public string? Id { get; set; }
        public string? AccountId { get; set; }
        public string? SellerId { get; set; }
        public string OrderCancel { get; set; }
        public string OrderStatus { get; set; }
    }
}
