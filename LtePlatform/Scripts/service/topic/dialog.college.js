angular.module("topic.dialog.college", ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .run(function($rootScope) {
        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };
    })
    .controller("college.flow.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            collegeService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "流量分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, name, $scope.mergeStats);
            };
            collegeService.queryCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("hotSpot.flow.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "流量分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, name, $scope.mergeStats);
            };
            complainService.queryHotSpotCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.feeling.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            collegeService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "感知速率分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFeelingStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFeelingCharts($scope.flowStats, name, $scope.mergeStats);
            };
            collegeService.queryCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("hotSpot.feeling.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "感知速率分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFeelingStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFeelingCharts($scope.flowStats, name, $scope.mergeStats);
            };
            complainService.queryHotSpotCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.coverage.all',
        function($scope,
            beginDate,
            endDate,
            $uibModalInstance,
            collegeDtService,
            collegeMapService) {
            $scope.dialogTitle = "校园网路测数据查询";
            $scope.dtInfos = [];
            $scope.query = function() {
                collegeMapService.showDtInfos($scope.dtInfos, beginDate.value, endDate.value);
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.flow.dump',
        function($scope,
            $uibModalInstance,
            beginDate,
            endDate,
            basicCalculationService,
            appRegionService,
            collegeQueryService,
            kpiDisplayService) {
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.dialogTitle = "校园网流量导入";
            $scope.query = function() {
                $scope.stats = basicCalculationService
                    .generateDateSpanSeries($scope.beginDate.value, $scope.endDate.value);
                angular.forEach($scope.stats,
                    function(stat) {
                        appRegionService.getCurrentDateTownFlowStats(stat.date, 'college').then(function(items) {
                            stat.items = items;
                            if (!items.length) {
                                collegeQueryService.retrieveDateCollegeFlowStats(stat.date).then(function(newItems) {
                                    stat.items = newItems;
                                    angular.forEach(newItems,
                                        function(item) {
                                            appRegionService.updateTownFlowStat(item).then(function(result) {});
                                        });
                                    $scope.updateCollegeNames(newItems);
                                });
                            } else {
                                $scope.updateCollegeNames(items);
                            }
                        });
                    });
            };
            $scope.updateCollegeNames = function(items) {
                angular.forEach(items,
                    function(item) {
                        collegeQueryService.queryCollegeById(item.townId).then(function(college) {
                            item.name = college.name;
                        });
                    });
            };
            $scope.showChart = function (items) {
                $("#collegeFlowChart").highcharts(kpiDisplayService.generateCollegeFlowBarOptions(items));
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });