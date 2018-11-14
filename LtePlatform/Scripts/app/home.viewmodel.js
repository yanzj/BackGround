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
            .state('micro',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Micro.html",
                            controller: "complain.micro"
                        }
                    },
                    url: "/micro"
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
                        displayName: "手机伴侣",
                        url: "/#/micro"
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
angular.module("myApp",
[
    'home.root', 'home.route',
    'home.menu', 'home.complain', 'home.network'
]);