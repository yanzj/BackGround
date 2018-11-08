angular.module('kpi.coverage.interference', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('coverage.details.dialog',
        function($scope,
            $uibModalInstance,
            cellName,
            cellId,
            sectorId,
            topPreciseService,
            preciseChartService) {
            $scope.dialogTitle = cellName + '：覆盖详细信息';
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.coverageInfos);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        });