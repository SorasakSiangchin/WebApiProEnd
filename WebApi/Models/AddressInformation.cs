namespace WebApi.Models
{
    public class AddressInformation
    {
        public int Id { get; set; }
        public string SubDistrict { get; set; } // ตำบล
        public string District { get; set; } // อำเภอ
        public string Province { get; set; } // จังหวัด
        public string ZipCode { get; set; } // รหัสไปษณี
        public string RecipientName { get; set; } // ชื่อผู้รับ
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
      
    }
}
