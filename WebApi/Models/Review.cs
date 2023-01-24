using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.OrderAggregate;

namespace WebApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string? Information { get; set; }
        public string? VdoUrl { get; set; }
        public double score { set; get; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public int OrderItemID { get; set; }
        [ForeignKey("OrderItemID")]
        public virtual OrderItem OrderItem { get; set; }

    }
}
