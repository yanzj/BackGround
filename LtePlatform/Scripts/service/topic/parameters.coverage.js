angular.module('topic.parameters.coverage', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
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