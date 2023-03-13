using WebApi.Models;
using WebApi.RequestHelpers;
using WebApiProjectEnd.Modes.DTOS.Accounts;

namespace WebApiProjectEnd.Repositorys.IRepositorys
{
    public interface IAccountRepository
    {
        Task<ICollection<AccountDTO>> GetAllAsync(AccountParams accountParams);
        Task<Account> CheckEmail(string email);
        Task UpdateAsync(Account account);
        Task<AccountDTO> UpdatePassword(Account account, string passwordNew);
        Task<Account> GetAsync(string id, bool tracked = true);
        bool IsUniqueEmail(string email);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LoginResponseDTO> LoginGoogle(LoginRequestDTO loginRequestDTO);
        Task<AccountDTO> Register(Account account);
        Account GetInfo(string accessToken);
        Task<(string errorMessage, string imageName)> UploadImage(IFormFileCollection formFiles);
        Task DeleteImage(string fileName);
    }
}
