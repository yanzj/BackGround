angular.module('region.kpi', ['app.core'])
    .factory('kpiPreciseService',
        function(generalHttpService) {
            return {
                getRecentPreciseRegionKpi: function(city, initialDate) {
                    return generalHttpService.getApiData('PreciseRegion',
                    {
                        city: city,
                        statDate: initialDate
                    });
                },
                getDateSpanPreciseRegionKpi: function(city, beginDate, endDate) {
                    return generalHttpService.getApiData('PreciseRegion',
                    {
                        city: city,
                        begin: beginDate,
                        end: endDate
                    });
                },
                getRecentRrcRegionKpi: function(city, initialDate) {
                    return generalHttpService.getApiData('RrcRegion',
                    {
                        city: city,
                        statDate: initialDate
                    });
                },
                getRecentFlowRegionKpi: function (city, initialDate) {
                    return generalHttpService.getApiData('FlowRegion',
                        {
                            city: city,
                            statDate: initialDate
                        });
                },
                getDateSpanCqiRegionKpi: function (city, beginDate, endDate) {
                    return generalHttpService.getApiData('CqiRegion',
                        {
                            city: city,
                            begin: beginDate,
                            end: endDate
                        });
                },
                getDateSpanFlowRegionKpi: function(city, beginDate, endDate, frequency) {
                    return generalHttpService.getApiData('TownFlow',
                    {
                        city: city,
                        begin: beginDate,
                        end: endDate, 
                        frequency: frequency
                    });
                },
                getOrderSelection: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "OrderPreciseStatPolicy"
                    });
                },
                getDownSwitchOrderSelection: function () {
                    return generalHttpService.getApiData('KpiOptions',
                        {
                            key: "OrderDownSwitchPolicy"
                        });
                },
                getCqiOrderSelection: function () {
                    return generalHttpService.getApiData('KpiOptions',
                        {
                            key: "OrderCqiPolicy"
                        });
                },
                getHotSpotTypeSelection: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "HotspotType"
                    });
                }
            };
        })
    .factory('connection3GService',
        function(generalHttpService) {
            return {
                queryDayStats: function(city, initialDate) {
                    return generalHttpService.getApiData('TopConnection3G',
                    {
                        city: city,
                        statDate: initialDate
                    });
                },
                queryOrderPolicy: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "OrderTopConnection3GPolicy"
                    });
                },
                queryCellTrend: function(begin, end, city, policy, topCount) {
                    return generalHttpService.getApiData('TopConnection3G',
                    {
                        begin: begin,
                        end: end,
                        city: city,
                        policy: policy,
                        topCount: topCount
                    });
                }
            }
        });