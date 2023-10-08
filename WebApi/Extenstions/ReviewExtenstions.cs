using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Models;
using WebApi.Models.DTOS.Review;
using WebApi.Modes;
using WebApi.Settings;
using static System.Net.Mime.MediaTypeNames;

namespace WebApi.Extenstions
{
    public static class ReviewExtenstions
    {
        public static IQueryable<Review> ByProductID(this IQueryable<Review> query, string productId)
        {
            if (string.IsNullOrEmpty(productId)) return query;
            return query.Where(p => p.OrderItem.ItemOrdered.ProductID.Equals(productId));
        }

        public static IQueryable<Review> FilterRate(this IQueryable<Review> query, int score)
        {
            if (score == 0) return query;
            return query.Where(p => p.Score == score);
        }

        public static IQueryable<ReviewDTO> ProjectReviewToReviewDTO(this IQueryable<Review> query, ApplicationDbContext db)
        {
            return query
                .Select(review => new ReviewDTO
                {
                    Id = review.Id,
                    AccountID = review.AccountID,
                    Created = review.Created,
                    Information = review.Information,
                    OrderItemID = review.OrderItemID,
                    Score = review.Score,
                    VdoUrl = !string.IsNullOrEmpty(review.VdoUrl) ? $"{ApplicationUrl.UrlServer}/review/{review.VdoUrl}" : "",
                    imageReviews = db.ImageReviews.Where(e => e.ReviewID.Equals(review.Id)).Select(
                       image => new ImageReview
                       {
                           Id = image.Id,
                           ReviewID = image.ReviewID,
                           ImageUrl = !string.IsNullOrEmpty(image.ImageUrl) ? image.ImageUrl : "",
                       }).ToList(),
                    Account = AccountResponse.FromAccount(db.Accounts.Include(e => e.Role).FirstOrDefault(e => e.Id.Equals(review.AccountID)))
                }).AsNoTracking();
        }
        

    }
}
