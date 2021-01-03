using System;
using MongoDB.Driver;

namespace reception.database.mongodb
{
    public class Repository<T>
    {
        private MongoClient client;
        public string DatabaseName { get; private set; }
        public string CollectionName { get; private set; }
        
        public Repository(MongoClient client)
        {
            this.client = client;
            CollectionName = typeof(T).Name;
            DatabaseName = "";
        }
        
        public Repository(string connectionString)
        : this( new MongoClient(connectionString))
        { } 
    }
}