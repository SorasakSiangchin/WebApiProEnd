namespace WebApi.RequestHelpers
{
    public class ReviewParams : PaginationParams
    {
        public string productId { get; set; }
        public int score { get; set; }
    }
}
