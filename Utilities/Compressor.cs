using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Microsoft.Extensions.FileProviders;

namespace Unsplash.Utilities
{
    public static class Compressor
    {
        public static void Compress(string path)
        {
            var image = new FileInfo(path);

            var optimizer = new ImageOptimizer();
            optimizer.LosslessCompress(image);

            image.Refresh();
        }

        // Creates temporary resized image and returns path to it.
        public static string CreateTemp(Models.File image)
        {
            if (!File.Exists(image.Path))
            {
                throw new Exception("File doesn't exist.");
            }

            string tempPath = Path.Combine("Uploads", "Temp", $"{image.Name}.{image.Extension}");

            using (var img = new MagickImage(image.Path))
            {
                img.Resize(200, 0);
                img.Write(tempPath);
            }

            return tempPath;
        }
    }
}