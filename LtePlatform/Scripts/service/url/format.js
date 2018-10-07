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