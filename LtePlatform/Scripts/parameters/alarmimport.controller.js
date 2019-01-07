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
            });
            alarmImportService.queryCoverageHistory($scope.beginDate.value, $scope.endDate.value).then(function(result) {
                $scope.coverageHistory = result;
                angular.forEach(result, function (stat) {
                    if (stat.coverageStats > 12000) {
                        if (stat.townCoverageStats === 0) {
                            alarmImportService.updateTownCoverageStats(stat.dateString, 'all').then(function(count) {
                                stat.townCoverageStats = count;
                            });
                        }
                        if (stat.townCoverage800 === 0) {
                            alarmImportService.updateTownCoverageStats(stat.dateString, '800').then(function(count) {
                                stat.townCoverage800 = count;
                            });
                        }
                        if (stat.townCoverage1800 === 0) {
                            alarmImportService.updateTownCoverageStats(stat.dateString, '1800').then(function(count) {
                                stat.townCoverage1800 = count;
                            });
                        }
                        if (stat.townCoverage2100 === 0) {
                            alarmImportService.updateTownCoverageStats(stat.dateString, '2100').then(function(count) {
                                stat.townCoverage2100 = count;
                            });
                        }
                        if (stat.collegeCoverageStats === 0) {
                            alarmImportService.updateCollegeCoverageStats(stat.dateString).then(function(count) {
                                stat.collegeCoverageStats = count;
                            });
                        }
                        if (stat.marketCoverageStats === 0) {
                            alarmImportService.updateMarketCoverageStats(stat.dateString).then(function(count) {
                                stat.marketCoverageStats = count;
                            });
                        }
                    }
                        
                });
            });
            alarmImportService.queryDumpItems().then(function(result) {
                $scope.progressInfo.totalDumpItems = result;
            });
            alarmImportService.queryCoverageDumpItems().then(function (result) {
                $scope.progressInfo.totalCoverageDumpItems = result;
            });
            alarmImportService.queryZhangshangyouQualityDumpItems().then(function (result) {
                $scope.progressInfo.totalZhangshangyouQualityItems = result;
            });
            alarmImportService.queryZhangshangyouCoverageDumpItems().then(function (result) {
                $scope.progressInfo.totalZhangshangyouCoverageItems = result;
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

        $scope.dumpZhangshangyouQualityItems = function () {
            alarmImportService.dumpSingleZhangshangyouQualityItem().then(function (result) {
                if (result) {
                    $scope.progressInfo.totalZhangshangyouQualitySuccessItems += 1;
                } else {
                    $scope.progressInfo.totalZhangshangyouQualityFailItems += 1;
                }
                if ($scope.progressInfo.totalZhangshangyouQualitySuccessItems
                    + $scope.progressInfo.totalZhangshangyouQualityFailItems
                    < $scope.progressInfo.totalZhangshangyouQualityItems) {
                    $scope.dumpZhangshangyouQualityItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalZhangshangyouQualityItems = 0;
                    $scope.progressInfo.totalZhangshangyouQualitySuccessItems = 0;
                    $scope.progressInfo.totalZhangshangyouQualityFailItems = 0;
                }
            }, function () {
                $scope.progressInfo.totalZhangshangyouQualityFailItems += 1;
                if ($scope.progressInfo.totalZhangshangyouQualitySuccessItems
                    + $scope.progressInfo.totalZhangshangyouQualityFailItems
                    < $scope.progressInfo.totalZhangshangyouQualityItems) {
                    $scope.dumpZhangshangyouQualityItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalZhangshangyouQualityItems = 0;
                    $scope.progressInfo.totalZhangshangyouQualitySuccessItems = 0;
                    $scope.progressInfo.totalZhangshangyouQualityFailItems = 0;
                }
            });
        };

        $scope.dumpZhangshangyouCoverageItems = function () {
            alarmImportService.dumpSingleZhangshangyouCoverageItem().then(function (result) {
                if (result) {
                    $scope.progressInfo.totalZhangshangyouCoverageSuccessItems += 1;
                } else {
                    $scope.progressInfo.totalZhangshangyouCoverageFailItems += 1;
                }
                if ($scope.progressInfo.totalZhangshangyouCoverageSuccessItems
                    + $scope.progressInfo.totalZhangshangyouCoverageFailItems
                    < $scope.progressInfo.totalZhangshangyouCoverageItems) {
                    $scope.dumpZhangshangyouCoverageItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalZhangshangyouCoverageItems = 0;
                    $scope.progressInfo.totalZhangshangyouCoverageSuccessItems = 0;
                    $scope.progressInfo.totalZhangshangyouCoverageFailItems = 0;
                }
            }, function () {
                $scope.progressInfo.totalZhangshangyouCoverageFailItems += 1;
                if ($scope.progressInfo.totalZhangshangyouCoverageSuccessItems
                    + $scope.progressInfo.totalZhangshangyouCoverageFailItems
                    < $scope.progressInfo.totalZhangshangyouCoverageItems) {
                    $scope.dumpZhangshangyouCoverageItems();
                } else {
                    $scope.updateDumpHistory();

                    $scope.progressInfo.totalZhangshangyouCoverageItems = 0;
                    $scope.progressInfo.totalZhangshangyouCoverageSuccessItems = 0;
                    $scope.progressInfo.totalZhangshangyouCoverageFailItems = 0;
                }
            });
        };

        $scope.clearItems = function () {
            alarmImportService.clearImportItems().then(function () {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            });
        };

        $scope.clearCoverageItems = function () {
            alarmImportService.clearCoverageImportItems().then(function () {
                $scope.progressInfo.totalCoverageDumpItems = 0;
                $scope.progressInfo.totalCoverageSuccessItems = 0;
                $scope.progressInfo.totalCoverageFailItems = 0;
            });
        };

        $scope.clearZhangshangyouQualityItems = function () {
            alarmImportService.clearZhangshangyouQualityImportItems().then(function () {
                $scope.progressInfo.totalZhangshangyouQualityItems = 0;
                $scope.progressInfo.totalZhangshangyouQualitySuccessItems = 0;
                $scope.progressInfo.totalZhangshangyouQualityFailItems = 0;
            });
        };

        $scope.clearZhangshangyouCoverageItems = function () {
            alarmImportService.clearZhangshangyouCoverageItems().then(function () {
                $scope.progressInfo.totalZhangshangyouCoverageItems = 0;
                $scope.progressInfo.totalZhangshangyouCoverageSuccessItems = 0;
                $scope.progressInfo.totalZhangshangyouCoverageFailItems = 0;
            });
        };

        $scope.progressInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0,
            totalCoverageDumpItems: 0,
            totalCoverageSuccessItems: 0,
            totalCoverageFailItems: 0,
            totalZhangshangyouQualityItems: 0,
            totalZhangshangyouQualitySuccessItems: 0,
            totalZhangshangyouQualityFailItems: 0,
            totalZhangshangyouCoverageItems: 0,
            totalZhangshangyouCoverageSuccessItems: 0,
            totalZhangshangyouCoverageFailItems: 0
        };
        $scope.updateDumpHistory();
    });