using WebApi.Models.OrderAggregate;

namespace WebApi.Models.DTOS.Order
{
    public class UpdateOrderDTO
    {
        public string Id { get; set; }
        public long Subtotal { get; set; }  // ราคารวม
        public long DeliveryFee { get; set; } // ค่าจัดส่ง
        public bool CustomerStatus { get; set; }
        public bool SellerStatus { get; set; }
        public bool OrderCancel { get; set; }
        public int OrderStatus { get; set; }
        public int AddressID { get; set; }
    }
}
