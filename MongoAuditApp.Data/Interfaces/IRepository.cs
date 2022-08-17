using MongoDB.Bson;
using System.Linq.Expressions;

namespace MongoAuditApp.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Determines whether this repository is healthy.
        /// </summary>
        /// <returns></returns>
        Task<bool> IsHealthyAsync();

        /// <summary>
        /// Determines whether an <typeparamref name="T"/> with the specified properties exists in this repository.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets the <typeparamref name="T"/> in this repository with the specified identifier.
        /// Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ValueTask<T> GetAsync(ObjectId id);

        /// <summary>
        /// Gets the <typeparamref name="T"/> in this repository with the specified identifier.
        /// Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Gets all <typeparamref name="T"/> in this repository.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync();

        /// <summary>
        /// Gets all <typeparamref name="T"/> that match predicate in this repository.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <returns></returns>
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Adds the specified <typeparamref name="T"/> to this repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Adds the specified <typeparamref name="T"/>'s to this repository.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Deletes the specified <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity);

        /// <summary>
        /// Deletes the specified <typeparamref name="T"/>'s.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> DeleteRangeAsync(ICollection<T> entities);

        /// <summary>
        /// Updates the specified <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Updates the specified <typeparamref name="T"/>'s.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(ICollection<T> entities);
    }
}
