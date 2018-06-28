angular.module('home.mr', ['app.common'])
    .controller("home.mr",
        function($scope,
            baiduMapService,
            coverageService,
            kpiDisplayService,
            parametersMapService,
            appUrlService,
            coverageDialogService,
            dumpPreciseService,
            appRegionService) {
            baiduMapService.initializeMap("map", 13);

            $scope.currentDataLabel = "districtPoints";
            $scope.overlays = {
                coverage: [],
                sites: [],
                cells: []
            };
            $scope.initializeMap = function() {
                $scope.overlays.coverage = [];
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
            };

            $scope.showStats = function() {
                coverageDialogService.showAgpsStats($scope.data, $scope.legend.criteria);
            };
            $scope.showInfrasturcture = function() {
                parametersMapService.clearOverlaySites($scope.overlays.sites);
                parametersMapService.clearOverlaySites($scope.overlays.cells);
                parametersMapService.showElementsInOneTown($scope.city.selected,
                    $scope.district.selected,
                    $scope.town.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.overlays.sites,
                    $scope.overlays.cells);
            };

            $scope.showTelecomCoverage = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var index = parseInt($scope.data.length / 2);
                baiduMapService.setCellFocus($scope.data[index].longtitute, $scope.data[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateTelecomRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.displayTelecomAgps = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var index = parseInt($scope.telecomAgps.length / 2);
                baiduMapService.setCellFocus($scope.telecomAgps[index].longtitute,
                    $scope.telecomAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.telecomAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showUnicomCoverage = function() {
                $scope.currentView = "联通";
                $scope.initializeMap();
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateUnicomRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals);
            };
            $scope.displayUnicomAgps = function() {
                $scope.currentView = "联通";
                $scope.initializeMap();
                var index = parseInt($scope.unicomAgps.length / 2);
                baiduMapService.setCellFocus($scope.unicomAgps[index].longtitute,
                    $scope.unicomAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.uniAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showMobileCoverage = function() {
                $scope.currentView = "移动";
                $scope.initializeMap();
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateMobileRsrpPoints($scope.coveragePoints, $scope.data);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals);
            };
            $scope.displayMobileAgps = function() {
                $scope.currentView = "移动";
                $scope.initializeMap();
                var index = parseInt($scope.mobileAgps.length / 2);
                baiduMapService.setCellFocus($scope.mobileAgps[index].longtitute,
                    $scope.mobileAgps[index].lattitute,
                    15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateAverageRsrpPoints($scope.coveragePoints, $scope.mobileAgps);
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.updateIndexedData = function() {
                var items = [];
                angular.forEach($scope.data,
                    function(data) {
                        data['topic'] = data.longtitute + ',' + data.lattitute;
                        items.push(data);
                    });
                appUrlService.refreshIndexedDb($scope.indexedDB.db, $scope.currentDataLabel, 'topic', items);
            };
            $scope.queryAndDisplayTelecom = function() {
                coverageService.queryAgisDtPointsByTopic($scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.district.selected + $scope.town.selected).then(function(result) {
                    $scope.data = result;
                    $scope.showTelecomCoverage();
                });
            };
            $scope.showTelecomWeakCoverage = function() {
                $scope.currentView = "电信";
                $scope.initializeMap();
                var weakData = _.filter($scope.data, function(stat) { return stat.telecomRsrp < 40; });
                if (!weakData) return;
                var index = parseInt(weakData.length / 2);
                baiduMapService.setCellFocus(weakData[index].longtitute, weakData[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateTelecomRsrpPoints($scope.coveragePoints, weakData);
                parametersMapService.showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
            };
            $scope.showSmallRange = function() {
                coverageService.queryAgisDtPointsByTopic($scope.beginDate.value, $scope.endDate.value, '小范围')
                    .then(function(result) {
                        $scope.data = result;
                        $scope.currentDataLabel = 'rangePoints';
                        $scope.updateIndexedData();
                        $scope.showTelecomCoverage();
                    });
            };

            $scope.queryAgps = function() {
                $scope.currentDistrict = $scope.district.selected;
                $scope.currentTown = $scope.town.selected;
                switch ($scope.type.selected) {
                case '电信':
                    coverageService.queryAgpsTelecomByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.telecomAgps = result;
                    });
                    break;
                case '移动':
                    coverageService.queryAgpsMobileByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.mobileAgps = result;
                    });
                    break;
                case '联通':
                    coverageService.queryAgpsUnicomByTown($scope.beginDate.value,
                        $scope.endDate.value,
                        $scope.district.selected,
                        $scope.town.selected).then(function(result) {
                        $scope.unicomAgps = result;
                    });
                    break;
                }
            };
            $scope.updateTelecomAgps = function() {
                coverageService.updateAgpsTelecomView($scope.currentDistrict, $scope.currentTown, $scope.telecomAgps)
                    .then(function(result) {

                    });
            };
            $scope.updateMobileAgps = function() {
                coverageService.updateAgpsMobileView($scope.currentDistrict, $scope.currentTown, $scope.mobileAgps)
                    .then(function(result) {

                    });
            };
            $scope.updateUnicomAgps = function() {
                coverageService.updateAgpsUnicomView($scope.currentDistrict, $scope.currentTown, $scope.unicomAgps)
                    .then(function(result) {

                    });
            };
            $scope.type = {
                options: ['电信', '移动', '联通'],
                selected: '电信'
            };
            $scope.initializeRsrpLegend();
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        var districts = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            districts,
                            function(district, $index) {
                                if ($index === 0) {
                                    $scope.district = {
                                        options: districts,
                                        selected: districts[0]
                                    };
                                }
                            });
                    }
                });
            $scope.$watch('district.selected',
                function(district) {
                    if (district) {
                        appRegionService.queryTowns($scope.city.selected, district).then(function(towns) {
                            $scope.town = {
                                options: towns,
                                selected: towns[0]
                            };
                        });
                    }
                });
        })
    .controller("mr.grid",
        function($scope,
            baiduMapService,
            coverageService,
            dumpPreciseService,
            kpiDisplayService,
            baiduQueryService,
            coverageDialogService,
            appRegionService,
            parametersMapService,
            collegeMapService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.currentView = "自身覆盖";
            $scope.setRsrpLegend = function() {
                var legend = kpiDisplayService.queryCoverageLegend('rsrpInterval');
                $scope.legend.title = 'RSRP区间';
                $scope.legend.intervals = legend.criteria;
                $scope.colorDictionary = {};
                $scope.areaStats = {};
                angular.forEach(legend.criteria,
                    function(info) {
                        $scope.colorDictionary[info.threshold] = info.color;
                        $scope.areaStats[info.threshold] = 0;
                    });
            };
            $scope.setCompeteLegend = function() {
                var legend = kpiDisplayService.queryCoverageLegend('competeResult');
                $scope.legend.title = '竞争结果';
                $scope.legend.intervals = legend.criteria;
                $scope.colorDictionary = {};
                $scope.areaStats = {};
                angular.forEach(legend.criteria,
                    function(info) {
                        $scope.colorDictionary[info.threshold] = info.color;
                        $scope.areaStats[info.threshold] = 0;
                    });
            };
            $scope.showGridStats = function() {
                var keys = [];
                angular.forEach($scope.legend.intervals,
                    function(info) {
                        keys.push(info.threshold);
                    });
                coverageDialogService.showGridStats($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.currentView,
                    $scope.legend.title,
                    $scope.areaStats,
                    keys);
            };
            $scope.showDistrictSelfCoverage = function(district, town, color) {
                baiduMapService.clearOverlays();
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                if (town === '全区') {
                    coverageService.queryMrGridSelfCoverage(district, $scope.endDate.value).then(function(result) {
                        var coors = result[0].coordinates.split(';')[0];
                        var longtitute = parseFloat(coors.split(',')[0]);
                        var lattitute = parseFloat(coors.split(',')[1]);
                        collegeMapService.showRsrpMrGrid(result,
                            longtitute,
                            lattitute,
                            $scope.areaStats,
                            $scope.colorDictionary);
                    });
                } else {
                    parametersMapService.showTownBoundaries($scope.city.selected, district, town, color);

                    coverageService.queryTownMrGridSelfCoverage(district, town, $scope.endDate.value)
                        .then(function(result) {
                            appRegionService.queryTown($scope.city.selected, district, town).then(function(stat) {
                                var longtitute = stat.longtitute;
                                var lattitute = stat.lattitute;
                                collegeMapService
                                    .showRsrpMrGrid(result,
                                        longtitute,
                                        lattitute,
                                        $scope.areaStats,
                                        $scope.colorDictionary);
                            });
                        });
                }

            };
            $scope.showDistrictCompeteCoverage = function(district, town, color, competeDescription) {
                baiduMapService.clearOverlays();
                baiduMapService.addDistrictBoundary($scope.city.selected + '市' + district + '区', color);
                if (town === '全区') {
                    coverageService.queryMrGridCompete(district, $scope.endDate.value, competeDescription)
                        .then(function(result) {
                            var coors = result[0].coordinates.split(';')[0];
                            var longtitute = parseFloat(coors.split(',')[0]);
                            var lattitute = parseFloat(coors.split(',')[1]);
                            collegeMapService
                                .showRsrpMrGrid(result,
                                    longtitute,
                                    lattitute,
                                    $scope.areaStats,
                                    $scope
                                    .colorDictionary);
                        });
                } else {
                    parametersMapService.showTownBoundaries($scope.city.selected, district, town, color);
                    coverageService.queryTownMrGridCompete(district, town, $scope.endDate.value, competeDescription)
                        .then(function(result) {
                            appRegionService.queryTown($scope.city.selected, district, town).then(function(stat) {
                                var longtitute = stat.longtitute;
                                var lattitute = stat.lattitute;
                                collegeMapService
                                    .showRsrpMrGrid(result,
                                        longtitute,
                                        lattitute,
                                        $scope.areaStats,
                                        $scope.colorDictionary);
                            });
                        });
                }

            };
            $scope.showMrGrid = function(district, town) {
                $scope.currentDistrict = district;
                $scope.currentTown = town;
                if ($scope.currentView === '自身覆盖') {
                    $scope.setRsrpLegend();
                    $scope.showDistrictSelfCoverage(district, town, $scope.colors[0]);
                } else {
                    $scope.setCompeteLegend();
                    $scope.showDistrictCompeteCoverage(district, town, $scope.colors[0], $scope.currentView);
                }

            };
            $scope.showDistrictMrGrid = function(district) {
                $scope.showMrGrid(district.name, district.towns[0]);
            };
            $scope.showTelecomCoverage = function() {
                $scope.currentView = "自身覆盖";
                $scope.setRsrpLegend();
                $scope.showDistrictSelfCoverage($scope.currentDistrict, town, $scope.colors[0]);
            };
            $scope.showMobileCompete = function() {
                $scope.currentView = "移动竞对";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.showUnicomCompete = function() {
                $scope.currentView = "联通竞对";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.showOverallCompete = function() {
                $scope.currentView = "竞对总体";
                $scope.setCompeteLegend();
                $scope.showDistrictCompeteCoverage($scope.currentDistrict,
                    $scope.currentTown,
                    $scope.colors[0],
                    $scope.currentView);
            };
            $scope.districts = [];
            $scope.setRsrpLegend();
            $scope.$watch('city.selected',
                function(city) {
                    if (city) {
                        var districts = [];
                        dumpPreciseService.generateUsersDistrict(city,
                            districts,
                            function(district, $index) {
                                appRegionService.queryTowns($scope.city.selected, district).then(function(towns) {
                                    towns.push('全区');
                                    $scope.districts.push({
                                        name: district,
                                        towns: towns
                                    });
                                    if ($index === 0) {
                                        $scope.showMrGrid(district, towns[0]);
                                    }
                                });
                            });
                    }
                });
        })
    .controller("mr.app",
        function($scope,
            baiduMapService,
            alarmsService,
            coverageDialogService,
            kpiDisplayService,
            parametersMapService,
            alarmImportService) {
            baiduMapService.initializeMap("map", 11);
            baiduMapService.addCityBoundary("佛山");
            $scope.overlays = {
                planSites: [],
                sites: [],
                cells: []
            };
            $scope.initializeMap = function() {
                $scope.overlays.coverage = [];
                baiduMapService.clearOverlays();
                baiduMapService.addCityBoundary("佛山");
            };

            $scope.currentCluster = {
                list: [],
                stat: {}
            };
            $scope.calculateCoordinate = function(list) {
                angular.forEach(list,
                    function(item) {
                        var sum = _.reduce(item.gridPoints,
                            function(memo, num) {
                                return {
                                    longtitute: memo.longtitute + num.longtitute,
                                    lattitute: memo.lattitute + num.lattitute
                                };
                            });
                        item.longtitute = sum.longtitute / item.gridPoints.length;
                        item.lattitute = sum.lattitute / item.gridPoints.length;
                        var bestPoint = _.min(item.gridPoints,
                            function(stat) {
                                return (stat.longtitute - item.longtitute) * (stat.longtitute - item.longtitute) +
                                    (stat.lattitute - item.lattitute) * (stat.lattitute - item.lattitute);
                            });
                        item.bestLongtitute = bestPoint.longtitute;
                        item.bestLattitute = bestPoint.lattitute;
                    });
            };
            $scope.showCurrentCluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var gridList = $scope.currentCluster.list;
                if (!gridList.length) return;
                var index = parseInt(gridList.length / 2);
                baiduMapService.setCellFocus(gridList[index].longtitute, gridList[index].lattitute, 15);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateRealRsrpPoints($scope.coveragePoints, gridList);
                parametersMapService.showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
                if ($scope.currentCluster.stat) {
                    alarmImportService.updateClusterKpi($scope.currentCluster.stat,
                        function(stat) {
                            parametersMapService.displayClusterPoint(stat, $scope.currentCluster.list);
                        });
                } else {
                    parametersMapService.displayClusterPoint($scope.currentCluster.stat, $scope.currentCluster.list);
                }
            };
            $scope.showSubClusters = function(stats) {
                angular.forEach(stats,
                    function(stat) {
                        var gridList = stat.gridPoints;
                        var index = parseInt(gridList.length / 2);
                        baiduMapService.setCellFocus(gridList[index].longtitute, gridList[index].lattitute, 17);
                        $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                        alarmsService.queryClusterGridKpis(gridList).then(function(list) {
                            stat.gridPoints = list;
                            kpiDisplayService.generateRealRsrpPoints($scope.coveragePoints, list);
                            parametersMapService
                                .showIntervalGrids($scope.coveragePoints.intervals, $scope.overlays.coverage);
                            alarmImportService.updateClusterKpi(stat,
                                function(item) {
                                    parametersMapService.displayClusterPoint(item, list);
                                });
                        });
                    });
            };
            $scope.showRange500Cluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('500', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showRange1000Cluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('1000', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showRangeAdvanceCluster = function() {
                $scope.initializeRsrpLegend();
                $scope.initializeMap();
                var range = baiduMapService.getCurrentMapRange(-0.012, -0.003);
                alarmsService.queryGridClusterRange('Advance8000', range.west, range.east, range.south, range.north)
                    .then(function(stats) {
                        $scope.showSubClusters(stats);
                    });
            };
            $scope.showInfrasturcture = function() {
                parametersMapService.clearOverlaySites($scope.overlays.sites);
                parametersMapService.clearOverlaySites($scope.overlays.cells);
                var gridList = $scope.currentCluster.list;
                var west = _.min(gridList, 'longtitute').longtitute - 0.01;
                var east = _.max(gridList, 'longtitute').longtitute + 0.01;
                var south = _.min(gridList, 'lattitute').lattitute - 0.01;
                var north = _.max(gridList, 'lattitute').lattitute + 0.01;
                parametersMapService.showElementsInRange(west,
                    east,
                    south,
                    north,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.overlays.sites,
                    $scope.overlays.cells);
            };

            $scope.showCluster500List = function() {
                if ($scope.cluster500List) {
                    coverageDialogService.showGridClusterStats("500个分簇结构",
                        $scope.cluster500List,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('500').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.cluster500List = _.filter(list, function(stat) { return stat.gridPoints.length > 50 });
                        coverageDialogService.showGridClusterStats("500个分簇结构",
                            $scope.cluster500List,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            };
            $scope.showCluster1000List = function() {
                if ($scope.cluster1000List) {
                    coverageDialogService.showGridClusterStats("1000个分簇结构",
                        $scope.cluster1000List,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('1000').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.cluster1000List = _.filter(list, function(stat) { return stat.gridPoints.length > 35 });
                        coverageDialogService.showGridClusterStats("1000个分簇结构",
                            $scope.cluster1000List,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            };
            $scope.showClusterAdvance = function() {
                if ($scope.clusterAdvanceList) {
                    coverageDialogService.showGridClusterStats("改进分簇结构",
                        $scope.clusterAdvanceList,
                        $scope.currentCluster,
                        $scope.showCurrentCluster);
                } else {
                    alarmsService.queryGridClusters('Advance8000').then(function(list) {
                        $scope.calculateCoordinate(list);
                        $scope.clusterAdvanceList = _
                            .filter(list, function(stat) { return stat.gridPoints.length > 5 });
                        coverageDialogService.showGridClusterStats("改进分簇结构",
                            $scope.clusterAdvanceList,
                            $scope.currentCluster,
                            $scope.showCurrentCluster);
                    });
                }
            }
            $scope.showCluster500Points = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.cluster500List,
                    $scope.overlays.planSites,
                    50,
                    $scope.currentCluster.list[0]);
            };
            $scope.showCluster1000Points = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.cluster1000List,
                    $scope.overlays.planSites,
                    35,
                    $scope.currentCluster.list[0]);
            };
            $scope.showClusterAdvancePoints = function() {
                parametersMapService.clearOverlaySites($scope.overlays.planSites);
                parametersMapService.displayClusterPoints($scope.clusterAdvanceList,
                    $scope.overlays.planSites,
                    5,
                    $scope.currentCluster.list[0]);
            };
        });