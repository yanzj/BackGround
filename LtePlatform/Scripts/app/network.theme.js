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