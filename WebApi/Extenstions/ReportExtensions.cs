using WebApi.Models;
using WebApi.Models.OrderAggregate;

namespace WebApi.Extenstions
{
    public static class ReportExtensions
    {
        public static IQueryable<OrderDTO> ByAccountID(this IQueryable<OrderDTO> query, string accountId)
        {
            if (string.IsNullOrEmpty(accountId)) return query ;
            List<OrderDTO> Order  = new();
            foreach (var item in query)
            {
               var items = item.OrderItems.Where(e => e.AccountID == accountId);
                if (items != null)
                {
                    Order.Add(item);
                }
            }
            return Order.AsQueryable();
        }

        public static IQueryable<Order> RangeTime(this IQueryable<Order> query, DateTime? DateStart, DateTime? DateEnd)
        {
            if (DateStart.Value.Date.ToString() == "1/1/0544 0:00:00" || DateEnd.Value.Date.ToString() == "1/1/0544 0:00:00") return query;

            Console.WriteLine(DateStart.Value.Date);
            query = query.Where(p => p.Created.Date >= DateStart.Value.Date && p.Created.Date <= DateEnd.Value.Date);
            return query;
        }

        public static IQueryable<Order> FilterByYear(this IQueryable<Order> query, int? Year)
        {
            if (Year == 1 || Year == null) return query.Where(e => e.Created.Year == DateTime.Now.Year);
            query = query.Where(e => e.Created.Year == Year);
            return query;
        }
    }
}
