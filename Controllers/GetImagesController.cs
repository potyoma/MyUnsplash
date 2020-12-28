using System.Collections.Generic;
using System.IO;
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

            string temp = Compressor.CreateTemp(image);

            var ms = new MemoryStream();

            using (var fs = System.IO.File.OpenRead(temp))
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
    }
}