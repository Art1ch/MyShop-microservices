using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using StoreService.Core.Entities;
using StoreService.Infrastructure.Settings;

namespace StoreService.Infrastructure.Context
{
    public class StoreContext
    {
        private readonly IMongoDatabase _database;
        public StoreContext(IOptions<MongoDbSettings> settings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            var mongoClient = new MongoClient(settings.Value.ConnectionString);
            _database = mongoClient.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<ProductEntity> Products => _database.GetCollection<ProductEntity>("Products");
        public IMongoCollection<BasketEntity> Baskets => _database.GetCollection<BasketEntity>("Baskets");
    }
}
