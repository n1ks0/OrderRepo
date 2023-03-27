using Order.BL.Exceptions;
using Order.BL.Interfaces.Repo;
using Order.DAL.Data;
using System.Linq.Expressions;

namespace Order.DAL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly Context orderContext;

        public Repository(Context orderContext)
        {
            this.orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public async Task AddAsync(T entity)
        {
            await orderContext.Set<T>().AddAsync(entity);
        }

        public async Task Delete(int id)
        {
            var entry = await orderContext.Set<T>().FindAsync(id);

            if (entry == null)
            {
                throw new EntityNotFoundException(id); 
            }

            orderContext.Set<T>().Remove(entry);
        }

        public void Delete(T entity)
        {
            orderContext.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            orderContext.Set<T>().RemoveRange(entities);
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return orderContext.Set<T>().Where(predicate);
        }

        public async Task<T> Get(int id)
        {
            return await orderContext.Set<T>().FindAsync(id);
        }

        public IQueryable<T> GetAll()
        {
            return orderContext.Set<T>().AsQueryable();
        }

        public void Update(T entity)
        {
            var entry = orderContext.Entry(entity);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            entry.CurrentValues.SetValues(entity);
        }
    }
}
