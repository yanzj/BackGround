angular.module('rutrace.module', [
        'rutrace.top.cell',
        'rutrace.interference',
        'rutrace.analyze'
    ])
    .constant('htmlRoot', '/directives/rutrace/');

angular.module('rutrace.top.cell', ['app.format', 'myApp.kpi', 'myApp.region', 'topic.dialog'])
    .controller('topCellController', function ($scope,
        appFormatService,
        workitemService,
        networkElementService,
        neighborDialogService,
        workItemDialog) {
        $scope.gridOptions = {
            columnDefs: [
                appFormatService.generateLteCellNameDef(),
                {
                    field: 'totalMrs',
                    name: 'MRO总数'
                },
                appFormatService.generatePreciseRateDef(0),
                appFormatService.generatePreciseRateDef(1),
                appFormatService.generatePreciseRateDef(2),
                { field: 'topDates', name: 'TOP天数' },
                {
                    name: '分析',
                    width: 150,
                    cellTemplate: '<div class="btn-group-xs">\
                            <button class="btn btn-default btn-xs" ng-click="grid.appScope.showCellTrend(row.entity)">\
                                <i class="glyphicon glyphicon-stats" title="单小区按日期的变化趋势"></i>\
                                趋势\
                            </button>\
                            <button class="btn btn-default btn-xs" ng-click="grid.appScope.dump(row.entity)">\
                                <i class="glyphicon glyphicon-cloud-download" title="监控导入"></i>\
                                导入\
                            </button>\
                        </div>'
                },
                {
                    name: '指标',
                    width: 150,
                    cellTemplate: '<div class="btn-group-xs">\
                            <button class="btn btn-warning btn-xs" ng-click="grid.appScope.showInterference(row.entity)">\
                                <i class="glyphicon glyphicon-fullscreen" title="干扰信息"></i>\
                                干扰\
                            </button>\
                            <button class="btn btn-success btn-xs" ng-click="grid.appScope.showCoverage(row.entity)">\
                                <i class="glyphicon glyphicon-tree-conifer" title="覆盖信息"></i>\
                                覆盖\
                            </button>\
                        </div>'
                },
                {
                    name: '处理',
                    width: 160,
                    cellTemplate: '<div class="btn-group-xs">\
                            <button class="btn btn-default btn-xs" ng-click="grid.appScope.showMap(row.entity)">\
                                <i class="glyphicon glyphicon-globe"></i>\
                                地理化\
                            </button>\
                            <button class="btn btn-primary btn-xs" ng-click="grid.appScope.createWorkitem(row.entity)">\
                                建单\
                            </button>\
                            <button class="btn btn-default" ng-show="row.entity.hasWorkItems === true"\
                                ng-click="grid.appScope.processWorkItems(row.entity)">\
                                <i class="glyphicon glyphicon-dashboard" title="工单处理"></i>\
                                处理\
                            </button>\
                        </div>'
                }
            ],
            data: []
        };
        $scope.createWorkitem = function(cell) {
            workitemService.constructPreciseItem(cell, $scope.beginDate.value, $scope.endDate.value)
                .then(function(result) {
                    if (result) {
                        $scope.updateMessages.push({
                            cellName: result
                        });
                        cell.hasWorkItems = true;
                    }
                });
        };
        $scope.dump = function(cell) {
            networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(info) {
                neighborDialogService.dumpCellMongo({
                        eNodebId: cell.cellId,
                        sectorId: cell.sectorId,
                        pci: info.pci,
                        name: cell.eNodebName
                    },
                    $scope.beginDate.value,
                    $scope.endDate.value);
            });
        };
        $scope.showInterference = function(cell) {
            neighborDialogService.showRutraceInterference({
                    cellId: cell.cellId,
                    sectorId: cell.sectorId,
                    name: cell.eNodebName
                },
                $scope.beginDate.value,
                $scope.endDate.value);
        };
        $scope.showCoverage = function(cell) {
            neighborDialogService.showRutraceCoverage({
                    cellId: cell.cellId,
                    sectorId: cell.sectorId,
                    name: cell.eNodebName
                },
                $scope.beginDate.value,
                $scope.endDate.value);
        };
        $scope.showMap = function(cell) {
            neighborDialogService.showRutraceInterferenceMap({
                    cellId: cell.cellId,
                    sectorId: cell.sectorId,
                    name: cell.eNodebName
                },
                $scope.beginDate,
                $scope.endDate);
        };
        $scope.showCellTrend = function(cell) {
            workItemDialog.showPreciseCellTrend(cell.eNodebName + "-" + cell.sectorId, cell.cellId, cell.sectorId);
        };
        $scope.processWorkItems = function(cell) {
            workItemDialog.processPreciseWorkItem(cell, $scope.beginDate, $scope.endDate);
        };
    })
    .directive('topCell', function ($compile, calculateService) {
        return calculateService.generateGridDirective({
                controllerName: 'topCellController',
                scope: {
                    topCells: '=',
                    beginDate: '=',
                    endDate: '=',
                    updateMessages: '='
                },
                argumentName: 'topCells'
            },
            $compile);
    })
    .controller('topDownSwitchController', function ($scope, appFormatService, workItemDialog, neighborDialogService) {
            $scope.gridOptions = {
                columnDefs: [
                    appFormatService.generateLteCellNameDef(),
                    {
                        field: 'redirectCdma2000',
                        name: '下切总次数'
                    },
                    {
                        field: 'downSwitchRate',
                        name: '下切比例（次/GB）',
                        cellFilter: 'number: 2'
                    },
                    {
                        field: 'pdcpDownlinkFlow',
                        name: '下行流量（MByte）',
                        cellFilter: 'bitToByte'
                    },
                    {
                        field: 'pdcpUplinkFlow',
                        name: '上行流量（MByte）',
                        cellFilter: 'bitToByte'
                    },
                    {
                        field: 'maxUsers',
                        name: '最大用户数'
                    },
                    {
                        field: 'downlinkFeelingRate',
                        name: '下行感知速率（Mbit/s）',
                        cellFilter: 'number: 2'
                    },
                    {
                        field: 'uplinkFeelingRate',
                        name: '上行感知速率（Mbit/s）',
                        cellFilter: 'number: 2'
                    },
                    {
                        field: 'rank2Rate',
                        name: '双流比（%）',
                        cellFilter: 'number: 2'
                    },
                    {
                        name: '分析',
                        width: 150,
                        cellTemplate: '<div class="btn-group-xs">\
                            <button class="btn btn-default btn-xs" ng-click="grid.appScope.showCellTrend(row.entity)">\
                                <i class="glyphicon glyphicon-stats" title="单小区按日期的变化趋势"></i>\
                                趋势\
                            </button>\
                            <button class="btn btn-success btn-xs" ng-click="grid.appScope.showCoverage(row.entity)">\
                                <i class="glyphicon glyphicon-tree-conifer" title="覆盖信息"></i>\
                                覆盖\
                            </button>\
                        </div>'
                    }
                ]
            };
            $scope.showCellTrend = function(cell) {
                workItemDialog.showDownSwitchCellTrend(cell.eNodebName + "-" + cell.sectorId, cell.eNodebId, cell.sectorId);
            };
            $scope.showCoverage = function(cell) {
                neighborDialogService.showGeneralCoverage({
                        cellId: cell.eNodebId,
                        sectorId: cell.sectorId,
                        name: cell.eNodebName
                    },
                    $scope.beginDate.value,
                    $scope.endDate.value);
            };
        })
    .directive('topDownSwitch', function ($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'topDownSwitchController',
                    scope: {
                        topCells: '=',
                        beginDate: '=',
                        endDate: '=',
                        updateMessages: '='
                    },
                    argumentName: 'topCells'
                },
                $compile);
        })

    .controller('topCqiController', function ($scope, appFormatService, workItemDialog, neighborDialogService) {
            $scope.gridOptions = {
                columnDefs: [
                    appFormatService.generateLteCellNameDef(),
                    {
                        field: 'cqiCounts.item1',
                        name: 'CQI总调度次数'
                    },
                    {
                        field: 'cqiCounts.item2',
                        name: 'CQI优良次数'
                    },
                    {
                        field: 'cqiRate',
                        name: 'CQI优良比（%）',
                        cellFilter: 'number: 2'
                    },
                    {
                        name: '分析',
                        width: 150,
                        cellTemplate: '<div class="btn-group-xs">\
                            <button class="btn btn-default btn-xs" ng-click="grid.appScope.showCellTrend(row.entity)">\
                                <i class="glyphicon glyphicon-stats" title="单小区按日期的变化趋势"></i>\
                                趋势\
                            </button>\
                            <button class="btn btn-success btn-xs" ng-click="grid.appScope.showCoverage(row.entity)">\
                                <i class="glyphicon glyphicon-tree-conifer" title="覆盖信息"></i>\
                                覆盖\
                            </button>\
                        </div>'
                    }
                ]
            };

            $scope.showCellTrend = function (cell) {
                workItemDialog.showDownSwitchCellTrend(cell.eNodebName + "-" + cell.sectorId, cell.eNodebId, cell.sectorId);
            };
            $scope.showCoverage = function (cell) {
                neighborDialogService.showGeneralCoverage({
                    cellId: cell.eNodebId,
                    sectorId: cell.sectorId,
                    name: cell.eNodebName
                },
                    $scope.beginDate.value,
                    $scope.endDate.value);
            };
    })
    .directive('topCqi', function ($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'topCqiController',
                    scope: {
                        topCells: '=',
                        beginDate: '=',
                        endDate: '=',
                        updateMessages: '='
                    },
                    argumentName: 'topCells'
                },
                $compile);
        })
    .controller('DistrictStatController', function ($scope, mapDialogService, calculateService) {
        $scope.cityFlag = '全网';
        $scope.gridOptions = {
            columnDefs: calculateService.generateDistrictColumnDefs($scope.kpiType),
            data: []
        };
        $scope.showWorkItemDistrict = function (district) {
            mapDialogService.showPreciseWorkItemDistrict(district, $scope.endDate);
        };
        $scope.showTopDistrict = function (district) {
            switch ($scope.kpiType) {
            case 'precise':
                mapDialogService.showPreciseTopDistrict($scope.beginDate, $scope.endDate, district);
                break;
            case 'downSwitch':
                mapDialogService.showDownSwitchTopDistrict($scope.beginDate, $scope.endDate, district);
                break;
            }
        };
    })
    .directive('districtStatTable', function ($compile, calculateService) {
        return calculateService.generateGridDirective({
            controllerName: 'DistrictStatController',
            scope: {
                overallStat: '=',
                beginDate: '=',
                endDate: '=',
                kpiType: '='
            },
            argumentName: 'overallStat.districtStats'
        }, $compile);
    })

    .controller('TownStatController', function ($scope, calculateService) {
        $scope.gridOptions = {
            columnDefs: calculateService.generateTownColumnDefs($scope.kpiType),
            data: []
        };
    })
    .directive('townStatTable', function ($compile, filterFilter) {
        return {
            controller: 'TownStatController',
            restrict: 'EA',
            replace: true,
            scope: {
                overallStat: '=',
                kpiType: '='
            },
            template: '<div></div>',
            link: function (scope, element, attrs) {
                scope.initialize = false;
                scope.$watch('overallStat.townStats', function (townStats) {
                    scope.gridOptions.data = filterFilter(townStats, { district: scope.overallStat.currentDistrict });

                    if (!scope.initialize) {
                        var linkDom = $compile('<div ui-grid="gridOptions"></div>')(scope);
                        element.append(linkDom);
                        scope.initialize = true;
                    }
                });
                scope.$watch('overallStat.currentDistrict', function (currentDistrict) {
                    scope.gridOptions.data = filterFilter(scope.overallStat.townStats, { district: currentDistrict });

                    if (!scope.initialize) {
                        var linkDom = $compile('<div ui-grid="gridOptions"></div>')(scope);
                        element.append(linkDom);
                        scope.initialize = true;
                    }
                });
            }
        };
    })
