using WebApi.Modes.OrderAggregate;

namespace WebApi.Models.OrderAggregate
{
    public class OrderItem
    {
        public int Id { get; set; }
        // ItemOrdered จะใช้ key ของตารางนี้
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
