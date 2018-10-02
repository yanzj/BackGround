angular.module('kpi.parameter.dump', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('neighbors.dialog',
        function($scope,
            $uibModalInstance,
            geometryService,
            dialogTitle,
            candidateNeighbors,
            currentCell) {
            $scope.pciNeighbors = [];
            $scope.indoorConsidered = false;
            $scope.distanceOrder = "distance";
            $scope.dialogTitle = dialogTitle;
            $scope.candidateNeighbors = candidateNeighbors;
            $scope.currentCell = currentCell;

            angular.forEach($scope.candidateNeighbors,
                function(neighbor) {
                    neighbor.distance = geometryService.getDistance($scope.currentCell.lattitute,
                        $scope.currentCell.longtitute,
                        neighbor.lattitute,
                        neighbor.longtitute);

                    $scope.pciNeighbors.push(neighbor);
                });

            $scope.updateNearestCell = function() {
                var minDistance = 10000;
                angular.forEach($scope.candidateNeighbors,
                    function(neighbor) {
                        if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
                            minDistance = neighbor.distance;
                            $scope.nearestCell = neighbor;
                        }
                    });

            };

            $scope.ok = function() {
                $scope.updateNearestCell();
                $uibModalInstance.close($scope.nearestCell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });