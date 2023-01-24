


using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Modes.DTOS.Order;
using WebApi.Models.OrderAggregate;

namespace WebApi.Extenstions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDTO> ProjectOrderToOrderDto(this IQueryable<Order> query)
        {
            return query
                .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    OrderCancel = order.OrderCancel ,
                    Address = order.Address,
                    AccountID = order.Address.AccountID,
                    Created = order.Created,
                    DeliveryFee = order.DeliveryFee,
                    Subtotal = order.Subtotal,
                    OrderStatus = order.OrderStatus.ToString(),
                    Total = order.GetTotal(),
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Amount = item.Amount,
                        ImageUrl = item.ItemOrdered.ImageUrl,
                        Name = item.ItemOrdered.Name,
                        Price = item.Price,
                        ProductID = item.ItemOrdered.ProductID
                    }).ToList()
                }).AsNoTracking();
        }
    }
}
