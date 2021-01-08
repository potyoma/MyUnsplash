using System;

namespace Unsplash.Models
{
    public class DownloadImgInfoViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Label { get; set; }
        public DateTime? Uploaded { get; set; }
        public string LinkPreviewDownload { get; set; }
        public string LinkFullDownload { get; set; }

        public DownloadImgInfoViewModel() { }

        public DownloadImgInfoViewModel(File image)
        {
            Id = image.Id;
            Name = image.Name;
            Description = image.Description;
            Label = image.Label;
            Uploaded = image.Uploaded;
        }
    }
}