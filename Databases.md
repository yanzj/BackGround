# 数据库接口定义
定义了对数据库访问的适配数据结构。
目前实现了对SQLServer、MySQL和MongoDB三种数据库引擎的访问，分别对应不同的数据库表。
## 本平台所用数据库说明
### 概述 
本网优平台主要运用几个数据库平台，按照数据库引擎内容可以分为SQLServer数据库、MySQL数据库和Mongo数据库。
### Lte.Parameters
该工程定义了整个解决方案的基础数据库。
传统上采用SQLServer数据库，采用Entity Framework code first框架。
随着时间的推移，大部分表都迁移到MySQL数据库中。
另外从2016年开始，对接了外部的MongoDB数据库，主要存储MR数据和LTE网管参数。
### Lte.MySqlFramework
该项目定义了大部分关系型数据库的表。
同样采用Entity Framework code first框架。
### 数据库包括但不仅限于：
1. LTE基础数据库：主要定义了eNodeb、小区信息，从工参文件导入；
1. CDMA基础数据库：类似于LTE基础数据库，并作为后者的补充，主要定义了BTS、CDMA小区信息，也是从工参文件导入；
1. 日常2G、3G、4G KPI指标（传统指标）：目前包括掉话率、3G连接成功率、话务量、3G流量等指标，由每日收到省中心发出的地市监控日报数据再次导入，更新较为完善。
1. 日常4G精确覆盖率指标：主要包括按照镇区聚合、单小区的每天重叠覆盖率指标统计，已提供专门的导入接口，每日由合作伙伴导入；
1. MRS和MRO数据提取结果：统计MR数据的总体统计指标，目前采用MongoDB数据库；
1. MRO数据提取结果：统计周期性上报小区测量信息，已转化为小区间干扰关系，目前导入机制是从Mongo数据库间接生成按小区的统计记录，后续计划直接在MongoDB上存储原始数据；
1. 邻区定义信息：由于MR数据仅上报邻区PCI，这里定义了根据中心小区和邻区PCI推断邻小区信息的关系数据；
1. 告警信息：从网管系统导入，目前仅用于校园网专题。
1. 工单信息：已提供导入接口，从省中心4G平台上导出后再导入。
1. 校园网基础信息：2015年专项时建立，待完善。
1. DT数据：Sqlserver数据，采用存储过程导入。
### 通用目录结构
1. Entities-定义数据库实体类，对应数据库表中的结构定义
1. Abstract-定义数据库仓储（Repository）接口
1. Concrete-定义数据库仓储（Repository）具体类，从接口到具体类实现由应用程序的注入依赖实现，具体到本项目，采用MVC和API的注入依赖实现。

## 数据访问基础
数据实体（Entity）对应一张数据库表。
我们的数据库中，SQLServer和MySQL属于关系型数据库，MongoDB属于非关系型数据库。
在实际的操作中，前者需要定义上下文（Context），后者需要定义Provider。
### EFParametersContext
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
### MasterTestContext
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
### MyMongoProvider
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
### MySqlContext
```C#
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {
            
        }
        ...
    }
```
## 数据仓储
本解决方案的数据仓储（Repository）大多采用ABP。
定义的接口集成自ABP的IRepository，使用泛型。
无论采用SQLServer还是MySQL，其定义的方式都是一致的。
### SQLServer仓储接口定义例子
```C#
    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>, ISaveChanges
    {
    }
```
### MySQL仓储接口定义例子
```C#
    public interface ICollegeYearRepository : IRepository<CollegeYearInfo>, ISaveChanges
    {
        CollegeYearInfo GetByCollegeAndYear(int collegeId, int year);

        List<CollegeYearInfo> GetAllList(int year);
    }
```
## 数据仓储实现