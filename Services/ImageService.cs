using System;
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

            return added == 1;
        }

        public async Task<IEnumerable<File>> GetAllImagesAsync()
        {
            // A collection of all images sorted by uploaded date and time in descending order.
            var images = await _db.Files
                .OrderByDescending(f => f.Uploaded)
                .ToListAsync();
            
            return images;
        }

        public async Task<File> GetImageByIdAsync(int id)
        {
            var result = await _db.Files
                .FirstOrDefaultAsync(f => f.Id == id);

            return result;
        }

        public async Task<File> GetImageByNameAsync(string name)
        {
            var image = await _db.Files
                .FirstOrDefaultAsync(f => f.Name == name);

            return image;
        }

        public async Task<File> RemoveImageAsync(int id)
        {
            var image = await _db.Files
                .FirstOrDefaultAsync(f => f.Id == id);
            
            if (image is null)
            {
                throw new Exception("Image is not found.");
            }

            _db.Files.Remove(image);
            var success = await _db.SaveChangesAsync();

            if (success != 0)
            {
                throw new Exception("Error saving changes in db.");
            }

            return image;
        }

        public async Task<bool> UpdateImageAsync(int originalId, File updatedImage)
        {
            var image = await _db.Files
                .FirstOrDefaultAsync(f => f.Id == originalId);

            if (image.Id != originalId)
            {
                return false;
            }

            updatedImage.Id = image.Id;
            _db.Update(updatedImage);
            int success = await _db.SaveChangesAsync();

            return success == 1;
        }   

        public async Task<IEnumerable<File>> GetImagesByLabelAsync(string label)
        {
            var images = await _db.Files
                .Where(f => f.Label == label)
                .OrderByDescending(f => f.Uploaded)
                .ToListAsync();

            return images;
        }
    }
}