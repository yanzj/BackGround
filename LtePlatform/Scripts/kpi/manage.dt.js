angular.module("myApp", ['app.common'])
    .controller("csv.dt",
        function($scope,
            collegeService) {
            
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 221);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };

            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };

            $scope.query = function() {
                collegeService.queryCsvFileNames($scope.beginDate.value, $scope.endDate.value).then(function(infos) {
                    $scope.fileInfos = infos;
                    angular.forEach(infos,
                        function(info) {
                            if (info.fileType) {
                                info.networkType = info.fileType;
                            } else {
                                collegeService.queryCsvFileType(info.csvFileName.replace('.csv', '')).then(
                                    function (type) {
                                        info.networkType = type;
                                    });
                            }
                            collegeService.queryFileTownDtTestInfo(info.id).then(function(items) {
                                info.townInfos = items;
                            });
                            collegeService.queryFileRoadDtTestInfo(info.id).then(function(items) {
                                info.roadInfos = items;
                            });
                        });
                });
            };

            $scope.query();
        });