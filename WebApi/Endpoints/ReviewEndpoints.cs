
using AutoMapper;
using System.Net;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Review;
using WebApi.Modes.DTOS.Product;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;

namespace WebApi.Endpoints
{
    public static class ReviewEndpoints
    {
        public static void ConfigureReviewEndpoints(this WebApplication app)
        {
            app.MapGet("review/orderItemId", GetReviewByOrderItemId).WithName("GetReviewByOrderItemId").Produces<APIResponse>(200);
            app.MapPost("review/productId", GetReviewByProductId).WithName("GetReviewByProductId").Accepts<ReviewParams>("application/json").Produces<APIResponse>(200);
            app.MapPost("review", CreateReview).WithName("CreateReview").Accepts<ReviewRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400);
        }

        private async static Task<IResult> CreateReview(IMapper _mapper, IReviewRepository _reviewRepo, ReviewRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            (string errorImage, List<string> imageNames) = await _reviewRepo.UploadImage(model.ImageFiles);
            if (!string.IsNullOrEmpty(errorImage))
            {
                response.ErrorMessages.Add(errorImage);
                return Results.BadRequest(response);
            }
            (string errorVedio, string vedioName) = await _reviewRepo.UploadVedio(model.VideoFiles);
            if (!string.IsNullOrEmpty(errorVedio))
            {
                response.ErrorMessages.Add(errorVedio);
                return Results.BadRequest(response);
            }
            var review = _mapper.Map<Review>(model);
            review.VdoUrl = vedioName;
            review.Created = DateTime.Now;
            await _reviewRepo.CreactAsync(review);
            await _reviewRepo.SaveAsync();
            await _reviewRepo.CreactImageAsync(imageNames, review.Id);
            await _reviewRepo.SaveAsync();
            response.Result = review;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetReviewByProductId(HttpResponse httpResponse, IReviewRepository _reviewRepo, ReviewParams reviewParams)
        {
            APIResponse response = new();
            var query = _reviewRepo.GetByProductId(reviewParams).GetAwaiter().GetResult();
            var reviews = await PagedList<ReviewDTO>.ToPagedList(query.Reviews,
                        reviewParams.PageNumber, reviewParams.PageSize);
            httpResponse.AddPaginationHeader(reviews.MetaData);
            response.Result = new ReviewAverageDTO { AverageScore = query.AverageScore, Reviews = reviews };
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetReviewByOrderItemId(IReviewRepository _reviewRepo,int orderItemId )
        {
            APIResponse response = new();
            var query = _reviewRepo.GetByOrderItemId(orderItemId).GetAwaiter().GetResult();
            response.Result = query ;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
