using WebApi.Models;
using System.Net;
using AutoMapper;
using WebApi.Models.DTOS.EvidenceMoneyTransfer;
using WebApi.Repositorys.IRepositorys;
using WebApi.Models.OrderAggregate;

namespace WebApi.Endpoints
{
    public static class EvidenceMoneyTransferEndpoints
    {
        public static void ConfigureEvidenceMoneyTransferEndpoints(this WebApplication app)
        {
            app.MapGet("evidenceMoneyTransfer/orderId", GetEvidenceMoneyTransferByOrderId).WithName("GetEvidenceMoneyTransferByOrderId").Produces<APIResponse>(200);
            app.MapGet("evidenceMoneyTransfer/cancel", GetEvidenceMoneyTransferCancel).WithName("GetEvidenceMoneyTransferCancel").Produces<APIResponse>(200);
            app.MapPost("evidenceMoneyTransfer", CreateEvidenceMoneyTransfer).WithName("CreateEvidenceMoneyTransfer").Accepts<CreateEvidenceMoneyTransferDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400).Produces(404); ;
            app.MapPost("evidenceMoneyTransfer/put", UpdateEvidenceMoneyTransfer).WithName("UpdateEvidenceMoneyTransfer").Accepts<UpdateEvidenceMoneyTransferDTO>("application/json").Produces<APIResponse>(200).Produces(400); ;
          
        }

        private static async Task<IResult> GetEvidenceMoneyTransferByOrderId(IMapper _mapper, IEvidenceMoneyTransferRepository _evidenceMoneyTransferRepo , string orderId)
        {
            APIResponse response = new();
            var evidenceMoneyTransfer = await _evidenceMoneyTransferRepo.GetByOrderIdAsync(orderId);
            response.Result = evidenceMoneyTransfer;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> GetEvidenceMoneyTransferCancel(IEvidenceMoneyTransferRepository _evidenceMoneyTransferRepo, string orderId)
        {
            APIResponse response = new();
            var evidenceMoneyTransfer = await _evidenceMoneyTransferRepo.GetCancelAsync(orderId);
            response.Result = evidenceMoneyTransfer;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private static async Task<IResult> CreateEvidenceMoneyTransfer(IMapper _mapper, IOrderRepository _orderRepo, IEvidenceMoneyTransferRepository _evidenceMoneyTransferRepo, CreateEvidenceMoneyTransferDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };

            var order = await _orderRepo.GetAsync(model.OrderID);

            if (order == null)
            {
                response.ErrorMessages.Add("ไม่พบใบสั่งซื้อ");
                return Results.BadRequest(response);
            }

            (string erorrMesage, string imageName) = await _evidenceMoneyTransferRepo.UploadImage(model.FormFiles);
            if (!string.IsNullOrEmpty(erorrMesage))
            {
                response.ErrorMessages.Add(erorrMesage);
                return Results.BadRequest(response);
            }

            var evidenceMoneyTransfer = _mapper.Map<EvidenceMoneyTransfer>(model);
            evidenceMoneyTransfer.Evidence = imageName;
            evidenceMoneyTransfer.Status = true;
            evidenceMoneyTransfer.Created = DateTime.Now;
            await _evidenceMoneyTransferRepo.CreateAsync(evidenceMoneyTransfer);
            await _evidenceMoneyTransferRepo.SaveAsync();
            response.Result = evidenceMoneyTransfer;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateEvidenceMoneyTransfer(IMapper _mapper, IOrderRepository _orderRepo , IEvidenceMoneyTransferRepository _evidenceMoneyTransferRepo, UpdateEvidenceMoneyTransferDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var evidenceMoneyTransfer = await _evidenceMoneyTransferRepo.GetAsync(model.Id , tracked : false);
            var orderDto = await _orderRepo.GetAsync(model.OrderID);

            if (evidenceMoneyTransfer == null) return Results.NotFound();
            var evidence = _mapper.Map<EvidenceMoneyTransfer>(model);
            var order = _mapper.Map<Order>(orderDto);
            evidence.Evidence = evidenceMoneyTransfer.Evidence;
            evidence.Created = evidenceMoneyTransfer.Created;
            order.OrderStatus = OrderStatus.WaitingForPayment;

            await _orderRepo.UpdateAsync(order);
            await _evidenceMoneyTransferRepo.UpdateAsync(evidence);
            await _evidenceMoneyTransferRepo.SaveAsync();
            response.Result = evidence;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
