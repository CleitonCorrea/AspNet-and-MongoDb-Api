using MongoDB.Bson.Serialization.Attributes;

namespace ApiRestWithNetCoreAndMongoDb.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        private string Id { get; set; }
        private string Name { get; set; }

        private string Price { get; set; }
    }
}
