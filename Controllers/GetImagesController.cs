using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Models;
using Unsplash.Services;
using Unsplash.Utilities;

namespace Unsplash.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class GetImagesController : ControllerBase
    {
        private readonly IImageService _imgService;

        public GetImagesController(IImageService imgService)
        {
            _imgService = imgService;
        }

        [HttpGet]
        public async Task<ActionResult<List<DownloadImgInfoViewModel>>> GetAllByDate()
        {
            var images = await _imgService.GetAllImagesAsync();

            if (images is null)
            {
                return NotFound();
            }

            return images.Select(image => new DownloadImgInfoViewModel(image)).ToList();
        }

        [HttpGet("id={id}/")]
        public async Task<IActionResult> GetImageById(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var image = await _imgService.GetImageByIdAsync((int)id);

            if (image is null)
            {
                return NotFound("Sorry, file doesn't exist.");
            }

            if (!System.IO.File.Exists(image.Path))
            {
                return NotFound();
            }

            string temp = Compressor.CreateTemp(image);
            var ms = new MemoryStream();

            await using(var fs = System.IO.File.OpenRead(temp))
            {
                await fs.CopyToAsync(ms);
            }

            ms.Position = 0;
            System.IO.File.Delete(temp);

            return new FileStreamResult(ms, image.MimeType)
            {
                FileDownloadName = $"{image.Name}.{image.Extension}"
            };
        }
        
        [HttpGet("label={label}")]
        public async Task<ActionResult<List<DownloadImgInfoViewModel>>> GetImagesByLabel(string label)
        {
            if (string.IsNullOrEmpty(label))
            {
                return BadRequest();
            }
            
            var images = await _imgService.GetImagesByLabelAsync(label);

            if (images is null)
            {
                return NotFound();
            }

            return images.Select(image => new DownloadImgInfoViewModel(image)).ToList();
        }
    }
}