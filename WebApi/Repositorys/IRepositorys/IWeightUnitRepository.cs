using WebApi.Models;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IWeightUnitRepository 
    {
        Task<ICollection<WeightUnit>> GetAllAsync();
    }
}
