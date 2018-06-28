angular.module('kpi.parameter', ['app.menu', 'app.core', 'region.network'])
    .factory('neighborDialogService',
        function(menuItemService, networkElementService, stationFormatService, baiduMapService) {
            return {
                dumpCellMongo: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Interference/DumpCellMongoDialog.html',
                        controller: 'dump.cell.mongo',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "干扰数据导入";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showRutraceInterference: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Interference/Index.html',
                        controller: 'rutrace.interference.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "干扰指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showRutraceInterferenceMap: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Rutrace/Map/Index.html',
                            controller: 'rutrace.map.analysis',
                            resolve: stationFormatService.dateSpanDateResolve({
                                    dialogTitle: function() {
                                        return "小区地理化分析" + ": " + cell.name + "-" + cell.sectorId;
                                    },
                                    cell: function() {
                                        return cell;
                                    }
                                },
                                beginDate,
                                endDate)
                        },
                        function(info) {
                            baiduMapService.switchMainMap();
                        });
                },
                showRutraceCoverage: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Coverage/Index.html',
                        controller: 'rutrace.coverage.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "覆盖指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showGeneralCoverage: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Coverage/General.html',
                        controller: 'general.coverage.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "覆盖指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showPrecise: function(precise) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/PreciseSectorMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return precise.eNodebName + "-" + precise.sectorId + "精确覆盖率指标";
                            },
                            neighbor: function() {
                                return precise;
                            }
                        }
                    });
                },
                showCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/CellInfo.html',
                        controller: 'cell.info.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return cell.eNodebName + "-" + cell.sectorId + "小区详细信息";
                            },
                            cell: function() {
                                return cell;
                            }
                        }
                    });
                },
                setQueryConditions: function(city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/QueryMap.html',
                        controller: 'query.setting.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "小区信息查询条件设置";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                queryList: function(city) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/List.html',
                        controller: 'parameters.list',
                        resolve: {
                            dialogTitle: function() {
                                return "全网基站小区信息统计";
                            },
                            city: function() {
                                return city;
                            }
                        }
                    });
                },
                queryCellTypeChart: function(city) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/DoubleChartDialog.html',
                        controller: 'cell.type.chart',
                        resolve: {
                            dialogTitle: function() {
                                return "全网小区类型统计";
                            },
                            city: function() {
                                return city;
                            }
                        }
                    });
                },
                showFlowCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/FlowKpiInfo.html',
                        controller: 'flow.kpi.dialog',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.item.eNodebName + "-" + cell.item.sectorId + "小区流量相关指标信息";
                                },
                                cell: function() {
                                    return cell.item;
                                }
                            },
                            cell.beginDate.value,
                            cell.endDate.value)
                    });
                },
                showRrcCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/RrcKpiInfo.html',
                        controller: 'rrc.kpi.dialog',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.item.eNodebName + "-" + cell.item.sectorId + "小区RRC连接指标信息";
                                },
                                cell: function() {
                                    return cell.item;
                                }
                            },
                            cell.beginDate.value,
                            cell.endDate.value)
                    });
                },
                showInterferenceSource: function(neighbor) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/SourceMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return neighbor.neighborCellName + "干扰源信息";
                            },
                            neighbor: function() {
                                return neighbor;
                            }
                        }
                    });
                },
                showInterferenceVictim: function(neighbor) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/VictimMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return neighbor.victimCellName + "被干扰小区信息";
                            },
                            neighbor: function() {
                                return neighbor;
                            }
                        }
                    });
                },
                matchNeighbor: function(center, candidate, neighbors) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Rutrace/Interference/MatchCellDialog.html',
                            controller: 'neighbors.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return center.eNodebName +
                                        "-" +
                                        center.sectorId +
                                        "的邻区PCI=" +
                                        candidate.destPci +
                                        "，频点=" +
                                        candidate.neighborEarfcn +
                                        "的可能小区";
                                },
                                candidateNeighbors: function() {
                                    return neighbors;
                                },
                                currentCell: function() {
                                    return center;
                                }
                            }
                        },
                        function(nearestCell) {
                            networkElementService.updateNeighbors(center.cellId,
                                center.sectorId,
                                candidate.destPci,
                                nearestCell.eNodebId,
                                nearestCell.sectorId).then(function() {
                                candidate.neighborCellName = nearestCell.eNodebName + "-" + nearestCell.sectorId;
                            });
                        });
                }
            }
        });