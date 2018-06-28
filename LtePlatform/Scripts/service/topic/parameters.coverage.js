angular.module('topic.parameters.coverage', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('town.dt.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            item,
            kpiDisplayService,
            baiduMapService,
            parametersMapService,
            coverageService,
            collegeService) {
            $scope.dialogTitle = dialogTitle;

            $scope.includeAllFiles = false;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };
            $scope.dataFile = {
                options: [],
                selected: ''
            };
            $scope.data = [];
            $scope.coverageOverlays = [];

            $scope.query = function() {
                $scope.kpi = kpiDisplayService.queryKpiOptions($scope.network.selected);
                collegeService.queryTownRaster($scope.network.selected,
                    item.townName,
                    $scope.longBeginDate.value,
                    $scope.endDate.value).then(function(results) {
                    baiduMapService.initializeMap("all-map", 14);
                    baiduMapService.setCellFocus(item.longtitute, item.lattitute, 14);
                    if (results.length) {
                        $scope.dataFile.options = results;
                        $scope.dataFile.selected = results[0];
                    }
                });

            };

            $scope.$watch('network.selected',
                function() {
                    baiduMapService.switchSubMap();
                    $scope.query();
                });

            $scope.showDtPoints = function() {
                $scope.legend = kpiDisplayService.queryCoverageLegend($scope.kpi.selected);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateCoveragePoints($scope.coveragePoints, $scope.data, $scope.kpi.selected);
                angular.forEach($scope.coverageOverlays,
                    function(overlay) {
                        baiduMapService.removeOverlay(overlay);
                    });
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.coverageOverlays);
            };

            var queryRasterInfo = function(index) {
                coverageService.queryByRasterInfo($scope.dataFile.options[index], $scope.network.selected)
                    .then(function(result) {
                        $scope.data.push.apply($scope.data, result);
                        if (index < $scope.dataFile.options.length - 1) {
                            queryRasterInfo(index + 1);
                        } else {
                            $scope.showDtPoints();
                        }
                    });
            };

            $scope.showStat = function() {
                $scope.data = [];
                if ($scope.includeAllFiles) {
                    queryRasterInfo(0);
                } else {
                    coverageService.queryByRasterInfo($scope.dataFile.selected, $scope.network.selected)
                        .then(function(result) {
                            $scope.data = result;
                            $scope.showDtPoints();
                        });
                }
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('csv.dt.dialog',
        function($scope,
            dialogTitle,
            beginDate,
            endDate,
            collegeService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };

            $scope.query = function() {
                collegeService.queryCsvFileNames($scope.beginDate.value, $scope.endDate.value).then(function(infos) {
                    $scope.fileInfos = infos;
                    angular.forEach(infos,
                        function(info) {
                            collegeService.queryCsvFileType(info.csvFileName.replace('.csv', '')).then(function(type) {
                                info.networkType = type;
                            });
                            collegeService.queryFileTownDtTestInfo(info.id).then(function(items) {
                                info.townInfos = items;
                            });
                            collegeService.queryFileRoadDtTestInfo(info.id).then(function(items) {
                                info.roadInfos = items;
                            });
                        });
                });
            };

            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('highway.dt.dialog',
        function($scope,
            dialogTitle,
            beginDate,
            endDate,
            name,
            collegeService,
            parametersChartService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.query = function() {
                collegeService.queryRoadDtFileInfos(name, $scope.beginDate.value, $scope.endDate.value)
                    .then(function(infos) {
                        $scope.fileInfos = infos;
                        angular.forEach(infos,
                            function(info) {
                                collegeService.queryCsvFileType(info.csvFileName.replace('.csv', ''))
                                    .then(function(type) {
                                        info.networkType = type;
                                    });
                            });
                        $("#distanceDistribution").highcharts(parametersChartService
                            .getHotSpotDtDistancePieOptions(name, infos));
                        $("#coverageRate").highcharts(parametersChartService
                            .getHotSpotDtCoverageRateOptions(name, infos));
                    });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query();
        })
    .controller('college.coverage.name',
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            coverageOverlays,
            baiduMapService,
            collegeQueryService,
            baiduQueryService,
            collegeService,
            collegeMapService,
            collegeDtService,
            coverageService,
            kpiDisplayService,
            parametersMapService) {
            $scope.dialogTitle = name + '覆盖情况评估';
            $scope.includeAllFiles = false;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };
            $scope.dataFile = {
                options: [],
                selected: ''
            };
            $scope.data = [];
            $scope.coverageOverlays = coverageOverlays;

            $scope.query = function() {
                $scope.kpi = kpiDisplayService.queryKpiOptions($scope.network.selected);
                $scope.legend = kpiDisplayService.queryCoverageLegend($scope.kpi.selected);
                $scope.legend.title = $scope.kpi.selected;
                collegeDtService.queryRaster($scope.center,
                    $scope.network.selected,
                    beginDate.value,
                    endDate.value,
                    function(files) {
                        $scope.dataFile.options = files;
                        if (files.length) {
                            $scope.dataFile.selected = files[0];
                        }
                        $scope.dtList = files;
                        angular.forEach(files,
                            function(file) {
                                collegeService.queryCsvFileInfo(file.csvFileName).then(function(info) {
                                    angular.extend(file, info);
                                });
                            });
                    });
            };

            $scope.$watch('network.selected',
                function() {
                    if ($scope.center) {
                        $scope.query();
                    }
                });

            $scope.showDtPoints = function() {
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateCoveragePoints($scope.coveragePoints, $scope.data, $scope.kpi.selected);
                angular.forEach($scope.coverageOverlays,
                    function(overlay) {
                        baiduMapService.removeOverlay(overlay);
                    });
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.coverageOverlays);
            };

            var queryRasterInfo = function(index) {
                coverageService.queryByRasterInfo($scope.dataFile.options[index],
                        $scope.network.selected)
                    .then(function(result) {
                        $scope.data.push.apply($scope.data, result);
                        if (index < $scope.dataFile.options.length - 1) {
                            queryRasterInfo(index + 1);
                        } else {
                            $scope.showDtPoints();
                        }
                    });
            };

            $scope.showResults = function() {
                $scope.data = [];
                if ($scope.includeAllFiles) {
                    queryRasterInfo(0);
                } else {
                    coverageService.queryByRasterInfo($scope.dataFile.selected,
                            $scope.network.selected)
                        .then(function(result) {
                            $scope.data = result;
                            $scope.showDtPoints();
                        });
                }
            };

            $scope.showStat = function() {
                if ($scope.includeAllFiles) {
                    var combined = _.reduce($scope.dataFile.options,
                        function(memo, num) {
                            return {
                                count: memo.count + num.count,
                                coverageCount: memo.coverageCount + num.coverageCount
                            }
                        });
                    $scope.coverageRate = 100 * combined.coverageCount / combined.count;
                } else {
                    $scope.coverageRate = $scope.dataFile.selected.coverageRate;
                }
            };

            collegeMapService.queryCenterAndCallback(name,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                        $scope.query();
                    });
                });

            $scope.ok = function() {
                $scope.showResults();
                $uibModalInstance.close($scope.legend);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cluster.point.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            site,
            currentClusterList,
            alarmsService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentClusterList = currentClusterList;
            angular.forEach(currentClusterList,
                function(stat) {
                    alarmsService.queryDpiGridKpi(stat.x, stat.y).then(function(result) {
                        stat.firstPacketDelay = result.firstPacketDelay;
                        stat.pageOpenDelay = result.pageOpenDelay;
                        stat.firstPacketDelayClass = result.firstPacketDelayClass;
                        stat.pageOpenDelayClass = result.pageOpenDelayClass;
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.site);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });