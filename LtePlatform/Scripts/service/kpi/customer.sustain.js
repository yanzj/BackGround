angular.module('kpi.customer.sustain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('hot.spot.dialog',
        function($scope, dialogTitle, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = {
                longtitute: 112.99,
                lattitute: 22.98
            };

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                var options =_.filter(result,
                    function(stat) {
                        return stat !== '高速公路' && stat !== '其他' &&
                            stat !== '高速铁路' && stat !== '区域定义' &&
                            stat !== '地铁' && stat !== 'TOP小区' && stat !== '校园网';
                    });
                $scope.spotType = {
                    options: options,
                    selected: options[0]
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.modify',
        function($scope, dialogTitle, dto, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = dto;
            $scope.modify = true;

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                $scope.spotType = {
                    options: result,
                    selected: $scope.dto.typeDescription
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addOneMarker(baiduMapService
                    .generateMarker(coors.x, coors.y));
                baiduMapService.setCellFocus(coors.x, coors.y);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.cell.dialog',
        function($scope,
            dialogTitle,
            address,
            name,
            center,
            $uibModalInstance,
            basicImportService,
            collegeQueryService,
            networkElementService,
            neighborImportService,
            complainService) {
            $scope.dialogTitle = dialogTitle;
            $scope.address = address;
            $scope.gridApi = {};
            $scope.gridApi2 = {};
            $scope.updateCellInfos = function(positions, existedCells) {
                neighborImportService.updateENodebRruInfo($scope.positionCells,
                    {
                        dstCells: positions,
                        cells: existedCells,
                        longtitute: center.longtitute,
                        lattitute: center.lattitute
                    });
            };
            $scope.query = function() {
                basicImportService.queryHotSpotCells(name).then(function(result) {
                    $scope.candidateIndoorCells = result;
                });
                complainService.queryHotSpotCells(name).then(function(existedCells) {
                    $scope.existedCells = existedCells;
                    $scope.positionCells = [];
                    networkElementService.queryRangeCells({
                        west: center.longtitute - 0.003,
                        east: center.longtitute + 0.003,
                        south: center.lattitute - 0.003,
                        north: center.lattitute + 0.003
                    }).then(function(positions) {
                        if (positions.length > 5) {
                            $scope.updateCellInfos(positions, existedCells);
                        } else {
                            networkElementService.queryRangeCells({
                                west: center.longtitute - 0.01,
                                east: center.longtitute + 0.01,
                                south: center.lattitute - 0.01,
                                north: center.lattitute + 0.01
                            }).then(function(pos) {
                                if (pos.length > 5) {
                                    $scope.updateCellInfos(pos, existedCells);
                                } else {
                                    networkElementService.queryRangeCells({
                                        west: center.longtitute - 0.02,
                                        east: center.longtitute + 0.02,
                                        south: center.lattitute - 0.02,
                                        north: center.lattitute + 0.02
                                    }).then(function(pos2) {
                                        $scope.updateCellInfos(pos2, existedCells);
                                    });
                                }
                                
                            });
                        }
                    });
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.importCells = function() {
                var cellNames = [];
                angular.forEach($scope.gridApi.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                angular.forEach($scope.gridApi2.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                collegeQueryService.saveCollegeCells({
                    collegeName: name,
                    cellNames: cellNames
                }).then(function() {
                    $scope.query();
                });
            }
            $scope.query();
        })
    .controller('college.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;

            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });