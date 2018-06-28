# Angular工厂服务基础路由层
在这一层中，完成了一些基本运算和数据处理模块服务。
分为核心、菜单、格式、几何和运算五大模块。
## app.core模块
这是核心模块，是前端模块中最基本的服务。
主要包括appUrlService和generalHttpService两个服务。于是代码结构如下：
```javascript
angular.module('app.core', [])
...
    .factory('appUrlService', function(publicNetworkIp) {
    ...
    })
    ...
    .factory('generalHttpService', function ($q, $http, $sce, appUrlService) {
    ...
    });
```
### appUrlService服务
### generalHttpService服务
## app.menu模块
## app.format模块
## app.geometry模块
## app.calculation