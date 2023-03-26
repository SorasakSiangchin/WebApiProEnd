using WebApi.Models;
using WebApi.Models.DTOS.Review;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IReviewRepository
    {
        Task<ReviewAverageDTO> GetByProductId(ReviewParams reviewParams);
        Task<ReviewDTO> GetByOrderItemId(int id);
        Task<(string errorImage, List<string> imageNames)> UploadImage(IFormFileCollection formFiles);
        Task<(string errorVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles);
        Task CreactImageAsync(List<string> imageNames, int reviewId);
        Task CreactAsync(Review review);
        Task SaveAsync();
    }
}