;

angular.module('rutrace.interference', ['myApp.region'])
    .directive('interferenceSourceDialogList', function(htmlRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                interferenceCells: '=',
                orderPolicy: '=',
                displayItems: '='
            },
            templateUrl: htmlRoot + 'interference/SourceDialogList.html'
        };
    })
    .directive('interferenceSourceCoverageList', function(htmlRoot, coverageDialogService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                interferenceCells: '=',
                orderPolicy: '='
            },
            templateUrl: htmlRoot + 'coverage/InterferenceSourceList.html',
            link: function(scope, element, attrs) {
                scope.analyzeTa = function(cell) {
                    coverageDialogService.showDetails(cell.neighborCellName, cell.destENodebId, cell.destSectorId);
                };
            }
        };
    })
    .directive('interferenceSourceList', function (htmlRoot, neighborDialogService, networkElementService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                interferenceCells: '=',
                orderPolicy: '=',
                current: '='
            },
            templateUrl: htmlRoot + 'interference/SourceList.html',
            link: function(scope, element, attrs) {
                scope.match = function(candidate) {
                    var center = scope.current;
                    networkElementService
                        .queryNearestCellsWithFrequency(center.cellId,
                            center.sectorId,
                            candidate.destPci,
                            candidate.neighborEarfcn).then(function(neighbors) {
                            neighborDialogService.matchNeighbor(center, candidate, neighbors);
                        });
                };
            }
        };
    })
    .directive('interferenceVictimList', function(htmlRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                victimCells: '=',
                orderPolicy: '='
            },
            templateUrl: htmlRoot + 'interference/VictimList.html'
        };
    })
    .directive('interferenceVictimDialogList', function(htmlRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                victimCells: '=',
                orderPolicy: '=',
                displayItems: '='
            },
            templateUrl: htmlRoot + 'interference/VictimDialogList.html'
        };
    })
    .directive('interferenceVictimCoverageList', function(htmlRoot, coverageDialogService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                victimCells: '=',
                orderPolicy: '='
            },
            templateUrl: htmlRoot + 'coverage/InterferenceVictimList.html',
            link: function(scope, element, attrs) {
                scope.analyzeTa = function(cell) {
                    coverageDialogService.showDetails(cell.victimCellName, cell.victimENodebId, cell.victimSectorId);
                };
            }
        };
    })
    .directive('mroInterferenceTable', function (htmlRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentDetails: '='
            },
            templateUrl: htmlRoot + 'interference/MroInterferenceList.html'
        };
    })
    .directive('mroRsrpTable', function (htmlRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentDetails: '='
            },
            templateUrl: htmlRoot + 'interference/MroRsrpList.html'
        };
    })
    .directive('mroTaTable', function (htmlRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentDetails: '='
            },
            templateUrl: htmlRoot + 'interference/MroTaList.html'
        };
    })
    .directive('mrsRsrpTable', function (htmlRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentDetails: '='
            },
            templateUrl: htmlRoot + 'interference/MrsRsrpList.html',
            link: function (scope, element, attrs) {
                scope.indices1 = _.range(16);
                scope.indices2 = _.range(16, 32);
                scope.indices3 = _.range(32, 48);
            }
        };
    })
    .directive('mrsTaTable', function (htmlRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentDetails: '='
            },
            templateUrl: htmlRoot + 'interference/MrsTaList.html',
            link: function (scope, element, attrs) {
                scope.indices1 = _.range(16);
                scope.indices2 = _.range(16, 32);
            }
        };
    });

