# Lte.Parameters

该工程定义了整个解决方案的基础数据库。
传统上采用SQLServer数据库，采用Entity Framework code first框架。
随着时间的推移，大部分表都迁移到MySQL数据库中。
另外从2016年开始，对接了外部的MongoDB数据库，主要存储MR数据和LTE网管参数。

## EFParametersContext

这个上下文运用SQLServer引擎，运用了基于ABP的EntityFramework
代码如下：

```C#
    public class EFParametersContext : AbpDbContext
    {
        public EFParametersContext() : base("EFParametersContext")
        {
        }
        ...
    }
```

## MasterTestContext

这个上下文也运用SQLServer引擎，但没有用EntityFramework，代码如下：

```C#
    [Database(Name = "masterTest")]
    public class MasterTestContext : DataContext
    {
        private static readonly MappingSource _mappingSource = new AttributeMappingSource();

        public MasterTestContext()
            : base(
                ConfigurationManager.ConnectionStrings["MasterTest"].ConnectionString,
                _mappingSource)
        {

        }
        ...
    }
```

## MyMongoProvider

这个上下文运用MySQL引擎，运用了基于ABP的EntityFramework

```C#
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

## SQLServer仓储接口定义例子

```C#
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>, ISaveChanges
    {
    }
```