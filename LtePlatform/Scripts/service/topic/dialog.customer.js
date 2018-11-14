angular.module('topic.dialog.customer', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('micro.dialog',
        function($scope, $uibModalInstance, dialogTitle, item, appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.detailsGroups = appFormatService.generateMicroAddressGroups(item);
            $scope.microGroups = [];
            angular.forEach(item.microItems,
                function(micro) {
                    $scope.microGroups.push(appFormatService.generateMicroItemGroups(micro));
                });
        });