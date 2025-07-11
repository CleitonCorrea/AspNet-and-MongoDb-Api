﻿using MongoDB.Bson.Serialization.Attributes;

namespace ApiRestWithNetCoreAndMongoDb.Entities
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        public string Price { get; set; }
    }
}
