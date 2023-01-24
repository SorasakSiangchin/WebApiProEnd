namespace WebApi.Models.DTOS.Address
{
    public class CreateAddressDTO
    {
        public List<AddressInformation> AddressInformations { get; set; }
        public string AccountID { get; set; }
    }
}
