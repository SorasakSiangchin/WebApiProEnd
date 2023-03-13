using System.Reflection;

namespace WebApiProjectEnd.Modes.DTOS.Accounts
{
    public class LoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        private static string SetData(string name, HttpContext context)
        {
            return context.Request.Form[name];
        }
        public static ValueTask<LoginRequestDTO?> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
        {
            return ValueTask.FromResult<LoginRequestDTO?>(new LoginRequestDTO
            {
              Email = SetData("Email" , context) ,
              Password = SetData("Password" , context)

            }); ;
        }
    }
}
