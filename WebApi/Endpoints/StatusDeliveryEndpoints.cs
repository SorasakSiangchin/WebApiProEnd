using WebApi.Models;
using System.Net;
using WebApi.Repositorys.IRepositorys;
using AutoMapper;
using WebApi.Models.DTOS.StatusDelivery;

namespace WebApi.Endpoints
{
    public static class StatusDeliveryEndpoints
    {
        public static void ConfigureStatusDeliveryEndpoints(this WebApplication app)
        {
            app.MapGet("statusDeliverys", GetAllStatusDeliverys).WithName("GetAllStatusDeliverys").Produces<APIResponse>(200);
            app.MapPost("statusDelivery", CreateStatusDelivery).WithName("CreateStatusDelivery").Accepts<StatusDeliveryDTO>("application/json").Produces<APIResponse>(200).Produces(400);
            app.MapPut("statusDelivery", UpdateStatusDelivery).WithName("UpdateStatusDelivery").Accepts<StatusDeliveryDTO>("application/json").Produces<APIResponse>(200).Produces(400).Produces(404);
            app.MapDelete("statusDelivery/{id}", DeleteStatusDelivery);
        }

        private static async Task<IResult> GetAllStatusDeliverys(IStatusDeliveryRepository _statusDeliveryRepo)
        {
            APIResponse response = new();
            var status = await _statusDeliveryRepo.GetAllAsync();
            response.Result = status;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> CreateStatusDelivery(IMapper _mapper, IStatusDeliveryRepository _statusDeliveryRepo,  StatusDeliveryDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            var statusDelivery = _mapper.Map<StatusDelivery>(model);
            await _statusDeliveryRepo.CreateAsync(statusDelivery);
            await _statusDeliveryRepo.SaveAsync();
            response.Result = statusDelivery;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateStatusDelivery(IMapper _mapper, IStatusDeliveryRepository _statusDeliveryRepo, StatusDeliveryDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var statusDelivery = await _statusDeliveryRepo.GetAsync(model.Id, tracked: false);
            if (statusDelivery == null) return Results.NotFound();
            var data = _mapper.Map<StatusDelivery>(model);
            await _statusDeliveryRepo.UpdateAsync(data);
            await _statusDeliveryRepo.SaveAsync();
            response.Result = data;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> DeleteStatusDelivery(IStatusDeliveryRepository _statusDeliveryRepo, int id)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var statusDelivery = await _statusDeliveryRepo.GetAsync(id);
            if (statusDelivery != null)
            {
                await _statusDeliveryRepo.RemoveAsync(statusDelivery);
                await _statusDeliveryRepo.SaveAsync();
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
