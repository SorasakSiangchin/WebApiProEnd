using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Modes.DTOS.Address;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;

namespace WebApi.Repositorys
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext _db;

        public AddressRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task CreactAsync(Address entity)
        {
            await _db.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<ICollection<AddressDTO>> GetAllAsync(string accountId)
        {
            return await _db.Addresses
                .Include(x => x.Account)
                 .ProjectAddressToAddressDto()
                .Where(x => x.AccountID == accountId)
                .AsQueryable()
                .ToListAsync();
        }

        public async Task<AddressDTO> GetAsync(int id)
        {
            return await _db.Addresses
                .Include(x => x.Account)
                .ProjectAddressToAddressDto()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(Address entity)
        {
             _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Address entity)
        {
             _db.Update(entity);
            await _db.SaveChangesAsync();
        }
    }
}
