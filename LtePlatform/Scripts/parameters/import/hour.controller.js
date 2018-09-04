angular.module("myApp", ['app.common'])
    .controller("hour.import",
        function($scope, basicImportService, neighborImportService, flowImportService, mapDialogService) {
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };

            $scope.prbInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.usersInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.cqiInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.clearPrbItems = function() {
                flowImportService.clearDumpHourPrbs().then(function() {
                    $scope.prbInfo.totalDumpItems = 0;
                    $scope.prbInfo.totalSuccessItems = 0;
                    $scope.prbInfo.totalFailItems = 0;
                });
            };
            $scope.clearUsersItems = function() {
                flowImportService.clearDumpHourUserses().then(function() {
                    $scope.usersInfo.totalDumpItems = 0;
                    $scope.usersInfo.totalSuccessItems = 0;
                    $scope.usersInfo.totalFailItems = 0;
                });
            };
            $scope.clearCqiItems = function() {
                flowImportService.clearDumpHourCqis().then(function() {
                    $scope.cqiInfo.totalDumpItems = 0;
                    $scope.cqiInfo.totalSuccessItems = 0;
                    $scope.cqiInfo.totalFailItems = 0;
                });
            };
            $scope.dumpPrbItems = function() {
                flowImportService.dumpHourPrb().then(function(result) {
                        neighborImportService.updateSuccessProgress(result, $scope.prbInfo, $scope.dumpPrbItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.prbInfo, $scope.dumpPrbItems);
                    });
            };
            $scope.dumpUsersItems = function() {
                flowImportService.dumpHourUserses().then(function(result) {
                        neighborImportService.updateSuccessProgress(result, $scope.usersInfo, $scope.dumpUsersItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.usersInfo, $scope.dumpUsersItems);
                    });
            };
            $scope.dumpCqiItems = function() {
                flowImportService.dumpHourCqis().then(function(result) {
                        neighborImportService.updateSuccessProgress(result, $scope.cqiInfo, $scope.dumpCqiItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.cqiInfo, $scope.dumpCqiItems);
                    });
            };

            $scope.updateDumpHistory = function() {
                flowImportService.queryHourPrbs().then(function(result) {
                    $scope.prbInfo.totalDumpItems = result;
                });
                flowImportService.queryHourUserses().then(function(result) {
                    $scope.usersInfo.totalDumpItems = result;
                });
                flowImportService.queryHourCqis().then(function(result) {
                    $scope.cqiInfo.totalDumpItems = result;
                });
                flowImportService.queryHourDumpHistory($scope.beginDate.value, $scope.endDate.value).then(
                    function(result) {
                        $scope.dumpHistory = result;
                        angular.forEach(result,
                            function(record) {
                                if ((record.prbItems > 27000 && record.townPrbs < 44)
                                    || (record.usersItems > 27000 && record.townUserses < 44)) {
                                    flowImportService.dumpTownHourStats(record.dateString).then(function(count) {
                                        record.townPrbs = count[0];
                                        record.townUserses = count[1];
                                    });
                                }
                            });
                    });
            };

            $scope.updateDumpHistory();

        });