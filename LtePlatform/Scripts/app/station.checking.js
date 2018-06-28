angular.module('station.checking', ['app.common', 'home.station'])
    .controller("menu.checking-station",
    function ($scope, downSwitchService, baiduMapService, parametersDialogService, baiduQueryService, mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getCheckingStationByName($scope.stationName, 'JZ', '', 1, 10)
                    .then(function(response) {
                        $scope.stations = response.result.rows;
                    });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showCommonStationInfo($scope.stations[index - 1]);
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
                                    mapDialogService.showCommonStationInfo(this.data);
                                });
                        });
                });
        })
    .controller("menu.checking-indoor",
    function ($scope, downSwitchService, baiduMapService, parametersDialogService, baiduQueryService, mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getCheckingStationByName($scope.stationName, 'SF', '', 1, 10)
                    .then(function(response) {
                        $scope.stations = response.result.rows;
                    });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showCommonStationInfo($scope.stations[index - 1]);
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
                                    mapDialogService.showCommonStationInfo(this.data);
                                });
                        });
                });
        })
    .controller("checking-station.network",
        function($scope,
            $location,
            downSwitchService,
            baiduMapService,
            geometryService,
            collegeMapService,
            dumpPreciseService,
            parametersDialogService) {
            $scope.areaNames = new Array('FS', 'SD', 'NH', 'CC', 'SS', 'GM');
            $scope.distincts = new Array('佛山市', '顺德区', '南海区', '禅城区', '三水区', '高明区');
            $scope.distinct = $scope.distincts[0];
            $scope.statusNames = new Array('未巡检', '已巡检', '全部');
            baiduMapService.initializeMap("map", 13);

            $scope.statusIndex = 0;
            $scope.status = $scope.statusNames[$scope.statusIndex];
            $scope.distinctIndex = 0;

            $scope.getStations = function(areaIndex, status, color) {
                var areaName = $scope.areaNames[areaIndex];
                downSwitchService.getCheckingStation(areaName, status, 'JZ', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        collegeMapService.showCheckingStations(stations, color, status,'JZ');
                    }
                });
            };
            $scope.checkPlanList = function () {
                parametersDialogService.showCheckPlanList('JZ');
            }
            $scope.changeDistinct = function(index) {

                $scope.distinctIndex = index;
                $scope.distinct = $scope.distincts[$scope.distinctIndex];

                $scope.reflashMap();
            };
            $scope.changeStatus = function(index) {
                $scope.statusIndex = index;
                $scope.status = $scope.statusNames[$scope.statusIndex];

                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter($scope.distinctIndex);
                if ($scope.distinctIndex !== 0) {
                    if ($scope.statusIndex !== 2) {
                        $scope.getStations($scope.distinctIndex,
                            $scope.statusNames[$scope.statusIndex],
                            $scope.colors[$scope.statusIndex]);
                    } else {
                        for (var i = 0; i < 2; ++i) {
                            $scope.getStations($scope.distinctIndex, $scope.statusNames[i], $scope.colors[i]);
                        }
                    }
                } else {
                    if ($scope.statusIndex !== 2) {
                        for (var i = 1; i < 6; ++i) {
                            $scope.getStations(i,
                                $scope.statusNames[$scope.statusIndex],
                                $scope.colors[$scope.statusIndex]);
                        }
                    } else {
                        for (var i = 1; i < 6; ++i) {
                            for (var j = 0; j < 2; ++j)
                                $scope.getStations(i, $scope.statusNames[j], $scope.colors[j]);
                        }
                    }
                }
            };
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.initializeLegend();
                        baiduMapService.clearOverlays();
                        $scope.legend.title = "站点状态";
                        $scope.legend.intervals = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district, $index) {
                                $scope.pushStationArea(district);
                                angular.forEach($scope.statusNames.slice(0, $scope.statusNames.length - 1),
                                    function(status, $subIndex) {
                                        $scope.getStations($index + 1, status, $scope.colors[$subIndex]);
                                        if ($index === 0) {
                                            $scope.legend.intervals.push({
                                                threshold: status,
                                                color: $scope.colors[$subIndex]
                                            });
                                        }
                                    });

                            });
                    }
                });
        })
    .controller("checking-indoor.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            collegeMapService,
            dumpPreciseService,
            parametersDialogService) {
            $scope.areaNames = new Array('FS', 'SD', 'NH', 'CC', 'SS', 'GM');
            $scope.distincts = new Array('佛山市', '顺德区', '南海区', '禅城区', '三水区', '高明区');
            $scope.distinct = $scope.distincts[0];
            $scope.statusNames = new Array('未巡检', '已巡检', '全部');
            baiduMapService.initializeMap("map", 13);

            $scope.statusIndex = 0;
            $scope.status = $scope.statusNames[$scope.statusIndex];
            $scope.distinctIndex = 0;

            $scope.getStations = function(areaIndex, status, color) {
                var areaName = $scope.areaNames[areaIndex];
                downSwitchService.getCheckingStation(areaName, status, 'SF', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        collegeMapService.showCheckingStations(stations, color, status,'SF');
                    }
                });
            };
            $scope.checkPlanList = function () {
                parametersDialogService.showCheckPlanList('SF');
            }
            $scope.changeDistinct = function(index) {

                $scope.distinctIndex = index;
                $scope.distinct = $scope.distincts[$scope.distinctIndex];

                $scope.reflashMap();
            };
            $scope.changeStatus = function(index) {
                $scope.statusIndex = index;
                $scope.status = $scope.statusNames[$scope.statusIndex];

                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter($scope.distinctIndex);
                if ($scope.distinctIndex !== 0) {
                    if ($scope.statusIndex !== 2) {
                        $scope.getStations($scope.distinctIndex,
                            $scope.statusNames[$scope.statusIndex],
                            $scope.colors[$scope.statusIndex]);
                    } else {
                        for (var i = 0; i < 2; ++i) {
                            $scope.getStations($scope.distinctIndex, $scope.statusNames[i], $scope.colors[i]);
                        }
                    }
                } else {
                    if ($scope.statusIndex !== 2) {
                        for (var i = 1; i < 6; ++i) {
                            $scope.getStations(i,
                                $scope.statusNames[$scope.statusIndex],
                                $scope.colors[$scope.statusIndex]);
                        }
                    } else {
                        for (var i = 1; i < 6; ++i) {
                            for (var j = 0; j < 2; ++j)
                                $scope.getStations(i, $scope.statusNames[j], $scope.colors[j]);
                        }
                    }
                }
            };
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.initializeLegend();
                        baiduMapService.clearOverlays();
                        $scope.legend.title = "站点状态";
                        $scope.legend.intervals = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            $scope.districts,
                            function(district, $index) {
                                $scope.pushStationArea(district);
                                angular.forEach($scope.statusNames.slice(0, $scope.statusNames.length - 1),
                                    function(status, $subIndex) {
                                        $scope.getStations($index + 1, status, $scope.colors[$subIndex]);
                                        if ($index === 0) {
                                            $scope.legend.intervals.push({
                                                threshold: status,
                                                color: $scope.colors[$subIndex]
                                            });
                                        }
                                    });

                            });
                    }
                });
        })
    .controller("fault-station.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            geometryService,
            mapDialogService,
            baiduQueryService) {
            $scope.stationss = [];
            $scope.stationss[1] = [];
            $scope.stationss[2] = [];
            baiduMapService.initializeMap("map", 13);

            $scope.colorFault = new Array("#FF0000", "#00FF00");

            //获取站点
            $scope.getStations = function() {
                downSwitchService.getFaultStations(0, 10000).then(function(response) {

                    $scope.stationss[1] = response.result.rows;
                    var color = $scope.colorFault[0];
                    baiduQueryService.transformToBaidu($scope.stationss[1][0].longtitute,
                        $scope.stationss[1][0].lattitute).then(function(coors) {
                        var xOffset = coors.x - $scope.stationss[1][0].longtitute;
                        var yOffset = coors.y - $scope.stationss[1][0].lattitute;
                        baiduMapService.drawPointCollection($scope.stationss[1],
                            color,
                            -xOffset,
                            -yOffset,
                            function(e) {
                                mapDialogService.showFaultStationInfo(e.point.data);
                            });
                    });
                });
            };
            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter(0);
                $scope.getStations();
            };
            $scope.reflashMap();
        });