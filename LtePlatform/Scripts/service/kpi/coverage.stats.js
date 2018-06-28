angular.module('kpi.coverage.stats', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("town.stats",
        function($scope, cityName, dialogTitle, $uibModalInstance, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.queryDistrictInfrastructures(cityName).then(function(result) {
                appRegionService.accumulateCityStat(result, cityName);
                $("#leftChart").highcharts(
                    parametersChartService
                    .getDistrictLteENodebPieOptions(result.slice(0, result.length - 1), cityName));
                $("#rightChart").highcharts(
                    parametersChartService.getDistrictLteCellPieOptions(result.slice(0, result.length - 1), cityName));
                $("#thirdChart").highcharts(
                    parametersChartService.getDistrictLte800PieOptions(result.slice(0, result.length - 1), cityName));
                $("#fourthChart").highcharts(
                    parametersChartService
                    .getDistrictNbIotCellPieOptions(result.slice(0, result.length - 1), cityName));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("cdma.town.stats",
        function($scope, cityName, dialogTitle, $uibModalInstance, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.queryDistrictInfrastructures(cityName).then(function(result) {
                appRegionService.accumulateCityStat(result, cityName);
                $("#leftChart").highcharts(
                    parametersChartService.getDistrictCdmaBtsPieOptions(result.slice(0, result.length - 1), cityName));
                $("#rightChart").highcharts(
                    parametersChartService.getDistrictCdmaCellPieOptions(result.slice(0, result.length - 1), cityName));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("user.roles.dialog",
        function($scope, $uibModalInstance, dialogTitle, userName, authorizeService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query = function() {
                authorizeService.queryRolesInUser(userName).then(function(roles) {
                    $scope.existedRoles = roles;
                });
                authorizeService.queryCandidateRolesInUser(userName).then(function(roles) {
                    $scope.candidateRoles = roles;
                });
            };

            $scope.addRole = function(role) {
                authorizeService.assignRoleInUser(userName, role).then(function(result) {
                    if (result) {
                        $scope.query();
                    }
                });
            };

            $scope.removeRole = function(role) {
                authorizeService.releaseRoleInUser(userName, role).then(function(result) {
                    if (result) {
                        $scope.query();
                    }
                });
            };

            $scope.query();
        });
