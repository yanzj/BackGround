angular.module("myApp", ['app.common'])
    .controller("transportation.flows",
        function ($scope,
            basicCalculationService,
            appRegionService,
            collegeQueryService) {
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 21);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };

            $scope.query = function () {
                $scope.stats = basicCalculationService
                    .generateDateSpanSeries($scope.beginDate.value, $scope.endDate.value);
                angular.forEach($scope.stats,
                    function (stat) {
                        appRegionService.getCurrentDateTownFlowStats(stat.date, 'transportation').then(function (items) {
                            stat.items = items;
                            if (items.length < 40) {
                                collegeQueryService.retrieveDateTransportationFlowStats(stat.date).then(function (newItems) {
                                    stat.items = newItems;
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownFlowStat(item).then(function (result) { });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownCqiStats(stat.date, 'transportation').then(function (items) {
                            stat.cqis = items;
                            if (items.length < 40) {
                                collegeQueryService.retrieveDateTransportationCqiStats(stat.date).then(function (newItems) {

                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownCqiStat(item).then(function (result) {
                                                stat.cqis = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownPrbStats(stat.date, 'transportation').then(function (items) {
                            stat.prbs = items;
                            if (items.length < 40) {
                                collegeQueryService.retrieveDateTransportationPrbStats(stat.date).then(function (newItems) {

                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownPrbStat(item).then(function (result) {
                                                stat.prbs = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownDoubleFlowStats(stat.date, 'transportation').then(function (items) {
                            stat.doubleFlows = items;
                            if (items.length < 40) {
                                collegeQueryService.retrieveDateTransportationDoubleFlowStats(stat.date).then(function (newItems) {

                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownDoubleFlowStat(item).then(function (result) {
                                                stat.doubleFlows = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownHourCqiStats(stat.date, 'transportation').then(function (items) {
                            stat.hourCqis = items;
                            if (items.length < 40) {
                                collegeQueryService.retrieveDateTransportationHourCqiStats(stat.date).then(function (newItems) {

                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownHourCqiStat(item).then(function (result) {
                                                stat.hourCqis = newItems;
                                            });
                                        });
                                });
                            }
                        });
                    });
            };

            $scope.query();

        });