using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes.DTOS.Order;

namespace WebApi.Extenstions
{
    public static class OrderExtensions
    {

        public static IQueryable<OrderDTO> ProjectOrderToOrderDTO(this IQueryable<Order> query , ApplicationDbContext db)
        {
            return query
                .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    CustomerStatus= order.CustomerStatus,
                    OrderCancel = order.OrderCancel,
                    Address = order.Address,
                    AccountID = order.Address.AccountID,
                    Created = order.Created,
                    DeliveryFee = order.DeliveryFee,
                    Subtotal = order.Subtotal,
                    OrderStatus = (int)order.OrderStatus,
                    Total = order.GetTotal(),
                    OrderItems = db.OrderItems.Where(e => e.OrderID == order.Id).Select( item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        ImageUrl = item.ItemOrdered.ImageUrl,
                        Name = item.ItemOrdered.Name,
                        Price = item.Price,
                        ProductID = item.ItemOrdered.ProductID ,
                        AccountID = db.Products.FirstOrDefault(e=>e.Id == item.ItemOrdered.ProductID).AccountID
                    }).ToList()
                }).AsNoTracking();
        }

        public static IQueryable<Order> Search(this IQueryable<Order> query, string id)
        {
            if (string.IsNullOrEmpty(id)) return query;

            return query.Where(p => p.Id.Equals(id));
        }

        public static IQueryable<Order> ByAccountID(this IQueryable<Order> query, string accountId)
        {
            if (string.IsNullOrEmpty(accountId)) return query;

            return query.Where(p => p.Address.AccountID.Equals(accountId));
        }

        public static IQueryable<Order> FilterCancel(this IQueryable<Order> query, string orderCancel)
        {
            if (string.IsNullOrEmpty(orderCancel)) return query;
            return query.Where(p => p.OrderCancel == Convert.ToBoolean(orderCancel));
        }

        public static IQueryable<Order> FilterStatus(this IQueryable<Order> query, string orderStatus)
        {
            OrderStatus check ;
            if (string.IsNullOrEmpty(orderStatus)) return query;
            check = (OrderStatus)Convert.ToInt32(orderStatus);
            return query.Where(p => p.OrderStatus == check);
        }
    }
}
