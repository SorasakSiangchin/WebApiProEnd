using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Extenstions
{
    public static class EvidenceMoneyTransferExtenstions
    {
        public static IQueryable<EvidenceMoneyTransferDTO> ProjectEvidenceMoneyTransferToEvidenceMoneyTransferDto(this IQueryable<EvidenceMoneyTransfer> query, ApplicationDbContext db)
        {
            return query.Select(e => new EvidenceMoneyTransferDTO { 
                Created= e.Created,
                Evidence = !string.IsNullOrEmpty(e.Evidence) ? e.Evidence : "",
                Id= e.Id,
                OrderID = e.OrderID,
                Order = db.Orders.FirstOrDefault(x => x.Id == e.OrderID)
            }).AsNoTracking();
        }
    }
}
