using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IDetailProductRepository :  IRepository<DetailProduct>
    {
        Task<DetailProduct> GetAsync(int? id, bool tracked = true);
        Task<DetailProduct> GetByIdProductAsync(string idProduct, bool tracked = true);

    }
}
