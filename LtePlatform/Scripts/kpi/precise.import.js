angular.module("myApp", ['app.common'])
    .controller("precise.import", function($scope, preciseImportService) {
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
        $scope.progressInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0
        };
        $scope.dumpHistory = [];
        $scope.townPreciseViews = [];
        $scope.townMrsStats = [];
        $scope.topMrsStats = [];

        $scope.clearItems = function() {
            preciseImportService.clearImportItems().then(function() {
                $scope.progressInfo.totalDumpItems = 0;
                $scope.progressInfo.totalSuccessItems = 0;
                $scope.progressInfo.totalFailItems = 0;
                $scope.townPreciseViews = [];
            });
        };
        $scope.dumpTownItems = function() {
            preciseImportService.dumpTownItems($scope.townPreciseViews, $scope.townMrsStats, $scope.topMrsStats).then(function() {
                $scope.townPreciseViews = [];
                $scope.townMrsStats = [];
                $scope.topMrsStats = [];
                $scope.updateHistory();
            });
        };
        $scope.updateHistory = function() {
            preciseImportService.queryDumpHistroy($scope.beginDate.value, $scope.endDate.value).then(function(result) {
                $scope.dumpHistory = result;
            });
        };
        $scope.dumpItems = function() {
            preciseImportService.dumpSingleItem().then(function(result) {
                if (result) {
                    $scope.progressInfo.totalSuccessItems = $scope.progressInfo.totalSuccessItems + 1;
                } else {
                    $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                }
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.updateHistory();

                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            }, function() {
                $scope.progressInfo.totalFailItems = $scope.progressInfo.totalFailItems + 1;
                if ($scope.progressInfo.totalSuccessItems + $scope.progressInfo.totalFailItems < $scope.progressInfo.totalDumpItems) {
                    $scope.dumpItems();
                } else {
                    $scope.updateHistory();

                    $scope.progressInfo.totalDumpItems = 0;
                    $scope.progressInfo.totalSuccessItems = 0;
                    $scope.progressInfo.totalFailItems = 0;
                }
            });
        };
        $scope.updateTownItems = function(date) {
            preciseImportService.queryTownPreciseViews(date).then(function(result) {
                $scope.townPreciseViews = result;
            });
        };
        $scope.updateTownMrsItems = function(date) {
            preciseImportService.queryTownMrsStats(date).then(function (result) {
                $scope.townMrsStats = result;
            });
        };
        $scope.updateTopMrsItems = function(date) {
            preciseImportService.queryTopMrsStats(date).then(function (result) {
                $scope.topMrsStats = result;
            });
        };
        $scope.updateMongoItems = function(date) {
            preciseImportService.updateMongoItems(date).then(function(result) {
                $scope.progressInfo.totalDumpItems = result;
            });
        };

        $scope.updateHistory();

        preciseImportService.queryTotalDumpItems().then(function(result) {
            $scope.progressInfo.totalDumpItems = result;
        });

    });