using Microsoft.EntityFrameworkCore;
using WebApi.Endpoints;
using WebApi.Installers;
using WebApi.Middlewares;
using WebApi.Models;
using WebApiProjectEnd.Endpoints;
using WebApiProjectEnd.Installers;
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
    await context.Database.MigrateAsync();   
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

#region ส่ง error ไปให้ Axios ตอนทำ Interceptor
app.UseMiddleware<ExceptionMiddleware>();
#endregion

app.UseRouting();

app.UseCors(CORSInstaller.MyAllowSpecificOrigins);
//การตรวจสอบสิทธิ์
app.UseAuthentication();
//การอนุญาต
app.UseAuthorization();



app.ConfigureAccountEndpoints();
app.ConfigureProductEndpoints();
app.ConfigureDetailProductEndpoints();
app.ConfigureImageProductEndpoints();
app.ConfigureCategoryProductEndpoints();
app.ConfigureCartEndpoints();
app.ConfigureAddressEndpoints();
app.ConfigureOrderEndpoints();
app.ConfigureWeightUnitEndpoints();
app.ConfigureLevelProductEndpoints();
app.ConfigureEvidenceMoneyTransferEndpoints();
app.ConfigureReportEndpoints();
app.ConfigureDeliveryEndpoints();
app.ConfigureStatusDeliveryEndpoints();
app.ConfigureReviewEndpoints();
app.ConfigureOrderMessageEndpoints();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapFallbackToController("Index", "Fallback"); // บอกเส้นทางมันก่อน
});

app.Run();


