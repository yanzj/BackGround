angular.module('home.root', ['app.common'])
    .run(function ($rootScope, appUrlService, stationFactory, appRegionService, geometryService, kpiDisplayService) {
        $rootScope.sideBarShown = true;
        $rootScope.rootPath = "/#/";

        $rootScope.page = {
            title: "基础数据总览",
            myPromise: null
        };
        $rootScope.grades = stationFactory.stationGradeOptions;
        $rootScope.roomAttributions = stationFactory.stationRoomOptions;
        $rootScope.towerAttributions = stationFactory.stationTowerOptions;
        $rootScope.isBBUs = stationFactory.stationBbuOptions;
        $rootScope.netTypes = stationFactory.stationNetworkOptions;
        $rootScope.isPowers = stationFactory.stationPowerOptions;
        $rootScope.isNews = stationFactory.stationConstructionOptions;
        $rootScope.indoortypes = stationFactory.stationIndoorOptions;

        appUrlService.initializeAuthorization();
        $rootScope.legend = {
            criteria: [],
            intervals: [],
            sign: true,
            title: ''
        };
        $rootScope.initializeLegend = function() {
            $rootScope.legend.title = $rootScope.city.selected;
            $rootScope.legend.intervals = [];
            $rootScope.legend.criteria = [];
        };
        $rootScope.initializeRsrpLegend = function() {
            var legend = kpiDisplayService.queryCoverageLegend('RSRP');
            $rootScope.legend.title = 'RSRP';
            $rootScope.legend.criteria = legend.criteria;
            $rootScope.legend.intervals = [];
            $rootScope.legend.sign = legend.sign;
        };
        $rootScope.initializeFaultLegend = function(colors) {
            $rootScope.legend.title = "故障状态";
            $rootScope.legend.intervals = [
                {
                    threshold: '未恢复',
                    color: colors[0]
                }, {
                    threshold: '已恢复',
                    color: colors[1]
                }
            ];
        };
        $rootScope.initializeSolveLegend = function(colors) {
            $rootScope.legend.title = "问题状态";
            $rootScope.legend.intervals = [
                {
                    threshold: '未解决',
                    color: colors[0]
                }, {
                    threshold: '已解决',
                    color: colors[1]
                }
            ];
        };
        var longAgo = new Date();
        longAgo.setDate(longAgo.getDate() - 180);
        $rootScope.longBeginDate = {
            value: new Date(longAgo.getFullYear(), longAgo.getMonth(), longAgo.getDate(), 8),
            opened: false
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
        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };
        var yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        $rootScope.statDate = {
            value: yesterday,
            opened: false
        };

        $rootScope.status = {
            isopen: false
        };
        $rootScope.city = {
            selected: "",
            options: []
        };
        appRegionService.initializeCities()
            .then(function(result) {
                $rootScope.city.options = result;
                $rootScope.city.selected = result[0];
            });

        $rootScope.indexedDB = {
            name: 'ouyh18',
            version: 7,
            db: null
        };
        $rootScope.colors = geometryService.queryMapColors();

        $rootScope.areaNames = ['全市'];
        $rootScope.distincts = ['佛山市'];
    });
angular.module('home.route', ['app.common'])
    .config(function($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        var viewDir = "/appViews/Home/";
        $stateProvider
            .state('list',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Network.html",
                            controller: "home.network"
                        }
                    },
                    url: "/cells"
                })
            .state('query',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Query.html",
                            controller: "home.query"
                        }
                    },
                    url: "/query"
                })
            .state('building',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Query.html",
                            controller: "evaluation.home"
                        }
                    },
                    url: "/building"
                })
            .state('topic',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: "/appViews/Parameters/Topic.html",
                            controller: "query.topic"
                        }
                    },
                    url: "/topic"
                })
            .state('common-station',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/Home/StationSearchMenu.html",
                            controller: "menu.common-station"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Common.html",
                            controller: "common-station.network"
                        }
                    },
                    url: "/common-station"
                })
            .state('common-indoor',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/Home/StationSearchMenu.html",
                            controller: "menu.common-indoor"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Common.html",
                            controller: "common-indoor.network"
                        }
                    },
                    url: "/common-indoor"
                })
            .state('flow',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Flow.html",
                            controller: "home.flow"
                        }
                    },
                    url: "/flow"
                })
            .state('dt',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.dt"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Dt.html",
                            controller: "home.dt"
                        }
                    },
                    url: "/dt"
                })
            .state('plan',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.plan"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Plan.html",
                            controller: "home.plan"
                        }
                    },
                    url: "/plan"
                })
            .state('cdma',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.cdma"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Cdma.html",
                            controller: "home.cdma"
                        }
                    },
                    url: "/cdma"
                })
            .state('interference',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.cdma"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Plan.html",
                            controller: "home.interference"
                        }
                    },
                    url: "/interference"
                })
            .state('complain',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.complain"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Complain.html",
                            controller: "home.complain"
                        }
                    },
                    url: "/complain"
                })
            .state('micro',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.complain"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Micro.html",
                            controller: "complain.micro"
                        }
                    },
                    url: "/micro"
                })
            .state('college',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.college"
                        },
                        "contents": {
                            templateUrl: "/appViews/College/Root.html",
                            controller: "home.college"
                        }
                    },
                    url: "/college"
                })
            .state('college-coverage',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.college"
                        },
                        "contents": {
                            templateUrl: "/appViews/College/Coverage/Index.html",
                            controller: "college.coverage"
                        }
                    },
                    url: "/college-coverage"
                })
            .state('mr',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Mr.html",
                            controller: "home.mr"
                        }
                    },
                    url: "/mr"
                })
            .state('grid',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: viewDir + "MrGrid.html",
                            controller: "mr.grid"
                        }
                    },
                    url: "/grid"
                })
            .state('app',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: "/appViews/BasicKpi/GridApp.html",
                            controller: "mr.app"
                        }
                    },
                    url: "/app"
                })
            .state('analysis',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: viewDir + "Analysis.html",
                            controller: "network.analysis"
                        }
                    },
                    url: "/analysis"
                })
            .state('highway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Highway.html',
                            controller: "analysis.highway"
                        }
                    },
                    url: "/highway"
                })
            .state('railway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Highway.html',
                            controller: "analysis.railway"
                        }
                    },
                    url: "/railway"
                })
            .state('subway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Subway.html',
                            controller: "analysis.subway"
                        }
                    },
                    url: "/subway"
                })
            .state('highvalue',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/HighValue.html',
                            controller: "analysis.highvalue"
                        }
                    },
                    url: "/highvalue"
                });
        $urlRouterProvider.otherwise('/cells');
    });
