using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IDetailProductRepository 
    {
        Task<DetailProduct> GetAsync(int? id, bool tracked = true);
        Task<DetailProduct> GetByIdProductAsync(string idProduct, bool tracked = true);
        Task CreactAsync(DetailProduct detailProduct);
        Task UpdateAsync(DetailProduct detailProduct);
        Task RemoveAsync(DetailProduct detailProduct);
        Task SaveAsync();
    }
}
