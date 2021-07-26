using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ITest.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [NotNull]
        public Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
        public DateTime? ModifiedDateTime { get; set; }
    }
}
