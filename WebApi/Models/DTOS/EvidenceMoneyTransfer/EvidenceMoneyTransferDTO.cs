

using WebApi.Models.OrderAggregate;

namespace WebApi.Models
{
    public class EvidenceMoneyTransferDTO
    {
        public int? Id { get; set; }
        public string Evidence { get; set; }
        public DateTime? Created { get; set; }
        public string OrderID { get; set; }
        public Order Order { get; set; }
    }
}
