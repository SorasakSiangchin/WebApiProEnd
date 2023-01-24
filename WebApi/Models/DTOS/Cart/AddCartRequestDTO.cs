namespace WebApi.Modes.DTOS.Cart
{
    public class AddCartRequestDTO
    {
       public string productId { get; set; }
        public int amount { get; set; }
        public string accountId { get; set; }
    }
}
