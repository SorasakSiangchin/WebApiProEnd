using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Net;
using System.Net.Http;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Order;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.DTOS.Order;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using Order = WebApi.Models.OrderAggregate.Order;

namespace WebApi.Endpoints
{
    public static class OrderEndpoints
    {
        public static void ConfigureOrderEndpoints(this WebApplication app)
        {
            app.MapGet("orders/accountId", GetOrderByAccountId).WithName("GetOrderByAccountId").Produces<APIResponse>(200);
            app.MapPost("orders/accountId", GetAccountID).WithName("GetAccountID").Accepts<AccountIDRequest>("application/json").Produces<APIResponse>(200);
            app.MapGet("order/{Id}", GetOrder).WithName("GetOrder").Produces<APIResponse>(200);

            app.MapPost("orders", GetOrderAll).WithName("GetOrderAll").Accepts<OrderParams>("application/json").Produces<APIResponse>(200);

            app.MapPost("order", CreatOrder).WithName("CreatOrder").Accepts<CreateOrderDTO>("application/json").Produces<APIResponse>(200);
            app.MapPut("order", UpdateOrder).WithName("UpdateOrder").Accepts<UpdateOrderDTO>("application/json").Produces<APIResponse>(200);
       
            app.MapPost("order/stripe/webhook", StripeWebhook).WithName("StripeWebhook").Produces<APIResponse>(200);

        }
        private async static Task<IResult> GetOrderByAccountId(IOrderRepository _orderRepo, string accountId)
        {
            APIResponse response = new();
            var orders = _orderRepo.GetByAccountIdAsync(accountId).GetAwaiter().GetResult();
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> GetOrder(IOrderRepository _orderRepo, string Id)
        {
            APIResponse response = new();
            var orders = _orderRepo.GetAsync(Id).GetAwaiter().GetResult();
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetOrderAll(HttpResponse httpResponse, IOrderRepository _orderRepo, OrderParams orderParams)
        {
            APIResponse response = new();
            var query = _orderRepo.GetAllAsync(orderParams).GetAwaiter().GetResult();
            var orders = await PagedList<OrderDTO>.ToPagedList(query,
                        orderParams.PageNumber, orderParams.PageSize);
            httpResponse.AddPaginationHeader(orders.MetaData);
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreatOrder(IOrderRepository _orderRepo, CreateOrderDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            _orderRepo.CreateAsync(model).GetAwaiter().GetResult();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> UpdateOrder(IMapper _mapper, IOrderRepository _orderRepo, [FromBody] UpdateOrderDTO model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var order = _mapper.Map<Order>(_orderRepo.GetAsync(model.Id).GetAwaiter().GetResult());
            _mapper.Map(model, order);
            _orderRepo.UpdateAsync(order).GetAwaiter().GetResult();
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetAccountID(IOrderRepository _orderRepo, AccountIDRequest model)
        {
            APIResponse response = new();
            var accountIds = _orderRepo.GetAccountIdAsync(model.cartItemId, model.cartId).GetAwaiter().GetResult();
            response.Result = accountIds;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> StripeWebhook(ApplicationDbContext db , IConfiguration _config , HttpContext httpContext , HttpRequest httpRequest)
        {
            #region รับค่าเข้ามาจาก Webhook และได้รับออกเจค
            var json = await new StreamReader(httpContext.Request.Body).ReadToEndAsync();
            var stripeEvent = EventUtility.ConstructEvent(json, httpRequest.Headers["Stripe-Signature"],
                _config["StripeSettings:WhSecret"], throwOnApiVersionMismatch: false);
            var charge = (Charge)stripeEvent.Data.Object;
            #endregion

            //ค้นหา order ตาม PaymentIntentId
            var order = await db.Orders.FirstOrDefaultAsync(x =>
                x.PaymentIntentId == charge.PaymentIntentId);

            //เปลี่ยน OrderStatus ตามเหตุการณ์ที่ได้รับมา
            if (charge.Status == "succeeded") order.OrderStatus = OrderStatus.SuccessfulPayment;

            await db.SaveChangesAsync();

            return Results.Ok(new EmptyResult());
        }
        

        private class AccountIDRequest
        {
            public string cartId { get; set; }
            public int[] cartItemId { get; set; }
        }
    }
}

  

