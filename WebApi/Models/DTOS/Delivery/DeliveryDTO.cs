using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.OrderAggregate;

namespace WebApi.Models
{
    public class DeliveryDTO
    {
        public int Id { get; set; }
        public DateTime TimeArrive { get; set; } //เวลาที่สินค้าจะมาถึง
        public string ShippingServiceName { get; set; } //ชื่อบริการจัดส่ง
        public string OrderID { get; set; }
        public int StatusDeliveryID { get; set; }
        public  Order Order { get; set; }
        public  StatusDelivery StatusDelivery { get; set; }
    }
}
