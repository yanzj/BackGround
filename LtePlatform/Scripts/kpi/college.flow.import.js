﻿angular.module("myApp", ['app.common'])
    .controller("college.flows",
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
                        appRegionService.getCurrentDateTownFlowStats(stat.date, 'college').then(function(items) {
                            stat.items = items;
                            if (items.length < 20) {
                                collegeQueryService.retrieveDateCollegeFlowStats(stat.date).then(function(newItems) {
                                    stat.items = newItems;
                                    angular.forEach(newItems,
                                        function(item) {
                                            appRegionService.updateTownFlowStat(item).then(function(result) {});
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownCqiStats(stat.date, 'college').then(function (items) {
                            stat.cqis = items;
                            if (items.length < 20) {
                                collegeQueryService.retrieveDateCollegeCqiStats(stat.date).then(function (newItems) {
                                    
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownCqiStat(item).then(function (result) {
                                                stat.cqis = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownPrbStats(stat.date, 'college').then(function (items) {
                            stat.prbs = items;
                            if (items.length < 20) {
                                collegeQueryService.retrieveDateCollegePrbStats(stat.date).then(function (newItems) {
                                    
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownPrbStat(item).then(function (result) {
                                                stat.prbs = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownDoubleFlowStats(stat.date, 'college').then(function (items) {
                            stat.doubleFlows = items;
                            if (items.length < 20) {
                                collegeQueryService.retrieveDateCollegeDoubleFlowStats(stat.date).then(function (newItems) {
                                    
                                    angular.forEach(newItems,
                                        function (item) {
                                            appRegionService.updateTownDoubleFlowStat(item).then(function (result) {
                                                stat.doubleFlows = newItems;
                                            });
                                        });
                                });
                            }
                        });
                        appRegionService.getCurrentDateTownHourCqiStats(stat.date, 'college').then(function (items) {
                            stat.hourCqis = items;
                            if (items.length < 20) {
                                collegeQueryService.retrieveDateCollegeHourCqiStats(stat.date).then(function (newItems) {
                                    
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