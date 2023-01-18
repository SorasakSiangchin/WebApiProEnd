using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebApi.Endpoints;
using WebApi.Installers;
using WebApiProjectEnd.Endpoints;
using WebApiProjectEnd.Installers;
using WebApiProjectEnd.Modes;

var builder = WebApplication.CreateBuilder(args);
builder.Services.MyInstallerExtensions(builder);



var app = builder.Build();
#region  //���ӧ�����Ũ��ͧ Fake data 
using var scope = app.Services.CreateScope(); //using ��ѧ�ӧӹ���稨ж١���¨ӡMemory 
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    // orm
    await context.Database.MigrateAsync();   //���ӧ DB ��������Ѷ���ѧ����� ***** ��ҹ���ԧ
}

catch (Exception ex)
{
    logger.LogError(ex, "Problem migrating data");
}
#endregion

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles(); // ͹حҵ������¡����ҧ� � wwwroot

app.UseStaticFiles(); // ͹حҵ�����Ҷ֧����Ҥ������ 

app.UseRouting();

app.UseCors(CORSInstaller.MyAllowSpecificOrigins);
//��õ�Ǩ�ͺ�Է���
app.UseAuthentication();
//���͹حҵ
app.UseAuthorization();

//----- CodeArea --------
app.ConfigureAccountEndpoints();
app.ConfigureProductEndpoints();
app.ConfigureImageProductEndpoints();
app.ConfigureCategoryProductEndpoints();
app.ConfigureCartEndpoints();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//    endpoints.MapFallbackToController("Index", "Fallback"); // �͡��鹷ҧ�ѹ��͹
//});


app.Run();


