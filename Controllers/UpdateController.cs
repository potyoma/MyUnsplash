using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Models;
using Unsplash.Services;
using Unsplash.Utilities;

namespace Unsplash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UpdateController : ControllerBase
    {
        private readonly IImageService _imgService;
        private readonly string _path;

        public UpdateController(IImageService imageService,
            IWebHostEnvironment env)
        {
            _imgService = imageService;
            _path = Path.Combine(env.ContentRootPath, "Uploads");
            
            // If directory doesn't exist, create one.
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateImage(int id,
            [FromForm] UploadImgViewModel image)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All fields should be provided.");
            }

            if (image.Extension == "bmp")
            {
                return BadRequest("The type is not supported. Sorry.");
            }

            await DeleteExistingAsync(id);

            string path = Path.Combine(_path,
                $"{image.Name}.{image.Extension}");

            await using (var fs = new FileStream(path, FileMode.Create))
            {
                await image.Image.CopyToAsync(fs);
            }
            
            // Updating record in db.
            var img = new Models.File
            {
                Name = image.Name,
                Extension = image.Extension,
                MimeType = image.MimeType,
                Description = image.Description,
                Path = path,
                Uploaded = DateTime.Now,
                Label = image.Label
            };

            bool success = await _imgService.UpdateImageAsync(id, img);

            if (!success)
            {
                return BadRequest("Something went wrong.");
            }
            
            Compressor.Compress(path);

            return Ok("File uploaded.");
        }

        private async Task DeleteExistingAsync(int id)
        {
            var image = await _imgService.GetImageByIdAsync(id);

            if (image is null)
            {
                return;
            }

            if (System.IO.File.Exists(image.Path))
            {
                System.IO.File.Delete(image.Path);
            }
        }
    }
}