angular.module('home.menu', ['app.common'])
    .controller("menu.root",
        function($scope) {
            $scope.menuItem = {
                displayName: "站点信息",
                subItems: [
                    {
                        displayName: "数据总览",
                        url: "/#/cells"
                    }, {
                        displayName: "室内分布",
                        url: "/#/analysis"
                    }, {
                        displayName: "数据查询",
                        url: "/#/query"
                    }, {
                        displayName: "流量经营",
                        url: '/#/flow'
                    }
                ]
            };
        })
    .controller("menu.alarm",
        function($scope, downSwitchService, baiduMapService, mapDialogService, baiduQueryService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getAlarmStationByName($scope.stationName, 1, 10).then(function(response) {
                    $scope.stations = response.result.rows;
                });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showAlarmStationInfo($scope.stations[index - 1]);
            }
            $scope.$watch('stations',
                function() {
                    baiduMapService.clearOverlays();
                    if (!$scope.stations.length)
                        return;
                    document.getElementById("cardlist").style.display = "inline";
                    baiduQueryService.transformToBaidu($scope.stations[0].longtitute, $scope.stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - $scope.stations[0].longtitute;
                            var yOffset = coors.y - $scope.stations[0].lattitute;
                            baiduMapService.drawPointsUsual($scope.stations,
                                -xOffset,
                                -yOffset,
                                function() {
                                    mapDialogService.showAlarmStationInfo(this.data, $scope.beginDate, $scope.endDate);
                                });
                        });
                });
        })
    .controller('menu.resource',
        function($scope) {
            $scope.menuItem = {
                displayName: "资源资产",
                subItems: [
                    {
                        displayName: "资源资产-基站",
                        url: '/#/resource'
                    }, {
                        displayName: "资源资产-室分",
                        url: '/#/resource-indoor'
                    }
                ]
            };
        })
    .controller('menu.plan',
        function($scope, appUrlService) {
            $scope.menuItem = {
                displayName: "规划支撑",
                subItems: [
                    {
                        displayName: "规划辅助",
                        url: appUrlService.getPlanUrlHost() + 'guihua'
                    }, {
                        displayName: "智能规划",
                        url: appUrlService.getPlanUrlHost() + 'guihua'
                    }
                ]
            };
        })
    .controller('menu.cdma', function($scope) {
        $scope.menuItem = {
            displayName: "2G优化",
            subItems: [
                {
                    displayName: "指标优化",
                    url: '/#/cdma'
                }, {
                    displayName: "干扰管理",
                    url: '/#/interference'
                }
            ]
        };
    })
    .controller('menu.mr',
        function($scope) {
            var rootUrl = "/#";
            $scope.menuItem = {
                displayName: "MR分析",
                subItems: [
                    {
                        displayName: "模拟路测",
                        url: rootUrl + "/mr"
                    }, {
                        displayName: "栅格分析",
                        url: rootUrl + "/grid"
                    }, {
                        displayName: "智能规划",
                        url: rootUrl + "/app"
                    }, {
                        displayName: "工单管控",
                        url: "/Kpi/WorkItem",
                        tooltip: "对接本部优化部4G网优平台，结合日常优化，实现对日常工单的监控和分析"
                    }
                ]
            }
        })
    .controller('menu.dt',
        function($scope, appUrlService) {
            var rootUrl = "/#";
            $scope.menuItem = {
                displayName: "路测管理",
                subItems: [
                    {
                        displayName: "路测数据总览",
                        url: rootUrl + "/dt"
                    }, {
                        displayName: "路测分析",
                        url: appUrlService.getDtUrlHost()
                    }, {
                        displayName: "CQT管理",
                        url: appUrlService.getPlanUrlHost() + 'CQT'
                    }
                ]
            };
        });
