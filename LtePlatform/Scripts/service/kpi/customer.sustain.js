angular.module('kpi.customer.sustain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('hot.spot.dialog',
        function($scope, dialogTitle, $uibModalInstance, kpiPreciseService, baiduMapService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = {
                longtitute: 112.99,
                lattitute: 22.98
            };

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                $scope.spotType = {
                    options: result,
                    selected: result[0]
                };
                baiduMapService.switchSubMap();
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng;
                    $scope.dto.lattitute = e.point.lat;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker($scope.dto.longtitute, $scope.dto.lattitute));
                });
            });
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.modify',
        function($scope, dialogTitle, dto, $uibModalInstance, kpiPreciseService, baiduMapService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = dto;
            $scope.modify = true;

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                $scope.spotType = {
                    options: result,
                    selected: $scope.dto.typeDescription
                };
                baiduMapService.switchSubMap();
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addOneMarker(baiduMapService
                    .generateMarker($scope.dto.longtitute, $scope.dto.lattitute));
                baiduMapService.setCellFocus($scope.dto.longtitute, $scope.dto.lattitute);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng;
                    $scope.dto.lattitute = e.point.lat;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker($scope.dto.longtitute, $scope.dto.lattitute));
                });
            });
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
                        neighborImportService.updateENodebRruInfo($scope.positionCells,
                        {
                            dstCells: positions,
                            cells: existedCells,
                            longtitute: center.longtitute,
                            lattitute: center.lattitute
                        });
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