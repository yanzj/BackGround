angular.module('kpi.work.chart', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("rutrace.chart",
        function($scope,
            $uibModalInstance,
            $timeout,
            dateString,
            districtStats,
            townStats,
            appKpiService) {
            $scope.dialogTitle = dateString + "精确覆盖率指标";
            $scope.showCharts = function() {
                $("#leftChart").highcharts(appKpiService
                    .getMrPieOptions(districtStats.slice(0, districtStats.length - 1), townStats));
                $("#rightChart").highcharts(appKpiService.getPreciseRateOptions(districtStats, townStats));
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $timeout(function() {
                    $scope.showCharts();
                },
                500);
        })
    .controller("rrc.chart",
        function($scope,
            $uibModalInstance,
            $timeout,
            dateString,
            districtStats,
            townStats,
            appKpiService) {
            $scope.dialogTitle = dateString + "RRC连接成功率指标";
            $scope.showCharts = function() {
                $("#leftChart").highcharts(appKpiService
                    .getRrcRequestOptions(districtStats.slice(0, districtStats.length - 1), townStats));
                $("#rightChart").highcharts(appKpiService.getRrcRateOptions(districtStats, townStats));
                $("#thirdChart").highcharts(appKpiService.getMoSignallingRrcRateOptions(districtStats, townStats));
                $("#fourthChart").highcharts(appKpiService.getMtAccessRrcRateOptions(districtStats, townStats));
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $timeout(function() {
                    $scope.showCharts();
                },
                500);
        })
    .controller("down.switch.cell.trend",
        function($scope,
            $uibModalInstance,
            name,
            cellId,
            sectorId,
            flowService,
            parametersChartService,
            appFormatService) {
            $scope.dialogTitle = "小区指标变化趋势分析" + "-" + name;
            $scope.showTrend = function() {
                $scope.beginDateString = appFormatService.getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                $scope.endDateString = appFormatService.getDateString($scope.endDate.value, "yyyy年MM月dd日");
                flowService.queryCellFlowByDateSpan(cellId,
                    sectorId,
                    $scope.beginDate.value,
                    $scope.endDate.value).then(function (result) {
                        var dates = _.map(result,
                            function (stat) {
                                return stat.statTime;
                            });
                        $("#mrsConfig").highcharts(parametersChartService.getCellFeelingRateOptions(dates, result));
                        $("#preciseConfig").highcharts(parametersChartService.getCellDownSwitchOptions(dates, result));
                });
            };
            $scope.showTrend();

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });