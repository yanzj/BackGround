angular.module('kpi.college.maintain', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('cell.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            eNodebs,
            cells,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            angular.forEach(eNodebs,
                function(eNodeb) {
                    networkElementService.queryCellInfosInOneENodeb(eNodeb.eNodebId).then(function(cellInfos) {
                        neighborImportService.updateCellRruInfo($scope.supplementCells,
                        {
                            dstCells: cellInfos,
                            cells: cells,
                            longtitute: eNodeb.longtitute,
                            lattitute: eNodeb.lattitute
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cell.position.supplement.dialog',
        function($scope,
            $uibModalInstance,
            collegeMapService,
            baiduQueryService,
            collegeService,
            networkElementService,
            neighborImportService,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    collegeService.queryCells(collegeName).then(function(cells) {
                        baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                            collegeService.queryRange(collegeName).then(function(range) {
                                networkElementService
                                    .queryRangeCells(neighborImportService.generateRange(range, center, coors))
                                    .then(function(results) {
                                        neighborImportService.updateENodebRruInfo($scope.supplementCells,
                                        {
                                            dstCells: results,
                                            cells: cells,
                                            longtitute: center.X,
                                            lattitute: center.Y
                                        });
                                    });
                            });
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('eNodeb.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE基站补充";
            $scope.supplementENodebs = [];
            $scope.gridApi = {};

            $scope.query = function() {
                baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                    collegeService.queryRange(collegeName).then(function(range) {
                        var ids = [];
                        collegeService.queryENodebs(collegeName).then(function(eNodebs) {
                            angular.forEach(eNodebs,
                                function(eNodeb) {
                                    ids.push(eNodeb.eNodebId);
                                });
                            networkElementService
                                .queryRangeENodebs(neighborImportService
                                    .generateRangeWithExcludedIds(range, center, coors, ids)).then(function(results) {
                                    angular.forEach(results,
                                        function(item) {
                                            item.distance = geometryService
                                                .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                        });
                                    $scope.supplementENodebs = results;
                                });
                        });
                    });
                });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('bts.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "CDMA基站补充";
            $scope.supplementBts = [];
            $scope.gridApi = {};

            baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                collegeService.queryRange(collegeName).then(function(range) {
                    var ids = [];
                    collegeService.queryBtss(collegeName).then(function(btss) {
                        angular.forEach(btss,
                            function(bts) {
                                ids.push(bts.btsId);
                            });
                        networkElementService
                            .queryRangeBtss(neighborImportService
                                .generateRangeWithExcludedIds(range, center, coors, ids)).then(function(results) {
                                angular.forEach(results,
                                    function(item) {
                                        item.distance = geometryService
                                            .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                    });
                                $scope.supplementBts = results;
                            });
                    });
                });
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.new.dialog',
        function($scope,
            $uibModalInstance,
            baiduMapService,
            geometryService,
            baiduQueryService,
            appRegionService,
            $timeout) {
            $scope.dialogTitle = "新建校园信息";
            $scope.collegeRegion = {
                area: 0,
                regionType: 0,
                info: ""
            };
            $scope.saveCircleParameters = function(circle) {
                var center = circle.getCenter();
                var radius = circle.getRadius();
                $scope.collegeRegion.regionType = 0;
                $scope.collegeRegion.area = BMapLib.GeoUtils.getCircleArea(circle);
                $scope.collegeRegion.info = center.lng + ';' + center.lat + ';' + radius;
            };
            $scope.saveRetangleParameters = function(rect) {
                $scope.collegeRegion.regionType = 1;
                var pts = rect.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $scope.savePolygonParameters = function(polygon) {
                $scope.collegeRegion.regionType = 2;
                var pts = polygon.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $timeout(function() {
                    baiduMapService.initializeMap('map', 12);
                    baiduMapService.initializeDrawingManager();
                    baiduMapService.addDrawingEventListener('circlecomplete', $scope.saveCircleParameters);
                    baiduMapService.addDrawingEventListener('rectanglecomplete', $scope.saveRetangleParameters);
                    baiduMapService.addDrawingEventListener('polygoncomplete', $scope.savePolygonParameters);
                },
                500);
            $scope.matchPlace = function() {
                baiduQueryService.queryBaiduPlace($scope.collegeName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                        });
                });
            };
            appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                $scope.district = {
                    options: districts,
                    selected: districts[0]
                };
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town = {
                        options: towns,
                        selected: towns[0]
                    };
                });
            });
            $scope.ok = function() {
                $scope.college = {
                    name: $scope.collegeName,
                    townId: 0,
                    collegeRegion: $scope.collegeRegion
                };
                appRegionService.queryTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                    .then(function(town) {
                        if (town) {
                            $scope.college.townId = town.id;
                            $uibModalInstance.close($scope.college);
                        }
                    });
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("maintain.college.dialog",
        function($scope,
            $uibModalInstance,
            dialogTitle,
            year,
            collegeService,
            collegeQueryService,
            collegeDialogService,
            appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.collegeYearList = [];
            $scope.collegeInfo = {
                names: []
            };
            collegeService.queryNames().then(function(result) {
                $scope.collegeInfo.names = result;
                $scope.collegeName = $scope.collegeInfo.names[0];
            });

            $scope.updateInfos = function() {
                collegeService.queryStats(year).then(function(colleges) {
                    $scope.collegeList = colleges;
                });
                collegeQueryService.queryYearList(year).then(function(colleges) {
                    $scope.collegeYearList = colleges;
                });
                $scope.updateCollegeStatus($scope.collegeName);
            };
            $scope.updateCollegeStatus = function(name) {
                collegeQueryService.queryByNameAndYear(name, year).then(function(info) {
                    $scope.collegeExisted = !!info;
                });
            };
            $scope.$watch('collegeName',
                function(name) {
                    $scope.updateCollegeStatus(name);
                });
            $scope.addOneCollegeMarkerInfo = function() {
                collegeQueryService.queryByNameAndYear($scope.collegeName, year - 1).then(function(item) {
                    if (!item) {
                        var begin = new Date();
                        begin.setDate(begin.getDate() - 365 - 7);
                        var end = new Date();
                        end.setDate(end.getDate() - 365);
                        collegeQueryService.queryByName($scope.collegeName).then(function(college) {
                            item = {
                                oldOpenDate: appFormatService.getDateString(begin, 'yyyy-MM-dd'),
                                newOpenDate: appFormatService.getDateString(end, 'yyyy-MM-dd'),
                                collegeId: college.id
                            };
                            collegeDialogService.addYearInfo(item,
                                $scope.collegeName,
                                year,
                                function() {
                                    $scope.updateInfos();
                                });
                        });
                    } else {
                        collegeDialogService.addYearInfo(item,
                            $scope.collegeName,
                            year,
                            function() {
                                $scope.updateInfos();
                            });
                    }
                });
            };
            $scope.createNewCollege = function() {
                collegeDialogService.addNewCollege(function() {
                    $scope.updateInfos();
                });
            };

            $scope.updateInfos();

            $scope.ok = function() {
                $uibModalInstance.close($scope.cell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });
