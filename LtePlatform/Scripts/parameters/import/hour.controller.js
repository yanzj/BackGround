angular.module("myApp", ['app.common'])
    .controller("hour.import",
    function ($scope, basicImportService, neighborImportService, flowImportService, mapDialogService) {
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
            $scope.clearPrbItems = function () {
                flowImportService.clearDumpHourPrbs().then(function () {
                    $scope.prbInfo.totalDumpItems = 0;
                    $scope.prbInfo.totalSuccessItems = 0;
                    $scope.prbInfo.totalFailItems = 0;
                });
            };
            $scope.dumpPrbItems = function () {
                flowImportService.dumpHourPrb().then(function (result) {
                    neighborImportService.updateSuccessProgress(result, $scope.prbInfo, $scope.dumpPrbItems);
                    },
                    function () {
                        neighborImportService.updateFailProgress($scope.prbInfo, $scope.dumpPrbItems);
                    });
            };


        $scope.updateDumpHistory = function() {
            flowImportService.queryHourPrbs().then(function(result) {
                $scope.prbInfo.totalDumpItems = result;
            });
            flowImportService.queryHourDumpHistory($scope.beginDate.value, $scope.endDate.value).then(function(result) {
                $scope.dumpHistory = result;
            });
        };

            $scope.updateDumpHistory();

        });