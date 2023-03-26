using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Modes;
using WebApi.Modes.DTOS.Order;
using WebApi.Settings;

namespace WebApi.Extenstions
{
    public static class OrderExtensions
    {
        public static IQueryable<OrderDTO> ProjectOrderToOrderDTO(this IQueryable<Order> query, ApplicationDbContext db)
        {
            return query
                .Select(order => new OrderDTO
                {
                    Id = order.Id,
                    CustomerStatus = order.CustomerStatus,
                    OrderCancel = order.OrderCancel,
                    Address = order.Address,
                    AccountID = order.Address.AccountID,
                    Created = order.Created,
                    DeliveryFee = order.DeliveryFee,
                    Subtotal = order.Subtotal,
                    OrderStatus = (int)order.OrderStatus,
                    Total = order.GetTotal(),
                    PaymentMethod = order.PaymentMethod,
                    ClientSecret = order.ClientSecret,
                    PaymentIntentId = order.PaymentIntentId,
                    OrderUsage = order.OrderUsage,
                    orderMessage = db.OrderMessages.Where(e => e.OrderID.Equals(order.Id)).Select(e => FromOrderMessage(e, db)).ToList(),
                    evidenceMoney = FromEvidenceMoneyTransfer(db.EvidenceMoneyTransfers.AsNoTracking().Where(x => x.OrderID == order.Id && x.Status == true).FirstOrDefault()),
                    OrderItems = db.OrderItems.Where(e => e.OrderID == order.Id).Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        ImageUrl = item.ItemOrdered.ImageUrl,
                        Name = item.ItemOrdered.Name,
                        Price = item.Price,
                        ProductID = item.ItemOrdered.ProductID,
                        AccountID = db.Products.FirstOrDefault(e => e.Id == item.ItemOrdered.ProductID).AccountID
                    }).ToList()
                }).AsNoTracking();
        }

        private static EvidenceMoneyTransfer FromEvidenceMoneyTransfer(EvidenceMoneyTransfer evidence)
        {
            if (evidence == null) return null;
            return new EvidenceMoneyTransfer
            {
                Created = evidence.Created,
                Evidence = !string.IsNullOrEmpty(evidence.Evidence) ? $"{ApplicationUrl.Url}/evidenceMoneyTransfer/{evidence.Evidence}" : "",
                Id = evidence.Id,
                OrderID = evidence.OrderID,
            };
        }

        private static OrderMessageDTO FromOrderMessage(OrderMessage orderMessage, ApplicationDbContext db)
        {
            if (orderMessage == null) return null;
            return new OrderMessageDTO
            {
                OrderID = orderMessage.OrderID,
                AccountID = orderMessage.AccountID,
                Created = orderMessage.Created,
                Id = orderMessage.Id,
                Message = orderMessage.Message,
                account = AccountResponse.FromAccount(db.Accounts.Include(e => e.Role).FirstOrDefault(e => e.Id.Equals(orderMessage.AccountID))) 
            };
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
            OrderStatus check;
            if (string.IsNullOrEmpty(orderStatus)) return query;
            check = (OrderStatus)Convert.ToInt32(orderStatus);
            return query.Where(p => p.OrderStatus == check);
        }


        public static IQueryable<Order> HaveEvidence(this IQueryable<Order> query, bool haveEvidence)
        {
            if (haveEvidence == false) return query;
            return query.Where(p => p.OrderStatus != OrderStatus.WaitingForPayment);
        }

        public static IQueryable<Order> FilterOrderUsage(this IQueryable<Order> query, string orderUsage)
        {
            OrderUsage check;
            if (string.IsNullOrEmpty(orderUsage)) return query;
            check = (OrderUsage)Convert.ToInt32(orderUsage);
            return query.Where(p => p.OrderUsage == check);
        }
    }
}
