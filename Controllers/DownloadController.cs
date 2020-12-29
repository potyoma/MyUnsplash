using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Services;

namespace Unsplash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController : ControllerBase
    {
        private readonly IImageService _imgService;

        public DownloadController(IImageService imgService)
        {
            _imgService = imgService;
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> DownloadImg(int? id)
        {
            if (id is null)
            {
                return BadRequest("Id should be an integer number.");
            }

            var image = await _imgService.GetImageByIdAsync((int)id);

            if (image is null)
            {
                return NotFound("Sorry, file doesn't exist.");
            }

            if (!System.IO.File.Exists(image.Path))
            {
                return NotFound("Sorry, file doesn't exist.");
            }
            var fs = System.IO.File.OpenRead(image.Path);

            return new FileStreamResult(fs, image.MimeType)
            {
                FileDownloadName = $"{image.Name}.{image.Extension}"
            };
        }
    }
}