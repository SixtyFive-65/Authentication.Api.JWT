using Microsoft.EntityFrameworkCore;
using Polls.Api.Data.DomainModels;

namespace Polls.Api.Data
{
    public class SabeloSethuDbContext : DbContext
    {
        public SabeloSethuDbContext(DbContextOptions<SabeloSethuDbContext> options) : base(options)
        {
        }

        public DbSet<Poll> Polls { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Vote> Votes { get; set; } // Add this line

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Poll>()
                .HasMany(p => p.Questions)
                .WithOne(q => q.Poll)
                .HasForeignKey(q => q.PollId);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(o => o.Question)
                .HasForeignKey(o => o.QuestionId);

            modelBuilder.Entity<Poll>(entity => {
                entity.HasIndex(e => e.Question).IsUnique();
            });
        }
    }
}
