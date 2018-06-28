angular.module('kpi.work', ['app.menu', 'app.core', 'myApp.region'])
    .factory('workItemDialog', function (menuItemService, workitemService, stationFormatService) {
		return {
			feedback: function(view, callbackFunc) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/WorkItem/FeedbackDialog.html',
					controller: 'workitem.feedback.dialog',
					resolve: {
						dialogTitle: function() {
							return view.serialNumber + "工单反馈";
						},
						input: function() {
							return view;
						}
					}
				}, function(output) {
					workitemService.feedback(output, view.serialNumber).then(function(result) {
						if (result && callbackFunc)
							callbackFunc();
					});
				});
			},
			showDetails: function(view, callbackFunc) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/WorkItem/DetailsDialog.html',
					controller: 'workitem.details.dialog',
					resolve: {
						dialogTitle: function() {
							return view.serialNumber + "工单信息";
						},
						input: function() {
							return view;
						}
					}
				}, callbackFunc);
			},
			showENodebFlow: function(eNodeb, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Parameters/Region/ENodebFlow.html',
			        controller: 'eNodeb.flow',
			        resolve: stationFormatService.dateSpanDateResolve({
			                eNodeb: function() {
			                    return eNodeb;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showHotSpotCellFlow: function(hotSpot, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Parameters/Region/ENodebFlow.html',
			        controller: 'hotSpot.cell.flow',
			        resolve: stationFormatService.dateSpanDateResolve({
			                hotSpot: function() {
			                    return hotSpot;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showHotSpotCells: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Parameters/Region/TopicCells.html',
					controller: 'topic.cells',
					resolve: {
						dialogTitle: function() {
							return name + "热点小区信息";
						},
						name: function() {
							return name;
						}
					}
				});
			},
			showPreciseChart: function(overallStat) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Home/DoubleChartDialog.html',
					controller: 'rutrace.chart',
					resolve: {
						dateString: function() {
							return overallStat.dateString;
						},
						districtStats: function() {
							return overallStat.districtStats;
						},
						townStats: function() {
							return overallStat.townStats;
						}
					}
				});
            },
            showRrcChart: function (overallStat) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'rrc.chart',
                    resolve: {
                        dateString: function () {
                            return overallStat.dateString;
                        },
                        districtStats: function () {
                            return overallStat.districtStats;
                        },
                        townStats: function () {
                            return overallStat.townStats;
                        }
                    }
                });
            },
			showPreciseTrend: function(city, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Rutrace/Coverage/Trend.html',
			        controller: 'rutrace.trend.dialog',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
            showDownSwitchTrend: function (city, beginDate, endDate) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/WorkItem/RrcTrend.html',
                    controller: 'down.switch.trend.dialog',
                    resolve: stationFormatService.dateSpanDateResolve({
                            city: function() {
                                return city;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
			showBasicTrend: function(city, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/Trend.html',
			        controller: 'basic.kpi.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showTopDropTrend: function(city, beginDate, endDate, topCount) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/TopDrop2GTrend.html',
			        controller: 'kpi.topDrop2G.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                },
			                topCount: function() {
			                    return topCount;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showTopConnectionTrend: function(city, beginDate, endDate, topCount) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/TopConnection3GTrend.html',
			        controller: 'kpi.topConnection3G.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                },
			                topCount: function() {
			                    return topCount;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showDistributionInfo: function(distribution) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Parameters/Map/DistributionMapInfoBox.html',
					controller: 'map.distribution.dialog',
					resolve: {
						dialogTitle: function() {
							return distribution.name + "-" + "室内分布基本信息";
						},
						distribution: function() {
							return distribution;
						}
					}
				});
			},
			showPreciseCellTrend: function (name, cellId, sectorId) {
				menuItemService.showGeneralDialog({
				    templateUrl: '/appViews/Rutrace/WorkItem/CellTrend.html',
				    controller: 'rutrace.cell.trend',
					resolve: {
						name: function () {
							return name;
						},
						cellId: function () {
							return cellId;
						},
						sectorId: function() {
							return sectorId;
						}
					}
				});
            },
            showDownSwitchCellTrend: function (name, cellId, sectorId) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/WorkItem/CellTrend.html',
                    controller: 'down.switch.cell.trend',
                    resolve: {
                        name: function () {
                            return name;
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
            processPreciseWorkItem: function(cell, beginDate, endDate) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/WorkItem/ForCell.html',
                    controller: "rutrace.workitems.process",
                    resolve: stationFormatService.dateSpanDateResolve({
                            cell: function() {
                                return cell;
                            }
                        },
                        beginDate,
                        endDate)
                });
            }
		};
	});
