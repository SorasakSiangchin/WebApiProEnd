namespace WebApi.RequestHelpers
{
    public class ProductParams : PaginationParams
    {
        public string? Category { get; set; }
        public int RangePriceStart { get; set; } = 0;
        public int RangePriceEnd { get; set; } = 0;
        public string? SearchTerm { get; set; }
    }
}