angular.module('rutrace.trend', ['kpi.parameter', 'region.network'])
    .directive('dumpForwardNeighbors', function(htmlRoot, neighborDialogService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                neighborCells: '=',
                beginDate: '=',
                endDate: '='
            },
            templateUrl: htmlRoot + 'import/ForwardNeighbors.html',
            link: function(scope, element, attrs) {
                scope.dumpMongo = function(cell) {
                    neighborDialogService.dumpCellMongo({
                        eNodebId: cell.nearestCellId,
                        sectorId: cell.nearestSectorId,
                        pci: cell.pci,
                        name: cell.nearestENodebName
                    }, scope.beginDate.value, scope.endDate.value);
                };
            }
        };
    })
    .directive('dumpBackwardNeighbors', function(htmlRoot, dumpPreciseService) {
        var dumpSingleCell = function(cell, begin, end) {
            dumpPreciseService.dumpDateSpanSingleNeighborRecords(cell, begin, end);
        };
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                neighborCells: '=',
                beginDate: '=',
                endDate: '='
            },
            templateUrl: htmlRoot + 'import/BackwardNeighbors.html',
            link: function (scope, element, attrs) {
                scope.dumpAll = function() {
                    angular.forEach(scope.neighborCells, function(cell) {
                        dumpSingleCell(cell, new Date(scope.beginDate.value), new Date(scope.endDate.value));
                    });
                };
            }
        };
    })
    .directive('mongoNeighborList', function(htmlRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                mongoNeighbors: '='
            },
            templateUrl: htmlRoot + 'interference/MongoNeighbors.html'
        };
    });

