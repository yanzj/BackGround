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
        })
    .controller('interference.source.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            preciseInterferenceService,
            neighborMongoService) {
            $scope.dialogTitle = dialogTitle;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            var options = [
                {
                    name: "模3干扰数",
                    value: "mod3Interferences"
                }, {
                    name: "模6干扰数",
                    value: "mod6Interferences"
                }, {
                    name: "6dB干扰数",
                    value: "overInterferences6Db"
                }, {
                    name: "10dB干扰数",
                    value: "overInterferences10Db"
                }, {
                    name: "总干扰水平",
                    value: "interferenceLevel"
                }
            ];
            $scope.orderPolicy = {
                options: options,
                selected: options[4].value
            };
            $scope.displayItems = {
                options: [5, 10, 15, 20, 30],
                selected: 10
            };

            $scope.showInterference = function() {
                $scope.interferenceCells = [];

                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    angular.forEach(result,
                        function(cell) {
                            for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                                var neighbor = $scope.mongoNeighbors[i];
                                if (neighbor.neighborPci === cell.destPci) {
                                    cell.isMongoNeighbor = true;
                                    break;
                                }
                            }
                        });
                    $scope.interferenceCells = result;
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(eNodebId, sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
                $scope.showInterference();
            });
        })
    .controller('interference.source.db.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-over6db").highcharts(pieOptions.over6DbOption);
                    $("#interference-over10db").highcharts(pieOptions.over10DbOption);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.source.mod.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-mod3").highcharts(pieOptions.mod3Option);
                    $("#interference-mod6").highcharts(pieOptions.mod6Option);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.source.strength.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService,
            neighborMongoService,
            networkElementService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    networkElementService.queryCellInfo(eNodebId, sectorId).then(function(info) {
                        topPreciseService.queryCellStastic(eNodebId,
                            info.pci,
                            $scope.beginDate.value,
                            $scope.endDate.value).then(function(stastic) {
                            var columnOptions = kpiDisplayService.getStrengthColumnOptions(result,
                                stastic.mrCount,
                                $scope.currentCellName);
                            $("#strength-over6db").highcharts(columnOptions.over6DbOption);
                            $("#strength-over10db").highcharts(columnOptions.over10DbOption);
                        });
                    });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.victim.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            topPreciseService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            var options = [
                {
                    name: "模3干扰数",
                    value: "mod3Interferences"
                }, {
                    name: "模6干扰数",
                    value: "mod6Interferences"
                }, {
                    name: "6dB干扰数",
                    value: "overInterferences6Db"
                }, {
                    name: "10dB干扰数",
                    value: "overInterferences10Db"
                }, {
                    name: "总干扰水平",
                    value: "interferenceLevel"
                }
            ];
            $scope.orderPolicy = {
                options: options,
                selected: options[4].value
            };
            $scope.displayItems = {
                options: [5, 10, 15, 20, 30],
                selected: 10
            };

            $scope.showVictim = function() {
                $scope.victimCells = [];

                preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(victims) {
                    preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                        $scope.endDate.value,
                        eNodebId,
                        sectorId).then(function(result) {
                        angular.forEach(victims,
                            function(victim) {
                                for (var j = 0; j < result.length; j++) {
                                    if (result[j].destENodebId === victim.victimENodebId &&
                                        result[j].destSectorId === victim.victimSectorId) {
                                        victim.forwardInterferences6Db = result[j].overInterferences6Db;
                                        victim.forwardInterferences10Db = result[j].overInterferences10Db;
                                        break;
                                    }
                                }
                            });
                        $scope.victimCells = victims;
                    });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.victimCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showVictim();
        })
    .controller('interference.coverage.dialog',
        function($scope) {

        });