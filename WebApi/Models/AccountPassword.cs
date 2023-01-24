using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class AccountPassword
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string AccountID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
    }
}
