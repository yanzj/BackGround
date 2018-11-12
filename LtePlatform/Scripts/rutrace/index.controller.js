angular.module('myApp', ['app.common'])
    .config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Rutrace/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "",
                    controller: ""
                })
                .when('/traditional', {
                    templateUrl:  '/appViews/BasicKpi/Index.html',
                    controller: "kpi.basic"
                })
                .when('/topDrop2G', {
                    templateUrl: '/appViews/BasicKpi/TopDrop2G.html',
                    controller: 'kpi.topDrop2G'
                })
                .when('/details/:number', {
                    templateUrl: viewDir + "WorkItem/AnalyticDetails.html",
                    controller: "workitem.details"
                })
                .when('/workItems/:cellId/:sectorId/:name', {
                    templateUrl: viewDir + 'WorkItem/ForCell.html',
                    controller: "rutrace.workitems"
                })
                .when('/mongo', {
                    templateUrl: viewDir + 'FromMongo.html',
                    controller: 'interference.mongo'
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function ($rootScope, kpiPreciseService) {
        var rootUrl = "/Rutrace#";
        $rootScope.menuItems = [
            {
                displayName: "总体情况",
                isActive: true,
                subItems: [
                    {
                        displayName: "指标总览",
                        url: rootUrl + "/"
                    }, {
                        displayName: "精确覆盖",
                        url: rootUrl + "/trend"
                    }, {
                        displayName: "传统指标",
                        url: rootUrl + "/traditional"
                    }
                ]
            }, {
                displayName: "详细查询",
                isActive: false,
                subItems: []
            }, {
                displayName: "TOP指标",
                isActive: false,
                subItems: [
                    {
                        displayName: "精确覆盖率TOP指标",
                        url: rootUrl + "/top"
                    }, {
                        displayName: "TOP传统指标",
                        url: rootUrl + "/topDrop2G"
                    }
                ]
            }, {
                displayName: "辅助功能",
                isActive: false,
                subItems: [
                    {
                        displayName: "从MongoDB导入",
                        url: rootUrl + "/mongo"
                    }
                ]
            }
        ];
        $rootScope.rootPath = rootUrl + "/";

        $rootScope.viewData = {
            workItems: []
        };
        
        var lastWeek = new Date();
        lastWeek.setDate(lastWeek.getDate() - 7);
        $rootScope.beginDate = {
            value: new Date(lastWeek.getFullYear(), lastWeek.getMonth(), lastWeek.getDate(), 8),
            opened: false
        };
        
        var today = new Date();
        $rootScope.endDate = {
            value: new Date(today.getFullYear(), today.getMonth(), today.getDate(), 8),
            opened: false
        };
        
    })
    .controller('interference.mongo', function ($scope, neighborMongoService, neighborDialogService,
        dumpProgress, networkElementService, dumpPreciseService) {
        $scope.progressInfo = {
            dumpCells: [],
            totalSuccessItems: 0,
            totalFailItems: 0,
            cellInfo: ""
        };
        $scope.page.title = "从MongoDB导入";
        $scope.currentPage = 1;

        $scope.reset = function () {
            dumpProgress.resetProgress($scope.beginDate.value, $scope.endDate.value).then(function (result) {
                $scope.progressInfo.dumpCells = result;
                $scope.progressInfo.totalFailItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                angular.forEach($scope.progressInfo.dumpCells, function (cell) {
                    networkElementService.queryENodebInfo(cell.eNodebId).then(function (eNodeb) {
                        cell.name = eNodeb.name;
                    });
                    cell.dumpInfo = "未开始";
                });
            });
        };

        $scope.dumpMongo = function (cell) {
            neighborDialogService.dumpCellMongo(cell, $scope.beginDate.value, $scope.endDate.value);
        };

        $scope.generateDumpRecords = function (dumpRecords, startDate, endDate, eNodebId, sectorId, pci) {
            if (startDate >= endDate) {
                dumpPreciseService.dumpAllRecords(dumpRecords, 0, 0, eNodebId, sectorId, $scope.dump);
                return;
            }
            var date = new Date(startDate);
            dumpProgress.queryExistedItems(eNodebId, sectorId, date).then(function (existed) {
                dumpProgress.queryMongoItems(eNodebId, pci, date).then(function (records) {
                    dumpRecords.push({
                        date: date,
                        existedRecords: existed,
                        mongoRecords: records
                    });
                    startDate.setDate(date.getDate() + 1);
                    $scope.generateDumpRecords(dumpRecords, startDate, endDate, eNodebId, sectorId, pci);
                });
            });
        };

        $scope.dump = function () {
            if ($scope.progressInfo.totalSuccessItems >= $scope.progressInfo.dumpCells.length) return;
            var cell = $scope.progressInfo.dumpCells[$scope.progressInfo.totalSuccessItems];
            cell.dumpInfo = "已导入";
            $scope.progressInfo.totalSuccessItems += 1;
            var eNodebId = cell.eNodebId;
            var sectorId = cell.sectorId;
            var pci = cell.pci;
            var begin = $scope.beginDate.value;
            var startDate = new Date(begin);
            var end = $scope.endDate.value;
            var endDate = new Date(end);
            var dumpRecords = [];
            $scope.generateDumpRecords(dumpRecords, startDate, endDate, eNodebId, sectorId, pci);
        };

        $scope.reset();
    });
