using ITest.Models.Tests;
using ITest.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace ITest.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerChoice> AnswerChoices { get; set; }
        public DbSet<UserTestAnswer> TestAnswers { get; set; }
        public DbSet<UserQuestionAnswer> QuestionAnswers { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DatabaseContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ITest.db;Cache=Shared");
        }
    }
}
