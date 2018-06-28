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