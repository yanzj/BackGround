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