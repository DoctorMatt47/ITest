using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ITest.Data.Entities.Tests;
using ITest.Models;
using ITest.Models.Tests;

namespace ITest.Data.Entities.Accounts
{
    public class Account : BaseEntity
    {
        [Required]
        [MaxLength(30)]
        public string Login { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Password { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Mail { get; set; }
        
        [MaxLength(100)]
        public string City { get; set; }
        
        public AccountRole Role { get; set; } = AccountRole.User;
        
        public bool IsConfirmed { get; set; }

        public List<Test> Tests { get; set; }
        public List<TestAnswer> TestAnswers { get; set; }
    }
}
