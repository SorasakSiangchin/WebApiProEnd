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
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderUsage OrderUsage { get; set; } = OrderUsage.Buy;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.WaitingForPayment;
        public DateTime Created { get; set; }
        public int AddressID { get; set; }
        [ForeignKey("AddressID")]
        public virtual Address Address { get; set; }
        public long GetTotal() => Subtotal + DeliveryFee;
    }
}
