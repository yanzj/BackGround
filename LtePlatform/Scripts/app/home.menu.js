angular.module('home.menu', ['app.common'])
    .controller("menu.root",
        function($scope) {
            $scope.menuItem = {
                displayName: "站点信息",
                subItems: [
                    {
                        displayName: "数据总览",
                        url: "/#/cells"
                    }, {
                        displayName: "室内分布",
                        url: "/#/analysis"
                    }, {
                        displayName: "数据查询",
                        url: "/#/query"
                    }, {
                        displayName: "流量经营",
                        url: '/#/flow'
                    }
                ]
            };
        })
    .controller("menu.alarm",
        function($scope, downSwitchService, baiduMapService, mapDialogService, baiduQueryService) {

            $scope.stationName = "";
            $scope.stations = [];

            $scope.search = function() {
                downSwitchService.getAlarmStationByName($scope.stationName, 1, 10).then(function(response) {
                    $scope.stations = response.result.rows;
                });
            }
            $scope.showStationInfo = function(index) {
                document.getElementById("cardlist").style.display = "none";
                mapDialogService.showAlarmStationInfo($scope.stations[index - 1]);
            }
            $scope.$watch('stations',
                function() {
                    baiduMapService.clearOverlays();
                    if (!$scope.stations.length)
                        return;
                    document.getElementById("cardlist").style.display = "inline";
                    baiduQueryService.transformToBaidu($scope.stations[0].longtitute, $scope.stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - $scope.stations[0].longtitute;
                            var yOffset = coors.y - $scope.stations[0].lattitute;
                            baiduMapService.drawPointsUsual($scope.stations,
                                -xOffset,
                                -yOffset,
                                function() {
                                    mapDialogService.showAlarmStationInfo(this.data, $scope.beginDate, $scope.endDate);
                                });
                        });
                });
        })
    .controller('menu.resource',
        function($scope) {
            $scope.menuItem = {
                displayName: "资源资产",
                subItems: [
                    {
                        displayName: "资源资产-基站",
                        url: '/#/resource'
                    }, {
                        displayName: "资源资产-室分",
                        url: '/#/resource-indoor'
                    }
                ]
            };
        })
    .controller('menu.plan',
        function($scope, appUrlService) {
            $scope.menuItem = {
                displayName: "规划支撑",
                subItems: [
                    {
                        displayName: "规划辅助",
                        url: appUrlService.getPlanUrlHost() + 'guihua'
                    }, {
                        displayName: "智能规划",
                        url: appUrlService.getPlanUrlHost() + 'guihua'
                    }
                ]
            };
        })
    .controller('menu.cdma', function($scope) {
        $scope.menuItem = {
            displayName: "2G优化",
            subItems: [
                {
                    displayName: "指标优化",
                    url: '/#/cdma'
                }, {
                    displayName: "干扰管理",
                    url: '/#/interference'
                }
            ]
        };
    })
    .controller('menu.mr',
        function($scope) {
            var rootUrl = "/#";
            $scope.menuItem = {
                displayName: "MR分析",
                subItems: [
                    {
                        displayName: "模拟路测",
                        url: rootUrl + "/mr"
                    }, {
                        displayName: "栅格分析",
                        url: rootUrl + "/grid"
                    }, {
                        displayName: "智能规划",
                        url: rootUrl + "/app"
                    }, {
                        displayName: "工单管控",
                        url: "/Kpi/WorkItem",
                        tooltip: "对接本部优化部4G网优平台，结合日常优化，实现对日常工单的监控和分析"
                    }
                ]
            }
        })
    .controller('menu.dt',
        function($scope, appUrlService) {
            var rootUrl = "/#";
            $scope.menuItem = {
                displayName: "路测管理",
                subItems: [
                    {
                        displayName: "路测数据总览",
                        url: rootUrl + "/dt"
                    }, {
                        displayName: "路测分析",
                        url: appUrlService.getDtUrlHost()
                    }, {
                        displayName: "CQT管理",
                        url: appUrlService.getPlanUrlHost() + 'CQT'
                    }
                ]
            };
        });