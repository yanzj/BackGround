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