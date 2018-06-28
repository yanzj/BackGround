angular.module('region.mongo', ['app.core'])
    .factory('neighborMongoService',
        function(generalHttpService) {
            return {
                queryNeighbors: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('NeighborCellMongo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryReverseNeighbors: function(destENodebId, destSectorId) {
                    return generalHttpService.getApiData('NeighborCellMongo',
                    {
                        destENodebId: destENodebId,
                        destSectorId: destSectorId
                    });
                }
            };
        })
    .factory('cellHuaweiMongoService',
        function(generalHttpService) {
            return {
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('CellHuaweiMongo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryLocalCellDef: function(eNodebId) {
                    return generalHttpService.getApiData('CellHuaweiMongo',
                    {
                        eNodebId: eNodebId
                    });
                }
            };
        })
    .factory('cellPowerService',
        function(generalHttpService) {
            return {
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('CellPower',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryUlOpenLoopPowerControll: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('UplinkOpenLoopPowerControl',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        })
    .factory('intraFreqHoService',
        function(generalHttpService) {
            return {
                queryENodebParameters: function(eNodebId) {
                    return generalHttpService.getApiData('IntraFreqHo',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('IntraFreqHo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        })
    .factory('interFreqHoService',
        function(generalHttpService) {
            return {
                queryENodebParameters: function(eNodebId) {
                    return generalHttpService.getApiData('InterFreqHo',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('InterFreqHo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        });