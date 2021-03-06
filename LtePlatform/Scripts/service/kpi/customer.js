﻿angular.module('kpi.customer', ['myApp.url', 'myApp.region'])
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