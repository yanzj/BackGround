angular.module('myApp', ['app.common'])
    .config([
        '$routeProvider', function ($routeProvider) {
            var rootDir = "/appViews/WorkItem/";
            $routeProvider
                .when('/', {
                    templateUrl: rootDir + 'List.html',
                    controller: "kpi.workitem"
                })
                .when('/details/:number', {
                    templateUrl: rootDir + 'Details.html',
                    controller: 'kpi.workitem.details'
                })
                .when('/details/:number/:district', {
                    templateUrl: rootDir + 'Details.html',
                    controller: 'kpi.workitem.details.district'
                })
                .when('/chart', {
                    templateUrl: rootDir + 'Charts.html',
                    controller: 'kpi.workitem.chart'
                })
                .when('/eNodeb/:eNodebId/:serialNumber', {
                    templateUrl: rootDir + 'ENodebInfo.html',
                    controller: 'workitem.eNodeb'
                })
                .when('/bts/:btsId/:serialNumber', {
                    templateUrl: rootDir + 'BtsInfo.html',
                    controller: "workitem.bts"
                })
                .when('/cell/:eNodebId/:sectorId/:serialNumber', {
                    templateUrl: rootDir + 'CellInfo.html',
                    controller: "workitem.cell"
                })
                .when('/cdmaCell/:btsId/:sectorId/:serialNumber', {
                    templateUrl: rootDir + 'CdmaCellInfo.html',
                    controller: "workitem.cdmaCell"
                })
                .when('/stat/:district', {
                    templateUrl: rootDir + 'List.html',
                    controller: "workitem.district"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function ($rootScope) {
        var rootUrl = "/Kpi/WorkItem#";
        $rootScope.menuItems = [
            {
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "工单总览",
                        url: rootUrl + "/"
                    }, {
                        displayName: "统计图表",
                        url: rootUrl + "/chart"
                    }
                ]
            }, {
                displayName: "分区指标",
                isActive: true,
                subItems: []
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "工单总览",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };
        $rootScope.states = [
            {
                name: '未完成'
            }, {
                name: '全部'
            }
        ];
        $rootScope.types = [
            {
                name: '考核部分'
            },
            {
                name: '全部'
            }, {
                name: '2/3G'
            }, {
                name: '4G'
            }, {
                name: '作业计划'
            }
        ];
        $rootScope.pageSizeSelection = [
            {
                value: 10
            }, {
                value: 15
            }, {
                value: 20
            }, {
                value: 30
            }, {
                value: 50
            }
        ];
        $rootScope.viewData = {
            items: [],
            currentState: $rootScope.states[0],
            currentType: $rootScope.types[0],
            itemsPerPage: $rootScope.pageSizeSelection[1],
            totalItems: 0,
            currentPage: 1
        };
    })
.controller("kpi.workitem", function ($scope, workitemService, workItemDialog, appRegionService, menuItemService) {
    $scope.page.title = "工单总览";
    
    $scope.query = function () {
        workitemService.queryWithPaging($scope.viewData.currentState.name, $scope.viewData.currentType.name).then(function(result) {
            angular.forEach(result, function(view) {
                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
            });
            $scope.viewItems = result;
        });
    };

    $scope.updateSectorIds = function() {
        workitemService.updateSectorIds().then(function (result) {
            $scope.page.messages.push({
                contents: "一共更新扇区编号：" + result + "条",
                type: "success"
            });
        });
    };

    appRegionService.initializeCities().then(function (result) {
        appRegionService.queryDistricts(result[0]).then(function (districts) {
            angular.forEach(districts, function(district) {
                menuItemService.updateMenuItem($scope.menuItems, 1,
                    "工单统计-" + district,
                    $scope.rootPath + "stat/" + district);
            });
        });
    });
    $scope.query();
})
.controller("workitem.district", function ($scope, $routeParams, workitemService) {
    $scope.page.title = "工单统计-" + $routeParams.district;

    $scope.query = function () {
        workitemService.queryWithPagingByDistrict($scope.viewData.currentState.name, $scope.viewData.currentType.name,
            $routeParams.district).then(function (result) {
                angular.forEach(result, function (view) {
                    view.detailsPath = $scope.rootPath + "details/" + view.serialNumber + "/" + view.district;
                });
                $scope.viewItems = result;
            });
    };

    $scope.query();
})
.controller("kpi.workitem.details", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.gobackPath = $scope.rootPath;
    $scope.gobackTitle = "返回总览";

    workitemService.querySingleItem($routeParams.number).then(function (result) {
        $scope.currentView = result;
        $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
        $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
    });

})
    .controller("kpi.workitem.details.district", function ($scope, $routeParams, workitemService, workItemDialog) {
    $scope.gobackPath = $scope.rootPath + "stat/" + $routeParams.district;
    $scope.gobackTitle = "返回工单统计-" + $routeParams.district;

    workitemService.querySingleItem($routeParams.number).then(function (result) {
        $scope.currentView = result;
        $scope.platformInfos = workItemDialog.calculatePlatformInfo($scope.currentView.comments);
        $scope.feedbackInfos = workItemDialog.calculatePlatformInfo($scope.currentView.feedbackContents);
    });
    })
.controller("kpi.workitem.chart", function ($scope, $timeout, preciseChartService, workitemService) {
    $scope.page.title = "统计图表";
    workitemService.queryChartData("type-subtype").then(function (result) {
        $("#type-chart").highcharts(preciseChartService.getTypeOption(result));
    });
    workitemService.queryChartData("state-subtype").then(function (result) {
        $("#state-chart").highcharts(preciseChartService.getStateOption(result));
    });
    workitemService.queryChartData("district-town").then(function (result) {
        $("#district-chart").highcharts(preciseChartService.getDistrictOption(result));
    });
})
.controller('workitem.eNodeb', function ($scope, $uibModal, $log, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByENodebId($routeParams.eNodebId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    networkElementService.queryENodebInfo($routeParams.eNodebId).then(function (result) {
        $scope.eNodebDetails = result;
    });
    $scope.queryWorkItems();
})
.controller('workitem.bts', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByENodebId($routeParams.btsId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    networkElementService.queryBtsInfo($routeParams.btsId).then(function (result) {
        $scope.btsDetails = result;
    });
    $scope.queryWorkItems();
})
.controller('workitem.cell', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByCellId($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    networkElementService.queryCellInfo($routeParams.eNodebId, $routeParams.sectorId).then(function (result) {
        $scope.lteCellDetails = result;
    });
    $scope.queryWorkItems();
})
.controller('workitem.cdmaCell', function ($scope, networkElementService, $routeParams, workitemService) {
    $scope.serialNumber = $routeParams.serialNumber;
    $scope.queryWorkItems = function () {
        workitemService.queryByCellId($routeParams.btsId, $routeParams.sectorId).then(function (result) {
            $scope.viewItems = result;
        });
    };
    networkElementService.queryCdmaCellInfo($routeParams.btsId, $routeParams.sectorId).then(function (result) {
        $scope.cdmaCellDetails = result;
    });
    $scope.queryWorkItems();
});
