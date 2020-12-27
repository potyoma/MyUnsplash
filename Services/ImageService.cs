using System.Collections.Generic;
using System.Threading.Tasks;
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

        public Task<IEnumerable<File>> GetAllImagesAsync()
        {
            throw new System.NotImplementedException();
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