using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiProjectEnd.Modes
{
    public class Address
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public string AccountID { get; set; }
        public int AddressInformationID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }
        [ForeignKey("AddressInformationID")]
        public virtual AddressInformation AddressInformation { get; set; }
    }
}
