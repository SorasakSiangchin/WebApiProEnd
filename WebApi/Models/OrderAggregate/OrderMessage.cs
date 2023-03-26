using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models.OrderAggregate
{
    public class OrderMessage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Message { get; set; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public string OrderID { get; set; }
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }

    }
}
