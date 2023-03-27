using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Order.BL.Interfaces.Repo
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        IQueryable<T> GetAll();
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Update(T entity);
        Task AddAsync(T entity);
        Task Delete(int id);
        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);
    }
}
