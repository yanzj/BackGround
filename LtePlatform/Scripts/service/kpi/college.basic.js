angular.module('kpi.college.basic', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('year.info.dialog',
        function($scope,
            $uibModalInstance,
            appFormatService,
            name,
            year,
            item) {
            $scope.dialogTitle = name + year + "年校园信息补充";
            $scope.dto = item;
            $scope.beginDate = {
                value: appFormatService.getDate(item.oldOpenDate),
                opened: false
            };
            $scope.endDate = {
                value: appFormatService.getDate(item.newOpenDate),
                opened: false
            };
            $scope.beginDate.value.setDate($scope.beginDate.value.getDate() + 365);
            $scope.endDate.value.setDate($scope.endDate.value.getDate() + 365);

            $scope.ok = function() {
                $scope.dto.oldOpenDate = $scope.beginDate.value;
                $scope.dto.newOpenDate = $scope.endDate.value;
                $scope.dto.year = year;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('trace.planning.dialog',
        function($scope,
            $uibModalInstance,
            collegeName,
            baiduQueryService,
            collegeService,
            networkElementService,
            neighborImportService,
            collegeMapService) {
            $scope.dialogTitle = collegeName + "校园网规划站点跟踪";

            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        collegeService.queryRange(collegeName).then(function(range) {
                            networkElementService
                                .queryRangePlanningSites(neighborImportService.generateRange(range, center, coors))
                                .then(function(results) {
                                    $scope.items = results;
                                });
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($("#reports").html());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.college.dialog',
        function($scope,
            $uibModalInstance,
            college,
            year,
            dialogTitle,
            collegeQueryService,
            generalChartService,
            kpiChartCalculateService,
            emergencyService) {
            $scope.college = college;
            $scope.dialogTitle = dialogTitle;
            $scope.query = function() {
                collegeQueryService.queryCollegeDateFlows(college.name, $scope.beginDate.value, $scope.endDate.value)
                    .then(function (stats) {
                        angular.forEach(stats,
                            function (stat) {
                                stat.pdcpDownlinkFlow /= 8;
                                stat.pdcpUplinkFlow /= 8;
                            });
                        $("#flowConfig").highcharts(kpiChartCalculateService.generateMergeFeelingOptions(stats, college.name));
                        $("#usersConfig").highcharts(kpiChartCalculateService.generateMergeUsersOptions(stats, college.name));
                        $("#downSwitchConfig").highcharts(kpiChartCalculateService.generateMergeDownSwitchOptions(stats, college.name));
                    });
            };
            $scope.query();
            collegeQueryService.queryByNameAndYear(college.name, year).then(function(info) {
                if (info) {
                    $scope.college.expectedSubscribers = info.expectedSubscribers;
                    $scope.college.oldOpenDate = info.oldOpenDate;
                    $scope.college.newOpenDate = info.newOpenDate;
                }
            });
            emergencyService.queryCollegeVipDemand(year, college.name).then(function(item) {
                if (item) {
                    $scope.college.serialNumber = item.serialNumber;
                    $scope.college.currentStateDescription = item.currentStateDescription;
                }
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.college);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.query.name",
        function($scope, $uibModalInstance, dialogTitle, name, collegeService) {
            $scope.collegeName = name;
            $scope.dialogTitle = dialogTitle;
            $scope.updateLteCells = function() {
                collegeService.queryCells($scope.collegeName).then(function(cells) {
                    $scope.cellList = cells;
                });
            };
            collegeService.queryENodebs($scope.collegeName).then(function(eNodebs) {
                $scope.eNodebList = eNodebs;
            });
            $scope.updateLteCells();
            collegeService.queryBtss($scope.collegeName).then(function(btss) {
                $scope.btsList = btss;
            });
            collegeService.queryCdmaCells($scope.collegeName).then(function(cells) {
                $scope.cdmaCellList = cells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.college);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });