
using ApiRestWithNetCoreAndMongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ApiRestWithNetCoreAndMongoDb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public Repository(IOptions<MongoDbSettings> settings)
        {
            // Initialize the repository, e.g., set up a database connection

            //Instance of Mongo Db
            var client = new MongoClient(settings.Value.ConnectionString);

            //Database instance
            var database = client.GetDatabase(settings.Value.DatabaseName);

            //Collection from the database
            _collection = database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());

        }

        public async Task CreateAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _collection.DeleteOneAsync(filter);
        }

        public Task<bool> ExistsAsync(string id)
        {
            var objectId = new ObjectId(id);
            return _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).AnyAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var objectId = new ObjectId(id);

            return await _collection.Find(Builders<T>.Filter.Eq("_id", objectId)).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, T entity)
        {
           var objectId = new ObjectId(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }
}
