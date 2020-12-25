using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Services;
using Unsplash.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Unsplash.Utilities;

namespace Unsplash.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UploadController : ControllerBase
    {
        private readonly IImageService _imgService;
        private readonly string _path;

        public UploadController(IImageService imgService,
            IWebHostEnvironment env)
        {
            _imgService = imgService;
            _path = Path.Combine(env.WebRootPath, "Uploads");

            // If directory doesn't exist, create one.
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload([FromForm] IFormFile image)
        {
            if (image is null)
            {
                return BadRequest("No file attached");
            }

            var serverName = $"{Encryptor.EncryptName(image.FileName, "Banana7!")}.{image.ContentType}";
            var path = Path.Combine(_path, serverName);

            using (var fs = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(fs);
            }

            // Creating new DB record.
            var img =  new Models.File
            {
                Name = image.Name,
                ServerName = serverName,
                Path = path
            };

            var success = await _imgService.AddImageAsync(img);

            if (!success)
            {
                return BadRequest("Something went wrong.");
            }

            Compressor.Compress(path);
            
            return Ok("File uploaded.");
        }
    }
}