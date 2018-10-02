angular.module('region.network', ['app.core'])
    .factory('networkElementService', function (generalHttpService) {
        return {
            queryCellInfo: function (cellId, sectorId) {
                return generalHttpService.getApiData('Cell', {
                    eNodebId: cellId,
                    sectorId: sectorId
                });
            },
            queryLteRruFromCellName: function (cellName) {
                return generalHttpService.getApiData('LteRru', {
                    cellName: cellName
                });
            },
            queryLteRrusFromPlanNum: function (planNum) {
                return generalHttpService.getApiData('CellStation', {
                    planNum: planNum
                });
            },
            queryCellInfosInOneENodeb: function (eNodebId) {
                return generalHttpService.getApiData('Cell', {
                    eNodebId: eNodebId
                });
            },
            queryCellInfosInOneENodebUse: function (eNodebId) {
                return generalHttpService.getApiData('CellInUse', {
                    eNodebId: eNodebId
                });
            },
            queryCellViewsInOneENodeb: function (eNodebId) {
                return generalHttpService.getApiData('Cell', {
                    cellId: eNodebId
                });
            },
            queryCellStationInfoByRruName: function (rruName) {
                return generalHttpService.getApiData('CellStation', {
                    rruName: rruName
                });
            },
            queryCellSectorIds: function (name) {
                return generalHttpService.getApiData('Cell', {
                    eNodebName: name
                });
            },
            queryCdmaSectorIds: function (name) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsName: name
                });
            },
            queryCdmaCellViews: function (name) {
                return generalHttpService.getApiData('CdmaCell', {
                    name: name
                });
            },
            queryCdmaCellInfo: function (btsId, sectorId) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId,
                    sectorId: sectorId
                });
            },
            queryCdmaCellInfoWithType: function (btsId, sectorId, cellType) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId,
                    sectorId: sectorId,
                    cellType: cellType
                });
            },
            queryCdmaCellInfosInOneBts: function (btsId) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId
                });
            },
            queryENodebInfo: function (eNodebId) {
                return generalHttpService.getApiData('ENodeb', {
                    eNodebId: eNodebId
                });
            },
            queryENodebsInOneTown: function (city, district, town) {
                return generalHttpService.getApiData('ENodeb', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryENodebsInOneDistrict: function (city, district) {
                return generalHttpService.getApiData('ENodeb', {
                    city: city,
                    district: district
                });
            },
            queryRangeENodebs: function (container) {
                return generalHttpService.postApiData('ENodeb', container);
            },
            queryENodebsByGeneralName: function (name) {
                return generalHttpService.getApiData('ENodeb', {
                    name: name
                });
            },
            queryInRangeENodebs: function(west, east, south, north) {
                return generalHttpService.getApiData('ENodeb', {
                    west: west,
                    east: east,
                    south: south,
                    north: north
                });
            },
            queryENodebByPlanNum: function (planNum) {
                return generalHttpService.getApiData('ENodebQuery', {
                    planNum: planNum
                });
            },
            queryENodebsByTownArea: function(city, district, town) {
                return generalHttpService.getApiData('ENodebQuery',
                {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryBtssByTownArea: function (city, district, town) {
                return generalHttpService.getApiData('BtsQuery',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
            },
            updateENodebTownInfo: function(view) {
                return generalHttpService.getApiData('ENodebQuery',
                    {
                        eNodebId: view.eNodebId,
                        townId: view.townId
                    });
            },
            updateBtsTownInfo: function (view) {
                return generalHttpService.getApiData('BtsQuery',
                    {
                        btsId: view.btsId,
                        townId: view.townId
                    });
            },
            queryStationByENodeb: function (eNodebId, planNum) {
                return generalHttpService.getApiData('ENodebStation', {
                    eNodebId: eNodebId,
                    planNum: planNum
                });
            },
            queryENodebsInOneTownUse: function (city, district, town) {
                return generalHttpService.getApiData('ENodebInUse', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryDistributionsInOneDistrict: function (district) {
                return generalHttpService.getApiData('Distribution', {
                    district: district
                });
            },
            queryENodebsByGeneralNameInUse: function (name) {
                return generalHttpService.getApiData('ENodebInUse', {
                    name: name
                });
            },
            queryBtsInfo: function (btsId) {
                return generalHttpService.getApiData('Bts', {
                    btsId: btsId
                });
            },
            queryBtssInOneTown: function (city, district, town) {
                return generalHttpService.getApiData('Bts', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryBtssByGeneralName: function (name) {
                return generalHttpService.getApiData('Bts', {
                    name: name
                });
            },
            queryCellSectors: function (cells) {
                return generalHttpService.postApiData('Cell', {
                    views: cells
                });
            },
            queryRangeSectors: function (range, excludedIds) {
                return generalHttpService.postApiData('SectorView', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north,
                    excludedCells: excludedIds
                });
            },
            queryRangeCells: function (range) {
                return generalHttpService.getApiData('Cell', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangePlanningSites: function (range) {
                return generalHttpService.getApiData('PlanningSiteRange', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeDistributions: function (range) {
                return generalHttpService.getApiData('Distribution', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeComplains: function (range) {
                return generalHttpService.getApiData('OnlineSustain', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeBtss: function (container) {
                return generalHttpService.postApiData('Bts', container);
            },
            queryCellNeighbors: function (cellId, sectorId) {
                return generalHttpService.getApiData('NearestPciCell', {
                    'cellId': cellId,
                    'sectorId': sectorId
                });
            },
            querySystemNeighborCell: function (cellId, sectorId, pci) {
                return generalHttpService.getApiData('NearestPciCell', {
                    'cellId': cellId,
                    'sectorId': sectorId,
                    'pci': pci
                });
            },
            updateCellPci: function (cell) {
                return generalHttpService.postApiData('NearestPciCell', cell);
            },
            queryNearestCells: function (eNodebId, sectorId, pci) {
                return generalHttpService.getApiData('Cell', {
                    'eNodebId': eNodebId,
                    'sectorId': sectorId,
                    'pci': pci
                });
            },
            queryNearestCellsWithFrequency: function (eNodebId, sectorId, pci, frequency) {
                return generalHttpService.getApiData('Cell', {
                    'eNodebId': eNodebId,
                    'sectorId': sectorId,
                    'pci': pci,
                    'frequency': frequency
                });
            },
            updateNeighbors: function (cellId, sectorId, pci, nearestCellId, nearestSectorId) {
                return generalHttpService.putApiData('NearestPciCell', {
                    cellId: cellId,
                    sectorId: sectorId,
                    pci: pci,
                    nearestCellId: nearestCellId,
                    nearestSectorId: nearestSectorId
                });
            },
            queryOutdoorCellSites: function (city, district) {
                return generalHttpService.getApiData('OutdoorCellSite', {
                    city: city,
                    district: district
                });
            },
            queryVolteCellSites: function (city, district) {
                return generalHttpService.getApiData('VoLteCellSite', {
                    city: city,
                    district: district
                });
            },
            queryNbIotCellSites: function (city, district) {
                return generalHttpService.getApiData('NbIotCellSite', {
                    city: city,
                    district: district
                });
            },
            queryIndoorCellSites: function (city, district) {
                return generalHttpService.getApiData('IndoorCellSite', {
                    city: city,
                    district: district
                });
            },
            queryPlanningSites: function (city, district) {
                return generalHttpService.getApiData('PlanningSite', {
                    city: city,
                    district: district
                });
            },
            queryOpenningSites: function (city, district) {
                return generalHttpService.getApiData('OpenningSite', {
                    city: city,
                    district: district
                });
            },
            queryOpennedSites: function (city, district) {
                return generalHttpService.getApiData('OpennedSite', {
                    city: city,
                    district: district
                });
            }
        };
    })
    .factory('neighborImportService', function (geometryService, networkElementService) {
        return {
            updateSuccessProgress: function (result, progressInfo, callback) {
                if (result) {
                    progressInfo.totalSuccessItems += 1;
                } else {
                    progressInfo.totalFailItem += 1;
                }
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    callback();
                } else {
                    progressInfo.totalDumpItems = 0;
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            },
            updateFailProgress: function (progressInfo, callback) {
                progressInfo.totalFailItems += 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    callback();
                } else {
                    progressInfo.totalDumpItems = 0;
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            },
            updateCellRruInfo: function (supplementCells, settings) {
                angular.forEach(settings.dstCells, function (dstCell) {
                    var i;
                    for (i = 0; i < settings.cells.length; i++) {
                        if (dstCell.cellName === settings.cells[i].eNodebName + '-' + settings.cells[i].sectorId) {
                            break;
                        }
                    }
                    if (i === settings.cells.length) {
                        dstCell.distance = geometryService.getDistance(settings.lattitute, settings.longtitute, dstCell.lattitute, dstCell.longtitute);
                        networkElementService.queryLteRruFromCellName(dstCell.cellName).then(function (rru) {
                            dstCell.rruName = rru ? rru.rruName : '';
                            supplementCells.push(dstCell);
                        });
                    }
                });
            },
            updateENodebRruInfo: function (supplementCells, settings) {
                angular.forEach(settings.dstCells, function (item) {
                    var i;
                    for (i = 0; i < settings.cells.length; i++) {
                        if (settings.cells[i].eNodebId === item.eNodebId && settings.cells[i].sectorId === item.sectorId) {
                            break;
                        }
                    }
                    if (i === settings.cells.length) {
                        networkElementService.queryCellInfo(item.eNodebId, item.sectorId).then(function (view) {
                            view.distance = geometryService.getDistance(settings.lattitute, settings.longtitute, item.lattitute, item.longtitute);
                            networkElementService.queryLteRruFromCellName(view.cellName).then(function (rru) {
                                view.rruName = rru ? rru.rruName : '';
                                supplementCells.push(view);
                            });
                        });
                    }
                });
            },
            generateRange: function(range, center, coors) {
                return {
                    west: range.west + center.X - coors.x,
                    east: range.east + center.X - coors.x,
                    south: range.south + center.Y - coors.y,
                    north: range.north + center.Y - coors.y
                };
            },
            generateRangeWithExcludedIds: function(range, center, coors, ids) {
                return {
                    west: range.west + center.X - coors.x,
                    east: range.east + center.X - coors.x,
                    south: range.south + center.Y - coors.y,
                    north: range.north + center.Y - coors.y,
                    excludedIds: ids
                };
            }
        }
    })
    .factory('dumpProgress', function(generalHttpService, appFormatService) {
        var serviceInstance = {};

        serviceInstance.clear = function(progressInfo) {
            generalHttpService.deleteApiData('DumpInterference').then(function() {
                progressInfo.totalDumpItems = 0;
                progressInfo.totalSuccessItems = 0;
                progressInfo.totalFailItems = 0;
            });
        };

        serviceInstance.dump = function(actionUrl, progressInfo) {
            var self = serviceInstance;
            generalHttpService.putApiData(actionUrl, {}).then(function(result) {
                if (result === true) {
                    progressInfo.totalSuccessItems = progressInfo.totalSuccessItems + 1;
                } else {
                    progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                }
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            }, function() {
                progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            });
        };

        serviceInstance.dumpMongo = function(stat) {
            return generalHttpService.postApiData('DumpInterference', stat);
        };

        serviceInstance.dumpCellStat = function(stat) {
            return generalHttpService.postApiData('DumpCellStat', stat);
        };

        serviceInstance.resetProgress = function(begin, end) {
            return generalHttpService.getApiData('DumpInterference', {
                'begin': begin,
                'end': end
            });
        };
        
        serviceInstance.queryNeighborMongoItem = function(eNodebId, sectorId, neighborPci, date) {
            return generalHttpService.getApiData('InterferenceMatrix', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                neighborPci: neighborPci,
                date: date
            });
        };
        
        return serviceInstance;
    })
    .factory('dumpPreciseService', function(authorizeService) {
        var serviceInstance = {};
        serviceInstance.generateUsersDistrict = function(city, districts, callback) {
            if (city) {
                authorizeService.queryCurrentUserName().then(function(userName) {
                    authorizeService.queryRolesInUser(userName).then(function (roles) {
                        var roleDistricts = authorizeService.queryRoleDistricts(roles);
                        angular.forEach(roleDistricts, function (district, $index) {
                            districts.push(district);
                            if (callback) {
                                callback(district, $index);
                            }
                        });
                    });
                });
            }
        };

        return serviceInstance;
    });