using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Address
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public AddressInformation AddressInformations { get; set; }
        public string AccountID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
 
    }
}
