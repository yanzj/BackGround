angular.module('kpi.college.basic', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('year.info.dialog',
        function($scope,
            $uibModalInstance,
            appFormatService,
            name,
            year,
            item) {
            $scope.dialogTitle = name + year + "年校园信息补充";
            $scope.dto = item;
            $scope.beginDate = {
                value: appFormatService.getDate(item.oldOpenDate),
                opened: false
            };
            $scope.endDate = {
                value: appFormatService.getDate(item.newOpenDate),
                opened: false
            };
            $scope.beginDate.value.setDate($scope.beginDate.value.getDate() + 365);
            $scope.endDate.value.setDate($scope.endDate.value.getDate() + 365);

            $scope.ok = function() {
                $scope.dto.oldOpenDate = $scope.beginDate.value;
                $scope.dto.newOpenDate = $scope.endDate.value;
                $scope.dto.year = year;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.query.name",
        function($scope, $uibModalInstance, dialogTitle, name, collegeService) {
            $scope.collegeName = name;
            $scope.dialogTitle = dialogTitle;
            $scope.updateLteCells = function() {
                collegeService.queryCells($scope.collegeName).then(function(cells) {
                    $scope.cellList = cells;
                });
            };
            collegeService.queryENodebs($scope.collegeName).then(function(eNodebs) {
                $scope.eNodebList = eNodebs;
            });
            $scope.updateLteCells();
            collegeService.queryBtss($scope.collegeName).then(function(btss) {
                $scope.btsList = btss;
            });
            collegeService.queryCdmaCells($scope.collegeName).then(function(cells) {
                $scope.cdmaCellList = cells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.college);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });