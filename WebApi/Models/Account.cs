using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Account
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public string? LoginBy { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; }
        public int RoleID { get; set; }
        [ForeignKey("RoleID")]
        //[ValidateNever]
        public virtual Role Role { get; set; }
    }
}
