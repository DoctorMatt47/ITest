using System;

namespace ITest.Models
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
