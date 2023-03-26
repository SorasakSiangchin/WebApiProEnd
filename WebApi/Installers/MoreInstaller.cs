using System.Text.Json;

namespace WebApiProjectEnd.Installers
{
    public class MoreInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(MappingConfig));

        }
    }
}
