using WebApi.Models;
using WebApi.Modes.DTOS.Address;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IAddressRepository
    {
        Task<ICollection<AddressDTO>> GetAllAsync(string accountId);
        Task<AddressDTO> GetAsync(int id , string accountId);
        Task CreactAsync(Address address);
        Task UpdateAsync(Address address);
        Task RemoveAsync(Address address);
    }
}
