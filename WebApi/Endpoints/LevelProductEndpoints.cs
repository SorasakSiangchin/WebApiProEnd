using System.Net;
using System.Security.Principal;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApi.Endpoints
{
    public static class LevelProductEndpoints
    {
        public static void ConfigureLevelProductEndpoints(this WebApplication app)
        {
            app.MapGet("/levelProducts", GetAllLevelProduct).WithName("GetAllLevelProduct").Produces<APIResponse>(200); //.RequireAuthorization("AdminOnly");
         }
        private async static Task<IResult> GetAllLevelProduct(ILevelProductRepository _levelProductRepo)
        {
            APIResponse response = new();
            var orders = _levelProductRepo.GetAllAsync().GetAwaiter().GetResult();
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
