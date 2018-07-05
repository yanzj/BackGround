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
                getFixingStationById: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Fixing/single/',
                        {
                        "id": id
                    });
                },
                getCommonStationById: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/single/',
                        {
                        "id": id
                    });
                },
                getCheckDetailsById: function (id) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/Checking/details/', {
                        "id": id
                    });
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
                
                getCommonStations: function (name, type, areaName, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "stationName": name,
                            "type": type,
                            "areaName": areaName
                        });
                },
                getCommonStationIdAdd: function (distinct, type) {
                    return generalHttpService.postPhpUrlData(appUrlService.getPhpHost() +
                        'LtePlatForm/lte/index.php/StationCommon/generateId',
                        {
                        "distinct": distinct,
                        "type": type
                    });
                },
                getAllCommonStations: function(type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/StationCommon/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "type": type
                        });
                },
                deleteCommonStation: function(stationId) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/StationCommon/delete',
                        {
                            "idList": stationId
                        });
                },
                getCheckingStation: function(areaName, status, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Checking/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "areaName": areaName,
                            "status": status,
                            'type': type
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
                getFixingStation: function (areaName, status, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Fixing/search',
                        {
                        "curr_page": page,
                        "page_size": pageSize,
                        "areaName": areaName,
                        "level": status,
                        "type": type
                    });
                },
                getSpecialStations: function(recoverName, page, pageSize) {

                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Special/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "recoverName": recoverName
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
                getResourceStations: function(areaName, type, page, pageSize) {
                    return generalHttpService
                        .postPhpUrlData(appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/Resource/search',
                        {
                            "curr_page": page,
                            "page_size": pageSize,
                            "areaName": areaName,
                            "type": type
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