using WebApi.Models.OrderAggregate;
using WebApi.Models;
using WebApi.Modes.DTOS.Order;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Extenstions
{
    public static class  DeliveryExtenstions
    {
        public static IQueryable<DeliveryDTO> ProjectDeliveryToDeliveryDto(this IQueryable<Delivery> query, ApplicationDbContext db)
        {
            return query.Select(delivery => new DeliveryDTO
            {
                Id= delivery.Id,
                Order= delivery.Order,
                OrderID= delivery.OrderID,
                ShippingServiceName= delivery.ShippingServiceName,
                StatusDelivery= delivery.StatusDelivery,
                StatusDeliveryID= delivery.StatusDeliveryID,
                TimeArrive = delivery.TimeArrive
            }).AsNoTracking();
        }
    }
}
