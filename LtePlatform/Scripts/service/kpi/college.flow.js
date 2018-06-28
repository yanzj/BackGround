angular.module('kpi.college.flow', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller("hotSpot.flow",
        function($scope,
            $uibModalInstance,
            dialogTitle,
            hotSpots,
            theme,
            collegeQueryService,
            parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.hotSpotList = hotSpots;
            $scope.statCount = 0;
            $scope.query = function() {
                angular.forEach($scope.hotSpotList,
                    function(spot) {
                        collegeQueryService
                            .queryHotSpotFlow(spot.hotspotName, $scope.beginDate.value, $scope.endDate.value)
                            .then(function(stat) {
                                angular.extend(spot, stat);
                                $scope.statCount += 1;
                            });
                    });
            };
            $scope.$watch('statCount',
                function(count) {
                    if (count === $scope.hotSpotList.length && count > 0) {
                        $("#downloadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForDownlinkFlow($scope.hotSpotList, theme));
                        $("#uploadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForUplinkFlow($scope.hotSpotList, theme));
                        $("#averageUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForAverageUsers($scope.hotSpotList, theme));
                        $("#activeUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForActiveUsers($scope.hotSpotList, theme));
                        $scope.statCount = 0;
                    }
                });
            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.cell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.flow",
        function($scope, $uibModalInstance, dialogTitle, year, collegeQueryService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.collegeStatCount = 0;
            $scope.query = function() {
                angular.forEach($scope.collegeList,
                    function(college) {
                        collegeQueryService.queryCollegeFlow(college.name, $scope.beginDate.value, $scope.endDate.value)
                            .then(function(stat) {
                                angular.extend(college, stat);
                                $scope.collegeStatCount += 1;
                            });
                    });
            };
            $scope.$watch('collegeStatCount',
                function(count) {
                    if ($scope.collegeList && count === $scope.collegeList.length && count > 0) {
                        $("#downloadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForDownlinkFlow($scope.collegeList));
                        $("#uploadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForUplinkFlow($scope.collegeList));
                        $("#averageUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForAverageUsers($scope.collegeList));
                        $("#activeUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForActiveUsers($scope.collegeList));
                        $scope.collegeStatCount = 0;
                    }
                });
            collegeQueryService.queryYearList(year).then(function(colleges) {
                $scope.collegeList = colleges;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });