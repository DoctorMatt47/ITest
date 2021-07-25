using ITest.Models.Tests;
using ITest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace ITest.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Test> Tests { get; set; } = default!;
        public DbSet<Question> Questions { get; set; } = default!;
        public DbSet<Choice> Choices { get; set; } = default!;
        public DbSet<TestAnswer> TestAnswers { get; set; } = default!;
        public DbSet<Account> Accounts { get; set; } = default!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