angular.module('rutrace.analyze', ['myApp.kpi', 'myApp.region'])
    .controller('analyzeTableController', function($scope,
        preciseWorkItemGenerator,
        preciseWorkItemService,
        coverageDialogService) {
        $scope.analyzeInterferenceSource = function() {
            coverageDialogService.showSource($scope.currentView,
                $scope.serialNumber,
                $scope.beginDate,
                $scope.endDate,
                function(info) {
                    scope.interferenceSourceComments = '已完成干扰源分析';
                    var dtos = preciseWorkItemGenerator.generatePreciseInterferenceNeighborDtos(info);
                    preciseWorkItemService.updateInterferenceNeighbor($scope.serialNumber, dtos).then(function(result) {
                        $scope.interferenceSourceComments += ";已导入干扰源分析结果" ;
                        $scope.queryPreciseCells();
                    });
                });
        };
        $scope.showSourceDbChart = function() {
            coverageDialogService.showSourceDbChart($scope.currentView);
        };
        $scope.showSourceModChart = function() {
            coverageDialogService.showSourceModChart($scope.currentView,
                function(info) {
                    $scope.interferenceSourceComments = info;
                });
        };
        $scope.showSourceStrengthChart = function() {
            coverageDialogService.showSourceStrengthChart($scope.currentView,
                function(info) {
                    $scope.interferenceSourceComments = info;
                });
        };
        $scope.analyzeInterferenceVictim = function() {
            coverageDialogService.showInterferenceVictim($scope.currentView,
                $scope.serialNumber,
                function(info) {
                    scope.interferenceVictimComments = '已完成被干扰小区分析';
                    var dtos = preciseWorkItemGenerator.generatePreciseInterferenceVictimDtos(info);
                    preciseWorkItemService.updateInterferenceVictim($scope.serialNumber, dtos).then(function(result) {
                        $scope.interferenceVictimComments += ";已导入被干扰小区分析结果";
                        $scope.queryPreciseCells();
                    });
                });
        };
        $scope.analyzeCoverage = function() {
            coverageDialogService.showCoverage($scope.currentView,
                $scope.preciseCells,
                function(info) {
                    $scope.coverageComments = '已完成覆盖分析';
                    preciseWorkItemService.updateCoverage($scope.serialNumber, info).then(function(result) {
                        $scope.coverageComments += ";已导入覆盖分析结果";
                        $scope.queryPreciseCells();
                    });
                });
        };
    })
    .directive('analyzeTable', function (htmlRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                currentView: '=',
                serialNumber: '=',
                queryPreciseCells: '&',
                preciseCells: '=',
                beginDate: '=',
                endDate: '='
            },
            templateUrl: htmlRoot + 'AnalyzeTable.Tpl.html'
        };
    });