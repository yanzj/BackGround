angular.module("myApp", ['app.common'])
    .controller("college.infos",
        function(
            $scope,
            collegeService,
            collegeDialogService,
            collegeQueryService,
            baiduQueryService) {
            $scope.collegeInfos = [];
            $scope.page = {
                messages: []
            };
            $scope.center = {
                X: 0,
                Y: 0
            };

            $scope.closeAlert = function(messages, $index) {
                messages.splice($index, 1);
            };
            $scope.addSuccessMessage = function(message) {
                $scope.page.messages.push({
                    type: 'success',
                    contents: message
                });
            };

            $scope.updateCurrentCollege = function(info) {
                $scope.currentCollegeName = info.name;
                $scope.updateLteCells(info.name);
                $scope.queryENodebs(info.name);
            };

            $scope.queryENodebs = function(name) {
                collegeService.queryENodebs(name).then(function(result) {
                    $scope.eNodebList = result;
                });
                collegeQueryService.queryByName(name).then(function(college) {
                    baiduQueryService.transformToBaidu(college.longtitute, college.lattitute).then(function(coors) {
                        $scope.center = {
                            X: 2 * college.longtitute - coors.x,
                            Y: 2 * college.lattitute - coors.y
                        };
                    });
                });
            };
            
            $scope.addENodebs = function(name) {
                collegeDialogService.addENodeb(name,
                    $scope.center,
                    function(count) {
                        $scope.addSuccessMessage('增加ENodeb' + count + '个');
                        $scope.queryENodebs(name);
                    });
            };

            $scope.updateLteCells = function(name) {
                collegeService.queryCells(name).then(function(cells) {
                    $scope.cellList = cells;
                });
            };

            $scope.supplementCells = function(name) {
                collegeDialogService.supplementENodebCells($scope.eNodebList,
                    $scope.cellList,
                    name,
                    $scope.updateLteCells);
            };

            $scope.supplementLonelyCells = function(name) {
                collegeDialogService.supplementPositionCells(name, $scope.updateLteCells);
            };

            collegeQueryService.queryAll().then(function(infos) {
                $scope.collegeInfos = infos;
                $scope.updateCurrentCollege(infos[0]);
            });

        });