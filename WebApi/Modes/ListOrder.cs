using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class ListOrder
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public int TotalPrice { get; set; } // ราคารวมสินค้าต่อชิ้น
        public int Amount { get; set; }
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
