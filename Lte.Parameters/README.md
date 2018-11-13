# Lte.Parameters

该工程定义了整个解决方案的部分基础数据库。
传统上采用SQLServer数据库，采用Entity Framework code first框架。
随着时间的推移，大部分表都迁移到MySQL数据库中。
另外从2016年开始，对接了外部的MongoDB数据库，主要存储MR数据和LTE网管参数。

## EFParametersContext（已废弃）

这个上下文运用SQLServer引擎，运用了基于ABP的EntityFramework。
2018年10月5日开始，除了账号权限验证数据与基础库密切相关而暂时无法迁移外，其余数据表均迁移到MySQL引擎。

## MasterTestContext（已迁移到MySQL）

2018年10月5日开始，除了账号权限验证数据与基础库密切相关而暂时无法迁移外，其余数据表均迁移到MySQL引擎。

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
