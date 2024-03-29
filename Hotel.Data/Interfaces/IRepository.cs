﻿using System.Linq.Expressions;

namespace HotelApp.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Determines whether this repository is healthy.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> IsHealthyAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Determines whether an <typeparamref name="T"/> with the specified properties exists in this repository.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the <typeparamref name="T"/> in this repository with the specified identifier.
        /// Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        ValueTask<T> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the <typeparamref name="T"/> in this repository with the specified identifier.
        /// Or <c>null</c> if none exist.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all <typeparamref name="T"/> in this repository.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ICollection<T>> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets all <typeparamref name="T"/> that match predicate in this repository.
        /// </summary>
        /// <param name="predicate">The predicate to match</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds the specified <typeparamref name="T"/> to this repository.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Adds the specified <typeparamref name="T"/>'s to this repository.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Deletes the specified <typeparamref name="T"/>'s.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> DeleteRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified <typeparamref name="T"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Updates the specified <typeparamref name="T"/>'s.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> UpdateRangeAsync(ICollection<T> entities, CancellationToken cancellationToken = default(CancellationToken));
    }
}
