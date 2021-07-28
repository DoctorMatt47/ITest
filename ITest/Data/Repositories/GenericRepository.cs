using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ITest.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ITest.Data.Repositories
{
    public class GenericRepository<T> where T : BaseEntity 
    {
        private readonly DatabaseContext _db;

        protected GenericRepository(DatabaseContext db) => _db = db;

        protected IQueryable<T> GetAll()
        {
            return _db.Set<T>().AsQueryable();
        }
        
        protected void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        protected void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        protected async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}