using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class Delivery
    {
        public int Id { get; set; }
        public DateTime TimeArrive { get; set; } //เวลาที่สินค้าจะมาถึง
        public string ShippingServiceName { get; set; } //ชื่อบริการจัดส่ง
        public string OrderID { get; set; }
        public int StatusDeliveryID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
        [ForeignKey("StatusDeliveryID")]
        public virtual StatusDelivery StatusDelivery { get; set; }
    }
}
