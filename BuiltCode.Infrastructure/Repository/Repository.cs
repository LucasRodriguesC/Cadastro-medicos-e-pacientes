using BuiltCode.Domain.Abstractions;
using BuiltCode.Domain.Entities;
using BuiltCode.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuiltCode.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected AppDbContext _db;

        public Repository(AppDbContext db) { _db = db; }

        public void Add(T entity)
        {
            _db.Set<T>().Add(entity);
        }

        public async Task<int> Count()
        {
            return await _db.Set<T>().CountAsync();
        }

        public void Delete(T entity)
        {
            _db.Set<T>().Remove(entity);
        }

        public IQueryable<T> Get()
        {
            return _db.Set<T>().AsNoTracking().AsQueryable();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _db.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T entity)
        {
            _db.Set<T>().Update(entity);
        }
    }
}
