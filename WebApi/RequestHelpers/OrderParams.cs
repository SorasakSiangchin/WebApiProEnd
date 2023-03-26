using WebApi.Models.OrderAggregate;

namespace WebApi.RequestHelpers
{
    public class OrderParams : PaginationParams
    {
        public string? Id { get; set; }
        public string? AccountId { get; set; }
        public string? SellerId { get; set; }
        public string OrderCancel { get; set; }
        public string OrderStatus { get; set; }
        public string OrderUsage { get; set; }
        public bool HaveEvidence { get; set; }
        
    }
}
