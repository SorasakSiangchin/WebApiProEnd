using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApiProjectEnd.Installers
{
    public class DatabaseInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultDbConnection"))
);
        }
    }
}
