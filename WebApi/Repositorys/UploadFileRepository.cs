using Azure.Core;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
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

        // เป็นการอัฟโหลดรูปโดยเก็บ path ไว้ในฐานข้อมูล
        public async Task<List<string>> UploadFile(IFormFileCollection formFiles)
        {
            var listFileData = new List<string>();
            foreach (var formFile in formFiles)
            {
                var image = Image.FromStream(formFile.OpenReadStream());
                //var resized = new Bitmap(image, new Size(256, 256)); //กำหนดขนาดของรูปภาพ
                using var imageStream = new MemoryStream();
                image.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                var imageData = $"data:{formFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                listFileData.Add(imageData);
            }
            return listFileData;
        }

        public string Validation(IFormFileCollection formFiles)
        {
            foreach (var file in formFiles)
            {
                // เช็คนามสกุลไฟล์ 
                if (!ValidationExtension(file.FileName)) return "Invalid file extension";
                
                // เช็คขนาดของไฟล์
                if (!ValidationSize(file.Length)) return "The file is too large";
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
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)) return false;
            
            return true;
        }
        // configuration.GetValue<long>("FileSizeLimit") เป็นการเรียกใช้ค้าจาก appsettings.json
        public bool ValidationSize(long fileSize) => _configuration.GetValue<long>("FileSizeLimit") > fileSize;
    }
}
