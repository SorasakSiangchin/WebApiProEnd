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
#region  //สร้ำงข้อมูลจำลอง Fake data 
using var scope = app.Services.CreateScope(); //using หลังทำงำนเสร็จจะถูกทลำยจำกMemory 
var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    // orm
    await context.Database.MigrateAsync();   //สร้ำง DB ใหอ้ตัโนมตัถิำยังไม่มี ***** ใช้งานได้จริง
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

app.UseDefaultFiles(); // อนุญาตให้เรียกไฟล์ต่างๆ ใน wwwroot

app.UseStaticFiles(); // อนุญาตให้เข้าถึงไฟล์ค่าคงที่ได้ 

app.UseRouting();

app.UseCors(CORSInstaller.MyAllowSpecificOrigins);
//การตรวจสอบสิทธิ์
app.UseAuthentication();
//การอนุญาต
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
//    endpoints.MapFallbackToController("Index", "Fallback"); // บอกเส้นทางมันก่อน
//});


app.Run();


