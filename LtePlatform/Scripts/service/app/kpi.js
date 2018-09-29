angular.module('kpi.core', ['myApp.url', 'myApp.region'])
    .factory('kpiDisplayService',
        function(appFormatService,
            coverageService,
            topPreciseService,
            calculateService,
            chartCalculateService,
            generalChartService) {
            return {
                generateCollegeFlowBarOptions: function(stats) {
                    return chartCalculateService.generateMultiSeriesFuncBarOptions(stats,
                        'name',
                        [
                            {
                                dataFunc: function (stat) {
                                    return stat.pdcpDownlinkFlow / 1024 / 8;
                                },
                                seriesName: 'PDCP下行流量'
                            }, {
                                dataFunc: function (stat) {
                                    return stat.pdcpUplinkFlow / 1024 / 8;
                                },
                                seriesName: 'PDCP上行流量'
                            }
                        ],
                        {
                            title: "校园网流量统计",
                            xTitle: '校园名称',
                            yTitle: '流量（GB）'
                        });
                },
                generateComboChartOptions: function(data, name) {
                    var setting = {
                        title: name,
                        xtitle: '日期',
                        ytitle: name
                    };
                    var categories = data.statDates;
                    var dataList = [];
                    var seriesTitle = [];
                    var typeList = [];
                    var kpiOption = appFormatService.lowerFirstLetter(name);
                    var type = kpiOption === "2G呼建(%)" ? 'line' : 'column';
                    angular.forEach(data.regionList,
                        function(item, $index) {
                            typeList.push($index === data.regionList.length - 1 ? 'spline' : type);
                            dataList.push(data.kpiDetails[kpiOption][$index]);
                            seriesTitle.push(item);
                        });

                    return generalChartService
                        .queryMultipleComboOptions(setting, categories, dataList, seriesTitle, typeList);
                },
                getMrsOptions: function(stats, title) {
                    var chart = new ComboChart();
                    chart.title.text = title;
                    var categoryKey = 'dateString';
                    var dataKeys = [
                        'totalMrs',
                        'firstNeighbors',
                        'secondNeighbors',
                        'thirdNeighbors'
                    ];
                    var seriesInfo = {
                        totalMrs: {
                            type: 'column',
                            name: "MR总数"
                        },
                        firstNeighbors: {
                            type: "spline",
                            name: "第一邻区MR数"
                        },
                        secondNeighbors: {
                            type: "spline",
                            name: "第二邻区MR数"
                        },
                        thirdNeighbors: {
                            type: "spline",
                            name: "第三邻区MR数"
                        }
                    };
                    var seriesData = chartCalculateService.generateSeriesInfo(seriesInfo, stats, categoryKey, dataKeys);
                    chart.xAxis[0].categories = seriesData.categories;
                    chart.yAxis[0].title.text = "MR数量";
                    chart.xAxis[0].title.text = '日期';
                    chartCalculateService.writeSeriesData(chart.series, seriesData.info, dataKeys);
                    return chart.options;
                },
                getPreciseOptions: function(stats, title) {
                    var chart = new ComboChart();
                    chart.title.text = title;
                    var statDates = [];
                    var firstRate = [];
                    var secondRate = [];
                    var thirdRate = [];
                    angular.forEach(stats,
                        function(stat) {
                            statDates.push(stat.dateString);
                            firstRate.push(100 - parseFloat(stat.firstRate));
                            secondRate.push(100 - parseFloat(stat.secondRate));
                            thirdRate.push(100 - parseFloat(stat.thirdRate));
                        });
                    chart.xAxis[0].categories = statDates;
                    chart.xAxis[0].title.text = '日期';
                    chart.yAxis[0].title.text = "精确覆盖率";
                    chart.series.push({
                        type: "spline",
                        name: "第一邻区精确覆盖率",
                        data: firstRate
                    });
                    chart.series.push({
                        type: "spline",
                        name: "第二邻区精确覆盖率",
                        data: secondRate
                    });
                    chart.series.push({
                        type: "spline",
                        name: "第三邻区精确覆盖率",
                        data: thirdRate
                    });
                    return chart.options;
                },
                getInterferencePieOptions: function(interferenceCells, currentCellName) {
                    var over6DbPie = new GradientPie();
                    var over10DbPie = new GradientPie();
                    var mod3Pie = new GradientPie();
                    var mod6Pie = new GradientPie();
                    over6DbPie.series[0].name = '6dB干扰日平均次数';
                    over10DbPie.series[0].name = '10dB干扰日平均次数';
                    over6DbPie.title.text = currentCellName + ': 6dB干扰日平均次数';
                    over10DbPie.title.text = currentCellName + ': 10dB干扰日平均次数';
                    mod3Pie.series[0].name = 'MOD3干扰日平均次数';
                    mod6Pie.series[0].name = 'MOD6干扰日平均次数';
                    mod3Pie.title.text = currentCellName + ': MOD3干扰日平均次数';
                    mod6Pie.title.text = currentCellName + ': MOD6干扰日平均次数';
                    angular.forEach(interferenceCells,
                        function(cell) {
                            over6DbPie.series[0].data.push({
                                name: cell.neighborCellName,
                                y: cell.overInterferences6Db
                            });
                            over10DbPie.series[0].data.push({
                                name: cell.neighborCellName,
                                y: cell.overInterferences10Db
                            });
                            if (cell.mod3Interferences > 0) {
                                mod3Pie.series[0].data.push({
                                    name: cell.neighborCellName,
                                    y: cell.mod3Interferences
                                });
                            }
                            if (cell.mod6Interferences > 0) {
                                mod6Pie.series[0].data.push({
                                    name: cell.neighborCellName,
                                    y: cell.mod6Interferences
                                });
                            }
                        });
                    return {
                        over6DbOption: over6DbPie.options,
                        over10DbOption: over10DbPie.options,
                        mod3Option: mod3Pie.options,
                        mod6Option: mod6Pie.options
                    };
                },
                getStrengthColumnOptions: function(interferenceCells, mrCount, currentCellName) {
                    var over6DbColumn = new Column3d();
                    var over10DbColumn = new Column3d();
                    over6DbColumn.series[0].name = '6dB干扰强度';
                    over10DbColumn.series[0].name = '10dB干扰强度';
                    over6DbColumn.title.text = currentCellName + ': 6dB干扰干扰强度';
                    over10DbColumn.title.text = currentCellName + ': 10dB干扰干扰强度';

                    angular.forEach(interferenceCells,
                        function(cell) {
                            over6DbColumn.series[0].data.push(cell.overInterferences6Db / mrCount * 100);
                            over10DbColumn.series[0].data.push(cell.overInterferences10Db / mrCount * 100);
                            over6DbColumn.xAxis.categories.push(cell.neighborCellName);
                            over10DbColumn.xAxis.categories.push(cell.neighborCellName);
                        });
                    return {
                        over6DbOption: over6DbColumn.options,
                        over10DbOption: over10DbColumn.options
                    };
                },
                calculatePreciseChange: function(input) {
                    var preKpis = input.slice(0, 7);
                    var postKpis = input.slice(input.length - 7);
                    var preSum = 0;
                    var postSum = 0;
                    angular.forEach(preKpis,
                        function(kpi) {
                            preSum += kpi.secondRate;
                        });
                    angular.forEach(postKpis,
                        function(kpi) {
                            postSum += kpi.secondRate;
                        });
                    return {
                        pre: 100 - preSum / 7,
                        post: 100 - postSum / 7
                    };
                },
                queryKpiOptions: function(network) {
                    switch (network) {
                    case '2G':
                        return {
                            options: ['Ec/Io', 'RxAGC', 'TxPower'],
                            selected: 'Ec/Io'
                        };
                    case '3G':
                        return {
                            options: ['SINR(3G)', 'RxAGC0', 'RxAGC1'],
                            selected: 'SINR(3G)'
                        };
                    default:
                        return {
                            options: ['RSRP', 'SINR'],
                            selected: 'RSRP'
                        };
                    }
                },
                queryCoverageLegend: function(kpi) {
                    switch (kpi) {
                    case 'Ec/Io':
                        return {
                            criteria: coverageService.defaultEcioCriteria,
                            sign: true
                        };
                    case 'RxAGC':
                        return {
                            criteria: coverageService.defaultRxCriteria,
                            sign: true
                        };
                    case 'TxPower':
                        return {
                            criteria: coverageService.defaultTxCriteria,
                            sign: false
                        };
                    case 'SINR(3G)':
                        return {
                            criteria: coverageService.defaultSinr3GCriteria,
                            sign: true
                        };
                    case 'RxAGC0':
                        return {
                            criteria: coverageService.defaultRxCriteria,
                            sign: true
                        };
                    case 'RxAGC1':
                        return {
                            criteria: coverageService.defaultRxCriteria,
                            sign: true
                        };
                    case 'RSRP':
                        return {
                            criteria: coverageService.defaultRsrpCriteria,
                            sign: true
                        };
                    case 'rsrpInterval':
                        return {
                            criteria: coverageService.rsrpIntervalCriteria,
                            sign: true
                        };
                    case 'competeResult':
                        return {
                            criteria: coverageService.competeCriteria,
                            sign: true
                        };
                    default:
                        return {
                            criteria: coverageService.defaultSinrCriteria,
                            sign: true
                        };
                    }
                },
                initializeCoveragePoints: function(legend) {
                    var pointDef = {
                        sign: legend.sign,
                        intervals: []
                    };
                    angular.forEach(legend.criteria,
                        function(interval) {
                            pointDef.intervals.push({
                                color: interval.color,
                                threshold: interval.threshold,
                                coors: []
                            });
                        });
                    pointDef.intervals.push({
                        color: "#077f07",
                        threshold: legend.sign ? 10000 : -10000,
                        coors: []
                    });
                    return pointDef;
                },
                generateCoveragePoints: function(pointDef, points, kpi) {
                    calculateService.generateCoveragePointsWithFunc(pointDef,
                        points,
                        function(point) {
                            switch (kpi) {
                            case 'Ec/Io':
                                return point.ecio;
                            case 'RxAGC':
                                return point.rxAgc;
                            case 'TxPower':
                                return point.txPower;
                            case 'SINR(3G)':
                                return point.sinr;
                            case 'RxAGC0':
                                return point.rxAgc0;
                            case 'RxAGC1':
                                return point.rxAgc1;
                            case 'RSRP':
                                return point.rsrp;
                            default:
                                return point.sinr;
                            }
                        });
                },
                generateRealRsrpPoints: function(pointDef, points) {
                    calculateService.generateCoveragePointsWithOffset(pointDef,
                        points,
                        function(point) {
                            return point.rsrp;
                        },
                        0.000245,
                        0.000225);
                },
                generateMobileRsrpPoints: function(pointDef, points) {
                    calculateService.generateCoveragePointsWithFunc(pointDef,
                        points,
                        function(point) {
                            return point.mobileRsrp;
                        });
                },
                generateTelecomRsrpPoints: function(pointDef, points) {
                    calculateService.generateCoveragePointsWithOffset(pointDef,
                        points,
                        function(point) {
                            return point.telecomRsrp - 140;
                        },
                        0.000245,
                        0.000225);
                },
                generateUnicomRsrpPoints: function(pointDef, points) {
                    calculateService.generateCoveragePointsWithFunc(pointDef,
                        points,
                        function(point) {
                            return point.unicomRsrp;
                        });
                },
                generateAverageRsrpPoints: function(pointDef, points) {
                    calculateService.generateCoveragePointsWithOffset(pointDef,
                        points,
                        function(point) {
                            return point.averageRsrp - 140;
                        },
                        0.000245,
                        0.000225);
                },
                updateCoverageKpi: function(neighbor, cell, dateSpan) {
                    topPreciseService.queryCoverage(dateSpan.begin,
                        dateSpan.end,
                        cell.cellId,
                        cell.sectorId).then(function(coverage) {
                        if (coverage.length > 0) {
                            var coverageRate = calculateService.calculateWeakCoverageRate(coverage);
                            neighbor.weakBelow115 = coverageRate.rate115;
                            neighbor.weakBelow110 = coverageRate.rate110;
                            neighbor.weakBelow105 = coverageRate.rate105;
                        }

                    });
                    topPreciseService.queryTa(dateSpan.begin,
                        dateSpan.end,
                        cell.cellId,
                        cell.sectorId).then(function(taList) {
                        if (taList.length > 0) {
                            neighbor.overCover = calculateService.calculateOverCoverageRate(taList);
                        }
                    });
                }
            };
        })
    .factory('appKpiService',
        function(
            chartCalculateService,
            generalChartService,
            kpiRatingDivisionDefs,
            flowService,
            calculateService,
            appFormatService,
            preciseChartService) {
            return {
                getCityStat: function(districtStats, currentCity) {
                    var stat = calculateService.initializePreciseCityStat(currentCity);
                    angular.forEach(districtStats,
                        function(districtStat) {
                            calculateService.accumulatePreciseStat(stat, districtStat);
                        });
                    return calculateService.calculateDistrictRates(stat);
                },
                getRrcCityStat: function(districtStats, currentCity) {
                    var stat = calculateService.initializeRrcCityStat(currentCity);
                    angular.forEach(districtStats,
                        function(districtStat) {
                            calculateService.accumulateRrcStat(stat, districtStat);
                        });
                    return calculateService.calculateDistrictRrcRates(stat);
                },
                getFlowCityStat: function(districtStats, currentCity) {
                    var stat = calculateService.initializeFlowCityStat(currentCity);
                    angular.forEach(districtStats,
                        function(districtStat) {
                            calculateService.accumulateFlowStat(stat, districtStat);
                        });
                    return calculateService.calculateDistrictFlowRates(stat);
                },
                calculateFlowStats: function(cellList, flowStats, mergeStats, beginDate, endDate) {
                    flowStats.length = 0;
                    mergeStats.length = 0;
                    angular.forEach(cellList,
                        function(cell) {
                            flowService.queryCellFlowByDateSpan(cell.eNodebId,
                                cell.sectorId,
                                beginDate.value,
                                endDate.value).then(function(flowList) {
                                cell.flowList = flowList;
                                if (flowList.length > 0) {
                                    flowStats.push(chartCalculateService.calculateMemberSum(flowList,
                                        [
                                            'averageActiveUsers',
                                            'averageUsers',
                                            'maxActiveUsers',
                                            'maxUsers',
                                            'pdcpDownlinkFlow',
                                            'pdcpUplinkFlow'
                                        ],
                                        function(stat) {
                                            stat.cellName = cell.eNodebName + '-' + cell.sectorId;
                                        }));
                                    calculateService.mergeDataByKey(mergeStats,
                                        flowList,
                                        'statTime',
                                        [
                                            'averageActiveUsers',
                                            'averageUsers',
                                            'maxActiveUsers',
                                            'maxUsers',
                                            'pdcpDownlinkFlow',
                                            'pdcpUplinkFlow'
                                        ]);
                                }
                            });
                        });
                },
                calculateFeelingStats: function(cellList, flowStats, mergeStats, beginDate, endDate) {
                    flowStats.length = 0;
                    mergeStats.length = 0;
                    angular.forEach(cellList,
                        function(cell) {
                            flowService.queryCellFlowByDateSpan(cell.eNodebId,
                                cell.sectorId,
                                beginDate.value,
                                endDate.value).then(function(flowList) {
                                cell.feelingList = flowList;
                                if (flowList.length > 0) {
                                    flowStats.push(chartCalculateService.calculateMemberSum(flowList,
                                        [
                                            'downlinkFeelingThroughput',
                                            'downlinkFeelingDuration',
                                            'uplinkFeelingThroughput',
                                            'uplinkFeelingDuration',
                                            'pdcpDownlinkFlow',
                                            'pdcpUplinkFlow',
                                            'schedulingRank2',
                                            'schedulingTimes',
                                            'redirectCdma2000'
                                        ],
                                        function(stat) {
                                            stat.cellName = cell.eNodebName + '-' + cell.sectorId;
                                            stat
                                                .downlinkFeelingRate =
                                                cell.downlinkFeelingThroughput / cell.downlinkFeelingDuration;
                                            stat
                                                .uplinkFeelingRate =
                                                cell.uplinkFeelingThroughput / cell.uplinkFeelingDuration;
                                            stat.rank2Rate = cell.schedulingRank2 * 100 / cell.schedulingTimes;
                                        }));
                                    calculateService.mergeDataByKey(mergeStats,
                                        flowList,
                                        'statTime',
                                        [
                                            'downlinkFeelingThroughput',
                                            'downlinkFeelingDuration',
                                            'uplinkFeelingThroughput',
                                            'uplinkFeelingDuration',
                                            'pdcpDownlinkFlow',
                                            'pdcpUplinkFlow',
                                            'schedulingRank2',
                                            'schedulingTimes',
                                            'redirectCdma2000'
                                        ]);
                                }
                            });
                        });
                },
                getMrPieOptions: function(districtStats, townStats) {
                    return chartCalculateService.generateDrillDownPieOptionsWithFunc(chartCalculateService
                        .generateDrillDownData(districtStats,
                            townStats,
                            function(stat) {
                                return stat.totalMrs;
                            }),
                        {
                            title: "分镇区测量报告数分布图",
                            seriesName: "区域"
                        },
                        appFormatService.generateDistrictPieNameValueFuncs());
                },
                getPreciseRateOptions: function(districtStats, townStats) {
                    return chartCalculateService.generateDrillDownColumnOptionsWithFunc(chartCalculateService
                        .generateDrillDownData(districtStats,
                            townStats,
                            function(stat) {
                                return stat.preciseRate;
                            }),
                        {
                            title: "分镇区精确覆盖率分布图",
                            seriesName: "区域"
                        },
                        appFormatService.generateDistrictPieNameValueFuncs());
                },
                getMoSignallingRrcRateOptions: function(districtStats, townStats) {
                    return chartCalculateService.generateDrillDownColumnOptionsWithFunc(chartCalculateService
                        .generateDrillDownData(districtStats,
                            townStats,
                            function(stat) {
                                return stat.moSiganllingRrcRate;
                            }),
                        {
                            title: "分镇区主叫信令RRC连接成功率分布图",
                            seriesName: "区域",
                            yMin: 99,
                            yMax: 100
                        },
                        appFormatService.generateDistrictPieNameValueFuncs());
                },
                getMtAccessRrcRateOptions: function(districtStats, townStats) {
                    return chartCalculateService.generateDrillDownColumnOptionsWithFunc(chartCalculateService
                        .generateDrillDownData(districtStats,
                            townStats,
                            function(stat) {
                                return stat.mtAccessRrcRate;
                            }),
                        {
                            title: "分镇区被叫接入RRC连接成功率分布图",
                            seriesName: "区域",
                            yMin: 99,
                            yMax: 100
                        },
                        appFormatService.generateDistrictPieNameValueFuncs());
                },
                getMrsDistrictOptions: function(stats, inputDistricts) {
                    var districts = inputDistricts.concat("全网");
                    return preciseChartService.generateDistrictTrendOptions(stats,
                        districts,
                        function(stat) {
                            return stat.mr;
                        },
                        {
                            title: "MR总数变化趋势图",
                            xTitle: '日期',
                            yTitle: "MR总数"
                        });
                },
                getCqiCountsDistrictOptions: function(stats, inputDistricts) {
                    var districts = inputDistricts.concat("全网");
                    return preciseChartService.generateDistrictTrendOptions(stats,
                        districts,
                        function(stat) {
                            return stat.request;
                        },
                        {
                            title: "调度次数变化趋势图",
                            xTitle: '日期',
                            yTitle: "调度次数"
                        });
                },
                getPreciseDistrictOptions: function(stats, inputDistricts) {
                    var districts = inputDistricts.concat("全网");
                    return preciseChartService.generateDistrictTrendOptions(stats,
                        districts,
                        function(stat) {
                            return stat.precise;
                        },
                        {
                            title: "精确覆盖率变化趋势图",
                            xTitle: '日期',
                            yTitle: "精确覆盖率"
                        });
                },
                getCqiRateDistrictOptions: function(stats, inputDistricts) {
                    var districts = inputDistricts.concat("全网");
                    return preciseChartService.generateDistrictTrendOptions(stats,
                        districts,
                        function(stat) {
                            return stat.rate;
                        },
                        {
                            title: "CQI优良比变化趋势图",
                            xTitle: '日期',
                            yTitle: "CQI优良比"
                        });
                },
                generateDistrictStats: function(districts, stats) {
                    return chartCalculateService.generateDistrictStats(districts,
                        stats,
                        {
                            districtViewFunc: function(stat) {
                                return stat.districtViews;
                            },
                            initializeFunc: function(generalStat) {
                                generalStat.totalMrs = 0;
                                generalStat.totalSecondNeighbors = 0;
                            },
                            calculateFunc: function(view) {
                                return {
                                    mr: view.totalMrs,
                                    precise: view.preciseRate
                                };
                            },
                            accumulateFunc: function(generalStat, view) {
                                generalStat.totalMrs += view.totalMrs;
                                generalStat.totalSecondNeighbors += view.secondNeighbors;
                            },
                            zeroFunc: function() {
                                return {
                                    mr: 0,
                                    precise: 0
                                };
                            },
                            totalFunc: function(generalStat) {
                                return {
                                    mr: generalStat.totalMrs,
                                    precise: 100 - 100 * generalStat.totalSecondNeighbors / generalStat.totalMrs
                                }
                            }
                        });
                },
                generateRrcDistrictStats: function(districts, stats) {
                    return chartCalculateService.generateDistrictStats(districts,
                        stats,
                        {
                            districtViewFunc: function(stat) {
                                return stat.districtViews;
                            },
                            initializeFunc: function(generalStat) {
                                generalStat.totalRrcRequest = 0;
                                generalStat.totalRrcSuccess = 0;
                            },
                            calculateFunc: function(view) {
                                return {
                                    request: view.totalRrcRequest,
                                    rate: view.rrcSuccessRate
                                };
                            },
                            accumulateFunc: function(generalStat, view) {
                                generalStat.totalRrcRequest += view.totalRrcRequest;
                                generalStat.totalRrcSuccess += view.totalRrcSuccess;
                            },
                            zeroFunc: function() {
                                return {
                                    request: 0,
                                    rate: 0
                                };
                            },
                            totalFunc: function(generalStat) {
                                return {
                                    request: generalStat.totalRrcRequest,
                                    rate: 100 * generalStat.totalRrcSuccess / generalStat.totalRrcRequest
                                }
                            }
                        });
                },
                generateCqiDistrictStats: function(districts, stats) {
                    return chartCalculateService.generateDistrictStats(districts,
                        stats,
                        {
                            districtViewFunc: function(stat) {
                                return stat.districtViews;
                            },
                            initializeFunc: function(generalStat) {
                                generalStat.goodCounts = 0;
                                generalStat.totalCounts = 0;
                            },
                            calculateFunc: function(view) {
                                return {
                                    request: view.cqiCounts.item1 + view.cqiCounts.item2,
                                    rate: view.cqiRate
                                };
                            },
                            accumulateFunc: function(generalStat, view) {
                                generalStat.goodCounts += view.cqiCounts.item2;
                                generalStat.totalCounts += view.cqiCounts.item1 + view.cqiCounts.item2;
                            },
                            zeroFunc: function() {
                                return {
                                    request: 0,
                                    rate: 0
                                };
                            },
                            totalFunc: function(generalStat) {
                                return {
                                    request: generalStat.totalCounts,
                                    rate: 100 * generalStat.goodCounts / generalStat.totalCounts
                                }
                            }
                        });
                },
                calculateAverageRates: function(stats) {
                    var result = {
                        statDate: "平均值",
                        values: calculateService.calculateAverageValues(stats, ['mr', 'precise'])
                    };
                    return result;
                },
                calculateAverageRrcRates: function(stats) {
                    var result = {
                        statDate: "平均值",
                        values: calculateService.calculateAverageValues(stats, ['request', 'rate'])
                    };
                    return result;
                },
                calculateAverageDownSwitchRates: function(stats) {
                    var result = {
                        statDate: "平均值",
                        values: calculateService.calculateAverageValues(stats, ['downSwitchTimes', 'downSwitchRate'])
                    };
                    return result;
                },
                generateTrendStatsForPie: function(trendStat, result) {
                    chartCalculateService.generateStatsForPie(trendStat,
                        result,
                        {
                            districtViewsFunc: function(stat) {
                                return stat.districtViews;
                            },
                            townViewsFunc: function(stat) {
                                return stat.townViews;
                            },
                            accumulateFunc: function(source, accumulate) {
                                calculateService.accumulatePreciseStat(source, accumulate);
                            },
                            districtCalculate: function(stat) {
                                calculateService.calculateDistrictRates(stat);
                            },
                            townCalculate: function(stat) {
                                calculateService.calculateTownRates(stat);
                            }
                        });
                },
                generateRrcTrendStatsForPie: function(trendStat, result) {
                    chartCalculateService.generateStatsForPie(trendStat,
                        result,
                        {
                            districtViewsFunc: function(stat) {
                                return stat.districtViews;
                            },
                            townViewsFunc: function(stat) {
                                return stat.townViews;
                            },
                            accumulateFunc: function(source, accumulate) {
                                calculateService.accumulateRrcStat(source, accumulate);
                            },
                            districtCalculate: function(stat) {
                                calculateService.calculateDistrictRrcRates(stat);
                            },
                            townCalculate: function(stat) {
                                calculateService.calculateTownRrcRates(stat);
                            }
                        });
                },
                generateCqiTrendStatsForPie: function(trendStat, result) {
                    chartCalculateService.generateStatsForPie(trendStat,
                        result,
                        {
                            districtViewsFunc: function(stat) {
                                return stat.districtViews;
                            },
                            townViewsFunc: function(stat) {
                                return stat.townViews;
                            },
                            accumulateFunc: function(source, accumulate) {
                                calculateService.accumulateCqiStat(source, accumulate);
                            },
                            districtCalculate: function(stat) {
                                calculateService.calculateDistrictRrcRates(stat);
                            },
                            townCalculate: function(stat) {
                                calculateService.calculateTownRrcRates(stat);
                            }
                        });
                },
                getPreciseObject: function(district) {
                    var objectTable = {
                        "禅城": 89.8,
                        "南海": 90,
                        "三水": 90,
                        "高明": 90,
                        "顺德": 90.2
                    };
                    return objectTable[district] === undefined ? 90 : objectTable[district];
                },
                getRrcObject: function(district) {
                    var objectTable = {
                        "禅城": 99,
                        "南海": 99,
                        "三水": 99,
                        "高明": 99,
                        "顺德": 99
                    };
                    return objectTable[district] === undefined ? 99 : objectTable[district];
                },
                generateComplainTrendOptions: function(dates, counts, objects) {
                    var chart = new TimeSeriesLine();
                    chart.title.text = '月度抱怨量变化趋势图';
                    chart.setDefaultXAxis({
                        title: '日期',
                        categories: dates
                    });
                    chart.setDefaultYAxis({
                        title: '抱怨量'
                    });
                    chart.insertSeries({
                        name: '指标值',
                        data: counts
                    });
                    chart.insertSeries({
                        name: '目标值',
                        data: objects
                    });
                    return chart.options;
                },
                generateColumnOptions: function(stat, title, xtitle, ytitle) {
                    return generalChartService.getColumnOptions(stat,
                        {
                            title: title,
                            xtitle: xtitle,
                            ytitle: ytitle
                        },
                        function(data) {
                            return data.item1;
                        },
                        function(data) {
                            return data.item2;
                        });
                }
            }
        });
