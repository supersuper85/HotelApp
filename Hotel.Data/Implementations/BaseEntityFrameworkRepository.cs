using HotelApp.Data.Entities.Context;
using HotelApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HotelApp.Data.Implementations
{
    public class BaseEntityFrameworkRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbSet<T> _entities;
        protected readonly DataBaseContext Context;

        public BaseEntityFrameworkRepository(DataBaseContext context)
        {
            Context = context;
            _entities = context.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _entities.AddAsync(entity, cancellationToken);
            return await SaveChangesAsync(cancellationToken) > 0 ? entity : null;
        }

        public virtual async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            await _entities.AddRangeAsync(entities, cancellationToken);
            return await SaveChangesAsync(cancellationToken) > 0 ? entities : null;
        }

        public virtual async Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            _entities.Remove(entity);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            _entities.RemoveRange(entities);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public virtual Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) =>
            _entities.AnyAsync(predicate, cancellationToken);

        public virtual async Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken)) =>
            await _entities.Include("Customer").ToListAsync(cancellationToken);

        public async Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        => await _entities.Where(predicate).ToListAsync(cancellationToken);

        public virtual ValueTask<T> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken)) =>
            _entities.FindAsync(new object[] { id }, cancellationToken);

        public virtual Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken)) =>
            _entities.SingleOrDefaultAsync(predicate);

        public virtual async Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _entities.FirstOrDefaultAsync(cancellationToken);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            Context.Update(entity);
            return await SaveChangesAsync(cancellationToken) > 0;
        }

        public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken))
        {
            foreach (var entity in entities)
            {
                Context.Update(entity);
            }

            return await SaveChangesAsync(cancellationToken) > 0;
        }

        /*protected abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));*/
        protected async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Context.SaveChangesAsync(cancellationToken);
        }
    }
    
}
