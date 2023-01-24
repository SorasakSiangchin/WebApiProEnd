using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Modes.OrderAggregate;

namespace WebApi.Models.OrderAggregate
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public long Subtotal { get; set; }  // ราคารวม
        public long DeliveryFee { get; set; } // ค่าจัดส่ง
        public bool CustomerStatus { get; set; }
        public bool SellerStatus { get; set; }
        public bool OrderCancel { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public DateTime Created { get; set; } = DateTime.Now;
        public List<OrderItem> OrderItems { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        public long GetTotal()
        {
            return Subtotal + DeliveryFee;
        }
    }
}
