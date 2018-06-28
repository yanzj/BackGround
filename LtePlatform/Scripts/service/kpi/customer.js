angular.module('kpi.customer', ['myApp.url', 'myApp.region'])
    .factory('customerDialogService',
        function(menuItemService, customerQueryService, emergencyService, complainService, basicImportService) {
            return {
                constructEmergencyCommunication: function(city, district, type, messages, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Emergency.html',
                            controller: 'emergency.new.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "新增应急通信需求";
                                },
                                city: function() {
                                    return city;
                                },
                                district: function() {
                                    return district;
                                },
                                vehicularType: function() {
                                    return type;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.postDto(dto).then(function(result) {
                                if (result > 0) {
                                    messages.push({
                                        type: 'success',
                                        contents: '完成应急通信需求：' + dto.projectName + '的导入'
                                    });
                                    callback();
                                } else {
                                    messages.push({
                                        type: 'warning',
                                        contents: '最近已经有该需求，请不要重复导入'
                                    });
                                }
                            });
                        });
                },
                constructEmergencyCollege: function(serialNumber, collegeName, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Emergency.html',
                            controller: 'emergency.college.dialog',
                            resolve: {
                                serialNumber: function() {
                                    return serialNumber;
                                },
                                collegeName: function() {
                                    return collegeName;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.postDto(dto).then(function(result) {
                                callback();
                            });
                        });
                },
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
                modifyHotSpot: function(item, callback, callback2) {
                    menuItemService.showGeneralDialogWithDoubleAction({
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
                        },
                        callback2);
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
                constructFiberItem: function(id, num, callback, messages) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Fiber.html',
                            controller: 'fiber.new.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "新增光纤工单信息";
                                },
                                id: function() {
                                    return id;
                                },
                                num: function() {
                                    return num;
                                }
                            }
                        },
                        function(item) {
                            emergencyService.createFiberItem(item).then(function(result) {
                                if (result) {
                                    messages.push({
                                        type: 'success',
                                        contents: '完成光纤工单：' + item.workItemNumber + '的导入'
                                    });
                                    callback(result);
                                } else {
                                    messages.push({
                                        type: 'warning',
                                        contents: '最近已经有该工单，请不要重复导入'
                                    });
                                }
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