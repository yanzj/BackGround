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
                },
                queryAgisDtPoints: function(begin, end) {
                    return generalHttpService.getApiData('AgisDtPoints',
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryAgisDtPointsByTopic: function(begin, end, topic) {
                    return generalHttpService.getApiData('AgisDtPoints',
                    {
                        begin: begin,
                        end: end,
                        topic: topic
                    });
                },
                queryAgpsTelecomByTown: function(begin, end, district, town) {
                    return generalHttpService.getApiData('AgpsTelecom',
                    {
                        begin: begin,
                        end: end,
                        district: district,
                        town: town
                    });
                },
                updateAgpsTelecomView: function(district, town, views) {
                    return generalHttpService.postApiData('AgpsTelecom',
                    {
                        district: district,
                        town: town,
                        views: views
                    });
                },
                updateAgpsMobileView: function(district, town, views) {
                    return generalHttpService.postApiData('AgpsMobile',
                    {
                        district: district,
                        town: town,
                        views: views
                    });
                },
                updateAgpsUnicomView: function(district, town, views) {
                    return generalHttpService.postApiData('AgpsUnicom',
                    {
                        district: district,
                        town: town,
                        views: views
                    });
                },
                queryAgpsMobileByTown: function(begin, end, district, town) {
                    return generalHttpService.getApiData('AgpsMobile',
                    {
                        begin: begin,
                        end: end,
                        district: district,
                        town: town
                    });
                },
                queryAgpsUnicomByTown: function(begin, end, district, town) {
                    return generalHttpService.getApiData('AgpsUnicom',
                    {
                        begin: begin,
                        end: end,
                        district: district,
                        town: town
                    });
                },
                queryMrGridSelfCoverage: function(district, statDate) {
                    return generalHttpService.getApiData('MrGrid',
                    {
                        district: district,
                        statDate: statDate
                    });
                },
                queryTownMrGridSelfCoverage: function(district, town, statDate) {
                    return generalHttpService.getApiData('TownMrGrid',
                    {
                        district: district,
                        town: town,
                        statDate: statDate
                    });
                },
                queryMrGridCompete: function(district, statDate, competeDescription) {
                    return generalHttpService.getApiData('MrGrid',
                    {
                        district: district,
                        statDate: statDate,
                        competeDescription: competeDescription
                    });
                },
                queryTownMrGridCompete: function(district, town, statDate, competeDescription) {
                    return generalHttpService.getApiData('TownMrGrid',
                    {
                        district: district,
                        town: town,
                        statDate: statDate,
                        competeDescription: competeDescription
                    });
                },
                queryByRasterInfo: function(info, type) {
                    var api;
                    info.csvFileName = info.csvFileName.replace(".csv", "");
                    switch (type) {
                    case '2G':
                        api = "Record2G";
                        break;
                    case '3G':
                        api = "Record3G";
                        break;
                    default:
                        api = "Record4G";
                        break;
                    }
                    return generalHttpService.postApiData(api, info);
                },
                queryDetailsByRasterInfo: function(info, type) {
                    var api;
                    switch (type) {
                    case '2G':
                        api = "Record2GDetails";
                        break;
                    case '3G':
                        api = "Record3GDetails";
                        break;
                    default:
                        api = "Record4GDetails";
                        break;
                    }
                    return generalHttpService.postApiData(api, info);
                },
                querySingleRasterInfo: function(fileName, rasterNum, type) {
                    var api;
                    switch (type) {
                    case '2G':
                        api = "Record2G";
                        break;
                    case '3G':
                        api = "Record3G";
                        break;
                    default:
                        api = "Record4G";
                        break;
                    }
                    return generalHttpService.getApiData(api,
                    {
                        fileName: fileName,
                        rasterNum: rasterNum
                    });
                },
                defaultRsrpCriteria: [
                    {
                        threshold: -120,
                        color: "#ff0000"
                    }, {
                        threshold: -115,
                        color: "#FF6666"
                    }, {
                        threshold: -110,
                        color: "#FF6600"
                    }, {
                        threshold: -105,
                        color: "#FFFF00"
                    }, {
                        threshold: -95,
                        color: "#0099CC"
                    }, {
                        threshold: -80,
                        color: "#009933"
                    }
                ],
                rsrpIntervalCriteria: [
                    {
                        threshold: "-110dbm以下",
                        color: "#FF6666"
                    }, {
                        threshold: "-105dbm到-110dbm",
                        color: "#FF6600"
                    }, {
                        threshold: "-100dbm到-105dbm",
                        color: "#0099CC"
                    }
                ],
                competeCriteria: [
                    {
                        threshold: "覆盖率小于于80%且比对方差",
                        color: "#FF0000"
                    }, {
                        threshold: "覆盖率小于于80%且比对方好",
                        color: "#0099CC"
                    }, {
                        threshold: "覆盖率大于80%且低于对方5%",
                        color: "#00FFFF"
                    }, {
                        threshold: "覆盖率大于80%且高于对方5%",
                        color: "#00FF00"
                    }
                ],
                defaultSinrCriteria: [
                    {
                        threshold: -3,
                        color: "#ff0000"
                    }, {
                        threshold: 0,
                        color: "#7f0808"
                    }, {
                        threshold: 3,
                        color: "#3f0f0f"
                    }, {
                        threshold: 6,
                        color: "#077f7f"
                    }, {
                        threshold: 9,
                        color: "#07073f"
                    }, {
                        threshold: 15,
                        color: "#073f07"
                    }
                ],
                defaultRxCriteria: [
                    {
                        threshold: -110,
                        color: "#ff0000"
                    }, {
                        threshold: -105,
                        color: "#ff08f8"
                    }, {
                        threshold: -100,
                        color: "#ffff0f"
                    }, {
                        threshold: -95,
                        color: "#070fff"
                    }, {
                        threshold: -85,
                        color: "#07f7ff"
                    }, {
                        threshold: -70,
                        color: "#07ff07"
                    }
                ],
                defaultSinr3GCriteria: [
                    {
                        threshold: -9,
                        color: "#ff0000"
                    }, {
                        threshold: -6,
                        color: "#7f0808"
                    }, {
                        threshold: -3,
                        color: "#3f0f0f"
                    }, {
                        threshold: 0,
                        color: "#077f7f"
                    }, {
                        threshold: 3,
                        color: "#07073f"
                    }, {
                        threshold: 7,
                        color: "#073f07"
                    }
                ],
                defaultEcioCriteria: [
                    {
                        threshold: -15,
                        color: "#ff0000"
                    }, {
                        threshold: -12,
                        color: "#ff08f8"
                    }, {
                        threshold: -9,
                        color: "#ffff0f"
                    }, {
                        threshold: -7,
                        color: "#070fff"
                    }, {
                        threshold: -5,
                        color: "#07f7ff"
                    }, {
                        threshold: -3,
                        color: "#07ff07"
                    }
                ],
                defaultTxCriteria: [
                    {
                        threshold: 12,
                        color: "#ff0000"
                    }, {
                        threshold: 6,
                        color: "#7f0808"
                    }, {
                        threshold: 0,
                        color: "#3f0f0f"
                    }, {
                        threshold: -3,
                        color: "#077f7f"
                    }, {
                        threshold: -6,
                        color: "#07073f"
                    }, {
                        threshold: -12,
                        color: "#073f07"
                    }
                ]
            }
        })
    .factory('preciseInterferenceService',
        function(generalHttpService) {
            return {
                updateInterferenceNeighbor: function(cellId, sectorId) {
                    return generalHttpService.getApiData('InterferenceNeighbor',
                    {
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryInterferenceNeighbor: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('InterferenceNeighbor',
                    {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                },
                updateInterferenceVictim: function(cellId, sectorId) {
                    return generalHttpService.getApiData('InterferenceNeighbor',
                    {
                        neighborCellId: cellId,
                        neighborSectorId: sectorId
                    });
                },
                queryInterferenceVictim: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('InterferenceVictim',
                    {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                }
            };
        })
    .factory('topPreciseService',
        function(generalHttpService) {
            return {
                getOrderPolicySelection: function() {
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
                    return {
                        options: options,
                        selected: options[4].value
                    };
                },
                queryCoverage: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('MrsRsrp',
                    {
                        'begin': begin,
                        'end': end,
                        'eNodebId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryTa: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('MrsTadv',
                    {
                        'begin': begin,
                        'end': end,
                        'eNodebId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryRsrpTa: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('MrsTadvRsrp',
                    {
                        'begin': begin,
                        'end': end,
                        'eNodebId': cellId,
                        'sectorId': sectorId
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
    .factory('emergencyService',
        function(generalHttpService) {
            return {
                queryProcessList: function(id) {
                    return generalHttpService.getApiData('EmergencyProcess/' + id, {});
                },
                createProcess: function(dto) {
                    return generalHttpService.postApiDataWithHeading('EmergencyProcess', dto);
                },
                createVipProcess: function(dto) {
                    return generalHttpService.postApiDataWithHeading('VipProcess', dto);
                },
                updateProcess: function(process) {
                    return generalHttpService.putApiData('EmergencyProcess', process);
                },
                updateVipProcess: function(process) {
                    return generalHttpService.putApiData('VipProcess', process);
                },
                finishVipProcess: function(process) {
                    return generalHttpService.postApiDataWithHeading('VipProcessFinish', process);
                },
                createFiberItem: function(item) {
                    return generalHttpService.postApiData('EmergencyFiber', item);
                },
                finishFiberItem: function(item) {
                    return generalHttpService.putApiData('EmergencyFiber', item);
                },
                queryFiberList: function(id) {
                    return generalHttpService.getApiData('EmergencyFiber/' + id, {});
                },
                queryVipDemands: function(today) {
                    return generalHttpService.getApiData('VipDemand',
                    {
                        today: today
                    });
                },
                queryCollegeVipDemands: function(year) {
                    return generalHttpService.getApiData('CollegeVipDemand',
                    {
                        year: year
                    });
                },
                queryCollegeVipDemand: function(year, collegeName) {
                    return generalHttpService.getApiData('CollegeVipDemand',
                    {
                        collegeName: collegeName,
                        year: year
                    });
                },
                queryVipProcessList: function(number) {
                    return generalHttpService.getApiData('VipProcess',
                    {
                        serialNumber: number
                    });
                },
                constructCollegeVipDemand: function(stat) {
                    return generalHttpService.postApiDataWithHeading('CollegeVipDemand', stat);
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
                queryLastMonthComplainListInOneDistrict: function (today, city, district) {
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
                queryDateSpanComplainStats: function (begin, end) {
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
                queryDateSpanDistrictComplains: function (beginDate, endDate, district) {
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
            }
        })
    .factory('downSwitchService',
        function(generalHttpService, appUrlService) {
            return {
                getStationListByName: function (name, areaName, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/llist',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": name,
                            "areaName": areaName
                        });
                },
                getIndoorListByName: function (name, areaName, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Indoor/llist',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": name,
                            "areaName": areaName
                        });
                },
                getFixingStationByName: function (name, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Fixing/search',
                        {
                        "curr_page": page,
                        "page_size": pageSize,
                        "stationName": name,
                        'type': type
                    });
                },
                getResourceStationByName: function (name, type, page, pageSize) {
                    return generalHttpService.postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Resource/search',
                        {
                        "curr_page": page,
                        "page_size": pageSize,
                        "stationName": name,
                        'type': type
                    });
                },
                getResource: function (table, id) {
                    return generalHttpService.postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Resource/single',
                        {
                        "table": table,
                        "id": id
                    });
                },
                getResourceCounter: function (id) {
                    return generalHttpService.postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Resource/counter',
                        {
                        "id": id
                    });
                },
                deleteIndoorById: function (stationId) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Indoor/delete',
                        {
                            "idList": stationId
                        });
                },
                deleteAssessmentById: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Assessment/delete',
                        {
                            "id": id
                        });
                },
                getStationsByAreaName: function(areaName, page, pageSize) {
                    return generalHttpService.getMvcData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/search/',
                        {
                            curr_page: page,
                            page_size: pageSize,
                            areaName: areaName
                        });
                },
                getStations: function(page, pageSize) {
                    return generalHttpService.getMvcData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/search/',
                        {
                            curr_page: page,
                            page_size: pageSize
                        });
                },
               getStationCommonById: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/single',
                        {
                        "id": id
                    });
                },
                getStationByStationId: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/single',
                        {
                            "SysStationId": id
                        });
                },
                updateStation: function(station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/update',
                            station);
                },
                updateIndoor: function (indoor) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Indoor/update',
                        indoor);
                },
                updateStationCommon: function(station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/update',
                            station);
                },
                addStation: function(station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Station/add', station);
                },
                addCheckPlan: function (station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Checking/addCheckPlan', station);
                },
                addCheckResults: function (station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Checking/addCheckResults', station);
                },
                addIndoor: function (indoor) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Indoor/add', indoor);
                },
                getAlarmHistorybyFilter: function (id, curr_page, page_size, alarmLevel, beginDate, endDate) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Alarmhistory/search',
                        {
                        "id": id,
                        "alarmLevel": alarmLevel,
                        "curr_page": curr_page,
                        "page_size": page_size,
                        "starttime": beginDate,
                        "endtime": endDate
                    });
                },
                getStationAddListByName: function (areaName, stationName, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/addList',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": stationName,
                            "areaName": areaName,
                            "type": type
                        });
                },
                getCheckPlanByName: function (stationName,areaName , type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Checking/checkPlanList',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": stationName,
                            "areaName": areaName,
                            "type": type
                        });
                },
                addCommonStation: function (station) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/add',
                        station);
                },
                getCommonStationByName: function(areaName, stationName, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": stationName,
                            "areaName": areaName,
                            "type": type
                        });
                },
                
                getCheckingStationByName: function (name, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Checking/search',
                        {
                        "curr_page": page,
                        "page_size": pageSize,
                        "stationName": name,
                        'type': type
                    });
                },
                getSpecialIndoor: function(recoverName, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/SpecialIndoor/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "recoverName": recoverName
                        });
                },
                getFaultStations: function(page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Fault/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize
                        });
                },
                getZeroVoice: function(isSolve, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/ZeroVoice/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "isSolve": isSolve
                        });
                },
                getZeroFlow: function(isSolve, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/ZeroFlow/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "isSolve": isSolve
                        });
                },
                getStationCnt: function (distinct,cycle) {
                    return generalHttpService.postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Assessment/stationCnt',
                        {
                            "areaName": distinct,
                            "cycle": cycle
                        });
                },
                queryDwgList: function(btsId) {
                    return generalHttpService.getApiData('DwgQuery',
                    {
                        directory: 'Common',
                        btsId: btsId
                    });
                },
                queryDwgUrl: function(btsId, fileName) {
                    return generalHttpService.getApiData('DwgView',
                    {
                        directory: 'Common',
                        btsId: btsId,
                        filename: fileName
                    });
                }
            };
        });
