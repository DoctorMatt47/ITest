using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using ITest.Models.Tests;

namespace ITest.Models.Accounts
{
    public class Account : BaseEntity
    {
        [NotNull]
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; } = string.Empty;
        
        [NotNull]
        [Required(ErrorMessage = "Login is required")]
        public string Password { get; set; } = string.Empty;
        
        [NotNull]
        [EmailAddress]
        [Required(ErrorMessage = "Login is required")]
        public string Mail { get; set; } = string.Empty;
        
        public string? City { get; set; }
        
        public AccountRole Role { get; set; } = AccountRole.User;
        
        public bool IsConfirmed { get; set; } = false;

        public List<Test>? Tests { get; set; }
        public List<TestAnswer>? TestAnswers { get; set; }
    }
}
