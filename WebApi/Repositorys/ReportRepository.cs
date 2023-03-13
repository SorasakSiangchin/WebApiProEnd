using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Report;
using WebApi.Models.DTOS.Repost;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IProductRepository _productRepo;

        public ReportRepository(ApplicationDbContext db, IProductRepository productRepo)
        {
            _db = db;
            _productRepo = productRepo;
        }

        public async Task<List<ProductStatisticsDTO>> ProductStatistics(ProductStatisticsRequestDTO requestDTO)
        {
            List<ProductStatisticsDTO> ProductStatistics = new();
            var orders = await _db.Orders
                .RangeTime(requestDTO.DateStart, requestDTO.DateEnd)
                .ProjectOrderToOrderDTO(_db)
                .ToListAsync();

            foreach (var order in orders)
            {
                var items = order.OrderItems.Where(e => e.AccountID == requestDTO.AccountId);
                if (items.Count() > 0)
                {
                    foreach (var item in order.OrderItems)
                    {
                        var product = await _productRepo.GetAsync(item.ProductID, tracked: false);
                        if (product != null)
                        {
                            var productStatistic = ProductStatistics.FirstOrDefault(e => e.Product.Id.Equals(product.Id));
                            if (productStatistic != null) productStatistic.Amount += item.Amount;
                            else ProductStatistics.Add(new ProductStatisticsDTO { Product = product, Amount = item.Amount });

                        }
                    };
                }
            };


            var sum = ProductStatistics.Sum(x => x.Amount);

            foreach (var item in ProductStatistics) item.NumPercen = item.Amount * 100 / sum;

            return ProductStatistics.OrderByDescending(e => e.NumPercen).ToList();
        }

        public async Task<SalesStatisticsDTO> SalesStatistics(SalesStatisticsRequestDTO requestDTO)
        {
            SalesStatisticsDTO SalesStatistics = new();
            var Year = requestDTO.Year.Value.Year;
            var orders = await _db.Orders.FilterByYear(Year).ProjectOrderToOrderDTO(_db).ToListAsync();
            //SalesStatistics.TotalPrice = orders.Sum(x => x.Subtotal);
            foreach (var order in orders)
            {
                var items = order.OrderItems.Where(e => e.AccountID == requestDTO.AccountId);
                if (items.Count() > 0)
                {
                    
                    
                    var result = SalesStatistics.Sales.Find(x => x.Month == order.Created.Value.Month && x.Year == order.Created.Value.Year);
                    if (result == null)
                    {
                        SalesStatistics.Sales.Add(new SalesStatisticeItemDTO
                        {
                            price = order.Subtotal,
                            FullTime = order.Created,
                            Day = order.Created.Value.Day,
                            Month = order.Created.Value.Month,
                            Year = order.Created.Value.Year
                        });
                    }
                    else
                    {
                        result.price += order.Subtotal;
                    }
                }
               
            }
            SalesStatistics.TotalPrice = SalesStatistics.Sales.Sum(e => e.price);
            foreach (var item in SalesStatistics.Sales)
            {
                var Percen = item.price * 100 / SalesStatistics.TotalPrice;
                item.percent = Percen;
            }
            SalesStatistics.Sales = SalesStatistics.Sales.OrderByDescending(e => e.Month).ToList();


            return SalesStatistics;
        }
    }
}
