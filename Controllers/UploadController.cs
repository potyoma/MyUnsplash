using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Services;
using Unsplash.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Unsplash.Utilities;
using System;

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
            _path = Path.Combine(env.ContentRootPath, "Uploads");

            // If directory doesn't exist, create one.
            if (!Directory.Exists(_path))
            {
                Directory.CreateDirectory(_path);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upload(
            [FromForm] UploadImgViewModel image)
        {
            if (image is null)
            {
                return BadRequest("No file attached");
            }

            var path = Path.Combine(_path, $"{image.Name}.{image.Image.FileName.Split('.')[1]}");

            using (var fs = new FileStream(path, FileMode.Create))
            {
                await image.Image.CopyToAsync(fs);
            }

            // Updating file info record.
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