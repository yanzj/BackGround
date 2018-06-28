using Abp.EntityFramework.Dependency;
using MongoDB.Driver;

namespace Lte.Parameters.Concrete
{
    public class MyMongoProvider : IMongoDatabaseProvider
    {
        private static readonly MongoClient Client = new MongoClient("mongodb://root:Abcdef9*@132.110.71.123:27017");

        public MyMongoProvider(string databaseString)
        {
#pragma warning disable 618
            var server = Client.GetServer();
#pragma warning restore 618
            Database = server.GetDatabase(databaseString);
        }

        public MongoDatabase Database { get; }
    }

    public class OuterMongoProvider : IMongoDatabaseProvider
    {
        private static readonly MongoClient Client = new MongoClient("mongodb://root:Abcdef9*@119.145.142.74:8124");

        public OuterMongoProvider(string databaseString)
        {
#pragma warning disable 618
            var server = Client.GetServer();
#pragma warning restore 618
            Database = server.GetDatabase(databaseString);
        }

        public MongoDatabase Database { get; }
    }
}
