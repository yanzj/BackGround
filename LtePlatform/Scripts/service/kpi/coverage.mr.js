angular.module('kpi.coverage.mr', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("grid.stats",
        function($scope, dialogTitle, category, stats, $uibModalInstance, $timeout, generalChartService) {
            $scope.dialogTitle = dialogTitle;
            var options = generalChartService.getPieOptions(stats,
                {
                    title: dialogTitle,
                    seriesTitle: category
                },
                function(stat) {
                    return stat.key;
                },
                function(stat) {
                    return stat.value;
                });
            $timeout(function() {
                    $("#rightChart").highcharts(options);
                },
                500);

            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("grid.cluster",
        function($scope,
            dialogTitle,
            clusterList,
            currentCluster,
            $uibModalInstance,
            alarmImportService) {
            $scope.dialogTitle = dialogTitle;
            $scope.clusterList = clusterList;
            $scope.currentCluster = currentCluster;

            $scope.calculateKpis = function() {
                angular.forEach($scope.clusterList,
                    function(stat) {
                        alarmImportService.updateClusterKpi(stat);
                    });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.currentCluster);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("agps.stats",
        function($scope,
            dialogTitle,
            stats,
            legend,
            $uibModalInstance,
            $timeout,
            generalChartService) {
            $scope.dialogTitle = dialogTitle;
            var intervalStats = [];
            var low = -10000;
            angular.forEach(legend,
                function(interval) {
                    var high = interval.threshold;
                    intervalStats.push({
                        interval: '[' + low + ', ' + high + ')dBm',
                        count: _.countBy(stats,
                            function(stat) { return stat.telecomRsrp - 140 >= low && stat.telecomRsrp - 140 < high })[
                            'true']
                    });
                    low = high;
                });
            intervalStats.push({
                interval: 'RSRP >= ' + low + 'dBm',
                count: _.countBy(stats,
                    function(stat) { return stat.telecomRsrp - 140 >= low && stat.telecomRsrp - 140 < 10000 })['true']
            });
            var counts = stats.length;
            var operators = ['-110dBm以上', '-105dBm以上', '-100dBm以上'];
            var coverages = [
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 30 })['true'] / counts * 100,
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 35 })['true'] / counts * 100,
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 40 })['true'] / counts * 100
            ];
            var rate100Intervals = [];
            var rate105Intervals = [];
            var thresholds = [
                {
                    low: 0,
                    high: 0.5
                }, {
                    low: 0.5,
                    high: 0.75
                }, {
                    low: 0.75,
                    high: 0.9
                }, {
                    low: 0.9,
                    high: 1.01
                }
            ];
            angular.forEach(thresholds,
                function(threshold) {
                    rate100Intervals.push({
                        interval: '[' + threshold.low + ', ' + threshold.high + ')',
                        count: _.countBy(stats,
                            function(stat) {
                                return stat.telecomRate100 >= threshold.low && stat.telecomRate100 < threshold.high
                            })['true']
                    });
                    rate105Intervals.push({
                        interval: '[' + threshold.low + ', ' + threshold.high + ')',
                        count: _.countBy(stats,
                            function(stat) {
                                return stat.telecomRate105 >= threshold.low && stat.telecomRate105 < threshold.high
                            })['true']
                    });
                });
            $timeout(function() {
                    $("#leftChart").highcharts(generalChartService.getPieOptions(intervalStats,
                        {
                            title: 'RSRP区间分布',
                            seriesTitle: 'RSRP区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                    $("#rightChart").highcharts(generalChartService.queryColumnOptions({
                            title: 'RSRP覆盖优良率（%）',
                            ytitle: '覆盖优良率（%）',
                            xtitle: '覆盖标准',
                            min: 80,
                            max: 100
                        },
                        operators,
                        coverages));
                    $("#thirdChart").highcharts(generalChartService.getPieOptions(rate100Intervals,
                        {
                            title: '覆盖率区间分布（RSRP>-100dBm）',
                            seriesTitle: '覆盖率区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                    $("#fourthChart").highcharts(generalChartService.getPieOptions(rate105Intervals,
                        {
                            title: '覆盖率区间分布（RSRP>-105dBm）',
                            seriesTitle: '覆盖率区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                },
                500);

            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });