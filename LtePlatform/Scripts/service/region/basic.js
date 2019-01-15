angular.module('region.basic', ['app.core'])
    .factory('coverageService',
        function(generalHttpService) {
            return {
                queryAreaTestDate: function() {
                    return generalHttpService.getApiData('AreaTestDate', {});
                },
                updateTownTestDate: function(testDate, networkType, townId) {
                    return generalHttpService.getApiData('AreaTestDate',
                        {
                            testDate: testDate,
                            networkType: networkType,
                            townId: townId
                        });
                }
            };
        })
    .factory('customerQueryService',
        function(generalHttpService) {
            return {
                queryVehicleTypeOptions: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "VehicleType"
                    });
                },
                queryDemandLevelOptions: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "DemandLevel"
                    });
                },
                queryNetworkTypeOptions: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: 'NetworkType'
                    });
                },
                queryMarketThemeOptions: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: 'MarketTheme'
                    });
                },
                queryTransmitFunctionOptions: function() {
                    return ['光纤', '微波', '卫星'];
                },
                queryElectricSupplyOptions: function() {
                    return ['市电', '市电供电', '远供', '油机'];
                },
                postDto: function(dto) {
                    return generalHttpService.postApiData("EmergencyCommunication", dto);
                },
                queryAll: function(begin, end) {
                    return generalHttpService.getApiData("EmergencyCommunication",
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryAllVip: function(begin, end) {
                    return generalHttpService.getApiData("VipDemand",
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryOneVip: function(serialNumber) {
                    return generalHttpService.getApiData("VipDemand",
                    {
                        serialNumber: serialNumber
                    });
                },
                queryOneComplain: function(serialNumber) {
                    return generalHttpService.getApiData("ComplainQuery",
                    {
                        serialNumber: serialNumber
                    });
                },
                updateVip: function(dto) {
                    return generalHttpService.putApiData("VipDemand", dto);
                },
                queryOneEmergency: function(id) {
                    return generalHttpService.getApiData('EmergencyCommunication/' + id, {});
                }
            };
        })
    .factory('complainService',
        function(generalHttpService) {
            return {
                queryPositionList: function(begin, end) {
                    return generalHttpService.getApiData('ComplainPosition',
                        {
                            begin: begin,
                            end: end
                        });
                },
                postPosition: function(dto) {
                    return generalHttpService.postApiData('ComplainPosition', dto);
                },
                queryCurrentComplains: function(today) {
                    return generalHttpService.getApiData('ComplainTrend',
                        {
                            today: today
                        });
                },
                queryMonthTrend: function(date) {
                    return generalHttpService.getApiData('ComplainTrend',
                        {
                            date: date
                        });
                },
                queryBranchDemands: function(today) {
                    return generalHttpService.getApiData('BranchDemand',
                        {
                            today: today
                        });
                },
                queryLastMonthSustainCount: function(today) {
                    return generalHttpService.getApiData('SustainCount',
                        {
                            today: today
                        });
                },
                queryAll: function(begin, end) {
                    return generalHttpService.getApiData("ComplainQuery",
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryBranchList: function(begin, end) {
                    return generalHttpService.getApiData("BranchDemand",
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryOnlineList: function(begin, end) {
                    return generalHttpService.getApiData("OnlineSustain",
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryLastMonthOnlineList: function(today) {
                    return generalHttpService.getApiData("OnlineSustain",
                        {
                            today: today
                        });
                },
                queryLastMonthOnlineListInOneDistrict: function(today, city, district) {
                    return generalHttpService.getApiData("OnlineSustain",
                        {
                            today: today,
                            city: city,
                            district: district
                        });
                },
                queryLastMonthComplainListInOneDistrict: function(today, city, district) {
                    return generalHttpService.getApiData("ComplainQuery",
                        {
                            today: today,
                            city: city,
                            district: district
                        });
                },
                updateComplain: function(dto) {
                    return generalHttpService.putApiData("ComplainQuery", dto);
                },
                queryComplainMonthStats: function(date) {
                    return generalHttpService.getApiData("ComplainTrend",
                        {
                            countDate: date
                        });
                },
                queryLastDateComplainStats: function(date) {
                    return generalHttpService.getApiData("ComplainDate",
                        {
                            initialDate: date
                        });
                },
                queryDateSpanComplainStats: function(begin, end) {
                    return generalHttpService.getApiData("ComplainDate",
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryLastDateDistrictComplains: function(date, district) {
                    return generalHttpService.getApiData('ComplainQuery',
                        {
                            statDate: date,
                            district: district
                        });
                },
                queryDateSpanDistrictComplains: function(beginDate, endDate, district) {
                    return generalHttpService.getApiData('ComplainQuery',
                        {
                            beginDate: beginDate,
                            endDate: endDate,
                            district: district
                        });
                },
                queryHotSpotCells: function(name) {
                    return generalHttpService.getApiData('HotSpotCells',
                        {
                            name: name
                        });
                }
            };
        });