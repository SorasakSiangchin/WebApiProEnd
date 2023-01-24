using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string? Color { get; set; }
        public double? Weight { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; }
        public string AccountID { get; set; }
        public DateTime? Created { get; set; } = DateTime.Now;
        public DateTime? LastUpdate { get; set; }
        public int WeightUnitID { get; set; }
        public int CategoryProductID { get; set; }
        [ForeignKey("WeightUnitID")]
        public virtual WeightUnit WeightUnit { get; set; }
        [ForeignKey("CategoryProductID")]
        public virtual CategoryProduct CategoryProduct { get; set; }

    }
}
