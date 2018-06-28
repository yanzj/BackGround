angular.module('region.precise', ['app.core'])
    .factory('workitemService',
        function(generalHttpService) {
            return {
                queryWithPaging: function(state, type) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        'statCondition': state,
                        'typeCondition': type
                    });
                },
                queryWithPagingByDistrict: function(state, type, district) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        statCondition: state,
                        typeCondition: type,
                        district: district
                    });
                },
                querySingleItem: function(serialNumber) {
                    return generalHttpService.getApiData('WorkItem',
                    {
                        serialNumber: serialNumber
                    });
                },
                signIn: function(serialNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        signinNumber: serialNumber
                    });
                },
                queryChartData: function(chartType) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        chartType: chartType
                    });
                },
                updateSectorIds: function() {
                    return generalHttpService.putApiData('WorkItem', {});
                },
                feedback: function(message, serialNumber) {
                    return generalHttpService.postApiDataWithHeading('WorkItem',
                    {
                        message: message,
                        serialNumber: serialNumber
                    });
                },
                finish: function(comments, finishNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        finishNumber: finishNumber,
                        comments: comments
                    });
                },
                queryByENodebId: function(eNodebId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryByCellId: function(eNodebId, sectorId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryCurrentMonth: function() {
                    return generalHttpService.getApiData('WorkItemCurrentMonth', {});
                },
                constructPreciseItem: function(cell, begin, end) {
                    return generalHttpService.postApiDataWithHeading('PreciseWorkItem',
                    {
                        view: cell,
                        begin: begin,
                        end: end
                    });
                }
            };
        })
    .factory('dumpWorkItemService',
        function(generalHttpService) {
            return {
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpWorkItem', {});
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('DumpWorkItem');
                },
                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('DumpWorkItem', {});
                }
            };
        })
    .factory('preciseWorkItemService',
        function(generalHttpService) {
            return {
                queryByDateSpanDistrict: function(begin, end, district) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end,
                        district: district
                    });
                },
                queryByDateSpan: function(begin, end) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryBySerial: function(number) {
                    return generalHttpService.getApiDataWithHeading('PreciseWorkItem',
                    {
                        number: number
                    });
                },
                updateInterferenceNeighbor: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceNeighborWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateInterferenceVictim: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceVictimWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateCoverage: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('CoverageWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                }
            };
        })
    .factory('preciseImportService',
        function(generalHttpService) {
            return {
                queryDumpHistroy: function(beginDate, endDate) {
                    return generalHttpService.getApiData('PreciseImport',
                    {
                        begin: beginDate,
                        end: endDate
                    });
                },
                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('PreciseImport', {});
                },
                queryTownPreciseViews: function(statTime) {
                    return generalHttpService.getApiData('TownPreciseImport',
                    {
                        statTime: statTime
                    });
                },
                queryTownMrsStats: function (statTime) {
                    return generalHttpService.getApiData('TownPreciseImport',
                        {
                            statDate: statTime
                        });
                },
                queryTopMrsStats: function (statTime) {
                    return generalHttpService.getApiData('TownPreciseImport',
                        {
                            topDate: statTime
                        });
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('PreciseImport', {});
                },
                dumpTownItems: function(views, mrsStats, topMrsStats) {
                    return generalHttpService.postApiData('TownPreciseImport',
                    {
                        views: views,
                        mrsRsrps: mrsStats,
                        topMrsRsrps: topMrsStats
                    });
                },
                dumpTownAgpsItems: function(views) {
                    return generalHttpService.postApiData('MrGrid',
                    {
                        views: views
                    });
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('PreciseImport', {});
                },
                updateMongoItems: function(statDate) {
                    return generalHttpService.getApiData('PreciseMongo',
                    {
                        statDate: statDate
                    });
                }
            };
        })
    .factory('cellPreciseService',
        function(generalHttpService) {
            return {
                queryDataSpanKpi: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryOneWeekKpi: function(cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                }
            };
        })
    .factory('appRegionService',
        function(generalHttpService) {
            return {
                initializeCities: function() {
                    return generalHttpService.getApiData('CityList', {});
                },
                queryDistricts: function(cityName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName
                    });
                },
                queryDistrictInfrastructures: function(cityName) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName
                    });
                },
                queryDistrictIndoorCells: function(cityName) {
                    return generalHttpService.getApiData('DistrictIndoorCells',
                    {
                        city: cityName
                    });
                },
                queryDistrictBandCells: function(cityName) {
                    return generalHttpService.getApiData('DistrictBandCells',
                    {
                        city: cityName
                    });
                },
                queryTowns: function(cityName, districtName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName,
                        district: districtName
                    });
                },
                queryTownInfrastructures: function(cityName, districtName) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName,
                        district: districtName
                    });
                },
                queryTownLteCount: function(cityName, districtName, townName, isIndoor) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName,
                        district: districtName,
                        town: townName,
                        isIndoor: isIndoor
                    });
                },
                queryTown: function(city, district, town) {
                    return generalHttpService.getApiData('Town',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryTownBoundaries: function(city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                isInTownBoundary: function(longtitute, lattitute, city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        longtitute: longtitute,
                        lattitute: lattitute,
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryAreaBoundaries: function() {
                    return generalHttpService.getApiData('AreaBoundary', {});
                },
                queryENodebTown: function(eNodebId) {
                    return generalHttpService.getApiData('Town',
                    {
                        eNodebId: eNodebId
                    });
                },
                accumulateCityStat: function(stats, cityName) {
                    var cityStat = {
                        district: cityName,
                        totalLteENodebs: 0,
                        totalLteCells: 0,
                        totalNbIotCells: 0,
                        totalCdmaBts: 0,
                        totalCdmaCells: 0
                    };
                    angular.forEach(stats,
                        function(stat) {
                            cityStat.totalLteENodebs += stat.totalLteENodebs;
                            cityStat.totalLteCells += stat.totalLteCells;
                            cityStat.totalNbIotCells += stat.totalNbIotCells;
                            cityStat.totalCdmaBts += stat.totalCdmaBts;
                            cityStat.totalCdmaCells += stat.totalCdmaCells;
                        });
                    stats.push(cityStat);
                },
                getTownFlowStats: function(statDate, frequency) {
                    return generalHttpService.getApiData('TownFlow',
                    {
                        statDate: statDate,
                        frequency: frequency
                    });
                },
                getCurrentDateTownFlowStats: function (statDate, frequency) {
                    return generalHttpService.getApiData('TownFlow',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                updateTownFlowStat: function(stat) {
                    return generalHttpService.postApiData('TownFlow', stat);
                }
            };
        });