angular.module('home.network', ['app.common'])
    .controller("home.network",
        function($scope,
            appRegionService,
            networkElementService,
            baiduMapService,
            coverageDialogService,
            dumpPreciseService,
            collegeMapService) {
            baiduMapService.initializeMap("map", 11);
            $scope.currentView = "LTE基站";

            $scope.updateDistrictLegend = function(district, color) {
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                $scope.legend.intervals.push({
                    threshold: district,
                    color: color
                });
            };

            $scope.showDistrictOutdoor = function (district, color) {
                $scope.updateDistrictLegend(district, color);
                var city = $scope.city.selected;
                networkElementService.queryOutdoorCellSites(city, district).then(function(sites) {
                    collegeMapService.showOutdoorCellSites(sites, color);
                });
            };

            $scope.showOutdoorSites = function() {
                $scope.currentView = "室外小区-1.8G/2.1G";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.initializeLegend();
                angular.forEach($scope.districts,
                    function(district, $index) {
                        $scope.showDistrictOutdoor(district, $scope.colors[$index]);
                    });
            };

            $scope.showDistrictVolte = function (district, color) {
                $scope.updateDistrictLegend(district, color);
                var city = $scope.city.selected;
                networkElementService.queryVolteCellSites(city, district).then(function (sites) {
                    collegeMapService.showOutdoorCellSites(sites, color);
                });
            };

            $scope.showVolteSites = function () {
                $scope.currentView = "室外小区-800M/VoLTE";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.initializeLegend();
                angular.forEach($scope.districts,
                    function (district, $index) {
                        $scope.showDistrictVolte(district, $scope.colors[$index]);
                    });
            };

            $scope.showDistrictNbIot = function (district, color) {
                $scope.updateDistrictLegend(district, color);
                var city = $scope.city.selected;
                networkElementService.queryNbIotCellSites(city, district).then(function (sites) {
                    collegeMapService.showOutdoorCellSites(sites, color);
                });
            };

            $scope.showNbIotSites = function () {
                $scope.currentView = "室外小区-NB-IoT";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.initializeLegend();
                angular.forEach($scope.districts,
                    function (district, $index) {
                        $scope.showDistrictNbIot(district, $scope.colors[$index]);
                    });
            };

            $scope.showDistrictIndoor = function(district, color) {
                $scope.updateDistrictLegend(district, color);
                var city = $scope.city.selected;

                networkElementService.queryIndoorCellSites(city, district).then(function(sites) {
                    collegeMapService.showIndoorCellSites(sites, color);
                });
            };

            $scope.showIndoorSites = function() {
                $scope.currentView = "室内小区";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.initializeLegend();
                angular.forEach($scope.districts,
                    function(district, $index) {
                        $scope.showDistrictIndoor(district, $scope.colors[$index]);
                    });
            };

            $scope.showDistrictENodebs = function(district, color) {
                var city = $scope.city.selected;
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                $scope.legend.intervals.push({
                    threshold: district,
                    color: color
                });
                $scope.page.myPromise = networkElementService.queryENodebsInOneDistrict(city, district);
                $scope.page.myPromise.then(function(sites) {
                    collegeMapService.showENodebSites(sites, color, $scope.beginDate, $scope.endDate);
                });
            };
            $scope.showLteENodebs = function() {
                $scope.currentView = "LTE基站";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.initializeLegend();
                angular.forEach($scope.districts,
                    function(district, $index) {
                        $scope.showDistrictENodebs(district, $scope.colors[$index]);
                    });
            };

            $scope.showLteTownStats = function() {
                var city = $scope.city.selected;
                coverageDialogService.showTownStats(city);
            };
            $scope.showCdmaTownStats = function() {
                var city = $scope.city.selected;
                coverageDialogService.showCdmaTownStats(city);
            };
            $scope.districts = [];

            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.initializeLegend();
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district, $index) {
                                $scope.showDistrictENodebs(district, $scope.colors[$index]);
                            });
                    }
                });
        })
    .controller("evaluation.home",
        function($scope,
            baiduMapService,
            baiduQueryService,
            parametersMapService,
            mapDialogService) {
            baiduMapService.initializeMap("map", 12);
            baiduQueryService.queryWandonglouyu().then(function(buildings) {
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                parametersMapService.showPhpElements(buildings, mapDialogService.showBuildingInfo);
            });
        })
    .controller('home.flow',
        function($scope,
            baiduMapService,
            baiduQueryService,
            coverageDialogService,
            flowService,
            chartCalculateService) {
            baiduMapService.initializeMap("map", 11);
            $scope.frequencyOption = 'all';

            $scope.showFeelingRate = function () {
                if (!$scope.flowGeoPoints) {
                    alert("计算未完成！请稍后点击。");
                    return;
                }
                $scope.currentView = "下行感知速率";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                $scope.legend.intervals = [];
                var max = 60;
                var gradient = chartCalculateService.updateHeatMapIntervalDefs($scope.legend.intervals, max);
                $scope.legend.title = '速率（Mbit/s）';
                baiduQueryService.transformToBaidu($scope.flowGeoPoints[0].longtitute,
                    $scope.flowGeoPoints[0].lattitute).then(function(coors) {
                    var xOffset = coors.x - $scope.flowGeoPoints[0].longtitute;
                    var yOffset = coors.y - $scope.flowGeoPoints[0].lattitute;
                    var points = _.map($scope.flowGeoPoints,
                        function(stat) {
                            return {
                                "lng": stat.longtitute + xOffset,
                                "lat": stat.lattitute + yOffset,
                                "count": stat.downlinkFeelingRate
                            };
                        });

                    var heatmapOverlay = new BMapLib.HeatmapOverlay({
                        "radius": 10,
                        "opacity": 50,
                        "gradient": gradient
                    });
                    baiduMapService.addOverlays([heatmapOverlay]);
                    heatmapOverlay.setDataSet({ data: points, max: max });
                    heatmapOverlay.show();
                });
            };

            $scope.showUplinkFeelingRate = function() {
                if (!$scope.flowGeoPoints) {
                    alert("计算未完成！请稍后点击。");
                    return;
                }
                $scope.currentView = "上行感知速率";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                $scope.legend.intervals = [];
                var max = 10;
                var gradient = chartCalculateService.updateHeatMapIntervalDefs($scope.legend.intervals, max);
                baiduQueryService.transformToBaidu($scope.flowGeoPoints[0].longtitute,
                    $scope.flowGeoPoints[0].lattitute).then(function(coors) {
                    var xOffset = coors.x - $scope.flowGeoPoints[0].longtitute;
                    var yOffset = coors.y - $scope.flowGeoPoints[0].lattitute;
                    var points = _.map($scope.flowGeoPoints,
                        function(stat) {
                            return {
                                "lng": stat.longtitute + xOffset,
                                "lat": stat.lattitute + yOffset,
                                "count": stat.uplinkFeelingRate
                            };
                        });
                    var heatmapOverlay = new BMapLib.HeatmapOverlay({
                        "radius": 10,
                        "opacity": 50,
                        "gradient": gradient
                    });
                    baiduMapService.addOverlays([heatmapOverlay]);
                    heatmapOverlay.setDataSet({ data: points, max: max });
                    heatmapOverlay.show();
                });

            };

            $scope.showHotMap = function() {
                $scope.calculating = true;
                flowService.queryENodebGeoFlowByDateSpan($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.frequencyOption)
                    .then(function(result) {
                        $scope.flowGeoPoints = result;
                        $scope.showFeelingRate();
                        $scope.calculating = false;
                    });
            };

            $scope.displayAllFrequency = function() {
                if ($scope.frequencyOption !== 'all') {
                    $scope.frequencyOption = 'all';
                    $scope.showHotMap();
                }
            };
            $scope.display1800Frequency = function() {
                if ($scope.frequencyOption !== '1800') {
                    $scope.frequencyOption = '1800';
                    $scope.showHotMap();
                }
            };
            $scope.display2100Frequency = function () {
                if ($scope.frequencyOption !== '2100') {
                    $scope.frequencyOption = '2100';
                    $scope.showHotMap();
                }
            };
            $scope.display800Frequency = function () {
                if ($scope.frequencyOption !== '800') {
                    $scope.frequencyOption = '800';
                    $scope.showHotMap();
                }
            };
            $scope.showFlow = function() {
                coverageDialogService.showFlowStats($scope.statDate.value || new Date(), $scope.frequencyOption);
            };
            $scope.showFlowTrend = function() {
                coverageDialogService.showFlowTrend($scope.city.selected, $scope.beginDate, $scope.endDate, $scope.frequencyOption);
            };
            $scope.showUsersTrend = function() {
                coverageDialogService.showUsersTrend($scope.city.selected, $scope.beginDate, $scope.endDate, $scope.frequencyOption);
            };
            $scope.showFeelingRateTrend = function() {
                coverageDialogService.showFeelingRateTrend($scope.city.selected, $scope.beginDate, $scope.endDate, $scope.frequencyOption);
            };
            $scope.showDownSwitchTrend = function() {
                coverageDialogService.showDownSwitchTrend($scope.city.selected, $scope.beginDate, $scope.endDate, $scope.frequencyOption);
            };
            $scope.showRank2RateTrend = function() {
                coverageDialogService.showRank2RateTrend($scope.city.selected, $scope.beginDate, $scope.endDate, $scope.frequencyOption);
            };
            $scope.showHotMap();
        })
    .controller("home.dt",
        function($scope,
            baiduMapService,
            coverageService,
            appFormatService,
            parametersDialogService,
            parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.legend.intervals = [];
            coverageService.queryAreaTestDate().then(function(result) {
                angular.forEach(result,
                    function(item, $index) {
                        if (item.cityName) {
                            baiduMapService.drawCustomizeLabel(item.longtitute,
                                item.lattitute + 0.005,
                                item.districtName + item.townName,
                                '4G测试日期:' +
                                appFormatService
                                .getDateString(appFormatService.getDate(item.latestDate4G), 'yyyy-MM-dd') +
                                '<br/>3G测试日期:' +
                                appFormatService
                                .getDateString(appFormatService.getDate(item.latestDate3G), 'yyyy-MM-dd') +
                                '<br/>2G测试日期:' +
                                appFormatService
                                .getDateString(appFormatService.getDate(item.latestDate2G), 'yyyy-MM-dd'),
                                3);
                            var marker = baiduMapService.generateIconMarker(item.longtitute,
                                item.lattitute,
                                "/Content/Images/Hotmap/site_or.png");
                            baiduMapService.addOneMarkerToScope(marker, parametersDialogService.showTownDtInfo, item);
                            parametersMapService
                                .showTownBoundaries(item.cityName,
                                    item.districtName,
                                    item.townName,
                                    $scope.colors[$index % $scope.colors.length]);
                        }
                    });
            });
            $scope.manageCsvFiles = function() {
                parametersDialogService.manageCsvDtInfos($scope.longBeginDate, $scope.endDate);
            };
        })
    .controller("network.analysis",
        function($scope,
            baiduMapService,
            networkElementService,
            dumpPreciseService,
            baiduQueryService,
            neGeometryService,
            parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.currentView = "信源类别";
            $scope.districts = [];
            $scope.distributionFilters = [
                function(site) {
                    return site.isLteRru && site.isCdmaRru;
                }, function(site) {
                    return site.isLteRru && !site.isCdmaRru;
                }, function(site) {
                    return !site.isLteRru && site.isCdmaRru;
                }, function(site) {
                    return !site.isLteRru && !site.isCdmaRru;
                }
            ];
            $scope.scaleFilters = [
                function(site) {
                    return site.scaleDescription === '超大型';
                }, function(site) {
                    return site.scaleDescription === '大型';
                }, function(site) {
                    return site.scaleDescription === '中型';
                }, function(site) {
                    return site.scaleDescription === '小型';
                }
            ];
            $scope.showDistrictDistributions = function(district) {
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区');
                networkElementService.queryDistributionsInOneDistrict(district).then(function(sites) {
                    angular.forEach(sites,
                        function(site) {
                            $scope.indoorDistributions.push(site);
                        });
                    parametersMapService
                        .displaySourceDistributions($scope.indoorDistributions,
                            $scope.distributionFilters,
                            $scope.colors);
                });
            };

            $scope.showSourceDistributions = function() {
                $scope.currentView = "信源类别";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.updateSourceLegendDefs();

                angular.forEach($scope.districts,
                    function(district) {
                        baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区');
                    });
                parametersMapService
                    .displaySourceDistributions($scope.indoorDistributions, $scope.distributionFilters, $scope.colors);
            };
            $scope.showScaleDistributions = function() {
                $scope.currentView = "规模";
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary($scope.city.selected);
                $scope.updateScaleLegendDefs();

                angular.forEach($scope.districts,
                    function(district) {
                        baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区');
                    });
                parametersMapService
                    .displaySourceDistributions($scope.indoorDistributions, $scope.scaleFilters, $scope.colors);
            };

            $scope.updateSourceLegendDefs = function() {
                $scope.legend.title = "信源类别";
                $scope.legend.intervals = [];
                var sourceDefs = ['LC信源', '纯L信源', '纯C信源', '非RRU信源'];
                angular.forEach(sourceDefs,
                    function(def, $index) {
                        $scope.legend.intervals.push({
                            threshold: def,
                            color: $scope.colors[$index]
                        });
                    });
            };
            $scope.updateScaleLegendDefs = function() {
                $scope.legend.title = "规模";
                $scope.legend.intervals = [];
                var sourceDefs = ['超大型', '大型', '中型', '小型'];
                angular.forEach(sourceDefs,
                    function(def, $index) {
                        $scope.legend.intervals.push({
                            threshold: def,
                            color: $scope.colors[$index]
                        });
                    });
            };

            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.legend.title = '信源类别';
                        $scope.updateSourceLegendDefs();
                        $scope.indoorDistributions = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district) {
                                $scope.showDistrictDistributions(district);
                            });
                    }
                });
        })
    .controller("home.query",
        function($scope,
            baiduMapService,
            neighborDialogService,
            dumpPreciseService,
            appRegionService,
            mapDialogService,
            parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.currentView = "镇区站点";
            $scope.queryConditions = function() {
                baiduMapService.clearOverlays();
                neighborDialogService.setQueryConditions($scope.city, $scope.beginDate, $scope.endDate);
            };
            $scope.queryByTowns = function() {
                neighborDialogService.queryList($scope.city);
            };
            $scope.queryType = function() {
                neighborDialogService.queryCellTypeChart($scope.city);
            };
            $scope.showDistrictTownStat = function(district, color) {
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                appRegionService.queryTownInfrastructures($scope.city.selected, district).then(function(result) {
                    angular.forEach(result,
                        function(stat, $index) {
                            appRegionService.queryTown($scope.city.selected, district, stat.town).then(function(town) {
                                angular.extend(stat, town);
                                baiduMapService.drawCustomizeLabel(stat.longtitute,
                                    stat.lattitute + 0.005,
                                    stat.districtName + stat.townName,
                                    'LTE基站个数:' +
                                    stat.totalLteENodebs +
                                    '<br/>LTE小区个数:' +
                                    stat.totalLteCells +
                                    '<br/>NB-IoT小区个数:' +
                                    stat.totalNbIotCells +
                                    '<br/>CDMA基站个数:' +
                                    stat.totalCdmaBts +
                                    '<br/>CDMA小区个数:' +
                                    stat.totalCdmaCells,
                                    5);
                                var marker = baiduMapService.generateIconMarker(stat.longtitute,
                                    stat.lattitute,
                                    "/Content/Images/Hotmap/site_or.png");
                                baiduMapService.addOneMarkerToScope(marker,
                                    function(item) {
                                        mapDialogService.showTownENodebInfo(item, $scope.city.selected, district);
                                        parametersMapService
                                            .showTownBoundaries(item.cityName,
                                                item.districtName,
                                                item.townName,
                                                $scope.colors[$index % $scope.colors.length]);
                                    },
                                    stat);
                            });
                        });
                });
            };
            $scope.showTownSites = function() {
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                $scope.currentView = "镇区站点";
                angular.forEach($scope.districts,
                    function(district, $index) {
                        $scope.showDistrictTownStat(district, $scope.colors[$index]);
                    });
            };
            $scope.showAreaDivision = function() {
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                $scope.currentView = "区域划分";
                parametersMapService.showAreaBoundaries();
            };
            $scope.districts = [];
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.legend.intervals = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district, $index) {
                                $scope.showDistrictTownStat(district, $scope.colors[$index]);
                            });

                    }
                });
        });
