angular.module('region.college', ['app.core'])
    .factory('collegeService', function(generalHttpService) {
        return {
            queryNames: function() {
                return generalHttpService.getApiData('CollegeNames', {});
            },
            queryStats: function(year) {
                return generalHttpService.getApiData('CollegeNames',
                    {
                        year: year
                    });
            },
            queryRegion: function(id) {
                return generalHttpService.getApiData('CollegeRegion/' + id, {});
            },
            queryENodebs: function(name) { /////////////////////////////////////////////
                return generalHttpService.getApiData('CollegeENodeb',
                    {
                        collegeName: name
                    });
            },
            queryBtss: function(name) {
                return generalHttpService.getApiData('CollegeBtss',
                    {
                        collegeName: name
                    });
            },
            queryCells: function(name) { //////////////////////////////////////////////
                return generalHttpService.getApiData('CollegeCells',
                    {
                        collegeName: name
                    });
            },
            queryCdmaCells: function(name) {
                return generalHttpService.getApiData('CollegeCdmaCells',
                    {
                        collegeName: name
                    });
            },
            queryLteDistributions: function(name) {
                return generalHttpService.getApiData('CollegeLteDistributions',
                    {
                        collegeName: name
                    });
            },
            queryCdmaDistributions: function(name) {
                return generalHttpService.getApiData('CollegeCdmaDistributions',
                    {
                        collegeName: name
                    });
            },
            queryRaster: function(dataType, range, begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        dataType: dataType,
                        west: range.west,
                        east: range.east,
                        south: range.south,
                        north: range.north,
                        begin: begin,
                        end: end
                    });
            },
            queryTownRaster: function(dataType, town, begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        dataType: dataType,
                        townName: town,
                        begin: begin,
                        end: end
                    });
            },
            queryCsvFileNames: function(begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        begin: begin,
                        end: end
                    });
            },
            queryCsvFileType: function(name) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        csvFileName: name
                    });
            },
            queryCsvFileInfo: function(fileName) {
                return generalHttpService.getApiData('CsvFileInfo',
                    {
                        fileName: fileName
                    });
            },
            updateCsvFileDistance: function(filesInfo) {
                return generalHttpService.postApiData('CsvFileInfo', filesInfo);
            },
            updateAreaDtInfo: function(info) {
                return generalHttpService.putApiData('TownTestInfo', info);
            },
            calculateTownDtTestInfos: function(name, type) {
                return generalHttpService.getApiData('TownTestInfo',
                    {
                        csvFileName: name,
                        type: type
                    });
            },
            calculateRoadDtTestInfos: function(name, type) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        csvFileName: name,
                        type: type
                    });
            },
            queryFileTownDtTestInfo: function(fileId) {
                return generalHttpService.getApiData('TownTestInfo',
                    {
                        fileId: fileId
                    });
            },
            queryFileRoadDtTestInfo: function(fileId) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        fileId: fileId
                    });
            },
            queryRoadDtFileInfos: function(name, begin, end) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        roadName: name,
                        begin: begin,
                        end: end
                    });
            },
            query2GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record2G",
                    {
                        fileName: fileName
                    });
            },
            query3GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record3G",
                    {
                        fileName: fileName
                    });
            },
            queryVolteFileRecords: function(fileName) {
                return generalHttpService.getApiData("RecordVolte",
                    {
                        fileName: fileName
                    });
            },
            query4GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record4G",
                    {
                        fileName: fileName
                    });
            }
        };
    })
    .factory('collegeQueryService', function(generalHttpService) {
        return {
            queryAll: function() { //////////////////////////////////////////////////////////
                return generalHttpService.getApiData('CollegeQuery', {});
            },
            queryByName: function(name) { //////////////////////////////////////////////////
                return generalHttpService.getApiData('CollegeNames', {
                    name: name
                });
            },
            queryCollegeById: function(id) {
                return generalHttpService.getApiData('CollegeQuery',
                {
                    id: id
                });
            },
            queryByNameAndYear: function(name, year) {
                return generalHttpService.getApiData('CollegeQuery', {
                    name: name,
                    year: year
                });
            },
            queryYearList: function(year) {
                return generalHttpService.getApiData('CollegeStat', {
                    year: year
                });
            },
            saveYearInfo: function(info) {
                return generalHttpService.postApiData('CollegeQuery', info);
            },
            constructCollegeInfo: function(info) {
                return generalHttpService.postApiDataWithHeading('CollegeStat', info);
            },
            saveCollegeCells: function(container) {
                return generalHttpService.postApiData('CollegeCellContainer', container);
            },
            queryCollegeCellSectors: function(collegeName) {
                return generalHttpService.getApiData('CollegeCellContainer', {
                    collegeName: collegeName
                });
            },
            saveCollegeCdmaCells: function(container) {
                return generalHttpService.postApiData('CollegeCdmaCellContainer', container);
            },
            queryCollegeCdmaCellSectors: function(collegeName) {
                return generalHttpService.getApiData('CollegeCdmaCellContainer', {
                    collegeName: collegeName
                });
            },
            saveCollegeENodebs: function(container) {
                return generalHttpService.postApiData('CollegeENodeb', container);
            },
            saveCollegeBtss: function(container) {
                return generalHttpService.postApiData('CollegeBtss', container);
            },
            saveCollege3GTest: function(view) {
                return generalHttpService.postApiData('College3GTest', view);
            },
            saveCollege4GTest: function(view) {
                return generalHttpService.postApiData('College4GTest', view);
            },
            retrieveDateCollegeFlowStats: function (statDate) {
                return generalHttpService.getApiData('CollegeFlow',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateCollegeCqiStats: function (statDate) {
                return generalHttpService.getApiData('CollegeCqi',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateCollegePrbStats: function (statDate) {
                return generalHttpService.getApiData('CollegePrb',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateCollegeDoubleFlowStats: function (statDate) {
                return generalHttpService.getApiData('CollegeDoubleFlow',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateCollegeHourCqiStats: function (statDate) {
                return generalHttpService.getApiData('CollegeHourCqi',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateMarketFlowStats: function (statDate) {
                return generalHttpService.getApiData('MarketFlow',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateMarketCqiStats: function (statDate) {
                return generalHttpService.getApiData('MarketCqi',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateMarketPrbStats: function (statDate) {
                return generalHttpService.getApiData('MarketPrb',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateMarketDoubleFlowStats: function (statDate) {
                return generalHttpService.getApiData('MarketDoubleFlow',
                    {
                        statDate: statDate
                    });
            },
            retrieveDateMarketHourCqiStats: function (statDate) {
                return generalHttpService.getApiData('MarketHourCqi',
                    {
                        statDate: statDate
                    });
            }
        };
    })
    .factory('collegeDtService', function(collegeService) {
        var queryRange = function(info) {
            return {
                west: info.centerX - 0.02,
                east: info.centerX + 0.02,
                south: info.centerY - 0.02,
                north: info.centerY + 0.03
            }
        };
        return {
            updateFileInfo: function(info, begin, end) {
                var range = queryRange(info);
                collegeService.queryRaster('2G', range, begin, end).then(function(files) {
                    info.file2Gs = files;
                });
                collegeService.queryRaster('3G', range, begin, end).then(function(files) {
                    info.file3Gs = files;
                });
                collegeService.queryRaster('4G', range, begin, end).then(function(files) {
                    info.file4Gs = files;
                });
            },
            queryRaster: function(center, type, begin, end, callback) {
                var range = queryRange(center);
                collegeService.queryRaster(type, range, begin, end).then(function(files) {
                    callback(files);
                });
            },
            default3GTestView: function(collegeName, place, tester) {
                return {
                    testTime: new Date(),
                    collegeName: collegeName,
                    place: place,
                    tester: tester,
                    downloadRate: 1024,
                    accessUsers: 23,
                    minRssi: -109,
                    maxRssi: -99,
                    vswr: 1.11
                };
            },
            default4GTestView: function(collegeName, place, tester) {
                return {
                    testTime: new Date(),
                    collegeName: collegeName,
                    place: place,
                    tester: tester,
                    downloadRate: 38024,
                    uploadRate: 21024,
                    accessUsers: 33,
                    rsrp: -109,
                    sinr: 12,
                    cellName: "",
                    pci: 0
                };
            }
        };
    });