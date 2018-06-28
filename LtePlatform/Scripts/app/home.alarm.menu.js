angular.module('home.alarm.menu', ['app.common', 'home.station'])
    .controller("menu.resource-station",
        function($scope,
            downSwitchService,
            baiduMapService,
            parametersDialogService,
            baiduQueryService,
            mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getCommonStations($scope.stationName, 'JZ', '', 1, 10).then(function(response) {
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
    .controller("menu.resource-indoor",
        function($scope,
            downSwitchService,
            baiduMapService,
            parametersDialogService,
            baiduQueryService,
            mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getCommonStations($scope.stationName, 'SF', '', 1, 10).then(function(response) {
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
    .controller("resource-station.network",
        function($scope,
            downSwitchService,
            myValue,
            baiduMapService,
            geometryService,
            parametersDialogService,
            collegeMapService,
            dumpPreciseService,
            appUrlService,
            generalMapService) {
            $scope.distinct = $scope.distincts[0];
            baiduMapService.initializeMap("map", 13);

            $scope.getStations = function(areaName, index) {
                downSwitchService.getResourceStations(areaName, 'JZ', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        var color = $scope.colors[index];
                        $scope.legend.intervals.push({
                            threshold: areaName,
                            color: color
                        });
                        collegeMapService.showResourceStations(stations, color);
                    }

                });
            };
            $scope.reflashMap = function(areaNameIndex) {
                baiduMapService.clearOverlays();
                $scope.initializeLegend();
                var areaName = $scope.areaNames[areaNameIndex];
                $scope.distinct = $scope.distincts[areaNameIndex];

                if (areaNameIndex !== 0) {
                    baiduMapService.setCenter(dumpPreciseService.getDistrictIndex(areaName));
                    $scope.getStations(areaName, areaNameIndex);
                } else {
                    baiduMapService.setCenter(0);
                    angular.forEach($scope.areaNames.slice(1, $scope.areaNames.length),
                        function(name, $index) {
                            $scope.getStations(name, $index);
                        });
                }

            };


            $scope.districts = [];
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.initializeLegend();
                        baiduMapService.clearOverlays();
                        if ($scope.distincts.length === 1) {
                            generalMapService
                                .generateUsersDistrictsAndDistincts(city,
                                    $scope.districts,
                                    $scope.distincts,
                                    $scope.areaNames,
                                    function(district, $index) {
                                        $scope.getStations('FS' + district, $index + 1);
                                    });
                        } else {
                            generalMapService.generateUsersDistrictsOnly(city,
                                $scope.districts,
                                function(district, $index) {
                                    $scope.getStations('FS' + district, $index + 1);
                                });
                        }
                    }
                });

        })
    .controller("resource-indoor.network",
        function($scope,
            downSwitchService,
            myValue,
            baiduMapService,
            geometryService,
            parametersDialogService,
            collegeMapService,
            dumpPreciseService,
            appUrlService,
            generalMapService) {
            $scope.distinct = $scope.distincts[0];
            baiduMapService.initializeMap("map", 13);

            $scope.getStations = function(areaName, index) {
                downSwitchService.getResourceStations(areaName, 'SF', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        var color = $scope.colors[index];
                        $scope.legend.intervals.push({
                            threshold: areaName,
                            color: color
                        });
                        collegeMapService.showResourceStations(stations, color);
                    }

                });
            };
            $scope.reflashMap = function(areaNameIndex) {
                baiduMapService.clearOverlays();
                $scope.initializeLegend();
                var areaName = $scope.areaNames[areaNameIndex];
                $scope.distinct = $scope.distincts[areaNameIndex];

                if (areaNameIndex !== 0) {
                    baiduMapService.setCenter(dumpPreciseService.getDistrictIndex(areaName));
                    $scope.getStations(areaName, areaNameIndex);
                } else {
                    baiduMapService.setCenter(0);
                    angular.forEach($scope.areaNames.slice(1, $scope.areaNames.length),
                        function(name, $index) {
                            $scope.getStations(name, $index);
                        });
                }

            };


            $scope.districts = [];
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        $scope.initializeLegend();
                        baiduMapService.clearOverlays();
                        if ($scope.distincts.length === 1) {
                            generalMapService
                                .generateUsersDistrictsAndDistincts(city,
                                    $scope.districts,
                                    $scope.distincts,
                                    $scope.areaNames,
                                    function(district, $index) {
                                        $scope.getStations('FS' + district, $index + 1);
                                    });
                        } else {
                            generalMapService.generateUsersDistrictsOnly(city,
                                $scope.districts,
                                function(district, $index) {
                                    $scope.getStations('FS' + district, $index + 1);
                                });
                        }
                    }
                });

        });
   
