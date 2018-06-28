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