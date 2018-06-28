angular.module("myApp", ['app.common'])
    .controller("alarm.import", function($scope, alarmImportService) {
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
        $scope.updateDumpHistory = function() {
            alarmImportService.queryDumpHistory($scope.beginDate.value, $scope.endDate.value).then(function(result) {
                $scope.dumpHistory = result;
                angular.forEach(result, function (stat) {
                    if (stat.coverageStats > 12000 && stat.townCoverageStats === 0) {
                        alarmImportService.updateTownCoverageStats(stat.dateString).then(function(count) {
                            stat.townCoverageStats = count;
                        });
                    }
                });
            });
            alarmImportService.queryDumpItems().then(function(result) {
                $scope.progressInfo.totalDumpItems = result;
            });
            alarmImportService.queryCoverageDumpItems().then(function (result) {
                $scope.progressInfo.totalCoverageDumpItems = result;
            });
        };
        $scope.dumpItems = function() {
            alarmImportService.dumpSingleItem().then(function(result) {
                if (result) {
                    $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
                } else {
                    $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                }
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            }, function() {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            });
        };
        $scope.clearItems = function() {
            alarmImportService.clearImportItems().then(function() {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            });
        };

        $scope.clearCoverageItems = function() {
            alarmImportService.clearCoverageImportItems().then(function () {
                $scope.progressInfo.totalCoverageDumpItems = 0;
                $scope.progressInfo.totalCoverageSuccessItems = 0;
                $scope.progressInfo.totalCoverageFailItems = 0;
            });
        };

        $scope.dumpCoverageItems = function() {
            alarmImportService.dumpSingleCoverageItem().then(function (result) {
                if (result) {
                    $scope.progressInfo.totalCoverageSuccessItems = $scope.progressInfo.totalCoverageSuccessItems + 1;
                } else {
                    $scope.progressInfo.totalCoverageFailItems = $scope.progressInfo.totalCoverageFailItems + 1;
                }
                if ($scope.progressInfo.totalCoverageSuccessItems + $scope.progressInfo.totalCoverageFailItems
                    < $scope.progressInfo.totalCoverageDumpItems) {
                    $scope.dumpCoverageItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalCoverageDumpItems = 0;
                    $scope.progressInfo.totalCoverageSuccessItems = 0;
                    $scope.progressInfo.totalCoverageFailItems = 0;
                }
            }, function () {
                $scope.progressInfo.totalCoverageFailItems = $scope.progressInfo.totalCoverageFailItems + 1;
                if ($scope.progressInfo.totalCoverageSuccessItems + $scope.progressInfo.totalCoverageFailItems
                    < $scope.progressInfo.totalCoverageDumpItems) {
                    $scope.dumpCoverageItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalCoverageDumpItems = 0;
                    $scope.progressInfo.totalCoverageSuccessItems = 0;
                    $scope.progressInfo.totalCoverageFailItems = 0;
                }
            });
        };

        $scope.progressInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0,
            totalCoverageDumpItems: 0,
            totalCoverageSuccessItems: 0,
            totalCoverageFailItems: 0
        };
        $scope.updateDumpHistory();
    });