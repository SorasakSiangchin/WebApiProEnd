using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public int PriceTotal { get; set; }
        public bool CustomerStatus { get; set; }
        public bool SellerStatus { get; set; }
        public bool PaymentStatus { get; set; }
        public DateTime? Created { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
    }
}
