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
        $scope.sinrUlInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0
        };

        $scope.dumpHistory = [];
        $scope.dumpSinrHistory = [];

        $scope.townPreciseViews = [];
        $scope.collegePreciseStats = [];
        $scope.townPrecise800Views = [];
        $scope.townPrecise1800Views = [];
        $scope.townPrecise2100Views = [];
        $scope.townMrsStats = [];
        $scope.collegeMrsStats = [];
        $scope.townMrsStats800 = [];
        $scope.townMrsStats1800 = [];
        $scope.townMrsStats2100 = [];
        $scope.townSinrUlStats = [];
        $scope.collegeSinrUlStats = [];
        $scope.townSinrUlStats800 = [];
        $scope.townSinrUlStats1800 = [];
        $scope.townSinrUlStats2100 = [];

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
        $scope.clearSinrUlItems = function() {
            preciseImportService.clearMrsSinrUlItems().then(function () {
                $scope.sinrUlInfo.totalDumpItems = 0;
                $scope.sinrUlInfo.totalSuccessItems = 0;
                $scope.sinrUlInfo.totalFailItems = 0;
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
        $scope.dumpSinrUlItems = function () {
            preciseImportService.dumpSingleMrsSinrUlItem().then(function (result) {
                neighborImportService.updateSuccessProgress(result, $scope.sinrUlInfo, $scope.dumpSinrUlItems);
            }, function () {
                neighborImportService.updateFailProgress($scope.sinrUlInfo, $scope.dumpSinrUlItems);
            });
        };

        $scope.dumpTownItems = function () {
            preciseImportService.dumpTownItems(
                $scope.townPreciseViews, $scope.collegePreciseStats, 
                $scope.townPrecise800Views, $scope.townPrecise1800Views, $scope.townPrecise2100Views, 
                $scope.townMrsStats, $scope.collegeMrsStats, $scope.townMrsStats800,
                $scope.townMrsStats1800, $scope.townMrsStats2100
            ).then(function () {
                $scope.townPreciseViews = [];
                $scope.collegePreciseStats = [];
                $scope.townPrecise800Views = [];
                $scope.townPrecise1800Views = [];
                $scope.townPrecise2100Views = [];
                $scope.townMrsStats = [];
                $scope.collegeMrsStats = [];
                $scope.townMrsStats800 = [];
                $scope.townMrsStats1800 = [];
                $scope.townMrsStats2100 = [];
                $scope.updatePreciseHistory();
            });            
        };
        $scope.dumpTownSinrItems = function () {
            preciseImportService.dumpTownSinrItems(
                $scope.townSinrUlStats, $scope.collegeSinrUlStats, $scope.townSinrUlStats800,
                $scope.townSinrUlStats1800, $scope.townSinrUlStats2100
            ).then(function () {
                $scope.townSinrUlStats = [];
                $scope.collegeSinrUlStats = [];
                $scope.townSinrUlStats800 = [];
                $scope.townSinrUlStats1800 = [];
                $scope.townSinrUlStats2100 = [];
                $scope.updateSinrHistory();
            });
        };

        $scope.updatePreciseHistory = function() {
            preciseImportService.queryDumpHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function(result) {
                $scope.dumpHistory = result;
            });
        };

        $scope.updateSinrHistory = function () {
            preciseImportService.queryDumpSinrHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function (result) {
                $scope.dumpSinrHistory = result;
            });
        };

        $scope.updateHistory = function () {
            $scope.updatePreciseHistory();
            $scope.updateSinrHistory();
        };

        $scope.updateTownItems = function(date) {
            preciseImportService.queryTownPreciseViews(date, 'all').then(function(result) {
                $scope.townPreciseViews = result;
            });
            preciseImportService.queryCollegePreciseViews(date).then(function(result) {
                $scope.collegePreciseStats = result;
            });
            preciseImportService.queryTownPreciseViews(date, '800').then(function(result) {
                $scope.townPrecise800Views = result;
            });
            preciseImportService.queryTownPreciseViews(date, '1800').then(function(result) {
                $scope.townPrecise1800Views = result;
            });
            preciseImportService.queryTownPreciseViews(date, '2100').then(function(result) {
                $scope.townPrecise2100Views = result;
            });
        };
        $scope.updateTownMrsItems = function(date) {
            preciseImportService.queryTownMrsStats(date, 'all').then(function (result) {
                $scope.townMrsStats = result;
            });
            preciseImportService.queryCollegeMrsStats(date).then(function (result) {
                $scope.collegeMrsStats = result;
            });
            preciseImportService.queryTownMrsStats(date, '800').then(function (result) {
                $scope.townMrsStats800 = result;
            });
            preciseImportService.queryTownMrsStats(date, '1800').then(function (result) {
                $scope.townMrsStats1800 = result;
            });
            preciseImportService.queryTownMrsStats(date, '2100').then(function (result) {
                $scope.townMrsStats2100 = result;
            });
        };
        $scope.updateTownSinrUlItems = function(date) {
            preciseImportService.queryTownSinrUlStats(date, 'all').then(function (result) {
                $scope.townSinrUlStats = result;
            });
            preciseImportService.queryCollegeSinrUlStats(date).then(function (result) {
                $scope.collegeSinrUlStats = result;
            });
            preciseImportService.queryTownSinrUlStats(date, '800').then(function (result) {
                $scope.townSinrUlStats800 = result;
            });
            preciseImportService.queryTownSinrUlStats(date, '1800').then(function (result) {
                $scope.townSinrUlStats1800 = result;
            });
            preciseImportService.queryTownSinrUlStats(date, '2100').then(function (result) {
                $scope.townSinrUlStats2100 = result;
            });
        };
        $scope.updateTopMrsItems = function(date) {
            preciseImportService.queryTopMrsStats(date).then(function (result) {
                $scope.rsrpInfo.totalDumpItems = result;
            });
        };
        $scope.updateTopSinrUlItems = function (date) {
            preciseImportService.queryTopMrsSinrUlStats(date).then(function (result) {
                $scope.sinrUlInfo.totalDumpItems = result;
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
        preciseImportService.queryTotalMrsSinrUlItems().then(function (result) {
            $scope.sinrUlInfo.totalDumpItems = result;
        });

    });