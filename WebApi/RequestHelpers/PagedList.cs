using WebApi.Models;
using WebApi.Modes;

namespace WebApi.RequestHelpers
{
    public class PagedList<T> : List<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize) //จำนวนหน้าทั้งหมด
            };
            AddRange(items); // List<T>.AddRange(items)
        }

        //PagedList<Product> : List<Product> กำหนดชนิดเป็นลิสต์ของ product
        public static async Task<PagedList<T>> ToPagedList(IEnumerable<T> query,
            int pageNumber, int pageSize)
        {
            var count =  query.Count();
            var items =  query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
