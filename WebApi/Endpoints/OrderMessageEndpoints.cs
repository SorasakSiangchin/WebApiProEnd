using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Net;
using WebApi.Models;
using WebApi.Models.DTOS.OrderMessage;
using WebApi.Models.OrderAggregate;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class OrderMessageEndpoints
    {
        public static void ConfigureOrderMessageEndpoints(this WebApplication app)
        {
            app.MapPost("/orderMessage", CreateOrderMessage).WithName("CreateOrderMessage").Accepts<OrderMessageRequest>("application/json").Produces<APIResponse>(200);
        }

        private static async Task<IResult> CreateOrderMessage(ApplicationDbContext db, IMapper _mapper, IOrderRepository _orderRepo ,IOrderMessageRepository _orderMessageRepo, OrderMessageRequest model)
        {
            APIResponse response = new() { IsSuccess = false, StatusCode = HttpStatusCode.BadRequest };
            var order = await db.Orders.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(model.OrderID));
            var orderMessage = _mapper.Map<OrderMessage>(model);
           
            order.OrderCancel = true;
            await _orderRepo.UpdateAsync(order);

            orderMessage.Created = DateTime.Now;
            await _orderMessageRepo.CreateAsync(orderMessage);
            await _orderMessageRepo.SaveAsync();

            response.Result = orderMessage;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.Created;
            return Results.Ok(response);
        }

    }
}
