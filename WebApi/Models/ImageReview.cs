using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class ImageReview
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int ReviewID { get; set; }
        [ForeignKey("ReviewID")]
        public virtual Review Review { get; set; }
    }
}
