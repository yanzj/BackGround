angular.module("myApp", ['app.common'])
    .controller("market.flows",
        function($scope,
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

            $scope.query = function() {
                $scope.stats = basicCalculationService
                    .generateDateSpanSeries($scope.beginDate.value, $scope.endDate.value);
                angular.forEach($scope.stats,
                    function(stat) {
                        appRegionService.getCurrentDateTownFlowStats(stat.date, 'market').then(function(items) {
                            stat.items = items;
                            if (items.length < 50) {
                                collegeQueryService.retrieveDateMarketFlowStats(stat.date).then(function(newItems) {
                                    stat.items = newItems;
                                    angular.forEach(newItems,
                                        function(item) {
                                            appRegionService.updateTownFlowStat(item).then(function(result) {});
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownCqiStats(stat.date, 'market').then(function (items) {
                            stat.cqis = items;
                            if (items.length < 50) {
                                collegeQueryService.retrieveDateMarketCqiStats(stat.date).then(function (newItems) {
                                    
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownCqiStat(item).then(function (result) {
                                                stat.cqis = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownPrbStats(stat.date, 'market').then(function (items) {
                            stat.prbs = items;
                            if (items.length < 50) {
                                collegeQueryService.retrieveDateMarketPrbStats(stat.date).then(function (newItems) {
                                    
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownPrbStat(item).then(function (result) {
                                                stat.prbs = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownHourCqiStats(stat.date, 'market').then(function (items) {
                            stat.hourCqis = items;
                            if (items.length < 50) {
                                collegeQueryService.retrieveDateMarketHourCqiStats(stat.date).then(function (newItems) {
                                    
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