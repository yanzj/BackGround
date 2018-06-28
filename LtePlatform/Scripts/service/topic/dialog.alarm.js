angular.module('topic.dialog.alarm', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('map.zero-voice.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateZeroVoiceGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.zero-flow.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateZeroFlowGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.resource.dialog',
        function($scope, $uibModalInstance, station, dialogTitle, downSwitchService) {
            $scope.station = station;
            $scope.dialogTitle = dialogTitle;
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.tab = 1;

            $scope.selectTab = function(setTab) {
                $scope.tab = setTab;
                if (0 === setTab) {
                    downSwitchService.getResourceCounter(station.id).then(function(response) {
                        $scope.counter = response.result;
                    });
                } else if (1 === setTab) {
                    $scope.table = "bts";
                } else if (2 === setTab) {
                    $scope.table = "enodeb";
                } else if (3 === setTab) {
                    $scope.table = "rru";
                } else if (4 === setTab) {
                    $scope.table = "lrru";
                } else if (5 === setTab) {
                    $scope.table = "sfz";
                } else if (6 === setTab) {
                    $scope.table = "zfz";
                } else if (7 === setTab) {
                    $scope.table = "asset";
                }
                if (0 !== setTab) {
                    downSwitchService.getResource($scope.table, station.id).then(function(response) {
                        $scope.resourceList = response.result;
                    });
                }
            }

            $scope.isSelectTab = function(checkTab) {
                return $scope.tab === checkTab
            }
            $scope.selectTab(0);
        });