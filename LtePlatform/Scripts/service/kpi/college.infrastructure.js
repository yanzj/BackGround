angular.module('kpi.college.infrastructure', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .run(function($rootScope) {
        $rootScope.page = {
            messages: []
        };
        $rootScope.closeAlert = function(messages, $index) {
            messages.splice($index, 1);
        };
        $rootScope.addSuccessMessage = function(message) {
            $rootScope.page.messages.push({
                type: 'success',
                contents: message
            });
        };
    })
    .controller('eNodeb.dialog',
        function($scope,
            $uibModalInstance,
            collegeService,
            collegeDialogService,
            geometryService,
            collegeQueryService,
            baiduQueryService,
            name,
            dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            $scope.query = function() {
                collegeService.queryENodebs(name).then(function(result) {
                    $scope.eNodebList = result;
                });
            };
            collegeQueryService.queryByName(name).then(function(college) {
                collegeService.queryRegion(college.id).then(function(region) {
                    var center = geometryService.queryRegionCenter(region);
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            X: 2 * center.X - coors.x,
                            Y: 2 * center.Y - coors.y
                        };
                    });
                });
            });

            $scope.addENodebs = function() {
                collegeDialogService.addENodeb(name,
                    $scope.center,
                    function(count) {
                        $scope.addSuccessMessage('增加ENodeb' + count + '个');
                        $scope.query();
                    });
            };

            $scope.query();


            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('bts.dialog',
        function($scope,
            $uibModalInstance,
            collegeService,
            collegeDialogService,
            collegeQueryService,
            geometryService,
            baiduQueryService,
            name,
            dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeQueryService.queryByName(name).then(function(college) {
                collegeService.queryRegion(college.id).then(function(region) {
                    var center = geometryService.queryRegionCenter(region);
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            X: 2 * center.X - coors.x,
                            Y: 2 * center.Y - coors.y
                        };
                    });
                });
            });

            $scope.query = function() {
                collegeService.queryBtss(name).then(function(result) {
                    $scope.btsList = result;
                });
            };

            $scope.addBts = function() {
                collegeDialogService.addBts(name,
                    $scope.center,
                    function(count) {
                        $scope.addSuccessMessage('增加Bts' + count + '个');
                        $scope.query();
                    });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.btsList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cell.dialog',
        function($scope,
            $uibModalInstance,
            collegeService,
            collegeDialogService,
            name,
            dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            $scope.updateLteCells = function() {
                collegeService.queryCells(name).then(function(cells) {
                    $scope.cellList = cells;
                });
            };
            $scope.supplementCells = function() {
                collegeDialogService.supplementENodebCells($scope.eNodebList,
                    $scope.cellList,
                    name,
                    $scope.updateLteCells);
            };
            $scope.supplementLonelyCells = function() {
                collegeDialogService.supplementPositionCells(name, $scope.updateLteCells);
            };

            $scope.updateLteCells();
            collegeService.queryENodebs(name).then(function(eNodebs) {
                $scope.eNodebList = eNodebs;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdmaCell.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaCells(name).then(function(result) {
                $scope.cdmaCellList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cdmaCellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('lte.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryLteDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdma.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });