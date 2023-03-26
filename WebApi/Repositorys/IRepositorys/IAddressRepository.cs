using WebApi.Models;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync(string accountId, bool tracked = true);
        Task<Address> GetAsync(int id, bool tracked = true);
        Task CreateAsync(Address address);
        Task UpdateAsync(Address address);
        Task UpdateRangeAsync(List<Address> address);
        Task RemoveAsync(Address address);
    }
}