angular.module('kpi.college.infrastructure', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .run(function($rootScope) {
        $rootScope.page = {
            messages: []
        };
        $rootScope.closeAlert = function(messages, $index) {
            messages.splice($index, 1);
        };
        $rootScope.addSuccessMessage = function(message) {
            $rootScope.page.messages.push({
                type: 'success',
                contents: message
            });
        };
    })
    .controller('bts.dialog',
        function($scope,
            $uibModalInstance,
            collegeService,
            collegeDialogService,
            collegeQueryService,
            geometryService,
            baiduQueryService,
            name,
            dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeQueryService.queryByName(name).then(function(college) {
                collegeService.queryRegion(college.id).then(function(region) {
                    var center = geometryService.queryRegionCenter(region);
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            X: 2 * center.X - coors.x,
                            Y: 2 * center.Y - coors.y
                        };
                    });
                });
            });

            $scope.query = function() {
                collegeService.queryBtss(name).then(function(result) {
                    $scope.btsList = result;
                });
            };

            $scope.addBts = function() {
                collegeDialogService.addBts(name,
                    $scope.center,
                    function(count) {
                        $scope.addSuccessMessage('增加Bts' + count + '个');
                        $scope.query();
                    });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.btsList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdmaCell.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaCells(name).then(function(result) {
                $scope.cdmaCellList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cdmaCellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('lte.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryLteDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cdma.distribution.dialog',
        function($scope, $uibModalInstance, collegeService, name, dialogTitle) {
            $scope.dialogTitle = dialogTitle;
            collegeService.queryCdmaDistributions(name).then(function(result) {
                $scope.distributionList = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
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
angular.module('kpi.college.maintain', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('cell.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            eNodebs,
            cells,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            angular.forEach(eNodebs,
                function(eNodeb) {
                    networkElementService.queryCellInfosInOneENodeb(eNodeb.eNodebId).then(function(cellInfos) {
                        neighborImportService.updateCellRruInfo($scope.supplementCells,
                        {
                            dstCells: cellInfos,
                            cells: cells,
                            longtitute: eNodeb.longtitute,
                            lattitute: eNodeb.lattitute
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cell.position.supplement.dialog',
        function($scope,
            $uibModalInstance,
            collegeMapService,
            baiduQueryService,
            collegeService,
            collegeQueryService,
            networkElementService,
            neighborImportService,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE小区补充";
            $scope.supplementCells = [];
            $scope.gridApi = {};

            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    collegeService.queryCells(collegeName).then(function(cells) {
                        baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                            collegeQueryService.queryByName(collegeName).then(function(info) {
                                networkElementService
                                    .queryRangeCells(neighborImportService.generateRange(info.rectangleRange, center, coors))
                                    .then(function(results) {
                                        neighborImportService.updateENodebRruInfo($scope.supplementCells,
                                        {
                                            dstCells: results,
                                            cells: cells,
                                            longtitute: center.X,
                                            lattitute: center.Y
                                        });
                                    });
                            });
                        });
                    });
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('eNodeb.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            collegeQueryService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "LTE基站补充";
            $scope.supplementENodebs = [];
            $scope.gridApi = {};

            $scope.query = function() {
                baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                    collegeQueryService.queryByName(collegeName).then(function(info) {
                        var ids = [];
                        collegeService.queryENodebs(collegeName).then(function(eNodebs) {
                            angular.forEach(eNodebs,
                                function(eNodeb) {
                                    ids.push(eNodeb.eNodebId);
                                });
                            networkElementService
                                .queryRangeENodebs(neighborImportService
                                    .generateRangeWithExcludedIds(info.rectangleRange, center, coors, ids)).then(function(results) {
                                    angular.forEach(results,
                                        function(item) {
                                            item.distance = geometryService
                                                .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                        });
                                    $scope.supplementENodebs = results;
                                });
                        });
                    });
                });
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('bts.supplement.dialog',
        function($scope,
            $uibModalInstance,
            networkElementService,
            neighborImportService,
            geometryService,
            baiduQueryService,
            collegeService,
            center,
            collegeName) {
            $scope.dialogTitle = collegeName + "CDMA基站补充";
            $scope.supplementBts = [];
            $scope.gridApi = {};

            baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                collegeService.queryRange(collegeName).then(function(range) {
                    var ids = [];
                    collegeService.queryBtss(collegeName).then(function(btss) {
                        angular.forEach(btss,
                            function(bts) {
                                ids.push(bts.btsId);
                            });
                        networkElementService
                            .queryRangeBtss(neighborImportService
                                .generateRangeWithExcludedIds(range, center, coors, ids)).then(function(results) {
                                angular.forEach(results,
                                    function(item) {
                                        item.distance = geometryService
                                            .getDistance(item.lattitute, item.longtitute, coors.y, coors.x);
                                    });
                                $scope.supplementBts = results;
                            });
                    });
                });
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.gridApi.selection.getSelectedRows());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.new.dialog',
        function($scope,
            $uibModalInstance,
            baiduMapService,
            geometryService,
            baiduQueryService,
            appRegionService,
            $timeout) {
            $scope.city = {
                selected: "佛山",
                options: ["佛山"]
            };
            $scope.dialogTitle = "新建校园信息";
            $scope.collegeRegion = {
                area: 0,
                regionType: 0,
                info: ""
            };
            $scope.saveCircleParameters = function(circle) {
                var center = circle.getCenter();
                var radius = circle.getRadius();
                $scope.collegeRegion.regionType = 0;
                $scope.collegeRegion.area = BMapLib.GeoUtils.getCircleArea(circle);
                $scope.collegeRegion.info = center.lng + ';' + center.lat + ';' + radius;
            };
            $scope.saveRetangleParameters = function(rect) {
                $scope.collegeRegion.regionType = 1;
                var pts = rect.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $scope.savePolygonParameters = function(polygon) {
                $scope.collegeRegion.regionType = 2;
                var pts = polygon.getPath();
                $scope.collegeRegion.info = geometryService.getPathInfo(pts);
                $scope.collegeRegion.area = BMapLib.GeoUtils.getPolygonArea(pts);
            };
            $timeout(function() {
                    baiduMapService.initializeMap('map', 12);
                    baiduMapService.initializeDrawingManager();
                    baiduMapService.addDrawingEventListener('circlecomplete', $scope.saveCircleParameters);
                    baiduMapService.addDrawingEventListener('rectanglecomplete', $scope.saveRetangleParameters);
                    baiduMapService.addDrawingEventListener('polygoncomplete', $scope.savePolygonParameters);
                },
                500);
            $scope.matchPlace = function() {
                baiduQueryService.queryBaiduPlace($scope.collegeName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                $scope.district = {
                    options: districts,
                    selected: districts[0]
                };
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town = {
                        options: towns,
                        selected: towns[0]
                    };
                });
            });
            $scope.ok = function() {
                $scope.college = {
                    name: $scope.collegeName,
                    townId: 0,
                    collegeRegion: $scope.collegeRegion
                };
                appRegionService.queryTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                    .then(function(town) {
                        if (town) {
                            $scope.college.townId = town.id;
                            $uibModalInstance.close($scope.college);
                        }
                    });
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });

angular.module('kpi.college.work', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller('college.test3G.dialog',
        function($scope,
            $uibModalInstance,
            collegeName,
            collegeDtService,
            coverageService,
            collegeMapService,
            baiduQueryService) {
            $scope.dialogTitle = collegeName + "-3G测试结果上报";
            $scope.item = collegeDtService.default3GTestView(collegeName, '饭堂', '许良镇');

            var queryRasterInfo = function(files, index, data, callback) {
                coverageService.queryDetailsByRasterInfo(files[index], '3G').then(function(result) {
                    data.push.apply(data, result);
                    if (index < files.length - 1) {
                        queryRasterInfo(files, index + 1, data, callback);
                    } else {
                        callback(data);
                    }
                });
            };
            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                    });
                });

            $scope.matchCoverage = function() {
                collegeDtService.queryRaster($scope.center,
                    '3G',
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    function(files) {
                        if (files.length) {
                            var data = [];
                            queryRasterInfo(files,
                                0,
                                data,
                                function(result) {
                                    console.log(result);
                                });
                        }
                    });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.item);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.test4G.dialog',
        function($scope,
            $uibModalInstance,
            collegeName,
            collegeDtService,
            collegeService,
            networkElementService,
            collegeMapService,
            baiduQueryService,
            coverageService,
            appFormatService) {
            $scope.dialogTitle = collegeName + "-4G测试结果上报";
            $scope.item = collegeDtService.default4GTestView(collegeName, '饭堂', '许良镇');
            collegeService.queryCells(collegeName).then(function(cellList) {
                var names = [];
                angular.forEach(cellList,
                    function(cell) {
                        names.push(cell.cellName);
                    });
                $scope.cellName = {
                    options: names,
                    selected: names[0]
                };
            });
            collegeMapService.queryCenterAndCallback(collegeName,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                    });
                });
            $scope.$watch('cellName.selected',
                function(cellName) {
                    if (cellName) {
                        networkElementService.queryLteRruFromCellName(cellName).then(function(rru) {
                            $scope.rru = rru;
                        });
                    }
                });

            var queryRasterInfo = function(files, index, data, callback) {
                coverageService.queryDetailsByRasterInfo(files[index], '4G').then(function(result) {
                    data.push.apply(data, result);
                    if (index < files.length - 1) {
                        queryRasterInfo(files, index + 1, data, callback);
                    } else {
                        callback(data);
                    }
                });
            };

            $scope.matchCoverage = function() {
                collegeDtService.queryRaster($scope.center,
                    '4G',
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    function(files) {
                        if (files.length) {
                            var data = [];
                            queryRasterInfo(files,
                                0,
                                data,
                                function(result) {
                                    var testEvaluations = appFormatService.calculateAverages(result,
                                    [
                                        function(point) {
                                            return point.rsrp;
                                        }, function(point) {
                                            return point.sinr;
                                        }, function(point) {
                                            return point.pdcpThroughputDl > 10 ? point.pdcpThroughputDl : 0;
                                        }, function(point) {
                                            return point.pdcpThroughputUl > 1 ? point.pdcpThroughputUl : 0;
                                        }
                                    ]);
                                    $scope.item.rsrp = testEvaluations[0].sum / testEvaluations[0].count;
                                    $scope.item.sinr = testEvaluations[1].sum / testEvaluations[1].count;
                                    $scope.item.downloadRate = testEvaluations[2].sum / testEvaluations[2].count * 1024;
                                    $scope.item.uploadRate = testEvaluations[3].sum / testEvaluations[3].count * 1024;
                                });
                        }
                    });
            };

            $scope.ok = function() {
                $scope.item.cellName = $scope.cellName.selected;
                $uibModalInstance.close($scope.item);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('test.process.dialog',
        function($scope, $uibModalInstance, collegeName, collegeQueryService, appFormatService) {
            $scope.dialogTitle = collegeName + "校园网测试信息确认";

            $scope.query = function() {
                collegeQueryService.queryCollege3GTestList($scope.beginDate.value, $scope.endDate.value, collegeName)
                    .then(function(test3G) {
                        $scope.items3G = test3G;
                    });
                collegeQueryService.queryCollege4GTestList($scope.beginDate.value, $scope.endDate.value, collegeName)
                    .then(function(test4G) {
                        $scope.items4G = test4G;
                        var testEvaluations = appFormatService.calculateAverages(test4G,
                        [
                            function(point) {
                                return point.rsrp;
                            }, function(point) {
                                return point.sinr;
                            }, function(point) {
                                return point.downloadRate;
                            }, function(point) {
                                return point.uploadRate;
                            }
                        ]);
                        $scope.averageRsrp = testEvaluations[0].sum / testEvaluations[0].count;
                        $scope.averageSinr = testEvaluations[1].sum / testEvaluations[1].count;
                        $scope.averageDownloadRate = testEvaluations[2].sum / testEvaluations[2].count;
                        $scope.averageUploadRate = testEvaluations[3].sum / testEvaluations[3].count;
                    });
            };
            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($("#reports").html());
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.college.flow', ['myApp.url', 'myApp.region', "ui.bootstrap", 'topic.basic'])
    .controller("hotSpot.flow",
        function($scope,
            $uibModalInstance,
            dialogTitle,
            hotSpots,
            theme,
            collegeQueryService,
            parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.hotSpotList = hotSpots;
            $scope.statCount = 0;
            $scope.query = function() {
                angular.forEach($scope.hotSpotList,
                    function(spot) {
                        collegeQueryService
                            .queryHotSpotFlow(spot.hotspotName, $scope.beginDate.value, $scope.endDate.value)
                            .then(function(stat) {
                                angular.extend(spot, stat);
                                $scope.statCount += 1;
                            });
                    });
            };
            $scope.$watch('statCount',
                function(count) {
                    if (count === $scope.hotSpotList.length && count > 0) {
                        $("#downloadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForDownlinkFlow($scope.hotSpotList, theme));
                        $("#uploadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForUplinkFlow($scope.hotSpotList, theme));
                        $("#averageUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForAverageUsers($scope.hotSpotList, theme));
                        $("#activeUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForActiveUsers($scope.hotSpotList, theme));
                        $scope.statCount = 0;
                    }
                });
            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.cell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("college.flow",
        function($scope, $uibModalInstance, dialogTitle, year, collegeQueryService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.collegeStatCount = 0;
            $scope.query = function() {
                angular.forEach($scope.collegeList,
                    function(college) {
                        collegeQueryService.queryCollegeFlow(college.name, $scope.beginDate.value, $scope.endDate.value)
                            .then(function(stat) {
                                angular.extend(college, stat);
                                $scope.collegeStatCount += 1;
                            });
                    });
            };
            $scope.$watch('collegeStatCount',
                function(count) {
                    if ($scope.collegeList && count === $scope.collegeList.length && count > 0) {
                        $("#downloadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForDownlinkFlow($scope.collegeList));
                        $("#uploadFlowConfig").highcharts(parametersChartService
                            .getCollegeDistributionForUplinkFlow($scope.collegeList));
                        $("#averageUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForAverageUsers($scope.collegeList));
                        $("#activeUsersConfig").highcharts(parametersChartService
                            .getCollegeDistributionForActiveUsers($scope.collegeList));
                        $scope.collegeStatCount = 0;
                    }
                });
            collegeQueryService.queryYearList(year).then(function(colleges) {
                $scope.collegeList = colleges;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.cell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });
angular.module('kpi.college', ['app.menu', 'region.college'])
	.factory('collegeDialogService', function(collegeQueryService, menuItemService) {
		var resolveScope = function(name, topic) {
			return {
				dialogTitle: function() {
					return name + "-" + topic;
				},
				name: function() {
					return name;
				}
			};
		};
		return {
			showENodebs: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/ENodebDialog.html',
					controller: 'eNodeb.dialog',
					resolve: resolveScope(name, "LTE基站信息")
				});
			},
			showBtss: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/BtsDialog.html',
					controller: 'bts.dialog',
					resolve: resolveScope(name, "CDMA基站信息")
				});
			},
			showCdmaCells: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/CdmaCellDialog.html',
					controller: 'cdmaCell.dialog',
					resolve: resolveScope(name, "CDMA小区信息")
				});
			},
			showLteDistributions: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/DistributionDialog.html',
					controller: 'lte.distribution.dialog',
					resolve: resolveScope(name, "LTE室分信息")
				});
			},
			showCdmaDistributions: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/DistributionDialog.html',
					controller: 'cdma.distribution.dialog',
					resolve: resolveScope(name, "CDMA室分信息")
				});
			},
			showCollegeDetails: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Infrastructure/CollegeQuery.html',
					controller: 'college.query.name',
					resolve: resolveScope(name, "详细信息")
				});
			},

			addYearInfo: function(item, name, year, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/YearInfoDialog.html',
					controller: 'year.info.dialog',
					resolve: {
						name: function() {
							return name;
						},
						year: function() {
							return year;
						},
						item: function() {
							return item;
						}
					}
				}, function(info) {
					collegeQueryService.saveYearInfo(info).then(function() {
						callback();
					});
				});
			},
			addNewCollege: function(callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/NewCollegeDialog.html',
					controller: 'college.new.dialog',
					resolve: {}
				}, function(info) {
					collegeQueryService.constructCollegeInfo(info).then(function() {
						callback();
					});
				});
			},
			supplementENodebCells: function(eNodebs, cells, collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/CellSupplementDialog.html',
					controller: 'cell.supplement.dialog',
					resolve: {
						eNodebs: function() {
							return eNodebs;
						},
						cells: function() {
							return cells;
						},
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					var cellNames = [];
					angular.forEach(info, function(cell) {
						cellNames.push(cell.cellName);
					});
					collegeQueryService.saveCollegeCells({
						collegeName: collegeName,
						cellNames: cellNames
					}).then(function() {
						callback(collegeName);
					});

				});
			},
			supplementPositionCells: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/CellSupplementDialog.html',
					controller: 'cell.position.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					var cellNames = [];
					angular.forEach(info, function(cell) {
						cellNames.push(cell.cellName);
					});
					collegeQueryService.saveCollegeCells({
						collegeName: collegeName,
						cellNames: cellNames
					}).then(function() {
						callback(collegeName);
					});

				});
			},
			construct3GTest: function(collegeName) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Construct3GTest.html',
					controller: 'college.test3G.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					collegeQueryService.saveCollege3GTest(info).then(function() {
						console.log(info);
					});
				});
			},
			construct4GTest: function(collegeName) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Construct4GTest.html',
					controller: 'college.test4G.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					collegeQueryService.saveCollege4GTest(info).then(function() {
						console.log(info);
					});
				});
			},
			processTest: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Process.html',
					controller: 'test.process.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					callback(info);
				});
			},
			tracePlanning: function(collegeName, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Test/Planning.html',
					controller: 'trace.planning.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						}
					}
				}, function(info) {
					callback(info);
				});
			},
			showCollegDialog: function(college, year) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Table/CollegeMapInfoBox.html',
					controller: 'map.college.dialog',
					resolve: {
						dialogTitle: function() {
							return college.name + "-" + "基本信息";
						},
						college: function() {
							return college;
						},
						year: function() {
							return year;
						}
					}
				});
			},
			addENodeb: function(collegeName, center, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/ENodebSupplementDialog.html',
					controller: 'eNodeb.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						},
						center: function() {
							return center;
						}
					}
				}, function(info) {
					var ids = [];
					angular.forEach(info, function(eNodeb) {
						ids.push(eNodeb.eNodebId);
					});
					collegeQueryService.saveCollegeENodebs({
						collegeName: collegeName,
						eNodebIds: ids
					}).then(function(count) {
						callback(count);
					});
				});
			},
			addBts: function(collegeName, center, callback) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/College/Infrastructure/BtsSupplementDialog.html',
					controller: 'bts.supplement.dialog',
					resolve: {
						collegeName: function() {
							return collegeName;
						},
						center: function() {
							return center;
						}
					}
				}, function(info) {
					var ids = [];
					angular.forEach(info, function(bts) {
						ids.push(bts.btsId);
					});
					collegeQueryService.saveCollegeBtss({
						collegeName: collegeName,
						btsIds: ids
					}).then(function(count) {
						callback(count);
					});
				});
			},
			showCollegeFlow: function(year) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/College/Test/Flow.html',
					controller: 'college.flow',
					resolve: {
						dialogTitle: function() {
							return "校园流量分析（" + year + "年）";
						},
						year: function() {
							return year;
						}
					}
				});
            },
            showHotSpotFlow: function (hotSpots, theme) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/College/Test/Flow.html',
                    controller: 'hotSpot.flow',
                    resolve: {
                        dialogTitle: function () {
                            return theme + "热点流量分析";
                        },
                        hotSpots: function () {
                            return hotSpots;
                        },
                        theme: function() {
                            return theme;
                        }
                    }
                });
            }
		};
	});
