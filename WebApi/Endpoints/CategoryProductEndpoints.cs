using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;
using WebApi.Models;
using WebApi.Modes;
using WebApi.Repositorys.IRepositorys;
using WebApiProjectEnd.Modes;

namespace WebApi.Endpoints
{
    public static class CategoryProductEndpoints
    {
        public static void ConfigureCategoryProductEndpoints(this WebApplication app)
        {
            app.MapGet("category/products", GetAllCategoryProducts).WithName("GetAllCategoryProducts").Produces<APIResponse>(200);
        }

        private static async Task<IResult> GetAllCategoryProducts(ICategoryProductRepository _categoryProductRepo)
        {
            APIResponse response = new();
            var accounts = await _categoryProductRepo.GetAllAsync();
            response.Result = accounts;
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
