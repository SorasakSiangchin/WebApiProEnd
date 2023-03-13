namespace WebApi.Models.DTOS.Review
{
    public class ReviewAverageDTO
    {
        public double AverageScore { set; get; }
        public List<ReviewDTO> Reviews { set; get; }
    }
}