angular.module('region.mongo', ['app.core'])
    .factory('neighborMongoService',
        function(generalHttpService) {
            return {
                queryNeighbors: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('NeighborCellMongo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryReverseNeighbors: function(destENodebId, destSectorId) {
                    return generalHttpService.getApiData('NeighborCellMongo',
                    {
                        destENodebId: destENodebId,
                        destSectorId: destSectorId
                    });
                }
            };
        })
    .factory('cellHuaweiMongoService',
        function(generalHttpService) {
            return {
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('CellHuaweiMongo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryLocalCellDef: function(eNodebId) {
                    return generalHttpService.getApiData('CellHuaweiMongo',
                    {
                        eNodebId: eNodebId
                    });
                }
            };
        })
    .factory('cellPowerService',
        function(generalHttpService) {
            return {
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('CellPower',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryUlOpenLoopPowerControll: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('UplinkOpenLoopPowerControl',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        })
    .factory('intraFreqHoService',
        function(generalHttpService) {
            return {
                queryENodebParameters: function(eNodebId) {
                    return generalHttpService.getApiData('IntraFreqHo',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('IntraFreqHo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        })
    .factory('interFreqHoService',
        function(generalHttpService) {
            return {
                queryENodebParameters: function(eNodebId) {
                    return generalHttpService.getApiData('InterFreqHo',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryCellParameters: function(eNodebId, sectorId) {
                    return generalHttpService.getApiData('InterFreqHo',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                }
            };
        });
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
    .factory('kpi2GService',
        function(generalHttpService) {
            return {
                queryDayStats: function(city, initialDate) {
                    return generalHttpService.getApiData('KpiDataList',
                    {
                        city: city,
                        statDate: initialDate
                    });
                },
                queryKpiOptions: function() {
                    return generalHttpService.getApiData('KpiDataList', {});
                },
                queryKpiTrend: function(city, begin, end) {
                    return generalHttpService.getApiData('KpiDataList',
                    {
                        city: city,
                        beginDate: begin,
                        endDate: end
                    });
                }
            };
        })
    .factory('drop2GService',
        function(generalHttpService) {
            return {
                queryDayStats: function(city, initialDate) {
                    return generalHttpService.getApiData('TopDrop2G',
                    {
                        city: city,
                        statDate: initialDate
                    });
                },
                queryOrderPolicy: function() {
                    return generalHttpService.getApiData('KpiOptions',
                    {
                        key: "OrderTopDrop2GPolicy"
                    });
                },
                queryCellTrend: function(begin, end, city, policy, topCount) {
                    return generalHttpService.getApiData('TopDrop2G',
                    {
                        begin: begin,
                        end: end,
                        city: city,
                        policy: policy,
                        topCount: topCount
                    });
                }
            }
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
angular.module('region.import', ['app.core'])
    .factory('basicImportService',
        function(generalHttpService) {
            return {
                queryENodebExcels: function() {
                    return generalHttpService.getApiData('NewENodebExcels', {});
                },
                queryCellExcels: function() {
                    return generalHttpService.getApiData('NewCellExcels', {});
                },
                queryBtsExcels: function() {
                    return generalHttpService.getApiData('NewBtsExcels', {});
                },
                queryCdmaCellExcels: function() {
                    return generalHttpService.getApiData('NewCdmaCellExcels', {});
                },
                queryCellCount: function() {
                    return generalHttpService.getApiData('DumpLteRru', {});
                },
                queryCdmaCellCount: function() {
                    return generalHttpService.getApiData('DumpCdmaRru', {});
                },
                queryVanishedENodebs: function() {
                    return generalHttpService.getApiData('DumpENodebExcel', {});
                },
                queryVanishedBtss: function() {
                    return generalHttpService.getApiData('DumpBtsExcel', {});
                },
                queryVanishedCells: function() {
                    return generalHttpService.getApiData('DumpCellExcel', {});
                },
                queryVanishedCdmaCells: function() {
                    return generalHttpService.getApiData('DumpCdmaCellExcel', {});
                },
                dumpOneENodebExcel: function(item) {
                    return generalHttpService.postApiData('DumpENodebExcel', item);
                },
                dumpOneBtsExcel: function(item) {
                    return generalHttpService.postApiData('DumpBtsExcel', item);
                },
                dumpOneCellExcel: function(item) {
                    return generalHttpService.postApiData('DumpCellExcel', item);
                },
                dumpOneCdmaCellExcel: function(item) {
                    return generalHttpService.postApiData('DumpCdmaCellExcel', item);
                },
                updateLteCells: function() {
                    return generalHttpService.postApiData('DumpLteRru', {});
                },
                dumpLteRrus: function() {
                    return generalHttpService.putApiData('DumpLteRru', {});
                },
                dumpCdmaRrus: function() {
                    return generalHttpService.putApiData('DumpCdmaRru', {});
                },
                dumpMultipleENodebExcels: function(items) {
                    return generalHttpService.postApiData('NewENodebExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleBtsExcels: function(items) {
                    return generalHttpService.postApiData('NewBtsExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleCellExcels: function(items) {
                    return generalHttpService.postApiData('NewCellExcels',
                    {
                        infos: items
                    });
                },
                dumpMultipleCdmaCellExcels: function(items) {
                    return generalHttpService.postApiData('NewCdmaCellExcels',
                    {
                        infos: items
                    });
                },
                vanishENodebIds: function(ids) {
                    return generalHttpService.putApiData('DumpENodebExcel',
                    {
                        eNodebIds: ids
                    });
                },
                vanishBtsIds: function(ids) {
                    return generalHttpService.putApiData('DumpBtsExcel',
                    {
                        eNodebIds: ids
                    });
                },
                vanishCellIds: function(ids) {
                    return generalHttpService.putApiData('DumpCellExcel',
                    {
                        cellIdPairs: ids
                    });
                },
                vanishCdmaCellIds: function(ids) {
                    return generalHttpService.putApiData('DumpCdmaCellExcel',
                    {
                        cellIdPairs: ids
                    });
                },
                dumpOneHotSpot: function(item) {
                    return generalHttpService.postApiData('HotSpot', item);
                },
                queryAllHotSpots: function() {
                    return generalHttpService.getApiData('HotSpot', {});
                },
                queryHotSpotsByType: function(type) {
                    return generalHttpService.getApiData('HotSpot',
                    {
                        type: type
                    });
                },
                queryHotSpotCells: function(name) {
                    return generalHttpService.getApiData('LteRruCell',
                    {
                        rruName: name
                    });
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpNeighbor', {});
                },
                queryStationInfos: function() {
                    return generalHttpService.getApiData('DumpStationInfo', {});
                },
                dumpStationInfo: function () {
                    return generalHttpService.putApiData('DumpStationInfo', {});
                },
                resetStationInfo: function () {
                    return generalHttpService.deleteApiData('StationInfoReset', {});
                },
                queryStationENodebs: function () {
                    return generalHttpService.getApiData('DumpStationENodeb', {});
                },
                dumpStationENodeb: function () {
                    return generalHttpService.putApiData('DumpStationENodeb', {});
                },
                resetStationENodeb: function () {
                    return generalHttpService.deleteApiData('StationENodebReset', {});
                },
                queryStationCells: function () {
                    return generalHttpService.getApiData('DumpStationCell', {});
                },
                dumpStationCell: function () {
                    return generalHttpService.putApiData('DumpStationCell', {});
                },
                resetStationCell: function () {
                    return generalHttpService.deleteApiData('StationCellReset', {});
                },
                queryStationRrus: function () {
                    return generalHttpService.getApiData('DumpStationRru', {});
                },
                dumpStationRru: function () {
                    return generalHttpService.putApiData('DumpStationRru', {});
                },
                resetStationRru: function () {
                    return generalHttpService.deleteApiData('StationRruReset', {});
                },
                queryStationAntennas: function () {
                    return generalHttpService.getApiData('DumpStationAntenna', {});
                },
                dumpStationAntenna: function () {
                    return generalHttpService.putApiData('DumpStationAntenna', {});
                },
                resetStationAntenna: function () {
                    return generalHttpService.deleteApiData('StationAntennaReset', {});
                },
                queryStationDistributions: function () {
                    return generalHttpService.getApiData('DumpStationDistribution', {});
                },
                dumpStationDistribution: function () {
                    return generalHttpService.putApiData('DumpStationDistribution', {});
                },
                resetStationDistribution: function () {
                    return generalHttpService.deleteApiData('StationDistributionReset', {});
                }
            };
        })
    .factory('flowImportService',
        function(generalHttpService) {
            return {
                queryHuaweiFlows: function() {
                    return generalHttpService.getApiData('DumpHuaweiFlow', {});
                },
                queryHuaweiCqis: function() {
                    return generalHttpService.getApiData('DumpHuaweiCqi', {});
                },
                queryZteFlows: function() {
                    return generalHttpService.getApiData('DumpZteFlow', {});
                },
                queryHourPrbs: function () {
                    return generalHttpService.getApiData('DumpHourPrb', {});
                },
                queryHourUserses: function () {
                    return generalHttpService.getApiData('DumpHourUsers', {});
                },
                queryHourCqis: function () {
                    return generalHttpService.getApiData('DumpHourCqi', {});
                },

                clearDumpHuaweis: function() {
                    return generalHttpService.deleteApiData('DumpHuaweiFlow');
                },
                clearDumpCqiHuaweis: function() {
                    return generalHttpService.deleteApiData('DumpHuaweiCqi');
                },
                clearDumpZtes: function() {
                    return generalHttpService.deleteApiData('DumpZteFlow');
                },
                clearDumpHourPrbs: function() {
                    return generalHttpService.deleteApiData('DumpHourPrb');
                },
                clearDumpHourUserses: function () {
                    return generalHttpService.deleteApiData('DumpHourUsers');
                },
                clearDumpHourCqis: function () {
                    return generalHttpService.deleteApiData('DumpHourCqi');
                },

                dumpHuaweiItem: function() {
                    return generalHttpService.putApiData('DumpHuaweiFlow', {});
                },
                dumpHuaweiCqiItem: function() {
                    return generalHttpService.putApiData('DumpHuaweiCqi', {});
                },
                dumpZteItem: function() {
                    return generalHttpService.putApiData('DumpZteFlow', {});
                },
                dumpHourPrb: function () {
                    return generalHttpService.putApiData('DumpHourPrb', {});
                },
                dumpHourUserses: function () {
                    return generalHttpService.putApiData('DumpHourUsers', {});
                },
                dumpHourCqis: function () {
                    return generalHttpService.putApiData('DumpHourCqi', {});
                },

                queryHuaweiStat: function(index) {
                    return generalHttpService.getApiData('DumpHuaweiFlow',
                        {
                            index: index
                        });
                },
                queryFlowDumpHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            begin: begin,
                            end: end
                        });
                },
                queryHourDumpHistory: function (begin, end) {
                    return generalHttpService.getApiData('DumpHour',
                        {
                            begin: begin,
                            end: end
                        });
                },
                dumpTownStats: function(statDate) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            statDate: statDate
                        });
                },
                dumpTownHourStats: function (statDate) {
                    return generalHttpService.getApiData('DumpHour',
                        {
                            statDate: statDate
                        });
                },
                dumpTownCqis: function(statDate) {
                    return generalHttpService.getApiData('DumpFlow',
                        {
                            statTime: statDate
                        });
                }
            };
        })
    .factory('flowService',
        function(generalHttpService) {
            return {
                queryCellFlowByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('FlowQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        begin: begin,
                        end: end
                    });
                },
                queryCellRrcByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('RrcQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        begin: begin,
                        end: end
                    });
                },
                queryAverageFlowByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('FlowQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        beginDate: begin,
                        endDate: end
                    });
                },
                queryAverageRrcByDateSpan: function(eNodebId, sectorId, begin, end) {
                    return generalHttpService.getApiData('RrcQuery',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId,
                        beginDate: begin,
                        endDate: end
                    });
                },
                queryENodebGeoFlowByDateSpan: function(begin, end, frequency) {
                    return generalHttpService.getApiData('ENodebFlow',
                    {
                        begin: begin,
                        end: end,
                        frequency: frequency
                    });
                }
            };
        })
    .factory('alarmsService',
        function(generalHttpService) {
            return {
                queryENodebAlarmsByDateSpanAndLevel: function(eNodebId, begin, end, level) {
                    return generalHttpService.getApiData('Alarms',
                    {
                        eNodebId: eNodebId,
                        begin: begin,
                        end: end,
                        level: level
                    });
                },
                queryMicroItems: function() {
                    return generalHttpService.getApiData('MicroAmplifier', {});
                },
                queryGridClusters: function(theme) {
                    return generalHttpService.getApiData('GridCluster',
                    {
                        theme: theme
                    });
                },
                queryGridClusterRange: function(theme, west, east, south, north) {
                    return generalHttpService.getApiData('GridCluster',
                    {
                        theme: theme,
                        west: west,
                        east: east,
                        south: south,
                        north: north
                    });
                },
                queryClusterGridKpis: function(points) {
                    return generalHttpService.postApiData('GridCluster', points);
                },
                queryClusterKpi: function(points) {
                    return generalHttpService.putApiData('GridCluster', points);
                },
                queryDpiGridKpi: function(x, y) {
                    return generalHttpService.getApiData('DpiGridKpi',
                    {
                        x: x,
                        y: y
                    });
                }
            };
        })
    .factory('alarmImportService',
        function(generalHttpService, alarmsService) {
            return {
                queryDumpHistory: function(begin, end) {
                    return generalHttpService.getApiData('DumpAlarm',
                    {
                        begin: begin,
                        end: end
                    });
                },
                updateTownCoverageStats: function(statDate) {
                    return generalHttpService.getApiData('DumpCoverage',
                    {
                        statDate: statDate
                    });
                },
                queryDumpItems: function() {
                    return generalHttpService.getApiData('DumpAlarm', {});
                },
                queryCoverageDumpItems: function () {
                    return generalHttpService.getApiData('DumpCoverage', {});
                },
                queryZhangshangyouQualityDumpItems: function () {
                    return generalHttpService.getApiData('DumpZhangshangyouQuality', {});
                },
                queryZhangshangyouCoverageDumpItems: function () {
                    return generalHttpService.getApiData('DumpZhangshangyouCoverage', {});
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpAlarm', {});
                },
                dumpSingleCoverageItem: function () {
                    return generalHttpService.putApiData('DumpCoverage', {});
                },
                dumpSingleZhangshangyouQualityItem: function () {
                    return generalHttpService.putApiData('DumpZhangshangyouQuality', {});
                },
                dumpSingleZhangshangyouCoverageItem: function () {
                    return generalHttpService.putApiData('DumpZhangshangyouCoverage', {});
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('DumpAlarm');
                },
                clearCoverageImportItems: function () {
                    return generalHttpService.deleteApiData('DumpCoverage');
                },
                clearZhangshangyouQualityImportItems: function () {
                    return generalHttpService.deleteApiData('DumpZhangshangyouQuality');
                },
                clearZhangshangyouCoverageItems: function () {
                    return generalHttpService.deleteApiData('DumpZhangshangyouCoverage');
                },
                updateHuaweiAlarmInfos: function(cellDef) {
                    return generalHttpService.postApiData('Alarms', cellDef);
                },
                updateClusterKpi: function(stat, callback) {
                    alarmsService.queryClusterKpi(stat.gridPoints).then(function(result) {
                        stat.rsrp = result.rsrp;
                        stat.weakRate = result.weakCoverageRate;
                        stat.bestLongtitute = result.longtitute;
                        stat.bestLattitute = result.lattitute;
                        stat.x = result.x;
                        stat.y = result.y;
                        if (callback) {
                            callback(stat);
                        }

                    });
                }
            };
        });
angular.module('region.authorize', ['app.core'])
    .constant('roleDistrictDictionary', [
        {
            role: "顺德管理",
            district: "顺德"
        }, {
            role: "南海管理",
            district: "南海"
        }, {
            role: "禅城管理",
            district: "禅城"
        }, {
            role: "三水管理",
            district: "三水"
        }, {
            role: "高明管理",
            district: "高明"
        }
    ])
    .factory('authorizeService', function(generalHttpService, roleDistrictDictionary) {
        return {
            queryCurrentUserInfo: function() {
                return generalHttpService.getApiData('CurrentUser', {});
            },
            queryCurrentUserName: function() {
                return generalHttpService.getApiDataWithHeading('CurrentUserName', {});
            },
            queryAllUsers: function() {
                return generalHttpService.getApiDataWithHeading('ApplicationUsers', {});
            },
            queryRolesInUser: function(userName) {
                return generalHttpService.getApiDataWithHeading('ApplicationUsers', {
                    userName: userName
                });
            },
            queryCandidateRolesInUser: function(userName) {
                return generalHttpService.getApiDataWithHeading('ManageUsers', {
                    userName: userName
                });
            },
            queryEmailConfirmed: function(name) {
                return generalHttpService.getMvcData('/Manage/EmailHasBeenConfirmed', {
                    userName: name
                });
            },
            updateRoleList: function() {
                return generalHttpService.getApiDataWithHeading('ApplicationRoles', {});
            },
            addRole: function(name) {
                return generalHttpService.getApiDataWithHeading('CreateRole', {
                    roleName: name
                });
            },
            deleteRole: function(name) {
                return generalHttpService.getApiDataWithHeading('DeleteRole', {
                    roleName: name
                });
            },
            assignRoleInUser: function(userName, roleName) {
                return generalHttpService.getApiDataWithHeading('ApplicationRoles', {
                    userName: userName,
                    roleName: roleName
                });
            },
            releaseRoleInUser: function(userName, roleName) {
                return generalHttpService.getApiDataWithHeading('ManageRoles', {
                    userName: userName,
                    roleName: roleName
                });
            },
            changePassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ChangePassword', input);
            },
            forgotPassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ForgotPassword', input);
            },
            resetPassword: function(input) {
                return generalHttpService.postMvcData('/Manage/ResetPassword', input);
            },
            addPhoneNumber: function(input) {
                return generalHttpService.postMvcData('/Manage/AddPhoneNumber', input);
            },
            verifyPhoneNumber: function(input) {
                return generalHttpService.postMvcData('/Manage/VerifyPhoneNumber', input);
            },
            removePhoneNumber: function() {
                return generalHttpService.postMvcData('/Manage/RemovePhoneNumber', {});
            },
            confirmEmail: function(input) {
                return generalHttpService.postMvcData('/Manage/ConfirmEmail', input);
            },
            queryRoleDistricts: function (roles) {
                var districts = [];
                angular.forEach(roleDistrictDictionary, function(dict) {
                    var role = _.find(roles, function(x) { return x === dict.role });
                    if (role) {
                        districts.push(dict.district);
                    }
                });
                return districts;
            }
        };
    });
angular.module('region.college', ['app.core'])
    .factory('collegeService', function(generalHttpService) {
        return {
            queryNames: function() {
                return generalHttpService.getApiData('CollegeNames', {});
            },
            queryStats: function(year) {
                return generalHttpService.getApiData('CollegeStat',
                    {
                        year: year
                    });
            },
            queryRegion: function(id) {
                return generalHttpService.getApiData('CollegeRegion/' + id, {});
            },
            queryENodebs: function(name) {
                return generalHttpService.getApiData('CollegeENodeb',
                    {
                        collegeName: name
                    });
            },
            queryBtss: function(name) {
                return generalHttpService.getApiData('CollegeBtss',
                    {
                        collegeName: name
                    });
            },
            queryCells: function(name) {
                return generalHttpService.getApiData('CollegeCells',
                    {
                        collegeName: name
                    });
            },
            queryCdmaCells: function(name) {
                return generalHttpService.getApiData('CollegeCdmaCells',
                    {
                        collegeName: name
                    });
            },
            queryLteDistributions: function(name) {
                return generalHttpService.getApiData('CollegeLteDistributions',
                    {
                        collegeName: name
                    });
            },
            queryCdmaDistributions: function(name) {
                return generalHttpService.getApiData('CollegeCdmaDistributions',
                    {
                        collegeName: name
                    });
            },
            queryRaster: function(dataType, range, begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        dataType: dataType,
                        west: range.west,
                        east: range.east,
                        south: range.south,
                        north: range.north,
                        begin: begin,
                        end: end
                    });
            },
            queryTownRaster: function(dataType, town, begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        dataType: dataType,
                        townName: town,
                        begin: begin,
                        end: end
                    });
            },
            queryCsvFileNames: function(begin, end) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        begin: begin,
                        end: end
                    });
            },
            queryCsvFileType: function(name) {
                return generalHttpService.getApiData('RasterFile',
                    {
                        csvFileName: name
                    });
            },
            queryCsvFileInfo: function(fileName) {
                return generalHttpService.getApiData('CsvFileInfo',
                    {
                        fileName: fileName
                    });
            },
            updateCsvFileDistance: function(filesInfo) {
                return generalHttpService.postApiData('CsvFileInfo', filesInfo);
            },
            updateAreaDtInfo: function(info) {
                return generalHttpService.putApiData('TownTestInfo', info);
            },
            calculateTownDtTestInfos: function(name, type) {
                return generalHttpService.getApiData('TownTestInfo',
                    {
                        csvFileName: name,
                        type: type
                    });
            },
            calculateRoadDtTestInfos: function(name, type) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        csvFileName: name,
                        type: type
                    });
            },
            queryFileTownDtTestInfo: function(fileId) {
                return generalHttpService.getApiData('TownTestInfo',
                    {
                        fileId: fileId
                    });
            },
            queryFileRoadDtTestInfo: function(fileId) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        fileId: fileId
                    });
            },
            queryRoadDtFileInfos: function(name, begin, end) {
                return generalHttpService.getApiData('RoadTestInfo',
                    {
                        roadName: name,
                        begin: begin,
                        end: end
                    });
            },
            query2GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record2G",
                    {
                        fileName: fileName
                    });
            },
            query3GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record3G",
                    {
                        fileName: fileName
                    });
            },
            queryVolteFileRecords: function(fileName) {
                return generalHttpService.getApiData("RecordVolte",
                    {
                        fileName: fileName
                    });
            },
            query4GFileRecords: function(fileName) {
                return generalHttpService.getApiData("Record4G",
                    {
                        fileName: fileName
                    });
            }
        };
    })
    .factory('collegeQueryService', function(generalHttpService) {
        return {
            queryAll: function() {
                return generalHttpService.getApiData('CollegeQuery', {});
            },
            queryByName: function(name) {
                return generalHttpService.getApiData('CollegeNames', {
                    name: name
                });
            },
            queryCollegeById: function(id) {
                return generalHttpService.getApiData('CollegeQuery',
                {
                    id: id
                });
            },
            queryByNameAndYear: function(name, year) {
                return generalHttpService.getApiData('CollegeQuery', {
                    name: name,
                    year: year
                });
            },
            queryYearList: function(year) {
                return generalHttpService.getApiData('CollegeYear', {
                    year: year
                });
            },
            saveYearInfo: function(info) {
                return generalHttpService.postApiData('CollegeQuery', info);
            },
            constructCollegeInfo: function(info) {
                return generalHttpService.postApiDataWithHeading('CollegeStat', info);
            },
            saveCollegeCells: function(container) {
                return generalHttpService.postApiData('CollegeCellContainer', container);
            },
            queryCollegeCellSectors: function(collegeName) {
                return generalHttpService.getApiData('CollegeCellContainer', {
                    collegeName: collegeName
                });
            },
            saveCollegeCdmaCells: function(container) {
                return generalHttpService.postApiData('CollegeCdmaCellContainer', container);
            },
            queryCollegeCdmaCellSectors: function(collegeName) {
                return generalHttpService.getApiData('CollegeCdmaCellContainer', {
                    collegeName: collegeName
                });
            },
            saveCollegeENodebs: function(container) {
                return generalHttpService.postApiData('CollegeENodeb', container);
            },
            saveCollegeBtss: function(container) {
                return generalHttpService.postApiData('CollegeBtss', container);
            },
            saveCollege3GTest: function(view) {
                return generalHttpService.postApiData('College3GTest', view);
            },
            saveCollege4GTest: function(view) {
                return generalHttpService.postApiData('College4GTest', view);
            },
            queryCollege3GTestList: function(begin, end, name) {
                return generalHttpService.getApiData('College3GTest', {
                    begin: begin,
                    end: end,
                    name: name
                });
            },
            queryCollege4GTestList: function(begin, end, name) {
                return generalHttpService.getApiData('College4GTest', {
                    begin: begin,
                    end: end,
                    name: name
                });
            },
            queryCollegeFlow: function(collegeName, begin, end) {
                return generalHttpService.getApiData('CollegeFlow', {
                    collegeName: collegeName,
                    begin: begin,
                    end: end
                });
            },
            queryHotSpotFlow: function (name, begin, end) {
                return generalHttpService.getApiData('HotSpotFlow', {
                    name: name,
                    begin: begin,
                    end: end
                });
            },
            queryCollegeDateFlows: function(collegeName, begin, end) {
                return generalHttpService.getApiData('CollegeFlow', {
                    collegeName: collegeName,
                    beginDate: begin,
                    endDate: end
                });
            },
            retrieveDateCollegeFlowStats: function (statDate) {
                return generalHttpService.getApiData('CollegeFlow',
                    {
                        statDate: statDate
                    });
            }
        };
    })
    .factory('collegeDtService', function(collegeService) {
        var queryRange = function(info) {
            return {
                west: info.centerX - 0.02,
                east: info.centerX + 0.02,
                south: info.centerY - 0.02,
                north: info.centerY + 0.03
            }
        };
        return {
            updateFileInfo: function(info, begin, end) {
                var range = queryRange(info);
                collegeService.queryRaster('2G', range, begin, end).then(function(files) {
                    info.file2Gs = files;
                });
                collegeService.queryRaster('3G', range, begin, end).then(function(files) {
                    info.file3Gs = files;
                });
                collegeService.queryRaster('4G', range, begin, end).then(function(files) {
                    info.file4Gs = files;
                });
            },
            queryRaster: function(center, type, begin, end, callback) {
                var range = queryRange(center);
                collegeService.queryRaster(type, range, begin, end).then(function(files) {
                    callback(files);
                });
            },
            default3GTestView: function(collegeName, place, tester) {
                return {
                    testTime: new Date(),
                    collegeName: collegeName,
                    place: place,
                    tester: tester,
                    downloadRate: 1024,
                    accessUsers: 23,
                    minRssi: -109,
                    maxRssi: -99,
                    vswr: 1.11
                };
            },
            default4GTestView: function(collegeName, place, tester) {
                return {
                    testTime: new Date(),
                    collegeName: collegeName,
                    place: place,
                    tester: tester,
                    downloadRate: 38024,
                    uploadRate: 21024,
                    accessUsers: 33,
                    rsrp: -109,
                    sinr: 12,
                    cellName: "",
                    pci: 0
                };
            }
        };
    });
