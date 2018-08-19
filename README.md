# LTE网络优化平台

<p align="center">
  <a href="https://travis-ci.com/fsouyh18/BackGround"><img src="https://travis-ci.com/fsouyh18/BackGround.svg?branch=master" alt="slack" ></a>
</p>

该解决方案是一个主要以WEB页面形式为呈现方式的`LTE`综合网络优化分析呈现平台。
该平台的程序是一个用`Visual Studio`开发的解决方案。

## 解决方案总体结构

解决方案共分XX个项目，
其中程序项目X个（主项目X个，辅助项目X个），测试项目X个。
程序项目分为`后端模块`、`前端模块`和`辅助模块`三部分。

### 主项目

#### Abp.EntityFramework.Customize

该项目在`ABP`模块库的一个工程`Abp.EntityFramework`的一个部分。
本解决方案利用ABP的基础模块，加上自己的一部分内容，扩充数据库访问的通用功能，2018年搬迁部分数据库实体到本项目。

#### Lte.Domain

定义了公共的数据结构和基础数据设施。

#### Lte.MySqlFramework

定义了使用MySQL数据库引擎的有关数据实体、仓储及其实现，
以及部分数据实体类的映射。

#### Lte.Parameters

定义了使用SQLServer和MongoDB数据库引擎的有关数据实体、仓储及其实现，
以及部分数据实体类的映射。

#### Lte.Evaluations

定义了数据应用层模块。

#### LtePlatform

是解决方案的主项目。
包括前端模块和后端模块中的用户认证模块，
各类 `ASP.Net Web API2`控制器的定义。

### 辅助项目

#### TraceParser

LTE信令数据的解析库，采用C#语言对16进制信令内容进行解码。
由于目前信令的提取和存储脚本还没开发，因此本项目暂时没有应用。

#### ZipLib

压缩文件处理库，由于是C#项目，目前暂时没有应用。

### 测试项目

这部分主要是对各个后端模块的工程的单元测试，而对前端的测试模块在主工程中。
包括以下测试项目：

#### Abp.EntityFramework.Tests

`Abp.EntityFramework`对应的测试项目，采用NUnit。

#### Abp.Tests

`ABP`库配置的测试文件，采用XUnit。

#### Lte.Domain.Test

`Lte.Domain`对应的测试项目，采用NUnit。

#### Lte.Evaluation.Test

`Lte.Evaluation`对应的测试项目，采用NUnit。

#### Lte.Parameters.Test

`Lte.Parameters`和`Lte.MySqlFramework`对应的测试项目，采用NUnit。

#### LtePlatform.Tests

`LtePlatform`对应的测试项目，采用NUnit。

#### TraceParser.Test

## 后端模块

### 数据库接口模块

本平台采用了SQLServer、MySQL、MongoDB等数据库引擎。
采用ABP的仓储架构。详细说明详见
[这里](https://github.com/WirelessFoshan/LtePlatform/blob/master/Databases.md)

### 数据应用层

调用各种数据库访问模块，详细说明详见
[这里](https://github.com/WirelessFoshan/LtePlatform/blob/master/Evaluations.md)

### 基础数据设施

定义了基本的数据类型和各类公用操作函数、类等，详细说明详见
[这里](https://github.com/WirelessFoshan/LtePlatform/blob/master/Infrastructure.md)

## 前端模块

前端程序主要采用了谷歌的AngularJS1.x和Twitter的Bootstrap3框架。

### Javascript脚本

AngularJS架构的Javascript脚本详见
[这里](https://github.com/WirelessFoshan/LtePlatform/blob/master/Angular.md) 
