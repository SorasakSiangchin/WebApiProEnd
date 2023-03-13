using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.OrderAggregate;

namespace WebApi.Models
{
    public class EvidenceMoneyTransfer // หลักฐานการโอนเงิน
    {
        public int Id { get; set; }
        public string Evidence { get; set; }
        public bool Status { get; set; }
        public DateTime? Created { get; set; }
        public string OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
