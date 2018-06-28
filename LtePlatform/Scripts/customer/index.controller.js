angular.module('myApp', ['app.common'])
    .config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Customer/";
            $routeProvider
                .when('/emergency', {
                    templateUrl: viewDir + "EmergencyList.html",
                    controller: "emergency.list"
                })
                .when('/vip', {
                    templateUrl: viewDir + "VipList.html",
                    controller: "vip.list"
                })
                .when('/vip/process/:number', {
                    templateUrl: viewDir + "Process/Vip.html",
                    controller: "vip.process"
                })
                .when('/emergency/process/:id', {
                    templateUrl: viewDir + "Process/Emergency.html",
                    controller: "emergency.process"
                })
                .when('/complain/adjust', {
                    templateUrl: viewDir,
                    controller: "complain.adjust"
                })
                .when('/dailyStat', {
                    templateUrl: viewDir + "Complain/Stat.html",
                    controller: "daily.stat"
                })
                .when('/complain/list', {
                    templateUrl: viewDir + "Complain/List.html",
                    controller: "complain.list"
                })
                .when('/complain/branch', {
                    templateUrl: viewDir + "Complain/Branch.html",
                    controller: "complain.branch"
                })
                .when('/complain/online', {
                    templateUrl: viewDir + "Complain/Online.html",
                    controller: "complain.online"
                })
                .when('/complain/process/:number', {
                    templateUrl: viewDir + "Process/Complain.html",
                    controller: "complain.process"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Customer#";
        $rootScope.menuItems = [
            {
                displayName: "投诉处理",
                isActive: true,
                subItems: [
                    {
                        displayName: "10000号投诉",
                        url: rootUrl + "/complain/list"
                    }, {
                        displayName: "分公司投诉",
                        url: rootUrl + "/complain/branch"
                    }, {
                        displayName: "在线支撑",
                        url: rootUrl + "/complain/online"
                    }
                ]
            }, {
                displayName: "专项支撑",
                isActive: true,
                subItems: [
                    {
                        displayName: "应急需求",
                        url: rootUrl + "/emergency",
                        tooltip: "应急需求（围绕应急通信车）的申请和查询"
                    }, {
                        displayName: "政企支撑",
                        url: rootUrl + "/vip",
                        tooltip: "政企客户支撑需求的管理"
                    }, {
                        displayName: "校园网专题优化",
                        url: "/College/Map",
                        tooltip: "校园网专项优化，包括数据管理、指标分析、支撑工作管理和校园网覆盖呈现"
                    }
                ]
            }, {
                displayName: "统计分析",
                isActive: false,
                subItems: [
                    {
                        displayName: "抱怨量信息校正",
                        url: rootUrl + "/complain/adjust"
                    }, {
                        displayName: "统计分析",
                        url: rootUrl + "/dailyStat"
                    }
                ]
            }
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            messages: []
        };
        $rootScope.closeAlert = function(index) {
            $rootScope.page.messages.splice(index, 1);
        };
        $rootScope.city = {
            selected: "",
            options: []
        };
        $rootScope.district = {
            options: [],
            selected: ""
        };
        $rootScope.town = {
            options: [],
            selected: ""
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
    .controller("customer.index", function($scope, complainService, appKpiService, emergencyService) {
        $scope.overallStats = [
            {
                tips: "抱怨量",
                count: 120,
                type: "shopping-cart",
                color: "blue",
                path: "complain/list"
            }, {
                tips: "分公司需求",
                count: 80,
                type: "comment",
                color: "orange",
                path: "complain/branch"
            }, {
                tips: "在线支撑",
                count: 7,
                type: "user",
                color: "teal",
                path: "complain/online"
            }, {
                tips: "政企支撑",
                count: 44,
                type: "stats",
                color: "red",
                path: "vip"
            }
        ];
        $scope.statDate = {
            value: new Date(),
            opened: false
        };
        $scope.monthObject = 198;
        $scope.query = function() {
            complainService.queryCurrentComplains($scope.statDate.value).then(function(count) {
                $scope.overallStats[0].count = count;
                var objects = [];
                complainService.queryMonthTrend($scope.statDate.value).then(function(stat) {
                    angular.forEach(stat.item1, function(date, index) {
                        objects.push((index + 1) / stat.item1.length * $scope.monthObject);
                    });
                    var options = appKpiService.generateComplainTrendOptions(stat.item1, stat.item2, objects);
                    $('#line-chart').highcharts(options);
                });
            });
            complainService.queryBranchDemands($scope.statDate.value).then(function(count) {
                $scope.overallStats[1].count = count;
            });
            complainService.queryOnlineSustains($scope.statDate.value).then(function(count) {
                $scope.overallStats[2].count = count;
            });
            emergencyService.queryVipDemands($scope.statDate.value).then(function(count) {
                $scope.overallStats[3].count = count;
            });
        };

        $scope.notices = [
            {
                person: '卢立双',
                dateString: "2016/7/24",
                contents: "乐从劳村站点停闭公告，应答口径：投诉点一带 2/4G 覆盖劳村的基站，因为周边村民误认为幅射影响，不停到劳村村委会和乐从政府投诉，而且威胁业主拆除。导致站点被迫需要闭站几天进行信号监测，由于村民要求拆站的情绪强烈，难以协商。建议客户向劳村村委和乐从政府反映信号需求，力求获得支持开启，以早日改善信号覆盖。顺德政务服务热线： 0757-22838180"
            }, {
                person: '卢立双',
                dateString: "2016/7/19",
                contents: "佛山无线网络紧急异动通告：（南海区罗村荣星工业区）从2016年7月19日9：00开始，南海区罗村务庄、罗村务庄荣星工业区、大丰田工业区、沿江南路、罗村朗沙、朗沙村、朗沙工业区，罗沙一带、幸福村、罗务路、新光源产业基地附近等一带出现外部干扰，预计2016年8月19日24：00前可以解决。请注意支撑拦截"
            }
        ];

        $scope.query();
    })
    .controller("complain.list", function($scope, complainService, customerQueryService) {
        $scope.page.title = "10000号投诉";
        $scope.query = function() {
            complainService.queryAll($scope.beginDate.value, $scope.endDate.value).then(function(items) {
                $scope.items = items;
            });
        };

        customerQueryService.queryNetworkTypeOptions().then(function(options) {
            $scope.type = {
                options: options,
                selected: options[0]
            };
        });
        $scope.query();
    })
    .controller("complain.process", function($scope, $routeParams, customerQueryService, complainService) {

        $scope.query = function() {
            customerQueryService.queryOneComplain($routeParams.number).then(function(item) {
                $scope.item = item;
                if ($scope.item.nextStateDescription) {
                    $scope.processInfo = "已完成" + $scope.item.nextStateDescription;
                } else {
                    $scope.processInfo = "";
                }
            });
            complainService.queryComplainProcessList($routeParams.number).then(function(items) {
                $scope.processItems = items;
            });
        };

        $scope.createProcess = function() {
            complainService.createComplainProcess($scope.item).then(function(process) {
                if (process) {
                    process.beginInfo = $scope.processInfo;
                    complainService.updateComplainProcess(process).then(function() {
                        $scope.query();
                    });
                }
            });
        };
        $scope.save = function() {
            complainService.updateComplain($scope.item).then(function() {
                $scope.page.messages.push({
                    type: 'success',
                    contents: '完成10000号投诉工单：' + $scope.item.serialNumber
                });
            });
        }

        $scope.query();
    })
    .controller("complain.branch", function($scope, complainService, customerQueryService) {
        $scope.page.title = "分公司投诉";
        $scope.query = function() {
            complainService.queryBranchList($scope.beginDate.value, $scope.endDate.value).then(function(items) {
                $scope.items = items;
            });
        };

        customerQueryService.queryNetworkTypeOptions().then(function(options) {
            $scope.type = {
                options: options,
                selected: options[0]
            };
        });
        $scope.query();
    })
    .controller("complain.online", function($scope, complainService, customerQueryService) {
        $scope.page.title = "在线支撑";
        $scope.query = function() {
            complainService.queryOnlineList($scope.beginDate.value, $scope.endDate.value).then(function(items) {
                $scope.items = items;
            });
        };

        customerQueryService.queryNetworkTypeOptions().then(function(options) {
            $scope.type = {
                options: options,
                selected: options[0]
            };
        });
        $scope.query();
    })
    .controller("emergency.list", function($scope, customerDialogService, customerQueryService) {
        $scope.page.title = "应急需求";
        $scope.construct = function() {
            customerDialogService.constructEmergencyCommunication(
                $scope.city, $scope.district, $scope.type, $scope.page.messages, $scope.query);
        };
        $scope.query = function() {
            customerQueryService.queryAll($scope.beginDate.value, $scope.endDate.value).then(function(items) {
                $scope.items = items;
            });
        };

        customerQueryService.queryVehicleTypeOptions().then(function(options) {
            $scope.type = {
                options: options,
                selected: options[0]
            };
        });
        $scope.query();
    })
    .controller("emergency.process", function($scope, $routeParams,
        customerQueryService,
        emergencyService,
        customerDialogService) {

        $scope.canGoNextStep = false;
        $scope.fiberList = [];

        $scope.query = function() {
            customerQueryService.queryOneEmergency($routeParams.id).then(function(item) {
                $scope.item = item;
                if ($scope.item.nextStateDescription) {
                    $scope.processInfo = "已完成" + $scope.item.nextStateDescription;
                } else {
                    $scope.processInfo = "";
                }
            });
            emergencyService.queryProcessList($routeParams.id).then(function(list) {
                $scope.processItems = list;
            });
        };

        $scope.createProcess = function() {
            emergencyService.createProcess($scope.item).then(function(process) {
                if (process) {
                    process.processInfo = $scope.processInfo;
                    emergencyService.updateProcess(process).then(function() {
                        $scope.query();
                    });
                }
            });
        };
        $scope.queryFiberItems = function() {
            emergencyService.queryFiberList($routeParams.id).then(function(list) {
                $scope.fiberList = list;
            });
        };

        $scope.createFiber = function() {
            customerDialogService.constructFiberItem($routeParams.id, $scope.fiberList.length, function(item) {
                $scope.canGoNextStep = true;
                $scope.fiberList.push(item);
            }, $scope.page.messages);
        };

        $scope.$watch('item.nextStateDescription', function(state) {
            $scope.canGoNextStep = state && (state !== '光纤起单' || $scope.fiberList.length > 0);
        });

        $scope.query();
        $scope.queryFiberItems();
    })
    .controller("vip.list", function ($scope, customerDialogService, customerQueryService) {
        $scope.page.title = "政企支撑";
        $scope.construct = function () {
            customerDialogService.constructEmergencyCommunication(
                $scope.city, $scope.district, $scope.type, $scope.page.messages, $scope.query);
        };
        $scope.query = function () {
            customerQueryService.queryAllVip($scope.beginDate.value, $scope.endDate.value).then(function (items) {
                $scope.items = items;
            });
        };

        customerQueryService.queryNetworkTypeOptions().then(function (options) {
            $scope.type = {
                options: options,
                selected: options[2]
            };
        });
        $scope.query();
    })
    .controller("vip.process", function ($scope, $routeParams, customerQueryService, emergencyService) {

        $scope.query = function () {
            customerQueryService.queryOneVip($routeParams.number).then(function (item) {
                $scope.item = item;
                if ($scope.item.nextStateDescription) {
                    $scope.processInfo = "已完成" + $scope.item.nextStateDescription;
                } else {
                    $scope.processInfo = "";
                }
                customerQueryService.queryMarketThemeOptions().then(function (options) {
                    $scope.marketTheme = {
                        options: options,
                        selected: item.marketThemeDescription
                    };
                });
            });
            emergencyService.queryVipProcessList($routeParams.number).then(function (items) {
                $scope.processItems = items;
            });
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
        $scope.save = function () {
            $scope.item.marketThemeDescription = $scope.marketTheme.selected;
            customerQueryService.updateVip($scope.item).then(function () {
                $scope.page.messages.push({
                    type: 'success',
                    contents: '完成政企需求工单：' + $scope.item.serialNumber
                });
            });
        }

        $scope.query();
    })
    .controller("daily.stat", function ($scope, complainService, appKpiService) {
        $scope.page.title = "客服指标统计分析";
        complainService.queryComplainMonthStats(new Date()).then(function (stat) {
            var options = appKpiService.generateColumnOptions(stat, '抱怨量月度统计', '月份', '抱怨量');
            $("#line-chart").highcharts(options);
        });
    });
