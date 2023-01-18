using WebApi.Settings;
using WebApiProjectEnd.Modes;

namespace WebApi.Modes
{
    public class ImageProductResponse
    {
        public string Id { get; set; }
        public string ImageUrl { get; set; }
        public string ProductID { get; set; }

         public static ImageProductResponse FromImageProduct(ImageProduct imageProduct)
        {
            return new ImageProductResponse
            {
                Id = imageProduct.Id,
                ImageUrl = !string.IsNullOrEmpty(imageProduct.ImageUrl) ? $"{applicationUrl.Url}/imageProduct/{imageProduct.ImageUrl}" : "",
                ProductID = imageProduct.ProductID
            };
        }
    }
}
