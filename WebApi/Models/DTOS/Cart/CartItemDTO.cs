namespace WebApi.Modes.DTOS.Cart
{
    public class CartItemDTO
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryProductName { get; set; }
        public int Amount { get; set; }
    }
}
