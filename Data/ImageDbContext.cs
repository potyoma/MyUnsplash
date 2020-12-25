using Microsoft.EntityFrameworkCore;
using Unsplash.Models;

namespace Unsplash.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<File> Files { get; set; }
    }
}