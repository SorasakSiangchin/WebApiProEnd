using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Extenstions;
using WebApi.Models;
using WebApi.Models.DTOS.Review;
using WebApi.Repositorys.IRepositorys;
using WebApi.RequestHelpers;

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
                    imageName = await UploadFileReview(formFiles.Where(e => e.ContentType != "video/mp4").ToList(), "reviewImage");
            }
            return (errorMessage, imageName);
        }

        public async Task<(string errorVedio, string vedioName)> UploadVedio(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageName = string.Empty;
            if (_uploadFileRepo.IsUpload(formFiles))
            {
                errorMessage = _uploadFileRepo.Validation(formFiles);
                if (string.IsNullOrEmpty(errorMessage))
                    imageName = (await _uploadFileRepo.UploadFile(formFiles, "reviewVdo"))[0];
            }
            return (errorMessage, imageName);
        }

        private async Task<List<string>> UploadFileReview(List<IFormFile> formFiles, string key)
        {
            var listFileName = new List<string>();
            var uploadPath = $"{_webHostEnvironment.WebRootPath}/{key}/";
            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
            foreach (var formFile in formFiles)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string fullName = uploadPath + fileName;
                using (var stream = File.Create(fullName)) await formFile.CopyToAsync(stream);
                listFileName.Add(fileName);
            };
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
    }
}
