angular.module('topic.dialog',[ 'app.menu', 'app.core' ])
    .factory('mapDialogService',
        function (menuItemService, stationFormatService) {
            return {
                showMicroAmpliferInfos: function(item) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Dialog/Micro.html',
                        controller: 'micro.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return item.addressNumber + "手机伴侣基本信息";
                            },
                            item: function() {
                                return item;
                            }
                        }
                    });
                },
                showCollegeCoverageList: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Coverage/All.html',
                        controller: 'college.coverage.all',
                        resolve: stationFormatService.dateSpanDateResolve({}, beginDate, endDate)
                    });
                },
                showHotSpotFlowTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'hotSpot.flow.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showCollegeFeelingTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'college.feeling.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showHotSpotFeelingTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'hotSpot.feeling.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showCollegeFlowDumpDialog: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Table/DumpCollegeFlowTable.html',
                        controller: 'college.flow.dump',
                        resolve: stationFormatService.dateSpanDateResolve({},
                            beginDate,
                            endDate)
                    });
                }
            };
        });