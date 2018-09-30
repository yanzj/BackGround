angular.module("myApp", ['app.common'])
    .controller('complain.adjust',
        function($scope,
            complainService) {

            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 30);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.query = function() {
                complainService.queryPositionList($scope.beginDate.value, $scope.endDate.value).then(function(list) {
                    $scope.items = list;
                });
            };


            $scope.query();
        });