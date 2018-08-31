angular.module("myApp", ['app.common'])
    .controller('station.import',
        function($scope, basicImportService, neighborImportService) {
            
            $scope.stationInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.eNodebInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.cellInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.rruInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.antennaInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.distributionInfo = {
                totalSuccessItems: 0,
                totalFailItems: 0,
                totalDumpItems: 0
            };

            $scope.updateDumpHistory = function() {
                
                basicImportService.queryStationInfos().then(function(result) {
                    $scope.stationInfo.totalDumpItems = result;
                });
                basicImportService.queryStationENodebs().then(function (result) {
                    $scope.eNodebInfo.totalDumpItems = result;
                });
                basicImportService.queryStationCells().then(function (result) {
                    $scope.cellInfo.totalDumpItems = result;
                });
                basicImportService.queryStationRrus().then(function (result) {
                    $scope.rruInfo.totalDumpItems = result;
                });
                basicImportService.queryStationAntennas().then(function (result) {
                    $scope.antennaInfo.totalDumpItems = result;
                });
                basicImportService.queryStationDistributions().then(function (result) {
                    $scope.distributionInfo.totalDumpItems = result;
                });
            };

            $scope.dumpStationItems = function() {
                basicImportService.dumpStationInfo().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.stationInfo,
                            $scope.dumpStationItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.stationInfo, $scope.dumpStationItems);
                    });
            };

            $scope.dumpENodebItems = function() {
                basicImportService.dumpStationENodeb().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.eNodebInfo,
                            $scope.dumpENodebItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.eNodebInfo, $scope.dumpENodebItems);
                    });
            };

            $scope.dumpCellItems = function() {
                basicImportService.dumpStationCell().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.cellInfo,
                            $scope.dumpCellItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.cellInfo, $scope.dumpCellItems);
                    });
            };

            $scope.dumpRruItems = function() {
                basicImportService.dumpStationRru().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.rruInfo,
                            $scope.dumpRruItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.rruInfo, $scope.dumpRruItems);
                    });
            };

            $scope.dumpAntennaItems = function() {
                basicImportService.dumpStationAntenna().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.antennaInfo,
                            $scope.dumpAntennaItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.antennaInfo, $scope.dumpAntennaItems);
                    });
            };

            $scope.dumpDistributionItems = function() {
                basicImportService.dumpStationDistribution().then(function(result) {
                        neighborImportService.updateSuccessProgress(result,
                            $scope.distributionInfo,
                            $scope.dumpDistributionItems);
                    },
                    function() {
                        neighborImportService.updateFailProgress($scope.distributionInfo, $scope.dumpDistributionItems);
                    });
            };
            
            $scope.updateDumpHistory();

        });