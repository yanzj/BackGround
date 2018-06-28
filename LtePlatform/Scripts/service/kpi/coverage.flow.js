angular.module('kpi.coverage.flow', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("flow.stats",
        function($scope,
            today,
            dialogTitle,
            frequency,
            $uibModalInstance,
            appRegionService,
            preciseChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.getTownFlowStats(today, frequency).then(function(result) {
                $("#leftChart").highcharts(preciseChartService.getTownFlowOption(result, frequency));
                $("#rightChart").highcharts(preciseChartService.getTownUsersOption(result, frequency));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("flow.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyFlowTrendCharts(districts, frequency, result);
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("users.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyUsersTrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("feelingRate.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyFeelingTrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('downSwitch.trend',
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyDownSwitchTrendCharts(districts, frequency, result);
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('rank2Rate.trend',
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyRand2TrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });