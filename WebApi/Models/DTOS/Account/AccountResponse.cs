using WebApi.Models;
using WebApi.Settings;
using WebApiProjectEnd.Modes;

namespace WebApi.Modes
{
    public class AccountResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
        public bool? Status { get; set; }
        public int RoleID { get; set; }
        public Role Role { get; set; }

        public static AccountResponse FromAccount(Account account)
        {
            return new AccountResponse
            {
                Id = account.Id,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = account.Password,
                PhoneNumber = account.PhoneNumber,
                ImageUrl = account.ImageUrl ,
                Role = new Role
                {
                    Id = account.Role.Id,
                    Name = account.Role.Name,
                },
                RoleID = account.RoleID,
                Status = account.Status,
            };
        }
    }
}
