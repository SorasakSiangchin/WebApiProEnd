using System.Net;

namespace WebApi.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } // เก็บค่า true หรือ false ว่าสำเร็จหรือไม่ 
        public Object Result { get; set; } // เก็บค่าที่ต้องการแสดง
        public HttpStatusCode StatusCode { get; set; } // เก็บสถานะ
        public List<string> ErrorMessages { get; set; } // เก็บ Error

    }
}
