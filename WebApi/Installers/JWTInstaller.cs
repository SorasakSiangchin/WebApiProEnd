using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebApiProjectEnd.Installers
{
    public class JWTInstaller : IInstallers
    {
        public void InstallServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthorization(
                options => {
                    // เพิ่มนโยบาย 
                    options.AddPolicy("AdminOnly", oplicy => oplicy.RequireRole("admin"));
                    options.AddPolicy("SellerOnly", oplicy => oplicy.RequireRole("seller"));
                    options.AddPolicy("CustomerOnly", oplicy => oplicy.RequireRole("customer"));
                });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                // การบันทึก Token ให้เป็น True
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                      builder.Configuration.GetValue<string>("ApiSettings:Secret")

                 )),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

        }
    }
}
