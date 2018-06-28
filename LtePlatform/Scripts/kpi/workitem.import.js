angular.module("myApp", ['app.common'])
    .controller("workitem.import", function($scope, dumpWorkItemService) {
        $scope.progressInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0
        };
        $scope.clearItems = function() {
            dumpWorkItemService.clearImportItems().then(function() {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
            });
        };
        $scope.dumpItems = function() {
            dumpWorkItemService.dumpSingleItem().then(function(result) {
                if (result) {
                    $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
                } else {
                    $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                }
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            }, function() {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            });
        };

        dumpWorkItemService.queryTotalDumpItems().then(function(result) {
            $scope.progressInfo.totalDumpItems = result;
        });
    });