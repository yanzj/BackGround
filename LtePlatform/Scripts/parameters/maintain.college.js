angular.module("myApp", ['app.common'])
    .controller('maintain.college',
        function($scope,
            collegeService,
            collegeQueryService,
            collegeDialogService,
            appFormatService) {
            $scope.year = new Date().getYear() + 1900;
            $scope.collegeYearList = [];
            $scope.collegeList = [];
            $scope.collegeInfo = {
                names: []
            };

            $scope.updateInfos = function() {
                collegeService.queryStats($scope.year).then(function(colleges) {
                    $scope.collegeYearList = colleges;
                });
                collegeQueryService.queryYearList($scope.year).then(function(colleges) {
                    $scope.collegeList = colleges;
                });
                collegeService.queryNames().then(function(result) {
                    $scope.collegeInfo.names = result;
                    $scope.collegeName = $scope.collegeInfo.names[0];
                    $scope.updateCollegeStatus($scope.collegeName);
                });

            };
            $scope.updateCollegeStatus = function(name) {
                collegeQueryService.queryByNameAndYear(name, $scope.year).then(function(info) {
                    $scope.collegeExisted = !!info;
                });
            };
            $scope.$watch('collegeName',
                function(name) {
                    if (name) {
                        $scope.updateCollegeStatus(name);
                    }
                    
                });
            $scope.addOneCollegeMarkerInfo = function() {
                collegeQueryService.queryByNameAndYear($scope.collegeName, $scope.year - 1).then(function(item) {
                    if (!item) {
                        var begin = new Date();
                        begin.setDate(begin.getDate() - 365 - 7);
                        var end = new Date();
                        end.setDate(end.getDate() - 365);
                        collegeQueryService.queryByName($scope.collegeName).then(function(college) {
                            item = {
                                oldOpenDate: appFormatService.getDateString(begin, 'yyyy-MM-dd'),
                                newOpenDate: appFormatService.getDateString(end, 'yyyy-MM-dd'),
                                collegeId: college.id
                            };
                            collegeDialogService.addYearInfo(item,
                                $scope.collegeName,
                                $scope.year,
                                function() {
                                    $scope.updateInfos();
                                });
                        });
                    } else {
                        collegeDialogService.addYearInfo(item,
                            $scope.collegeName,
                            $scope.year,
                            function() {
                                $scope.updateInfos();
                            });
                    }
                });
            };
            $scope.createNewCollege = function() {
                collegeDialogService.addNewCollege(function() {
                    $scope.updateInfos();
                });
            };

            $scope.updateInfos();

        });