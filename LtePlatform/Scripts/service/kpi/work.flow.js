angular.module('kpi.work.flow', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("topic.cells",
        function($scope, $uibModalInstance, dialogTitle, name, complainService) {
            $scope.dialogTitle = dialogTitle;
            complainService.queryHotSpotCells(name).then(function(existedCells) {
                $scope.cellList = existedCells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });