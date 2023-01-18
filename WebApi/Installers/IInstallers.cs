namespace WebApiProjectEnd.Installers
{
    public interface IInstallers
    {
        // WebApplicationBuilder คือ WebApplication.CreateBuilder(args); ใน Program.cs
        void InstallServices(WebApplicationBuilder builder);
    }
}