angular.module('home.complain', ['app.common'])
    .controller('menu.complain',
        function($scope, appUrlService) {
            $scope.menuItem = {
                displayName: "投诉管理",
                subItems: [
                    {
                        displayName: "统计分析",
                        url: '/#/complain'
                    }, {
                        displayName: "手机伴侣",
                        url: '/#/micro'
                    }, {
                        displayName: "在线支撑",
                        url: appUrlService.getCustomerHost() + 'IndexOfComplaints.aspx'
                    }
                ]
            };
        })
    .controller("home.complain",
        function($scope,
            baiduMapService,
            dumpPreciseService,
            complainService,
            mapDialogService,
            collegeMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");

            $scope.showDistrictOss = function(district, color) {
                var city = $scope.city.selected;
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                $scope.legend.intervals.push({
                    threshold: district,
                    color: color
                });
                complainService.queryLastMonthOnlineListInOneDistrict($scope.endDate.value, city, district)
                    .then(function(sites) {
                        if (sites.length) {
                            collegeMapService.showComplainItems(sites, color);
                        }
                    });
            };
            $scope.showOssWorkItem = function() {
                $scope.legend.title = $scope.city.selected;
                $scope.initializeLegend();
                baiduMapService.clearOverlays();
                $scope.currentView = "电子运维工单";
                angular.forEach($scope.districts.concat(['其他']),
                    function(district, $index) {
                        $scope.showDistrictOss(district, $scope.colors[$index]);
                    });
            };
            $scope.showDistrictBackworks = function (district, color) {
                var city = $scope.city.selected;
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                $scope.legend.intervals.push({
                    threshold: district,
                    color: color
                });
                complainService.queryLastMonthComplainListInOneDistrict($scope.endDate.value, city, district)
                    .then(function (sites) {
                        if (sites.length) {
                            collegeMapService.showComplainItems(sites, color);
                        }
                    });
            };
            $scope.showBackWorkItem = function() {
                $scope.legend.title = $scope.city.selected;
                $scope.initializeLegend();
                baiduMapService.clearOverlays();
                $scope.currentView = "后端工单";
                angular.forEach($scope.districts.concat(['其他']),
                    function (district, $index) {
                        $scope.showDistrictBackworks(district, $scope.colors[$index]);
                    });
            };

            $scope.showYesterdayItems = function() {
                mapDialogService.showYesterdayComplainItems($scope.city.selected);
            };
            $scope.showMonthlyTrend = function() {
                mapDialogService.showMonthComplainItems();
            };
            $scope.showRecentTrend = function() {
                mapDialogService.showRecentComplainItems($scope.city.selected, $scope.beginDate, $scope.endDate);
            };
            $scope.positionModify = function() {
                mapDialogService.adjustComplainItems();
            };

            $scope.districts = [];
            $scope.currentView = "电子运维工单";
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.legend.title = city;
                        $scope.initializeLegend();
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district, $index) {
                                $scope.showDistrictOss(district, $scope.colors[$index]);
                            });
                    }
                });

        })
    .controller("complain.micro",
        function($scope, baiduMapService, alarmsService, dumpPreciseService, mapDialogService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");

            alarmsService.queryMicroItems().then(function(items) {
                angular.forEach(items,
                    function(item) {
                        var marker = baiduMapService
                            .generateIconMarker(item.longtitute, item.lattitute, "/Content/themes/baidu/address.png");
                        baiduMapService.addOneMarkerToScope(marker,
                            function(stat) {
                                mapDialogService.showMicroAmpliferInfos(stat);
                            },
                            item);
                    });
            });

            $scope.districts = [];

            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.legend.title = city;
                        $scope.initializeLegend();
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts.concat(['其他']),
                            function(district, $index) {
                                baiduMapService.addDistrictBoundary(district, $scope.colors[$index]);
                            });
                    }
                });
        });
