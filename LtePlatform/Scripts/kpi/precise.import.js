angular.module("myApp", ['app.common'])
    .controller("precise.import", function ($scope, preciseImportService, neighborImportService) {
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
        $scope.rsrpInfo = {
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
            });
        };
        $scope.clearRsrpItems = function () {
            preciseImportService.clearMrsRsrpItems().then(function () {
                $scope.rsrpInfo.totalDumpItems = 0;
                $scope.rsrpInfo.totalSuccessItems = 0;
                $scope.rsrpInfo.totalFailItems = 0;
            });
        };

        $scope.dumpItems = function() {
            preciseImportService.dumpSingleItem().then(function(result) {
                neighborImportService.updateSuccessProgress(result, $scope.progressInfo, $scope.dumpItems);
            }, function() {
                neighborImportService.updateFailProgress($scope.progressInfo, $scope.dumpItems);
            });
        };
        $scope.dumpRsrpItems = function () {
            preciseImportService.dumpSingleMrsRsrpItem().then(function (result) {
                neighborImportService.updateSuccessProgress(result, $scope.rsrpInfo, $scope.dumpRsrpItems);
            }, function () {
                neighborImportService.updateFailProgress($scope.rsrpInfo, $scope.dumpRsrpItems);
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
                $scope.rsrpInfo.totalDumpItems = result;
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
        preciseImportService.queryTotalMrsRsrpItems().then(function (result) {
            $scope.rsrpInfo.totalDumpItems = result;
        });

    });