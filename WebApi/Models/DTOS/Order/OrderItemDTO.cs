using WebApi.Models;

namespace WebApi.Modes.DTOS.Order
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public string ProductID { get; set; }
        public string AccountID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
    }
}
