using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Modes.DTOS.Address;

namespace WebApi.Extenstions
{
    public static class AddressExtenstions
    {
        public static IQueryable<AddressDTO> ProjectAddressToAddressDto(this IQueryable<Address> query)
        {
            return query.Select(address => new AddressDTO
            {
                Id = address.Id,
                AccountID = address.AccountID,
                Account = address.Account,
                AddressInformations = address.AddressInformations,
                Status = address.Status
            }).AsNoTracking();
        }
    }
}