angular.module('home.kpi', ['app.common'])
    .controller("query.topic",
        function($scope, baiduMapService, customerDialogService, basicImportService, mapDialogService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.query = function() {
                mapDialogService.showHotSpotsInfo($scope.hotSpotList);
            };

            $scope.updateMap = function() {
                basicImportService.queryAllHotSpots().then(function(result) {
                    $scope.hotSpotList = result;
                    angular.forEach(result,
                        function(item) {
                            baiduMapService.drawCustomizeLabel(item.longtitute,
                                item.lattitute + 0.005,
                                item.hotspotName,
                                '地址:' + item.address + '<br/>类型:' + item.typeDescription + '<br/>说明:' + item.sourceName,
                                3);
                            var marker = baiduMapService.generateIconMarker(item.longtitute,
                                item.lattitute,
                                "/Content/Images/Hotmap/site_or.png");
                            baiduMapService.addOneMarkerToScope(marker,
                                function(stat) {
                                    customerDialogService.modifyHotSpot(stat,
                                        function() {
                                            baiduMapService.switchMainMap();
                                            baiduMapService.clearOverlays();
                                            $scope.updateMap();
                                        },
                                        baiduMapService.switchMainMap);
                                },
                                item);
                        });
                });
            };
            $scope.addHotSpot = function() {
                customerDialogService.constructHotSpot(function() {
                        baiduMapService.switchMainMap();
                        baiduMapService.clearOverlays();
                        $scope.updateMap();
                    },
                    baiduMapService.switchMainMap);
            };
            $scope.updateMap();
        })
    .controller("home.interference",
        function($scope, baiduMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
        })
    .controller("home.cdma",
        function($scope, baiduMapService, mapDialogService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");

            $scope.showBasicKpi = function() {
                mapDialogService.showBasicKpiDialog($scope.city, $scope.beginDate, $scope.endDate);
            };
            $scope.showTopDrop2G = function() {
                mapDialogService.showTopDrop2GDialog($scope.city, $scope.beginDate, $scope.endDate);
            };
        });
/// <reference path="./common.js" />

angular.module('home.college', ['app.common'])
    .controller('menu.college',
        function($scope) {
            $scope.menuItem = {
                displayName: "校园网专题",
                subItems: [
                    {
                        displayName: "小区分布",
                        url: '/#/college'
                    }, {
                        displayName: "校园覆盖",
                        url: '/#/college-coverage'
                    }
                ]
            };
        })
    .controller("college.coverage",
        function($scope,
            baiduMapService,
            collegeQueryService,
            mapDialogService,
            collegeMapService,
            parametersDialogService,
            parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            $scope.coverageOverlays = [];

            $scope.showOverallCoverage = function() {
                mapDialogService.showCollegeCoverageList($scope.beginDate, $scope.endDate);
            };
            $scope.siteOverlays = [];
            $scope.sectorOverlays = [];

            $scope.showCoverageView = function(name) {
                $scope.currentView = name;
                collegeQueryService.queryByName(name).then(function(college) {
                    collegeMapService.drawCollegeArea(college.id, function() {});
                });
                parametersDialogService.showCollegeCoverage(name,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.coverageOverlays,
                    function(legend) {
                        $scope.legend.criteria = legend.criteria;
                        $scope.legend.title = legend.title;
                        $scope.legend.sign = legend.sign;
                        var range = baiduMapService.getRange();
                        parametersMapService.showElementsInRange(range.west,
                            range.east,
                            range.south,
                            range.north,
                            $scope.beginDate,
                            $scope.endDate,
                            $scope.siteOverlays,
                            $scope.sectorOverlays);
                    });
            };

            collegeQueryService.queryAll().then(function(spots) {
                $scope.hotSpots = spots;
                $scope.currentView = spots[0].name;
            });
        });
