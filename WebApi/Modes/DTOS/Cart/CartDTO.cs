namespace WebApi.Modes.DTOS.Cart
{
    public class CartDTO
    {
        public string Id { get; set; }
        public string AccountID { get; set; }
        public List<CartItemDTO>? Items { get; set; }
    }
}
