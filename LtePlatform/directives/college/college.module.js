angular.module('college.module',
    ['college.info.module', 'college.stat.module', 'general.test.module']);

angular.module('college.info.module', ['ui.grid', 'myApp.region', 'myApp.url'])
    .controller('CollegeInfoController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'name', name: '校园名称', width: 200, enableColumnResizing: false },
                    { field: 'totalStudents', name: '在校学生数' },
                    { field: 'graduateStudents', name: '毕业用户数' },
                    { field: 'currentSubscribers', name: '当前用户数' },
                    { field: 'newSubscribers', name: '新发展用户数' },
                    { field: 'expectedSubscribers', name: '预计到达用户数' },
                    { field: 'oldOpenDate', name: '老生开学日期', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'newOpenDate', name: '新生开学日期', cellFilter: 'date: "yyyy-MM-dd"' }
                ],
                data: []
            };
        })
    .directive('collegeInfoList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CollegeInfoController',
                    scope: {
                        colleges: '='
                    },
                    argumentName: 'colleges'
                },
                $compile);
        });

angular.module('college.stat.module', ['ui.grid', 'myApp.region', 'myApp.url'])
    .controller('CollegeStatController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'name', name: '校园名称', width: 170, enableColumnResizing: false },
                    { field: 'expectedSubscribers', name: '用户数', width: 40 },
                    {
                        field: 'area',
                        name: '区域面积（平方米）',
                        cellFilter: 'number: 2',
                        width: 90,
                        enableColumnResizing: false
                    },
                    {
                        name: '4G基站数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-primary">' +
                                '详情<span class="badge pull-right">{{row.entity.totalLteENodebs}}</span></button>'
                    },
                    {
                        name: '4G小区数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-primary">' +
                                '详情<span class="badge pull-right">{{row.entity.totalLteCells}}</span></button>'
                    },
                    {
                        name: '3G基站数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-default">' +
                                '详情<span class="badge pull-right">{{row.entity.totalCdmaBts}}</span></button>'
                    },
                    {
                        name: '3G小区数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-default">' +
                                '详情<span class="badge pull-right">{{row.entity.totalCdmaCells}}</span></button>'
                    },
                    {
                        name: '4G室分数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-default">' +
                                '详情<span class="badge pull-right">{{row.entity.totalLteIndoors}}</span></button>'
                    },
                    {
                        name: '3G室分数',
                        cellTemplate:
                            '<button class="btn btn-sm btn-default">' +
                                '详情<span class="badge pull-right">{{row.entity.totalCdmaIndoors}}</span></button>'
                    }
                ],
                data: []
            };
        })
    .directive('collegeStatTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CollegeStatController',
                    scope: {
                        collegeList: '='
                    },
                    argumentName: 'collegeList'
                },
                $compile);
    });

angular.module('general.test.module', ['ui.grid', 'myApp.region', 'myApp.url'])
    .controller('DtInfoListController', function ($scope, collegeService, generalMapService, coverageService) {
        $scope.gridOptions = {
            paginationPageSizes: [20, 40, 60],
            paginationPageSize: 20,
            columnDefs: [
                { field: 'csvFileName', name: '测试文件名称', width: 200 },
                { field: 'networkType', name: '网络类型' },
                { field: 'distance', name: '测试里程' },
                { field: 'testDate', name: '测试日期', cellFilter: 'date: "yyyy-MM-dd"' },
                { field: 'count', name: '测试点数' },
                { field: 'coverageRate', name: '覆盖率（%）' },
                {
                    name: '计算指标',
                    width: 100,
                    cellTemplate: '<button class="btn btn-sm btn-primary" ng-click="grid.appScope.calculateDistance(row.entity)">计算</button>\
                    <button class="btn btn-sm btn-default" ng-click="grid.appScope.updateDistance(row.entity)">更新</button>'
                },
                {
                    name: '分镇区信息',
                    cellTemplate: '<button class="btn btn-sm btn-success" ng-disabled="row.entity.townInfos.length" ng-click="grid.appScope.calculateTownInfo(row.entity)">\
                        <div class="badge">{{row.entity.townInfos.length}}</div>计算</button>'
                },
                {
                    name: '道路信息',
                    cellTemplate: '<button class="btn btn-sm btn-success" ng-disabled="row.entity.roadInfos.length" ng-click="grid.appScope.calculateRoadInfo(row.entity)">\
                        <div class="badge">{{row.entity.roadInfos.length}}</div>计算</button>'
                }
            ],
            data: []
        };
        $scope.calculateDistance = function (file) {
            var name = file.csvFileName.replace(".csv", "");
            switch (file.networkType) {
                case '3G':
                    collegeService.query3GFileRecords(name).then(function (records) {
                        file.distance = generalMapService.calculateRoadDistance(records);
                        file.count = records.length;
                        file.coverageCount = _.countBy(records,
                            function (record) {
                                return record.sinr > -6.5 && record.rxAgc0 > -90 && record.rxAgc1 > -90 && record.txAgc < 15;
                            })['true'];
                        file.coverageRate = 100 * file.coverageCount / file.count;
                    });
                    break;
                case '4G':
                    collegeService.query4GFileRecords(name).then(function (records) {
                        file.distance = generalMapService.calculateRoadDistance(records);
                        file.count = records.length;
                        file.coverageCount = _.countBy(records,
                            function (record) {
                                return record.sinr > -3 && record.rsrp > -105;
                            })['true'];
                        file.coverageRate = 100 * file.coverageCount / file.count;
                    });
                    break;
                case 'Volte':
                    collegeService.queryVolteFileRecords(name).then(function (records) {
                        file.distance = generalMapService.calculateRoadDistance(records);
                        file.count = records.length;
                        file.coverageCount = _.countBy(records,
                            function (record) {
                                return record.sinr > -3 && record.rsrp > -105;
                            })['true'];
                        file.coverageRate = 100 * file.coverageCount / file.count;
                    });
                    break;
                default:
                    collegeService.query2GFileRecords(name).then(function (records) {
                        file.distance = generalMapService.calculateRoadDistance(records);
                        file.count = records.length;
                        file.coverageCount = _.countBy(records,
                            function (record) {
                                return record.ecio > -12 && record.rxAgc > -90 && record.txAgc < 15;
                            })['true'];
                        file.coverageRate = 100 * file.coverageCount / file.count;
                    });
                    break;
            }
        };
        $scope.updateDistance = function (file) {
            collegeService.updateCsvFileDistance(file).then(function (result) {

            });
        };
        $scope.calculateTownInfo = function(file) {
            collegeService.calculateTownDtTestInfos(file.csvFileName.replace(".csv", ""), file.networkType).then(function(results) {
                file.townInfos = results;
                angular.forEach(results,
                    function(info) {
                        collegeService.updateAreaDtInfo(info).then(function(result) {
                            coverageService.updateTownTestDate(file.testDate, file.networkType, info.townId)
                                .then(function(count) {

                                });
                        });
                    });
            });
        };
        $scope.calculateRoadInfo = function (file) {
            collegeService.calculateRoadDtTestInfos(file.csvFileName.replace(".csv", ""), file.networkType).then(function (results) {
                file.roadInfos = results;
                angular.forEach(results,
                    function (info) {
                        collegeService.updateAreaDtInfo(info).then(function (result) {

                        });
                    });
            });
        };
    })
    .directive('dtInfoList', function ($compile, calculateService) {
        return calculateService.generatePagingGridDirective({
            controllerName: 'DtInfoListController',
            scope: {
                items: '='
            },
            argumentName: 'items'
        }, $compile);
    });