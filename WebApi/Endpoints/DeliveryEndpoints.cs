using WebApi.Models;
using System.Net;
using WebApi.Repositorys.IRepositorys;
using AutoMapper;
using WebApi.Models.DTOS.Delivery;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Endpoints
{
    public static class DeliveryEndpoints
    {
        public static void ConfigureDeliveryEndpoints(this WebApplication app)
        {
            app.MapGet("delivery/orderId", GetDeliveryByOrderId).WithName("GetDeliveryByOrderId").Produces<APIResponse>(); 
            app.MapPost("delivery", CreateDelivery).WithName("CreateDelivery").Accepts<DeliveryRequestDTO>("multipart/form-data").Produces<APIResponse>(200); 
            app.MapPost("delivery/put", UpdateDelivery).WithName("UpdateDelivery").Accepts<DeliveryRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400);
        }
        
        private async static Task<IResult> GetDeliveryByOrderId(IDeliveryRepository _deliveryRepo, string orderId)
        {
            APIResponse response = new();
            var data = _deliveryRepo.GetByOrderId(orderId).GetAwaiter().GetResult();
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        [Authorize(Roles = "admin,seller")]
        private static async Task<IResult> CreateDelivery(IMapper _mapper, IDeliveryRepository _deliveryRepo, DeliveryRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var delivery = _mapper.Map<Delivery>(model);
            await _deliveryRepo.CreateAsync(delivery);
            await _deliveryRepo.SaveAsync();
            response.Result = delivery;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }
        [Authorize(Roles = "admin,seller")]
        private static async Task<IResult> UpdateDelivery(IMapper _mapper, IDeliveryRepository _deliveryRepo, DeliveryRequestDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var delivery = await _deliveryRepo.GetAsync(model.Id, tracked: false);
            if (delivery == null) return Results.NotFound();
            var result = _mapper.Map<Delivery>(model);
            await _deliveryRepo.UpdateAsync(result);
            await _deliveryRepo.SaveAsync();
            response.Result = result;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }
    }
}