angular.module('kpi.coverage.interference', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('coverage.details.dialog',
        function($scope,
            $uibModalInstance,
            cellName,
            cellId,
            sectorId,
            topPreciseService,
            preciseChartService) {
            $scope.dialogTitle = cellName + '：覆盖详细信息';
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cellId,
                    sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.coverageInfos);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller('interference.source.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            preciseInterferenceService,
            neighborMongoService) {
            $scope.dialogTitle = dialogTitle;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            var options = [
                {
                    name: "模3干扰数",
                    value: "mod3Interferences"
                }, {
                    name: "模6干扰数",
                    value: "mod6Interferences"
                }, {
                    name: "6dB干扰数",
                    value: "overInterferences6Db"
                }, {
                    name: "10dB干扰数",
                    value: "overInterferences10Db"
                }, {
                    name: "总干扰水平",
                    value: "interferenceLevel"
                }
            ];
            $scope.orderPolicy = {
                options: options,
                selected: options[4].value
            };
            $scope.displayItems = {
                options: [5, 10, 15, 20, 30],
                selected: 10
            };

            $scope.showInterference = function() {
                $scope.interferenceCells = [];

                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    angular.forEach(result,
                        function(cell) {
                            for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                                var neighbor = $scope.mongoNeighbors[i];
                                if (neighbor.neighborPci === cell.destPci) {
                                    cell.isMongoNeighbor = true;
                                    break;
                                }
                            }
                        });
                    $scope.interferenceCells = result;
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(eNodebId, sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
                $scope.showInterference();
            });
        })
    .controller('interference.source.db.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-over6db").highcharts(pieOptions.over6DbOption);
                    $("#interference-over10db").highcharts(pieOptions.over10DbOption);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.source.mod.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-mod3").highcharts(pieOptions.mod3Option);
                    $("#interference-mod6").highcharts(pieOptions.mod6Option);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.source.strength.chart',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            name,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService,
            neighborMongoService,
            networkElementService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentCellName = name + "-" + sectorId;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            $scope.showChart = function() {
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(result) {
                    networkElementService.queryCellInfo(eNodebId, sectorId).then(function(info) {
                        topPreciseService.queryCellStastic(eNodebId,
                            info.pci,
                            $scope.beginDate.value,
                            $scope.endDate.value).then(function(stastic) {
                            var columnOptions = kpiDisplayService.getStrengthColumnOptions(result,
                                stastic.mrCount,
                                $scope.currentCellName);
                            $("#strength-over6db").highcharts(columnOptions.over6DbOption);
                            $("#strength-over10db").highcharts(columnOptions.over10DbOption);
                        });
                    });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close('已处理');
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showChart();
        })
    .controller('interference.victim.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            eNodebId,
            sectorId,
            topPreciseService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            var lastWeek = new Date();
            lastWeek.setDate(lastWeek.getDate() - 7);
            $scope.beginDate = {
                value: lastWeek,
                opened: false
            };
            $scope.endDate = {
                value: new Date(),
                opened: false
            };
            var options = [
                {
                    name: "模3干扰数",
                    value: "mod3Interferences"
                }, {
                    name: "模6干扰数",
                    value: "mod6Interferences"
                }, {
                    name: "6dB干扰数",
                    value: "overInterferences6Db"
                }, {
                    name: "10dB干扰数",
                    value: "overInterferences10Db"
                }, {
                    name: "总干扰水平",
                    value: "interferenceLevel"
                }
            ];
            $scope.orderPolicy = {
                options: options,
                selected: options[4].value
            };
            $scope.displayItems = {
                options: [5, 10, 15, 20, 30],
                selected: 10
            };

            $scope.showVictim = function() {
                $scope.victimCells = [];

                preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                    $scope.endDate.value,
                    eNodebId,
                    sectorId).then(function(victims) {
                    preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                        $scope.endDate.value,
                        eNodebId,
                        sectorId).then(function(result) {
                        angular.forEach(victims,
                            function(victim) {
                                for (var j = 0; j < result.length; j++) {
                                    if (result[j].destENodebId === victim.victimENodebId &&
                                        result[j].destSectorId === victim.victimSectorId) {
                                        victim.forwardInterferences6Db = result[j].overInterferences6Db;
                                        victim.forwardInterferences10Db = result[j].overInterferences10Db;
                                        break;
                                    }
                                }
                            });
                        $scope.victimCells = victims;
                    });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.victimCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showVictim();
        })
    .controller('interference.coverage.dialog',
        function($scope) {

        });
angular.module('kpi.coverage.mr', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("grid.stats",
        function($scope, dialogTitle, category, stats, $uibModalInstance, $timeout, generalChartService) {
            $scope.dialogTitle = dialogTitle;
            var options = generalChartService.getPieOptions(stats,
                {
                    title: dialogTitle,
                    seriesTitle: category
                },
                function(stat) {
                    return stat.key;
                },
                function(stat) {
                    return stat.value;
                });
            $timeout(function() {
                    $("#rightChart").highcharts(options);
                },
                500);

            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("grid.cluster",
        function($scope,
            dialogTitle,
            clusterList,
            currentCluster,
            $uibModalInstance,
            alarmImportService) {
            $scope.dialogTitle = dialogTitle;
            $scope.clusterList = clusterList;
            $scope.currentCluster = currentCluster;

            $scope.calculateKpis = function() {
                angular.forEach($scope.clusterList,
                    function(stat) {
                        alarmImportService.updateClusterKpi(stat);
                    });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.currentCluster);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("agps.stats",
        function($scope,
            dialogTitle,
            stats,
            legend,
            $uibModalInstance,
            $timeout,
            generalChartService) {
            $scope.dialogTitle = dialogTitle;
            var intervalStats = [];
            var low = -10000;
            angular.forEach(legend,
                function(interval) {
                    var high = interval.threshold;
                    intervalStats.push({
                        interval: '[' + low + ', ' + high + ')dBm',
                        count: _.countBy(stats,
                            function(stat) { return stat.telecomRsrp - 140 >= low && stat.telecomRsrp - 140 < high })[
                            'true']
                    });
                    low = high;
                });
            intervalStats.push({
                interval: 'RSRP >= ' + low + 'dBm',
                count: _.countBy(stats,
                    function(stat) { return stat.telecomRsrp - 140 >= low && stat.telecomRsrp - 140 < 10000 })['true']
            });
            var counts = stats.length;
            var operators = ['-110dBm以上', '-105dBm以上', '-100dBm以上'];
            var coverages = [
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 30 })['true'] / counts * 100,
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 35 })['true'] / counts * 100,
                _.countBy(stats, function(stat) { return stat.telecomRsrp >= 40 })['true'] / counts * 100
            ];
            var rate100Intervals = [];
            var rate105Intervals = [];
            var thresholds = [
                {
                    low: 0,
                    high: 0.5
                }, {
                    low: 0.5,
                    high: 0.75
                }, {
                    low: 0.75,
                    high: 0.9
                }, {
                    low: 0.9,
                    high: 1.01
                }
            ];
            angular.forEach(thresholds,
                function(threshold) {
                    rate100Intervals.push({
                        interval: '[' + threshold.low + ', ' + threshold.high + ')',
                        count: _.countBy(stats,
                            function(stat) {
                                return stat.telecomRate100 >= threshold.low && stat.telecomRate100 < threshold.high
                            })['true']
                    });
                    rate105Intervals.push({
                        interval: '[' + threshold.low + ', ' + threshold.high + ')',
                        count: _.countBy(stats,
                            function(stat) {
                                return stat.telecomRate105 >= threshold.low && stat.telecomRate105 < threshold.high
                            })['true']
                    });
                });
            $timeout(function() {
                    $("#leftChart").highcharts(generalChartService.getPieOptions(intervalStats,
                        {
                            title: 'RSRP区间分布',
                            seriesTitle: 'RSRP区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                    $("#rightChart").highcharts(generalChartService.queryColumnOptions({
                            title: 'RSRP覆盖优良率（%）',
                            ytitle: '覆盖优良率（%）',
                            xtitle: '覆盖标准',
                            min: 80,
                            max: 100
                        },
                        operators,
                        coverages));
                    $("#thirdChart").highcharts(generalChartService.getPieOptions(rate100Intervals,
                        {
                            title: '覆盖率区间分布（RSRP>-100dBm）',
                            seriesTitle: '覆盖率区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                    $("#fourthChart").highcharts(generalChartService.getPieOptions(rate105Intervals,
                        {
                            title: '覆盖率区间分布（RSRP>-105dBm）',
                            seriesTitle: '覆盖率区间'
                        },
                        function(stat) {
                            return stat.interval;
                        },
                        function(stat) {
                            return stat.count;
                        }));
                },
                500);

            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.coverage.stats', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("town.stats",
        function($scope, cityName, dialogTitle, $uibModalInstance, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.queryDistrictInfrastructures(cityName).then(function(result) {
                appRegionService.accumulateCityStat(result, cityName);
                $("#leftChart").highcharts(
                    parametersChartService
                    .getDistrictLteENodebPieOptions(result.slice(0, result.length - 1), cityName));
                $("#rightChart").highcharts(
                    parametersChartService.getDistrictLteCellPieOptions(result.slice(0, result.length - 1), cityName));
                $("#thirdChart").highcharts(
                    parametersChartService.getDistrictLte800PieOptions(result.slice(0, result.length - 1), cityName));
                $("#fourthChart").highcharts(
                    parametersChartService
                    .getDistrictNbIotCellPieOptions(result.slice(0, result.length - 1), cityName));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("cdma.town.stats",
        function($scope, cityName, dialogTitle, $uibModalInstance, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.queryDistrictInfrastructures(cityName).then(function(result) {
                appRegionService.accumulateCityStat(result, cityName);
                $("#leftChart").highcharts(
                    parametersChartService.getDistrictCdmaBtsPieOptions(result.slice(0, result.length - 1), cityName));
                $("#rightChart").highcharts(
                    parametersChartService.getDistrictCdmaCellPieOptions(result.slice(0, result.length - 1), cityName));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("user.roles.dialog",
        function($scope, $uibModalInstance, dialogTitle, userName, authorizeService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query = function() {
                authorizeService.queryRolesInUser(userName).then(function(roles) {
                    $scope.existedRoles = roles;
                });
                authorizeService.queryCandidateRolesInUser(userName).then(function(roles) {
                    $scope.candidateRoles = roles;
                });
            };

            $scope.addRole = function(role) {
                authorizeService.assignRoleInUser(userName, role).then(function(result) {
                    if (result) {
                        $scope.query();
                    }
                });
            };

            $scope.removeRole = function(role) {
                authorizeService.releaseRoleInUser(userName, role).then(function(result) {
                    if (result) {
                        $scope.query();
                    }
                });
            };

            $scope.query();
        });

angular.module('kpi.coverage.flow', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("flow.stats",
        function($scope,
            today,
            dialogTitle,
            frequency,
            $uibModalInstance,
            appRegionService,
            preciseChartService) {
            $scope.dialogTitle = dialogTitle;
            appRegionService.getTownFlowStats(today, frequency).then(function(result) {
                $("#leftChart").highcharts(preciseChartService.getTownFlowOption(result, frequency));
                $("#rightChart").highcharts(preciseChartService.getTownUsersOption(result, frequency));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("flow.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyFlowTrendCharts(districts, frequency, result);
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("users.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyUsersTrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("feelingRate.trend",
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyFeelingTrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('downSwitch.trend',
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyDownSwitchTrendCharts(districts, frequency, result);
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('rank2Rate.trend',
        function($scope,
            beginDate,
            endDate,
            city,
            frequency,
            dialogTitle,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            kpiChartService,
            appRegionService) {
            $scope.dialogTitle = appFormatService.getDateString(beginDate.value, "yyyy年MM月dd日") +
                '-' +
                appFormatService.getDateString(endDate.value, "yyyy年MM月dd日") +
                dialogTitle;
            kpiPreciseService.getDateSpanFlowRegionKpi(city, beginDate.value, endDate.value, frequency)
                .then(function(result) {
                    appRegionService.queryDistricts(city).then(function(districts) {
                        kpiChartService.generateDistrictFrequencyRand2TrendCharts(districts, frequency, result);
                    });

                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.city);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.coverage', ['app.menu', 'app.core'])

    .factory('coverageDialogService', function (menuItemService, stationFormatService) {
        return {
            showDetails: function (cellName, cellId, sectorId) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/Coverage/DetailsChartDialog.html',
                    controller: 'coverage.details.dialog',
                    resolve: {
                        cellName: function () {
                            return cellName;
                        },
                        cellId: function () {
                            return cellId;
                        },
                        sectorId: function () {
                            return sectorId;
                        }
                    }
                });
            },
            showSource: function (currentView, serialNumber, beginDate, endDate, callback) {
                menuItemService.showGeneralDialogWithAction({
                        templateUrl: '/appViews/Rutrace/Interference/SourceDialog.html',
                        controller: 'interference.source.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return currentView.eNodebName + "-" + currentView.sectorId + "干扰源分析";
                                },
                                eNodebId: function() {
                                    return currentView.eNodebId;
                                },
                                sectorId: function() {
                                    return currentView.sectorId;
                                },
                                serialNumber: function() {
                                    return serialNumber;
                                }
                            },
                            beginDate,
                            endDate)
                    },
                    function(info) {
                        callback(info);
                    });
            },
            showSourceDbChart: function (currentView) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/Interference/SourceDbChartDialog.html',
                    controller: 'interference.source.db.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰源图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                });
            },
            showSourceModChart: function (currentView, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/SourceModChartDialog.html',
                    controller: 'interference.source.mod.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "MOD3/MOD6干扰图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showSourceStrengthChart: function (currentView, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/SourceStrengthChartDialog.html',
                    controller: 'interference.source.strength.chart',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰强度图表";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        name: function () {
                            return currentView.eNodebName;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showInterferenceVictim: function (currentView, serialNumber, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/VictimDialog.html',
                    controller: 'interference.victim.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "干扰小区分析";
                        },
                        eNodebId: function () {
                            return currentView.eNodebId;
                        },
                        sectorId: function () {
                            return currentView.sectorId;
                        },
                        serialNumber: function () {
                            return serialNumber;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },
            showCoverage: function (currentView, preciseCells, callback) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/Rutrace/Interference/CoverageDialog.html',
                    controller: 'interference.coverage.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return currentView.eNodebName + "-" + currentView.sectorId + "覆盖分析";
                        },
                        preciseCells: function () {
                            return preciseCells;
                        }
                    }
                }, function (info) {
                    callback(info);
                });
            },///////////未完成
            showGridStats: function (district, town, theme, category, data, keys) {
                var stats = [];
                angular.forEach(keys, function (key) {
                    stats.push({
                        key: key,
                        value: data[key]
                    });
                });
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/SingleChartDialog.html',
                    controller: 'grid.stats',
                    resolve: {
                        dialogTitle: function () {
                            return district + town + theme;
                        },
                        category: function () {
                            return category;
                        },
                        stats: function () {
                            return stats;
                        }
                    }
                });
            },
            showGridClusterStats: function (theme, clusterList, currentCluster, action) {
                menuItemService.showGeneralDialogWithAction({
                    templateUrl: '/appViews/BasicKpi/GridClusterDialog.html',
                    controller: 'grid.cluster',
                    resolve: {
                        dialogTitle: function () {
                            return theme + "栅格簇信息";
                        },
                        clusterList: function () {
                            return clusterList;
                        },
                        currentCluster: function () {
                            return currentCluster;
                        }
                    }
                }, action);
            },
            showAgpsStats: function (stats, legend) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'agps.stats',
                    resolve: {
                        dialogTitle: function () {
                            return 'AGPS三网对比覆盖指标';
                        },
                        stats: function () {
                            return stats;
                        },
                        legend: function() {
                            return legend;
                        }
                    }
                });
            },
            showTownStats: function (cityName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'town.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市LTE基站小区分布";
                        },
                        cityName: function () {
                            return cityName;
                        }
                    }
                });
            },
            showCdmaTownStats: function (cityName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/DoubleChartDialog.html',
                    controller: 'cdma.town.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市CDMA基站小区分布";
                        },
                        cityName: function () {
                            return cityName;
                        }
                    }
                });
            },
            showFlowStats: function (today, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/DoubleChartDialog.html',
                    controller: 'flow.stats',
                    resolve: {
                        dialogTitle: function () {
                            return "全市4G流量和用户数分布";
                        },
                        today: function () {
                            return today;
                        },
                        frequency: function() {
                            return frequency;
                        }
                    }
                });
            },
            showFlowTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'flow.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "流量变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showUsersTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'users.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "用户数变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showFeelingRateTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'feelingRate.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "感知速率变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showDownSwitchTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'downSwitch.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "4G下切3G变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showRank2RateTrend: function (city, beginDate, endDate, frequency) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'rank2Rate.trend',
                    resolve: stationFormatService.dateSpanDateResolve({
                            dialogTitle: function() {
                                return city + "4G双流比变化趋势";
                            },
                            city: function() {
                                return city;
                            },
                            frequency: function() {
                                return frequency;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
            showUserRoles: function (userName) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Manage/UserRolesDialog.html',
                    controller: 'user.roles.dialog',
                    resolve: {
                        dialogTitle: function () {
                            return userName + "角色管理";
                        },
                        userName: function () {
                            return userName;
                        }
                    }
                });
            }
        }
    })

