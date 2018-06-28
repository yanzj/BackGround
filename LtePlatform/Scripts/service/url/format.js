angular.module('app.format', [])
    .factory('appFormatService',
        function() {
            return {
                getDate: function(strDate) {
                    var date = eval('new Date(' +
                        strDate.replace(/\d+(?=-[^-]+$)/,
                            function(a) { return parseInt(a, 10) - 1; }).match(/\d+/g) +
                        ')');
                    return date;
                },
                getUTCTime: function(strDate) {
                    var date = eval('new Date(' +
                        strDate.replace(/\d+(?=-[^-]+$)/,
                            function(a) { return parseInt(a, 10) - 1; }).match(/\d+/g) +
                        ')');
                    return Date.UTC(date.getFullYear(),
                        date.getMonth() + 1,
                        date.getDate(),
                        date.getHours(),
                        date.getMinutes(),
                        date.getSeconds(),
                        0);
                },
                getDateString: function(dateTime, fmt) {
                    var o = {
                        "M+": dateTime.getMonth() + 1, //月份 
                        "d+": dateTime.getDate(), //日 
                        "h+": dateTime.getHours(), //小时 
                        "m+": dateTime.getMinutes(), //分 
                        "s+": dateTime.getSeconds(), //秒 
                        "q+": Math.floor((dateTime.getMonth() + 3) / 3), //季度 
                        "S": dateTime.getMilliseconds() //毫秒 
                    };
                    if (/(y+)/.test(fmt))
                        fmt = fmt.replace(RegExp.$1, (dateTime.getFullYear() + "").substr(4 - RegExp.$1.length));
                    for (var k in o)
                        if (o.hasOwnProperty(k))
                            if (new RegExp("(" + k + ")").test(fmt))
                                fmt = fmt.replace(RegExp.$1,
                                    (RegExp.$1.length === 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
                    return fmt;
                },
                lowerFirstLetter: function(str) {
                    return str.substring(0, 1).toLowerCase() +
                        str.substring(1);
                },
                getId: function(name) {
                    return window.document !== undefined && document.getElementById && document.getElementById(name);
                },
                queryEscapeText: function(s) {
                    if (!s) {
                        return "";
                    }
                    s = s + "";

                    // Both single quotes and double quotes (for attributes)
                    return s.replace(/['"<>&]/g,
                        function(ss) {
                            switch (ss) {
                            case "'":
                                return "&#039;";
                            case "\"":
                                return "&quot;";
                            case "<":
                                return "&lt;";
                            case ">":
                                return "&gt;";
                            case "&":
                                return "&amp;";
                            default:
                                return "";
                            }
                        });
                },
                getUrlParams: function() {
                    var urlParams = {};
                    var params = window.location.search.slice(1).split("&");

                    if (params[0]) {
                        angular.forEach(params,
                            function(param) {
                                var current = param.split("=");
                                current[0] = decodeURIComponent(current[0]);

                                // allow just a key to turn on a flag, e.g., test.html?noglobals
                                current[1] = current[1] ? decodeURIComponent(current[1]) : true;
                                if (urlParams[current[0]]) {
                                    urlParams[current[0]] = [].concat(urlParams[current[0]], current[1]);
                                } else {
                                    urlParams[current[0]] = current[1];
                                }
                            });
                    }

                    return urlParams;
                },
                searchText: function(options, matchFunction) {
                    for (var i = 0; i < options.length; i++) {
                        if (matchFunction(options[i])) {
                            return options[i];
                        }
                    }
                    return null;
                },
                searchPattern: function(options, text) {
                    for (var i = 0; i < options.length; i++) {
                        var pattern = new RegExp(options[i]);
                        if (pattern.test(text)) {
                            return options[i];
                        }
                    }
                    return null;
                },
                calculateAverages: function(data, queryFunctions) {
                    var outputs = [];
                    angular.forEach(queryFunctions,
                        function(func) {
                            outputs.push({
                                sum: 0,
                                count: 0
                            });
                        });
                    angular.forEach(data,
                        function(item) {
                            angular.forEach(queryFunctions,
                                function(func, index) {
                                    var value = func(item);
                                    if (value !== 0) {
                                        outputs[index].sum += value;
                                        outputs[index].count += 1;
                                    }
                                });
                        });
                    return outputs;
                },
                prefixInteger: function(num, length) {
                    return (Array(length).join('0') + num).slice(-length);
                },
                generateSiteGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '正式名称',
                                    value: site.formalName
                                }, {
                                    key: '规划名称',
                                    value: site.planName
                                }, {
                                    key: '规划编号',
                                    value: site.planNum
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '区域',
                                    value: site.district
                                }, {
                                    key: '镇区',
                                    value: site.town
                                }, {
                                    key: '获取日期',
                                    value: site.gottenDate
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '杆塔类型',
                                    value: site.towerType
                                }, {
                                    key: '天线高度',
                                    value: site.antennaHeight
                                }, {
                                    key: '开通日期',
                                    value: site.finishedDate
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: site.longtitute
                                }, {
                                    key: '纬度',
                                    value: site.lattitute
                                }
                            ]
                        }
                    ];
                },
                generateSiteDetailsGroups: function(site) {
                    return [
                        {
                            items: [
                                {
                                    key: '天线类型',
                                    value: site.antennaType
                                }, {
                                    key: '完工日期',
                                    value: site.completeDate
                                }, {
                                    key: '合同签订日期',
                                    value: site.contractDate
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否获取',
                                    value: site.isGotton ? '已获取' : '未获取'
                                }, {
                                    key: '受阻说明',
                                    value: site.shouzuShuoming
                                }, {
                                    key: '站点类型',
                                    value: site.siteCategory
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '站点来源',
                                    value: site.siteSource
                                }, {
                                    key: '铁塔联系人',
                                    value: site.towerContaction
                                }, {
                                    key: '铁塔盖章方案',
                                    value: site.towerScheme
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '铁塔规划需求名',
                                    value: site.towerSiteName
                                }, {
                                    key: '验收交付时间',
                                    value: site.yanshouDate
                                }
                            ]
                        }
                    ];
                },
                generateStationGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站点名称',
                                    value: station.StationName
                                }, {
                                    key: '机房名称',
                                    value: station.EngineRoom
                                }, {
                                    key: '站点类型',
                                    value: station.StationType
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '区域',
                                    value: station.AreaName
                                }, {
                                    key: '镇区',
                                    value: station.Town
                                }, {
                                    key: '安装地址',
                                    value: station.InstallAddr
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '杆塔类型',
                                    value: station.TowerType
                                }, {
                                    key: '天线高度',
                                    value: station.TowerHeight
                                }, {
                                    key: '开通日期',
                                    value: station.IntersectionDate
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '系统站址ID',
                                    value: station.SysStationId
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '机房归属',
                                    value: station.RoomAttribution
                                }, {
                                    key: '是否新建机房',
                                    value: station.IsNewRoom
                                }, {
                                    key: 'CL网是否共用天线',
                                    value: station.IsShare
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '网络类型',
                                    value: station.NetType
                                }, {
                                    key: '是否高危站点',
                                    value: station.IsDangerous
                                }, {
                                    key: '是否简易机房',
                                    value: station.IsSimple
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '属地性质',
                                    value: station.AttributionNature
                                }, {
                                    key: '是否新建铁塔',
                                    value: station.IsNewTower
                                }, {
                                    key: '铁塔归属',
                                    value: station.TowerAttribution
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '铁塔高度',
                                    value: station.TowerHeight
                                }, {
                                    key: '铁塔类型',
                                    value: station.TowerType
                                }, {
                                    key: '铁塔编号',
                                    value: station.TowerCode
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否自维',
                                    value: station.IsSelf
                                }
                            ]
                        }
                    ];
                },
                generateENodebGroups: function(station, positionColor) {
                    return [
                        {
                            items: [
                                {
                                    key: '基站名称',
                                    value: station.name
                                }, {
                                    key: '基站编号',
                                    value: station.eNodebId
                                }, {
                                    key: '城市',
                                    value: station.cityName
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '区域',
                                    value: station.districtName
                                }, {
                                    key: '镇区',
                                    value: station.townName
                                }, {
                                    key: '安装地址',
                                    value: station.address
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '规划编号',
                                    value: station.planNum
                                }, {
                                    key: '入网日期',
                                    value: station.openDate
                                }, {
                                    key: '厂家',
                                    value: station.factory
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute,
                                    color: positionColor || 'black'
                                }, {
                                    key: '纬度',
                                    value: station.lattitute,
                                    color: positionColor || 'black'
                                }, {
                                    key: '制式',
                                    value: station.divisionDuplex
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '网关',
                                    value: station.gatewayIp.addressString
                                }, {
                                    key: 'IP',
                                    value: station.ip.addressString
                                }, {
                                    key: '是否在用',
                                    value: station.isInUse
                                }
                            ]
                        }
                    ];
                },
                generateCellGroups: function(cell) {
                    return [
                        {
                            items: [
                                {
                                    key: '小区名称',
                                    value: cell.cellName
                                }, {
                                    key: '基站编号',
                                    value: cell.eNodebId
                                }, {
                                    key: 'TAC',
                                    value: cell.tac
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '频段',
                                    value: cell.bandClass
                                }, {
                                    key: '频点',
                                    value: cell.frequency
                                }, {
                                    key: '天线增益',
                                    value: cell.antennaGain
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '天线挂高',
                                    value: cell.height
                                }, {
                                    key: '方位角',
                                    value: cell.azimuth
                                }, {
                                    key: '下倾角',
                                    value: cell.downTilt
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: cell.longtitute
                                }, {
                                    key: '纬度',
                                    value: cell.lattitute
                                }, {
                                    key: '室内外',
                                    value: cell.indoor
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: 'PCI',
                                    value: cell.pci
                                }, {
                                    key: 'PRACH',
                                    value: cell.prach
                                }, {
                                    key: 'RS功率',
                                    value: cell.rsPower
                                }
                            ]
                        }
                    ];
                },
                generateSustainGroups: function(cell) {
                    return [
                        {
                            items: [
                                {
                                    key: '工单编号',
                                    value: cell.serialNumber
                                }, {
                                    key: '派单日期',
                                    value: cell.beginDate
                                }, {
                                    key: '投诉站点',
                                    value: cell.site
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉分类',
                                    value: cell.complainCategoryDescription
                                }, {
                                    key: '投诉原因',
                                    value: cell.complainReasonDescription
                                }, {
                                    key: '投诉子原因',
                                    value: cell.complainSubReasonDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉地址',
                                    value: cell.address
                                }, {
                                    key: '联系电话',
                                    value: cell.contactPhone
                                }, {
                                    key: '是否预处理',
                                    value: cell.isPreProcessedDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '城市',
                                    value: cell.city
                                }, {
                                    key: '区域',
                                    value: cell.district
                                }, {
                                    key: '镇区',
                                    value: cell.town
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: cell.longtitute
                                }, {
                                    key: '纬度',
                                    value: cell.lattitute
                                }, {
                                    key: '专家回复',
                                    value: cell.specialResponse
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉来源',
                                    value: cell.complainSourceDescription
                                }, {
                                    key: '问题描述',
                                    value: cell.issue
                                }, {
                                    key: '反馈信息',
                                    value: cell.feedbackInfo
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉现象',
                                    value: cell.phenomenon,
                                    span: 5
                                }
                            ]
                        }
                    ];
                },
                generateRruGroups: function(cell) {
                    return [
                        {
                            items: [
                                {
                                    key: '天线厂家',
                                    value: cell.antennaFactoryDescription
                                }, {
                                    key: '天线信息',
                                    value: cell.antennaInfo
                                }, {
                                    key: '天线型号',
                                    value: cell.antennaModel
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '电下倾角',
                                    value: cell.eTilt
                                }, {
                                    key: '机械下倾',
                                    value: cell.mTilt
                                }, {
                                    key: 'RRU名称',
                                    value: cell.rruName
                                }
                            ]
                        }
                    ];
                },
                generateSpecialStationGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '网管站名',
                                    value: station.enodebName
                                }, {
                                    key: '基站编号',
                                    value: station.stationId
                                }, {
                                    key: '行政区',
                                    value: station.areaName
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '归属',
                                    value: station.alongTo
                                }, {
                                    key: '基站ID',
                                    value: station.enodebId
                                }, {
                                    key: '故障',
                                    value: station.fault
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '故障分类',
                                    value: station.faultType
                                }, {
                                    key: '是否恢复',
                                    value: station.isRecover
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '反馈',
                                    value: station.feedback
                                }
                            ]
                        }
                    ];
                },
                generateSpecialIndoorGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站点名称',
                                    value: station.enodebName
                                }, {
                                    key: '行政区',
                                    value: station.areaName
                                }, {
                                    key: '割接后施主基站',
                                    value: station.afterName
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '故障类型',
                                    value: station.faultType
                                }, {
                                    key: '解决方案',
                                    value: station.solution
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }
                            ]
                        }
                    ];
                },
                generateZeroFlowGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '基站名',
                                    value: station.enodebName
                                }, {
                                    key: '地市',
                                    value: station.city
                                }, {
                                    key: '厂家',
                                    value: station.factory
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否断站',
                                    value: station.isOff
                                }, {
                                    key: '是否存在小区退服',
                                    value: station.isOut
                                }, {
                                    key: 'BBU是否交维',
                                    value: station.isIntersect
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: 'BBU是否下挂RRU',
                                    value: station.isUnderbarrel
                                }, {
                                    key: '是否已解决',
                                    value: station.isSolve
                                }, {
                                    key: '零流量原因类型',
                                    value: station.reasonType
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '计划解决时间',
                                    value: station.planDate
                                }, {
                                    key: '完成时间',
                                    value: station.finishDate
                                }, {
                                    key: '责任单位',
                                    value: station.unit
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '解决措施',
                                    value: station.solution
                                }
                            ]
                        }
                    ];
                },
                generateZeroVoiceGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '基站名',
                                    value: station.BTSName
                                }, {
                                    key: '地市',
                                    value: station.city
                                }, {
                                    key: '厂家',
                                    value: station.factory
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否断站',
                                    value: station.isOff
                                }, {
                                    key: '是否存在小区退服',
                                    value: station.isOut
                                }, {
                                    key: 'BBU是否交维',
                                    value: station.isIntersect
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: 'BBU是否下挂RRU',
                                    value: station.isUnderbarrel
                                }, {
                                    key: '是否已解决',
                                    value: station.isSolve
                                }, {
                                    key: '零话务原因类型',
                                    value: station.reasonType
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '计划解决时间',
                                    value: station.planDate
                                }, {
                                    key: 'BTSTYPE',
                                    value: station.BTSTYPE
                                }, {
                                    key: '责任单位',
                                    value: station.unit
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '解决措施',
                                    value: station.solution
                                }
                            ]
                        }
                    ];
                },
                generateFaultStationGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站点名称',
                                    value: station.enodebName
                                }, {
                                    key: '单号',
                                    value: station.orderId
                                }, {
                                    key: '告警名称',
                                    value: station.alarmName
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '故障反馈',
                                    value: station.feedback
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }
                            ]
                        }
                    ];
                },
                generateCheckingStationGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '网元编号',
                                    value: station.enodebCode
                                }, {
                                    key: '网元名称',
                                    value: station.enodebName
                                }, {
                                    key: '网格名称',
                                    value: station.areaName
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '巡检单位',
                                    value: station.checkingComp
                                }, {
                                    key: '巡检人',
                                    value: station.checkingMan
                                }, {
                                    key: '状态',
                                    value: station.status
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '巡检问题',
                                    value: station.problem
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '上次巡检时间',
                                    value: station.lastCheckTime
                                }, {
                                    key: '上次巡检问题',
                                    value: station.lastProblem
                                }
                            ]
                        }
                    ];
                },

                generateFixingStationGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站址编号',
                                    value: station.id
                                }, {
                                    key: '站址名称',
                                    value: station.name
                                }, {
                                    key: '地址',
                                    value: station.address
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '状态',
                                    value: station.status
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '需求时间',
                                    value: station.requireTime
                                }, {
                                    key: '整治人员',
                                    value: station.checkingMan
                                }, {
                                    key: '预算金额',
                                    value: station.cost
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '整治级别',
                                    value: station.level
                                }, {
                                    key: '整治计划',
                                    value: station.plan
                                }, {
                                    key: '整治内容描述',
                                    value: station.details
                                }
                            ]
                        }
                    ];
                },

                generateCdmaCellGroups: function(cell) {
                    return [
                        {
                            items: [
                                {
                                    key: '基站名称',
                                    value: cell.btsName
                                }, {
                                    key: '基站编号',
                                    value: cell.btsId
                                }, {
                                    key: '扇区编号',
                                    value: cell.sectorId
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '1X频点',
                                    value: cell.onexFrequencyList
                                }, {
                                    key: 'DO频点',
                                    value: cell.evdoFrequencyList
                                }, {
                                    key: '天线增益',
                                    value: cell.antennaGain
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '天线挂高',
                                    value: cell.height
                                }, {
                                    key: '方位角',
                                    value: cell.azimuth
                                }, {
                                    key: '下倾角',
                                    value: cell.downTilt
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: cell.longtitute
                                }, {
                                    key: '纬度',
                                    value: cell.lattitute
                                }, {
                                    key: '室内外',
                                    value: cell.indoor
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: 'PN',
                                    value: cell.pn
                                }, {
                                    key: 'LAC',
                                    value: cell.lac
                                }, {
                                    key: '小区编号',
                                    value: cell.cellId
                                }
                            ]
                        }
                    ];
                },
                generateDistributionGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '室分名称',
                                    value: station.name
                                }, {
                                    key: '城市',
                                    value: station.city
                                }, {
                                    key: '区域',
                                    value: station.district
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '站点编号',
                                    value: station.stationNum
                                }, {
                                    key: '地址',
                                    value: station.address
                                }, {
                                    key: '维护单位',
                                    value: station.server
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否为LTE RRU',
                                    value: station.isLteRru ? '是' : '否'
                                }, {
                                    key: 'LTE基站编号',
                                    value: station.eNodebId
                                }, {
                                    key: 'LTE小区编号',
                                    value: station.lteSectorId
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否为CDMA RRU',
                                    value: station.isCdmaRru ? '是' : '否'
                                }, {
                                    key: 'CDMA基站编号',
                                    value: station.btsId
                                }, {
                                    key: 'CDMA小区编号',
                                    value: station.cdmaSectorId
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '干放个数',
                                    value: station.amplifiers
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '规模',
                                    value: station.scaleDescription
                                }, {
                                    key: '服务区域编码',
                                    value: station.serviceArea
                                }, {
                                    key: '有源器件数',
                                    value: station.sourceAppliances
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '室外微基站数',
                                    value: station.outdoorPicos
                                }, {
                                    key: '室外拉远数',
                                    value: station.outdoorRrus
                                }, {
                                    key: '室外直放站数',
                                    value: station.outdoorRepeaters
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '室内微基站数',
                                    value: station.indoorPicos
                                }, {
                                    key: '室内拉远数',
                                    value: station.indoorRrus
                                }, {
                                    key: '室内直放站数',
                                    value: station.indoorRepeaters
                                }
                            ]
                        }
                    ];
                },
                generateIndoorGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站点名称',
                                    value: station.name
                                }, {
                                    key: '区分公司',
                                    value: station.areaName
                                }, {
                                    key: '镇区',
                                    value: station.town
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '站点等级',
                                    value: station.grade
                                }, {
                                    key: '站点归属',
                                    value: station.isNew
                                }, {
                                    key: '站点类型属性',
                                    value: station.indoortype
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '主要覆盖范围',
                                    value: station.coverage
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '地址',
                                    value: station.address
                                }, {
                                    key: '系统分类',
                                    value: station.systemclassify
                                }, {
                                    key: '交维日期',
                                    value: station.IntersectionDate
                                }
                            ]
                        }
                    ];
                },
                generateConstructionGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '站点名称',
                                    value: station.eNodebName
                                }, {
                                    key: '营销中心',
                                    value: station.town
                                }, {
                                    key: '区域',
                                    value: station.district
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '站点编号',
                                    value: station.siteNumber
                                }, {
                                    key: 'FSL编号',
                                    value: station.fslNumber
                                }, {
                                    key: 'FSC编号',
                                    value: station.fscNumber
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否室内',
                                    value: station.isIndoor
                                }, {
                                    key: '是否已移交',
                                    value: station.isTransfer
                                }, {
                                    key: '会审时间',
                                    value: station.uploadTime
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '建设状态',
                                    value: station.status
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '开通时间',
                                    value: station.openTime
                                }, {
                                    key: '完工时间',
                                    value: station.completedTime
                                }, {
                                    key: '建设时间',
                                    value: station.constructionTime
                                }
                            ]
                        }
                    ];
                },
                generateMicroAddressGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '地址编号',
                                    value: station.addressNumber
                                }, {
                                    key: '镇区',
                                    value: station.town
                                }, {
                                    key: '区域',
                                    value: station.district
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '经度',
                                    value: station.longtitute
                                }, {
                                    key: '纬度',
                                    value: station.lattitute
                                }, {
                                    key: '施主基站',
                                    value: station.baseStation
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '详细地址',
                                    value: station.address,
                                    span: 5
                                }
                            ]
                        }
                    ];
                },
                generateMicroItemGroups: function(station) {
                    return [
                        {
                            items: [
                                {
                                    key: '设备名称',
                                    value: station.name
                                }, {
                                    key: '设备型号',
                                    value: station.type,
                                    span: 3
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '设备编码',
                                    value: station.itemNumber
                                }, {
                                    key: '设备厂家',
                                    value: station.factory
                                }, {
                                    key: '资产编号',
                                    value: station.materialNumber
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '出仓日期',
                                    value: station.borrowDate
                                }, {
                                    key: '领取人',
                                    value: station.borrower
                                }, {
                                    key: '编号',
                                    value: station.serialNumber
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉人',
                                    value: station.complainer
                                }, {
                                    key: '投诉电话',
                                    value: station.complainPhone
                                }, {
                                    key: '是否交申请表',
                                    value: station.submitForm
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '是否交协议',
                                    value: station.protocol
                                }, {
                                    key: '备注',
                                    value: station.comments
                                }
                            ]
                        }
                    ];
                },
                generateComplainItemGroups: function(item) {
                    return [
                        {
                            items: [
                                {
                                    key: '接单时间',
                                    value: item.beginDate
                                }, {
                                    key: '时限要求',
                                    value: item.deadline
                                }, {
                                    key: '处理时间',
                                    value: item.processTime
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '城市',
                                    value: item.city
                                }, {
                                    key: '区域',
                                    value: item.district
                                }, {
                                    key: '室内外',
                                    value: item.isIndoorDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '楼宇名称',
                                    value: item.buildingName
                                }, {
                                    key: '道路名称',
                                    value: item.roadName
                                }, {
                                    key: '匹配站点',
                                    value: item.sitePosition
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '原因定位',
                                    value: item.causeLocation
                                }, {
                                    key: '投诉分类',
                                    value: item.complainCategoryDescription
                                }, {
                                    key: '投诉原因',
                                    value: item.complainReasonDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉场景',
                                    value: item.complainSceneDescription
                                }, {
                                    key: '投诉来源',
                                    value: item.complainSourceDescription
                                }, {
                                    key: '投诉子原因',
                                    value: item.complainSubReasonDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '联系地址',
                                    value: item.contactAddress
                                }, {
                                    key: '联系人',
                                    value: item.contactPerson
                                }, {
                                    key: '联系电话',
                                    value: item.contactPhone
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '投诉内容',
                                    value: item.complainContents,
                                    span: 5
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '用户姓名',
                                    value: item.subscriberInfo
                                }, {
                                    key: '用户号码',
                                    value: item.subscriberPhone
                                }, {
                                    key: '业务类型',
                                    value: item.serviceCategoryDescription
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '室内外',
                                    value: item.isIndoorDescription

                                }, {
                                    key: '经度',
                                    value: item.longtitute
                                }, {
                                    key: '纬度',
                                    value: item.lattitute
                                }
                            ]
                        }
                    ];
                },
                generateComplainPositionGroups: function(item) {
                    return [
                        {
                            items: [
                                {
                                    key: '工单编号',
                                    value: item.serialNumber
                                }, {
                                    key: '经度',
                                    value: item.longtitute
                                }, {
                                    key: '纬度',
                                    value: item.lattitute
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '城市',
                                    value: item.city
                                }, {
                                    key: '区域',
                                    value: item.district
                                }, {
                                    key: '镇区',
                                    value: item.town
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '楼宇名称',
                                    value: item.buildingName
                                }, {
                                    key: '道路名称',
                                    value: item.roadName
                                }, {
                                    key: '匹配站点',
                                    value: item.sitePosition
                                }
                            ]
                        }, {
                            items: [
                                {
                                    key: '联系地址',
                                    value: item.contactAddress,
                                    span: 2
                                }, {
                                    key: '投诉内容',
                                    value: item.complainContents,
                                    span: 2
                                }
                            ]
                        }
                    ];
                },

                generateDistrictButtonTemplate: function() {
                    return '<button class="btn btn-sm btn-default" ng-hide="row.entity.district===grid.appScope.cityFlag" ' +
                        'ng-click="grid.appScope.overallStat.currentDistrict = row.entity.district">{{row.entity.district}}</button>';
                },
                generateProcessButtonTemplate: function() {
                    return '<a class="btn btn-sm btn-primary" ng-hide="row.entity.district===grid.appScope.cityFlag" ' +
                        'ng-click="grid.appScope.showWorkItemDistrict(row.entity.district)">工单处理</a>';
                },
                generateAnalyzeButtonTemplate: function() {
                    return '<a class="btn btn-sm btn-default" ng-hide="row.entity.district===grid.appScope.cityFlag" ' +
                        'ng-click="grid.appScope.showTopDistrict(row.entity.district)">TOP指标</a>';
                },
                generateDistrictTownKpiDefs: function(kpiFields) {
                    return [
                        { field: 'district', name: '区域' },
                        { field: 'town', name: '镇区' }
                    ].concat(kpiFields);
                },
                generateLteCellNameDef: function() {
                    return {
                        cellTemplate: '<span class="text-primary">{{row.entity.eNodebName}}-{{row.entity.sectorId}}</span>',
                        name: '小区名称'
                    }
                },
                generatePreciseRateDef: function(index) {
                    var englishDicts = ['first', 'second', 'third'];
                    var chineseDicts = ['一', '二', '三'];
                    return {
                        cellTemplate: '<span class="text-primary">{{100 - row.entity.' +
                            englishDicts[index] +
                            'Rate | number:2}}</span>',
                        name: '第' + chineseDicts[index] + '邻区精确覆盖率',
                        cellTooltip: function(row) {
                            return '第' +
                                chineseDicts[index] +
                                '邻区与主服务小区RSRP的差值小于6dB的比例是: ' +
                                row.entity[englishDicts[index] + 'Rate'];
                        }
                    }
                },
                generateCqiTimesDef: function() {
                    var result = [];
                    for (var index = 0; index < 16; index++) {
                        result.push({
                            field: 'cqi' + index + 'times',
                            name: 'CQI=' + index + '次数'
                        });
                    }
                    return result;
                },
                generateDistrictPieNameValueFuncs: function() {
                    return {
                        nameFunc: function(stat) {
                            return stat.district;
                        },
                        valueFunc: function(stat) {
                            return stat.districtData;
                        }
                    };
                },
                generateLowThresholdClassFunc: function(grid, row, col) {
                    if (grid.getCellValue(row, col) < row.entity.objectRate) {
                        return 'text-danger';
                    }
                    return 'text-success';
                },
                generateHighThresholdClassFunc: function(grid, row, col) {
                    if (grid.getCellValue(row, col) > row.entity.objectRate) {
                        return 'text-danger';
                    }
                    return 'text-success';
                }
            };
        });