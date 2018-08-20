angular.module("myApp", ['app.common'])
    .controller("hour.import",
        function($scope, basicImportService, neighborImportService, flowImportService, mapDialogService) {

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
                flowImportService.queryHourPrbs().then(function (result) {
                    $scope.prbInfo.totalDumpItems = result;
                });
            }

            $scope.updateDumpHistory();

        });