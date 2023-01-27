namespace WebApi.Models.DTOS.Address
{
    public class UpdateAddressDTO
    {
        public int Id { get; set; }
        public string AccountID { get; set; }
        public bool Status { get; set; }
        public AddressInformation AddressInformations { get; set; }
    }
}
