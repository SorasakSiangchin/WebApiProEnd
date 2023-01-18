using WebApi.Repositorys.IRepositorys;

namespace WebApi.Repositorys
{
    public class UploadFileRepository : IUploadFileRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        // เป็น Class ที่ใช้อ้างอิงถึงค่าที่อยู่ใน appSetting
        private readonly IConfiguration _configuration;

        public UploadFileRepository(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }

        public Task DeleteFile(string filename , string key)
        {
            if (!string.IsNullOrEmpty(filename))
            {
                var uploadPath = $"{_webHostEnvironment.WebRootPath}/{key}/";
                var fullName = uploadPath + filename;
                if (File.Exists(fullName)) File.Delete(fullName);
            }
            return Task.CompletedTask;  //เสร็จสิ้นภาระกิจ 
        }

        // เช็คว่ามีไฟล์หรือปป่าว
        public bool IsUpload(IFormFileCollection formFiles)
        {
            //if (formFiles != null) var testc = formFiles.Count > 0;
            return formFiles?.Count > 0;
        }

        public async Task<List<string>> UploadFile(IFormFileCollection formFiles , string key)
        {

            var listFileName = new List<string>();
            // uploadPath จะเอามาบวกกับชื่อไฟล์
            var uploadPath = $"{_webHostEnvironment.WebRootPath}/{key}/";

            // ถ้ามันไม่มีไฟล์น้ให้สร้างขึ้นมา
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

            foreach (var formFile in formFiles)
            {
                // Guid.NewGuid().ToString() สุ่ม id ขึ้นมา + Path.GetExtension(formFile.FileName) เอานามสกุลมา Ex 111111111111.jpg
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = uploadPath + fileName;
                // สร้่างในมันมีตัวตน
                using (var stream = File.Create(fullName))
                {
                    // Copy เนื้อ ไฟล์มา
                    await formFile.CopyToAsync(stream);
                }
                // นำชื่อไฟล์ใส่ใน List
                listFileName.Add(fileName);
            }
            return listFileName;
        }

        public string Validation(IFormFileCollection formFiles)
        {
            foreach (var file in formFiles)
            {
                // เช็คนามสกุลไฟล์ 
                if (!ValidationExtension(file.FileName))
                {
                    return "Invalid file extension";
                }
                // เช็คขนาดของไฟล์
                if (!ValidationSize(file.Length))
                {
                    return "The file is too large";
                }
            }
            return null;
        }

        public bool ValidationExtension(string filename)
        {
            // สร้าง LIST ขึ้นมา
            string[] permittedExtensions = { ".jpg", ".png", ".mov", ".mp4" };
            // Path.GetExtension(filename) ดึงนามสกุลไฟล์มา
            // .ToLowerInvariant(); แปลงให้เป็นตัวเล็ก
            string extension = Path.GetExtension(filename).ToLowerInvariant();
            // string.IsNullOrEmpty(extension)  เป็นค่าว่างหรือป่าว
            //!permittedExtensions.Contains(extension) เอานามสกุลไปเช็คว่ามันมีหรือป่าว
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
            {
                return false;
            };
            return true;
        }
        // configuration.GetValue<long>("FileSizeLimit") เป็นการเรียกใช้ค้าจาก appsettings.json
        public bool ValidationSize(long fileSize) => _configuration.GetValue<long>("FileSizeLimit") > fileSize;
    }
}
