namespace WebApiProjectEnd.Installers
{
    public class MoreInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            // ตัวเข้าถึงบริบท
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAutoMapper(typeof(MappingConfig));
            builder.Services.AddSignalR();
        }
    }
}