angular.module('kpi.customer', ['myApp.url', 'myApp.region'])
    .factory('customerDialogService',
        function(menuItemService, customerQueryService, emergencyService, complainService, basicImportService) {
            return {
                constructEmergencyCommunication: function(city, district, type, messages, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Emergency.html',
                            controller: 'emergency.new.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "新增应急通信需求";
                                },
                                city: function() {
                                    return city;
                                },
                                district: function() {
                                    return district;
                                },
                                vehicularType: function() {
                                    return type;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.postDto(dto).then(function(result) {
                                if (result > 0) {
                                    messages.push({
                                        type: 'success',
                                        contents: '完成应急通信需求：' + dto.projectName + '的导入'
                                    });
                                    callback();
                                } else {
                                    messages.push({
                                        type: 'warning',
                                        contents: '最近已经有该需求，请不要重复导入'
                                    });
                                }
                            });
                        });
                },
                constructEmergencyCollege: function(serialNumber, collegeName, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Emergency.html',
                            controller: 'emergency.college.dialog',
                            resolve: {
                                serialNumber: function() {
                                    return serialNumber;
                                },
                                collegeName: function() {
                                    return collegeName;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.postDto(dto).then(function(result) {
                                callback();
                            });
                        });
                },
                constructHotSpot: function(callback, callback2) {
                    menuItemService.showGeneralDialogWithDoubleAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpot.html',
                            controller: 'hot.spot.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return '新增热点信息';
                                }
                            }
                        },
                        function(dto) {
                            basicImportService.dumpOneHotSpot(dto).then(function(result) {
                                callback();
                            });
                        },
                        callback2);
                },
                modifyHotSpot: function(item, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpot.html',
                            controller: 'hot.spot.modify',
                            resolve: {
                                dialogTitle: function() {
                                    return '修改热点信息-' + item.hotspotName;
                                },
                                dto: function() {
                                    return item;
                                }
                            }
                        },
                        function(dto) {
                            basicImportService.dumpOneHotSpot(dto).then(function(result) {
                                callback();
                            });
                        });
                },
                manageHotSpotCells: function(hotSpot, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Parameters/Import/HotSpotCell.html',
                            controller: 'hot.spot.cell.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return hotSpot.hotspotName + '热点小区管理';
                                },
                                name: function() {
                                    return hotSpot.hotspotName;
                                },
                                address: function() {
                                    return hotSpot.address;
                                },
                                center: function() {
                                    return {
                                        longtitute: hotSpot.longtitute,
                                        lattitute: hotSpot.lattitute
                                    }
                                }
                            }
                        },
                        function(dto) {
                            callback(dto);
                        });
                },
                supplementVipDemandInfo: function(view, city, district, messages, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/VipSupplement.html',
                            controller: 'vip.supplement.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "补充政企客户支撑需求信息";
                                },
                                view: function() {
                                    return view;
                                },
                                city: function() {
                                    return city;
                                },
                                district: function() {
                                    return district;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.updateVip(dto).then(function() {
                                messages.push({
                                    type: 'success',
                                    contents: '完成政企客户支撑需求：' + dto.serialNumber + '的补充'
                                });
                                callback();
                            });
                        });
                },
                supplementCollegeDemandInfo: function(view, messages) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/CollegeSupplement.html',
                            controller: 'college.supplement.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "补充校园网支撑需求信息";
                                },
                                view: function() {
                                    return view;
                                }
                            }
                        },
                        function(dto) {
                            customerQueryService.updateVip(dto).then(function() {
                                messages.push({
                                    type: 'success',
                                    contents: '完成校园网支撑需求：' + dto.serialNumber + '的补充'
                                });
                            });
                        });
                },
                constructFiberItem: function(id, num, callback, messages) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Fiber.html',
                            controller: 'fiber.new.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return "新增光纤工单信息";
                                },
                                id: function() {
                                    return id;
                                },
                                num: function() {
                                    return num;
                                }
                            }
                        },
                        function(item) {
                            emergencyService.createFiberItem(item).then(function(result) {
                                if (result) {
                                    messages.push({
                                        type: 'success',
                                        contents: '完成光纤工单：' + item.workItemNumber + '的导入'
                                    });
                                    callback(result);
                                } else {
                                    messages.push({
                                        type: 'warning',
                                        contents: '最近已经有该工单，请不要重复导入'
                                    });
                                }
                            });
                        });
                },
                supplementComplainInfo: function(item, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Customer/Dialog/Complain.html',
                            controller: 'complain.supplement.dialog',
                            resolve: {
                                item: function() {
                                    return item;
                                }
                            }
                        },
                        function(info) {
                            complainService.postPosition(info).then(function() {
                                callback();
                            });
                        });
                }
            };
        });
