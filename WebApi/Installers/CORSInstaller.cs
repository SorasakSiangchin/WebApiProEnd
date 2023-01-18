using WebApiProjectEnd.Installers;

namespace WebApi.Installers
{
    public class CORSInstaller : IInstallers
    {   public static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public void InstallServices(WebApplicationBuilder builder)
        {
            #region Cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:5173");
                    });
            });
            #endregion
        }
    }
}
