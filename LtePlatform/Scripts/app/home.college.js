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
    .controller("home.college",
        function($scope,
            baiduMapService,
            collegeQueryService,
            parametersMapService,
            collegeService,
            collegeMapService,
            mapDialogService,
            baiduQueryService) {
            baiduMapService.initializeMap("map", 11);
            $scope.year = new Date().getYear() + 1900;
            $scope.showView = function(college) {
                $scope.currentView = college.name;
                $scope.currentCollege = college;
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
                collegeMapService.drawCollegeArea(college.id,
                    function(center) {
                        baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                            $scope.center = {
                                X: 2 * center.X - coors.x,
                                Y: 2 * center.Y - coors.y,
                                points: center.points
                            };
                        });
                    });

                parametersMapService.showHotSpotCellSectors(college.name, $scope.beginDate, $scope.endDate);
                parametersMapService.showCollegeENodebs(college.name, $scope.beginDate, $scope.endDate);
            };
            $scope.showFlowTrend = function() {
                mapDialogService.showCollegeFlowTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            $scope.showFeelingRateTrend = function() {
                mapDialogService.showCollegeFeelingTrend($scope.beginDate, $scope.endDate, $scope.currentView);
            };
            collegeQueryService.queryAll().then(function(spots) {
                $scope.hotSpots = spots;
                $scope.showView($scope.hotSpots[0]);
            });
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