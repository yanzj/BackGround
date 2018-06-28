angular.module('app.chart', ['app.format', 'app.calculation'])
    .factory('chartCalculateService',
        function (appFormatService, basicCalculationService) {
            return {
                generateDrillDownData: function(districtStats, townStats, queryFunction) {
                    var results = [];
                    angular.forEach(districtStats,
                        function(districtStat) {
                            var subData = [];
                            var district = districtStat.district;
                            var districtData = queryFunction(districtStat);
                            angular.forEach(townStats,
                                function(townStat) {
                                    if (townStat.district === district) {
                                        subData.push([townStat.town, queryFunction(townStat)]);
                                    }
                                });

                            results.push({
                                district: district,
                                districtData: districtData,
                                subData: subData
                            });
                        });
                    return results;
                },
                generateDrillDownPieOptions: function(stats, settings) {
                    var chart = new DrilldownPie();
                    chart.initialize(settings);
                    angular.forEach(stats,
                        function(stat) {
                            chart.addOneSeries({
                                name: stat.type,
                                value: stat.total,
                                subData: stat.subData
                            });
                        });
                    return chart.options;
                },
                generateDrillDownPieOptionsWithFunc: function(stats, settings, func) {
                    var chart = new DrilldownPie();
                    chart.initialize(settings);
                    angular.forEach(stats,
                        function(stat) {
                            chart.addOneSeries({
                                name: func.nameFunc(stat),
                                value: func.valueFunc(stat),
                                subData: stat.subData
                            });
                        });
                    return chart.options;
                },
                generateDrillDownColumnOptionsWithFunc: function(stats, settings, func) {
                    var chart = new DrilldownColumn();
                    chart.initialize(settings);
                    angular.forEach(stats,
                        function(stat) {
                            chart.addOneSeries({
                                name: func.nameFunc(stat),
                                value: func.valueFunc(stat),
                                subData: stat.subData
                            });
                        });
                    return chart.options;
                },
                generateSplineChartOptions: function(result, districts, settings) {
                    var chart = new ComboChart();
                    chart.initialize(settings);
                    chart.xAxis[0].categories = result.statDates;

                    angular.forEach(districts,
                        function(district, index) {
                            chart.series.push({
                                type: "spline",
                                name: district,
                                data: result.districtStats[index]
                            });
                        });
                    return chart.options;
                },
                generateSingleSeriesBarOptions: function(stats, categoryKey, dataKey, settings) {
                    var chart = new BarChart();
                    chart.title.text = settings.title;
                    chart.legend.enabled = false;
                    var category = _.map(stats,
                        function(stat) {
                            return stat[categoryKey];
                        });
                    var precise = _.map(stats,
                        function(stat) {
                            return stat[dataKey];
                        });
                    category.push(settings.summaryStat[categoryKey]);
                    precise.push(settings.summaryStat[dataKey]);
                    chart.xAxis.categories = category;
                    chart.xAxis.title.text = settings.xTitle;
                    var yAxisConfig = basicCalculationService.generateYAxisConfig(settings);
                    chart.setDefaultYAxis(yAxisConfig);
                    var series = {
                        name: settings.seriesName,
                        data: precise
                    };
                    chart.asignSeries(series);
                    return chart.options;
                },
                generateMultiSeriesFuncBarOptions: function (stats, categoryKey, seriesDefs, settings) {
                    var chart = new BarChart();
                    chart.title.text = settings.title;
                    chart.legend.enabled = false;
                    var category = _.map(stats,
                        function(stat) {
                            return stat[categoryKey];
                        });
                    chart.xAxis.categories = category;
                    chart.xAxis.title.text = settings.xTitle;
                    var yAxisConfig = basicCalculationService.generateYAxisConfig(settings);
                    chart.setDefaultYAxis(yAxisConfig);
                    angular.forEach(seriesDefs,
                        function(def) {
                            var precise = _.map(stats,
                                function(stat) {
                                    return def.dataFunc(stat);
                                });
                            var series = {
                                name: def.seriesName,
                                data: precise
                            };
                            chart.addSeries(series, 'bar');
                        });
                    
                    return chart.options;
                },
                calculateMemberSum: function(array, memberList, categoryFunc) {
                    var result = basicCalculationService.calculateArraySum(array, memberList);
                    categoryFunc(result);
                    return result;
                },
                generateDistrictStats: function(districts, stats, funcs) {
                    var outputStats = [];
                    var initFuncs =
                    {
                        districtViewFunc: function(stat) { return 0; },
                        initializeFunc: function(stat) { return 0; },
                        calculateFunc: function (stat) { return 0; },
                        accumulateFunc: function (stat, view) { return 0; },
                        zeroFunc: function () { return 0; },
                        totalFunc: function (stat) { return 0; }
                    };
                    funcs = funcs || initFuncs;
                    funcs.districtViewFunc = funcs.districtViewFunc || initFuncs.districtViewFunc;
                    funcs.initializeFunc = funcs.initializeFunc || initFuncs.initializeFunc;
                    funcs.calculateFunc = funcs.calculateFunc || initFuncs.calculateFunc;
                    funcs.accumulateFunc = funcs.accumulateFunc || initFuncs.accumulateFunc;
                    funcs.zeroFunc = funcs.zeroFunc || initFuncs.zeroFunc;
                    funcs.totalFunc = funcs.totalFunc || initFuncs.totalFunc;
                    angular.forEach(stats,
                        function(stat) {
                            var districtViews = funcs.districtViewFunc(stat);
                            var statDate = stat.statDate;
                            var generalStat = {};
                            funcs.initializeFunc(generalStat);
                            var values = [];
                            angular.forEach(districts,
                                function(district) {
                                    for (var k = 0; k < districtViews.length; k++) {
                                        var view = districtViews[k];
                                        if (view.district === district) {
                                            values.push(funcs.calculateFunc(view));
                                            funcs.accumulateFunc(generalStat, view);
                                            break;
                                        }
                                    }
                                    if (k === districtViews.length) {
                                        values.push(funcs.zeroFunc());
                                    }
                                });
                            values.push(funcs.totalFunc(generalStat));
                            outputStats.push({
                                statDate: statDate,
                                values: values
                            });
                        });
                    return outputStats;
                },
                generateDateDistrictStats: function(stats, districtLength, queryFunction) {
                    var statDates = [];
                    var districtStats = [];
                    angular.forEach(stats,
                        function(stat, index) {
                            statDates.push(stat.statDate.split('T')[0]);
                            for (var j = 0; j < districtLength; j++) {
                                var statValue = stat.values[j] ? queryFunction(stat.values[j]) : 0;
                                if (index === 0) {
                                    districtStats.push([statValue]);
                                } else {
                                    districtStats[j].push(statValue);
                                }
                            }
                        });
                    return {
                        statDates: statDates,
                        districtStats: districtStats
                    };
                },
                generateStatsForPie: function(trendStat, result, funcs) {
                    trendStat.districtStats = funcs.districtViewsFunc(result[0]);
                    trendStat.townStats = funcs.townViewsFunc(result[0]);
                    for (var i = 1; i < result.length; i++) {
                        angular.forEach(funcs.districtViewsFunc(result[i]),
                            function(currentDistrictStat) {
                                var found = false;
                                for (var k = 0; k < trendStat.districtStats.length; k++) {
                                    if (trendStat.districtStats[k].city === currentDistrictStat.city &&
                                        trendStat.districtStats[k].district === currentDistrictStat.district) {
                                        funcs.accumulateFunc(trendStat.districtStats[k], currentDistrictStat);
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found) {
                                    trendStat.districtStats.push(currentDistrictStat);
                                }
                            });
                        angular.forEach(funcs.townViewsFunc(result[i]),
                            function(currentTownStat) {
                                var found = false;
                                for (var k = 0; k < trendStat.townStats.length; k++) {
                                    if (trendStat.townStats[k].city === currentTownStat.city &&
                                        trendStat.townStats[k].district === currentTownStat.district &&
                                        trendStat.townStats[k].town === currentTownStat.town) {
                                        funcs.accumulateFunc(trendStat.townStats[k], currentTownStat);
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found) {
                                    trendStat.townStats.push(currentTownStat);
                                }
                            });
                    }
                    angular.forEach(trendStat.districtStats,
                        function(stat) {
                            if (funcs.districtCalculate) {
                                funcs.districtCalculate(stat);
                            }

                        });
                    angular.forEach(trendStat.townStats,
                        function(stat) {
                            if (funcs.townCalculate) {
                                funcs.townCalculate(stat);
                            }
                        });
                },
                generateSeriesInfo: function(seriesInfo, stats, categoryKey, dataKeys) {
                    var categories = [];
                    angular.forEach(dataKeys,
                        function(key) {
                            seriesInfo[key].data = [];
                        });
                    angular.forEach(stats,
                        function(stat) {
                            categories.push(stat[categoryKey]);
                            angular.forEach(dataKeys,
                                function(key) {
                                    seriesInfo[key].data.push(stat[key]);
                                });
                        });
                    return {
                        categories: categories,
                        info: seriesInfo
                    };
                },
                writeSeriesData: function(series, seriesInfo, dataKeys) {
                    angular.forEach(dataKeys,
                        function(key) {
                            series.push({
                                type: seriesInfo[key].type,
                                name: seriesInfo[key].name,
                                data: seriesInfo[key].data
                            });
                        });
                },
                generateMrsRsrpStats: function(stats) {
                    var categories = _.range(-140, -43);
                    var values = _.range(0, 97, 0);
                    var array = _.map(_.range(48),
                        function(index) {
                            var value = stats['rsrP_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    var i;
                    for (i = 2; i < 37; i++) {
                        values[i + 24] = array[i];
                    }
                    for (i = 37; i < 47; i++) {
                        values[2 * i - 13] = values[2 * i - 12] = array[i] / 2;
                    }
                    var tail = array[47];
                    for (i = 0; i < 17; i++) {
                        if (tail > values[80 + i] / 2) {
                            tail -= values[80 + i] / 2 + 1;
                            values[81 + i] = values[80 + i] / 2 + 1;
                        } else {
                            values[81 + i] = tail;
                            break;
                        }
                    }
                    var avg = array[1] / 5;
                    var step = (values[26] - avg) / 3;
                    for (i = 0; i < 5; i++) {
                        values[21 + i] = avg + (i - 2) * step;
                    }
                    var head = values[21];
                    for (i = 0; i < 21; i++) {
                        if (head > values[21 - i] / 2) {
                            head -= values[21 - i] / 2 + 1;
                            values[21 - i - 1] = values[21 - i] / 2 + 1;
                        } else {
                            values[21 - i - 1] = head;
                        }
                    }
                    return {
                        categories: categories,
                        values: values
                    };
                },
                generateCoverageStats: function(stats) {
                    var array = _.map(_.range(48),
                        function(index) {
                            var value = stats['rsrP_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    var sum = _.reduce(array, function(memo, num) { return memo + num; }, 0);
                    return {
                        total: sum,
                        sumBelow115: array[0] + array[1],
                        sumBetween115And110: array[2] + array[3] + array[4] + array[5] + array[6],
                        sumBetween110And105: array[7] + array[8] + array[9] + array[10] + array[11]
                    };
                },
                generateMrsTaStats: function(stats) {
                    var array = _.map(_.range(45),
                        function(index) {
                            var value = stats['tadv_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    return {
                        categories: [
                            100,
                            200,
                            300,
                            400,
                            500,
                            600,
                            700,
                            800,
                            900,
                            1000,
                            1150,
                            1300,
                            1450,
                            1600,
                            1750,
                            1900,
                            2050,
                            2200,
                            2350,
                            2500,
                            2700,
                            2900,
                            3100,
                            3300,
                            3500,
                            3700,
                            3900,
                            4100,
                            4300,
                            4500,
                            4700,
                            4900,
                            5500,
                            6500,
                            8000,
                            9500,
                            11500,
                            15000,
                            20000
                        ],
                        values: [
                            array[0] + 0.280082 * array[1],
                            0.719918 * array[1] + 0.560164 * array[2],
                            0.489836 * array[2] + 0.840246 * array[3],
                            0.159754 * array[3] + array[4] + 0.120328 * array[5],
                            0.879672 * array[5] + 0.40041 * array[6],
                            0.59959 * array[6] + 0.680492 * array[7],
                            0.319508 * array[7] + 0.960593 * array[8],
                            0.039427 * array[8] + array[9] + 0.240655 * array[10],
                            0.759345 * array[11] + 0.520737 * array[12],
                            0.479263 * array[12] + 0.40041 * array[13],
                            0.59959 * array[13] + 0.360471 * array[14],
                            0.639529 * array[14] + 0.320533 * array[15],
                            0.679467 * array[15] + 0.280594 * array[16],
                            0.719406 * array[16] + 0.240655 * array[17],
                            0.759345 * array[17] + 0.200717 * array[18],
                            0.799283 * array[18] + 0.160778 * array[19],
                            0.839222 * array[19] + 0.12084 * array[20],
                            0.87916 * array[20] + 0.080901 * array[21],
                            0.919099 * array[21] + 0.040963 * array[22],
                            0.959037 * array[22] + 0.001024 * array[23],
                            0.998976 * array[23] + 0.281106 * array[24],
                            0.718894 * array[24] + 0.561188 * array[25],
                            0.438812 * array[25] + 0.84127 * array[26],
                            0.15873 * array[26] + array[27] + 0.121352 * array[28],
                            0.878648 * array[28] + 0.401434 * array[29],
                            0.598566 * array[29] + 0.681516 * array[30],
                            0.318484 * array[30] + 0.961598 * array[31],
                            0.038402 * array[31] + array[32] + 0.241679 * array[33],
                            0.758321 * array[33] + 0.521761 * array[34],
                            0.478239 * array[34] + 0.801843 * array[35],
                            0.198157 * array[35] + array[36] + 0.081925 * array[37],
                            0.918075 * array[37] + 0.362007 * array[38],
                            0.637993 * array[38] + 0.400282 * array[39],
                            0.599718 * array[39] + 0.200333 * array[40],
                            0.799667 * array[40] + 0.40041 * array[41],
                            0.59959 * array[41] + 0.600486 * array[42],
                            0.399514 * array[42] + 0.300147 * array[43],
                            0.699853 * array[43] + 0.000192 * array[44],
                            0.999808 * array[44]
                        ]
                    };
                },
                generateOverCoverageStats: function(stats) {
                    var array = _.map(_.range(45),
                        function(index) {
                            var value = stats['tadv_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    var arrayOver = _.map(_.range(18, 45),
                        function(index) {
                            var value = stats['tadv_' + appFormatService.prefixInteger(index, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    return {
                        total: _.reduce(array, function(memo, num) { return memo + num; }, 0),
                        over: _.reduce(arrayOver, function(memo, num) { return memo + num; }, 0)
                    }
                },
                generateRsrpTaStats: function(stats, rsrpIndex) {
                    var rsrpDivisions = [
                        -110,
                        -105,
                        -100,
                        -95,
                        -90,
                        -85,
                        -80,
                        -75,
                        -70,
                        -65,
                        -60,
                        -44
                    ];
                    rsrpIndex = Math.min(Math.max(0, rsrpIndex), 11);
                    var array = _.map(_.range(11),
                        function(index) {
                            var value = stats['tadv' +
                                appFormatService.prefixInteger(index, 2) +
                                'Rsrp' +
                                appFormatService.prefixInteger(rsrpIndex, 2)];
                            return _.isNumber(value) ? value : 0;
                        });
                    return {
                        seriesName: rsrpIndex === 0
                            ? 'RSRP<-110dBm'
                            : rsrpDivisions[rsrpIndex - 1] + 'dBm<=RSRP<' + rsrpDivisions[rsrpIndex] + 'dBm',
                        categories: [
                            100,
                            200,
                            300,
                            400,
                            500,
                            600,
                            700,
                            800,
                            900,
                            1000,
                            1200,
                            1400,
                            1600,
                            1800,
                            2000,
                            2400,
                            2800,
                            3200,
                            3600,
                            4000,
                            5000,
                            6000,
                            8000,
                            10000,
                            15000
                        ],
                        values: [
                            0.426693975 * array[0],
                            0.426693975 * array[0],
                            0.14661205 * array[0] + 0.280081925 * array[1],
                            0.426693975 * array[1],
                            0.2932241 * array[1] + 0.133469875 * array[2],
                            0.426693975 * array[2],
                            0.426693975 * array[2],
                            0.013142174 * array[2] + 0.413551801 * array[3],
                            0.426693975 * array[3],
                            0.159754224 * array[3] + 0.133469875 * array[4],
                            0.426693975 * array[4],
                            0.426693975 * array[4],
                            0.013142174 * array[4] + 0.413551801 * array[5],
                            0.426693975 * array[5],
                            0.159754224 * array[5] + 0.133469875 * array[6],
                            0.426693975 * array[6],
                            0.426693975 * array[6],
                            0.013142174 * array[6] + 0.413551801 * array[7],
                            0.426693975 * array[7],
                            0.159754224 * array[7] + 0.266939751 * array[8],
                            0.129363573 * array[8] + 0.058883769 * array[9],
                            0.188247342 * array[9],
                            0.376494684 * array[9],
                            0.376374206 * array[9] + 0.000064 * array[10],
                            0.500032002 * array[10]
                        ]
                    };
                },
                updateHeatMapIntervalDefs: function(intervalDef, max) {
                    var gradient = {};
                    var heatIntervals = [
                        {
                            key: 0.2,
                            color: 'rgb(100,255,100)'
                        }, {
                            key: 0.5,
                            color: 'rgb(100, 255, 255)'
                        }, {
                            key: 0.7,
                            color: 'rgb(255,100,200)'
                        }, {
                            key: 0.85,
                            color: 'rgb(255,100,100)'
                        }
                    ];
                    angular.forEach(heatIntervals,
                        function(interval) {
                            gradient[interval.key] = interval.color;
                            intervalDef.push({
                                color: interval.color,
                                threshold: interval.key * max
                            });
                        });
                    return gradient;
                },
                generateDistrictKpiDefs: function(kpiFields) {
                    return [
                        { field: 'city', name: '城市' },
                        {
                            name: '区域',
                            cellTemplate: appFormatService.generateDistrictButtonTemplate()
                        }
                    ].concat(kpiFields).concat([
                        {
                            name: '处理',
                            cellTemplate: appFormatService.generateProcessButtonTemplate()
                        },
                        {
                            name: '分析',
                            cellTemplate: appFormatService.generateAnalyzeButtonTemplate()
                        }
                    ]);
                }
            };
        })
    .factory('calculateService',
        function(chartCalculateService, appFormatService) {
            return {
                calculateOverCoverageRate: function(taList) {
                    var sum = 0;
                    var sumOver = 0;
                    angular.forEach(taList,
                        function(ta) {
                            var stat = chartCalculateService.generateOverCoverageStats(ta);
                            sum += stat.total;
                            sumOver += stat.over;
                        });
                    return sumOver / sum;
                },
                calculateWeakCoverageRate: function(coverageList) {
                    var sum = 0;
                    var sum115 = 0;
                    var sum110 = 0;
                    var sum105 = 0;
                    angular.forEach(coverageList,
                        function(coverage) {
                            var stat = chartCalculateService.generateCoverageStats(coverage);
                            sum += stat.total;
                            sum115 += stat.sumBelow115;
                            sum110 += stat.sumBetween115And110;
                            sum105 += stat.sumBetween110And105;
                        });
                    return {
                        rate115: sum115 / sum,
                        rate110: (sum115 + sum110) / sum,
                        rate105: (sum115 + sum110 + sum105) / sum
                    };
                },
                generateGridDirective: function(settings, $compile) {
                    return {
                        controller: settings.controllerName,
                        restrict: 'EA',
                        replace: true,
                        scope: settings.scope,
                        template: '<div></div>',
                        link: function(scope, element, attrs) {
                            scope.initialize = false;
                            scope.$watch(settings.argumentName,
                                function(items) {
                                    scope.gridOptions.data = items;
                                    if (!scope.initialize) {
                                        var linkDom = $compile('<div ui-grid="gridOptions"></div>')(scope);
                                        element.append(linkDom);
                                        scope.initialize = true;
                                    }
                                });
                        }
                    };
                },
                generateShortGridDirective: function(settings, $compile) {
                    return {
                        controller: settings.controllerName,
                        restrict: 'EA',
                        replace: true,
                        scope: settings.scope,
                        template: '<div></div>',
                        link: function(scope, element, attrs) {
                            scope.initialize = false;
                            scope.$watch(settings.argumentName,
                                function(items) {
                                    scope.gridOptions.data = items;
                                    if (!scope.initialize) {
                                        var linkDom =
                                            $compile('<div style="height: 150px" ui-grid="gridOptions"></div>')(scope);
                                        element.append(linkDom);
                                        scope.initialize = true;
                                    }
                                });
                        }
                    };
                },
                generatePagingGridDirective: function(settings, $compile) {
                    return {
                        controller: settings.controllerName,
                        restrict: 'EA',
                        replace: true,
                        scope: settings.scope,
                        template: '<div></div>',
                        link: function(scope, element, attrs) {
                            scope.initialize = false;
                            scope.$watch(settings.argumentName,
                                function(items) {
                                    scope.gridOptions.data = items;
                                    if (!scope.initialize) {
                                        var linkDom =
                                            $compile('<div ui-grid="gridOptions" ui-grid-pagination style="height: 600px"></div>')(scope);
                                        element.append(linkDom);
                                        scope.initialize = true;
                                    }
                                });
                        }
                    };
                },
                generateSelectionGridDirective: function(settings, $compile) {
                    return {
                        controller: settings.controllerName,
                        restrict: 'EA',
                        replace: true,
                        scope: settings.scope,
                        template: '<div></div>',
                        link: function(scope, element, attrs) {
                            scope.initialize = false;
                            scope.$watch(settings.argumentName,
                                function(items) {
                                    scope.gridOptions.data = items;
                                    if (!scope.initialize) {
                                        var linkDom =
                                            $compile('<div ui-grid="gridOptions" ui-grid-selection></div>')(scope);
                                        element.append(linkDom);
                                        scope.initialize = true;
                                    }
                                });
                        }
                    };
                },
                mergeDataByKey: function(list, data, key, dataKeys) {
                    angular.forEach(data,
                        function(num) {
                            var finder = {};
                            finder[key] = num[key];
                            var match = _.where(list, finder);
                            if (match.length > 0) {
                                angular.forEach(dataKeys,
                                    function(dataKey) {
                                        match[0][dataKey] += num[dataKey];
                                    });
                            } else {
                                var newNum = {};
                                newNum[key] = num[key];
                                angular.forEach(dataKeys,
                                    function(dataKey) {
                                        newNum[dataKey] = num[dataKey];
                                    });
                                list.push(newNum);
                            }
                        });
                    return _.sortBy(list, function(num) { return num[key]; });
                },

                generateCellDetailsGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '频点',
                                    value: site.frequency
                                }, {
                                    key: '经度',
                                    value: site.longtitute
                                }, {
                                    key: '纬度',
                                    value: site.lattitute
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '天线挂高',
                                    value: site.height
                                }, {
                                    key: '方位角',
                                    value: site.azimuth
                                }, {
                                    key: '下倾角',
                                    value: site.downTilt
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '室内外',
                                    value: site.indoor
                                }, {
                                    key: '天线增益',
                                    value: site.antennaGain
                                }, {
                                    key: 'RS功率',
                                    value: site.rsPower
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: 'PCI',
                                    value: site.pci
                                }, {
                                    key: 'PRACH',
                                    value: site.prach
                                }, {
                                    key: 'TAC',
                                    value: site.tac
                                }
                            ]
                        }
                    ];
                },
                generateFlowDetailsGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '小区名称',
                                    value: site.eNodebName + '-' + site.sectorId
                                }, {
                                    key: '经度',
                                    value: site.longtitute
                                }, {
                                    key: '纬度',
                                    value: site.lattitute
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '天线挂高',
                                    value: site.height
                                }, {
                                    key: '方位角',
                                    value: site.azimuth
                                }, {
                                    key: '下倾角',
                                    value: site.downTilt
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '室内外',
                                    value: site.indoor
                                }, {
                                    key: '天线增益',
                                    value: site.antennaGain
                                }, {
                                    key: 'RS功率',
                                    value: site.rsPower
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '下行流量（MB）',
                                    value: site.pdcpDownlinkFlow
                                }, {
                                    key: '上行流量（MB）',
                                    value: site.pdcpUplinkFlow
                                }, {
                                    key: '用户数',
                                    value: site.maxUsers
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '下行感知速率（Mbit/s）',
                                    value: site.downlinkFeelingRate
                                }, {
                                    key: '上行感知速率（Mbit/s）',
                                    value: site.uplinkFeelingRate
                                }, {
                                    key: 'PCI',
                                    value: site.pci
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '4G下切3G次数',
                                    value: site.redirectCdma2000
                                }, {
                                    key: '4G双流比',
                                    value: site.rank2Rate
                                }, {
                                    key: 'PRACH',
                                    value: site.prach
                                }
                            ]
                        }
                    ];
                },
                generateRrcDetailsGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '基站编号',
                                    value: site.eNodebId
                                }, {
                                    key: '扇区编号',
                                    value: site.sectorId
                                }, {
                                    key: '统计日期',
                                    value: site.statTime,
                                    format: 'date'
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '被叫接入RRC请求次数',
                                    value: site.mtAccessRrcRequest
                                }, {
                                    key: '被叫接入RRC失败次数',
                                    value: site.mtAccessRrcFail
                                }, {
                                    key: '被叫接入RRC成功率',
                                    value: 100 * site.mtAccessRrcRate,
                                    format: 'percentage'
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '主叫信令RRC请求次数',
                                    value: site.moSignallingRrcRequest
                                }, {
                                    key: '主叫信令RRC失败次数',
                                    value: site.moSignallingRrcFail
                                }, {
                                    key: '主叫信令RRC成功率',
                                    value: 100 * site.moSiganllingRrcRate,
                                    format: 'percentage'
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '主叫数据RRC请求次数',
                                    value: site.moDataRrcRequest
                                }, {
                                    key: '主叫数据RRC失败次数',
                                    value: site.moDataRrcFail
                                }, {
                                    key: '主叫数据RRC成功率',
                                    value: 100 * site.moDataRrcRate,
                                    format: 'percentage'
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '全部RRC请求次数',
                                    value: site.totalRrcRequest
                                }, {
                                    key: '全部RRC失败次数',
                                    value: site.totalRrcFail
                                }, {
                                    key: '总体RRC成功率',
                                    value: 100 * site.rrcSuccessRate,
                                    format: 'percentage'
                                }
                            ]
                        }
                    ];
                },
                generateCellMongoGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '网管PCI',
                                    value: site.phyCellId
                                }, {
                                    key: '网管PRACH',
                                    value: site.rootSequenceIdx
                                }, {
                                    key: '本地小区标识（仅华为有效）',
                                    value: site.localCellId
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '服务小区偏移量',
                                    value: site.cellSpecificOffset,
                                    filter: 'dbStep32'
                                }, {
                                    key: '服务小区偏置',
                                    value: site.qoffsetFreq,
                                    filter: 'dbStep32'
                                }, {
                                    key: '前导格式',
                                    value: site.preambleFmt
                                }
                            ]
                        }
                    ];
                },

                accumulatePreciseStat: function(source, accumulate) {
                    source.totalMrs += accumulate.totalMrs;
                    source.firstNeighbors += accumulate.firstNeighbors;
                    source.secondNeighbors += accumulate.secondNeighbors;
                    source.thirdNeighbors += accumulate.thirdNeighbors;
                },
                accumulateRrcStat: function(source, accumulate) {
                    source.totalRrcRequest += accumulate.totalRrcRequest;
                    source.totalRrcSuccess += accumulate.totalRrcSuccess;
                    source.moDataRrcRequest += accumulate.moDataRrcRequest;
                    source.moDataRrcSuccess += accumulate.moDataRrcSuccess;
                    source.moSignallingRrcRequest += accumulate.moSignallingRrcRequest;
                    source.moSignallingRrcSuccess += accumulate.moSignallingRrcSuccess;
                    source.mtAccessRrcRequest += accumulate.mtAccessRrcRequest;
                    source.mtAccessRrcSuccess += accumulate.mtAccessRrcSuccess;
                },
                accumulateCqiStat: function(source, accumulate) {
                    source.goodCounts += accumulate.goodCounts;
                    source.totalCounts += accumulate.totalCounts;
                },
                accumulateFlowStat: function(source, accumulate) {
                    source.pdcpDownlinkFlow += accumulate.pdcpDownlinkFlow;
                    source.pdcpUplinkFlow += accumulate.pdcpUplinkFlow;
                    source.redirectCdma2000 += accumulate.redirectCdma2000;
                },
                calculateDistrictRates: function(districtStat) {
                    districtStat.firstRate = 100 - 100 * districtStat.firstNeighbors / districtStat.totalMrs;
                    districtStat.preciseRate = 100 - 100 * districtStat.secondNeighbors / districtStat.totalMrs;
                    districtStat.thirdRate = 100 - 100 * districtStat.thirdNeighbors / districtStat.totalMrs;
                    return districtStat;
                },
                calculateDistrictRrcRates: function(districtStat) {
                    districtStat.rrcSuccessRate = 100 * districtStat.totalRrcSuccess / districtStat.totalRrcRequest;
                    districtStat.moSiganllingRrcRate = 100 *
                        districtStat.moDataRrcSuccess /
                        districtStat.moDataRrcRequest;
                    districtStat.moDataRrcRate = 100 * districtStat.moDataRrcSuccess / districtStat.moDataRrcRequest;
                    districtStat.mtAccessRrcRate = 100 *
                        districtStat.mtAccessRrcSuccess /
                        districtStat.mtAccessRrcRequest;
                    return districtStat;
                },
                calculateDistrictCqiRates: function(districtStat) {
                    districtStat.cqiRate = 100 * districtStat.goodCounts / districtStat.totalCounts;
                    return districtStat;
                },
                calculateDistrictFlowRates: function(districtStat) {
                    districtStat.totalFlowMByte = (districtStat.pdcpDownlinkFlow + districtStat.pdcpUplinkFlow) / 8;
                    districtStat.downSwitchRate = 1024 * districtStat.redirectCdma2000 / districtStat.totalFlowMByte;
                    return districtStat;
                },

                calculateTownRates: function(townStat) {
                    townStat.firstRate = 100 - 100 * townStat.firstNeighbors / townStat.totalMrs;
                    townStat.preciseRate = 100 - 100 * townStat.secondNeighbors / townStat.totalMrs;
                    townStat.thirdRate = 100 - 100 * townStat.thirdNeighbors / townStat.totalMrs;
                },
                calculateTownRrcRates: function(townStat) {
                    townStat.rrcSuccessRate = 100 * townStat.totalRrcSuccess / townStat.totalRrcRequest;
                    townStat.moSiganllingRrcRate = 100 * townStat.moDataRrcSuccess / townStat.moDataRrcRequest;
                    townStat.moDataRrcRate = 100 * townStat.moDataRrcSuccess / townStat.moDataRrcRequest;
                    townStat.mtAccessRrcRate = 100 * townStat.mtAccessRrcSuccess / townStat.mtAccessRrcRequest;
                },
                getValueFromDivisionAbove: function(division, value) {
                    for (var i = 0; i < division.length; i++) {
                        if (value > division[i]) {
                            return 5 - i;
                        }
                    }
                    return 0;
                },
                getValueFromDivisionBelow: function(division, value) {
                    for (var i = 0; i < division.length; i++) {
                        if (value < division[i]) {
                            return 5 - i;
                        }
                    }
                    return 0;
                },
                generateCoveragePointsWithFunc: function(pointDef, points, func) {
                    var intervals = pointDef.intervals;
                    var i;
                    for (i = 0; i < intervals.length; i++) {
                        intervals[i].coors = [];
                    }
                    angular.forEach(points,
                        function(point) {
                            var value = func(point);
                            for (i = 0; i < intervals.length; i++) {
                                if ((pointDef.sign && value < intervals[i].threshold) ||
                                    (!pointDef.sign && value > intervals[i].threshold)) {
                                    intervals[i].coors.push({
                                        longtitute: point.longtitute,
                                        lattitute: point.lattitute
                                    });
                                    break;
                                }
                            }
                        });
                },
                generateCoveragePointsWithOffset: function(pointDef, points, func, xOffset, yOffset) {
                    var intervals = pointDef.intervals;
                    var i;
                    for (i = 0; i < intervals.length; i++) {
                        intervals[i].coors = [];
                    }
                    angular.forEach(points,
                        function(point) {
                            var value = func(point);
                            for (i = 0; i < intervals.length; i++) {
                                if ((pointDef.sign && value < intervals[i].threshold) ||
                                    (!pointDef.sign && value > intervals[i].threshold)) {
                                    intervals[i].coors.push({
                                        longtitute: point.longtitute + xOffset,
                                        lattitute: point.lattitute + yOffset
                                    });
                                    break;
                                }
                            }
                        });
                },

                generateDistrictColumnDefs: function(kpiType) {
                    switch (kpiType) {
                    case 'precise':
                        return chartCalculateService.generateDistrictKpiDefs([
                            { field: 'totalMrs', name: 'MR总数' },
                            {
                                field: 'preciseRate',
                                name: '精确覆盖率',
                                cellFilter: 'number: 2',
                                cellClass: appFormatService.generateLowThresholdClassFunc
                            },
                            { field: 'objectRate', name: '本区目标值', cellFilter: 'number: 2' },
                            { field: 'firstRate', name: '第一精确覆盖率', cellFilter: 'number: 2' },
                            { field: 'thirdRate', name: '第三精确覆盖率', cellFilter: 'number: 2' }
                        ]);
                    case 'downSwitch':
                        return chartCalculateService.generateDistrictKpiDefs([
                            { field: 'redirectCdma2000', name: '4G下切3G次数' },
                            { field: 'totalFlowMByte', name: '总流量(MByte)', cellFilter: 'number: 2' },
                            {
                                field: 'downSwitchRate',
                                name: '4G下切3G比例(次/GByte)',
                                cellFilter: 'number: 2',
                                cellClass: appFormatService.generateHighThresholdClassFunc
                            },
                            { field: 'objectRate', name: '本区目标值', cellFilter: 'number: 2' }
                        ]);;
                    case 'doubleFlow':
                        return [];
                    case 'rrc':
                        return chartCalculateService.generateDistrictKpiDefs([
                            { field: 'totalRrcRequest', name: 'RRC连接请求' },
                            {
                                field: 'rrcSuccessRate',
                                name: 'RRC连接成功率',
                                cellFilter: 'number: 2',
                                cellClass: appFormatService.generateLowThresholdClassFunc
                            },
                            { field: 'objectRate', name: '本区目标值', cellFilter: 'number: 2' },
                            { field: 'moDataRrcRate', name: '主叫数据成功率', cellFilter: 'number: 2' },
                            { field: 'moSiganllingRrcRate', name: '主叫信令成功率', cellFilter: 'number: 2' },
                            { field: 'mtAccessRrcRate', name: '被叫接入成功率', cellFilter: 'number: 2' }
                        ]);
                    case 'cqi':
                        return chartCalculateService.generateDistrictKpiDefs([
                            { field: 'goodCounts', name: 'CQI优良调度次数' },
                            { field: 'totalCounts', name: '总调度次数' },
                            {
                                field: 'cqiRate',
                                name: 'CQI优良率(CQI>=7)(%)',
                                cellFilter: 'number: 2',
                                cellClass: appFormatService.generateLowThresholdClassFunc
                            },
                            { field: 'objectRate', name: '本区目标值', cellFilter: 'number: 2' }
                        ]);
                    default:
                        return [];
                    }
                },
                generateTownColumnDefs: function(kpiType) {
                    switch (kpiType) {
                    case 'precise':
                        return appFormatService.generateDistrictTownKpiDefs([
                            { field: 'totalMrs', name: 'MR总数' },
                            { field: 'preciseRate', name: '精确覆盖率', cellFilter: 'number: 2' },
                            { field: 'firstRate', name: '第一精确覆盖率', cellFilter: 'number: 2' },
                            { field: 'thirdRate', name: '第三精确覆盖率', cellFilter: 'number: 2' }
                        ]);
                    case 'downSwitch':
                        return appFormatService.generateDistrictTownKpiDefs([
                            { field: 'redirectCdma2000', name: '4G下切3G次数' },
                            { field: 'pdcpDownlinkFlow', name: '下行流量(Mbit)', cellFilter: 'number: 2' },
                            { field: 'pdcpUplinkFlow', name: '上行流量(Mbit)', cellFilter: 'number: 2' },
                            { field: 'downSwitchRate', name: '4G下切3G比例(次/GByte)', cellFilter: 'number: 2' }
                        ]);
                    case 'doubleFlow':
                        return [];
                    case 'rrc':
                        return appFormatService.generateDistrictTownKpiDefs([
                            { field: 'totalRrcRequest', name: 'RRC连接请求' },
                            { field: 'rrcSuccessRate', name: 'RRC连接成功率', cellFilter: 'number: 2' },
                            { field: 'moDataRrcRate', name: '主叫数据成功率', cellFilter: 'number: 2' },
                            { field: 'moSiganllingRrcRate', name: '主叫信令成功率', cellFilter: 'number: 2' },
                            { field: 'mtAccessRrcRate', name: '被叫接入成功率', cellFilter: 'number: 2' },
                            { field: 'totalRrcFail', name: '接入失败总次数', cellFilter: 'number: 2' },
                            { field: 'moSignallingRrcFail', name: '主叫信令接入失败次数', cellFilter: 'number: 2' }
                        ]);
                    case 'cqi':
                        return appFormatService.generateDistrictTownKpiDefs([
                            { field: 'cqiCounts.item2', name: 'CQI优良调度次数' },
                            { field: 'cqiCounts.item1', name: 'CQI质差调度次数' },
                            { field: 'cqiRate', name: 'CQI优良率(CQI>=7)(%)' }
                        ]);
                    default:
                        return [];
                    }
                },
                calculateAverageValues: function(stats, keys) {
                    var values = [];
                    if (stats.length === 0) return [];
                    for (var i = 0; i < stats.length; i++) {
                        for (var j = 0; j < stats[i].values.length; j++) {
                            var k;
                            if (i === 0) {
                                var result = {};
                                for (k = 0; k < keys.length; k++) {
                                    result[keys[k]] = stats[0].values[j][keys[k]] / stats.length;
                                }
                                values.push(result);
                            } else {
                                for (k = 0; k < keys.length; k++) {
                                    values[j][keys[k]] += stats[i].values[j][keys[k]] / stats.length;
                                }
                            }
                        }
                    }
                    return values;
                },
                initializePreciseCityStat: function(currentCity) {
                    return {
                        city: currentCity,
                        district: "全网",
                        totalMrs: 0,
                        firstNeighbors: 0,
                        secondNeighbors: 0,
                        thirdNeighbors: 0,
                        firstRate: 0,
                        preciseRate: 0,
                        objectRate: 90
                    };
                },
                initializeRrcCityStat: function(currentCity) {
                    return {
                        city: currentCity,
                        district: "全网",
                        totalRrcRequest: 0,
                        totalRrcSuccess: 0,
                        moDataRrcRequest: 0,
                        moDataRrcSuccess: 0,
                        moSignallingRrcRequest: 0,
                        moSignallingRrcSuccess: 0,
                        mtAccessRrcRequest: 0,
                        mtAccessRrcSuccess: 0,
                        rrcSuccessRate: 0,
                        moSiganllingRrcRate: 0,
                        moDataRrcRate: 0,
                        mtAccessRrcRate: 0,
                        objectRate: 99
                    };
                },
                initializeCqiCityStat: function(currentCity) {
                    return {
                        city: currentCity,
                        district: "全网",
                        goodCounts: 0,
                        totalCounts: 0,
                        cqiRate: 0,
                        objectRate: 92
                    };
                },
                initializeFlowCityStat: function(currentCity) {
                    return {
                        city: currentCity,
                        district: "全网",
                        redirectCdma2000: 0,
                        pdcpDownlinkFlow: 0,
                        pdcpUplinkFlow: 0,
                        totalFlowMByte: 0,
                        downSwitchRate: 0,
                        objectRate: 24
                    };
                }
            };
        });