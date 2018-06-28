angular.module('station.fixing', ['app.common', 'home.station'])
    .controller("menu.fixing-station",
    function ($scope, downSwitchService, baiduMapService, parametersDialogService, baiduQueryService, mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getFixingStationByName($scope.stationName, 'JZ', '', 1, 10).then(function(response) {
                    $scope.stations = response.result.rows;
                });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showFixingStationInfo($scope.stations[index - 1]);
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
                                    mapDialogService.showFixingStationInfo(this.data);
                                });
                        });
                });
        })
    .controller("menu.fixing-indoor",
    function ($scope, downSwitchService, baiduMapService, parametersDialogService, baiduQueryService, mapDialogService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getFixingStationByName($scope.stationName, 'SF', '', 1, 10).then(function(response) {
                    $scope.stations = response.result.rows;
                });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showFixingStationInfo($scope.stations[index - 1]);
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
                                    mapDialogService.showFixingStationInfo(this.data);
                                });
                        });
                });
        })
    .controller("fixing-station.network",
        function($scope,
            $location,
            downSwitchService,
            baiduMapService,
            collegeMapService,
            dumpPreciseService) {

            $scope.distincts = new Array('佛山市', '顺德区', '南海区', '禅城区', '三水区', '高明区');
            $scope.distinct = $scope.distincts[0];
            $scope.alphabetNames = new Array('FS', 'SD', 'NH', 'CC', 'SS', 'GM');
            $scope.statusNames = new Array('很紧急', '紧急', '极重要', '重要', '一般', '整治完成', '全部');
            baiduMapService.initializeMap("map", 13);

            $scope.statusIndex = 0;
            $scope.status = $scope.statusNames[$scope.statusIndex];
            $scope.distinctIndex = 0;

            //获取站点
            $scope.getStations = function(areaIndex, status, color) {
                var areaName = $scope.alphabetNames[areaIndex];
                downSwitchService.getFixingStation(areaName, status, 'JZ', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        collegeMapService.showFixingStations(stations, color);
                    }

                });
            };

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
                    if ($scope.statusIndex !== 6) {
                        $scope.getStations($scope.distinctIndex,
                            $scope.statusNames[$scope.statusIndex],
                            $scope.colors[$scope.statusIndex]);
                    } else {
                        for (var i = 0; i < 6; ++i) {
                            $scope.getStations($scope.distinctIndex, $scope.statusNames[i], $scope.colors[i]);
                        }
                    }
                } else {
                    if ($scope.statusIndex !== 6) {
                        for (var i = 1; i < 6; ++i) {
                            $scope.getStations(i,
                                $scope.statusNames[$scope.statusIndex],
                                $scope.colors[$scope.statusIndex]);
                        }
                    } else {
                        for (var i = 1; i < 6; ++i) {
                            for (var j = 0; j < 6; ++j)
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
    .controller("fixing-indoor.network",
        function($scope, downSwitchService, baiduMapService, collegeMapService, dumpPreciseService) {
            $scope.distincts = new Array('佛山市', '顺德区', '南海区', '禅城区', '三水区', '高明区');
            $scope.distinct = $scope.distincts[0];
            $scope.alphabetNames = new Array('FS', 'SD', 'NH', 'CC', 'SS', 'GM');
            $scope.statusNames = new Array('很紧急', '紧急', '极重要', '重要', '一般', '整治完成', '全部');
            baiduMapService.initializeMap("map", 13);

            $scope.statusIndex = 0;
            $scope.status = $scope.statusNames[$scope.statusIndex];
            $scope.distinctIndex = 0;

            //获取站点
            $scope.getStations = function(areaIndex, status, color) {
                var areaName = $scope.alphabetNames[areaIndex];
                downSwitchService.getFixingStation(areaName, status, 'JZ', 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    if (stations.length) {
                        collegeMapService.showFixingStations(stations, color);
                    }

                });
            };

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
                    if ($scope.statusIndex !== 6) {
                        $scope.getStations($scope.distinctIndex,
                            $scope.statusNames[$scope.statusIndex],
                            $scope.colors[$scope.statusIndex]);
                    } else {
                        for (var i = 0; i < 6; ++i) {
                            $scope.getStations($scope.distinctIndex, $scope.statusNames[i], $scope.colors[i]);
                        }
                    }
                } else {
                    if ($scope.statusIndex !== 6) {
                        for (var i = 1; i < 6; ++i) {
                            $scope.getStations(i,
                                $scope.statusNames[$scope.statusIndex],
                                $scope.colors[$scope.statusIndex]);
                        }
                    } else {
                        for (var i = 1; i < 6; ++i) {
                            for (var j = 0; j < 6; ++j)
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
    .controller("special-station.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            geometryService,
            mapDialogService,
            baiduQueryService) {

            $scope.isRecovers = new Array('未恢复', '已恢复', '全部');
            $scope.isRecover = new Array('否', '是');
            baiduMapService.initializeMap("map", 13);

            $scope.recoverIndex = 0;
            $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];


            //获取站点
            $scope.getStations = function(recoverIndex) {
                var recoverName = $scope.isRecover[recoverIndex];
                downSwitchService.getSpecialStations(recoverName, 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    var color = $scope.colors[recoverIndex];
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointCollection(stations,
                                color,
                                -xOffset,
                                -yOffset,
                                function(e) {
                                    mapDialogService.showSpecialStationInfo(e.point.data);
                                });
                        });
                });
            };


            $scope.changeRecover = function(index) {
                $scope.recoverIndex = index;
                $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];
                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter(0);
                if ($scope.recoverIndex === 2) {
                    for (var i = 0; i < 2; ++i) {
                        $scope.getStations(i);
                    }
                } else {
                    $scope.getStations($scope.recoverIndex);
                }

            };
            $scope.initializeLegend();
            $scope.legend.title = "故障状态";
            $scope.initializeFaultLegend($scope.colors);
            $scope.reflashMap();
        })
    .controller("special-indoor.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            geometryService,
            mapDialogService,
            baiduQueryService) {

            $scope.isRecovers = new Array('未恢复', '已恢复', '全部');
            $scope.isRecover = new Array('否', '是');
            baiduMapService.initializeMap("map", 13);

            $scope.colorFault = new Array("#FF0000", "#00FF00");

            $scope.recoverIndex = 0;
            $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];

            //获取站点
            $scope.getStations = function(recoverIndex) {

                var recoverName = $scope.isRecover[recoverIndex];
                downSwitchService.getSpecialIndoor(recoverName, 0, 10000).then(function(response) {

                    var stations = response.result.rows;
                    var color = $scope.colorFault[recoverIndex];
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointCollection(stations,
                                color,
                                -xOffset,
                                -yOffset,
                                function(e) {
                                    mapDialogService.showSpecialIndoorInfo(e.point.data);
                                });
                        });
                });
            };


            $scope.changeRecover = function(index) {
                $scope.recoverIndex = index;
                $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];
                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter(0);
                if ($scope.recoverIndex === 2) {
                    for (var i = 0; i < 2; ++i) {
                        $scope.getStations(i);
                    }
                } else {
                    $scope.getStations($scope.recoverIndex);
                }

            };
            $scope.initializeLegend();
            $scope.initializeFaultLegend($scope.colorFault);
            $scope.reflashMap();
        })
    .controller("clear-voice.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            geometryService,
            mapDialogService,
            baiduQueryService) {

            $scope.isRecovers = new Array('未解决', '已解决', '全部');
            $scope.isRecover = new Array('未解决', '已解决');
            baiduMapService.initializeMap("map", 13);

            $scope.colorFault = new Array("#FF0000", "#00FF00");


            $scope.recoverIndex = 0;
            $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];


            //获取站点
            $scope.getStations = function(recoverIndex) {

                var recoverName = $scope.isRecover[recoverIndex];
                downSwitchService.getZeroVoice(recoverName, 0, 10000).then(function(response) {

                    var stations = response.result.rows;
                    var color = $scope.colorFault[recoverIndex];
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointCollection(stations,
                                color,
                                -xOffset,
                                -yOffset,
                                function(e) {
                                    mapDialogService.showZeroVoiceInfo(e.point.data);
                                });
                        });
                });
            };


            $scope.changeRecover = function(index) {
                $scope.recoverIndex = index;
                $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];
                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter(0);
                if ($scope.recoverIndex === 2) {
                    for (var i = 0; i < 2; ++i) {
                        $scope.getStations(i);
                    }
                } else {
                    $scope.getStations($scope.recoverIndex);
                }

            };
            $scope.initializeLegend();
            $scope.initializeSolveLegend($scope.colorFault);
            $scope.reflashMap();
        })
    .controller("clear-flow.network",
        function($scope,
            downSwitchService,
            baiduMapService,
            geometryService,
            mapDialogService,
            baiduQueryService) {

            $scope.isRecovers = new Array('未解决', '已解决', '全部');
            $scope.isRecover = new Array('未解决', '已解决');
            baiduMapService.initializeMap("map", 13);

            $scope.colorFault = new Array("#FF0000", "#00FF00");

            $scope.recoverIndex = 0;
            $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];


            //获取站点
            $scope.getStations = function(recoverIndex) {

                var recoverName = $scope.isRecover[recoverIndex];
                downSwitchService.getZeroFlow(recoverName, 0, 10000).then(function(response) {
                    var stations = response.result.rows;
                    var color = $scope.colorFault[recoverIndex];
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointCollection(stations,
                                color,
                                -xOffset,
                                -yOffset,
                                function(e) {
                                    mapDialogService.showZeroFlowInfo(e.point.data);
                                });
                        });
                });
            };

            $scope.changeRecover = function(index) {
                $scope.recoverIndex = index;
                $scope.recoverName = $scope.isRecovers[$scope.recoverIndex];
                $scope.reflashMap();
            };

            $scope.reflashMap = function() {
                baiduMapService.clearOverlays();
                baiduMapService.setCenter(0);
                if ($scope.recoverIndex === 2) {
                    for (var i = 0; i < 2; ++i) {
                        $scope.getStations(i);
                    }
                } else {
                    $scope.getStations($scope.recoverIndex);
                }

            };
            $scope.initializeLegend();
            $scope.initializeSolveLegend($scope.colorFault);
            $scope.reflashMap();
        });