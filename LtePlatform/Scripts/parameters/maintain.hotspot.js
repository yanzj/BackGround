angular.module("myApp", ['app.common'])
    .controller('maintain.hotspot',
        function($scope,
            basicImportService,
            customerDialogService) {
            $scope.query = function() {
                basicImportService.queryAllHotSpots().then(function(result) {
                    $scope.hotSpotList = _.filter(result,
                        function(stat) {
                            return stat.typeDescription !== '高速公路' &&
                                stat.typeDescription !== '高速铁路' &&
                                stat.typeDescription !== '地铁';
                        });
                });
            };
            $scope.addHotSpot = function() {
                customerDialogService.constructHotSpot(function() {
                        $scope.query();
                    });
            };

            $scope.query();
        });