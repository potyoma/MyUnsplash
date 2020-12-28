using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Unsplash.Data;
using Unsplash.Models;

namespace Unsplash.Services
{
    public class ImageService : IImageService
    {
        private readonly ImageDbContext _db;

        public ImageService(ImageDbContext db)
        {
            _db = db;
        }

        public async Task<bool> AddImageAsync(File image)
        {
            if (image is null)
            {
                return false;
            }

            await _db.AddAsync(image);
            var added = await _db.SaveChangesAsync();

            if (added < 0)
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<File>> GetAllImagesAsync()
        {
            // A collection of all images sorted by uploaded date and time in descending order.
            var images = await _db.Files
                .OrderByDescending(f => f.Uploaded)
                .ToListAsync();
            
            return images;
        }

        public Task<File> GetImageByIdAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<File> GetImageByNameAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveImageAsync(File image)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateImageAsync(File image)
        {
            throw new System.NotImplementedException();
        }
    }
}