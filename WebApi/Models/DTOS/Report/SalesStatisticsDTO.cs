namespace WebApi.Models.DTOS.Report
{
    public class SalesStatisticsDTO
    {
        public double TotalPrice { get; set; }
        public List<SalesStatisticeItemDTO> Sales { get; set; } = new List<SalesStatisticeItemDTO>();
        
    };

}


