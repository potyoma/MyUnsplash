using System.Collections.Generic;
using System.Threading.Tasks;
using Unsplash.Models;

namespace Unsplash.Services
{
    public interface IImageService
    {
        Task<bool> AddImageAsync(File image);
        Task<File> GetImageByIdAsync(int id);
        Task<File> GetImageByNameAsync(string name);
        Task<IEnumerable<File>> GetAllImagesAsync();
        Task<bool> UpdateImageAsync(int originalId, File image);
        Task<File> RemoveImageAsync(int id);
        Task<IEnumerable<File>> GetImagesByLabelAsync(string label);
    }
}