angular.module('kpi.customer.complain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('emergency.new.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            dialogTitle,
            city,
            district,
            vehicularType) {
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";
            $scope.city = city;
            $scope.district = district;
            $scope.vehicularType = vehicularType;

            var firstDay = new Date();
            firstDay.setDate(firstDay.getDate() + 7);
            var nextDay = new Date();
            nextDay.setDate(nextDay.getDate() + 14);
            $scope.itemBeginDate = {
                value: firstDay,
                opened: false
            };
            $scope.itemEndDate = {
                value: nextDay,
                opened: false
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            $scope.dto = {
                projectName: "和顺梦里水乡百合花文化节",
                expectedPeople: 500000,
                vehicles: 1,
                area: "万顷洋园艺世界",
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "此次活动为佛山市南海区政府组织的一次大型文化活动，是宣传天翼品牌的重要场合。",
                townId: 1
            };

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('emergency.college.dialog',
        function($scope,
            $uibModalInstance,
            serialNumber,
            collegeName,
            collegeQueryService,
            appFormatService,
            customerQueryService,
            appRegionService) {
            $scope.dialogTitle = collegeName + "应急通信车申请-" + serialNumber;
            $scope.dto = {
                projectName: collegeName + "应急通信车申请",
                expectedPeople: 500000,
                vehicles: 1,
                area: collegeName,
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "应急通信车申请。",
                townId: 1
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            customerQueryService.queryVehicleTypeOptions().then(function(options) {
                $scope.vehicularType = {
                    options: options,
                    selected: options[17]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            collegeQueryService.queryByNameAndYear(collegeName, $scope.collegeInfo.year.selected).then(function(item) {
                $scope.itemBeginDate = {
                    value: appFormatService.getDate(item.oldOpenDate),
                    opened: false
                };
                $scope.itemEndDate = {
                    value: appFormatService.getDate(item.newOpenDate),
                    opened: false
                };
                $scope.dto.expectedPeople = item.expectedSubscribers;
            });
            customerQueryService.queryOneVip(serialNumber).then(function(item) {
                angular.forEach($scope.district.options,
                    function(district) {
                        if (district === item.district) {
                            $scope.district.selected = item.district;
                        }
                    });
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town.options = towns;
                    $scope.town.selected = towns[0];
                    angular.forEach(towns,
                        function(town) {
                            if (town === item.town) {
                                $scope.town.selected = town;
                            }
                        });
                });
            });

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('vip.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view,
            city,
            district) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;
            $scope.city = city;
            $scope.district = district;
            $scope.matchFunction = function(text) {
                return $scope.view.projectName.indexOf(text) >= 0 || $scope.view.projectContents.indexOf(text) >= 0;
            };
            $scope.matchDistrictTown = function() {
                var districtOption = appFormatService.searchText($scope.district.options, $scope.matchFunction);
                if (districtOption) {
                    $scope.district.selected = districtOption;
                }
            };
            $scope.$watch('town.selected',
                function() {
                    var townOption = appFormatService.searchText($scope.town.options, $scope.matchFunction);
                    if (townOption) {
                        $scope.town.selected = townOption;
                    }
                });

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };
        })
    .controller('fiber.new.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            id,
            num) {
            $scope.dialogTitle = dialogTitle;

            $scope.item = {
                id: 0,
                emergencyId: id,
                workItemNumber: "FS-Fiber-" +
                    new Date().getYear() +
                    "-" +
                    new Date().getMonth() +
                    "-" +
                    new Date().getDate() +
                    "-" +
                    num,
                person: "",
                beginDate: new Date(),
                finishDate: null
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.item);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('complain.supplement.dialog',
        function($scope,
            $uibModalInstance,
            appRegionService,
            appFormatService,
            baiduMapService,
            parametersMapService,
            parametersDialogService,
            item) {
            $scope.dialogTitle = item.serialNumber + "工单信息补充";

            $scope.itemGroups = appFormatService.generateComplainPositionGroups(item);
            appRegionService.initializeCities().then(function(cities) {
                $scope.city.options = cities;
                $scope.city.selected = cities[0];
                appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                    $scope.district.options = districts;
                    $scope.district.selected = (item.district) ? item.district.replace('区', '') : districts[0];
                    baiduMapService.initializeMap("map", 11);
                    baiduMapService.addCityBoundary("佛山");
                    if (item.longtitute && item.lattitute) {
                        var marker = baiduMapService.generateMarker(item.longtitute, item.lattitute);
                        baiduMapService.addOneMarker(marker);
                        baiduMapService.setCellFocus(item.longtitute, item.lattitute, 15);
                    }
                    if (item.sitePosition) {
                        parametersMapService.showElementsWithGeneralName(item.sitePosition,
                            parametersDialogService.showENodebInfo,
                            parametersDialogService.showCellInfo);
                    }
                });
            });

            $scope.matchTown = function() {
                var town = appFormatService.searchPattern($scope.town.options, item.sitePosition);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.buildingName);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.roadName);
                if (town) {
                    $scope.town.selected = town;
                }
            };

            $scope.ok = function() {
                $scope.item.district = $scope.district.selected;
                $scope.item.town = $scope.town.selected;
                $uibModalInstance.close($scope.item);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.customer.sustain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('hot.spot.dialog',
        function($scope, dialogTitle, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = {
                longtitute: 112.99,
                lattitute: 22.98
            };

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                var options =_.filter(result,
                    function(stat) {
                        return stat !== '高速公路' && stat !== '其他' &&
                            stat !== '高速铁路' && stat !== '区域定义' &&
                            stat !== '地铁' && stat !== 'TOP小区' && stat !== '校园网';
                    });
                $scope.spotType = {
                    options: options,
                    selected: options[0]
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.modify',
        function($scope, dialogTitle, dto, $uibModalInstance, kpiPreciseService, baiduMapService, baiduQueryService) {
            $scope.dialogTitle = dialogTitle;
            $scope.dto = dto;
            $scope.modify = true;

            kpiPreciseService.getHotSpotTypeSelection().then(function(result) {
                $scope.spotType = {
                    options: result,
                    selected: $scope.dto.typeDescription
                };
            });
            baiduQueryService.transformToBaidu($scope.dto.longtitute, $scope.dto.lattitute).then(function(coors) {
                var xOffset = coors.x - $scope.dto.longtitute;
                var yOffset = coors.y - $scope.dto.lattitute;
                baiduMapService.initializeMap("hot-map", 15);
                baiduMapService.addOneMarker(baiduMapService
                    .generateMarker(coors.x, coors.y));
                baiduMapService.setCellFocus(coors.x, coors.y);
                baiduMapService.addClickListener(function(e) {
                    $scope.dto.longtitute = e.point.lng - xOffset;
                    $scope.dto.lattitute = e.point.lat - yOffset;
                    baiduMapService.clearOverlays();
                    baiduMapService.addOneMarker(baiduMapService
                        .generateMarker(e.point.lng, e.point.lat));
                });
            });
            $scope.matchPlace = function() {
                if (!$scope.dto.hotspotName || $scope.dto.hotspotName === '') return;
                baiduQueryService.queryBaiduPlace($scope.dto.hotspotName).then(function(result) {
                    angular.forEach(result,
                        function(place) {
                            var marker = baiduMapService.generateMarker(place.location.lng, place.location.lat);
                            baiduMapService.addOneMarker(marker);
                            baiduMapService.drawLabel(place.name, place.location.lng, place.location.lat);
                            baiduMapService.setCellFocus(place.location.lng, place.location.lat);
                        });
                });
            };
            $scope.ok = function() {
                $scope.dto.typeDescription = $scope.spotType.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('hot.spot.cell.dialog',
        function($scope,
            dialogTitle,
            address,
            name,
            center,
            $uibModalInstance,
            basicImportService,
            collegeQueryService,
            networkElementService,
            neighborImportService,
            complainService) {
            $scope.dialogTitle = dialogTitle;
            $scope.address = address;
            $scope.gridApi = {};
            $scope.gridApi2 = {};
            $scope.updateCellInfos = function(positions, existedCells) {
                neighborImportService.updateENodebRruInfo($scope.positionCells,
                    {
                        dstCells: positions,
                        cells: existedCells,
                        longtitute: center.longtitute,
                        lattitute: center.lattitute
                    });
            };
            $scope.query = function() {
                basicImportService.queryHotSpotCells(name).then(function(result) {
                    $scope.candidateIndoorCells = result;
                });
                complainService.queryHotSpotCells(name).then(function(existedCells) {
                    $scope.existedCells = existedCells;
                    $scope.positionCells = [];
                    networkElementService.queryRangeCells({
                        west: center.longtitute - 0.003,
                        east: center.longtitute + 0.003,
                        south: center.lattitute - 0.003,
                        north: center.lattitute + 0.003
                    }).then(function(positions) {
                        if (positions.length > 5) {
                            $scope.updateCellInfos(positions, existedCells);
                        } else {
                            networkElementService.queryRangeCells({
                                west: center.longtitute - 0.01,
                                east: center.longtitute + 0.01,
                                south: center.lattitute - 0.01,
                                north: center.lattitute + 0.01
                            }).then(function(pos) {
                                if (pos.length > 5) {
                                    $scope.updateCellInfos(pos, existedCells);
                                } else {
                                    networkElementService.queryRangeCells({
                                        west: center.longtitute - 0.02,
                                        east: center.longtitute + 0.02,
                                        south: center.lattitute - 0.02,
                                        north: center.lattitute + 0.02
                                    }).then(function(pos2) {
                                        $scope.updateCellInfos(pos2, existedCells);
                                    });
                                }
                                
                            });
                        }
                    });
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.importCells = function() {
                var cellNames = [];
                angular.forEach($scope.gridApi.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                angular.forEach($scope.gridApi2.selection.getSelectedRows(),
                    function(cell) {
                        cellNames.push(cell.cellName);
                    });
                collegeQueryService.saveCollegeCells({
                    collegeName: name,
                    cellNames: cellNames
                }).then(function() {
                    $scope.query();
                });
            }
            $scope.query();
        })
    .controller('college.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;

            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.parameter.dump', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('dump.cell.mongo',
        function($scope,
            $uibModalInstance,
            dumpProgress,
            appFormatService,
            dumpPreciseService,
            neighborMongoService,
            preciseInterferenceService,
            networkElementService,
            dialogTitle,
            cell,
            begin,
            end) {
            $scope.dialogTitle = dialogTitle;

            $scope.dateRecords = [];
            $scope.currentDetails = [];
            $scope.currentIndex = 0;

            $scope.ok = function() {
                $uibModalInstance.close($scope.dateRecords);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.queryRecords = function() {
                $scope.mrsRsrpStats = [];
                $scope.mrsTaStats = [];
                angular.forEach($scope.dateRecords,
                    function(record) {
                        dumpProgress.queryExistedItems(cell.eNodebId, cell.sectorId, record.date)
                            .then(function(result) {
                                record.existedRecords = result;
                            });
                        dumpProgress.queryMongoItems(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mongoRecords = result;
                        });
                        dumpProgress.queryMrsRsrpItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsRsrpStats = result;
                            if (result) {
                                $scope.mrsRsrpStats.push({
                                    statDate: result.statDate,
                                    data: _.map(_.range(48),
                                        function(index) {
                                            return result['rsrP_' + appFormatService.prefixInteger(index, 2)];
                                        })
                                });
                            }

                        });
                        dumpProgress.queryMrsTadvItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsTaStats = result;
                            if (result) {
                                $scope.mrsTaStats.push({
                                    statDate: result.statDate,
                                    data: _.map(_.range(44),
                                        function(index) {
                                            return result['tadv_' + appFormatService.prefixInteger(index, 2)];
                                        })
                                });
                            }

                        });
                        dumpProgress.queryMrsPhrItem(cell.eNodebId, cell.sectorId, record.date).then(function(result) {
                            record.mrsPhrStats = result;
                        });
                        dumpProgress.queryMrsTadvRsrpItem(cell.eNodebId, cell.sectorId, record.date)
                            .then(function(result) {
                                record.mrsTaRsrpStats = result;
                            });
                    });
            };

            $scope.updateDetails = function(index) {
                $scope.currentIndex = index % $scope.dateRecords.length;
            };

            $scope.dumpAllRecords = function() {
                dumpPreciseService.dumpAllRecords($scope.dateRecords,
                    0,
                    0,
                    cell.eNodebId,
                    cell.sectorId,
                    $scope.queryRecords);
            };

            $scope.showNeighbors = function() {
                $scope.neighborCells = [];
                networkElementService.queryCellNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                    $scope.neighborCells = result;
                });

            };
            $scope.showReverseNeighbors = function() {
                neighborMongoService.queryReverseNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                    $scope.reverseCells = result;
                    angular.forEach(result,
                        function(neighbor) {
                            networkElementService.queryENodebInfo(neighbor.cellId).then(function(info) {
                                neighbor.eNodebName = info.name;
                            });
                        });
                });
            }
            $scope.updatePci = function() {
                networkElementService.updateCellPci(cell).then(function(result) {
                    $scope.updateMessages.push({
                        cellName: cell.name + '-' + cell.sectorId,
                        counts: result
                    });
                    $scope.showNeighbors();
                });
            };
            $scope.synchronizeNeighbors = function() {
                var count = 0;
                neighborMongoService.queryNeighbors(cell.eNodebId, cell.sectorId).then(function(neighbors) {
                    angular.forEach(neighbors,
                        function(neighbor) {
                            if (neighbor.neighborCellId > 0 && neighbor.neighborPci > 0) {
                                networkElementService.updateNeighbors(neighbor.cellId,
                                    neighbor.sectorId,
                                    neighbor.neighborPci,
                                    neighbor.neighborCellId,
                                    neighbor.neighborSectorId).then(function() {
                                    count += 1;
                                    if (count === neighbors.length) {
                                        $scope.updateMessages.push({
                                            cellName: $scope.currentCellName,
                                            counts: count
                                        });
                                        $scope.showNeighbors();
                                    }
                                });
                            } else {
                                count += 1;
                                if (count === neighbors.length) {
                                    $scope.updateMessages.push({
                                        cellName: $scope.currentCellName,
                                        counts: count
                                    });
                                    $scope.showNeighbors();
                                }
                            }
                        });
                });
            };

            var startDate = new Date(begin);
            while (startDate < end) {
                var date = new Date(startDate);
                $scope.dateRecords.push({
                    date: date,
                    existedRecords: 0,
                    existedStat: false
                });
                startDate.setDate(date.getDate() + 1);
            }
            $scope.neighborCells = [];
            $scope.updateMessages = [];

            $scope.queryRecords();
            $scope.showReverseNeighbors();
            $scope.showNeighbors();
        })
    .controller('neighbors.dialog',
        function($scope,
            $uibModalInstance,
            geometryService,
            dialogTitle,
            candidateNeighbors,
            currentCell) {
            $scope.pciNeighbors = [];
            $scope.indoorConsidered = false;
            $scope.distanceOrder = "distance";
            $scope.dialogTitle = dialogTitle;
            $scope.candidateNeighbors = candidateNeighbors;
            $scope.currentCell = currentCell;

            angular.forEach($scope.candidateNeighbors,
                function(neighbor) {
                    neighbor.distance = geometryService.getDistance($scope.currentCell.lattitute,
                        $scope.currentCell.longtitute,
                        neighbor.lattitute,
                        neighbor.longtitute);

                    $scope.pciNeighbors.push(neighbor);
                });

            $scope.updateNearestCell = function() {
                var minDistance = 10000;
                angular.forEach($scope.candidateNeighbors,
                    function(neighbor) {
                        if (neighbor.distance < minDistance && (neighbor.indoor === '室外' || $scope.indoorConsidered)) {
                            minDistance = neighbor.distance;
                            $scope.nearestCell = neighbor;
                        }
                    });

            };

            $scope.ok = function() {
                $scope.updateNearestCell();
                $uibModalInstance.close($scope.nearestCell);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.parameter.rutrace', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller("rutrace.interference.analysis",
        function($scope,
            $uibModalInstance,
            cell,
            topPreciseService,
            kpiDisplayService,
            preciseInterferenceService,
            neighborMongoService,
            networkElementService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "TOP指标干扰分析: " + $scope.currentCellName;
            $scope.oneAtATime = false;
            $scope.orderPolicy = topPreciseService.getOrderPolicySelection();
            $scope.updateMessages = [];

            networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(info) {
                $scope.current = {
                    cellId: cell.cellId,
                    sectorId: cell.sectorId,
                    eNodebName: cell.name,
                    longtitute: info.longtitute,
                    lattitute: info.lattitute
                };
            });

            $scope.showInterference = function() {
                $scope.interferenceCells = [];
                $scope.victimCells = [];

                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    angular.forEach(result,
                        function(interference) {
                            for (var i = 0; i < $scope.mongoNeighbors.length; i++) {
                                var neighbor = $scope.mongoNeighbors[i];
                                if (neighbor.neighborPci === interference.destPci) {
                                    interference.isMongoNeighbor = true;
                                    break;
                                }
                            }
                        });
                    $scope.interferenceCells = result;
                    preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(victims) {
                        angular.forEach(victims,
                            function(victim) {
                                for (var j = 0; j < result.length; j++) {
                                    if (result[j].destENodebId === victim.victimENodebId &&
                                        result[j].destSectorId === victim.victimSectorId) {
                                        victim.forwardInterferences6Db = result[j].overInterferences6Db;
                                        victim.forwardInterferences10Db = result[j].overInterferences10Db;
                                        break;
                                    }
                                }
                            });
                        $scope.victimCells = victims;
                    });
                    var pieOptions = kpiDisplayService.getInterferencePieOptions(result, $scope.currentCellName);
                    $("#interference-over6db").highcharts(pieOptions.over6DbOption);
                    $("#interference-over10db").highcharts(pieOptions.over10DbOption);
                    $("#interference-mod3").highcharts(pieOptions.mod3Option);
                    $("#interference-mod6").highcharts(pieOptions.mod6Option);
                    topPreciseService.queryRsrpTa($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(info) {
                    });
                });
            };

            $scope.updateNeighborInfos = function() {
                preciseInterferenceService.updateInterferenceNeighbor(cell.cellId, cell.sectorId)
                    .then(function(result) {
                        $scope.updateMessages.push({
                            cellName: $scope.currentCellName,
                            counts: result,
                            type: "干扰"
                        });
                    });

                preciseInterferenceService.updateInterferenceVictim(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.updateMessages.push({
                        cellName: $scope.currentCellName,
                        counts: result,
                        type: "被干扰"
                    });
                });
            }

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(cell.cellId, cell.sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
                $scope.showInterference();
                $scope.updateNeighborInfos();
            });
        })
    .controller("rutrace.map.analysis",
        function($scope,
            $uibModalInstance,
            cell,
            dialogTitle,
            beginDate,
            endDate,
            baiduQueryService,
            baiduMapService,
            networkElementService,
            neighborDialogService,
            parametersDialogService,
            menuItemService,
            cellPreciseService,
            neighborMongoService,
            preciseInterferenceService) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.neighborLines = [];
            $scope.displayNeighbors = false;
            $scope.reverseNeighborLines = [];
            $scope.displayReverseNeighbors = false;
            $scope.interferenceLines = [];
            $scope.interferenceCircles = [];
            $scope.displayInterference = false;
            $scope.victimLines = [];
            $scope.victimCircles = [];
            $scope.displayVictims = false;

            $scope.initializeMap = function() {
                cellPreciseService.queryOneWeekKpi(cell.cellId, cell.sectorId).then(function(cellView) {
                    baiduMapService.switchSubMap();
                    baiduMapService.initializeMap("all-map", 12);
                    networkElementService.queryCellSectors([cellView]).then(function(result) {
                        baiduQueryService.transformToBaidu(result[0].longtitute, result[0].lattitute)
                            .then(function(coors) {
                                var xOffset = coors.x - result[0].longtitute;
                                var yOffset = coors.y - result[0].lattitute;
                                result[0].longtitute = coors.x;
                                result[0].lattitute = coors.y;

                                var sectorTriangle = baiduMapService.generateSector(result[0], "blue", 1.25);
                                baiduMapService.addOneSectorToScope(sectorTriangle,
                                    neighborDialogService.showPrecise,
                                    result[0]);

                                baiduMapService.setCellFocus(result[0].longtitute, result[0].lattitute, 15);
                                var range = baiduMapService.getCurrentMapRange(-xOffset, -yOffset);

                                networkElementService.queryRangeSectors(range, []).then(function(sectors) {
                                    angular.forEach(sectors,
                                        function(sector) {
                                            sector.longtitute += xOffset;
                                            sector.lattitute += yOffset;
                                            baiduMapService.addOneSectorToScope(
                                                baiduMapService.generateSector(sector, "green"),
                                                function() {
                                                    parametersDialogService
                                                        .showCellInfo(sector, $scope.beginDate, $scope.endDate);
                                                },
                                                sector);
                                        });
                                });
                            });

                    });
                    networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(item) {
                        if (item) {
                            $scope.generateComponents(item);
                        }
                    });
                });
            };


            $scope.generateComponents = function(item) {
                baiduQueryService.transformToBaidu(item.longtitute, item.lattitute).then(function(coors) {
                    var xOffset = coors.x - item.longtitute;
                    var yOffset = coors.y - item.lattitute;
                    neighborMongoService.queryNeighbors(cell.cellId, cell.sectorId).then(function(neighbors) {
                        baiduMapService.generateNeighborLines($scope.neighborLines,
                        {
                            cell: item,
                            neighbors: neighbors,
                            xOffset: xOffset,
                            yOffset: yOffset
                        });
                    });
                    neighborMongoService.queryReverseNeighbors(cell.cellId, cell.sectorId).then(function(neighbors) {
                        baiduMapService.generateReverseNeighborLines($scope.reverseNeighborLines,
                        {
                            cell: item,
                            neighbors: neighbors,
                            xOffset: xOffset,
                            yOffset: yOffset
                        });
                    });
                    preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(interference) {
                        baiduMapService.generateInterferenceComponents(
                            $scope.interferenceLines,
                            $scope.interferenceCircles,
                            item,
                            interference,
                            xOffset,
                            yOffset,
                            "orange",
                            neighborDialogService.showInterferenceSource);
                    });
                    preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                        $scope.endDate.value,
                        cell.cellId,
                        cell.sectorId).then(function(victims) {
                        baiduMapService.generateVictimComponents($scope.victimLines,
                            $scope.victimCircles,
                            item,
                            victims,
                            xOffset,
                            yOffset,
                            "green",
                            neighborDialogService.showInterferenceVictim);
                    });
                });
            };

            $scope.toggleNeighbors = function() {
                if ($scope.displayNeighbors) {
                    baiduMapService.removeOverlays($scope.neighborLines);
                    $scope.displayNeighbors = false;
                } else {
                    baiduMapService.addOverlays($scope.neighborLines);
                    $scope.displayNeighbors = true;
                }
            };

            $scope.toggleReverseNeighbers = function() {
                if ($scope.displayReverseNeighbors) {
                    baiduMapService.removeOverlays($scope.reverseNeighborLines);
                    $scope.displayReverseNeighbors = false;
                } else {
                    baiduMapService.addOverlays($scope.reverseNeighborLines);
                    $scope.displayReverseNeighbors = true;
                }
            };

            $scope.toggleInterference = function() {
                if ($scope.displayInterference) {
                    baiduMapService.removeOverlays($scope.interferenceLines);
                    baiduMapService.removeOverlays($scope.interferenceCircles);
                    $scope.displayInterference = false;
                } else {
                    baiduMapService.addOverlays($scope.interferenceLines);
                    baiduMapService.addOverlays($scope.interferenceCircles);
                    $scope.displayInterference = true;
                }
            };

            $scope.toggleVictims = function() {
                if ($scope.displayVictims) {
                    baiduMapService.removeOverlays($scope.victimLines);
                    baiduMapService.removeOverlays($scope.victimCircles);
                    $scope.displayVictims = false;
                } else {
                    baiduMapService.addOverlays($scope.victimLines);
                    baiduMapService.addOverlays($scope.victimCircles);
                    $scope.displayVictims = true;
                }
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.initializeMap();
        })
    .controller("rutrace.coverage.analysis",
        function($scope,
            cell,
            $uibModalInstance,
            topPreciseService,
            preciseInterferenceService,
            preciseChartService,
            coverageService,
            kpiDisplayService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "TOP指标覆盖分析: " + $scope.currentCellName;
            $scope.orderPolicy = topPreciseService.getOrderPolicySelection();
            $scope.detailsDialogTitle = cell.name + "-" + cell.sectorId + "详细小区统计";
            $scope.cellId = cell.cellId;
            $scope.sectorId = cell.sectorId;
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
                preciseInterferenceService.queryInterferenceNeighbor($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    $scope.interferenceCells = result;
                    angular.forEach($scope.interferenceCells,
                        function(neighbor) {
                            if (neighbor.destENodebId > 0) {
                                kpiDisplayService.updateCoverageKpi(neighbor,
                                    {
                                        cellId: neighbor.destENodebId,
                                        sectorId: neighbor.destSectorId
                                    },
                                    {
                                        begin: $scope.beginDate.value,
                                        end: $scope.endDate.value
                                    });
                            }
                        });
                });
                preciseInterferenceService.queryInterferenceVictim($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    $scope.interferenceVictims = result;
                    angular.forEach($scope.interferenceVictims,
                        function(victim) {
                            if (victim.victimENodebId > 0) {
                                kpiDisplayService.updateCoverageKpi(victim,
                                    {
                                        cellId: victim.victimENodebId,
                                        sectorId: victim.victimSectorId
                                    },
                                    {
                                        begin: $scope.beginDate.value,
                                        end: $scope.endDate.value
                                    });
                            }
                        });
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller("general.coverage.analysis",
        function($scope,
            cell,
            $uibModalInstance,
            topPreciseService,
            preciseInterferenceService,
            preciseChartService) {
            $scope.currentCellName = cell.name + "-" + cell.sectorId;
            $scope.dialogTitle = "小区覆盖分析: " + $scope.currentCellName;
            $scope.detailsDialogTitle = cell.name + "-" + cell.sectorId + "详细小区统计";
            $scope.cellId = cell.cellId;
            $scope.sectorId = cell.sectorId;
            $scope.showCoverage = function() {
                topPreciseService.queryRsrpTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    for (var rsrpIndex = 0; rsrpIndex < 12; rsrpIndex++) {
                        var options = preciseChartService.getRsrpTaOptions(result, rsrpIndex);
                        $("#rsrp-ta-" + rsrpIndex).highcharts(options);
                    }
                });
                topPreciseService.queryCoverage($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getCoverageOptions(result);
                    $("#coverage-chart").highcharts(options);
                });
                topPreciseService.queryTa($scope.beginDate.value,
                    $scope.endDate.value,
                    cell.cellId,
                    cell.sectorId).then(function(result) {
                    var options = preciseChartService.getTaOptions(result);
                    $("#ta-chart").highcharts(options);
                });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.interferenceCells);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showCoverage();
        })
    .controller('map.source.dialog',
        function($scope, $uibModalInstance, neighbor, dialogTitle, topPreciseService, preciseChartService) {
            $scope.neighbor = neighbor;
            $scope.dialogTitle = dialogTitle;
            if (neighbor.cellId !== undefined) {
                $scope.cellId = neighbor.cellId;
                $scope.sectorId = neighbor.sectorId;
            } else {
                $scope.cellId = neighbor.destENodebId;
                $scope.sectorId = neighbor.destSectorId;
            }
            topPreciseService.queryCoverage($scope.beginDate.value,
                $scope.endDate.value,
                $scope.cellId,
                $scope.sectorId).then(function(result) {
                var options = preciseChartService.getCoverageOptions(result);
                $("#coverage-chart").highcharts(options);
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("cell.info.dialog",
        function($scope, cell, dialogTitle, neighborMongoService, $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.isHuaweiCell = false;
            $scope.eNodebId = cell.eNodebId;
            $scope.sectorId = cell.sectorId;

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            neighborMongoService.queryNeighbors(cell.eNodebId, cell.sectorId).then(function(result) {
                $scope.mongoNeighbors = result;
            });

        });
angular.module('kpi.parameter.query', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('query.setting.dialog',
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            beginDate,
            endDate,
            appRegionService,
            parametersMapService,
            parametersDialogService,
            networkElementService) {
            $scope.network = {
                options: ["LTE", "CDMA"],
                selected: "LTE"
            };
            $scope.queryText = "";
            $scope.city = city;
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.eNodebList = [];
            $scope.btsList = [];

            $scope.updateDistricts = function() {
                appRegionService.queryDistricts($scope.city.selected).then(function(result) {
                    $scope.district.options = result;
                    $scope.district.selected = result[0];
                });
            };
            $scope.updateTowns = function() {
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(result) {
                    $scope.town.options = result;
                    $scope.town.selected = result[0];
                });
            };

            $scope.queryItems = function() {
                if ($scope.network.selected === "LTE") {
                    if ($scope.queryText.trim() === "") {
                        networkElementService
                            .queryENodebsInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                            .then(function(eNodebs) {
                                $scope.eNodebList = eNodebs;
                            });
                    } else {
                        networkElementService.queryENodebsByGeneralName($scope.queryText).then(function(eNodebs) {
                            $scope.eNodebList = eNodebs;
                        });
                    }
                } else {
                    if ($scope.queryText.trim() === "") {
                        networkElementService
                            .queryBtssInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected)
                            .then(function(btss) {
                                $scope.btsList = btss;
                            });
                    } else {
                        networkElementService.queryBtssByGeneralName($scope.queryText).then(function(btss) {
                            $scope.btsList = btss;
                        });
                    }
                }
            };
            appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                $scope.district = {
                    options: districts,
                    selected: districts[0]
                };
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town = {
                        options: towns,
                        selected: towns[0]
                    };
                });
            });
            $scope.ok = function() {
                if ($scope.network.selected === "LTE") {
                    if ($scope.queryText.trim() === "") {
                        parametersMapService.showElementsInOneTown($scope.city.selected,
                            $scope.district.selected,
                            $scope.town.selected,
                            $scope.beginDate,
                            $scope.endDate);
                    } else {
                        parametersMapService
                            .showElementsWithGeneralName($scope.queryText, $scope.beginDate, $scope.endDate);
                    }
                } else {
                    if ($scope.queryText.trim() === "") {
                        parametersMapService
                            .showCdmaInOneTown($scope.city.selected, $scope.district.selected, $scope.town.selected);
                    } else {
                        parametersMapService.showCdmaWithGeneralName($scope.queryText);
                    }
                }
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cell.type.chart',
        function($scope, $uibModalInstance, city, dialogTitle, appRegionService, parametersChartService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            appRegionService.queryDistrictIndoorCells(city.selected).then(function(stats) {
                $("#leftChart").highcharts(parametersChartService.getCellIndoorTypeColumnOptions(stats));
            });
            appRegionService.queryDistrictBandCells(city.selected).then(function(stats) {
                $("#rightChart").highcharts(parametersChartService.getCellBandClassColumnOptions(stats));
            });
        })
    .controller("flow.kpi.dialog",
        function($scope,
            cell,
            begin,
            end,
            dialogTitle,
            flowService,
            generalChartService,
            calculateService,
            parametersChartService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.itemGroups = calculateService.generateFlowDetailsGroups(cell);
            flowService.queryAverageRrcByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                $scope.rrcGroups = calculateService.generateRrcDetailsGroups(result);
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            flowService.queryCellFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#flowChart").highcharts(parametersChartService.getCellFlowOptions(dates, result));
                $("#feelingRateChart").highcharts(parametersChartService.getCellFeelingRateOptions(dates, result));
                $("#downSwitchChart").highcharts(parametersChartService.getCellDownSwitchOptions(dates, result));
                $("#rank2Chart").highcharts(parametersChartService.getCellRank2Options(dates, result));
            });
        })
    .controller("rrc.kpi.dialog",
        function($scope,
            cell,
            begin,
            end,
            dialogTitle,
            flowService,
            generalChartService,
            calculateService,
            parametersChartService,
            networkElementService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.rrcGroups = calculateService.generateRrcDetailsGroups(cell);
            flowService.queryAverageFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                networkElementService.queryCellInfo(cell.eNodebId, cell.sectorId).then(function(item) {
                    $scope.itemGroups = calculateService.generateFlowDetailsGroups(angular.extend(result, item));
                });
            });

            flowService.queryCellRrcByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#rrcRequestChart").highcharts(parametersChartService.getCellRrcRequestOptions(dates, result));
                $("#rrcFailChart").highcharts(parametersChartService.getCellRrcFailOptions(dates, result));
                $("#rrcRateChart").highcharts(parametersChartService.getCellRrcRateOptions(dates, result));
            });
            flowService.queryCellFlowByDateSpan(cell.eNodebId, cell.sectorId, begin, end).then(function(result) {
                var dates = _.map(result,
                    function(stat) {
                        return stat.statTime;
                    });
                $("#flowChart").highcharts(parametersChartService.getCellFlowUsersOptions(dates, result));
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.mongoNeighbors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });
angular.module('kpi.parameter', ['app.menu', 'app.core', 'region.network'])
    .factory('neighborDialogService',
        function(menuItemService, networkElementService, stationFormatService, baiduMapService) {
            return {
                dumpCellMongo: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Interference/DumpCellMongoDialog.html',
                        controller: 'dump.cell.mongo',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "干扰数据导入";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showRutraceInterference: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Interference/Index.html',
                        controller: 'rutrace.interference.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "干扰指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showRutraceInterferenceMap: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Rutrace/Map/Index.html',
                            controller: 'rutrace.map.analysis',
                            resolve: stationFormatService.dateSpanDateResolve({
                                    dialogTitle: function() {
                                        return "小区地理化分析" + ": " + cell.name + "-" + cell.sectorId;
                                    },
                                    cell: function() {
                                        return cell;
                                    }
                                },
                                beginDate,
                                endDate)
                        },
                        function(info) {
                            baiduMapService.switchMainMap();
                        });
                },
                showRutraceCoverage: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Coverage/Index.html',
                        controller: 'rutrace.coverage.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "覆盖指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showGeneralCoverage: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Coverage/General.html',
                        controller: 'general.coverage.analysis',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.name + "-" + cell.sectorId + "覆盖指标分析";
                                },
                                cell: function() {
                                    return cell;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showPrecise: function(precise) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/PreciseSectorMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return precise.eNodebName + "-" + precise.sectorId + "精确覆盖率指标";
                            },
                            neighbor: function() {
                                return precise;
                            }
                        }
                    });
                },
                showCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/CellInfo.html',
                        controller: 'cell.info.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return cell.eNodebName + "-" + cell.sectorId + "小区详细信息";
                            },
                            cell: function() {
                                return cell;
                            }
                        }
                    });
                },
                setQueryConditions: function(city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/QueryMap.html',
                        controller: 'query.setting.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "小区信息查询条件设置";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                queryCellTypeChart: function(city) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/DoubleChartDialog.html',
                        controller: 'cell.type.chart',
                        resolve: {
                            dialogTitle: function() {
                                return "全网小区类型统计";
                            },
                            city: function() {
                                return city;
                            }
                        }
                    });
                },
                showFlowCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/FlowKpiInfo.html',
                        controller: 'flow.kpi.dialog',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.item.eNodebName + "-" + cell.item.sectorId + "小区流量相关指标信息";
                                },
                                cell: function() {
                                    return cell.item;
                                }
                            },
                            cell.beginDate.value,
                            cell.endDate.value)
                    });
                },
                showRrcCell: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/RrcKpiInfo.html',
                        controller: 'rrc.kpi.dialog',
                        resolve: stationFormatService.dateSpanResolve({
                                dialogTitle: function() {
                                    return cell.item.eNodebName + "-" + cell.item.sectorId + "小区RRC连接指标信息";
                                },
                                cell: function() {
                                    return cell.item;
                                }
                            },
                            cell.beginDate.value,
                            cell.endDate.value)
                    });
                },
                showInterferenceSource: function(neighbor) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/SourceMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return neighbor.neighborCellName + "干扰源信息";
                            },
                            neighbor: function() {
                                return neighbor;
                            }
                        }
                    });
                },
                showInterferenceVictim: function(neighbor) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/VictimMapInfoBox.html',
                        controller: 'map.source.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return neighbor.victimCellName + "被干扰小区信息";
                            },
                            neighbor: function() {
                                return neighbor;
                            }
                        }
                    });
                },
                matchNeighbor: function(center, candidate, neighbors) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/Rutrace/Interference/MatchCellDialog.html',
                            controller: 'neighbors.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return center.eNodebName +
                                        "-" +
                                        center.sectorId +
                                        "的邻区PCI=" +
                                        candidate.destPci +
                                        "，频点=" +
                                        candidate.neighborEarfcn +
                                        "的可能小区";
                                },
                                candidateNeighbors: function() {
                                    return neighbors;
                                },
                                currentCell: function() {
                                    return center;
                                }
                            }
                        },
                        function(nearestCell) {
                            networkElementService.updateNeighbors(center.cellId,
                                center.sectorId,
                                candidate.destPci,
                                nearestCell.eNodebId,
                                nearestCell.sectorId).then(function() {
                                candidate.neighborCellName = nearestCell.eNodebName + "-" + nearestCell.sectorId;
                            });
                        });
                }
            }
        });
