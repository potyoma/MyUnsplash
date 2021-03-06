using Microsoft.AspNetCore.Http;

namespace Unsplash.Models
{
    public class UploadImgViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public string MimeType { get; set; }
        public string Extension { get; set; }
        public IFormFile Image { get; set; }
    }
}