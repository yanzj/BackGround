angular.module('topic.parameters.coverage', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
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