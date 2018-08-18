angular.module('station.checking', ['app.common'])
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