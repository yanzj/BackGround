# Lte.MySqlFramework

该项目定义了大部分关系型数据库的表。
同样采用Entity Framework code first框架。

## 通用目录结构

1. Entities-定义数据库实体类，对应数据库表中的结构定义
1. Abstract-定义数据库仓储（Repository）接口
1. Concrete-定义数据库仓储（Repository）具体类，从接口到具体类实现由应用程序的注入依赖实现，具体到本项目，采用MVC和API的注入依赖实现。

## MySqlContext

```C#
    public class MySqlContext : AbpDbContext
    {
        public MySqlContext() : base("MySqlContext")
        {

        }
        ...
    }
```

## MySQL数据库表实体定义

## MySQL仓储接口定义例子

以下是校园网年度信息对应的仓储

```C#
    public interface ICollegeYearRepository : IRepository<CollegeYearInfo>, ISaveChanges
    {
        CollegeYearInfo GetByCollegeAndYear(int collegeId, int year);

        List<CollegeYearInfo> GetAllList(int year);
    }
```

## 数据仓储实现
