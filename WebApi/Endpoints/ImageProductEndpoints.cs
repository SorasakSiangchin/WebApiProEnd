using WebApi.Repositorys.IRepositorys;
using System.Net;
using WebApi.Modes.DTOS.ImageProduct;
using AutoMapper;
using WebApi.Modes;
using WebApi.Models;

namespace WebApi.Endpoints
{
    public static class ImageProductEndpoints
    {
        public static void ConfigureImageProductEndpoints(this WebApplication app)
        {
            app.MapGet("/imageproduct/{productID}", GetImageProduct).WithName("GetImageProduct").Produces<APIResponse>(200);
            app.MapPost("/imageproduct", CreateImageProduct).WithName("CreateImageProduct").Accepts<ImageProductDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ;
            app.MapDelete("/imageproduct/{id}", DeleteImageProduct).WithName("DeleteImageProduct");
        }
        private static async Task<IResult> GetImageProduct(string productID, IImageProductRepository _imageProductRepo)
        {
            APIResponse response = new();
            var result = await _imageProductRepo.GetAllAsync(productID);
            response.Result = result.Select(ImageProductResponse.FromImageProduct);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private static async Task<IResult> CreateImageProduct(IMapper _mapper, ImageProductDTO model, IImageProductRepository _imageProductRepo)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            (string errorMessage, List<string> imageName) = await _imageProductRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                response.ErrorMessages.Add(errorMessage);
                return Results.Ok(response);
            }
            var imageProduct = _mapper.Map<ImageProduct>(model);
            await _imageProductRepo.CreactAsync(imageProduct, imageName);
            response.Result = imageProduct;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private static async Task<IResult> DeleteImageProduct(IImageProductRepository _imageProductRepo, string id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            var imageProduct = await _imageProductRepo.GetAsync(id);
            if (imageProduct != null)
            {
                await _imageProductRepo.RemoveAsync(imageProduct);
                await _imageProductRepo.DeleteImage(imageProduct.ImageUrl);
            }
            else
            {
                response.ErrorMessages.Add("รหัสไม่ถูกต้อง");
                return Results.BadRequest(response);
            }

            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.NoContent;
            return Results.Ok(response);
        }
    }
}
