angular.module('topic.dialog.customer', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('online.sustain.dialog',
        function($scope,
            $uibModalInstance,
            items,
            dialogTitle,
            networkElementService,
            cellHuaweiMongoService,
            alarmImportService,
            intraFreqHoService,
            interFreqHoService,
            appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.itemGroups = [];

            angular.forEach(items,
                function(item) {
                    $scope.itemGroups.push(appFormatService.generateSustainGroups(item));
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        })
    .controller('micro.dialog',
        function($scope, $uibModalInstance, dialogTitle, item, appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.detailsGroups = appFormatService.generateMicroAddressGroups(item);
            $scope.microGroups = [];
            angular.forEach(item.microItems,
                function(micro) {
                    $scope.microGroups.push(appFormatService.generateMicroItemGroups(micro));
                });
        })
    .controller("workitem.city",
        function($scope,
            $uibModalInstance,
            endDate,
            preciseWorkItemService,
            workItemDialog) {
            $scope.dialogTitle = "精确覆盖优化工单一览";
            var lastSeason = new Date();
            lastSeason.setDate(lastSeason.getDate() - 100);
            $scope.seasonDate = {
                value: new Date(lastSeason.getFullYear(), lastSeason.getMonth(), lastSeason.getDate(), 8),
                opened: false
            };
            $scope.endDate = endDate;
            $scope.queryWorkItems = function() {
                preciseWorkItemService.queryByDateSpan($scope.seasonDate.value, $scope.endDate.value)
                    .then(function(views) {
                        angular.forEach(views,
                            function(view) {
                                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
                            });
                        $scope.viewItems = views;
                    });
            };
            $scope.showDetails = function(view) {
                workItemDialog.showDetails(view, $scope.queryWorkItems);
            };
            $scope.queryWorkItems();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("workitem.district",
        function($scope,
            $uibModalInstance,
            district,
            endDate,
            preciseWorkItemService,
            workItemDialog) {
            $scope.dialogTitle = district + "精确覆盖优化工单一览";
            var lastSeason = new Date();
            lastSeason.setDate(lastSeason.getDate() - 100);
            $scope.seasonDate = {
                value: new Date(lastSeason.getFullYear(), lastSeason.getMonth(), lastSeason.getDate(), 8),
                opened: false
            };
            $scope.endDate = endDate;
            $scope.queryWorkItems = function() {
                preciseWorkItemService.queryByDateSpanDistrict($scope.seasonDate.value, $scope.endDate.value, district)
                    .then(function(views) {
                        angular.forEach(views,
                            function(view) {
                                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
                            });
                        $scope.viewItems = views;
                    });
            };
            $scope.showDetails = function(view) {
                workItemDialog.showDetails(view, $scope.queryWorkItems);
            };
            $scope.queryWorkItems();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("customer.index",
        function($scope,
            $uibModalInstance,
            complainService,
            appKpiService) {
            $scope.statDate = {
                value: new Date(),
                opened: false
            };
            $scope.monthObject = 498;
            $scope.query = function() {
                complainService.queryCurrentComplains($scope.statDate.value).then(function(count) {
                    $scope.count = count;
                    var objects = [];
                    complainService.queryMonthTrend($scope.statDate.value).then(function(stat) {
                        angular.forEach(stat.item1,
                            function(date, index) {
                                objects.push((index + 1) / stat.item1.length * $scope.monthObject);
                            });
                        var options = appKpiService.generateComplainTrendOptions(stat.item1, stat.item2, objects);
                        $('#line-chart').highcharts(options);
                    });
                });
            };
            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("customer.yesterday",
        function($scope,
            $uibModalInstance,
            city,
            complainService) {
            $scope.statDate = {
                value: new Date(),
                opened: false
            };
            $scope.city = city;
            $scope.data = {
                complainList: []
            };
            $scope.query = function() {
                complainService.queryLastDateComplainStats($scope.statDate.value).then(function(result) {
                    $scope.statDate.value = result.statDate;
                    $scope.districtStats = result.districtComplainViews;
                });
            };
            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
    })
    .controller("customer.recent",
        function ($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            complainService) {
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.city = city;
            $scope.data = {
                complainList: []
            };
            $scope.query = function () {
                complainService.queryDateSpanComplainStats($scope.beginDate.value, $scope.endDate.value).then(function (result) {
                    $scope.districtStats = result;
                });
            };
            $scope.query();
            $scope.ok = function () {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("complain.details",
        function($scope, $uibModalInstance, item, appFormatService) {
            $scope.dialogTitle = item.serialNumber + "投诉工单详细信息";
            $scope.complainGroups = appFormatService.generateComplainItemGroups(item);
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('complain.adjust',
        function ($scope, $uibModalInstance, complainService) {
            $scope.dialogTitle = "抱怨量信息校正";
            $scope.items = [];

            $scope.query = function() {
                complainService.queryPositionList($scope.beginDate.value, $scope.endDate.value).then(function(list) {
                    $scope.items = list;
                });
            };

            $scope.ok = function () {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.query();
        });