# Lte.MySqlFramework

该项目定义了大部分关系型数据库的表。
同样采用Entity Framework code first框架。

## 通用目录结构

1. Entities-定义数据库实体类，对应数据库表中的结构定义
1. Abstract-定义数据库仓储（Repository）接口
1. Concrete-定义数据库仓储（Repository）具体类，从接口到具体类实现由应用程序的注入依赖实现，具体到本项目，采用MVC和API的注入依赖实现。

## MySqlContext

```CSharp
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {

        }
        ...
    }
```

## MySQL数据库表实体定义

一个类对应数据库中的一张表，这部分内容大多迁移到`Abp.EntityFramework`项目中

## 数据视图定义

原来在项目`Lte.Evaluations`中，迁移到本项目。

## MySQL仓储接口定义例子

### 基本配置

以下是校园网年度信息对应的仓储接口，它继承了两个接口：

1. `IRepository<T>`，来自ABP库，定义了较完备的增删查改操作
1. `ISaveChanges`，增和改后需要保存，这个接口包含了`SaveChanges()`函数

```CSharp
    public interface ICollegeYearRepository : IRepository<CollegeYearInfo>, ISaveChanges
    {
        CollegeYearInfo GetByCollegeAndYear(int collegeId, int year);

        List<CollegeYearInfo> GetAllList(int year);
    }
```

### 扩展配置

以下是华为CQI优良比指标对应的仓储接口，除了继承以上两个接口外，
还继承了`IMatchRepository<T>`和`IMatchRepository<T>`两个接口。

```CSharp
    public interface ICqiZteRepository : IRepository<CqiZte>, ISaveChanges, IMatchRepository<CqiZte>,
        IFilterTopRepository<CqiZte>
    {
    }
```

## 数据仓储实现

对仓储接口进行实现，我们采用单例模式，应用程序里面采用依赖注入调用相应接口的实现。
