# 数据访问基础

## 数据实体（Entity）

对应一张数据库表。
我们的数据库中，SQLServer和MySQL属于关系型数据库，MongoDB属于非关系型数据库。
在实际的操作中，前者需要定义上下文（Context），后者需要定义Provider。

## 数据仓储

本解决方案的数据仓储（Repository）大多采用ABP。
定义的接口集成自ABP的IRepository，使用泛型。
无论采用SQLServer还是MySQL，其定义的方式都是一致的。
