using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Polls.Api.Data.DomainModels;
using SabeloSethu.Api.Data.DomainModels.Application;

namespace Polls.Api.Data
{
    public class SabeloSethuAuthDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public SabeloSethuAuthDbContext(DbContextOptions<SabeloSethuAuthDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
               .HasIndex(u => u.PhoneNumber)
               .IsUnique();

            modelBuilder.Entity<ApplicationUser>()
            .HasIndex(u => u.Email)
            .IsUnique();
        }
    }
}
