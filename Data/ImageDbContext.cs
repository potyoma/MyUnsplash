using Microsoft.EntityFrameworkCore;
using Unsplash.Models;

namespace Unsplash.Data
{
    public class ImageDbContext : DbContext
    {
        public ImageDbContext(DbContextOptions<ImageDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<File> Files { get; set; }

        protected override void OnModelCreating(ModelBuilder md)
        {
            md.Entity<File>()
                .Property(f => f.Name)
                .IsRequired()
                .HasMaxLength(50);

            md.Entity<File>()
                .Property(f => f.Path)
                .IsRequired();

            md.Entity<File>()
                .Property(f => f.Uploaded)
                .IsRequired();

            md.Entity<File>()
                .Property(f => f.Label)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}