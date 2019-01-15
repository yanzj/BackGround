angular.module('kpi.college.infrastructure', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .run(function($rootScope) {
        $rootScope.page = {
            messages: []
        };
        $rootScope.closeAlert = function(messages, $index) {
            messages.splice($index, 1);
        };
        $rootScope.addSuccessMessage = function(message) {
            $rootScope.page.messages.push({
                type: 'success',
                contents: message
            });
        };
    })
    .controller('bts.dialog',
        function($scope,
            $uibModalInstance,
            collegeService,
            collegeDialogService,
            collegeQueryService,
            geometryService,
            baiduQueryService,
            name,
            dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeQueryService.queryByName(name).then(function(college) {
                collegeService.queryRegion(college.id).then(function(region) {
                    var center = geometryService.queryRegionCenter(region);
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            X: 2 * center.X - coors.x,
                            Y: 2 * center.Y - coors.y
                        };
                    });
                });
            });

            $scope.query = function() {
                collegeService.queryBtss(name).then(function(result) {
                    $scope.btsList = result;
                });
            };

            $scope.addBts = function() {
                collegeDialogService.addBts(name,
                    $scope.center,
                    function(count) {
                        $scope.addSuccessMessage('增加Bts' + count + '个');
                        $scope.query();
                    });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.btsList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdmaCell.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaCells(name).then(function(result) {
                $scope.cdmaCellList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cdmaCellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('lte.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryLteDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdma.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.college.basic', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('year.info.dialog',
        function($scope,
            $uibModalInstance,
            appFormatService,
            name,
            year,
            item) {
            $scope.dialogTitle = name + year + "年校园信息补充";
            $scope.dto = item;
            $scope.beginDate = {
                value: appFormatService.getDate(item.oldOpenDate),
                opened: false
            };
            $scope.endDate = {
                value: appFormatService.getDate(item.newOpenDate),
                opened: false
            };
            $scope.beginDate.value.setDate($scope.beginDate.value.getDate() + 365);
            $scope.endDate.value.setDate($scope.endDate.value.getDate() + 365);

            $scope.ok = function() {
                $scope.dto.oldOpenDate = $scope.beginDate.value;
                $scope.dto.newOpenDate = $scope.endDate.value;
                $scope.dto.year = year;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.query.name",
        function($scope, $uibModalInstance, dialogTitle, name, collegeService) {
            $scope.collegeName = name;
            $scope.dialogTitle = dialogTitle;
            $scope.updateLteCells = function() {
                collegeService.queryCells($scope.collegeName).then(function(cells) {
                    $scope.cellList = cells;
                });
            };
            collegeService.queryENodebs($scope.collegeName).then(function(eNodebs) {
                $scope.eNodebList = eNodebs;
            });
            $scope.updateLteCells();
            collegeService.queryBtss($scope.collegeName).then(function(btss) {
                $scope.btsList = btss;
            });
            collegeService.queryCdmaCells($scope.collegeName).then(function(cells) {
                $scope.cdmaCellList = cells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.college);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.college.maintain', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('cell.supplement.dialog', ////////////////////////////////////////////////////////
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            eNodebs,
            cells,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            angular.forEach(eNodebs,
                function(eNodeb) {
                    networkElementService.queryCellInfosInOneENodeb(eNodeb.eNodebId).then(function(cellInfos) {
                        neighborImportService.updateCellRruInfo($scope.supplementCells,
                        {
                            dstCells: cellInfos,
                            cells: cells,
                            longtitute: eNodeb.longtitute,
                            lattitute: eNodeb.lattitute
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cell.position.supplement.dialog', ///////////////////////////////////////////////////
        function($scope,
            $uibModalInstance,
            collegeMapService,
            baiduQueryService,
            collegeService,
            collegeQueryService,
            networkElementService,
            neighborImportService,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    collegeService.queryCells(collegeName).then(function(cells) {
                        baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                            collegeQueryService.queryByName(collegeName).then(function(info) {
                                networkElementService
                                    .queryRangeCells(neighborImportService.generateRange(info.rectangleRange, center, coors))
                                    .then(function(results) {
                                        neighborImportService.updateENodebRruInfo($scope.supplementCells,
                                        {
                                            dstCells: results,
                                            cells: cells,
                                            longtitute: center.X,
                                            lattitute: center.Y
                                        });
                                    });
                            });
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('eNodeb.supplement.dialog', ////////////////////////////////////////////
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            collegeQueryService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE基站补充";
            $scope.supplementENodebs = [];
            $scope.gridApi = {};

            $scope.query = function() {
                baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                    collegeQueryService.queryByName(collegeName).then(function(info) {
                        var ids = [];
                        collegeService.queryENodebs(collegeName).then(function(eNodebs) {
                            angular.forEach(eNodebs,
                                function(eNodeb) {
                                    ids.push(eNodeb.eNodebId);
                                });
                            networkElementService
                                .queryRangeENodebs(neighborImportService
                                    .generateRangeWithExcludedIds(info.rectangleRange, center, coors, ids)).then(function(results) {
                                    angular.forEach(results,
                                        function(item) {
                                            item.distance = geometryService
                                                .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                        });
                                    $scope.supplementENodebs = results;
                                });
                        });
                    });
                });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('bts.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "CDMA基站补充";
            $scope.supplementBts = [];
            $scope.gridApi = {};

            baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                collegeService.queryRange(collegeName).then(function(range) {
                    var ids = [];
                    collegeService.queryBtss(collegeName).then(function(btss) {
                        angular.forEach(btss,
                            function(bts) {
                                ids.push(bts.btsId);
                            });
                        networkElementService
                            .queryRangeBtss(neighborImportService
                                .generateRangeWithExcludedIds(range, center, coors, ids)).then(function(results) {
                                angular.forEach(results,
                                    function(item) {
                                        item.distance = geometryService
                                            .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                    });
                                $scope.supplementBts = results;
                            });
                    });
                });
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.new.dialog',
        function($scope,
            $uibModalInstance,
            baiduMapService,
            geometryService,
            baiduQueryService,
            appRegionService,
            $timeout) {
            $scope.city = {
                selected: "佛山",
                options: ["佛山"]
            };
            $scope.dialogTitle = "新建校园信息";
            $scope.collegeRegion = {
                area: 0,
                regionType: 0,
                info: ""
            };
            $scope.saveCircleParameters = function(circle) {
                var center = circle.getCenter();
                var radius = circle.getRadius();
                $scope.collegeRegion.regionType = 0;
                $scope.collegeRegion.area = BMapLib.GeoUtils.getCircleArea(circle);
                $scope.collegeRegion.info = center.lng + ';' + center.lat + ';' + radius;
            };
            $scope.saveRetangleParameters = function(rect) {
                $scope.collegeRegion.regionType = 1;
                var pts = rect.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $scope.savePolygonParameters = function(polygon) {
                $scope.collegeRegion.regionType = 2;
                var pts = polygon.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $timeout(function() {
                    baiduMapService.initializeMap('map', 12);
                    baiduMapService.initializeDrawingManager();
                    baiduMapService.addDrawingEventListener('circlecomplete', $scope.saveCircleParameters);
                    baiduMapService.addDrawingEventListener('rectanglecomplete', $scope.saveRetangleParameters);
                    baiduMapService.addDrawingEventListener('polygoncomplete', $scope.savePolygonParameters);
                },
                500);
            $scope.matchPlace = function() {
                baiduQueryService.queryBaiduPlace($scope.collegeName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
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
                $scope.college = {
                    name: $scope.collegeName,
                    townId: 0,
                    collegeRegion: $scope.collegeRegion
                };
                appRegionService.queryTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                    .then(function(town) {
                        if (town) {
                            $scope.college.townId = town.id;
                            $uibModalInstance.close($scope.college);
                        }
                    });
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });

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
			supplementENodebCells: function(eNodebs, cells, collegeName, callback) { ////////////////////////////////////
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
						callback(collegeName);
					});

				});
			}, ////////////////////////////////////////////
			supplementPositionCells: function(collegeName, callback) { ////////////////////////////////////////////////////
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
						callback(collegeName);
					});

				});
			}, /////////////////////////////////////////////////////////

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
			addENodeb: function(collegeName, center, callback) { /////////////////////////////////////////////////////
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
			}, ///////////////////////////////////////////////////////////
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
			}, //////////////////////////////////////////////////////////
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
            }
		};
	});
angular.module('kpi.customer', ['myApp.url', 'myApp.region'])
    .factory('customerDialogService',
        function(menuItemService, customerQueryService, complainService, basicImportService) {
            return {
                constructHotSpot: function(callback, callback2) {
                    menuItemService.showGeneralDialogWithDoubleAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpot.html',
                            controller: 'hot.spot.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return '新增热点信息';
                                }
                            }
                        },
                        function(dto) {
                            basicImportService.dumpOneHotSpot(dto).then(function(result) {
                                callback();
                            });
                        },
                        callback2);
                },
                modifyHotSpot: function(item, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpot.html',
                            controller: 'hot.spot.modify',
                            resolve: {
                                dialogTitle: function() {
                                    return '修改热点信息-' + item.hotspotName;
                                },
                                dto: function() {
                                    return item;
                                }
                            }
                        },
                        function(dto) {
                            basicImportService.dumpOneHotSpot(dto).then(function(result) {
                                callback();
                            });
                        });
                },
                manageHotSpotCells: function(hotSpot, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpotCell.html',
                            controller: 'hot.spot.cell.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return hotSpot.hotspotName + '热点小区管理';
                                },
                                name: function() {
                                    return hotSpot.hotspotName;
                                },
                                address: function() {
                                    return hotSpot.address;
                                },
                                center: function() {
                                    return {
                                        longtitute: hotSpot.longtitute,
                                        lattitute: hotSpot.lattitute
                                    }
                                }
                            }
                        },
                        function(dto) {
                            callback(dto);
                        });
                },
                supplementVipDemandInfo: function(view, city, district, messages, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/VipSupplement.html',
                            controller: 'vip.supplement.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "补充政企客户支撑需求信息";
                                },
                                view: function() {
                                    return view;
                                },
                                city: function() {
                                    return city;
                                },
                                district: function() {
                                    return district;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.updateVip(dto).then(function() {
                                messages.push({
                                    type: 'success',
                                    contents: '完成政企客户支撑需求：' + dto.serialNumber + '的补充'
                                });
                                callback();
                            });
                        });
                },
                supplementCollegeDemandInfo: function(view, messages) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/CollegeSupplement.html',
                            controller: 'college.supplement.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "补充校园网支撑需求信息";
                                },
                                view: function() {
                                    return view;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.updateVip(dto).then(function() {
                                messages.push({
                                    type: 'success',
                                    contents: '完成校园网支撑需求：' + dto.serialNumber + '的补充'
                                });
                            });
                        });
                },

                supplementComplainInfo: function(item, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Complain.html',
                            controller: 'complain.supplement.dialog',
                            resolve: {
                                item: function() {
                                    return item;
                                }
                            }
                        },
                        function(info) {
                            complainService.postPosition(info).then(function() {
                                callback();
                            });
                        });
                }
            };
        });
