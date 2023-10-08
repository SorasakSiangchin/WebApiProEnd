using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;

namespace WebApiProjectEnd.Installers
{
    public class RepositoryInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
            {
                containerBuilder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                //- จะค้นหาชื่อไฟล์ที่ลงท้ายด้วย Service แล้วจะ DI โดยอัตโนมัติ
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();
            }));
        }
    }
}
