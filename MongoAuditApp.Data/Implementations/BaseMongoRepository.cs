using System.Linq.Expressions;
using MongoAuditApp.Data.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoAuditApp.Data.Implementations
{
    public abstract class BaseMongoRepository<T> : IRepository<T> where T : class, IMongoEntity
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoDatabase Database;

        private BaseMongoRepository(IMongoDatabase database, string collectionName)
        {
            Database = database;
            _collection = Database.GetCollection<T>(collectionName);
        }

        protected BaseMongoRepository(IMongoDatabase database)
            : this(database, typeof(T).Name)
        {
        }


        public virtual async Task<T> AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<ICollection<T>> AddRangeAsync(ICollection<T> entities)
        {
            await _collection.InsertManyAsync(entities);
            return entities;
        }

        public virtual async Task<bool> DeleteAsync(T entity)
        {
            var result = await _collection.DeleteOneAsync(e => e.Id == entity.Id);
            return result.IsAcknowledged;
        }

        public virtual async Task<bool> DeleteRangeAsync(ICollection<T> entities)
        {
            bool errorOccured = false;
            var deletedEntities = new List<T>();

            foreach (var entity in entities)
            {
                var response = await _collection.DeleteOneAsync(e => e.Id == entity.Id);
                if (response.IsAcknowledged)
                {
                    deletedEntities.Add(entity);
                }
                else
                {
                    errorOccured = true;
                    break;
                }
            }

            if (errorOccured == true)
            {
                await _collection.InsertManyAsync(deletedEntities);
            }

            return (errorOccured == false);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate) =>
            (await _collection.FindAsync(predicate)).First() != null;

        public virtual async Task<ICollection<T>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<ICollection<T>> FilterAsync(Expression<Func<T, bool>> predicate) =>
            await _collection.Find(predicate).ToListAsync();

        public virtual async ValueTask<T> GetAsync(ObjectId id) =>
            (await _collection.FindAsync(e => e.Id == id)).First();

        public virtual async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            var response = await _collection.FindAsync(predicate);

            return await response.SingleOrDefaultAsync();
        }

        public virtual async Task<bool> IsHealthyAsync()
        {
            await SingleOrDefaultAsync(_ => true);
            return true;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            var response = await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);

            return response.IsAcknowledged;
        }

        public virtual async Task<bool> UpdateRangeAsync(ICollection<T> entities)
        {
            bool errorOccured = false;
            foreach (var entity in entities)
            {
                var response = await _collection.UpdateOneAsync(e => e.Id == entity.Id, Builders<T>.Update.Set(e => e, entity));

                if (response.IsAcknowledged == false)
                {
                    errorOccured = true;
                    break;
                }
            }

            return errorOccured == false;
        }
    }
}
