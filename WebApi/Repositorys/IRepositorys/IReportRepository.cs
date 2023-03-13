using WebApi.Models;
using WebApi.Models.DTOS.Report;
using WebApi.Models.DTOS.Repost;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IReportRepository
    {  
        Task<List<ProductStatisticsDTO>> ProductStatistics(ProductStatisticsRequestDTO requestDTO);
        Task<SalesStatisticsDTO> SalesStatistics(SalesStatisticsRequestDTO requestDTO);
    }
}
