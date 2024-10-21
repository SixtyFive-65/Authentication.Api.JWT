using Microsoft.EntityFrameworkCore;
using Polls.Api.Data.DomainModels;

namespace Polls.Api.Data
{
    public class SabeloSethuAuthDbContext : DbContext
    {
        public SabeloSethuAuthDbContext(DbContextOptions<SabeloSethuAuthDbContext> options) : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>(entity => {
                entity.HasIndex(e => e.Username).IsUnique();
            });
            builder.Entity<ApplicationUser>(entity => {
                entity.HasIndex(e => e.Email).IsUnique();
            });
            builder.Entity<ApplicationUser>(entity => {
                entity.HasIndex(e => e.MobileNumber).IsUnique();
            });
        }
    }
}
