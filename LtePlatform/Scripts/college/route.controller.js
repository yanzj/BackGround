angular.module('myApp', ['app.common'])
    .config(function($stateProvider, $urlRouterProvider) {
        var viewDir = "/appViews/College/";
        $stateProvider
            .state('root', {
                templateUrl: viewDir + "Root.html"
            })
            .state('root.collegeMap', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "CollegeMap.html",
                        controller: "map.name"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenuType.html",
                        controller: "college.menu"
                    }
                },
                url: "/map/:name/:type"
            }).state('root.coverage', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "/Coverage/All.html",
                        controller: "all.coverage"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenu.html",
                        controller: "college.menu"
                    }
                },
                url: "/coverage"
            }).state('root.collegeCoverage', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "/Coverage/CollegeMap.html",
                        controller: "coverage.name"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenu.html",
                        controller: "college.menu"
                    }
                },
                url: "/coverage/:name"
            }).state('root.support', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "/Coverage/Support.html",
                        controller: "all.support"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "/Coverage/SupportMenu.html",
                        controller: "support.menu"
                    }
                },
                url: "/support"
            }).state('root.collegeSupport', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "/Coverage/CollegeSupport.html",
                        controller: "support.name"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "/Coverage/SupportMenu.html",
                        controller: "support.menu"
                    }
                },
                url: "/support/:number"
            }).state('root.collegeFlow', {
                views: {
                    "contents": {
                        templateUrl: viewDir + "/Test/CollegeFlow.html",
                        controller: "flow.name"
                    },
                    'collegeList': {
                        templateUrl: viewDir + "CollegeMenu.html",
                        controller: "college.menu"
                    }
                },
                url: "/flow/:name"
            });
        $urlRouterProvider.otherwise('/');
    })
    .run(function ($rootScope, collegeService, appRegionService) {
        $rootScope.menuTitle = "功能列表";
        var rootUrl = "/College/Map#";
        $rootScope.menuItems = [
            {
                displayName: "覆盖情况",
                tag: "coverage",
                isActive: true,
                subItems: [
                    {
                        displayName: "校园网总览",
                        url: rootUrl + "/"
                    }, {
                        displayName: "基础信息查看",
                        url: rootUrl + "/query"
                    }, {
                        displayName: "校园覆盖",
                        url: rootUrl + "/coverage"
                    }
                ]
            }, {
                displayName: "容量质量",
                tag: "management",
                isActive: true,
                subItems: [
                    {
                        displayName: "支撑任务",
                        url: rootUrl + "/support"
                    }, {
                        displayName: "流量分析",
                        url: rootUrl + "/flow"
                    }, {
                        displayName: "精确覆盖",
                        url: rootUrl + "/precise"
                    }
                ]
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.menuPath = "/appViews/GeneralMenu.html";

        $rootScope.collegeInfo = {
            year: {
                options: [2015, 2016, 2017],
                selected: new Date().getYear() + 1900
            },
            url: $rootScope.rootPath + "map",
            names: [],
            type: "",
            supportInfos: []
        };
        $rootScope.page = {
            title: "校园网总览",
            messages: [],
            projecteName: ""
        };
        
        $rootScope.town = {
            options: [],
            selected: ""
        };
        appRegionService.initializeCities().then(function (cities) {
            $rootScope.city = {
                selected: cities[0],
                options: cities
            };
            appRegionService.queryDistricts(cities[0]).then(function (districts) {
                $rootScope.district = {
                    options: districts,
                    selected: districts[0]
                };
            });
        });

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
        $rootScope.closeAlert = function($index) {
            $rootScope.page.messages.splice($index, 1);
        };
    })

    .controller("college.menu", function ($scope, $stateParams) {
        $scope.collegeInfo.type = $stateParams.type || 'lte';
        $scope.collegeName = $stateParams.name;
    })
    .controller("support.menu", function ($scope, emergencyService) {
        $scope.collegeInfo.url = $scope.rootPath + "support";
        emergencyService.queryCollegeVipDemands($scope.collegeInfo.year.selected).then(function(items) {
            $scope.collegeInfo.supportInfos = items;
        });
    })
    .controller("map.name", function($scope, $stateParams,
        baiduMapService, baiduQueryService, collegeService, collegeQueryService, collegeMapService,
        parametersMapService, parametersDialogService) {

        $scope.collegeInfo.url = $scope.rootPath + "map";
        $scope.collegeName = $stateParams.name;
        
        switch ($stateParams.type) {
        case 'cdma':
            collegeService.queryBtss($scope.collegeName).then(function (btss) {
                if (btss.length) {
                    parametersMapService.showENodebsElements(btss, parametersDialogService.showENodebInfo);
                }
            });
            collegeService.queryCdmaCells($scope.collegeName).then(function (cells) {
                if (cells.length) {
                    parametersMapService.showCellSectors(cells, parametersDialogService.showCollegeCdmaCellInfo);
                }
            });
            break;
        case 'lteDistribution':
            collegeService.queryLteDistributions($scope.collegeName).then(function (distributions) {
                if (distributions.length) {
                    parametersMapService.showENodebsElements(distributions, parametersDialogService.showDistributionInfo);
                }
            });
            break;
        default:
            collegeService.queryCdmaDistributions($scope.collegeName).then(function (distributions) {
                if (distributions.length) {
                    parametersMapService.showENodebsElements(distributions, parametersDialogService.showDistributionInfo);
                }
            });
            break;
        }
    })

    .controller("all.support", function ($scope, collegeQueryService, emergencyService) {
        $scope.page.title = "支撑任务";
        $scope.updateInfos = function (year) {
            collegeQueryService.queryYearList(year).then(function (colleges) {
                $scope.collegeYearList = colleges;
            });
        };
        $scope.query = function() {
            emergencyService.queryCollegeVipDemands($scope.collegeInfo.year.selected).then(function(items) {
                $scope.collegeInfo.supportInfos = items;
            });
        };

        $scope.$watch('collegeInfo.year.selected', function (year) {
            $scope.updateInfos(year);
        });
    })
    .controller("support.name", function ($scope, $stateParams, customerQueryService, emergencyService, collegeDialogService) {
        $scope.queryProcessList = function() {
            emergencyService.queryVipProcessList($stateParams.number).then(function(items) {
                $scope.processItems = items;
            });
        };
        $scope.query = function() {
            customerQueryService.queryOneVip($stateParams.number).then(function(item) {
                $scope.item = item;
                $scope.page.projectName = item.projectName;
                if ($scope.item.nextStateDescription) {
                    $scope.processInfo = "已发起" + $scope.item.nextStateDescription + "流程";
                } else {
                    $scope.processInfo = "";
                }
            });
            $scope.queryProcessList();
        };
        $scope.createProcess = function () {
            emergencyService.createVipProcess($scope.item).then(function (process) {
                if (process) {
                    process.beginInfo = $scope.processInfo;
                    emergencyService.updateVipProcess(process).then(function () {
                        $scope.query();
                    });
                }
            });
        };
        $scope.construct3GTest = function() {
            collegeDialogService.construct3GTest($scope.item.area);
        };
        $scope.construct4GTest = function () {
            collegeDialogService.construct4GTest($scope.item.area);
        };

        $scope.query();
    });