angular.module('kpi.work.dialog', ['myApp.url', 'myApp.region', "ui.bootstrap", "kpi.core"])
    .controller('workitem.feedback.dialog',
        function($scope, $uibModalInstance, input, dialogTitle) {
            $scope.item = input;
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";

            $scope.ok = function() {
                $uibModalInstance.close($scope.message);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('workitem.details.dialog',
        function($scope, $uibModalInstance, input, dialogTitle, preciseWorkItemGenerator) {
            $scope.currentView = input;
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";
            $scope.platformInfos = preciseWorkItemGenerator.calculatePlatformInfo($scope.currentView.comments);
            $scope.feedbackInfos = preciseWorkItemGenerator.calculatePlatformInfo($scope.currentView.feedbackContents);
            $scope.preventChangeParentView = true;

            $scope.ok = function() {
                $uibModalInstance.close($scope.message);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.distribution.dialog',
        function($scope,
            $uibModalInstance,
            distribution,
            dialogTitle,
            appFormatService,
            networkElementService,
            alarmsService) {
            $scope.distribution = distribution;
            $scope.dialogTitle = dialogTitle;
            $scope.distributionGroups = appFormatService.generateDistributionGroups(distribution);
            $scope.alarmLevel = {
                options: ["严重告警", "重要以上告警", "所有告警"],
                selected: "重要以上告警"
            };
            $scope.alarms = [];
            $scope.searchAlarms = function() {
                alarmsService.queryENodebAlarmsByDateSpanAndLevel(distribution.eNodebId,
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.alarmLevel.selected).then(function(result) {
                    $scope.alarms = result;
                });
            };
            if (distribution.eNodebId > 0) {
                networkElementService.queryCellInfo(distribution.eNodebId, distribution.lteSectorId)
                    .then(function(cell) {
                        $scope.lteGroups = appFormatService.generateCellGroups(cell);
                    });
                $scope.searchAlarms();
            }
            if (distribution.btsId > 0) {
                networkElementService.queryCdmaCellInfo(distribution.btsId, distribution.cdmaSectorId)
                    .then(function(cell) {
                        $scope.cdmaGroups = appFormatService.generateCdmaCellGroups(cell);
                    });
            }

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("rutrace.workitems.process",
        function($scope,
            $uibModalInstance,
            cell,
            beginDate,
            endDate,
            workitemService,
            networkElementService,
            appFormatService) {
            $scope.dialogTitle = cell.eNodebName + "-" + cell.sectorId + ":TOP小区工单历史";
            $scope.queryWorkItems = function() {
                workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.viewItems = result;
                });
                networkElementService.queryCellInfo(cell.cellId, cell.sectorId).then(function(result) {
                    $scope.lteCellGroups = appFormatService.generateCellGroups(result);
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.queryWorkItems();
        });
angular.module('kpi.work.flow', ['myApp.url', 'myApp.region', "ui.bootstrap", "kpi.core"])
    .controller("eNodeb.flow",
        function($scope,
            $uibModalInstance,
            eNodeb,
            beginDate,
            endDate,
            networkElementService,
            appKpiService,
            kpiChartService) {
            $scope.eNodebName = eNodeb.name;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.queryFlow = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };

            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, $scope.eNodebName, $scope.mergeStats);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            networkElementService.queryCellViewsInOneENodeb(eNodeb.eNodebId).then(function(result) {
                $scope.cellList = result;
                $scope.queryFlow();
            });
        })
    .controller("hotSpot.cell.flow",
        function($scope,
            $uibModalInstance,
            hotSpot,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.eNodebName = hotSpot.hotspotName;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.queryFlow = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };

            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, $scope.eNodebName, $scope.mergeStats);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            complainService.queryHotSpotCells(hotSpot.hotspotName).then(function(result) {
                $scope.cellList = result;
                $scope.queryFlow();
            });
        })
    .controller("topic.cells",
        function($scope, $uibModalInstance, dialogTitle, name, complainService) {
            $scope.dialogTitle = dialogTitle;
            complainService.queryHotSpotCells(name).then(function(existedCells) {
                $scope.cellList = existedCells;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        });
angular.module('kpi.work.chart', ['myApp.url', 'myApp.region', "ui.bootstrap", "kpi.core"])
    .controller("rutrace.chart",
        function($scope,
            $uibModalInstance,
            $timeout,
            dateString,
            districtStats,
            townStats,
            appKpiService) {
            $scope.dialogTitle = dateString + "精确覆盖率指标";
            $scope.showCharts = function() {
                $("#leftChart").highcharts(appKpiService
                    .getMrPieOptions(districtStats.slice(0, districtStats.length - 1), townStats));
                $("#rightChart").highcharts(appKpiService.getPreciseRateOptions(districtStats, townStats));
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $timeout(function() {
                    $scope.showCharts();
                },
                500);
        })
    .controller("rrc.chart",
        function($scope,
            $uibModalInstance,
            $timeout,
            dateString,
            districtStats,
            townStats,
            appKpiService) {
            $scope.dialogTitle = dateString + "RRC连接成功率指标";
            $scope.showCharts = function() {
                $("#leftChart").highcharts(appKpiService
                    .getRrcRequestOptions(districtStats.slice(0, districtStats.length - 1), townStats));
                $("#rightChart").highcharts(appKpiService.getRrcRateOptions(districtStats, townStats));
                $("#thirdChart").highcharts(appKpiService.getMoSignallingRrcRateOptions(districtStats, townStats));
                $("#fourthChart").highcharts(appKpiService.getMtAccessRrcRateOptions(districtStats, townStats));
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.cellList);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $timeout(function() {
                    $scope.showCharts();
                },
                500);
        })
    .controller("down.switch.cell.trend",
        function($scope,
            $uibModalInstance,
            name,
            cellId,
            sectorId,
            flowService,
            parametersChartService,
            appFormatService) {
            $scope.dialogTitle = "小区指标变化趋势分析" + "-" + name;
            $scope.showTrend = function() {
                $scope.beginDateString = appFormatService.getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                $scope.endDateString = appFormatService.getDateString($scope.endDate.value, "yyyy年MM月dd日");
                flowService.queryCellFlowByDateSpan(cellId,
                    sectorId,
                    $scope.beginDate.value,
                    $scope.endDate.value).then(function (result) {
                        var dates = _.map(result,
                            function (stat) {
                                return stat.statTime;
                            });
                        $("#mrsConfig").highcharts(parametersChartService.getCellFeelingRateOptions(dates, result));
                        $("#preciseConfig").highcharts(parametersChartService.getCellDownSwitchOptions(dates, result));
                });
            };
            $scope.showTrend();

            $scope.ok = function() {
                $uibModalInstance.close($scope.distributionGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('kpi.work.trend', ['myApp.url', 'myApp.region', "ui.bootstrap", "kpi.core"])
    .run(function($rootScope) {
        $rootScope.trendStat = {
            stats: [],
            districts: [],
            districtStats: [],
            townStats: [],
            beginDateString: "",
            endDateString: ""
        };
    })
    .controller('basic.kpi.trend',
        function($scope, $uibModalInstance, city, beginDate, endDate, kpi2GService, kpiDisplayService) {
            $scope.dialogTitle = "指标变化趋势-" + city;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            kpi2GService.queryKpiOptions().then(function(result) {
                $scope.kpi = {
                    options: result,
                    selected: result[0]
                };
            });

            $scope.$watch('kpi.options',
                function(options) {
                    if (options && options.length) {
                        $scope.showTrend();
                    }
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.kpi);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showTrend = function() {
                kpi2GService.queryKpiTrend(city, $scope.beginDate.value, $scope.endDate.value).then(function(data) {
                    angular.forEach($scope.kpi.options,
                        function(option, $index) {
                            $("#kpi-" + $index).highcharts(kpiDisplayService.generateComboChartOptions(data, option));
                        });
                });
            };
        })
    .controller("rutrace.trend.dialog",
        function($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            appRegionService,
            appKpiService,
            kpiPreciseService,
            appFormatService) {

            appRegionService.queryDistricts(city.selected)
                .then(function(districts) {
                    $scope.trendStat.districts = districts;
                });
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.dialogTitle = "精确覆盖率变化趋势";
            $scope.preciseFunc = function(stat) {
                return stat.precise;
            };
            $scope.mrFunc = function(stat) {
                return stat.mr;
            };
            $scope.showCharts = function() {
                $("#mr-pie").highcharts(appKpiService.getMrPieOptions($scope.trendStat.districtStats,
                    $scope.trendStat.townStats));
                $("#precise").highcharts(appKpiService.getPreciseRateOptions($scope.trendStat.districtStats,
                    $scope.trendStat.townStats));
                $("#time-mr").highcharts(appKpiService.getMrsDistrictOptions($scope.trendStat.stats,
                    $scope.trendStat.districts));
                $("#time-precise").highcharts(appKpiService.getPreciseDistrictOptions($scope.trendStat.stats,
                    $scope.trendStat.districts));
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            kpiPreciseService.getDateSpanPreciseRegionKpi($scope.city.selected,
                    $scope.beginDate.value,
                    $scope.endDate.value)
                .then(function(result) {
                    $scope.trendStat.stats = appKpiService.generateDistrictStats($scope.trendStat.districts, result);
                    if (result.length > 0) {
                        appKpiService.generateTrendStatsForPie($scope.trendStat, result);
                        $scope.trendStat.stats.push(appKpiService.calculateAverageRates($scope.trendStat.stats));
                    }
                    $scope.trendStat.beginDateString = appFormatService
                        .getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                    $scope.trendStat.endDateString = appFormatService
                        .getDateString($scope.endDate.value, "yyyy年MM月dd日");
                    $scope.showCharts();
                });

        })
    .controller("cqi.trend.dialog",
        function ($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            appRegionService,
            appKpiService,
            kpiPreciseService,
            mapDialogService,
            appFormatService) {

            appRegionService.queryDistricts(city.selected)
                .then(function (districts) {
                    $scope.trendStat.districts = districts;
                });
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.dialogTitle = "CQI优良比变化趋势";
            $scope.rateTitle = "CQI优良比";
            $scope.countTitle = "总调度次数";
            $scope.rateFunc = function (stat) {
                return stat.rate;
            };
            $scope.requestFunc = function (stat) {
                return stat.request;
            };
            $scope.showCharts = function () {
                $("#time-request").highcharts(appKpiService.getCqiCountsOptions($scope.trendStat.districtStats,
                    $scope.trendStat.townStats));
                $("#time-rate").highcharts(appKpiService.getCqiRateOptions($scope.trendStat.districtStats,
                    $scope.trendStat.townStats));
                $("#request-pie").highcharts(appKpiService.getCqiCountsDistrictOptions($scope.trendStat.stats,
                    $scope.trendStat.districts));
                $("#rate-column").highcharts(appKpiService.getCqiRateDistrictOptions($scope.trendStat.stats,
                    $scope.trendStat.districts));
            };
            $scope.showTopKpi = function () {
                mapDialogService.showCqiTop($scope.beginDate, $scope.endDate);
            };
            $scope.ok = function () {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            kpiPreciseService.getDateSpanCqiRegionKpi($scope.city.selected,
                $scope.beginDate.value,
                $scope.endDate.value)
                .then(function (result) {
                    $scope.trendStat.stats = appKpiService.generateCqiDistrictStats($scope.trendStat.districts, result);
                    if (result.length > 0) {
                        appKpiService.generateCqiTrendStatsForPie($scope.trendStat, result);
                        $scope.trendStat.stats.push(appKpiService.calculateAverageRrcRates($scope.trendStat.stats));
                    }
                    $scope.trendStat.beginDateString = appFormatService
                        .getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                    $scope.trendStat.endDateString = appFormatService
                        .getDateString($scope.endDate.value, "yyyy年MM月dd日");
                    $scope.showCharts();
                });
    })
    .controller("down.switch.trend.dialog",
        function ($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            appRegionService,
            appKpiService,
            kpiPreciseService,
            appFormatService) {

            appRegionService.queryDistricts(city.selected)
                .then(function (districts) {
                    $scope.trendStat.districts = districts;
                });
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.dialogTitle = "4G下切3G变化趋势";
            $scope.rateTitle = "下切比例";
            $scope.countTitle = "总下切次数";
            $scope.rateFunc = function (stat) {
                return stat.downSwitchRate;
            };
            $scope.requestFunc = function (stat) {
                return stat.downSwitchTimes;
            };
            $scope.showCharts = function () {
                $("#time-request").highcharts(appKpiService
                    .getDownSwitchTimesOptions($scope.trendStat.districtStats,
                        $scope.trendStat.townStats,
                        'all'));
                $("#time-rate").highcharts(appKpiService
                    .getDownSwitchRateOptions($scope.trendStat.districtStats,
                        $scope.trendStat.townStats,
                        'all'));
                $("#request-pie").highcharts(appKpiService
                    .getDownSwitchTimesDistrictOptions($scope.trendStat.stats,
                        $scope.trendStat.districts,
                        'all'));
                $("#rate-column").highcharts(appKpiService
                    .getDownSwitchRateDistrictOptions($scope.trendStat.stats,
                        $scope.trendStat.districts,
                        'all'));
            };
            $scope.ok = function () {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            kpiPreciseService.getDateSpanFlowRegionKpi($scope.city.selected,
                $scope.beginDate.value,
                $scope.endDate.value,
                'all')
                .then(function (result) {
                    $scope.trendStat.stats = appKpiService
                        .generateDownSwitchDistrictStats($scope.trendStat.districts, result);
                    if (result.length > 0) {
                        appKpiService.generateFlowTrendStatsForPie($scope.trendStat, result);
                        $scope.trendStat.stats.push(appKpiService.calculateAverageDownSwitchRates($scope.trendStat.stats));
                    }
                    $scope.trendStat.beginDateString = appFormatService
                        .getDateString($scope.beginDate.value, "yyyy年MM月dd日");
                    $scope.trendStat.endDateString = appFormatService
                        .getDateString($scope.endDate.value, "yyyy年MM月dd日");
                    $scope.showCharts();
                });
        })
    .controller('kpi.topConnection3G.trend',
        function($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            topCount,
            appRegionService,
            appFormatService,
            connection3GService) {
            $scope.dialogTitle = "TOP连接变化趋势-" + city;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.topCount = topCount;
            $scope.showTrend = function() {
                connection3GService.queryCellTrend($scope.beginDate.value,
                    $scope.endDate.value,
                    city,
                    $scope.orderPolicy.selected,
                    $scope.topCount.selected).then(function(result) {
                    $scope.trendCells = result;
                });
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            connection3GService.queryOrderPolicy().then(function(result) {
                $scope.orderPolicy = {
                    options: result,
                    selected: result[0]
                }
                $scope.showTrend();
            });
        })
    .controller('kpi.topDrop2G.trend',
        function($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            topCount,
            appRegionService,
            appFormatService,
            drop2GService) {
            $scope.dialogTitle = "TOP掉话变化趋势-" + city;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.topCount = topCount;
            $scope.ok = function() {
                $uibModalInstance.close($scope.trendStat);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.showTrend = function() {
                drop2GService.queryCellTrend($scope.beginDate.value,
                    $scope.endDate.value,
                    city,
                    $scope.orderPolicy.selected,
                    $scope.topCount.selected).then(function(result) {
                    $scope.trendCells = result;
                });
            };
            drop2GService.queryOrderPolicy().then(function(result) {
                $scope.orderPolicy = {
                    options: result,
                    selected: result[0]
                }
                $scope.showTrend();
            });
        });
angular.module('kpi.work', ['app.menu', 'app.core', 'myApp.region'])
    .factory('workItemDialog', function (menuItemService, workitemService, stationFormatService) {
		return {
			feedback: function(view, callbackFunc) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/WorkItem/FeedbackDialog.html',
					controller: 'workitem.feedback.dialog',
					resolve: {
						dialogTitle: function() {
							return view.serialNumber + "工单反馈";
						},
						input: function() {
							return view;
						}
					}
				}, function(output) {
					workitemService.feedback(output, view.serialNumber).then(function(result) {
						if (result && callbackFunc)
							callbackFunc();
					});
				});
			},
			showDetails: function(view, callbackFunc) {
				menuItemService.showGeneralDialogWithAction({
					templateUrl: '/appViews/WorkItem/DetailsDialog.html',
					controller: 'workitem.details.dialog',
					resolve: {
						dialogTitle: function() {
							return view.serialNumber + "工单信息";
						},
						input: function() {
							return view;
						}
					}
				}, callbackFunc);
			},
			showENodebFlow: function(eNodeb, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Parameters/Region/ENodebFlow.html',
			        controller: 'eNodeb.flow',
			        resolve: stationFormatService.dateSpanDateResolve({
			                eNodeb: function() {
			                    return eNodeb;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showHotSpotCellFlow: function(hotSpot, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Parameters/Region/ENodebFlow.html',
			        controller: 'hotSpot.cell.flow',
			        resolve: stationFormatService.dateSpanDateResolve({
			                hotSpot: function() {
			                    return hotSpot;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showHotSpotCells: function(name) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Parameters/Region/TopicCells.html',
					controller: 'topic.cells',
					resolve: {
						dialogTitle: function() {
							return name + "热点小区信息";
						},
						name: function() {
							return name;
						}
					}
				});
			},
			showPreciseChart: function(overallStat) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Home/DoubleChartDialog.html',
					controller: 'rutrace.chart',
					resolve: {
						dateString: function() {
							return overallStat.dateString;
						},
						districtStats: function() {
							return overallStat.districtStats;
						},
						townStats: function() {
							return overallStat.townStats;
						}
					}
				});
            },
            showRrcChart: function (overallStat) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Home/FourChartDialog.html',
                    controller: 'rrc.chart',
                    resolve: {
                        dateString: function () {
                            return overallStat.dateString;
                        },
                        districtStats: function () {
                            return overallStat.districtStats;
                        },
                        townStats: function () {
                            return overallStat.townStats;
                        }
                    }
                });
            },
			showPreciseTrend: function(city, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/Rutrace/Coverage/Trend.html',
			        controller: 'rutrace.trend.dialog',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
            showDownSwitchTrend: function (city, beginDate, endDate) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/WorkItem/RrcTrend.html',
                    controller: 'down.switch.trend.dialog',
                    resolve: stationFormatService.dateSpanDateResolve({
                            city: function() {
                                return city;
                            }
                        },
                        beginDate,
                        endDate)
                });
            },
			showBasicTrend: function(city, beginDate, endDate) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/Trend.html',
			        controller: 'basic.kpi.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showTopDropTrend: function(city, beginDate, endDate, topCount) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/TopDrop2GTrend.html',
			        controller: 'kpi.topDrop2G.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                },
			                topCount: function() {
			                    return topCount;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showTopConnectionTrend: function(city, beginDate, endDate, topCount) {
			    menuItemService.showGeneralDialog({
			        templateUrl: '/appViews/BasicKpi/TopConnection3GTrend.html',
			        controller: 'kpi.topConnection3G.trend',
			        resolve: stationFormatService.dateSpanDateResolve({
			                city: function() {
			                    return city;
			                },
			                topCount: function() {
			                    return topCount;
			                }
			            },
			            beginDate,
			            endDate)
			    });
			},
			showDistributionInfo: function(distribution) {
				menuItemService.showGeneralDialog({
					templateUrl: '/appViews/Parameters/Map/DistributionMapInfoBox.html',
					controller: 'map.distribution.dialog',
					resolve: {
						dialogTitle: function() {
							return distribution.name + "-" + "室内分布基本信息";
						},
						distribution: function() {
							return distribution;
						}
					}
				});
			},
			showPreciseCellTrend: function (name, cellId, sectorId) {
				menuItemService.showGeneralDialog({
				    templateUrl: '/appViews/Rutrace/WorkItem/CellTrend.html',
				    controller: 'rutrace.cell.trend',
					resolve: {
						name: function () {
							return name;
						},
						cellId: function () {
							return cellId;
						},
						sectorId: function() {
							return sectorId;
						}
					}
				});
            },
            showDownSwitchCellTrend: function (name, cellId, sectorId) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/WorkItem/CellTrend.html',
                    controller: 'down.switch.cell.trend',
                    resolve: {
                        name: function () {
                            return name;
                        },
                        cellId: function () {
                            return cellId;
                        },
                        sectorId: function () {
                            return sectorId;
                        }
                    }
                });
            },
            processPreciseWorkItem: function(cell, beginDate, endDate) {
                menuItemService.showGeneralDialog({
                    templateUrl: '/appViews/Rutrace/WorkItem/ForCell.html',
                    controller: "rutrace.workitems.process",
                    resolve: stationFormatService.dateSpanDateResolve({
                            cell: function() {
                                return cell;
                            }
                        },
                        beginDate,
                        endDate)
                });
            }
		};
	});

angular.module('myApp.kpi', ['kpi.core',
    'kpi.college.infrastructure', 'kpi.college.basic', 'kpi.college.maintain',
    'kpi.college.work', 'kpi.college.flow', 'kpi.college', 
    'kpi.coverage.interference', 'kpi.coverage.mr', 'kpi.coverage.stats', 'kpi.coverage.flow',
    "kpi.coverage", 'kpi.customer', 'kpi.customer.complain', 'kpi.customer.sustain',
    'kpi.parameter.dump', 'kpi.parameter.rutrace', 'kpi.parameter.query', 'kpi.parameter', 
    'kpi.work.dialog', 'kpi.work.flow', 'kpi.work.chart', 'kpi.work.trend', 'kpi.work']);