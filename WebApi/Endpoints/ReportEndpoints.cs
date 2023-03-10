using System.Net;
using WebApi.Models;
using WebApi.Models.DTOS.Report;
using WebApi.Models.DTOS.Repost;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Endpoints
{
    public static class ReportEndpoints
    {
        public static void ConfigureReportEndpoints(this WebApplication app)
        {
            app.MapPost("report/productStatistics", GetProductStatistics).WithName("GetProductStatistics").Accepts<ProductStatisticsRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400) ;
            app.MapPost("report/salesStatistics", GetSalesStatistics).WithName("GetSalesStatistics").Accepts<SalesStatisticsRequestDTO>("multipart/form-data").Produces<APIResponse>(200).Produces(400); ; ;
        }

        private async static Task<IResult> GetProductStatistics(IReportRepository _reportRepo , ProductStatisticsRequestDTO model)
        {
            APIResponse response = new();
            response.Result = await _reportRepo.ProductStatistics(model);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }

        private async static Task<IResult> GetSalesStatistics(IReportRepository _reportRepo, SalesStatisticsRequestDTO model)
        {
            APIResponse response = new();
            response.Result = await _reportRepo.SalesStatistics(model);
            response.IsSuccess = true;
            response.StatusCode = HttpStatusCode.OK;
            return Results.Ok(response);
        }
    }
}
