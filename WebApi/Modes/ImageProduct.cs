using System.ComponentModel.DataAnnotations.Schema;
using WebApiProjectEnd.Modes;

namespace WebApi.Modes
{
    public class ImageProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductID { get; set; }
        [ForeignKey("ProductID")]
        public virtual Product Product { get; set; }
    }
}
