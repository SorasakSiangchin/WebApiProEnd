using WebApiProjectEnd.Modes;

namespace WebApi.Extenstions
{
    public static class ProductExtenstions
    {
        public static IQueryable<Product> Filter(this IQueryable<Product> query , string category)
        {
            var categoryList = new List<string>();

            if (!string.IsNullOrEmpty(category)) categoryList.AddRange(category.ToLower().Split(",").ToList());
            //กระบวนการวนลูปอยู่ภายใน (ทำอยู่ข้างใน)
            //รูปแบบมันจะดูยาก
            query = query.Where(p => categoryList.Count == 0 || categoryList.Contains(p.CategoryProduct.Name.ToLower()));
            return query;
        }

        public static IQueryable<Product> RangePrice (this IQueryable<Product> query, int RangeStart  , int RangeEnd )
        {
           if(RangeStart == 0 && RangeEnd == 0) return query;
            query = query.Where(p => p.Price >= RangeStart && p.Price <= RangeEnd);
            return query;
        }
    }
}
