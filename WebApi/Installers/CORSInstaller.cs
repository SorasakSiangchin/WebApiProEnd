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
                        .WithOrigins("http://localhost:5173" , "https://tee.kru.ac.th" , "http://10.103.0.16/")
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
