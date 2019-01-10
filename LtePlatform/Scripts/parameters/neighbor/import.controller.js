angular.module("myApp", ['app.common'])
    .controller("neighbor.import",
        function($scope, neighborImportService, flowImportService, mapDialogService) {
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

            $scope.huaweiInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.clearHuaweiItems = function() {
                flowImportService.clearDumpHuaweis().then(function() {
                    $scope.huaweiInfo.totalDumpItems = 0;
                    $scope.huaweiInfo.totalSuccessItems = 0;
                    $scope.huaweiInfo.totalFailItems = 0;
                });
            };
            $scope.dumpHuaweiItems = function() {
                flowImportService.dumpHuaweiItem().then(function(result) {
                        neighborImportService.updateSuccessProgress(result, $scope.huaweiInfo, $scope.dumpHuaweiItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.huaweiInfo, $scope.dumpHuaweiItems);
                    });
            };

            $scope.huaweiCqiInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.clearCqiHuaweiItems = function() {
                flowImportService.clearDumpCqiHuaweis().then(function() {
                    $scope.huaweiCqiInfo.totalDumpItems = 0;
                    $scope.huaweiCqiInfo.totalSuccessItems = 0;
                    $scope.huaweiCqiInfo.totalFailItems = 0;
                });
            };
            $scope.dumpCqiHuaweiItems = function() {
                flowImportService.dumpHuaweiCqiItem().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.huaweiCqiInfo,
                            $scope.dumpCqiHuaweiItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.huaweiCqiInfo, $scope.dumpCqiHuaweiItems);
                    });
            };

            $scope.huaweiRssiInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.clearRssiHuaweiItems = function () {
                flowImportService.clearDumpRssiHuaweis().then(function () {
                    $scope.huaweiRssiInfo.totalDumpItems = 0;
                    $scope.huaweiRssiInfo.totalSuccessItems = 0;
                    $scope.huaweiRssiInfo.totalFailItems = 0;
                });
            };
            $scope.dumpRssiHuaweiItems = function () {
                flowImportService.dumpHuaweiRssiItem().then(function (result) {
                    neighborImportService.updateSuccessProgress(result,
                        $scope.huaweiRssiInfo,
                        $scope.dumpRssiHuaweiItems);
                },
                    function () {
                        neighborImportService.updateFailProgress($scope.huaweiRssiInfo, $scope.dumpRssiHuaweiItems);
                    });
            };

            $scope.zteInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };
            $scope.clearZteItems = function() {
                flowImportService.clearDumpHuaweis().then(function() {
                    $scope.zteInfo.totalDumpItems = 0;
                    $scope.zteInfo.totalSuccessItems = 0;
                    $scope.zteInfo.totalFailItems = 0;
                });
            };
            $scope.dumpZteItems = function() {
                flowImportService.dumpZteItem().then(function(result) {
                        neighborImportService.updateSuccessProgress(result, $scope.zteInfo, $scope.dumpZteItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.zteInfo, $scope.dumpZteItems);
                    });
            };

            $scope.updateDumpHistory = function() {
                flowImportService.queryHuaweiFlows().then(function(result) {
                    $scope.huaweiInfo.totalDumpItems = result;
                });
                flowImportService.queryHuaweiCqis().then(function(result) {
                    $scope.huaweiCqiInfo.totalDumpItems = result;
                });
                flowImportService.queryHuaweiRssis().then(function (result) {
                    $scope.huaweiRssiInfo.totalDumpItems = result;
                });
                flowImportService.queryZteFlows().then(function(result) {
                    $scope.zteInfo.totalDumpItems = result;
                });
                flowImportService.queryFlowDumpHistory($scope.beginDate.value, $scope.endDate.value).then(
                    function(result) {
                        $scope.dumpHistory = result;
                        angular.forEach(result,
                            function(record) {
                                if (record.huaweiItems > 9000 &&
                                    record.zteItems > 19000 &&
                                    (record.townRrcs === 0 ||
                                        record.townQcis === 0 ||
                                        record.townDoubleFlows < 44)) {
                                    flowImportService.dumpTownStats(record.dateString).then(function(count) {
                                        record.townRrcs = count[0];
                                        record.townQcis = count[1];
                                        record.townDoubleFlows = count[2];
                                    });
                                }

                                if (record.huaweiItems > 9000 &&
                                    record.zteItems > 19000 &&
                                    (record.townStats < 44 ||
                                        record.townStats2100 < 44 ||
                                        record.townStats1800 < 44 ||
                                        record.townStats800VoLte < 44)) {
                                    flowImportService.dumpTownFlows(record.dateString).then(function(count) {
                                        record.townStats = count[0];
                                        record.townStats2100 = count[1];
                                        record.townStats1800 = count[2];
                                        record.townStats800VoLte = count[3];
                                    });
                                }

                                if (record.huaweiCqis > 9000 &&
                                    record.zteCqis > 19000 &&
                                    (record.townCqis < 44 ||
                                        record.townCqis2100 < 44 ||
                                        record.townCqis1800 < 44 ||
                                        record.townCqis800VoLte < 44)) {
                                    flowImportService.dumpTownCqis(record.dateString).then(function(count) {
                                        record.townCqis = count[0];
                                        record.townCqis2100 = count[1];
                                        record.townCqis1800 = count[2];
                                        record.townCqis800VoLte = count[3];
                                    });
                                }
                                
                                if (record.huaweiPrbs > 9000 &&
                                    record.ztePrbs > 19000 &&
                                    (record.townPrbs < 44 ||
                                        record.townPrbs2100 < 44 ||
                                        record.townPrbs1800 < 44 ||
                                        record.townPrbs800VoLte < 44)) {
                                    flowImportService.dumpTownPrbs(record.dateString).then(function(count) {
                                        record.townPrbs = count[0];
                                        record.townPrbs2100 = count[1];
                                        record.townPrbs1800 = count[2];
                                        record.townPrbs800VoLte = count[3];
                                    });
                                }
                                
                                if (record.huaweiDoubleFlows > 9000 &&
                                    record.zteDoubleFlows > 19000 &&
                                    (record.townDoubleFlows < 44 ||
                                        record.townDoubleFlows2100 < 44 ||
                                        record.townDoubleFlows1800 < 44 ||
                                        record.townDoubleFlows800VoLte < 44)) {
                                    flowImportService.dumpTownDoubleFlows(record.dateString).then(function(count) {
                                        record.townDoubleFlows = count[0];
                                        record.townDoubleFlows2100 = count[1];
                                        record.townDoubleFlows1800 = count[2];
                                        record.townDoubleFlows800VoLte = count[3];
                                    });
                                }
                            });
                    });
            };
            $scope.updateDumpHistory();
        });