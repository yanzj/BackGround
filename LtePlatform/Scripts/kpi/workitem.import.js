angular.module("myApp", ['app.common'])
    .controller("workitem.import", function($scope, dumpWorkItemService) {
        $scope.progressInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0
        };
        $scope.progressSpecialInfo = {
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
        $scope.clearSpecialItems = function () {
            dumpWorkItemService.clearSpecialImportItems().then(function () {
                $scope.progressSpecialInfo.totalDumpItems = 0;
                $scope.progressSpecialInfo.totalSuccessItems = 0;
                $scope.progressSpecialInfo.totalFailItems = 0;
            });
        };

        $scope.dumpItems = function () {
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

        $scope.dumpSpecialItems = function () {
            dumpWorkItemService.dumpSingleSpecialItem().then(function (result) {
                if (result) {
                    $scope.progressSpecialInfo.totalSuccessItems = $scope.progressSpecialInfo.totalSuccessItems + 1;
                } else {
                    $scope.progressSpecialInfo.totalFailItems = $scope.progressSpecialInfo.totalFailItems + 1;
                }
                if ($scope.progressSpecialInfo.totalSuccessItems + $scope.progressSpecialInfo.totalFailItems < $scope.progressSpecialInfo.totalDumpItems) {
                    $scope.dumpSpecialItems();
                } else {
                    $scope.progressSpecialInfo.totalDumpItems = 0;
                    $scope.progressSpecialInfo.totalSuccessItems = 0;
                    $scope.progressSpecialInfo.totalFailItems = 0;
                }
            }, function () {
                $scope.progressSpecialInfo.totalFailItems = $scope.progressSpecialInfo.totalFailItems + 1;
                if ($scope.progressSpecialInfo.totalSuccessItems + $scope.progressSpecialInfo.totalFailItems < $scope.progressSpecialInfo.totalDumpItems) {
                    $scope.dumpSpecialItems();
                } else {
                    $scope.progressSpecialInfo.totalDumpItems = 0;
                    $scope.progressSpecialInfo.totalSuccessItems = 0;
                    $scope.progressSpecialInfo.totalFailItems = 0;
                }
            });
        };

        dumpWorkItemService.queryTotalDumpItems().then(function(result) {
            $scope.progressInfo.totalDumpItems = result;
        });
        dumpWorkItemService.queryTotalSpecialDumpItems().then(function (result) {
            $scope.progressSpecialInfo.totalDumpItems = result;
        });
    });