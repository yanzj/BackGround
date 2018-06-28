angular.module('topic.dialog.top',
        ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', 'topic.dialog.kpi', "ui.bootstrap"])
    .controller("rutrace.top",
        function($scope,
            $uibModalInstance,
            dialogTitle,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.kpiType = 'precise';

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopKpis(beginDate.value,
                    endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected).then(function(result) {
                    $scope.topCells = result;
                    angular.forEach(result,
                        function(cell) {
                            workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(items) {
                                if (items.length > 0) {
                                    for (var j = 0; j < $scope.topCells.length; j++) {
                                        if (items[0].eNodebId === $scope.topCells[j].cellId &&
                                            items[0].sectorId === $scope.topCells[j].sectorId) {
                                            $scope.topCells[j].hasWorkItems = true;
                                            break;
                                        }
                                    }
                                }
                            });
                        });
                });
            };
            $scope.initializeOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("rutrace.top.district",
        function($scope,
            $uibModalInstance,
            district,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = "TOP指标分析-" + district;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopKpisInDistrict($scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected,
                    $scope.city.selected,
                    district).then(function(result) {
                    $scope.topCells = result;
                    angular.forEach(result,
                        function(cell) {
                            workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(items) {
                                if (items.length > 0) {
                                    for (var j = 0; j < $scope.topCells.length; j++) {
                                        if (items[0].eNodebId === $scope.topCells[j].cellId &&
                                            items[0].sectorId === $scope.topCells[j].sectorId) {
                                            $scope.topCells[j].hasWorkItems = true;
                                            break;
                                        }
                                    }
                                }
                            });
                        });
                });
            };
            $scope.initializeOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("down.switch.top.district",
        function($scope,
            $uibModalInstance,
            district,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = "TOP指标分析-" + district;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.kpiType = 'downSwitch';

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopDownSwitchByPolicyInDistrict(beginDate.value,
                    endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected,
                    $scope.city.selected,
                    district).then(function(result) {
                    $scope.topCells = result;
                });
            };
            $scope.initializeDownSwitchOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });


            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('kpi.topDrop2G',
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            appFormatService,
            drop2GService,
            connection3GService,
            workItemDialog) {
            $scope.dialogTitle = dialogTitle;
            $scope.topData = {
                drop2G: [],
                connection3G: []
            };
            $scope.showKpi = function() {
                drop2GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.topData.drop2G = result.statViews;
                });
                connection3GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.topData.connection3G = result.statViews;
                });
            };
            $scope.showDropTrend = function() {
                workItemDialog.showTopDropTrend(city.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.topCount);
            };
            $scope.showConnectionTrend = function() {
                workItemDialog.showTopConnectionTrend(city.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.topCount);
            };
            $scope.$watch('city.selected',
                function(item) {
                    if (item) {
                        $scope.showKpi();
                    }
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });