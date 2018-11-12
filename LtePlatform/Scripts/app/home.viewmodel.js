angular.module('home.root', ['app.common'])
    .run(function ($rootScope, appUrlService, stationFactory, appRegionService, geometryService) {
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
    'home.menu', 'home.complain', 'home.network', 'home.kpi',
    'home.college', 'network.theme'
]);