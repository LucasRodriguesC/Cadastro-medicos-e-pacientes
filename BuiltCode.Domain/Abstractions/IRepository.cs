using BuiltCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuiltCode.Domain.Abstractions
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T> GetById(Guid id);
        IQueryable<T> Get();
        Task<int> Count();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
