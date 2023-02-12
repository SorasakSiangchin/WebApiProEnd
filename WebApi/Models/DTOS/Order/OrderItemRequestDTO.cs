using WebApi.Modes.OrderAggregate;

namespace WebApi.Models.DTOS.Order
{
    public class OrderItemRequestDTO
    {
        public int Id { get; set; }
        // ItemOrdered จะใช้ key ของตารางนี้
        public ProductItemOrdered ItemOrdered { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
