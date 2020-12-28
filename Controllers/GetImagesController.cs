using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Models;
using Unsplash.Services;

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
        public async Task<IEnumerable<DownloadImgInfoViewModel>> GetAllByDate()
        {
            var images = await _imgService.GetAllImagesAsync();

            var result = new List<DownloadImgInfoViewModel>();

            foreach (var image in images)
            {
                result.Add(new DownloadImgInfoViewModel(image));
            }

            return result;
        }

        [HttpGet("id={id}/")]
        public async Task<IActionResult> GetImageById(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var image = await _imgService.GetImageByIdAsync((int)id);
            
            if (!System.IO.File.Exists(image.Path))
            {
                return NotFound();
            }

            var stream = System.IO.File.OpenRead(image.Path);

            return new FileStreamResult(stream, "img/png")
            {
                FileDownloadName = $"{image.Name}.png"
            };
        }
    }
}