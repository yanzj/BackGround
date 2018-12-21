angular.module('workitem.module', ['workitem.module.feedback', 'workitem.module.details'])
    .constant('workitemRoot', '/directives/workitem/');

angular.module('workitem.module.feedback', ['ui.grid', 'myApp.kpi'])
    .directive('platformAndFeedbackInfo',
        function(workitemRoot, workItemDialog, workitemService) {
            return {
                restrict: 'ECMA',
                replace: true,
                scope: {
                    currentView: '=',
                    serialNumber: '='
                },
                templateUrl: workitemRoot + 'PlatformAndFeedbackInfo.html',
                link: function(scope, element, attrs) {
                    scope.feedback = function() {
                        workItemDialog.feedback(scope.currentView, scope.updateWorkItems);
                    };
                    scope.$watch('currentView',
                        function(view) {
                            if (view) {
                                scope.platformInfos = workItemDialog.calculatePlatformInfo(view.comments);
                                scope.feedbackInfos = workItemDialog.calculatePlatformInfo(view.feedbackContents);
                            }
                        });

                    scope.updateWorkItems = function() {
                        workitemService.querySingleItem(scope.serialNumber).then(function(result) {
                            scope.currentView = result;
                        });
                    };
                }
            };
        })
    .controller('FlowDumpHistoryController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'dateString', name: '日期' },
                    { field: 'huaweiItems', name: '华为流量' },
                    { field: 'zteItems', name: '中兴流量' },
                    { field: 'townStats', name: '镇流量-全部' },
                    { field: 'townStats2100', name: '镇-2.1G' },
                    { field: 'townStats1800', name: '镇-1.8G' },
                    { field: 'townStats800VoLte', name: '镇-800M' }
                ],
                data: []
            };
        })
    .directive('flowDumpHistoryTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                controllerName: 'FlowDumpHistoryController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
    })
    .controller('CqiDumpHistoryController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'dateString', name: '日期' },
                    { field: 'huaweiCqis', name: '华为CQI' },
                    { field: 'zteCqis', name: '中兴CQI' },
                    { field: 'townCqis', name: '镇CQI2-全部' },
                    { field: 'townCqis2100', name: '镇-2.1G' },
                    { field: 'townCqis1800', name: '镇-1.8G' },
                    { field: 'townCqis800VoLte', name: '镇-800M' }
                ],
                data: []
            };
        })
    .directive('cqiDumpHistoryTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CqiDumpHistoryController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('PrbDumpHistoryController',
        function ($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'dateString', name: '日期' },
                    { field: 'huaweiPrbs', name: '华为PRB' },
                    { field: 'ztePrbs', name: '中兴PRB' },
                    { field: 'townPrbs', name: '镇PRB-全部' },
                    { field: 'townPrbs2100', name: '镇-2.1G' },
                    { field: 'townPrbs1800', name: '镇-1.8G' },
                    { field: 'townPrbs800VoLte', name: '镇-800M' }
                ],
                data: []
            };
        })
    .directive('prbDumpHistoryTable',
        function ($compile, calculateService) {
            return calculateService.generateGridDirective({
                controllerName: 'PrbDumpHistoryController',
                scope: {
                    items: '='
                },
                argumentName: 'items'
            },
                $compile);
        })
    .controller('ItemsDumpHistoryController',
        function ($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'dateString', name: '日期' },
                    { field: 'huaweiRssis', name: '华为RSSI' },
                    { field: 'zteRssis', name: '中兴RSSI' },
                    { field: 'townRrcs', name: '镇RRC统计' },
                    { field: 'townQcis', name: '镇CQI1统计' },
                    { field: 'townDoubleFlows', name: '镇双流统计' }
                ],
                data: []
            };
        })
    .directive('itemsDumpHistoryTable',
        function ($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'ItemsDumpHistoryController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })

    .controller('HourDumpHistoryController',
        function ($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'dateString', name: '日期' },
                    { field: 'prbItems', name: 'PRB利用率' },
                    { field: 'townPrbs', name: '镇-PRB利用率' },
                    { field: 'usersItems', name: '用户数' },
                    { field: 'townUserses', name: '镇-用户数' },
                    { field: 'cqiItems', name: 'CQI优良率' },
                    { field: 'townCqis', name: '镇CQI-全部' },
                    { field: 'townCqi2100s', name: '镇CQI-2.1G' },
                    { field: 'townCqi1800s', name: '镇CQI-1.8G' },
                    { field: 'townCqi800s', name: '镇CQI-800M' }
                ],
                data: []
            };
        })
    .directive('hourDumpHistoryTable',
        function ($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'HourDumpHistoryController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('workitem.module.details', [])
    .directive('workItemDetails', function(workitemRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                currentView: '=',
            },
            templateUrl: workitemRoot + 'WorkItem.Details.html',
            transclude: true
        };
    })
    .directive('preciseWorkItemCells', function(workitemRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                cells: '=',
            },
            templateUrl: workitemRoot + 'precise/Cell.html',
            transclude: true
        };
    })
    .directive('coverageWorkItemDialogCells', function (workitemRoot) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                cells: '='
            },
            templateUrl: workitemRoot + 'precise/CoverageCell.html',
            transclude: true
        };
    })
    .directive('workItemDetailsTable', function (workitemRoot, workitemService, preciseWorkItemGenerator) {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                currentView: '=',
                gobackPath: '=',
                gobackTitle: '=',
                rootPath: '=',
                preventChangeParentView: '=',
                platformInfos: '=',
                feedbackInfos: '='
            },
            templateUrl: workitemRoot + 'precise/DetailsTable.html',
            link: function(scope, element, attrs) {
                scope.signIn = function () {
                    workitemService.signIn(scope.currentView.serialNumber).then(function (result) {
                        if (result) {
                            scope.currentView = result;
                            scope.feedbackInfos = preciseWorkItemGenerator.calculatePlatformInfo(scope.currentView.feedbackContents);
                        }
                    });
                };
            }
        };
    });