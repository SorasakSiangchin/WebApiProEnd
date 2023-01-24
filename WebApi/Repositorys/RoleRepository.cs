using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<ICollection<Role>> GetAllAsync()
        {
            return  _db.Roles.ToListAsync().GetAwaiter().GetResult();
        }
    }
}
