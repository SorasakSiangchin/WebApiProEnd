using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Drawing.Imaging;
using System.Drawing;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Review;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApi.Repositorys
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;
        private readonly IUploadFileRepository _uploadFileRepo;
        private readonly IConfiguration _configuration;

        public ReviewRepository(ApplicationDbContext db, IUploadFileRepository uploadFileRepo, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
            _uploadFileRepo = uploadFileRepo;
            _db = db;
        }
        public async Task CreactAsync(Review review)
        {
            await _db.AddAsync(review);
        }

        public async Task CreactImageAsync(List<string> imageNames, int reviewId)
        {
            foreach (var imageName in imageNames)
                await _db.ImageReviews.AddAsync(new ImageReview { ImageUrl = imageName, ReviewID = reviewId });
        }

        public async Task<ReviewAverageDTO> GetByProductId(ReviewParams reviewParams)
        {
            var reviews = await _db.Reviews
                   .Include(e => e.OrderItem)
                   .ThenInclude(e => e.ItemOrdered)
                   .ByProductID(reviewParams.productId)
                   .FilterRate(reviewParams.score)
                   .ProjectReviewToReviewDTO(_db)
                   .ToListAsync();
            double averageScore = 0;
            if (reviews?.Count > 0)
            {
                var sumScore = reviews.Sum(e => e.Score);
                averageScore = sumScore / reviews.Count();
            }
            return new ReviewAverageDTO { AverageScore = averageScore, Reviews = reviews };
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }

        public async Task<(string errorImage, List<string> imageNames)> UploadImage(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            List<string> imageName = new();
            if (_uploadFileRepo.IsUpload(formFiles))
            {
                errorMessage = _uploadFileRepo.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                    imageName = await ReviewUploadFileImage(FilterTypeFile(formFiles , "video/mp4", false));
            }
            return (errorMessage, imageName);
        }

        private static List<IFormFile> FilterTypeFile(IFormFileCollection formFiles , string type , bool isCheck = true)
        {
            return formFiles.Where(e => isCheck ? e.ContentType == type : e.ContentType != type).ToList();
        }

        public async Task<(string errorVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var vedioName = string.Empty;
            if (formFiles?.Count() > 0)
            {
                if (_uploadFileRepo.IsUpload(formFiles))
                {
                    errorMessage = _uploadFileRepo.Validation(formFiles);
                    if (string.IsNullOrEmpty(errorMessage))
                        vedioName = (await ReviewUploadFileVdo(FilterTypeFile(formFiles , "video/mp4"), "review"))[0];
                }
                return (errorMessage, vedioName);
            }
            return (errorMessage, vedioName);
        }

        private async Task<List<string>> ReviewUploadFileImage(List<IFormFile> formFiles)
        {
            var listFileData = new List<string>();
            foreach (var formFile in formFiles)
            {
                var image = Image.FromStream(formFile.OpenReadStream());
                using var imageStream = new MemoryStream();
                image.Save(imageStream, ImageFormat.Jpeg);
                var imageBytes = imageStream.ToArray();
                var imageData = $"data:{formFile.ContentType};base64,{Convert.ToBase64String(imageBytes)}";
                listFileData.Add(imageData);
            }
            return listFileData;
        }

        private async Task<List<string>> ReviewUploadFileVdo(List<IFormFile> formFiles ,string key)
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

        public string Validation(List<IFormFile> formFiles)
        {
            foreach (var file in formFiles)
            {
                if (!ValidationExtension(file.FileName)) return "Invalid file extension";
                if (!ValidationSize(file.Length)) return "The file is too large";
            }
            return null;
        }

        public bool ValidationExtension(string filename)
        {
            string[] permittedExtensions = { ".jpg", ".png", ".mov", ".mp4" };
            string extension = Path.GetExtension(filename).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension)) return false;
            return true;
        }

        public bool ValidationSize(long fileSize) => _configuration.GetValue<long>("FileSizeLimit") > fileSize;

        public async Task<ReviewDTO> GetByOrderItemId(int id)
        {
            return await _db.Reviews
                .ProjectReviewToReviewDTO(_db)
                .FirstOrDefaultAsync(e => e.OrderItemID.Equals(id));
        }
    }
}
