angular.module('kpi.parameter.dump', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('dump.cell.mongo',
        function($scope,
            $uibModalInstance,
            dumpProgress,
            appFormatService,
            dumpPreciseService,
            neighborMongoService,
            preciseInterferenceService,
            networkElementService,
            dialogTitle,
            cell,
            begin,
            end) {
            $scope.dialogTitle = dialogTitle;

            $scope.dateRecords = [];
            $scope.currentDetails = [];
            $scope.currentIndex = 0;

            $scope.ok = function() {
                $uibModalInstance.close($scope.dateRecords);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.queryRecords = function() {
                $scope.mrsRsrpStats = [];
                $scope.mrsTaStats = [];
                angular.forEach($scope.dateRecords,
                    function(record) {
                        dumpProgress.queryExistedItems(cell.eNodebId, cell.sectorId, record.date)
                            .then(function(result) {
                                record.existedRecords = result;
                            });
                        dumpProgress.queryMongoItems(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mongoRecords = result;
                        });
                        dumpProgress.queryMrsRsrpItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsRsrpStats = result;
                            if (result) {
                                $scope.mrsRsrpStats.push({
                                    statDate: result.statDate,
                                    data: _.map(_.range(48),
                                        function(index) {
                                            return result['rsrP_' + appFormatService.prefixInteger(index, 2)];
                                        })
                                });
                            }

                        });
                        dumpProgress.queryMrsTadvItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsTaStats = result;
                            if (result) {
                                $scope.mrsTaStats.push({
                                    statDate: result.statDate,
                                    data: _.map(_.range(44),
                                        function(index) {
                                            return result['tadv_' + appFormatService.prefixInteger(index, 2)];
                                        })
                                });
                            }

                        });
                        dumpProgress.queryMrsPhrItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsPhrStats = result;
                        });
                        dumpProgress.queryMrsTadvRsrpItem(cell.eNodebId, cell.sectorId, record.date)
                            .then(function(result) {
                                record.mrsTaRsrpStats = result;
                            });
                    });
            };

            $scope.updateDetails = function(index) {
                $scope.currentIndex = index % $scope.dateRecords.length;
            };

            $scope.dumpAllRecords = function() {
                dumpPreciseService.dumpAllRecords($scope.dateRecords,
                    0,
                    0,
                    cell.eNodebId,
                    cell.sectorId,
                    $scope.queryRecords);
            };

            $scope.showNeighbors = function() {
                $scope.neighborCells = [];
                networkElementService.queryCellNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                    $scope.neighborCells = result;
                });

            };
            $scope.showReverseNeighbors = function() {
                neighborMongoService.queryReverseNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                    $scope.reverseCells = result;
                    angular.forEach(result,
                        function(neighbor) {
                            networkElementService.queryENodebInfo(neighbor.cellId).then(function(info) {
                                neighbor.eNodebName = info.name;
                            });
                        });
                });
            }
            $scope.updatePci = function() {
                networkElementService.updateCellPci(cell).then(function(result) {
                    $scope.updateMessages.push({
                        cellName: cell.name + '-' + cell.sectorId,
                        counts: result
                    });
                    $scope.showNeighbors();
                });
            };
            $scope.synchronizeNeighbors = function() {
                var count = 0;
                neighborMongoService.queryNeighbors(cell.eNodebId, cell.sectorId).then(function(neighbors) {
                    angular.forEach(neighbors,
                        function(neighbor) {
                            if (neighbor.neighborCellId > 0 && neighbor.neighborPci > 0) {
                                networkElementService.updateNeighbors(neighbor.cellId,
                                    neighbor.sectorId,
                                    neighbor.neighborPci,
                                    neighbor.neighborCellId,
                                    neighbor.neighborSectorId).then(function() {
                                    count += 1;
                                    if (count === neighbors.length) {
                                        $scope.updateMessages.push({
                                            cellName: $scope.currentCellName,
                                            counts: count
                                        });
                                        $scope.showNeighbors();
                                    }
                                });
                            } else {
                                count += 1;
                                if (count === neighbors.length) {
                                    $scope.updateMessages.push({
                                        cellName: $scope.currentCellName,
                                        counts: count
                                    });
                                    $scope.showNeighbors();
                                }
                            }
                        });
                });
            };

            var startDate = new Date(begin);
            while (startDate < end) {
                var date = new Date(startDate);
                $scope.dateRecords.push({
                    date: date,
                    existedRecords: 0,
                    existedStat: false
                });
                startDate.setDate(date.getDate() + 1);
            }
            $scope.neighborCells = [];
            $scope.updateMessages = [];

            $scope.queryRecords();
            $scope.showReverseNeighbors();
            $scope.showNeighbors();
        })
    .controller('neighbors.dialog',
        function($scope,
            $uibModalInstance,
            geometryService,
            dialogTitle,
            candidateNeighbors,
            currentCell) {
            $scope.pciNeighbors = [];
            $scope.indoorConsidered = false;
            $scope.distanceOrder = "distance";
            $scope.dialogTitle = dialogTitle;
            $scope.candidateNeighbors = candidateNeighbors;
            $scope.currentCell = currentCell;

            angular.forEach($scope.candidateNeighbors,
                function(neighbor) {
                    neighbor.distance = geometryService.getDistance($scope.currentCell.lattitute,
                        $scope.currentCell.longtitute,
                        neighbor.lattitute,
                        neighbor.longtitute);

                    $scope.pciNeighbors.push(neighbor);
                });

            $scope.updateNearestCell = function() {
                var minDistance = 10000;
                angular.forEach($scope.candidateNeighbors,
                    function(neighbor) {
                        if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
                            minDistance = neighbor.distance;
                            $scope.nearestCell = neighbor;
                        }
                    });

            };

            $scope.ok = function() {
                $scope.updateNearestCell();
                $uibModalInstance.close($scope.nearestCell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });