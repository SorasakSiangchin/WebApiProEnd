using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class Review
    {
        public int Id { get; set; }
        public string? Information { get; set; }
        public string? VdoUrl { get; set; }
        public double score { set; get; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public string ListOrderID { get; set; }
        [ForeignKey("ListOrderID")]
        public virtual ListOrder ListOrder { get; set; }

    }
}
