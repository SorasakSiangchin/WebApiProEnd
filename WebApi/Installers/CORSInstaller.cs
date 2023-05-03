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
                        policy
                        .WithOrigins("http://localhost:5173" , "http://tee.kru.ac.th" )
                        //.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        ;
                    });
            });
            #endregion
        }
    }
}