angular.module('home.mr', ['app.common'])
    .controller("home.mr",
        function($scope,
            baiduMapService,
            coverageService,
            kpiDisplayService,
            parametersMapService,
            appUrlService,
            coverageDialogService,
            dumpPreciseService,
            appRegionService) {
            baiduMapService.initializeMap("map", 13);

            $scope.currentDataLabel = "districtPoints";
            $scope.overlays = {
                coverage: [],
                sites: [],
                cells: []
            };
            $scope.initializeMap = function() {
                $scope.overlays.coverage = [];
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
            };

            $scope.showStats = function() {
                coverageDialogService.showAgpsStats($scope.data, $scope.legend.criteria);
            };
            $scope.showInfrasturcture = function() {
                parametersMapService.clearOverlaySites($scope.overlays.sites);
                parametersMapService.clearOverlaySites($scope.overlays.cells);
                parametersMapService.showElementsInOneTown($scope.city.selected,
                    $scope.district.selected,
                    $scope.town.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.overlays.sites,
                    $scope.overlays.cells);
            };

            $scope.showTelecomCoverage = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var index = parseInt($scope.data.length / 2);
                baiduMapService.setCellFocus($scope.data[index].longtitute, $scope.data[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateTelecomRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.displayTelecomAgps = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var index = parseInt($scope.telecomAgps.length / 2);
                baiduMapService.setCellFocus($scope.telecomAgps[index].longtitute,
                    $scope.telecomAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.telecomAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showUnicomCoverage = function() {
                $scope.currentView = "联通";
                $scope.initializeMap();
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateUnicomRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals);
            };
            $scope.displayUnicomAgps = function() {
                $scope.currentView = "联通";
                $scope.initializeMap();
                var index = parseInt($scope.unicomAgps.length / 2);
                baiduMapService.setCellFocus($scope.unicomAgps[index].longtitute,
                    $scope.unicomAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.uniAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showMobileCoverage = function() {
                $scope.currentView = "移动";
                $scope.initializeMap();
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateMobileRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals);
            };
            $scope.displayMobileAgps = function() {
                $scope.currentView = "移动";
                $scope.initializeMap();
                var index = parseInt($scope.mobileAgps.length / 2);
                baiduMapService.setCellFocus($scope.mobileAgps[index].longtitute,
                    $scope.mobileAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.mobileAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.updateIndexedData = function() {
                var items = [];
                angular.forEach($scope.data,
                    function(data) {
                        data['topic'] = data.longtitute + ',' + data.lattitute;
                        items.push(data);
                    });
                appUrlService.refreshIndexedDb($scope.indexedDB.db, $scope.currentDataLabel, 'topic', items);
            };
            $scope.queryAndDisplayTelecom = function() {
                coverageService.queryAgisDtPointsByTopic($scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.district.selected + $scope.town.selected).then(function(result) {
                    $scope.data = result;
                    $scope.showTelecomCoverage();
                });
            };
            $scope.showTelecomWeakCoverage = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var weakData = _.filter($scope.data, function(stat) { return stat.telecomRsrp < 40; });
                if (!weakData) return;
                var index = parseInt(weakData.length / 2);
                baiduMapService.setCellFocus(weakData[index].longtitute, weakData[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateTelecomRsrpPoints($scope.coveragePoints, weakData);
                parametersMapService.showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showSmallRange = function() {
                coverageService.queryAgisDtPointsByTopic($scope.beginDate.value, $scope.endDate.value, '小范围')
                    .then(function(result) {
                        $scope.data = result;
                        $scope.currentDataLabel = 'rangePoints';
                        $scope.updateIndexedData();
                        $scope.showTelecomCoverage();
                    });
            };

            $scope.queryAgps = function() {
                $scope.currentDistrict = $scope.district.selected;
                $scope.currentTown = $scope.town.selected;
                switch ($scope.type.selected) {
                case '电信':
                    coverageService.queryAgpsTelecomByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.telecomAgps = result;
                    });
                    break;
                case '移动':
                    coverageService.queryAgpsMobileByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.mobileAgps = result;
                    });
                    break;
                case '联通':
                    coverageService.queryAgpsUnicomByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.unicomAgps = result;
                    });
                    break;
                }
            };
            $scope.updateTelecomAgps = function() {
                coverageService.updateAgpsTelecomView($scope.currentDistrict, $scope.currentTown, $scope.telecomAgps)
                    .then(function(result) {

                    });
            };
            $scope.updateMobileAgps = function() {
                coverageService.updateAgpsMobileView($scope.currentDistrict, $scope.currentTown, $scope.mobileAgps)
                    .then(function(result) {

                    });
            };
            $scope.updateUnicomAgps = function() {
                coverageService.updateAgpsUnicomView($scope.currentDistrict, $scope.currentTown, $scope.unicomAgps)
                    .then(function(result) {

                    });
            };
            $scope.type = {
                options: ['电信', '移动', '联通'],
                selected: '电信'
            };
            $scope.initializeRsrpLegend();
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        var districts = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            districts,
                            function(district, $index) {
                                if ($index === 0) {
                                    $scope.district = {
                                        options: districts,
                                        selected: districts[0]
                                    };
                                }
                            });
                    }
                });
            $scope.$watch('district.selected',
                function(district) {
                    if (district) {
                        appRegionService.queryTowns($scope.city.selected, district).then(function(towns) {
                            $scope.town = {
                                options: towns,
                                selected: towns[0]
                            };
                        });
                    }
                });
        })
    .controller("mr.grid",
        function($scope,
            baiduMapService,
            coverageService,
            dumpPreciseService,
            kpiDisplayService,
            baiduQueryService,
            coverageDialogService,
            appRegionService,
            parametersMapService,
            collegeMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.currentView = "自身覆盖";
            $scope.setRsrpLegend = function() {
                var legend = kpiDisplayService.queryCoverageLegend('rsrpInterval');
                $scope.legend.title = 'RSRP区间';
                $scope.legend.intervals = legend.criteria;
                $scope.colorDictionary = {};
                $scope.areaStats = {};
                angular.forEach(legend.criteria,
                    function(info) {
                        $scope.colorDictionary[info.threshold] = info.color;
                        $scope.areaStats[info.threshold] = 0;
                    });
            };
            $scope.setCompeteLegend = function() {
                var legend = kpiDisplayService.queryCoverageLegend('competeResult');
                $scope.legend.title = '竞争结果';
                $scope.legend.intervals = legend.criteria;
                $scope.colorDictionary = {};
                $scope.areaStats = {};
                angular.forEach(legend.criteria,
                    function(info) {
                        $scope.colorDictionary[info.threshold] = info.color;
                        $scope.areaStats[info.threshold] = 0;
                    });
            };
            $scope.showGridStats = function() {
                var keys = [];
                angular.forEach($scope.legend.intervals,
                    function(info) {
                        keys.push(info.threshold);
                    });
                coverageDialogService.showGridStats($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.currentView,
                    $scope.legend.title,
                    $scope.areaStats,
                    keys);
            };
            $scope.showDistrictSelfCoverage = function(district, town, color) {
                baiduMapService.clearOverlays();
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                if (town === '全区') {
                    coverageService.queryMrGridSelfCoverage(district, $scope.endDate.value).then(function(result) {
                        var coors = result[0].coordinates.split(';')[0];
                        var longtitute = parseFloat(coors.split(',')[0]);
                        var lattitute = parseFloat(coors.split(',')[1]);
                        collegeMapService.showRsrpMrGrid(result,
                            longtitute,
                            lattitute,
                            $scope.areaStats,
                            $scope.colorDictionary);
                    });
                } else {
                    parametersMapService.showTownBoundaries($scope.city.selected, district, town, color);

                    coverageService.queryTownMrGridSelfCoverage(district, town, $scope.endDate.value)
                        .then(function(result) {
                            appRegionService.queryTown($scope.city.selected, district, town).then(function(stat) {
                                var longtitute = stat.longtitute;
                                var lattitute = stat.lattitute;
                                collegeMapService
                                    .showRsrpMrGrid(result,
                                        longtitute,
                                        lattitute,
                                        $scope.areaStats,
                                        $scope.colorDictionary);
                            });
                        });
                }

            };
            $scope.showDistrictCompeteCoverage = function(district, town, color, competeDescription) {
                baiduMapService.clearOverlays();
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                if (town === '全区') {
                    coverageService.queryMrGridCompete(district, $scope.endDate.value, competeDescription)
                        .then(function(result) {
                            var coors = result[0].coordinates.split(';')[0];
                            var longtitute = parseFloat(coors.split(',')[0]);
                            var lattitute = parseFloat(coors.split(',')[1]);
                            collegeMapService
                                .showRsrpMrGrid(result,
                                    longtitute,
                                    lattitute,
                                    $scope.areaStats,
                                    $scope
                                    .colorDictionary);
                        });
                } else {
                    parametersMapService.showTownBoundaries($scope.city.selected, district, town, color);
                    coverageService.queryTownMrGridCompete(district, town, $scope.endDate.value, competeDescription)
                        .then(function(result) {
                            appRegionService.queryTown($scope.city.selected, district, town).then(function(stat) {
                                var longtitute = stat.longtitute;
                                var lattitute = stat.lattitute;
                                collegeMapService
                                    .showRsrpMrGrid(result,
                                        longtitute,
                                        lattitute,
                                        $scope.areaStats,
                                        $scope.colorDictionary);
                            });
                        });
                }

            };
            $scope.showMrGrid = function(district, town) {
                $scope.currentDistrict = district;
                $scope.currentTown = town;
                if ($scope.currentView === '自身覆盖') {
                    $scope.setRsrpLegend();
                    $scope.showDistrictSelfCoverage(district, town, $scope.colors[0]);
                } else {
                    $scope.setCompeteLegend();
                    $scope.showDistrictCompeteCoverage(district, town, $scope.colors[0], $scope.currentView);
                }

            };
            $scope.showDistrictMrGrid = function(district) {
                $scope.showMrGrid(district.name, district.towns[0]);
            };
            $scope.showTelecomCoverage = function() {
                $scope.currentView = "自身覆盖";
                $scope.setRsrpLegend();
                $scope.showDistrictSelfCoverage($scope.currentDistrict, town, $scope.colors[0]);
            };
            $scope.showMobileCompete = function() {
                $scope.currentView = "移动竞对";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.showUnicomCompete = function() {
                $scope.currentView = "联通竞对";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.showOverallCompete = function() {
                $scope.currentView = "竞对总体";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.districts = [];
            $scope.setRsrpLegend();
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        var districts = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            districts,
                            function(district, $index) {
                                appRegionService.queryTowns($scope.city.selected, district).then(function(towns) {
                                    towns.push('全区');
                                    $scope.districts.push({
                                        name: district,
                                        towns: towns
                                    });
                                    if ($index === 0) {
                                        $scope.showMrGrid(district, towns[0]);
                                    }
                                });
                            });
                    }
                });
        })
    .controller("mr.app",
        function($scope,
            baiduMapService,
            alarmsService,
            coverageDialogService,
            kpiDisplayService,
            parametersMapService,
            alarmImportService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.overlays = {
                planSites: [],
                sites: [],
                cells: []
            };
            $scope.initializeMap = function() {
                $scope.overlays.coverage = [];
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
            };

            $scope.currentCluster = {
                list: [],
                stat: {}
            };
            $scope.calculateCoordinate = function(list) {
                angular.forEach(list,
                    function(item) {
                        var sum = _.reduce(item.gridPoints,
                            function(memo, num) {
                                return {
                                    longtitute: memo.longtitute + num.longtitute,
                                    lattitute: memo.lattitute + num.lattitute
                                };
                            });
                        item.longtitute = sum.longtitute / item.gridPoints.length;
                        item.lattitute = sum.lattitute / item.gridPoints.length;
                        var bestPoint = _.min(item.gridPoints,
                            function(stat) {
                                return (stat.longtitute - item.longtitute) * (stat.longtitute - item.longtitute) +
                                    (stat.lattitute - item.lattitute) * (stat.lattitute - item.lattitute);
                            });
                        item.bestLongtitute = bestPoint.longtitute;
                        item.bestLattitute = bestPoint.lattitute;
                    });
            };
            $scope.showCurrentCluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var gridList = $scope.currentCluster.list;
                if (!gridList.length) return;
                var index = parseInt(gridList.length / 2);
                baiduMapService.setCellFocus(gridList[index].longtitute, gridList[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateRealRsrpPoints($scope.coveragePoints, gridList);
                parametersMapService.showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
                if ($scope.currentCluster.stat) {
                    alarmImportService.updateClusterKpi($scope.currentCluster.stat,
                        function(stat) {
                            parametersMapService.displayClusterPoint(stat, $scope.currentCluster.list);
                        });
                } else {
                    parametersMapService.displayClusterPoint($scope.currentCluster.stat, $scope.currentCluster.list);
                }
            };
            $scope.showSubClusters = function(stats) {
                angular.forEach(stats,
                    function(stat) {
                        var gridList = stat.gridPoints;
                        var index = parseInt(gridList.length / 2);
                        baiduMapService.setCellFocus(gridList[index].longtitute, gridList[index].lattitute, 17);
                        $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                        alarmsService.queryClusterGridKpis(gridList).then(function(list) {
                            stat.gridPoints = list;
                            kpiDisplayService.generateRealRsrpPoints($scope.coveragePoints, list);
                            parametersMapService
                                .showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
                            alarmImportService.updateClusterKpi(stat,
                                function(item) {
                                    parametersMapService.displayClusterPoint(item, list);
                                });
                        });
                    });
            };
            $scope.showRange500Cluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('500', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showRange1000Cluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('1000', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showRangeAdvanceCluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('Advance8000', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showInfrasturcture = function() {
                parametersMapService.clearOverlaySites($scope.overlays.sites);
                parametersMapService.clearOverlaySites($scope.overlays.cells);
                var gridList = $scope.currentCluster.list;
                var west = _.min(gridList, 'longtitute').longtitute - 0.01;
                var east = _.max(gridList, 'longtitute').longtitute + 0.01;
                var south = _.min(gridList, 'lattitute').lattitute - 0.01;
                var north = _.max(gridList, 'lattitute').lattitute + 0.01;
                parametersMapService.showElementsInRange(west,
                    east,
                    south,
                    north,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.overlays.sites,
                    $scope.overlays.cells);
            };

            $scope.showCluster500List = function() {
                if ($scope.cluster500List) {
                    coverageDialogService.showGridClusterStats("500个分簇结构",
                        $scope.cluster500List,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('500').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.cluster500List = _.filter(list, function(stat) { return stat.gridPoints.length > 50 });
                        coverageDialogService.showGridClusterStats("500个分簇结构",
                            $scope.cluster500List,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            };
            $scope.showCluster1000List = function() {
                if ($scope.cluster1000List) {
                    coverageDialogService.showGridClusterStats("1000个分簇结构",
                        $scope.cluster1000List,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('1000').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.cluster1000List = _.filter(list, function(stat) { return stat.gridPoints.length > 35 });
                        coverageDialogService.showGridClusterStats("1000个分簇结构",
                            $scope.cluster1000List,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            };
            $scope.showClusterAdvance = function() {
                if ($scope.clusterAdvanceList) {
                    coverageDialogService.showGridClusterStats("改进分簇结构",
                        $scope.clusterAdvanceList,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('Advance8000').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.clusterAdvanceList = _
                            .filter(list, function(stat) { return stat.gridPoints.length > 5 });
                        coverageDialogService.showGridClusterStats("改进分簇结构",
                            $scope.clusterAdvanceList,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            }
            $scope.showCluster500Points = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.cluster500List,
                    $scope.overlays.planSites,
                    50,
                    $scope.currentCluster.list[0]);
            };
            $scope.showCluster1000Points = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.cluster1000List,
                    $scope.overlays.planSites,
                    35,
                    $scope.currentCluster.list[0]);
            };
            $scope.showClusterAdvancePoints = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.clusterAdvanceList,
                    $scope.overlays.planSites,
                    5,
                    $scope.currentCluster.list[0]);
            };
        });
angular.module('network.theme', ['app.common'])
    .controller("menu.analysis",
        function($scope) {
            $scope.menuItem = {
                displayName: "五高一地两美一场",
                subItems: [
                    {
                        displayName: "高校专题",
                        url: "/#/collegeMap"
                    }, {
                        displayName: "高速专题",
                        url: "/#/highway"
                    }, {
                        displayName: "高铁专题",
                        url: "/#/railway"
                    }, {
                        displayName: "高价值区域",
                        url: "/#/highvalue"
                    }, {
                        displayName: "地铁专题",
                        url: "/#/subway"
                    }, {
                        displayName: "高档楼宇",
                        url: '/#/building'
                    }
                ]
            };
        })
    .controller("analysis.highway",
        function($scope,
            baiduMapService,
            basicImportService,
            parametersMapService,
            collegeDialogService,
            parametersDialogService,
            mapDialogService) {
            baiduMapService.initializeMap("map", 11);
            $scope.showView = function(hotSpot) {
                $scope.currentView = hotSpot.hotspotName;
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                parametersMapService.showHotSpotCellSectors(hotSpot.hotspotName, $scope.beginDate, $scope.endDate);
                baiduMapService.setCellFocus(hotSpot.longtitute, hotSpot.lattitute, 13);
            };
            $scope.showFlow = function() {
                collegeDialogService.showHotSpotFlow($scope.hotSpots, "高速公路");
            };
            $scope.showFlowTrend = function() {
                mapDialogService.showHotSpotFlowTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            $scope.showFeelingRateTrend = function() {
                mapDialogService.showHotSpotFeelingTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            $scope.showCoverageDt = function() {
                parametersDialogService.showHighwayDtInfos($scope.longBeginDate, $scope.endDate, $scope.currentView);
            };
            basicImportService.queryHotSpotsByType("高速公路").then(function(spots) {
                $scope.hotSpots = spots;
                $scope.showView($scope.hotSpots[0]);
            });
        })
    .controller("analysis.railway",
        function ($scope,
            baiduMapService,
            basicImportService,
            parametersMapService,
            collegeDialogService,
            parametersDialogService,
            mapDialogService) {
            baiduMapService.initializeMap("map", 11);
            $scope.showView = function(hotSpot) {
                $scope.currentView = hotSpot.hotspotName;
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                parametersMapService.showHotSpotCellSectors(hotSpot.hotspotName, $scope.beginDate, $scope.endDate);
                baiduMapService.setCellFocus(hotSpot.longtitute, hotSpot.lattitute, 13);
            };
            $scope.showFlow = function () {
                collegeDialogService.showHotSpotFlow($scope.hotSpots, "高速铁路");
            };
            $scope.showFlowTrend = function () {
                mapDialogService.showHotSpotFlowTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            $scope.showFeelingRateTrend = function () {
                mapDialogService.showHotSpotFeelingTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            $scope.showCoverageDt = function () {
                parametersDialogService.showHighwayDtInfos($scope.longBeginDate, $scope.endDate, $scope.currentView);
            };
            basicImportService.queryHotSpotsByType("高速铁路").then(function(spots) {
                $scope.hotSpots = spots;
                $scope.showView($scope.hotSpots[0]);
            });
        })
    .controller("analysis.subway",
        function($scope, baiduMapService, basicImportService, parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            $scope.showView = function(hotSpot) {
                $scope.currentView = hotSpot.hotspotName;
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                parametersMapService.showHotSpotCellSectors(hotSpot.hotspotName, $scope.beginDate, $scope.endDate);
                baiduMapService.setCellFocus(hotSpot.longtitute, hotSpot.lattitute, 13);
            };
            basicImportService.queryHotSpotsByType("地铁").then(function(spots) {
                $scope.hotSpots = spots;
                $scope.showView($scope.hotSpots[0]);
            });
        })
    .controller("analysis.highvalue",
        function($scope, baiduMapService, basicImportService, parametersMapService) {
            baiduMapService.initializeMap("map", 11);
            $scope.showView = function(hotSpot) {
                $scope.currentView = hotSpot.hotspotName;
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                parametersMapService.showHotSpotCellSectors(hotSpot.hotspotName, $scope.beginDate, $scope.endDate);
                baiduMapService.setCellFocus(hotSpot.longtitute, hotSpot.lattitute, 13);
            };
            basicImportService.queryHotSpotsByType("高价值区域").then(function(spots) {
                $scope.hotSpots = spots;
                $scope.showView($scope.hotSpots[0]);
            });
        });
angular.module("myApp",
[
    'home.root', 'home.route',
    'home.menu', 'home.complain', 'home.network', 'home.mr', 'home.kpi',
    'home.college', 'network.theme'
]);