using WebApi.Models;
using WebApiProjectEnd.Modes;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApiProjectEnd.Repositorys.IRepositorys
{
    public interface IAccountRepository
    {
        Task<ICollection<Account>> GetAllAsync();
        Task UpdateAsync(Account account);
        Task<Account> GetAsync(string id, bool tracked = true);
        bool IsUniqueEmail(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<AccountDTO> Register(Account account);
        Account GetInfo(string accessToken);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
