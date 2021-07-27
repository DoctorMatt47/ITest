using ITest.Data.Entities.Accounts;
using ITest.Data.Entities.Tests;
using Microsoft.EntityFrameworkCore;

namespace ITest.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<TestAnswer> TestAnswers { get; set; } 
        public DbSet<Account> Accounts { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
    }
}
