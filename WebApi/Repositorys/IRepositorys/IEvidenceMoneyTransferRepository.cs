using WebApi.Models;
using WebApi.Models.DTOS.EvidenceMoneyTransfer;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IEvidenceMoneyTransferRepository : IRepository<EvidenceMoneyTransfer>
    {
        Task<EvidenceMoneyTransferDTO> GetByOrderIdAsync(string orderId, bool tracked = true);
        Task<EvidenceMoneyTransfer> GetAsync(int id, bool tracked = true);
        Task<ICollection<EvidenceMoneyTransferDTO>> GetCancelAsync(string orderId, bool status = false);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
    }
}
