using System.Net;
using WebApi.Models;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class WeightUnitEndpoints
    {
        public static void ConfigureWeightUnitEndpoints(this WebApplication app)
        {
            app.MapGet("/weightUnits", GetAllWeightUnit).WithName("GetAllWeightUnit").Produces<APIResponse>(200); 
        }

        private async static Task<IResult> GetAllWeightUnit(IWeightUnitRepository _weightUnitRepo)
        {
            APIResponse response = new();
            var orders = _weightUnitRepo.GetAllAsync().GetAwaiter().GetResult();
            response.Result = orders;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
