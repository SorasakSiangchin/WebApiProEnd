namespace WebApi.Models.DTOS.Account
{
    public class GoogleLoginRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ImageUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? RoleID { get; set; }

    }
}
