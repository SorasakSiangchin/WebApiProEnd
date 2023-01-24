using WebApi.Models;

namespace WebApi.Modes.DTOS.Address
{
    public class AddressDTO
    {
        public int Id { get; set; }
        public string AccountID { get; set; }
        public bool Status { get; set; }
        public List<AddressInformation> AddressInformations { get; set; }
        public Account Account { get; set; }
    }
}
