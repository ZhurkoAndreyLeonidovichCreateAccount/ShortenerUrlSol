
using Microsoft.EntityFrameworkCore;
using ShortenerUrl.DAL.Entity;

namespace ShortenerUrl.DAL.Data

{

    public class ApplicationDbContext : DbContext
    {
        public DbSet<ShortendUrl> ShortendUrls { get; set; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortendUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(7);
                builder.HasIndex(s => s.Code).IsUnique();
                builder.HasIndex(s => s.LongUrl).IsUnique();
            });
        }
    }
}

