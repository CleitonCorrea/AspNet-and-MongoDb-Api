using ApiRestWithNetCoreAndMongoDb.Settings;
using Microsoft.Extensions.Options;

namespace ApiRestWithNetCoreAndMongoDb.Repository
{
    public interface IProductRepository:         IRepository<Entities.Product>
    {
    }
    public class ProductRepository : Repository<Entities.Product>, IProductRepository
    {
        public ProductRepository(IOptions<MongoDbSettings> settings) : base(settings)
        {

        }
    }
}
