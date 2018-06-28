angular.module('kpi.parameter.rutrace', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("rutrace.interference.analysis",
        function($scope,
            $uibModalInstance,
            cell,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService,
            neighborMongoService,
            networkElementService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "TOP指标干扰分析: " + $scope.currentCellName;
            $scope.oneAtATime = false;
            $scope.orderPolicy = topPreciseService.getOrderPolicySelection();
            $scope.updateMessages = [];

            networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(info) {
                $scope.current = {
                    cellId: cell.cellId,
                    sectorId: cell.sectorId,
                    eNodebName: cell.name,
                    longtitute: info.longtitute,
                    lattitute: info.lattitute
                };
            });

            $scope.showInterference = function() {
                $scope.interferenceCells = [];
                $scope.victimCells = [];

                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    angular.forEach(result,
                        function(interference) {
                            for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                                var neighbor = $scope.mongoNeighbors[i];
                                if (neighbor.neighborPci === interference.destPci) {
                                    interference.isMongoNeighbor = true;
                                    break;
                                }
                            }
                        });
                    $scope.interferenceCells = result;
                    preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(victims) {
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
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-over6db").highcharts(pieOptions.over6DbOption);
                    $("#interference-over10db").highcharts(pieOptions.over10DbOption);
                    $("#interference-mod3").highcharts(pieOptions.mod3Option);
                    $("#interference-mod6").highcharts(pieOptions.mod6Option);
                    topPreciseService.queryRsrpTa($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(info) {
                    });
                });
            };

            $scope.updateNeighborInfos = function() {
                preciseInterferenceService.updateInterferenceNeighbor(cell.cellId, cell.sectorId)
                    .then(function(result) {
                        $scope.updateMessages.push({
                            cellName: $scope.currentCellName,
                            counts: result,
                            type: "干扰"
                        });
                    });

                preciseInterferenceService.updateInterferenceVictim(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.updateMessages.push({
                        cellName: $scope.currentCellName,
                        counts: result,
                        type: "被干扰"
                    });
                });
            }

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(cell.cellId, cell.sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
                $scope.showInterference();
                $scope.updateNeighborInfos();
            });
        })
    .controller("rutrace.map.analysis",
        function($scope,
            $uibModalInstance,
            cell,
            dialogTitle,
            beginDate,
            endDate,
            baiduQueryService,
            baiduMapService,
            networkElementService,
            neighborDialogService,
            parametersDialogService,
            menuItemService,
            cellPreciseService,
            neighborMongoService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.neighborLines = [];
            $scope.displayNeighbors = false;
            $scope.reverseNeighborLines = [];
            $scope.displayReverseNeighbors = false;
            $scope.interferenceLines = [];
            $scope.interferenceCircles = [];
            $scope.displayInterference = false;
            $scope.victimLines = [];
            $scope.victimCircles = [];
            $scope.displayVictims = false;

            $scope.initializeMap = function() {
                cellPreciseService.queryOneWeekKpi(cell.cellId, cell.sectorId).then(function(cellView) {
                    baiduMapService.switchSubMap();
                    baiduMapService.initializeMap("all-map", 12);
                    networkElementService.queryCellSectors([cellView]).then(function(result) {
                        baiduQueryService.transformToBaidu(result[0].longtitute, result[0].lattitute)
                            .then(function(coors) {
                                var xOffset = coors.x - result[0].longtitute;
                                var yOffset = coors.y - result[0].lattitute;
                                result[0].longtitute = coors.x;
                                result[0].lattitute = coors.y;

                                var sectorTriangle = baiduMapService.generateSector(result[0], "blue", 1.25);
                                baiduMapService.addOneSectorToScope(sectorTriangle,
                                    neighborDialogService.showPrecise,
                                    result[0]);

                                baiduMapService.setCellFocus(result[0].longtitute, result[0].lattitute, 15);
                                var range = baiduMapService.getCurrentMapRange(-xOffset, -yOffset);

                                networkElementService.queryRangeSectors(range, []).then(function(sectors) {
                                    angular.forEach(sectors,
                                        function(sector) {
                                            sector.longtitute += xOffset;
                                            sector.lattitute += yOffset;
                                            baiduMapService.addOneSectorToScope(
                                                baiduMapService.generateSector(sector, "green"),
                                                function() {
                                                    parametersDialogService
                                                        .showCellInfo(sector, $scope.beginDate, $scope.endDate);
                                                },
                                                sector);
                                        });
                                });
                            });

                    });
                    networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(item) {
                        if (item) {
                            $scope.generateComponents(item);
                        }
                    });
                });
            };


            $scope.generateComponents = function(item) {
                baiduQueryService.transformToBaidu(item.longtitute, item.lattitute).then(function(coors) {
                    var xOffset = coors.x - item.longtitute;
                    var yOffset = coors.y - item.lattitute;
                    neighborMongoService.queryNeighbors(cell.cellId, cell.sectorId).then(function(neighbors) {
                        baiduMapService.generateNeighborLines($scope.neighborLines,
                        {
                            cell: item,
                            neighbors: neighbors,
                            xOffset: xOffset,
                            yOffset: yOffset
                        });
                    });
                    neighborMongoService.queryReverseNeighbors(cell.cellId, cell.sectorId).then(function(neighbors) {
                        baiduMapService.generateReverseNeighborLines($scope.reverseNeighborLines,
                        {
                            cell: item,
                            neighbors: neighbors,
                            xOffset: xOffset,
                            yOffset: yOffset
                        });
                    });
                    preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(interference) {
                        baiduMapService.generateInterferenceComponents(
                            $scope.interferenceLines,
                            $scope.interferenceCircles,
                            item,
                            interference,
                            xOffset,
                            yOffset,
                            "orange",
                            neighborDialogService.showInterferenceSource);
                    });
                    preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(victims) {
                        baiduMapService.generateVictimComponents($scope.victimLines,
                            $scope.victimCircles,
                            item,
                            victims,
                            xOffset,
                            yOffset,
                            "green",
                            neighborDialogService.showInterferenceVictim);
                    });
                });
            };

            $scope.toggleNeighbors = function() {
                if ($scope.displayNeighbors) {
                    baiduMapService.removeOverlays($scope.neighborLines);
                    $scope.displayNeighbors = false;
                } else {
                    baiduMapService.addOverlays($scope.neighborLines);
                    $scope.displayNeighbors = true;
                }
            };

            $scope.toggleReverseNeighbers = function() {
                if ($scope.displayReverseNeighbors) {
                    baiduMapService.removeOverlays($scope.reverseNeighborLines);
                    $scope.displayReverseNeighbors = false;
                } else {
                    baiduMapService.addOverlays($scope.reverseNeighborLines);
                    $scope.displayReverseNeighbors = true;
                }
            };

            $scope.toggleInterference = function() {
                if ($scope.displayInterference) {
                    baiduMapService.removeOverlays($scope.interferenceLines);
                    baiduMapService.removeOverlays($scope.interferenceCircles);
                    $scope.displayInterference = false;
                } else {
                    baiduMapService.addOverlays($scope.interferenceLines);
                    baiduMapService.addOverlays($scope.interferenceCircles);
                    $scope.displayInterference = true;
                }
            };

            $scope.toggleVictims = function() {
                if ($scope.displayVictims) {
                    baiduMapService.removeOverlays($scope.victimLines);
                    baiduMapService.removeOverlays($scope.victimCircles);
                    $scope.displayVictims = false;
                } else {
                    baiduMapService.addOverlays($scope.victimLines);
                    baiduMapService.addOverlays($scope.victimCircles);
                    $scope.displayVictims = true;
                }
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.initializeMap();
        })
    .controller("rutrace.coverage.analysis",
        function($scope,
            cell,
            $uibModalInstance,
            topPreciseService,
            preciseInterferenceService,
            preciseChartService,
            coverageService,
            kpiDisplayService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "TOP指标覆盖分析: " + $scope.currentCellName;
            $scope.orderPolicy = topPreciseService.getOrderPolicySelection();
            $scope.detailsDialogTitle = cell.name + "-" + cell.sectorId + "详细小区统计";
            $scope.cellId = cell.cellId;
            $scope.sectorId = cell.sectorId;
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    $scope.interferenceCells = result;
                    angular.forEach($scope.interferenceCells,
                        function(neighbor) {
                            if (neighbor.destENodebId > 0) {
                                kpiDisplayService.updateCoverageKpi(neighbor,
                                    {
                                        cellId: neighbor.destENodebId,
                                        sectorId: neighbor.destSectorId
                                    },
                                    {
                                        begin: $scope.beginDate.value,
                                        end: $scope.endDate.value
                                    });
                            }
                        });
                });
                preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    $scope.interferenceVictims = result;
                    angular.forEach($scope.interferenceVictims,
                        function(victim) {
                            if (victim.victimENodebId > 0) {
                                kpiDisplayService.updateCoverageKpi(victim,
                                    {
                                        cellId: victim.victimENodebId,
                                        sectorId: victim.victimSectorId
                                    },
                                    {
                                        begin: $scope.beginDate.value,
                                        end: $scope.endDate.value
                                    });
                            }
                        });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller("general.coverage.analysis",
        function($scope,
            cell,
            $uibModalInstance,
            topPreciseService,
            preciseInterferenceService,
            preciseChartService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "小区覆盖分析: " + $scope.currentCellName;
            $scope.detailsDialogTitle = cell.name + "-" + cell.sectorId + "详细小区统计";
            $scope.cellId = cell.cellId;
            $scope.sectorId = cell.sectorId;
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller('map.source.dialog',
        function($scope, $uibModalInstance, neighbor, dialogTitle, topPreciseService, preciseChartService) {
            $scope.neighbor = neighbor;
            $scope.dialogTitle = dialogTitle;
            if (neighbor.cellId !== undefined) {
                $scope.cellId = neighbor.cellId;
                $scope.sectorId = neighbor.sectorId;
            } else {
                $scope.cellId = neighbor.destENodebId;
                $scope.sectorId = neighbor.destSectorId;
            }
            topPreciseService.queryCoverage($scope.beginDate.value,
                $scope.endDate.value,
                $scope.cellId,
                $scope.sectorId).then(function(result) {
                var options = preciseChartService.getCoverageOptions(result);
                $("#coverage-chart").highcharts(options);
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("cell.info.dialog",
        function($scope, cell, dialogTitle, neighborMongoService, $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.isHuaweiCell = false;
            $scope.eNodebId = cell.eNodebId;
            $scope.sectorId = cell.sectorId;

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
            });

        });