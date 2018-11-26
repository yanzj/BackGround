# 数据库仓储的实现

## MyMongoProvider

这个上下文运用MongoDB引擎，运用了基于ABP的EntityFramework

```CSharp
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
```

## MasterTestContext

这个上下文运用MySQL引擎，运用了基于ABP的EntityFramework。
定义了DT有关的数据库结构。

```CSharp
    public class MasterTestContext : AbpDbContext
    {
        public MasterTestContext()
            : base("MasterTest")
        {

        }

        public DbSet<FileRecord4G> FileRecord4Gs { get;set; }

        public DbSet<FileRecordVolte> FileRecordVoltes { get; set; }

        public DbSet<FileRecord3G> FileRecord3Gs { get; set; }

        public DbSet<FileRecord2G> FileRecord2Gs { get;set; }

    }
```
