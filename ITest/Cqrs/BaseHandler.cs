using System;
using ITest.Data;

namespace ITest.Cqrs
{
    public abstract class BaseHandler
    {
        protected readonly DatabaseContext _db;

        protected BaseHandler(DatabaseContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }
    }
}