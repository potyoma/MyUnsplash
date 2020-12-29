using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unsplash.Services;

namespace Unsplash.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeleteController : ControllerBase
    {
        private readonly IImageService _imgService;

        public DeleteController(IImageService imgService)
        {
            _imgService = imgService;
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            
            try 
            {
                var deleted = await _imgService.RemoveImageAsync((int)id);

                if (!System.IO.File.Exists(deleted.Path))
                {
                    throw new Exception("File not found.");
                }

                System.IO.File.Delete(deleted.Path);

                return Ok();
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}