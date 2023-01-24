
using WebApi.Models.OrderAggregate;

namespace WebApi.Modes.DTOS.Order
{
    public class CreateOrderDto
    {
        public int AddressID { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
