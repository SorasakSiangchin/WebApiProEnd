using System.Net;
using AutoMapper;
using WebApi.Models;
using WebApi.Models.DTOS.DetailProduct;
using WebApi.Modes;
using WebApi.Modes.DTOS.Product;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class DetailProductEndpoints
    {
        public static void ConfigureDetailProductEndpoints(this WebApplication app) {
            app.MapGet("detailProduct/idProduct", GetDetailProduct).WithName("GetDetailProduct").Produces<APIResponse>(200);
            app.MapPost("detailProduct", CreateDetailProduct).WithName("CreateDetailProduct").Accepts<DetailProductDTO>("application/json").Produces<APIResponse>(200);
            app.MapPut("detailProduct", UpdateDetailProduct).WithName("UpdateDetailProduct").Accepts<DetailProductDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapDelete("detailProduct/{id:int}", DeleteDetailProduct).WithName("DeleteDetailProduct");
        }

        private static async Task<IResult> GetDetailProduct(IDetailProductRepository _detailProductRepo, string idProduct)
        {
            APIResponse response = new();
            var data = await _detailProductRepo.GetByIdProductAsync(idProduct);
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> CreateDetailProduct(IMapper _mapper, IDetailProductRepository _detailProductRepo, DetailProductDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var detailProduct = _mapper.Map<DetailProduct>(model);
            await _detailProductRepo.CreactAsync(detailProduct);
            await _detailProductRepo.SaveAsync();
            response.Result = detailProduct;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private static async Task<IResult> UpdateDetailProduct(IMapper _mapper, IDetailProductRepository _detailProductRepo, DetailProductDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var detailProduct = await _detailProductRepo.GetAsync(model.Id , tracked : false);
            if (detailProduct == null) return Results.NotFound();
            await _detailProductRepo.UpdateAsync(_mapper.Map<DetailProduct>(model));
            await _detailProductRepo.SaveAsync();
            response.Result = detailProduct;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private static async Task<IResult> DeleteDetailProduct(IMapper _mapper, IDetailProductRepository _detailProductRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var detailProduct = await _detailProductRepo.GetAsync(id);
            if (detailProduct != null)
            {
                await _detailProductRepo.RemoveAsync(detailProduct);
                await _detailProductRepo.SaveAsync();
                response.IsSuccess = true;
                response.StatusCode = HttpStatusCode.NoContent;
                return Results.Ok(response);
            }
            else
            {
                response.ErrorMessages.Add("รหัสไม่ถูกต้อง");
                return Results.BadRequest(response);
            }
        }
    }
}
