using MongoDB.Driver;

namespace fmi.Services
{
    public class MongoDbService
    {

        private readonly IMongoClient client;

        public MongoDbService(IMongoClient client)
        {
            this.client = client;
       
        }

        public IMongoDatabase GetDatabase<T> (string dbName)
        {
            return client.GetDatabase(dbName);
        }

        public IMongoCollection<T> GetCollection<T> (string dbName, string collectionName)
        {
            return client.GetDatabase(dbName).GetCollection<T>(collectionName);
        }
    }
}
