using Microsoft.EntityFrameworkCore;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.OrderAggregate;
using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class EvidenceMoneyTransferRepository : IEvidenceMoneyTransferRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUploadFileRepository _uploadFile;

        public EvidenceMoneyTransferRepository(ApplicationDbContext db, IUploadFileRepository uploadFile)
        {
            _db = db;
            _uploadFile = uploadFile;
        }

        public async Task CreateAsync(EvidenceMoneyTransfer entity)
        {
            var order = await _db.Orders.AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.OrderID);
            order.OrderStatus = OrderStatus.PendingApproval;
            await _db.AddAsync(entity);
            _db.Update(order);
        }

        public async Task SaveAsync() => await _db.SaveChangesAsync();

        public async Task<EvidenceMoneyTransferDTO> GetByOrderIdAsync(string orderId, bool tracked = true)
        {
            IQueryable<EvidenceMoneyTransfer> query = _db.EvidenceMoneyTransfers;

            if (!tracked) query = query.AsNoTracking();

            return await query
                .Where(x => x.OrderID == orderId && x.Status == true)
                .ProjectEvidenceMoneyTransferToEvidenceMoneyTransferDto(_db)
                .FirstOrDefaultAsync();
        }

        public async Task<ICollection<EvidenceMoneyTransferDTO>> GetCancelAsync(string orderId, bool status = false)
        {
            return await _db.EvidenceMoneyTransfers
                .Where(x => x.OrderID == orderId && x.Status == status)
                .ProjectEvidenceMoneyTransferToEvidenceMoneyTransferDto(_db)
                .ToListAsync();
        }

        public async Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = string.Empty;
            if (_uploadFile.IsUpload(formFiles))
            {
                errorMessage = _uploadFile.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                {
                    imageName = (await _uploadFile.UploadFile(formFiles))[0];
                }
            }
            return (errorMessage, imageName);
        }

        public async Task<EvidenceMoneyTransfer> GetAsync(int id, bool tracked = true)
        {
            IQueryable<EvidenceMoneyTransfer> query = _db.EvidenceMoneyTransfers;

            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync(e => e.Id.Equals(id));
        }

        public async Task UpdateAsync(EvidenceMoneyTransfer evidenceMoneyTransfer) => _db.Update(evidenceMoneyTransfer);

        public Task RemoveAsync(EvidenceMoneyTransfer entity) => null;
    }
}
