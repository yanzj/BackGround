angular.module('station.fixing', ['app.common'])
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