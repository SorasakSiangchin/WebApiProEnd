
using WebApi.Models.DTOS.Order;
using WebApi.Models.OrderAggregate;

namespace WebApi.Modes.DTOS.Order
{
    public class CreateOrderDTO
    {
        public int AddressID { get; set; }
        public List<OrderItemRequestDTO> OrderItems { get; set; }
        public string[] AccountIdFromProduct { get; set; }
        public string CartID { get; set; }
    }
}
