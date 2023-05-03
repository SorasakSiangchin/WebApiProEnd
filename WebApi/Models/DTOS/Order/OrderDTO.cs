using WebApi.Models.OrderAggregate;
using WebApi.Modes.DTOS.Order;

namespace WebApi.Models
{
    public class OrderDTO
    {
        public string Id { get; set; }
        public string AccountID { get; set; }
        public string? SellerID { get; set; }
        public long Subtotal { get; set; }  // ราคารวม
        public long DeliveryFee { get; set; } // ค่าจัดส่ง
        public bool CustomerStatus { get; set; }
        public bool SellerStatus { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public OrderUsage OrderUsage { get; set; }
        public string? ClientSecret { get; set; }
        public string? PaymentIntentId { get; set; }
        public int OrderStatus { get; set; }
        public DateTime? Created { get; set; }
        public long Total { get; set; }
        public bool OrderCancel { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
        public Address Address { get; set; }
        public EvidenceMoneyTransfer evidenceMoney { get; set; }
        public List<OrderMessageDTO> orderMessage { get; set; }
        public long GetTotal() => Subtotal + DeliveryFee;
    }
}
