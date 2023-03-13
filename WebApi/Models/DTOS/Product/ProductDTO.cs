using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Modes;

namespace WebApi.Models.DTOS.Product
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Color { get; set; }
        public double? Weight { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string AccountID { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? LastUpdate { get; set; }
        public int WeightUnitID { get; set; }
        public int LevelProductID { get; set; }
        public int CategoryProductID { get; set; }
        public WeightUnit WeightUnit { get; set; }
        public CategoryProduct CategoryProduct { get; set; }
        public LevelProduct LevelProduct { get; set; }
        public List<ImageProductResponse> ImageProducts { get; set; }
    }
}
