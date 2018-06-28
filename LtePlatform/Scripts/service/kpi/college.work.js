angular.module('kpi.college.work', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('college.test3G.dialog',
        function($scope,
            $uibModalInstance,
            collegeName,
            collegeDtService,
            coverageService,
            collegeMapService,
            baiduQueryService) {
            $scope.dialogTitle = collegeName + "-3G测试结果上报";
            $scope.item = collegeDtService.default3GTestView(collegeName, '饭堂', '许良镇');

            var queryRasterInfo = function(files, index, data, callback) {
                coverageService.queryDetailsByRasterInfo(files[index], '3G').then(function(result) {
                    data.push.apply(data, result);
                    if (index < files.length - 1) {
                        queryRasterInfo(files, index + 1, data, callback);
                    } else {
                        callback(data);
                    }
                });
            };
            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                    });
                });

            $scope.matchCoverage = function() {
                collegeDtService.queryRaster($scope.center,
                    '3G',
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    function(files) {
                        if (files.length) {
                            var data = [];
                            queryRasterInfo(files,
                                0,
                                data,
                                function(result) {
                                    console.log(result);
                                });
                        }
                    });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.item);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.test4G.dialog',
        function($scope,
            $uibModalInstance,
            collegeName,
            collegeDtService,
            collegeService,
            networkElementService,
            collegeMapService,
            baiduQueryService,
            coverageService,
            appFormatService) {
            $scope.dialogTitle = collegeName + "-4G测试结果上报";
            $scope.item = collegeDtService.default4GTestView(collegeName, '饭堂', '许良镇');
            collegeService.queryCells(collegeName).then(function(cellList) {
                var names = [];
                angular.forEach(cellList,
                    function(cell) {
                        names.push(cell.cellName);
                    });
                $scope.cellName = {
                    options: names,
                    selected: names[0]
                };
            });
            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                    });
                });
            $scope.$watch('cellName.selected',
                function(cellName) {
                    if (cellName) {
                        networkElementService.queryLteRruFromCellName(cellName).then(function(rru) {
                            $scope.rru = rru;
                        });
                    }
                });

            var queryRasterInfo = function(files, index, data, callback) {
                coverageService.queryDetailsByRasterInfo(files[index], '4G').then(function(result) {
                    data.push.apply(data, result);
                    if (index < files.length - 1) {
                        queryRasterInfo(files, index + 1, data, callback);
                    } else {
                        callback(data);
                    }
                });
            };

            $scope.matchCoverage = function() {
                collegeDtService.queryRaster($scope.center,
                    '4G',
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    function(files) {
                        if (files.length) {
                            var data = [];
                            queryRasterInfo(files,
                                0,
                                data,
                                function(result) {
                                    var testEvaluations = appFormatService.calculateAverages(result,
                                    [
                                        function(point) {
                                            return point.rsrp;
                                        }, function(point) {
                                            return point.sinr;
                                        }, function(point) {
                                            return point.pdcpThroughputDl > 10 ? point.pdcpThroughputDl : 0;
                                        }, function(point) {
                                            return point.pdcpThroughputUl > 1 ? point.pdcpThroughputUl : 0;
                                        }
                                    ]);
                                    $scope.item.rsrp = testEvaluations[0].sum / testEvaluations[0].count;
                                    $scope.item.sinr = testEvaluations[1].sum / testEvaluations[1].count;
                                    $scope.item.downloadRate = testEvaluations[2].sum / testEvaluations[2].count * 1024;
                                    $scope.item.uploadRate = testEvaluations[3].sum / testEvaluations[3].count * 1024;
                                });
                        }
                    });
            };

            $scope.ok = function() {
                $scope.item.cellName = $scope.cellName.selected;
                $uibModalInstance.close($scope.item);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('test.process.dialog',
        function($scope, $uibModalInstance, collegeName, collegeQueryService, appFormatService) {
            $scope.dialogTitle = collegeName + "校园网测试信息确认";

            $scope.query = function() {
                collegeQueryService.queryCollege3GTestList($scope.beginDate.value, $scope.endDate.value, collegeName)
                    .then(function(test3G) {
                        $scope.items3G = test3G;
                    });
                collegeQueryService.queryCollege4GTestList($scope.beginDate.value, $scope.endDate.value, collegeName)
                    .then(function(test4G) {
                        $scope.items4G = test4G;
                        var testEvaluations = appFormatService.calculateAverages(test4G,
                        [
                            function(point) {
                                return point.rsrp;
                            }, function(point) {
                                return point.sinr;
                            }, function(point) {
                                return point.downloadRate;
                            }, function(point) {
                                return point.uploadRate;
                            }
                        ]);
                        $scope.averageRsrp = testEvaluations[0].sum / testEvaluations[0].count;
                        $scope.averageSinr = testEvaluations[1].sum / testEvaluations[1].count;
                        $scope.averageDownloadRate = testEvaluations[2].sum / testEvaluations[2].count;
                        $scope.averageUploadRate = testEvaluations[3].sum / testEvaluations[3].count;
                    });
            };
            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($("#reports").html());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });