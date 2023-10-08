using System.Reflection;

namespace WebApiProjectEnd.Modes.DTOS.Accounts
{
    public class AccountRequestDTO
    {
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PasswordNew { get; set; }
        public bool? Status { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
        public int? RoleID { get; set; }
        public string? StatusLogin { get; set; }
        private static string SetData(string name, HttpContext context) => context.Request.Form[name];

        public static ValueTask<AccountRequestDTO?> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
        {
            var file = context.Request.Form.Files;
            int.TryParse(context.Request.Form["RoleID"], out var roleID);
            return ValueTask.FromResult<AccountRequestDTO?>(new AccountRequestDTO
            {
                FormFiles = file,
                Id = SetData("Id", context),
                Email = SetData("Email", context),
                FirstName = SetData("FirstName", context),
                LastName = SetData("LastName", context),
                Password = SetData("Password", context),
                PhoneNumber = SetData("PhoneNumber", context),
                PasswordNew = SetData("PasswordNew", context),
                StatusLogin = SetData("StatusLogin", context),
                Status = Convert.ToBoolean(SetData("Status", context)),
                RoleID = roleID
            }); ;
        }


    }
}
