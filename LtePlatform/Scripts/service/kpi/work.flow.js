angular.module('kpi.work.flow', ['myApp.url', 'myApp.region', "ui.bootstrap", "kpi.core"])
    .controller("eNodeb.flow",
        function($scope,
            $uibModalInstance,
            eNodeb,
            beginDate,
            endDate,
            networkElementService,
            appKpiService,
            kpiChartService) {
            $scope.eNodebName = eNodeb.name;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.queryFlow = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };

            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, $scope.eNodebName, $scope.mergeStats);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            networkElementService.queryCellViewsInOneENodeb(eNodeb.eNodebId).then(function(result) {
                $scope.cellList = result;
                $scope.queryFlow();
            });
        })
    .controller("hotSpot.cell.flow",
        function($scope,
            $uibModalInstance,
            hotSpot,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.eNodebName = hotSpot.hotspotName;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.queryFlow = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };

            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, $scope.eNodebName, $scope.mergeStats);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            complainService.queryHotSpotCells(hotSpot.hotspotName).then(function(result) {
                $scope.cellList = result;
                $scope.queryFlow();
            });
        })
    .controller("topic.cells",
        function($scope, $uibModalInstance, dialogTitle, name, complainService) {
            $scope.dialogTitle = dialogTitle;
            complainService.queryHotSpotCells(name).then(function(existedCells) {
                $scope.cellList = existedCells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });