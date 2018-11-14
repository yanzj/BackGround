angular.module('app.core', [])
    .value('publicNetworkIp', '218.13.12.242')
    .factory('appUrlService', function(publicNetworkIp) {
        var parseQueryString = function(queryString) {
            var data = {}, pair, separatorIndex, escapedKey, escapedValue, key, value;

            if (queryString === null || queryString === undefined) {
                return data;
            }

            var pairs = queryString.split("?")[1].split("&");

            for (var i = 0; i < pairs.length; i++) {
                pair = pairs[i];
                separatorIndex = pair.indexOf("=");

                if (separatorIndex === -1) {
                    escapedKey = pair;
                    escapedValue = null;
                } else {
                    escapedKey = pair.substr(0, separatorIndex);
                    escapedValue = pair.substr(separatorIndex + 1);
                }

                key = decodeURIComponent(escapedKey);
                value = decodeURIComponent(escapedValue);

                data[key] = value;
            }

            return data;
        };
        var getFragment = function() {
            if (window.location.hash.indexOf("#") === 0) {
                var queryString = window.location.hash.replace("/", "?");
                return parseQueryString(queryString);
            } else {
                return {};
            }
        };
        var initializeAuthorization = function() {
            if (!sessionStorage.getItem("accessToken")) {
                var fragment = getFragment();

                if (fragment.access_token) {
                    // returning with access token, restore old hash, or at least hide token
                    window.location.hash = fragment.state || '';
                    sessionStorage.setItem("accessToken", fragment.access_token);
                } else {
                    // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                    window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
                }
            }
        };
        return {
            getApiUrl: function(topic) {
                return '/api/' + topic;
            },
            userInfoUrl: "/api/Me",
            siteUrl: "/",
            parseQueryString: parseQueryString,
            getAccessToken: function() {
                var token = sessionStorage.getItem("accessToken");
                if (!token) initializeAuthorization();
                return token || sessionStorage.getItem("accessToken");
            },
            initializeAuthorization: initializeAuthorization,
            getDtUrlHost: function () {
                return (window.location.hostname === publicNetworkIp) ? '/' : 'http://132.110.71.121:2889/';
            },
            getParameterUrlHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8110/' : 'http://132.110.71.122:8001/';
            },
            getBuildingUrlHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8111/' : 'http://132.110.71.122:8002/';
            },
            getTopnUrlHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8112/' : 'http://132.110.71.122:8006/';
            },
            getInterferenceHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8113/' : 'http://132.110.71.122:18080/';
            },
            getCustomerHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8102/' : 'http://132.110.71.121:8018/';
            },
            getPhpHost: function () {
                return (window.location.hostname === publicNetworkIp) ? 'http://119.145.142.74:8106/' : 'http://132.110.71.122:9000/';
            },
            getPhpUriComponent: function(obj) {
                var str = [];
                for (var p in obj) {
                    if (obj.hasOwnProperty(p)) {
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    }
                }
                return str.join("&");
            },

            initializeIndexedDb: function(myDb, storeNames, key, callback) {
                var version = myDb.version || 1;
                var request = window.indexedDB.open(myDb.name, version);
                request.onerror = function(e) {
                    console.log(e.currentTarget.error.message);
                };
                request.onsuccess = function(e) {
                    myDb.db = e.target.result;
                    if (callback) {
                        callback();
                    }
                };
                request.onupgradeneeded = function(e) {
                    var db = e.target.result;
                    angular.forEach(storeNames, function(storeName) {
                        if (!db.objectStoreNames.contains(storeName)) {
                            db.createObjectStore(storeName, { keyPath: key });
                        }
                    });

                    console.log('DB version changed to ' + version);
                };
            },
            updateIndexedDb: function(db, storeName, keyName, key, value) {
                var transaction = db.transaction(storeName, 'readwrite');
                var store = transaction.objectStore(storeName);
                var request = store.get(key);
                value[keyName] = key;
                request.onsuccess = function(e) {
                    var item = e.target.result;
                    if (item) {
                        store.put(value);
                    } else {
                        store.add(value);
                    }

                };
            },
            refreshIndexedDb: function(db, storeName, keyName, items) {
                var transaction = db.transaction(storeName, 'readwrite');
                var store = transaction.objectStore(storeName);
                store.clear();
                angular.forEach(items, function(item) {
                    store.put(item);
                });

            },
            fetchStoreByCursor: function(db, storeName, callback) {
                var transaction = db.transaction(storeName);
                var store = transaction.objectStore(storeName);
                var request = store.openCursor();
                var data = [];
                request.onsuccess = function(e) {
                    var cursor = e.target.result;
                    if (cursor) {
                        data.push(cursor.value);
                        cursor.continue();
                    } else if (callback) {
                        callback(data);
                    }
                };
            }
        };
    })
    .factory('stationFormatService', function() {
        return {
            dateSpanResolve: function (basicFields, beginDate, endDate) {
                angular.extend(basicFields,
                    {
                        begin: function () {
                            return beginDate;
                        },
                        end: function () {
                            return endDate;
                        }
                    });
                return basicFields;
            },
            dateSpanDateResolve: function (basicFields, beginDate, endDate) {
                angular.extend(basicFields,
                    {
                        beginDate: function () {
                            return beginDate;
                        },
                        endDate: function () {
                            return endDate;
                        }
                    });
                return basicFields;
            }
        };
    })
    .factory('stationFactory', function() {
        return {
            stationGradeOptions: [
                { value: '', name: '所有级别' },
                { value: 'A', name: '站点级别A' },
                { value: 'B', name: '站点级别B' },
                { value: 'C', name: '站点级别C' },
                { value: 'D', name: '站点级别D' }
            ],
            stationRoomOptions: [
                { value: '', name: '所有机房' },
                { value: '电信', name: '电信机房' },
                { value: '铁塔', name: '铁塔机房' },
                { value: '联通', name: '联通机房' }
            ],
            stationTowerOptions: [
                { value: '', name: '全部杆塔' },
                { value: '电信', name: '电信杆塔' },
                { value: '铁塔', name: '铁塔杆塔' },
                { value: '联通', name: '联通杆塔' }
            ],
            stationBbuOptions: [
                { value: '', name: '全部BBU' },
                { value: '是', name: 'BBU池' },
                { value: '否', name: '非BBU池' }
            ],
            stationNetworkOptions: [
                { value: '', name: '全部网络' },
                { value: 'C', name: 'C网络' },
                { value: 'L', name: 'L网络' },
                { value: 'VL', name: 'VL网络' },
                { value: 'C+L', name: 'C+L网络' },
                { value: 'C+VL', name: 'C+VL网络' },
                { value: 'L+VL', name: 'L+VL网络' }
            ],
            stationPowerOptions: [
                { value: '', name: '所有动力配套' },
                { value: '是', name: '有动力配套' },
                { value: '否', name: '没有动力配套' }
            ],
            stationConstructionOptions: [
                { value: '', name: '所有站点' },
                { value: '电信新建站点', name: '电信新建站点' },
                { value: '电信整改站点', name: '电信整改站点' },
                { value: '联通原有站点', name: '联通原有站点' },
                { value: '联通整改站点', name: '联通整改站点' }
            ],
            stationIndoorOptions: [
                { value: '', name: '所有类型' },
                { value: '居民住宅', name: '居民住宅' },
                { value: '餐饮娱乐', name: '餐饮娱乐' },
                { value: '机关企业', name: '机关企业' }
            ],
            stationDistincts: [
                { value: 'SD', name: '顺德' },
                { value: 'NH', name: '南海' },
                { value: 'CC', name: '禅城' },
                { value: 'SS', name: '三水' },
                { value: 'GM', name: '高明' }
            ],
            getAreaTypeColor: function(areaType) {
                switch(areaType) {
                    case '农村开阔地':
                        return 'green';
                    case '郊区':
                        return 'yellow';
                    case '一般城区':
                        return 'blue';
                    case '密集城区':
                        return 'red';
                    default:
                        return 'gray';
                }
            }
        };
    })
    .controller('header.menu', function($scope, appUrlService) {
        $scope.commonMenu = {
            $folded: true
        };
    })
    .factory('generalHttpService', function ($q, $http, $sce, appUrlService) {
        return {
            getMvcData: function (topic, params) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: topic,
                    params: params
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                .catch(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            getApiData: function (topic, params) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl(topic),
                    params: params
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                .catch(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            getApiDataWithHeading: function (topic, params) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: appUrlService.getApiUrl(topic),
                    params: params,
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                .catch(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            },
            postApiData: function (topic, data) {
                var deferred = $q.defer();
                $http.post(appUrlService.getApiUrl(topic), data)
                    .then(function (result) {
                        deferred.resolve(result.data);
                    })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            postMvcData: function (topic, data) {
                var deferred = $q.defer();
                $http.post(topic, data)
                    .then(function (result) {
                        deferred.resolve(result.data);
                    })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            postApiDataWithHeading: function (topic, data) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: appUrlService.getApiUrl(topic),
                    data: data,
                    headers: {
                        'Authorization': 'Bearer ' + appUrlService.getAccessToken()
                    }
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            postPhpUrlData: function (url, data) {
                var deferred = $q.defer();
                $http({
                    method: 'POST',
                    url: url,
                    data: data,
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                    transformRequest: function (obj) {
                        return appUrlService.getPhpUriComponent(obj);
                    }
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            putApiData: function (topic, data) {
                var deferred = $q.defer();
                $http.put(appUrlService.getApiUrl(topic), data)
                    .then(function (result) {
                        deferred.resolve(result.data);
                    })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            deleteApiData: function (topic) {
                var deferred = $q.defer();
                $http.delete(appUrlService.getApiUrl(topic))
                    .then(function (result) {
                    deferred.resolve(result.data);
                })
                    .catch(function (reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            getJsonpData: function (url, valueFunc) {
                var deferred = $q.defer();
                $http.jsonp(url, { jsonpCallbackParam: 'callback' })
                    .then(function(result) {
                        deferred.resolve(valueFunc(result.data));
                    })
                    .catch(function(reason) {
                        deferred.reject(reason);
                    });
                return deferred.promise;
            },
            getUrlData: function (url, params) {
                var deferred = $q.defer();
                $http({
                    method: 'GET',
                    url: url,
                    params: params
                }).then(function (result) {
                    deferred.resolve(result.data);
                })
                .catch(function (reason) {
                    deferred.reject(reason);
                });
                return deferred.promise;
            }
        };
    });
angular.module('app.menu', ['ui.grid', "ui.bootstrap"])
    .factory('menuItemService', function($uibModal, $log) {
        return {
            updateMenuItem: function(items, index, title, url, masterName) {
                if (index >= items.length) return;
                masterName = masterName || "";
                var subItems = items[index].subItems;
                for (var i = 0; i < subItems.length; i++) {
                    if (subItems[i].displayName === title) return;
                }
                subItems.push({
                    displayName: title,
                    url: url,
                    masterName: masterName
                });
                angular.forEach(items, function(item) {
                    item.isActive = false;
                });
                items[index].isActive = true;
            },
            showGeneralDialog: function(settings) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    console.log(info);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showGeneralDialogWithAction: function(settings, action) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    action(info);
                }, function() {
                    $log.info('Modal dismissed at: ' + new Date());
                });
            },
            showGeneralDialogWithDoubleAction: function(settings, action, action2) {
                var modalInstance = $uibModal.open({
                    animation: true,
                    templateUrl: settings.templateUrl,
                    controller: settings.controller,
                    size: settings.size || 'lg',
                    resolve: settings.resolve
                });
                modalInstance.result.then(function(info) {
                    action(info);
                }, function() {
                    action2();
                });
            }
        };
    });
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
                }
            };
        });
angular.module('app.chart', ['app.format', 'app.calculation'])
    .factory('calculateService',
        function() {
            return {
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
                }
            };
        });
angular.module('app.geometry', [])
    .factory('geometryCalculateService', function() {
        var getDistanceFunc = function(p1Lat, p1Lng, p2Lat, p2Lng) {
            var earthRadiusKm = 6378.137;
            var dLat1InRad = p1Lat * (Math.PI / 180);
            var dLong1InRad = p1Lng * (Math.PI / 180);
            var dLat2InRad = p2Lat * (Math.PI / 180);
            var dLong2InRad = p2Lng * (Math.PI / 180);
            var dLongitude = dLong2InRad - dLong1InRad;
            var dLatitude = dLat2InRad - dLat1InRad;
            var a = Math.pow(Math.sin(dLatitude / 2), 2) + Math.cos(dLat1InRad) * Math.cos(dLat2InRad) * Math.pow(Math.sin(dLongitude / 2), 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var dDistance = earthRadiusKm * c;
            return dDistance;
        };
        var getLonLatFunc = function(centre, x, y) {
            var lat = centre.lat + y / getDistanceFunc(centre.lat, centre.lng, centre.lat + 1, centre.lng);
            var lng = centre.lng + x / getDistanceFunc(centre.lat, centre.lng, centre.lat, centre.lng + 1);
            return new BMap.Point(lng, lat);
        };
        return {
            getDistanceFunc: function(p1Lat, p1Lng, p2Lat, p2Lng) {
                return getDistanceFunc(p1Lat, p1Lng, p2Lat, p2Lng);
            },
            getLonLatFunc: function(centre, x, y) {
                return getLonLatFunc(centre, x, y);
            },
            getPositionFunc: function(centre, r, angle) {
                var x = r * Math.cos(angle * Math.PI / 180);
                var y = r * Math.sin(angle * Math.PI / 180);
                return getLonLatFunc(centre, x, y);
            },
            getPositionRadius: function(centre, r, rad) {
                var x = r * Math.cos(rad);
                var y = r * Math.sin(rad);
                return getLonLatFunc(centre, x, y);
            },
            getPolygonCenter: function (coors) {
                var centerx = 0;
                var centery = 0;
                for (var p = 0; p < coors.length / 2; p++) {
                    centerx += parseFloat(coors[2 * p]);
                    centery += parseFloat(coors[2 * p + 1]);
                }
                centerx /= coors.length / 2;
                centery /= coors.length / 2;
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getRectangleCenter: function (coors) {
                var centerx = (parseFloat(coors[0]) + parseFloat(coors[2])) / 2;
                var centery = (parseFloat(coors[1]) + parseFloat(coors[3])) / 2;
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getCircleCenter: function (coors) {
                var centerx = parseFloat(coors[0]);
                var centery = parseFloat(coors[1]);
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getRadiusFunc: function(zoom) {
                var rSation = 70;
                var rSector = 0.2;
                switch (zoom) {
                case 15:
                    rSector *= 0.75;
                    rSation *= 0.75;
                    break;
                case 16:
                    rSector /= 2.5;
                    rSation /= 2.5;
                    break;
                case 17:
                    rSector /= 5;
                    rSation /= 5;
                    break;
                default:
                    break;
                }

                return { rSector: rSector, rStation: rSation };
            }

        };
    })
    .factory('geometryService', function(geometryCalculateService) {
        return {
            getDistance: function(p1Lat, p1Lng, p2Lat, p2Lng) {
                return geometryCalculateService.getDistanceFunc(p1Lat, p1Lng, p2Lat, p2Lng);
            },
            getLonLat: function(centre, x, y) {
                return geometryCalculateService.getLonLatFunc(centre, x, y);
            },
            getPosition: function(centre, r, angle) {
                return geometryCalculateService.getPositionFunc(centre, r, angle);
            },
            getPositionLonLat: function(centre, r, angle) {
                var x = r * Math.cos(angle * Math.PI / 180);
                var y = r * Math.sin(angle * Math.PI / 180);
                var lat = centre.lattitute + y / geometryCalculateService.getDistanceFunc(centre.lattitute, centre.longtitute, centre.lattitute + 1, centre.longtitute);
                var lng = centre.longtitute + x / geometryCalculateService.getDistanceFunc(centre.lattitute, centre.longtitute, centre.lattitute, centre.longtitute + 1);
                return {
                    longtitute: lng,
                    lattitute: lat
                };
            },
            generateSectorPolygonPoints: function(centre, irotation, iangle, zoom, scalor) {
                var assemble = [];
                var dot;
                var i;
                var r = geometryCalculateService.getRadiusFunc(zoom).rSector * (scalor || 1);

                for (i = 0; i <= r; i += r / 2) {
                    dot = geometryCalculateService.getPositionFunc(centre, i, irotation);
                    assemble.push(dot);
                }

                for (i = 0; i <= iangle; i += iangle / 5) {
                    dot = geometryCalculateService.getPositionFunc(centre, r, i + irotation);
                    assemble.push(dot);
                }

                return assemble;
            },
            getRadius: function(zoom) {
                return geometryCalculateService.getRadiusFunc(zoom);
            },
            getDtPointRadius: function(zoom) {
                var radius = 17;
                switch (zoom) {
                case 15:
                    radius *= 0.9;
                    break;
                case 16:
                    radius *= 0.8;
                    break;
                case 17:
                    radius *= 0.75;
                    break;
                default:
                    break;
                }
                return radius;
            },
            getArrowLine: function(x1, y1, x2, y2, r) {
                var rad = Math.atan2(y2 - y1, x2 - x1);
                var centre = {
                    lng: x2,
                    lat: y2
                };
                var point1 = geometryCalculateService.getPositionRadius(centre, -r, rad - 0.2);
                var point2 = geometryCalculateService.getPositionRadius(centre, -r, rad + 0.2);
                return new BMap.Polyline([
                    new BMap.Point(x2, y2),
                    point1,
                    point2,
                    new BMap.Point(x2, y2),
                    new BMap.Point(x1, y1)
                ], { strokeColor: "blue", strokeWeight: 1 });
            },
            getLine: function(x1, y1, x2, y2, color) {
                color = color || "orange";
                return new BMap.Polyline([
                    new BMap.Point(x2, y2),
                    new BMap.Point(x1, y1)
                ], { strokeColor: color, strokeWeight: 1 });
            },
            getCircle: function(x, y, r, color, callback, neighbor) {
                color = color || "orange";
                var circle = new BMap.Circle(new BMap.Point(x, y), r, { strokeColor: color, fillColor: color });
                circle.addEventListener("click", function() {
                    callback(neighbor);
                });
                return circle;
            },
            getPathInfo: function(path) {
                var result = '';
                angular.forEach(path, function(point, $index) {
                    if ($index > 0) result += ';';
                    result += point.lng + ';' + point.lat;
                });
                return result;
            },
            queryRegionCenter: function (region) {
                switch (region.regionType) {
                    case 2:
                        return geometryCalculateService.getPolygonCenter(region.info.split(';'));
                    case 1:
                        return geometryCalculateService.getRectangleCenter(region.info.split(';'));
                    default:
                        return geometryCalculateService.getCircleCenter(region.info.split(';'));
                }
            },
            queryMapColors: function() {
                return [
                    '#CC3333',
                    '#3366D0',
                    '#CC9966',
                    '#003300',
                    '#99CC00',
                    '#FF9966',
                    '#CC6699',
                    '#9933FF',
                    '#333300'
                ];
            },
            queryConstructionIcon: function(status) {
                var urlDictionary = {
                    '审计会审': "/Content/Images/BtsIcons/alarm_lightyellow.png",
                    '天馈施工': "/Content/Images/BtsIcons/alarm_yellow.png",
                    '整体完工': "/Content/Images/BtsIcons/alarm_lightblue.png",
                    '基站开通': "/Content/Images/BtsIcons/alarm_green.png",
                    '其他': "/Content/Images/BtsIcons/alarm_red.png"
                }
                var url = urlDictionary[status];
                if (!url) url = "/Content/Images/BtsIcons/alarm_purple.png";
                return new BMap.Icon(url, new BMap.Size(22, 28));
            },
            constructionStateOptions: ['全部', '审计会审', '天馈施工', '整体完工', '基站开通', '其他']
        };
    });
angular.module('app.calculation', ['app.format'])
    .factory('basicCalculationService', function (appFormatService) {
        var isLongtituteValid = function(longtitute) {
            return (!isNaN(longtitute)) && longtitute > 112 && longtitute < 114;
        };

        var isLattituteValid = function(lattitute) {
            return (!isNaN(lattitute)) && lattitute > 22 && lattitute < 24;
        };
        return {
            calculateArraySum: function(data, keys) {
                return _.reduce(data,
                    function(memo, num) {
                        var result = {};
                        angular.forEach(keys,
                            function(key) {
                                result[key] = memo[key] + (num[key] || 0);
                            });
                        return result;
                    });
            },
            isLonLatValid: function(item) {
                return isLongtituteValid(item.longtitute) && isLattituteValid(item.lattitute);
            },
            mapLonLat: function(source, destination) {
                source.longtitute = destination.longtitute;
                source.lattitute = destination.lattitute;
            },
            generateDateSpanSeries: function(begin, end) { ////////////////////////// Reserved
                var result = [];
                var beginDate = new Date(begin.getYear() + 1900, begin.getMonth(), begin.getDate());
                while (beginDate < end) {
                    result.push({
                        date: new Date(beginDate.getYear() + 1900, beginDate.getMonth(), beginDate.getDate())
                    });
                    beginDate.setDate(beginDate.getDate() + 1);
                }
                return result;
            },
            generateYAxisConfig: function(settings) {
                var yAxisConfig = {
                    title: settings.yTitle
                };
                if (settings.yMin !== undefined) {
                    angular.extend(yAxisConfig,
                        {
                            min: settings.yMin
                        });
                }
                if (settings.yMax !== undefined) {
                    angular.extend(yAxisConfig,
                        {
                            max: settings.yMax
                        });
                }
                return yAxisConfig;
            }
        };
    })
    .factory('preciseWorkItemGenerator', function (basicCalculationService) {
        return {
            generatePreciseInterferenceNeighborDtos: function(sources) {
                var dtos = [];
                var sum = basicCalculationService
                    .calculateArraySum(sources,
                        ['overInterferences6Db', 'overInterferences10Db', 'mod3Interferences', 'mod6Interferences']);
                angular.forEach(sources, function(source) {
                    if (source.destENodebId > 0 && source.destSectorId > 0) {
                        var db6Share = source.overInterferences6Db * 100 / sum.overInterferences6Db;
                        var db10Share = source.overInterferences10Db * 100 / sum.overInterferences10Db;
                        var mod3Share = source.mod3Interferences * 100 / sum.mod3Interferences;
                        var mod6Share = source.mod6Interferences * 100 / sum.mod6Interferences;
                        if (db6Share > 10 || db10Share > 10 || mod3Share > 10 || mod6Share > 10) {
                            dtos.push({
                                eNodebId: source.destENodebId,
                                sectorId: source.destSectorId,
                                db6Share: db6Share,
                                db10Share: db10Share,
                                mod3Share: mod3Share,
                                mod6Share: mod6Share
                            });
                        }
                    }
                });
                return dtos;
            },
            generatePreciseInterferenceVictimDtos: function(sources) {
                var dtos = [];
                var sum = basicCalculationService
                    .calculateArraySum(sources,
                        ['overInterferences6Db', 'overInterferences10Db', 'mod3Interferences', 'mod6Interferences']);
                angular.forEach(sources, function(source) {
                    if (source.victimENodebId > 0 && source.victimSectorId > 0) {
                        var db6Share = source.overInterferences6Db * 100 / sum.overInterferences6Db;
                        var db10Share = source.overInterferences10Db * 100 / sum.overInterferences10Db;
                        var mod3Share = source.mod3Interferences * 100 / sum.mod3Interferences;
                        var mod6Share = source.mod6Interferences * 100 / sum.mod6Interferences;
                        if (db6Share > 10 || db10Share > 10 || mod3Share > 10 || mod6Share > 10) {
                            dtos.push({
                                eNodebId: source.victimENodebId,
                                sectorId: source.victimSectorId,
                                backwardDb6Share: db6Share,
                                backwardDb10Share: db10Share,
                                backwardMod3Share: mod3Share,
                                backwardMod6Share: mod6Share
                            });
                        }
                    }
                });
                return dtos;
            },

            calculatePlatformInfo: function (comments) {
                var platformInfos = [];
                if (comments) {
                    var fields = comments.split('[');
                    if (fields.length > 1) {
                        angular.forEach(fields, function (field) {
                            var subFields = field.split(']');
                            platformInfos.push({
                                time: subFields[0],
                                contents: subFields[1]
                            });
                        });
                    }
                }

                return platformInfos;
            }
        };
    })
    .factory('neGeometryService', function (basicCalculationService) {
        return {
            queryENodebLonLatEdits: function(eNodebs) {
                var result = [];
                angular.forEach(eNodebs,
                    function(eNodeb, $index) {
                        if (!basicCalculationService.isLonLatValid(eNodeb)) {
                            var item = {
                                index: $index
                            };
                            angular.extend(item, eNodeb);
                            item.town = eNodeb.townName;
                            result.push(item);
                        }
                    });
                return result;
            },
            queryGeneralLonLatEdits: function(stats) {
                var result = [];
                angular.forEach(stats,
                    function(stat, $index) {
                        if (!basicCalculationService.isLonLatValid(stat)) {
                            var item = {
                                index: $index
                            };
                            angular.extend(item, stat);
                            result.push(item);
                        }
                    });
                return result;
            },
            mapLonLatEdits: function(sourceFunc, destList) {
                var sourceList = sourceFunc();
                for (var i = 0; i < destList.length; i++) {
                    destList[i].longtitute = parseFloat(destList[i].longtitute);
                    destList[i].lattitute = parseFloat(destList[i].lattitute);
                    if (basicCalculationService.isLonLatValid(destList[i])) {
                        basicCalculationService.mapLonLat(sourceList[destList[i].index], destList[i]);
                    }
                }
                sourceFunc(sourceList);
            },
            queryNearestRange: function(xCenter, yCenter) {
                return {
                    west: xCenter - 1e-6,
                    east: xCenter + 1e-6,
                    south: yCenter - 1e-6,
                    north: yCenter + 1e-6
                };
            },
            queryNearRange: function(xCenter, yCenter) {
                return {
                    west: xCenter - 1e-2,
                    east: xCenter + 1e-2,
                    south: yCenter - 1e-2,
                    north: yCenter + 1e-2
                };
            }
        };
    })
    .factory('generalChartService', function() {
        return {
            getPieOptions: function(data, setting, subNameFunc, subDataFunc) {
                var chart = new GradientPie();
                chart.title.text = setting.title;
                chart.series[0].name = setting.seriesTitle;
                angular.forEach(data, function(subData) {
                    chart.series[0].data.push({
                        name: subNameFunc(subData),
                        y: subDataFunc(subData)
                    });
                });
                return chart.options;
            },
            getPieOptionsByArrays: function(setting, nameArray, dataArray) {
                var chart = new GradientPie();
                chart.title.text = setting.title;
                chart.series[0].name = setting.seriesTitle;
                angular.forEach(nameArray,
                    function(name, index) {
                        chart.series[0].data.push({
                            name: name,
                            y: dataArray[index]
                        });
                    });
                return chart.options;
            },
            getColumnOptions: function(stat, setting, categoriesFunc, dataFunc) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitle;
                chart.xAxis[0].categories = categoriesFunc(stat);
                chart.series.push({
                    type: "column",
                    name: setting.ytitle,
                    data: dataFunc(stat)
                });
                return chart.options;
            },
            getStackColumnOptions: function(stats, setting, categoriesFunc, dataFuncList) {
                var chart = new StackColumnChart();
                chart.title.text = setting.title;
                chart.xAxis.title.text = setting.xtitle;
                chart.yAxis.title.text = setting.ytitle;
                chart.xAxis.categories = _.map(stats, function(stat) {
                    return categoriesFunc(stat);
                });
                angular.forEach(dataFuncList, function(dataFunc, $index) {
                    chart.series.push({
                        name: setting.seriesTitles[$index],
                        data: _.map(stats, function(stat) {
                            return dataFunc(stat);
                        })
                    });
                });

                return chart.options;
            },
            queryColumnOptions: function(setting, categories, data) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitle;
                chart.xAxis[0].categories = categories;
                if (setting.min) {
                    chart.setDefaultYAxis({ min: setting.min });
                }
                if (setting.max) {
                    chart.setDefaultYAxis({ max: setting.max });
                }
                chart.series.push({
                    type: "column",
                    name: setting.ytitle,
                    data: data
                });
                return chart.options;
            },
            queryDoubleColumnOptions: function(setting, categories, data1, data2) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitle;
                chart.xAxis[0].categories = categories;
                chart.series.push({
                    type: "column",
                    name: setting.ytitle1,
                    data: data1
                });
                chart.series.push({
                    type: "column",
                    name: setting.ytitle2,
                    data: data2
                });
                return chart.options;
            },
            queryMultipleColumnOptions: function(setting, categories, dataList, seriesTitle) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitle;
                chart.xAxis[0].categories = categories;
                angular.forEach(dataList, function(data, $index) {
                    chart.series.push({
                        type: "column",
                        name: seriesTitle[$index],
                        data: data
                    });
                });
                return chart.options;
            },
            queryMultipleComboOptions: function(setting, categories, dataList, seriesTitle, typeList) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitle;
                chart.xAxis[0].categories = categories;
                angular.forEach(dataList, function(data, $index) {
                    chart.series.push({
                        type: typeList[$index],
                        name: seriesTitle[$index],
                        data: data
                    });
                });
                return chart.options;
            },
            queryMultipleComboOptionsWithDoubleAxes: function(setting, categories, dataList, seriesTitle, typeList, yAxisList) {
                var chart = new ComboChart();
                chart.title.text = setting.title;
                chart.xAxis[0].title.text = setting.xtitle;
                chart.yAxis[0].title.text = setting.ytitles[0];
                chart.pushOneYAxis(setting.ytitles[1]);

                chart.xAxis[0].categories = categories;
                angular.forEach(dataList, function(data, $index) {
                    chart.series.push({
                        type: typeList[$index],
                        name: seriesTitle[$index],
                        data: data,
                        yAxis: yAxisList[$index]
                    });
                });
                return chart.options;
            },
            generateColumnData: function(stats, categoriesFunc, dataFuncs) {
                var result = {
                    categories: [],
                    dataList: []
                };
                angular.forEach(dataFuncs, function(func) {
                    result.dataList.push([]);
                });
                angular.forEach(stats, function(stat) {
                    result.categories.push(categoriesFunc(stat));
                    angular.forEach(dataFuncs, function(func, index) {
                        result.dataList[index].push(func(stat));
                    });
                });
                return result;
            },
            generateColumnDataByKeys: function(stats, categoriesKey, dataKeys) {
                var result = {
                    categories: [],
                    dataList: []
                };
                angular.forEach(dataKeys, function(key) {
                    result.dataList.push([]);
                });
                angular.forEach(stats, function(stat) {
                    result.categories.push(stat[categoriesKey]);
                    angular.forEach(dataKeys, function(key, index) {
                        result.dataList[index].push(stat[key]);
                    });
                });
                return result;
            },
            generateCompoundStats: function(views, getType, getSubType, getTotal) {
                var stats = [];
                angular.forEach(views, function(view) {
                    var type = getType !== undefined ? getType(view) : view.type;
                    var subType = getSubType !== undefined ? getSubType(view) : view.subType;
                    var total = getTotal !== undefined ? getTotal(view) : view.total;
                    var j;
                    for (j = 0; j < stats.length; j++) {
                        if (stats[j].type === (type || '无有效值')) {
                            stats[j].total += total;
                            stats[j].subData.push([subType || '无有效值', total]);
                            break;
                        }
                    }
                    if (j === stats.length) {
                        stats.push({
                            type: type || '无有效值',
                            total: total,
                            subData: [[subType || '无有效值', total]]
                        });
                    }
                });
                return stats;
            }
        };
    })
    .factory('parametersChartService', function(generalChartService) {
        return {
            getHotSpotDtDistancePieOptions: function(name, stats) {
                return generalChartService.getPieOptions(stats, {
                    title: name + "测试里程分布",
                    seriesTitle: "测试文件"
                }, function (subData) {
                    return subData.csvFileName;
                }, function (subData) {
                    return subData.distanceInMeter;
                });
            },
            getHotSpotDtCoverageRateOptions: function (name, stats) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                    title: name + "测试覆盖指标分布",
                    xtitle: '测试日期',
                    ytitles: ['覆盖率（%）', '测试点数']
                },
                    _.map(stats, function (stat) {
                        return stat.testDate;
                    }),
                    [
                        _.map(stats,
                            function (stat) {
                                return stat.coverageRate;
                            }),
                        _.map(stats,
                            function (stat) {
                                return stat.count;
                            })
                    ],
                    ['覆盖率（%）', '测试点数'],
                    ['line', 'column'],
                    [0, 1]);
            },
            getCellFlowOptions: function(dates, result) {
                return generalChartService.queryMultipleColumnOptions({
                        title: '流量统计',
                        xtitle: '日期',
                        ytitle: '流量（MB）'
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.pdcpDownlinkFlow;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.pdcpUplinkFlow;
                            })
                    ],
                    ['下行流量', '上行流量']);
            },
            getCellFeelingRateOptions: function(dates, result) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                        title: '感知速率',
                        xtitle: '日期',
                        ytitles: ['感知速率（Mbit/s）', '用户数']
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.downlinkFeelingRate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.uplinkFeelingRate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.maxUsers;
                            })
                    ],
                    ['下行感知速率', '上行感知速率', '用户数'],
                    ['line', 'line', 'column'],
                    [0, 0, 1]);
            },
            getCellDownSwitchOptions: function(dates, result) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                        title: '4G下切3G次数统计',
                        xtitle: '日期',
                        ytitles: ['4G下切3G次数', '流量（MB）']
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.redirectCdma2000;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.pdcpDownlinkFlow;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.pdcpUplinkFlow;
                            })
                    ],
                    ['4G下切3G次数', '下行流量', '上行流量'],
                    ['column', 'line', 'line'],
                    [0, 1, 1]);
            },
            getCellRank2Options: function(dates, result) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                        title: '4G双流比统计',
                        xtitle: '日期',
                        ytitles: ['4G双流比（%）', '感知速率（Mbit/s）']
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.rank2Rate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.downlinkFeelingRate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.uplinkFeelingRate;
                            })
                    ],
                    ['4G双流比', '下行感知速率', '上行感知速率'],
                    ['column', 'line', 'line'],
                    [0, 1, 1]);
            },
            getCellRrcRequestOptions: function(dates, result) {
                return generalChartService.queryMultipleColumnOptions({
                        title: '连接请求数统计',
                        xtitle: '日期',
                        ytitle: 'RRC连接请求数'
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.totalRrcRequest;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moDataRrcRequest;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moSignallingRrcRequest;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.mtAccessRrcRequest;
                            })
                    ],
                    ['总连接次数', '主叫数据连接次数', '主叫信令连接次数', '被叫连接次数']);
            },
            getCellRrcFailOptions: function(dates, result) {
                return generalChartService.queryMultipleColumnOptions({
                        title: '连接失败数统计',
                        xtitle: '日期',
                        ytitle: 'RRC连接失败次数'
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.totalRrcFail;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moDataRrcFail;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moSignallingRrcFail;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.mtAccessRrcFail;
                            })
                    ],
                    ['总连接失败次数', '主叫数据连接失败次数', '主叫信令连接失败次数', '被叫连接失败次数']);
            },
            getCellRrcRateOptions: function(dates, result) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                        title: 'RRC连接成功率统计',
                        xtitle: '日期',
                        ytitles: ['连接成功率（%）', '连接次数']
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.rrcSuccessRate * 100;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moSiganllingRrcRate * 100;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.totalRrcRequest;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.moSignallingRrcRequest;
                            })
                    ],
                    ['总体成功率', '主叫信令成功率', '总连接次数', '主叫信令连接次数'],
                    ['line', 'line', 'column', 'column'],
                    [0, 0, 1, 1]);
            },
            getCellFlowUsersOptions: function(dates, result) {
                return generalChartService.queryMultipleComboOptionsWithDoubleAxes({
                        title: '流量和用户数统计',
                        xtitle: '日期',
                        ytitles: ['流量（MB）', '感知速率（Mbit/s）']
                    },
                    dates,
                    [
                        _.map(result,
                            function(stat) {
                                return stat.downlinkFeelingRate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.uplinkFeelingRate;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.pdcpDownlinkFlow;
                            }),
                        _.map(result,
                            function(stat) {
                                return stat.pdcpUplinkFlow;
                            })
                    ],
                    ['下行感知速率', '上行感知速率', '下行流量', '上行流量'],
                    ['line', 'line', 'column', 'column'],
                    [1, 1, 0, 0]);
            },
            getCollegeDistributionForDownlinkFlow: function (data, theme) {
                theme = theme || "校园网";
                return generalChartService.getPieOptions(data, {
                    title: theme + "下行流量分布",
                    seriesTitle: "下行流量(MB)"
                }, function(subData) {
                    return subData.name || subData.hotspotName;
                }, function(subData) {
                    return subData.pdcpDownlinkFlow;
                });
            },
            getCollegeDistributionForUplinkFlow: function (data, theme) {
                theme = theme || "校园网";
                return generalChartService.getPieOptions(data, {
                    title: theme + "上行流量分布",
                    seriesTitle: "上行流量(MB)"
                }, function(subData) {
                    return subData.name || subData.hotspotName;
                }, function(subData) {
                    return subData.pdcpUplinkFlow;
                });
            },
            getCollegeDistributionForAverageUsers: function (data, theme) {
                theme = theme || "校园网";
                return generalChartService.getPieOptions(data, {
                    title: theme + "平均用户数分布",
                    seriesTitle: "平均用户数"
                }, function(subData) {
                    return subData.name || subData.hotspotName;
                }, function(subData) {
                    return subData.averageUsers;
                });
            },
            getCollegeDistributionForActiveUsers: function (data, theme) {
                theme = theme || "校园网";
                return generalChartService.getPieOptions(data, {
                    title: theme + "最大激活用户数分布",
                    seriesTitle: "最大激活用户数"
                }, function(subData) {
                    return subData.name || subData.hotspotName;
                }, function(subData) {
                    return subData.maxActiveUsers;
                });
            },
            getLteCellCountOptions: function(theme, stat) {
                return generalChartService.getPieOptionsByArrays({
                        title: theme + "小区数分布",
                        seriesTitle: "频段"
                    },
                    [
                        '2.1G', '1.8G', '800M', 'NB-IoT', '2.6G'
                    ],
                    [
                        stat.lte2100Cells, stat.lte1800Cells, stat.lte800Cells, stat.totalNbIotCells, stat.lte2600Cells
                    ]);
            },
            getCellDistributionForDownlinkFlow: function(data, index) {
                return generalChartService.queryColumnOptions({
                    title: "小区下行流量分布",
                    xtitle: "小区名称",
                    ytitle: "下行流量(MB)"
                }, data.categories, data.dataList[index]);
            },
            getCellDistributionForUplinkFlow: function(data, index) {
                return generalChartService.queryColumnOptions({
                    title: "小区上行流量分布",
                    xtitle: "小区名称",
                    ytitle: "上行流量(MB)"
                }, data.categories, data.dataList[index]);
            },
            getCellDistributionForAverageUsers: function(data, index) {
                return generalChartService.queryColumnOptions({
                    title: "平均用户数分布",
                    xtitle: "小区名称",
                    ytitle: "平均用户数"
                }, data.categories, data.dataList[index]);
            },
            getCellDistributionForActiveUsers: function(data, index) {
                return generalChartService.queryColumnOptions({
                    title: "最大激活用户数分布",
                    xtitle: "小区名称",
                    ytitle: "最大激活用户数"
                }, data.categories, data.dataList[index]);
            },
            getCellIndoorTypeColumnOptions: function(stats) {
                return generalChartService.getStackColumnOptions(stats, {
                    title: '各区室内外小区分布',
                    xtitle: '区域',
                    ytitle: '小区数',
                    seriesTitles: ['室内小区', '室外小区']
                }, function(stat) {
                    return stat.district;
                }, [
                    function(stat) {
                        return stat.totalIndoorCells;
                    }, function(stat) {
                        return stat.totalOutdoorCells;
                    }
                ]);
            },
            getCellBandClassColumnOptions: function(stats) {
                return generalChartService.getStackColumnOptions(stats, {
                    title: '各区小区频段分布',
                    xtitle: '区域',
                    ytitle: '小区数',
                    seriesTitles: ['2.1G频段', '1.8G频段', '800M频段-非NB-IoT', '800M频段-NB-IoT', 'TDD频段']
                }, function(stat) {
                    return stat.district;
                }, [
                    function(stat) {
                        return stat.band1Cells;
                    }, function(stat) {
                        return stat.band3Cells;
                    }, function(stat) {
                        return stat.band5Cells;
                    }, function (stat) {
                        return stat.nbIotCells;
                    }, function(stat) {
                        return stat.band41Cells;
                    }
                ]);
            }
        };
    });
angular.module('myApp.url',
    ['app.core', 'app.menu', 'app.format', 'app.chart', 'app.geometry', 'app.calculation']);