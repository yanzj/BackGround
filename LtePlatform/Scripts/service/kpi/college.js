angular.module('kpi.college', ['app.menu', 'region.college'])
	.factory('collegeDialogService', function(collegeQueryService, menuItemService) {
		var resolveScope = function(name, topic) {
			return {
				dialogTitle: function() {
					return name + "-" + topic;
				},
				name: function() {
					return name;
				}
			};
		};
		return {
			showENodebs: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/ENodebDialog.html',
					controller: 'eNodeb.dialog',
					resolve: resolveScope(name, "LTE基站信息")
				});
			},
			showCells: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/LteCellDialog.html',
					controller: 'cell.dialog',
					resolve: resolveScope(name, "LTE小区信息")
				});
			},
			showBtss: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/BtsDialog.html',
					controller: 'bts.dialog',
					resolve: resolveScope(name, "CDMA基站信息")
				});
			},
			showCdmaCells: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/CdmaCellDialog.html',
					controller: 'cdmaCell.dialog',
					resolve: resolveScope(name, "CDMA小区信息")
				});
			},
			showLteDistributions: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/DistributionDialog.html',
					controller: 'lte.distribution.dialog',
					resolve: resolveScope(name, "LTE室分信息")
				});
			},
			showCdmaDistributions: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/DistributionDialog.html',
					controller: 'cdma.distribution.dialog',
					resolve: resolveScope(name, "CDMA室分信息")
				});
			},
			showCollegeDetails: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/CollegeQuery.html',
					controller: 'college.query.name',
					resolve: resolveScope(name, "详细信息")
				});
			},

			addYearInfo: function(item, name, year, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/YearInfoDialog.html',
					controller: 'year.info.dialog',
					resolve: {
						name: function() {
							return name;
						},
						year: function() {
							return year;
						},
						item: function() {
							return item;
						}
					}
				}, function(info) {
					collegeQueryService.saveYearInfo(info).then(function() {
						callback();
					});
				});
			},
			addNewCollege: function(callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/NewCollegeDialog.html',
					controller: 'college.new.dialog',
					resolve: {}
				}, function(info) {
					collegeQueryService.constructCollegeInfo(info).then(function() {
						callback();
					});
				});
			},
			supplementENodebCells: function(eNodebs, cells, collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/CellSupplementDialog.html',
					controller: 'cell.supplement.dialog',
					resolve: {
						eNodebs: function() {
							return eNodebs;
						},
						cells: function() {
							return cells;
						},
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					var cellNames = [];
					angular.forEach(info, function(cell) {
						cellNames.push(cell.cellName);
					});
					collegeQueryService.saveCollegeCells({
						collegeName: collegeName,
						cellNames: cellNames
					}).then(function() {
						callback();
					});

				});
			},
			supplementPositionCells: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/CellSupplementDialog.html',
					controller: 'cell.position.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					var cellNames = [];
					angular.forEach(info, function(cell) {
						cellNames.push(cell.cellName);
					});
					collegeQueryService.saveCollegeCells({
						collegeName: collegeName,
						cellNames: cellNames
					}).then(function() {
						callback();
					});

				});
			},
			construct3GTest: function(collegeName) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Construct3GTest.html',
					controller: 'college.test3G.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					collegeQueryService.saveCollege3GTest(info).then(function() {
						console.log(info);
					});
				});
			},
			construct4GTest: function(collegeName) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Construct4GTest.html',
					controller: 'college.test4G.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					collegeQueryService.saveCollege4GTest(info).then(function() {
						console.log(info);
					});
				});
			},
			processTest: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Process.html',
					controller: 'test.process.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					callback(info);
				});
			},
			tracePlanning: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Planning.html',
					controller: 'trace.planning.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					callback(info);
				});
			},
			showCollegDialog: function(college, year) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Table/CollegeMapInfoBox.html',
					controller: 'map.college.dialog',
					resolve: {
						dialogTitle: function() {
							return college.name + "-" + "基本信息";
						},
						college: function() {
							return college;
						},
						year: function() {
							return year;
						}
					}
				});
			},
			addENodeb: function(collegeName, center, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/ENodebSupplementDialog.html',
					controller: 'eNodeb.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						},
						center: function() {
							return center;
						}
					}
				}, function(info) {
					var ids = [];
					angular.forEach(info, function(eNodeb) {
						ids.push(eNodeb.eNodebId);
					});
					collegeQueryService.saveCollegeENodebs({
						collegeName: collegeName,
						eNodebIds: ids
					}).then(function(count) {
						callback(count);
					});
				});
			},
			addBts: function(collegeName, center, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/BtsSupplementDialog.html',
					controller: 'bts.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						},
						center: function() {
							return center;
						}
					}
				}, function(info) {
					var ids = [];
					angular.forEach(info, function(bts) {
						ids.push(bts.btsId);
					});
					collegeQueryService.saveCollegeBtss({
						collegeName: collegeName,
						btsIds: ids
					}).then(function(count) {
						callback(count);
					});
				});
			},
			showCollegeFlow: function(year) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Test/Flow.html',
					controller: 'college.flow',
					resolve: {
						dialogTitle: function() {
							return "校园流量分析（" + year + "年）";
						},
						year: function() {
							return year;
						}
					}
				});
            },
            showHotSpotFlow: function (hotSpots, theme) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/College/Test/Flow.html',
                    controller: 'hotSpot.flow',
                    resolve: {
                        dialogTitle: function () {
                            return theme + "热点流量分析";
                        },
                        hotSpots: function () {
                            return hotSpots;
                        },
                        theme: function() {
                            return theme;
                        }
                    }
                });
            },
			maintainCollegeInfo: function (year) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Stat.html',
					controller: 'maintain.college.dialog',
					resolve: {
						dialogTitle: function () {
							return "校园基础信息维护（" + year + "年）";
						},
						year: function () {
							return year;
						}
					}
				});
			}
		};
	});