angular.module('region.precise', ['app.core'])
    .factory('workitemService',
        function(generalHttpService) {
            return {
                queryWithPaging: function(state, type) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        'statCondition': state,
                        'typeCondition': type
                    });
                },
                queryWithPagingByDistrict: function(state, type, district) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        statCondition: state,
                        typeCondition: type,
                        district: district
                    });
                },
                querySingleItem: function(serialNumber) {
                    return generalHttpService.getApiData('WorkItem',
                    {
                        serialNumber: serialNumber
                    });
                },
                signIn: function(serialNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        signinNumber: serialNumber
                    });
                },
                queryChartData: function(chartType) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        chartType: chartType
                    });
                },
                updateSectorIds: function() {
                    return generalHttpService.putApiData('WorkItem', {});
                },
                feedback: function(message, serialNumber) {
                    return generalHttpService.postApiDataWithHeading('WorkItem',
                    {
                        message: message,
                        serialNumber: serialNumber
                    });
                },
                finish: function(comments, finishNumber) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        finishNumber: finishNumber,
                        comments: comments
                    });
                },
                queryByENodebId: function(eNodebId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId
                    });
                },
                queryByCellId: function(eNodebId, sectorId) {
                    return generalHttpService.getApiDataWithHeading('WorkItem',
                    {
                        eNodebId: eNodebId,
                        sectorId: sectorId
                    });
                },
                queryCurrentMonth: function() {
                    return generalHttpService.getApiData('WorkItemCurrentMonth', {});
                },
                constructPreciseItem: function(cell, begin, end) {
                    return generalHttpService.postApiDataWithHeading('PreciseWorkItem',
                    {
                        view: cell,
                        begin: begin,
                        end: end
                    });
                }
            };
        })
    .factory('dumpWorkItemService',
        function(generalHttpService) {
            return {
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('DumpWorkItem', {});
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('DumpWorkItem');
                },
                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('DumpWorkItem', {});
                }
            };
        })
    .factory('preciseWorkItemService',
        function(generalHttpService) {
            return {
                queryByDateSpanDistrict: function(begin, end, district) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end,
                        district: district
                    });
                },
                queryByDateSpan: function(begin, end) {
                    return generalHttpService.getApiData('PreciseWorkItem',
                    {
                        begin: begin,
                        end: end
                    });
                },
                queryBySerial: function(number) {
                    return generalHttpService.getApiDataWithHeading('PreciseWorkItem',
                    {
                        number: number
                    });
                },
                updateInterferenceNeighbor: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceNeighborWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateInterferenceVictim: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('InterferenceVictimWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                },
                updateCoverage: function(number, items) {
                    return generalHttpService.postApiDataWithHeading('CoverageWorkItem',
                    {
                        workItemNumber: number,
                        items: items
                    });
                }
            };
        })
    .factory('preciseImportService',
        function(generalHttpService) {
            return {
                queryDumpHistroy: function(beginDate, endDate) {
                    return generalHttpService.getApiData('PreciseImport',
                    {
                        begin: beginDate,
                        end: endDate
                    });
                },
                queryTotalDumpItems: function() {
                    return generalHttpService.getApiData('PreciseImport', {});
                },
                queryTotalMrsRsrpItems: function () {
                    return generalHttpService.getApiData('MrsRsrpImport', {});
                },
                queryTotalMrsSinrUlItems: function () {
                    return generalHttpService.getApiData('MrsSinrUlImport', {});
                },
                queryTownPreciseViews: function(statTime, frequency) {
                    return generalHttpService.getApiData('TownPreciseImport',
                    {
                        statTime: statTime,
                        frequency: frequency
                    });
                },
                queryCollegePreciseViews: function(statTime) {
                    return generalHttpService.getApiData('TownPreciseImport',
                        {
                            statDate: statTime
                        });
                },
                queryTownMrsStats: function (statTime) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                        {
                            statDate: statTime
                        });
                },
                queryTownSinrUlStats: function (statTime) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            statDate: statTime
                        });
                },
                queryTopMrsStats: function (statTime) {
                    return generalHttpService.getApiData('MrsRsrpImport',
                        {
                            topDate: statTime
                        });
                },
                queryTopMrsSinrUlStats: function (statTime) {
                    return generalHttpService.getApiData('MrsSinrUlImport',
                        {
                            topDate: statTime
                        });
                },
                clearImportItems: function() {
                    return generalHttpService.deleteApiData('PreciseImport', {});
                },
                clearMrsRsrpItems: function () {
                    return generalHttpService.deleteApiData('MrsRsrpImport', {});
                },
                clearMrsSinrUlItems: function () {
                    return generalHttpService.deleteApiData('MrsSinrUlImport', {});
                },
                dumpTownItems: function(
                    views, collegeStats, views800, views1800, views2100, mrsStats, mrsSinrUls
                ) {
                    return generalHttpService.postApiData('TownPreciseImport',
                    {
                        views: views,
                        collegeStats: collegeStats,
                        views800: views800,
                        views1800: views1800,
                        views2100: views2100,
                        mrsRsrps: mrsStats,
                        mrsSinrUls: mrsSinrUls
                    });
                },
                dumpTownAgpsItems: function(views) {
                    return generalHttpService.postApiData('MrGrid',
                    {
                        views: views
                    });
                },
                dumpSingleItem: function() {
                    return generalHttpService.putApiData('PreciseImport', {});
                },
                dumpSingleMrsRsrpItem: function () {
                    return generalHttpService.putApiData('MrsRsrpImport', {});
                },
                dumpSingleMrsSinrUlItem: function () {
                    return generalHttpService.putApiData('MrsSinrUlImport', {});
                },
                updateMongoItems: function(statDate) {
                    return generalHttpService.getApiData('PreciseMongo',
                    {
                        statDate: statDate
                    });
                }
            };
        })
    .factory('cellPreciseService',
        function(generalHttpService) {
            return {
                queryDataSpanKpi: function(begin, end, cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'begin': begin,
                        'end': end,
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                },
                queryOneWeekKpi: function(cellId, sectorId) {
                    return generalHttpService.getApiData('PreciseStat',
                    {
                        'cellId': cellId,
                        'sectorId': sectorId
                    });
                }
            };
        })
    .factory('appRegionService',
        function(generalHttpService) {
            return {
                initializeCities: function() {
                    return generalHttpService.getApiData('CityList', {});
                },
                queryDistricts: function(cityName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName
                    });
                },
                queryDistrictInfrastructures: function(cityName) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName
                    });
                },
                queryDistrictIndoorCells: function(cityName) {
                    return generalHttpService.getApiData('DistrictIndoorCells',
                    {
                        city: cityName
                    });
                },
                queryDistrictBandCells: function(cityName) {
                    return generalHttpService.getApiData('DistrictBandCells',
                    {
                        city: cityName
                    });
                },
                queryTowns: function(cityName, districtName) {
                    return generalHttpService.getApiData('CityList',
                    {
                        city: cityName,
                        district: districtName
                    });
                },
                queryTownInfrastructures: function(cityName, districtName) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName,
                        district: districtName
                    });
                },
                queryTownLteCount: function(cityName, districtName, townName, isIndoor) {
                    return generalHttpService.getApiData('RegionStats',
                    {
                        city: cityName,
                        district: districtName,
                        town: townName,
                        isIndoor: isIndoor
                    });
                },
                queryTown: function(city, district, town) {
                    return generalHttpService.getApiData('Town',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryTownBoundaries: function(city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
                },
                isInTownBoundary: function(longtitute, lattitute, city, district, town) {
                    return generalHttpService.getApiData('TownBoundary',
                    {
                        longtitute: longtitute,
                        lattitute: lattitute,
                        city: city,
                        district: district,
                        town: town
                    });
                },
                queryAreaBoundaries: function() {
                    return generalHttpService.getApiData('AreaBoundary', {});
                },
                queryENodebTown: function(eNodebId) {
                    return generalHttpService.getApiData('Town',
                    {
                        eNodebId: eNodebId
                    });
                },
                accumulateCityStat: function(stats, cityName) {
                    var cityStat = {
                        district: cityName,
                        totalLteENodebs: 0,
                        totalLteCells: 0,
                        totalNbIotCells: 0,
                        totalCdmaBts: 0,
                        totalCdmaCells: 0
                    };
                    angular.forEach(stats,
                        function(stat) {
                            cityStat.totalLteENodebs += stat.totalLteENodebs;
                            cityStat.totalLteCells += stat.totalLteCells;
                            cityStat.totalNbIotCells += stat.totalNbIotCells;
                            cityStat.totalCdmaBts += stat.totalCdmaBts;
                            cityStat.totalCdmaCells += stat.totalCdmaCells;
                        });
                    stats.push(cityStat);
                },
                getTownFlowStats: function(statDate, frequency) {
                    return generalHttpService.getApiData('TownFlow',
                    {
                        statDate: statDate,
                        frequency: frequency
                    });
                },
                getCurrentDateTownFlowStats: function (statDate, frequency) {
                    return generalHttpService.getApiData('TownFlow',
                        {
                            currentDate: statDate,
                            frequency: frequency
                        });
                },
                updateTownFlowStat: function(stat) {
                    return generalHttpService.postApiData('TownFlow', stat);
                }
            };
        });
