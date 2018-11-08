angular.module('kpi.parameter.rutrace', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("general.coverage.analysis",
        function($scope,
            cell,
            $uibModalInstance,
            topPreciseService,
            preciseInterferenceService,
            preciseChartService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "小区覆盖分析: " + $scope.currentCellName;
            $scope.detailsDialogTitle = cell.name + "-" + cell.sectorId + "详细小区统计";
            $scope.cellId = cell.cellId;
            $scope.sectorId = cell.sectorId;
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller('map.source.dialog',
        function($scope, $uibModalInstance, neighbor, dialogTitle, topPreciseService, preciseChartService) {
            $scope.neighbor = neighbor;
            $scope.dialogTitle = dialogTitle;
            if (neighbor.cellId !== undefined) {
                $scope.cellId = neighbor.cellId;
                $scope.sectorId = neighbor.sectorId;
            } else {
                $scope.cellId = neighbor.destENodebId;
                $scope.sectorId = neighbor.destSectorId;
            }
            topPreciseService.queryCoverage($scope.beginDate.value,
                $scope.endDate.value,
                $scope.cellId,
                $scope.sectorId).then(function(result) {
                var options = preciseChartService.getCoverageOptions(result);
                $("#coverage-chart").highcharts(options);
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("cell.info.dialog",
        function($scope, cell, dialogTitle, neighborMongoService, $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.isHuaweiCell = false;
            $scope.eNodebId = cell.eNodebId;
            $scope.sectorId = cell.sectorId;

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
            });

        });