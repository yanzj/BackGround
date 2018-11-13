# 数据访问基础

## 数据实体（Entity）

对应一张数据库表。
我们的数据库中，SQLServer和MySQL属于关系型数据库，MongoDB属于非关系型数据库。
在实际的操作中，前者需要定义上下文（Context），后者需要定义Provider。

## 数据视图

部分数据视图也随数据实体放在本工程中。

## 数据仓储

本解决方案的数据仓储（Repository）大多采用ABP。
定义的接口集成自ABP的IRepository，使用泛型。
无论采用SQLServer还是MySQL，其定义的方式都是一致的。
在本项目中，仅定义了一部分公共数据结构。

## 数据映射逻辑（AutoMapper）

数据库结构与查询结果数据结构之间的映射逻辑。

## 查询辅助函数（Dependency）

数据库接口的辅助数据接口