angular.module('kpi.customer.complain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('emergency.new.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            dialogTitle,
            city,
            district,
            vehicularType) {
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";
            $scope.city = city;
            $scope.district = district;
            $scope.vehicularType = vehicularType;

            var firstDay = new Date();
            firstDay.setDate(firstDay.getDate() + 7);
            var nextDay = new Date();
            nextDay.setDate(nextDay.getDate() + 14);
            $scope.itemBeginDate = {
                value: firstDay,
                opened: false
            };
            $scope.itemEndDate = {
                value: nextDay,
                opened: false
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            $scope.dto = {
                projectName: "和顺梦里水乡百合花文化节",
                expectedPeople: 500000,
                vehicles: 1,
                area: "万顷洋园艺世界",
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "此次活动为佛山市南海区政府组织的一次大型文化活动，是宣传天翼品牌的重要场合。",
                townId: 1
            };

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('emergency.college.dialog',
        function($scope,
            $uibModalInstance,
            serialNumber,
            collegeName,
            collegeQueryService,
            appFormatService,
            customerQueryService,
            appRegionService) {
            $scope.dialogTitle = collegeName + "应急通信车申请-" + serialNumber;
            $scope.dto = {
                projectName: collegeName + "应急通信车申请",
                expectedPeople: 500000,
                vehicles: 1,
                area: collegeName,
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "应急通信车申请。",
                townId: 1
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            customerQueryService.queryVehicleTypeOptions().then(function(options) {
                $scope.vehicularType = {
                    options: options,
                    selected: options[17]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            collegeQueryService.queryByNameAndYear(collegeName, $scope.collegeInfo.year.selected).then(function(item) {
                $scope.itemBeginDate = {
                    value: appFormatService.getDate(item.oldOpenDate),
                    opened: false
                };
                $scope.itemEndDate = {
                    value: appFormatService.getDate(item.newOpenDate),
                    opened: false
                };
                $scope.dto.expectedPeople = item.expectedSubscribers;
            });
            customerQueryService.queryOneVip(serialNumber).then(function(item) {
                angular.forEach($scope.district.options,
                    function(district) {
                        if (district === item.district) {
                            $scope.district.selected = item.district;
                        }
                    });
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town.options = towns;
                    $scope.town.selected = towns[0];
                    angular.forEach(towns,
                        function(town) {
                            if (town === item.town) {
                                $scope.town.selected = town;
                            }
                        });
                });
            });

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('vip.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view,
            city,
            district) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;
            $scope.city = city;
            $scope.district = district;
            $scope.matchFunction = function(text) {
                return $scope.view.projectName.indexOf(text) >= 0 || $scope.view.projectContents.indexOf(text) >= 0;
            };
            $scope.matchDistrictTown = function() {
                var districtOption = appFormatService.searchText($scope.district.options, $scope.matchFunction);
                if (districtOption) {
                    $scope.district.selected = districtOption;
                }
            };
            $scope.$watch('town.selected',
                function() {
                    var townOption = appFormatService.searchText($scope.town.options, $scope.matchFunction);
                    if (townOption) {
                        $scope.town.selected = townOption;
                    }
                });

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };
        })

    .controller('complain.supplement.dialog',
        function($scope,
            $uibModalInstance,
            appRegionService,
            appFormatService,
            baiduMapService,
            parametersMapService,
            parametersDialogService,
            item) {
            $scope.dialogTitle = item.serialNumber + "工单信息补充";

            $scope.itemGroups = [
                {
                    items: [
                        {
                            key: '工单编号',
                            value: item.serialNumber
                        }, {
                            key: '经度',
                            value: item.longtitute
                        }, {
                            key: '纬度',
                            value: item.lattitute
                        }
                    ]
                }, {
                    items: [
                        {
                            key: '城市',
                            value: item.city
                        }, {
                            key: '区域',
                            value: item.district
                        }, {
                            key: '镇区',
                            value: item.town
                        }
                    ]
                }, {
                    items: [
                        {
                            key: '楼宇名称',
                            value: item.buildingName
                        }, {
                            key: '道路名称',
                            value: item.roadName
                        }, {
                            key: '匹配站点',
                            value: item.sitePosition
                        }
                    ]
                }, {
                    items: [
                        {
                            key: '联系地址',
                            value: item.contactAddress,
                            span: 2
                        }, {
                            key: '投诉内容',
                            value: item.complainContents,
                            span: 2
                        }
                    ]
                }
            ];
            appRegionService.initializeCities().then(function(cities) {
                $scope.city = {
                    options: cities,
                    selected: cities[0]
                };
                appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                    $scope.district = {
                        options: districts,
                        selected: (item.district) ? item.district.replace('区', '') : districts[0]
                    };

                    baiduMapService.initializeMap("map", 11);
                    baiduMapService.addCityBoundary("佛山");
                    if (item.longtitute && item.lattitute) {
                        var marker = baiduMapService.generateMarker(item.longtitute, item.lattitute);
                        baiduMapService.addOneMarker(marker);
                        baiduMapService.setCellFocus(item.longtitute, item.lattitute, 15);
                    }
                    if (item.sitePosition) {
                        parametersMapService.showElementsWithGeneralName(item.sitePosition,
                            parametersDialogService.showENodebInfo,
                            parametersDialogService.showCellInfo);
                    }
                });
            });

            $scope.matchTown = function() {
                var town = appFormatService.searchPattern($scope.town.options, item.sitePosition);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.buildingName);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.roadName);
                if (town) {
                    $scope.town.selected = town;
                }
            };

            $scope.ok = function() {
                $scope.item.district = $scope.district.selected;
                $scope.item.town = $scope.town.selected;
                $uibModalInstance.close($scope.item);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.customer.sustain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('hot.spot.dialog',
        function($scope, dialogTitle, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = {
                longtitute: 112.99,
                lattitute: 22.98
            };

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                var options =_.filter(result,
                    function(stat) {
                        return stat !== '高速公路' && stat !== '其他' &&
                            stat !== '高速铁路' && stat !== '区域定义' &&
                            stat !== '地铁' && stat !== 'TOP小区' && stat !== '校园网';
                    });
                $scope.spotType = {
                    options: options,
                    selected: options[0]
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.modify',
        function($scope, dialogTitle, dto, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = dto;
            $scope.modify = true;

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                $scope.spotType = {
                    options: result,
                    selected: $scope.dto.typeDescription
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addOneMarker(baiduMapService
                    .generateMarker(coors.x, coors.y));
                baiduMapService.setCellFocus(coors.x, coors.y);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.cell.dialog',
        function($scope,
            dialogTitle,
            address,
            name,
            center,
            $uibModalInstance,
            basicImportService,
            collegeQueryService,
            networkElementService,
            neighborImportService,
            complainService) {
            $scope.dialogTitle = dialogTitle;
            $scope.address = address;
            $scope.gridApi = {};
            $scope.gridApi2 = {};
            $scope.updateCellInfos = function(positions, existedCells) {
                neighborImportService.updateENodebRruInfo($scope.positionCells,
                    {
                        dstCells: positions,
                        cells: existedCells,
                        longtitute: center.longtitute,
                        lattitute: center.lattitute
                    });
            };
            $scope.query = function() {
                basicImportService.queryHotSpotCells(name).then(function(result) {
                    $scope.candidateIndoorCells = result;
                });
                complainService.queryHotSpotCells(name).then(function(existedCells) {
                    $scope.existedCells = existedCells;
                    $scope.positionCells = [];
                    networkElementService.queryRangeCells({
                        west: center.longtitute - 0.003,
                        east: center.longtitute + 0.003,
                        south: center.lattitute - 0.003,
                        north: center.lattitute + 0.003
                    }).then(function(positions) {
                        if (positions.length > 5) {
                            $scope.updateCellInfos(positions, existedCells);
                        } else {
                            networkElementService.queryRangeCells({
                                west: center.longtitute - 0.01,
                                east: center.longtitute + 0.01,
                                south: center.lattitute - 0.01,
                                north: center.lattitute + 0.01
                            }).then(function(pos) {
                                if (pos.length > 5) {
                                    $scope.updateCellInfos(pos, existedCells);
                                } else {
                                    networkElementService.queryRangeCells({
                                        west: center.longtitute - 0.02,
                                        east: center.longtitute + 0.02,
                                        south: center.lattitute - 0.02,
                                        north: center.lattitute + 0.02
                                    }).then(function(pos2) {
                                        $scope.updateCellInfos(pos2, existedCells);
                                    });
                                }
                                
                            });
                        }
                    });
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.importCells = function() {
                var cellNames = [];
                angular.forEach($scope.gridApi.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                angular.forEach($scope.gridApi2.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                collegeQueryService.saveCollegeCells({
                    collegeName: name,
                    cellNames: cellNames
                }).then(function() {
                    $scope.query();
                });
            }
            $scope.query();
        })
    .controller('college.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;

            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.parameter.dump', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('neighbors.dialog',
        function($scope,
            $uibModalInstance,
            geometryService,
            dialogTitle,
            candidateNeighbors,
            currentCell) {
            $scope.pciNeighbors = [];
            $scope.indoorConsidered = false;
            $scope.distanceOrder = "distance";
            $scope.dialogTitle = dialogTitle;
            $scope.candidateNeighbors = candidateNeighbors;
            $scope.currentCell = currentCell;

            angular.forEach($scope.candidateNeighbors,
                function(neighbor) {
                    neighbor.distance = geometryService.getDistance($scope.currentCell.lattitute,
                        $scope.currentCell.longtitute,
                        neighbor.lattitute,
                        neighbor.longtitute);

                    $scope.pciNeighbors.push(neighbor);
                });

            $scope.updateNearestCell = function() {
                var minDistance = 10000;
                angular.forEach($scope.candidateNeighbors,
                    function(neighbor) {
                        if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
                            minDistance = neighbor.distance;
                            $scope.nearestCell = neighbor;
                        }
                    });

            };

            $scope.ok = function() {
                $scope.updateNearestCell();
                $uibModalInstance.close($scope.nearestCell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.work.dialog', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('workitem.feedback.dialog',
        function($scope, $uibModalInstance, input, dialogTitle) {
            $scope.item = input;
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";

            $scope.ok = function() {
                $uibModalInstance.close($scope.message);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('workitem.details.dialog',
        function($scope, $uibModalInstance, input, dialogTitle, preciseWorkItemGenerator) {
            $scope.currentView = input;
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";
            $scope.platformInfos = preciseWorkItemGenerator.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = preciseWorkItemGenerator.calculatePlatformInfo($scope.currentView.feedbackContents);
            $scope.preventChangeParentView = true;

            $scope.ok = function() {
                $uibModalInstance.close($scope.message);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("rutrace.workitems.process",
        function($scope,
            $uibModalInstance,
            cell,
            beginDate,
            endDate,
            workitemService,
            networkElementService,
            appFormatService) {
            $scope.dialogTitle = cell.eNodebName + "-" + cell.sectorId + ":TOP小区工单历史";
            $scope.queryWorkItems = function() {
                workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.viewItems = result;
                });
                networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.lteCellGroups = appFormatService.generateCellGroups(result);
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.queryWorkItems();
        });
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

angular.module('myApp.kpi', [
    'kpi.college.infrastructure', 'kpi.college.basic', 'kpi.college.maintain',
    'kpi.college',
    'kpi.customer', 'kpi.customer.complain', 'kpi.customer.sustain',
    'kpi.parameter.dump', 'kpi.work.dialog', 'kpi.work']);