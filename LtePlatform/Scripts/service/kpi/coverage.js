angular.module('kpi.coverage', ['app.menu', 'app.core'])

    .factory('coverageDialogService', function (menuItemService, stationFormatService) {
        return {
            showDetails: function (cellName, cellId, sectorId) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/Coverage/DetailsChartDialog.html',
                    controller: 'coverage.details.dialog',
                    resolve: {
                        cellName: function () {
                            return cellName;
                        },
                        cellId: function () {
                            return cellId;
                        },
                        sectorId: function () {
                            return sectorId;
                        }
                    }
                });
            },
            showSource: function (currentView, serialNumber, beginDate, endDate, callback) {
                menuItemService.showGeneralDialogWithAction({
                        templateUrl: '/appViews/Rutrace/Interference/SourceDialog.html',
                        controller: 'interference.source.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return currentView.eNodebName + "-" + currentView.sectorId + "干扰源分析";
                                },
                                eNodebId: function() {
                                    return currentView.eNodebId;
                                },
                                sectorId: function() {
                                    return currentView.sectorId;
                                },
                                serialNumber: function() {
                                    return serialNumber;
                                }
                            },
                            beginDate,
                            endDate)
                    },
                    function(info) {
                        callback(info);
                    });
            },
            showSourceDbChart: function (currentView) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/Interference/SourceDbChartDialog.html',
                    controller: 'interference.source.db.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰源图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                });
            },
            showSourceModChart: function (currentView, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/SourceModChartDialog.html',
                    controller: 'interference.source.mod.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "MOD3/MOD6干扰图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showSourceStrengthChart: function (currentView, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/SourceStrengthChartDialog.html',
                    controller: 'interference.source.strength.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰强度图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showInterferenceVictim: function (currentView, serialNumber, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/VictimDialog.html',
                    controller: 'interference.victim.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰小区分析";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        serialNumber: function () {
                            return serialNumber;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showCoverage: function (currentView, preciseCells, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/CoverageDialog.html',
                    controller: 'interference.coverage.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "覆盖分析";
                        },
                        preciseCells: function () {
                            return preciseCells;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },///////////未完成
            showGridStats: function (district, town, theme, category, data, keys) {
                var stats = [];
                angular.forEach(keys, function (key) {
                    stats.push({
                        key: key,
                        value: data[key]
                    });
                });
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/SingleChartDialog.html',
                    controller: 'grid.stats',
                    resolve: {
                        dialogTitle: function () {
                            return district + town + theme;
                        },
                        category: function () {
                            return category;
                        },
                        stats: function () {
                            return stats;
                        }
                    }
                });
            },
            showGridClusterStats: function (theme, clusterList, currentCluster, action) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/BasicKpi/GridClusterDialog.html',
                    controller: 'grid.cluster',
                    resolve: {
                        dialogTitle: function () {
                            return theme + "栅格簇信息";
                        },
                        clusterList: function () {
                            return clusterList;
                        },
                        currentCluster: function () {
                            return currentCluster;
                        }
                    }
                }, action);
            },
            showAgpsStats: function (stats, legend) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'agps.stats',
                    resolve: {
                        dialogTitle: function () {
                            return 'AGPS三网对比覆盖指标';
                        },
                        stats: function () {
                            return stats;
                        },
                        legend: function() {
                            return legend;
                        }
                    }
                });
            },
            showTownStats: function (cityName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'town.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市LTE基站小区分布";
                        },
                        cityName: function () {
                            return cityName;
                        }
                    }
                });
            },
            showCdmaTownStats: function (cityName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/DoubleChartDialog.html',
                    controller: 'cdma.town.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市CDMA基站小区分布";
                        },
                        cityName: function () {
                            return cityName;
                        }
                    }
                });
            },
            showFlowStats: function (today, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/DoubleChartDialog.html',
                    controller: 'flow.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市4G流量和用户数分布";
                        },
                        today: function () {
                            return today;
                        },
                        frequency: function() {
                            return frequency;
                        }
                    }
                });
            },
            showFlowTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'flow.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "流量变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showUsersTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'users.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "用户数变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showFeelingRateTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'feelingRate.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "感知速率变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showDownSwitchTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'downSwitch.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "4G下切3G变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showRank2RateTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'rank2Rate.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "4G双流比变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showUserRoles: function (userName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Manage/UserRolesDialog.html',
                    controller: 'user.roles.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return userName + "角色管理";
                        },
                        userName: function () {
                            return userName;
                        }
                    }
                });
            }
        }
    })
