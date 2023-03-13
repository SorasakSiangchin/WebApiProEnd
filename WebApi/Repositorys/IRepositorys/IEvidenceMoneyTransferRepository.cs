using WebApi.Models;
using WebApi.Models.DTOS.EvidenceMoneyTransfer;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IEvidenceMoneyTransferRepository
    {
        Task<EvidenceMoneyTransferDTO> GetByOrderIdAsync(string orderId, bool tracked = true);
        Task<EvidenceMoneyTransfer> GetAsync(int id, bool tracked = true);
        Task CreateAsync(EvidenceMoneyTransfer evidenceMoneyTransfer);
        Task UpdateAsync(EvidenceMoneyTransfer evidenceMoneyTransfer);
        Task<ICollection<EvidenceMoneyTransferDTO>> GetCancelAsync(string orderId, bool status = false);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
        Task SaveAsync();
    }
}
