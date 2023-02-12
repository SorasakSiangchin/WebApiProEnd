﻿using WebApi.Models;
using WebApi.Modes.DTOS.Cart;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Modes;
using System.Net;
using WebApi.Modes.DTOS.Order;
using Microsoft.Identity.Client;

namespace WebApi.Endpoints
{
    public static class OrderEndpoints
    {
        public static void ConfigureOrderEndpoints(this WebApplication app)
        {
            app.MapGet("orders", GetAllOrder).WithName("GetAllOrder").Produces<APIResponse>(200);
            app.MapPost("orders/accountId", GetAccountID).WithName("GetAccountID").Accepts<AccountIDRequest>("application/json").Produces<APIResponse>(200);
            app.MapGet("order/{Id}", GetOrder).WithName("GetOrder").Produces<APIResponse>(200);
            app.MapPost("orders", CreatOrder).WithName("CreatOrder").Accepts<CreateOrderDto>("application/json").Produces<APIResponse>(200).Produces(400);

        }
        private async static Task<IResult> GetAllOrder(IOrderRepository _orderRepo, string accountId)
        {
            APIResponse response = new();
            var orders = _orderRepo.GetAllAsync(accountId).GetAwaiter().GetResult();
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
        private async static Task<IResult> GetOrder(IOrderRepository _orderRepo, string Id, string accountId)
        {
            APIResponse response = new();
            var orders = _orderRepo.GetAsync(Id, accountId).GetAwaiter().GetResult();
            if (orders == null)
            {
                response.ErrorMessages.Add("ไม่พบข้อมูล");
                return Results.Ok(response);
            }
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> CreatOrder(IOrderRepository _orderRepo, CreateOrderDto model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            _orderRepo.CreactAsync(model).GetAwaiter().GetResult();
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

        public class AccountIDRequest
        {
            public string cartId { get; set; }
            public int[] cartItemId { get; set; }
        }
    }
}
