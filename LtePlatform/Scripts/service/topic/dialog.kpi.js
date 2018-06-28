angular.module('topic.dialog.kpi', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .run(function($rootScope, kpiPreciseService) {
        $rootScope.orderPolicy = {
            options: [],
            selected: ""
        };
        $rootScope.topCount = {
            options: [5, 10, 15, 20, 30],
            selected: 15
        };
        $rootScope.initializeOrderPolicy = function() {
            kpiPreciseService.getOrderSelection().then(function(result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[5];
            });
        };
        $rootScope.initializeDownSwitchOrderPolicy = function() {
            kpiPreciseService.getDownSwitchOrderSelection().then(function(result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[1];
            });
        };
        $rootScope.initializeCqiOrderPolicy = function () {
            kpiPreciseService.getCqiOrderSelection().then(function (result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[1];
            });
        };

        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };

        $rootScope.overallStat = {
            currentDistrict: "",
            districtStats: [],
            townStats: [],
            cityStat: {},
            dateString: ""
        };
        var yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        $rootScope.statDate = {
            value: yesterday,
            opened: false
        };
    })
    .controller("rutrace.trend",
        function($scope,
            $uibModalInstance,
            mapDialogService,
            appKpiService,
            kpiPreciseService,
            appFormatService,
            workItemDialog,
            dialogTitle,
            city,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle +
                '-' +
                appFormatService.getDateString($scope.statDate.value, "yyyy年MM月dd日");
            $scope.kpiType = 'precise';
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.showKpi = function() {
                kpiPreciseService.getRecentPreciseRegionKpi(city.selected, $scope.statDate.value)
                    .then(function(result) {
                        $scope.statDate.value = appFormatService.getDate(result.statDate);
                        angular.forEach(result.districtViews,
                            function(view) {
                                view.objectRate = appKpiService.getPreciseObject(view.district);
                            });
                        $scope.overallStat.districtStats = result.districtViews;
                        $scope.overallStat.townStats = result.townViews;
                        $scope.overallStat.currentDistrict = result.districtViews[0].district;
                        $scope.overallStat.districtStats
                            .push(appKpiService.getCityStat($scope.overallStat.districtStats, city.selected));
                        $scope.overallStat.dateString = appFormatService
                            .getDateString($scope.statDate.value, "yyyy年MM月dd日");
                    });
            };
            $scope.showChart = function() {
                workItemDialog.showPreciseChart($scope.overallStat);
            };
            $scope.showWorkitemCity = function() {
                mapDialogService.showPreciseWorkItem($scope.endDate);
            };
            $scope.showTrend = function() {
                workItemDialog.showPreciseTrend(city, $scope.beginDate, $scope.endDate);
            };
            $scope.showTopKpi = function() {
                mapDialogService.showPreciseTop($scope.beginDate, $scope.endDate);
            };

            $scope.showKpi();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('rrc.trend',
        function($scope,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            appKpiService,
            workItemDialog,
            dialogTitle,
            city,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle +
                '-' +
                appFormatService.getDateString($scope.statDate.value, "yyyy年MM月dd日");
            $scope.kpiType = 'rrc';

            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.showKpi = function() {
                kpiPreciseService.getRecentRrcRegionKpi(city.selected, $scope.statDate.value)
                    .then(function(result) {
                        $scope.statDate.value = appFormatService.getDate(result.statDate);
                        angular.forEach(result.districtViews,
                            function(view) {
                                view.objectRate = appKpiService.getRrcObject(view.district);
                            });
                        $scope.overallStat.districtStats = result.districtViews;
                        $scope.overallStat.townStats = result.townViews;
                        $scope.overallStat.currentDistrict = result.districtViews[0].district;
                        $scope.overallStat.districtStats
                            .push(appKpiService.getRrcCityStat($scope.overallStat.districtStats, city.selected));
                        $scope.overallStat.dateString = appFormatService
                            .getDateString($scope.statDate.value, "yyyy年MM月dd日");
                    });
            };
            $scope.showChart = function() {
                workItemDialog.showRrcChart($scope.overallStat);
            };
            $scope.showTrend = function() {
                workItemDialog.showRrcTrend(city, $scope.beginDate, $scope.endDate);
            };
            $scope.showKpi();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("kpi.basic",
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            appRegionService,
            appFormatService,
            kpi2GService,
            workItemDialog) {
            $scope.dialogTitle = dialogTitle;
            $scope.views = {
                options: ['主要', '2G', '3G'],
                selected: '主要'
            };
            $scope.showKpi = function() {
                kpi2GService.queryDayStats(city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.statList = result.statViews;
                });
            };
            $scope.showTrend = function() {
                workItemDialog.showBasicTrend(city.selected, $scope.beginDate, $scope.endDate);
            };
            $scope.$watch('city.selected',
                function(item) {
                    if (item) {
                        $scope.showKpi();
                    }
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });