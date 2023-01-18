using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class EvidenceMoneyTransfer // หลักฐานการโอนเงิน
    {
        public int Id { get; set; }
        public string Evidence { get; set; }
        public DateTime? Created { get; set; }
        public string OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
