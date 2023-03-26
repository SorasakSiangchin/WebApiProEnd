using System.Threading.Tasks;
using WebApi.Models.OrderAggregate;

namespace WebApi.Repositorys.IRepositorys
{
    public interface IOrderMessageRepository 
    {
        Task CreateAsync(OrderMessage orderMessage);
        Task SaveAsync();
    }
}
