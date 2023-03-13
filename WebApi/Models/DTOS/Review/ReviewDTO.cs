using WebApi.Modes;

namespace WebApi.Models
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string? Information { get; set; }
        public string? VdoUrl { get; set; }
        public double Score { set; get; }
        public DateTime? Created { get; set; }
        public string AccountID { get; set; }
        public int OrderItemID { get; set; }
        public List<ImageReview> imageReviews { get; set; }
        public AccountResponse Account { get; set; }
    }
}
