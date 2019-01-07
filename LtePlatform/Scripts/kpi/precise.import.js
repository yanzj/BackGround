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
        $scope.taInfo = {
            totalDumpItems: 0,
            totalSuccessItems: 0,
            totalFailItems: 0
        };

        $scope.dumpHistory = [];
        $scope.dumpRsrpHistory = [];
        $scope.dumpSinrHistory = [];
        $scope.dumpTaHistory = [];

        $scope.townPreciseViews = [];
        $scope.collegePreciseStats = [];
        $scope.marketPreciseStats = [];
        $scope.townPrecise800Views = [];
        $scope.townPrecise1800Views = [];
        $scope.townPrecise2100Views = [];

        $scope.townMrsStats = [];
        $scope.collegeMrsStats = [];
        $scope.marketMrsStats = [];
        $scope.townMrsStats800 = [];
        $scope.townMrsStats1800 = [];
        $scope.townMrsStats2100 = [];

        $scope.townSinrUlStats = [];
        $scope.collegeSinrUlStats = [];
        $scope.marketSinrUlStats = [];
        $scope.townSinrUlStats800 = [];
        $scope.townSinrUlStats1800 = [];
        $scope.townSinrUlStats2100 = [];
        
        $scope.townTaStats = [];
        $scope.collegeTaStats = [];
        $scope.marketTaStats = [];
        $scope.townTaStats800 = [];
        $scope.townTaStats1800 = [];
        $scope.townTaStats2100 = [];

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
        $scope.clearTaItems = function() {
            preciseImportService.clearMrsTadvItems().then(function () {
                $scope.taInfo.totalDumpItems = 0;
                $scope.taInfo.totalSuccessItems = 0;
                $scope.taInfo.totalFailItems = 0;
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
        $scope.dumpTaItems = function () {
            preciseImportService.dumpSingleMrsTadvItem().then(function (result) {
                neighborImportService.updateSuccessProgress(result, $scope.taInfo, $scope.dumpTaItems);
            }, function () {
                neighborImportService.updateFailProgress($scope.taInfo, $scope.dumpTaItems);
            });
        };

        $scope.dumpTownItems = function () {
            preciseImportService.dumpTownItems(
                $scope.townPreciseViews, $scope.collegePreciseStats, $scope.marketPreciseStats,
                $scope.townPrecise800Views, $scope.townPrecise1800Views, $scope.townPrecise2100Views
            ).then(function () {
                $scope.townPreciseViews = [];
                $scope.collegePreciseStats = [];
                $scope.marketPreciseStats = [];
                $scope.townPrecise800Views = [];
                $scope.townPrecise1800Views = [];
                $scope.townPrecise2100Views = [];
                $scope.updatePreciseHistory();
            });            
        };
        $scope.dumpTownRsrpItems = function () {
            preciseImportService.dumpTownRsrpItems( 
                $scope.townMrsStats, $scope.collegeMrsStats, $scope.marketMrsStats,
                $scope.townMrsStats800, $scope.townMrsStats1800, $scope.townMrsStats2100
            ).then(function () {
                $scope.townMrsStats = [];
                $scope.collegeMrsStats = [];
                $scope.marketMrsStats = [];
                $scope.townMrsStats800 = [];
                $scope.townMrsStats1800 = [];
                $scope.townMrsStats2100 = [];
                $scope.updateRsrpHistory();
            });            
        };
        $scope.dumpTownSinrItems = function () {
            preciseImportService.dumpTownSinrItems(
                $scope.townSinrUlStats, $scope.collegeSinrUlStats, $scope.marketSinrUlStats,
                $scope.townSinrUlStats800, $scope.townSinrUlStats1800, $scope.townSinrUlStats2100
            ).then(function () {
                $scope.townSinrUlStats = [];
                $scope.collegeSinrUlStats = [];
                $scope.marketSinrUlStats = [];
                $scope.townSinrUlStats800 = [];
                $scope.townSinrUlStats1800 = [];
                $scope.townSinrUlStats2100 = [];
                $scope.updateSinrHistory();
            });
        };
        $scope.dumpTownTaItems = function () {
            preciseImportService.dumpTownTaItems(
                $scope.townTaStats, $scope.collegeTaStats, $scope.marketTaStats,
                $scope.townTaStats800, $scope.townTaStats1800, $scope.townTaStats2100
            ).then(function () {
                $scope.townTaStats = [];
                $scope.collegeTaStats = [];
                $scope.marketTaStats = [];
                $scope.townTaStats800 = [];
                $scope.townTaStats1800 = [];
                $scope.townTaStats2100 = [];
                $scope.updateTaHistory();
            });
        };

        $scope.updatePreciseHistory = function() {
            preciseImportService.queryDumpHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function(result) {
                $scope.dumpHistory = result;
            });
        };
        
        $scope.updateRsrpHistory = function() {
            preciseImportService.queryDumpRsrpHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function(result) {
                $scope.dumpRsrpHistory = result;
            });
        };

        $scope.updateSinrHistory = function () {
            preciseImportService.queryDumpSinrHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function (result) {
                $scope.dumpSinrHistory = result;
            });
        };
        
        $scope.updateTaHistory = function () {
            preciseImportService.queryDumpTaHistroy(
                $scope.beginDate.value, $scope.endDate.value
            ).then(function (result) {
                $scope.dumpTaHistory = result;
            });
        };

        $scope.updateHistory = function () {
            $scope.updatePreciseHistory();
            $scope.updateRsrpHistory();
            $scope.updateSinrHistory();
            $scope.updateTaHistory();
        };

        $scope.updateTownItems = function(date) {
            preciseImportService.queryTownPreciseViews(date, 'all').then(function(result) {
                $scope.townPreciseViews = result;
            });
            preciseImportService.queryCollegePreciseViews(date).then(function(result) {
                $scope.collegePreciseStats = result;
            });
            preciseImportService.queryMarketPreciseViews(date).then(function(result) {
                $scope.marketPreciseStats = result;
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
            preciseImportService.queryMarketMrsStats(date).then(function (result) {
                $scope.marketMrsStats = result;
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
            preciseImportService.queryMarketSinrUlStats(date).then(function (result) {
                $scope.marketSinrUlStats = result;
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
        $scope.updateTownTaItems = function(date) {
            preciseImportService.queryTownTaStats(date, 'all').then(function (result) {
                $scope.townTaStats = result;
            });
            preciseImportService.queryCollegeTaStats(date).then(function (result) {
                $scope.collegeTaStats = result;
            });
            preciseImportService.queryMarketTaStats(date).then(function (result) {
                $scope.marketTaStats = result;
            });
            preciseImportService.queryTownTaStats(date, '800').then(function (result) {
                $scope.townTaStats800 = result;
            });
            preciseImportService.queryTownTaStats(date, '1800').then(function (result) {
                $scope.townTaStats1800 = result;
            });
            preciseImportService.queryTownTaStats(date, '2100').then(function (result) {
                $scope.townTaStats2100 = result;
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
        $scope.updateTopTaItems = function (date) {
            preciseImportService.queryTopMrsTadvStats(date).then(function (result) {
                $scope.taInfo.totalDumpItems = result;
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
        preciseImportService.queryTotalMrsTadvItems().then(function (result) {
            $scope.taInfo.totalDumpItems = result;
        });

    });