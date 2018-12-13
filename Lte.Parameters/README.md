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

## DT(MySQL)仓储接口定义例子

```C#
    public interface IFileRecord2GRepository : IRepository<FileRecord2G>, ISaveChanges, IMatchRepository<FileRecord2G>
    {
    }
```

## 网管参数数据库设计

### 网管参数的定义和范围

1. 网管参数是指厂家网管（如华为、中兴）定义的各类软参数

1. 4G网管中最重要的参数类包括切换、重选、调度等

### 网管参数的组织形式

1. 存在于网管数据库中

1. 为了安全性，考虑到参数改动并不循环，我们从网管中提取XML文件，再导入我们自己建立的MongoDB数据库

### 网管参数在MongoDB数据库中的存储

1. 华为每条MML文件指令对应一张表

1. 中兴每个菜单项对应一张表

## 项目总体结构

### 数据实体和视图定义

按照ABP项目的规范定义

### 仓储接口定义

按照ABP项目的规范定义

### 仓储实现

按照ABP项目的规范定义
