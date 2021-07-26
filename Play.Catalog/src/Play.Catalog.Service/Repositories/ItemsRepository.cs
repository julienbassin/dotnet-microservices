using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Play.Catalog.Service.Entites;

namespace Play.Catalog.Service.Repositories
{
    public class ItemsRepository
    {
        private const string CollectionName = "items";
        private readonly IMongoCollection<Item> dbCollection;
        private readonly FilterDefinitionBuilder<Item> filterBuilder = Builders<Item>.Filter;

        public ItemsRepository()
        {
            var mongoClient = new MongoClient("mongodb://localhost:27017");
            var database = mongoClient.GetDatabase("Catalog");
            dbCollection = database.GetCollection<Item>(CollectionName);
        }

        public async Task<IReadOnlyCollection<Item>> GetTaskAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }
    }
}