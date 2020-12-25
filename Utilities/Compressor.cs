using System.IO;
using ImageMagick;

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
        public static string CreateTemp(string path)
        {
            return null;
        }
    }
}