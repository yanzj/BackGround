angular.module("myApp", ['app.common'])
    .controller('maintain.hotspot',
        function($scope,
            basicImportService,
            customerDialogService) {
            $scope.query = function() {
                basicImportService.queryAllHotSpots().then(function(result) {
                    $scope.hotSpotList = result;
                });
            };
            $scope.addHotSpot = function() {
                customerDialogService.constructHotSpot(function() {
                        $scope.updateMap();
                    });
            };

            $scope.query();
        });