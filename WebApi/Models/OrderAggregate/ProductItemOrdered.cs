using Microsoft.EntityFrameworkCore;

namespace WebApi.Modes.OrderAggregate
{
    [Owned]
    public class ProductItemOrdered
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}
