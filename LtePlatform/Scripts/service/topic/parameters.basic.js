angular.module('topic.parameters.basic', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('map.eNodeb.dialog',
        function($scope,
            $uibModalInstance,
            eNodeb,
            dialogTitle,
            networkElementService,
            cellHuaweiMongoService,
            alarmImportService,
            intraFreqHoService,
            interFreqHoService,
            appFormatService,
            downSwitchService,
            alarmsService,
            appRegionService) {
            $scope.dialogTitle = dialogTitle;
            $scope.alarmLevel = {
                options: ["严重告警", "重要以上告警", "所有告警"],
                selected: "重要以上告警"
            };
            $scope.alarms = [];
            $scope.searchAlarms = function() {
                alarmsService.queryENodebAlarmsByDateSpanAndLevel(eNodeb.eNodebId,
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.alarmLevel.selected).then(function(result) {
                    $scope.alarms = result;
                });
            };

            $scope.searchAlarms();

            networkElementService.queryENodebInfo(eNodeb.eNodebId).then(function(result) {
                appRegionService.isInTownBoundary(result.longtitute,
                    result.lattitute,
                    result.cityName,
                    result.districtName,
                    result.townName).then(function(conclusion) {
                    var color = conclusion ? 'green' : 'red';
                    $scope.eNodebGroups = appFormatService.generateENodebGroups(result, color);
                });
                networkElementService.queryStationByENodeb(eNodeb.eNodebId, eNodeb.planNum).then(function(dict) {
                    if (dict) {
                        downSwitchService.getStationByStationId(dict.stationNum).then(function(stations) {
                            stations.result[0].Town = result.townName;
                            $scope.stationGroups = appFormatService.generateStationGroups(stations.result[0]);
                        });
                    }

                });
                if (result.factory === '华为') {
                    cellHuaweiMongoService.queryLocalCellDef(result.eNodebId).then(function(cellDef) {
                        alarmImportService.updateHuaweiAlarmInfos(cellDef).then(function() {});
                    });
                }
            });

            //查询该基站下带的小区列表
            networkElementService.queryCellViewsInOneENodeb(eNodeb.eNodebId).then(function(result) {
                $scope.cellList = result;
            });

            //查询基站同频切换参数
            intraFreqHoService.queryENodebParameters(eNodeb.eNodebId).then(function(result) {
                $scope.intraFreqHo = result;
            });

            //查询基站异频切换参数
            interFreqHoService.queryENodebParameters(eNodeb.eNodebId).then(function(result) {
                $scope.interFreqHo = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.neighbor.dialog',
        function($scope,
            $uibModalInstance,
            interFreqHoService,
            neighborDialogService,
            flowService,
            kpiChartCalculateService,
            neighbor,
            dialogTitle,
            beginDate,
            endDate) {
            $scope.neighbor = neighbor;
            $scope.eNodebId = neighbor.otherInfos.split(': ')[5];
            $scope.sectorId = neighbor.cellName.split('-')[1];
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.parameter = {
                options: [
                    '基本参数', '同频切换', '异频切换'
                ],
                selected: '基本参数'
            };

            $scope.dump = function() {
                neighborDialogService.dumpCellMongo({
                        eNodebId: $scope.eNodebId,
                        sectorId: $scope.sectorId,
                        pci: neighbor.pci,
                        name: neighbor.cellName.split('-')[0]
                    },
                    $scope.beginDate.value,
                    $scope.endDate.value);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.queryFlow = function() {
                flowService.queryCellFlowByDateSpan($scope.eNodebId,
                    $scope.sectorId,
                    $scope.beginDate.value,
                    $scope.endDate.value).then(function(results) {
                        $("#flowChart").highcharts(kpiChartCalculateService.generateMergeFlowOptions(results, neighbor.cellName));
                        $("#usersChart").highcharts(kpiChartCalculateService.generateMergeUsersOptions(results, neighbor.cellName));
                });
            };

            interFreqHoService.queryCellParameters($scope.eNodebId, $scope.sectorId).then(function(result) {
                $scope.interFreqHo = result;
                $scope.queryFlow();
            });
        })
    .controller('map.cdma.cell.dialog',
        function($scope,
            $uibModalInstance,
            neighbor,
            dialogTitle,
            networkElementService) {
            $scope.cdmaCellDetails = neighbor;
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };
            networkElementService.queryCdmaCellInfo(neighbor.btsId, neighbor.sectorId).then(function(result) {
                angular.extend($scope.cdmaCellDetails, result);
            });
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.bts.dialog',
        function($scope, $uibModalInstance, bts, dialogTitle, networkElementService) {
            $scope.bts = bts;
            $scope.dialogTitle = dialogTitle;

            networkElementService.queryBtsInfo(bts.btsId).then(function(result) {
                $scope.btsDetails = result;
            });
            networkElementService.queryCdmaCellViews(bts.name).then(function(result) {
                $scope.cdmaCellList = result;
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });