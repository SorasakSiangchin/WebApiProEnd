using WebApi.Models;
using WebApi.Modes.DTOS.Address;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IAddressRepository
    {
        Task<List<Address>> GetAllAsync(string accountId, bool tracked = true);
        Task<Address> GetAsync(int id, bool tracked = true);
        Task CreactAsync(Address address);
        Task UpdateAsync(Address address);
        Task UpdateRangeAsync(List<Address> address);
        Task RemoveAsync(Address address);
    }
}