angular.module('region.network', ['app.core'])
    .factory('networkElementService', function (generalHttpService) {
        return {
            queryCellInfo: function (cellId, sectorId) {
                return generalHttpService.getApiData('Cell', {
                    eNodebId: cellId,
                    sectorId: sectorId
                });
            },
            queryLteRruFromCellName: function (cellName) {
                return generalHttpService.getApiData('LteRru', {
                    cellName: cellName
                });
            },
            queryLteRrusFromPlanNum: function (planNum) {
                return generalHttpService.getApiData('CellStation', {
                    planNum: planNum
                });
            },
            queryCellInfosInOneENodeb: function (eNodebId) {
                return generalHttpService.getApiData('Cell', {
                    eNodebId: eNodebId
                });
            },
            queryCellInfosInOneENodebUse: function (eNodebId) {
                return generalHttpService.getApiData('CellInUse', {
                    eNodebId: eNodebId
                });
            },
            queryCellViewsInOneENodeb: function (eNodebId) {
                return generalHttpService.getApiData('Cell', {
                    cellId: eNodebId
                });
            },
            queryCellStationInfoByRruName: function (rruName) {
                return generalHttpService.getApiData('CellStation', {
                    rruName: rruName
                });
            },
            queryCellSectorIds: function (name) {
                return generalHttpService.getApiData('Cell', {
                    eNodebName: name
                });
            },
            queryCdmaSectorIds: function (name) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsName: name
                });
            },
            queryCdmaCellViews: function (name) {
                return generalHttpService.getApiData('CdmaCell', {
                    name: name
                });
            },
            queryCdmaCellInfo: function (btsId, sectorId) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId,
                    sectorId: sectorId
                });
            },
            queryCdmaCellInfoWithType: function (btsId, sectorId, cellType) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId,
                    sectorId: sectorId,
                    cellType: cellType
                });
            },
            queryCdmaCellInfosInOneBts: function (btsId) {
                return generalHttpService.getApiData('CdmaCell', {
                    btsId: btsId
                });
            },
            queryENodebInfo: function (eNodebId) {
                return generalHttpService.getApiData('ENodeb', {
                    eNodebId: eNodebId
                });
            },
            queryENodebsInOneTown: function (city, district, town) {
                return generalHttpService.getApiData('ENodeb', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryENodebsInOneDistrict: function (city, district) {
                return generalHttpService.getApiData('ENodeb', {
                    city: city,
                    district: district
                });
            },
            queryRangeENodebs: function (container) {
                return generalHttpService.postApiData('ENodeb', container);
            },
            queryENodebsByGeneralName: function (name) {
                return generalHttpService.getApiData('ENodeb', {
                    name: name
                });
            },
            queryInRangeENodebs: function(west, east, south, north) {
                return generalHttpService.getApiData('ENodeb', {
                    west: west,
                    east: east,
                    south: south,
                    north: north
                });
            },
            queryENodebByPlanNum: function (planNum) {
                return generalHttpService.getApiData('ENodebQuery', {
                    planNum: planNum
                });
            },
            queryENodebsByTownArea: function(city, district, town) {
                return generalHttpService.getApiData('ENodebQuery',
                {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryBtssByTownArea: function (city, district, town) {
                return generalHttpService.getApiData('BtsQuery',
                    {
                        city: city,
                        district: district,
                        town: town
                    });
            },
            updateENodebTownInfo: function(view) {
                return generalHttpService.getApiData('ENodebQuery',
                    {
                        eNodebId: view.eNodebId,
                        townId: view.townId
                    });
            },
            updateBtsTownInfo: function (view) {
                return generalHttpService.getApiData('BtsQuery',
                    {
                        btsId: view.btsId,
                        townId: view.townId
                    });
            },
            queryStationByENodeb: function (eNodebId, planNum) {
                return generalHttpService.getApiData('ENodebStation', {
                    eNodebId: eNodebId,
                    planNum: planNum
                });
            },
            queryENodebsInOneTownUse: function (city, district, town) {
                return generalHttpService.getApiData('ENodebInUse', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryDistributionsInOneDistrict: function (district) {
                return generalHttpService.getApiData('Distribution', {
                    district: district
                });
            },
            queryENodebsByGeneralNameInUse: function (name) {
                return generalHttpService.getApiData('ENodebInUse', {
                    name: name
                });
            },
            queryBtsInfo: function (btsId) {
                return generalHttpService.getApiData('Bts', {
                    btsId: btsId
                });
            },
            queryBtssInOneTown: function (city, district, town) {
                return generalHttpService.getApiData('Bts', {
                    city: city,
                    district: district,
                    town: town
                });
            },
            queryBtssByGeneralName: function (name) {
                return generalHttpService.getApiData('Bts', {
                    name: name
                });
            },
            queryCellSectors: function (cells) {
                return generalHttpService.postApiData('Cell', {
                    views: cells
                });
            },
            queryRangeSectors: function (range, excludedIds) {
                return generalHttpService.postApiData('SectorView', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north,
                    excludedCells: excludedIds
                });
            },
            queryRangeCells: function (range) {
                return generalHttpService.getApiData('Cell', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangePlanningSites: function (range) {
                return generalHttpService.getApiData('PlanningSiteRange', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeDistributions: function (range) {
                return generalHttpService.getApiData('Distribution', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeComplains: function (range) {
                return generalHttpService.getApiData('OnlineSustain', {
                    west: range.west,
                    east: range.east,
                    south: range.south,
                    north: range.north
                });
            },
            queryRangeBtss: function (container) {
                return generalHttpService.postApiData('Bts', container);
            },
            queryCellNeighbors: function (cellId, sectorId) {
                return generalHttpService.getApiData('NearestPciCell', {
                    'cellId': cellId,
                    'sectorId': sectorId
                });
            },
            querySystemNeighborCell: function (cellId, sectorId, pci) {
                return generalHttpService.getApiData('NearestPciCell', {
                    'cellId': cellId,
                    'sectorId': sectorId,
                    'pci': pci
                });
            },
            updateCellPci: function (cell) {
                return generalHttpService.postApiData('NearestPciCell', cell);
            },
            queryNearestCells: function (eNodebId, sectorId, pci) {
                return generalHttpService.getApiData('Cell', {
                    'eNodebId': eNodebId,
                    'sectorId': sectorId,
                    'pci': pci
                });
            },
            queryNearestCellsWithFrequency: function (eNodebId, sectorId, pci, frequency) {
                return generalHttpService.getApiData('Cell', {
                    'eNodebId': eNodebId,
                    'sectorId': sectorId,
                    'pci': pci,
                    'frequency': frequency
                });
            },
            updateNeighbors: function (cellId, sectorId, pci, nearestCellId, nearestSectorId) {
                return generalHttpService.putApiData('NearestPciCell', {
                    cellId: cellId,
                    sectorId: sectorId,
                    pci: pci,
                    nearestCellId: nearestCellId,
                    nearestSectorId: nearestSectorId
                });
            },
            queryOutdoorCellSites: function (city, district) {
                return generalHttpService.getApiData('OutdoorCellSite', {
                    city: city,
                    district: district
                });
            },
            queryVolteCellSites: function (city, district) {
                return generalHttpService.getApiData('VoLteCellSite', {
                    city: city,
                    district: district
                });
            },
            queryNbIotCellSites: function (city, district) {
                return generalHttpService.getApiData('NbIotCellSite', {
                    city: city,
                    district: district
                });
            },
            queryIndoorCellSites: function (city, district) {
                return generalHttpService.getApiData('IndoorCellSite', {
                    city: city,
                    district: district
                });
            },
            queryPlanningSites: function (city, district) {
                return generalHttpService.getApiData('PlanningSite', {
                    city: city,
                    district: district
                });
            },
            queryOpenningSites: function (city, district) {
                return generalHttpService.getApiData('OpenningSite', {
                    city: city,
                    district: district
                });
            },
            queryOpennedSites: function (city, district) {
                return generalHttpService.getApiData('OpennedSite', {
                    city: city,
                    district: district
                });
            }
        };
    })
    .factory('neighborImportService', function (geometryService, networkElementService) {
        return {
            updateSuccessProgress: function (result, progressInfo, callback) {
                if (result) {
                    progressInfo.totalSuccessItems += 1;
                } else {
                    progressInfo.totalFailItem += 1;
                }
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    callback();
                } else {
                    progressInfo.totalDumpItems = 0;
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            },
            updateFailProgress: function (progressInfo, callback) {
                progressInfo.totalFailItems += 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    callback();
                } else {
                    progressInfo.totalDumpItems = 0;
                    progressInfo.totalSuccessItems = 0;
                    progressInfo.totalFailItems = 0;
                }
            },
            updateCellRruInfo: function (supplementCells, settings) {
                angular.forEach(settings.dstCells, function (dstCell) {
                    var i;
                    for (i = 0; i < settings.cells.length; i++) {
                        if (dstCell.cellName === settings.cells[i].eNodebName + '-' + settings.cells[i].sectorId) {
                            break;
                        }
                    }
                    if (i === settings.cells.length) {
                        dstCell.distance = geometryService.getDistance(settings.lattitute, settings.longtitute, dstCell.lattitute, dstCell.longtitute);
                        networkElementService.queryLteRruFromCellName(dstCell.cellName).then(function (rru) {
                            dstCell.rruName = rru ? rru.rruName : '';
                            supplementCells.push(dstCell);
                        });
                    }
                });
            },
            updateENodebRruInfo: function (supplementCells, settings) {
                angular.forEach(settings.dstCells, function (item) {
                    var i;
                    for (i = 0; i < settings.cells.length; i++) {
                        if (settings.cells[i].eNodebId === item.eNodebId && settings.cells[i].sectorId === item.sectorId) {
                            break;
                        }
                    }
                    if (i === settings.cells.length) {
                        networkElementService.queryCellInfo(item.eNodebId, item.sectorId).then(function (view) {
                            view.distance = geometryService.getDistance(settings.lattitute, settings.longtitute, item.lattitute, item.longtitute);
                            networkElementService.queryLteRruFromCellName(view.cellName).then(function (rru) {
                                view.rruName = rru ? rru.rruName : '';
                                supplementCells.push(view);
                            });
                        });
                    }
                });
            },
            generateRange: function(range, center, coors) {
                return {
                    west: range.west + center.X - coors.x,
                    east: range.east + center.X - coors.x,
                    south: range.south + center.Y - coors.y,
                    north: range.north + center.Y - coors.y
                };
            },
            generateRangeWithExcludedIds: function(range, center, coors, ids) {
                return {
                    west: range.west + center.X - coors.x,
                    east: range.east + center.X - coors.x,
                    south: range.south + center.Y - coors.y,
                    north: range.north + center.Y - coors.y,
                    excludedIds: ids
                };
            }
        }
    })
    .factory('dumpProgress', function(generalHttpService, appFormatService) {
        var serviceInstance = {};

        serviceInstance.clear = function(progressInfo) {
            generalHttpService.deleteApiData('DumpInterference').then(function() {
                progressInfo.totalDumpItems = 0;
                progressInfo.totalSuccessItems = 0;
                progressInfo.totalFailItems = 0;
            });
        };

        serviceInstance.dump = function(actionUrl, progressInfo) {
            var self = serviceInstance;
            generalHttpService.putApiData(actionUrl, {}).then(function(result) {
                if (result === true) {
                    progressInfo.totalSuccessItems = progressInfo.totalSuccessItems + 1;
                } else {
                    progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                }
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            }, function() {
                progressInfo.totalFailItems = progressInfo.totalFailItems + 1;
                if (progressInfo.totalSuccessItems + progressInfo.totalFailItems < progressInfo.totalDumpItems) {
                    self.dump(actionUrl, progressInfo);
                } else {
                    self.clear(actionUrl, progressInfo);
                }
            });
        };

        serviceInstance.dumpMongo = function(stat) {
            return generalHttpService.postApiData('DumpInterference', stat);
        };

        serviceInstance.dumpCellStat = function(stat) {
            return generalHttpService.postApiData('DumpCellStat', stat);
        };

        serviceInstance.resetProgress = function(begin, end) {
            return generalHttpService.getApiData('DumpInterference', {
                'begin': begin,
                'end': end
            });
        };

        serviceInstance.queryExistedItems = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('DumpInterference', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                date: appFormatService.getDateString(date, 'yyyy-MM-dd')
            });
        };

        serviceInstance.queryMongoItems = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('InterferenceMongo', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                date: date
            });
        };

        serviceInstance.queryNeighborMongoItem = function(eNodebId, sectorId, neighborPci, date) {
            return generalHttpService.getApiData('InterferenceMatrix', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                neighborPci: neighborPci,
                date: date
            });
        };

        serviceInstance.queryMrsRsrpItem = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('MrsRsrp', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                statDate: date
            });
        };

        serviceInstance.queryMrsTadvItem = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('MrsTadv', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                statDate: date
            });
        };

        serviceInstance.queryMrsPhrItem = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('MrsPhr', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                statDate: date
            });
        };

        serviceInstance.queryMrsTadvRsrpItem = function(eNodebId, sectorId, date) {
            return generalHttpService.getApiData('MrsTadvRsrp', {
                eNodebId: eNodebId,
                sectorId: sectorId,
                statDate: date
            });
        };

        return serviceInstance;
    })
    .factory('dumpPreciseService', function(dumpProgress, networkElementService, authorizeService) {
        var serviceInstance = {};
        serviceInstance.dumpAllRecords = function(records, outerIndex, innerIndex, eNodebId, sectorId, queryFunc) {
            if (outerIndex >= records.length) {
                if (queryFunc !== undefined)
                    queryFunc();
            } else {
                var subRecord = records[outerIndex];
                if (subRecord.existedRecords < subRecord.mongoRecords.length && innerIndex < subRecord.mongoRecords.length) {
                    var stat = subRecord.mongoRecords[innerIndex];
                    networkElementService.querySystemNeighborCell(eNodebId, sectorId, stat.neighborPci).then(function(neighbor) {
                        if (neighbor) {
                            stat.destENodebId = neighbor.nearestCellId;
                            stat.destSectorId = neighbor.nearestSectorId;
                        } else {
                            stat.destENodebId = 0;
                            stat.destSectorId = 0;
                        }
                        dumpProgress.dumpMongo(stat).then(function() {
                            serviceInstance.dumpAllRecords(records, outerIndex, innerIndex + 1, eNodebId, sectorId, queryFunc);
                        });
                    });
                } else
                    serviceInstance.dumpAllRecords(records, outerIndex + 1, 0, eNodebId, sectorId, queryFunc);
            }

        };
        serviceInstance.dumpDateSpanSingleNeighborRecords = function(cell, date, end) {
            if (date < end) {
                dumpProgress.queryNeighborMongoItem(cell.cellId, cell.sectorId, cell.neighborPci, date).then(function(result) {
                    var stat = result;
                    var nextDate = date;
                    nextDate.setDate(nextDate.getDate() + 1);
                    if (stat) {
                        stat.destENodebId = cell.neighborCellId;
                        stat.destSectorId = cell.neighborSectorId;
                        dumpProgress.dumpMongo(stat).then(function() {
                            serviceInstance.dumpDateSpanSingleNeighborRecords(cell, nextDate, end);
                        });
                    } else {
                        serviceInstance.dumpDateSpanSingleNeighborRecords(cell, nextDate, end);
                    }
                });
            } else {
                cell.finished = true;
            }
        };
        serviceInstance.generateUsersDistrict = function(city, districts, callback) {
            if (city) {
                authorizeService.queryCurrentUserName().then(function(userName) {
                    authorizeService.queryRolesInUser(userName).then(function (roles) {
                        var roleDistricts = authorizeService.queryRoleDistricts(roles);
                        angular.forEach(roleDistricts, function (district, $index) {
                            districts.push(district);
                            if (callback) {
                                callback(district, $index);
                            }
                        });
                    });
                });
            }
        };

        return serviceInstance;
    });
angular.module('myApp.region',
[
    'region.basic', 'region.mongo', 'region.kpi', 'region.import', 'region.authorize', 'region.college',
    'region.network', 'region.precise'
]);