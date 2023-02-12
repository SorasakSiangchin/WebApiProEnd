using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
        public async Task CreactAsync(Address address)
        {
            await _db.AddAsync(address);
            await _db.SaveChangesAsync();
        }
        public async Task<List<Address>> GetAllAsync(string accountId, bool tracked = true)
        {
            IQueryable<Address> query = _db.Addresses.Include(x => x.Account).Include(x => x.AddressInformations);
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.OrderByDescending(x => x.Status == true).Where(x => x.AccountID == accountId).AsQueryable().ToListAsync();
        }
        public async Task<Address> GetAsync(int id , bool tracked = true)
        {
            IQueryable<Address> query = _db.Addresses.Include(x => x.Account).Include(x => x.AddressInformations);
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.Where(e => e.Id == id).FirstOrDefaultAsync();
        }
        public async Task RemoveAsync(Address address)
        {
             _db.Remove(address);
             _db.Remove(address.AddressInformations);
             await _db.SaveChangesAsync();

        }

        public async Task UpdateAsync(Address address)
        {
             _db.Update(address);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(List<Address> address)
        {
            _db.UpdateRange(address);
            await _db.SaveChangesAsync();
        }
    }
}
