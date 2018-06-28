angular.module('kpi.parameter.query', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('query.setting.dialog',
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            beginDate,
            endDate,
            appRegionService,
            parametersMapService,
            parametersDialogService,
            networkElementService) {
            $scope.network = {
                options: ["LTE", "CDMA"],
                selected: "LTE"
            };
            $scope.queryText = "";
            $scope.city = city;
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.eNodebList = [];
            $scope.btsList = [];

            $scope.updateDistricts = function() {
                appRegionService.queryDistricts($scope.city.selected).then(function(result) {
                    $scope.district.options = result;
                    $scope.district.selected = result[0];
                });
            };
            $scope.updateTowns = function() {
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(result) {
                    $scope.town.options = result;
                    $scope.town.selected = result[0];
                });
            };

            $scope.queryItems = function() {
                if ($scope.network.selected === "LTE") {
                    if ($scope.queryText.trim() === "") {
                        networkElementService
                            .queryENodebsInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                            .then(function(eNodebs) {
                                $scope.eNodebList = eNodebs;
                            });
                    } else {
                        networkElementService.queryENodebsByGeneralName($scope.queryText).then(function(eNodebs) {
                            $scope.eNodebList = eNodebs;
                        });
                    }
                } else {
                    if ($scope.queryText.trim() === "") {
                        networkElementService
                            .queryBtssInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                            .then(function(btss) {
                                $scope.btsList = btss;
                            });
                    } else {
                        networkElementService.queryBtssByGeneralName($scope.queryText).then(function(btss) {
                            $scope.btsList = btss;
                        });
                    }
                }
            };
            appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                $scope.district = {
                    options: districts,
                    selected: districts[0]
                };
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town = {
                        options: towns,
                        selected: towns[0]
                    };
                });
            });
            $scope.ok = function() {
                if ($scope.network.selected === "LTE") {
                    if ($scope.queryText.trim() === "") {
                        parametersMapService.showElementsInOneTown($scope.city.selected,
                            $scope.district.selected,
                            $scope.town.selected,
                            $scope.beginDate,
                            $scope.endDate);
                    } else {
                        parametersMapService
                            .showElementsWithGeneralName($scope.queryText, $scope.beginDate, $scope.endDate);
                    }
                } else {
                    if ($scope.queryText.trim() === "") {
                        parametersMapService
                            .showCdmaInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected);
                    } else {
                        parametersMapService.showCdmaWithGeneralName($scope.queryText);
                    }
                }
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("parameters.list",
        function($scope, $uibModalInstance, city, dialogTitle, appRegionService, parametersChartService) {
            $scope.city = city;
            $scope.dialogTitle = dialogTitle;
            $scope.showCityStats = function() {
                appRegionService.queryDistrictInfrastructures($scope.city.selected).then(function(result) {
                    appRegionService.accumulateCityStat(result, $scope.city.selected);
                    $scope.districtStats = result;

                    $("#cityLteENodebConfig").highcharts(parametersChartService.getDistrictLteENodebPieOptions(result
                        .slice(0, result.length - 1),
                        $scope.city.selected));
                    $("#cityLteCellConfig").highcharts(parametersChartService.getDistrictLteCellPieOptions(result
                        .slice(0, result.length - 1),
                        $scope.city.selected));
                    $("#cityNbIotCellConfig").highcharts(parametersChartService.getDistrictNbIotCellPieOptions(result
                        .slice(0, result.length - 1),
                        $scope.city.selected));
                    $("#cityCdmaENodebConfig").highcharts(parametersChartService.getDistrictCdmaBtsPieOptions(result
                        .slice(0, result.length - 1),
                        $scope.city.selected));
                    $("#cityCdmaCellConfig").highcharts(parametersChartService.getDistrictCdmaCellPieOptions(result
                        .slice(0, result.length - 1),
                        $scope.city.selected));
                });
            };
            $scope.$watch('currentDistrict',
                function(district) {
                    appRegionService.queryTownInfrastructures($scope.city.selected, district).then(function(result) {
                        $scope.townStats = result;
                        $("#districtLteENodebConfig")
                            .highcharts(parametersChartService.getTownLteENodebPieOptions(result, district));
                        $("#districtLteCellConfig")
                            .highcharts(parametersChartService.getTownLteCellPieOptions(result, district));
                        $("#districtNbIotCellConfig")
                            .highcharts(parametersChartService.getTownNbIotCellPieOptions(result, district));
                        $("#districtCdmaENodebConfig")
                            .highcharts(parametersChartService.getTownCdmaBtsPieOptions(result, district));
                        $("#districtCdmaCellConfig")
                            .highcharts(parametersChartService.getTownCdmaCellPieOptions(result, district));
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCityStats();
        })
    .controller('cell.type.chart',
        function($scope, $uibModalInstance, city, dialogTitle, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            appRegionService.queryDistrictIndoorCells(city.selected).then(function(stats) {
                $("#leftChart").highcharts(parametersChartService.getCellIndoorTypeColumnOptions(stats));
            });
            appRegionService.queryDistrictBandCells(city.selected).then(function(stats) {
                $("#rightChart").highcharts(parametersChartService.getCellBandClassColumnOptions(stats));
            });
        })
    .controller("flow.kpi.dialog",
        function($scope,
            cell,
            begin,
            end,
            dialogTitle,
            flowService,
            generalChartService,
            calculateService,
            parametersChartService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.itemGroups = calculateService.generateFlowDetailsGroups(cell);
            flowService.queryAverageRrcByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                $scope.rrcGroups = calculateService.generateRrcDetailsGroups(result);
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            flowService.queryCellFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#flowChart").highcharts(parametersChartService.getCellFlowOptions(dates, result));
                $("#feelingRateChart").highcharts(parametersChartService.getCellFeelingRateOptions(dates, result));
                $("#downSwitchChart").highcharts(parametersChartService.getCellDownSwitchOptions(dates, result));
                $("#rank2Chart").highcharts(parametersChartService.getCellRank2Options(dates, result));
            });
        })
    .controller("rrc.kpi.dialog",
        function($scope,
            cell,
            begin,
            end,
            dialogTitle,
            flowService,
            generalChartService,
            calculateService,
            parametersChartService,
            networkElementService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.rrcGroups = calculateService.generateRrcDetailsGroups(cell);
            flowService.queryAverageFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                networkElementService.queryCellInfo(cell.eNodebId, cell.sectorId).then(function(item) {
                    $scope.itemGroups = calculateService.generateFlowDetailsGroups(angular.extend(result, item));
                });
            });

            flowService.queryCellRrcByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#rrcRequestChart").highcharts(parametersChartService.getCellRrcRequestOptions(dates, result));
                $("#rrcFailChart").highcharts(parametersChartService.getCellRrcFailOptions(dates, result));
                $("#rrcRateChart").highcharts(parametersChartService.getCellRrcRateOptions(dates, result));
            });
            flowService.queryCellFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#flowChart").highcharts(parametersChartService.getCellFlowUsersOptions(dates, result));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });