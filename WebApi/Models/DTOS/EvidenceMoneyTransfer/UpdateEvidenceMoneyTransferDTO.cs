using System.Reflection;

namespace WebApi.Models.DTOS.EvidenceMoneyTransfer
{
    public class UpdateEvidenceMoneyTransferDTO
    {
        public int Id { get; set; }
        public string OrderID { get; set; }
        public bool Status { get; set; }
        
    }
}
