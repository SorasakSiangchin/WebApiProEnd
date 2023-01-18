using WebApi.Modes.DTOS.Cart;

namespace WebApiProjectEnd.Modes.DTOS.Accounts
{
    public class LoginResponseDTO
    {
        public AccountDTO Account { get; set; }
        public string Token { get; set; } 
        public CartDTO Cart { get; set; }
    }
}
