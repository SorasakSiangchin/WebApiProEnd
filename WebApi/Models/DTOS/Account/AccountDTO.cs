using WebApi.Models;
using WebApi.Modes;
using WebApi.Modes.DTOS.Product;

namespace WebApiProjectEnd.Modes.DTOS.Accounts
{
    public class AccountDTO
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }  
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool? Status { get; set; }
        public int RoleID { get; set; }
        public string LoginBy { get; set; }
        public List<Product> Products { get; set; }
        public Role Role { get; set; }

    }
}
