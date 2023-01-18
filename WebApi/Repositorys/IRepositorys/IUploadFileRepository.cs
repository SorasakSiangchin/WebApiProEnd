namespace WebApi.Repositorys.IRepositorys
{
    public interface IUploadFileRepository
    {
        //ตรวจสอบมีการอัพโหลดไฟล์เข้ามาหรือไม่
        // IFormFileCollection เป็น interfaces ที่ใช้สำหรับ UploadFile
        bool IsUpload(IFormFileCollection formFiles);
        //ตรวจสอบนามสกุลไฟล์หรือรูปแบบที่่ต้องการ
        string Validation(IFormFileCollection formFiles);
        //อัพโหลดและส่งรายชื่อไฟล์ออกมา
        Task<List<string>> UploadFile(IFormFileCollection formFiles , string key);
        Task DeleteFile(string filename, string key);
    }
}
