angular.module('app.module', [
    'app.directives.date', 'app.directives.file', 'app.directives.district', 'app.directives.form', 'app.directives.glyphicon'])
    .constant('appRoot', '/directives/app/');

angular.module('app.directives.date', [])
    .directive('dateSpanColumn', function(appRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                beginDate: '=',
                endDate: '='
            },
            templateUrl: appRoot + 'DateSpan.Tpl.html'
        };
    })
    .directive('dateSpanRow', function(appRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                beginDate: '=',
                endDate: '='
            },
            templateUrl: appRoot + 'DateSpanRow.Tpl.html',
            transclude: true,
            link: function(scope, element, attrs) {
                scope.beginTips = attrs.beginTips || "开始日期：";
                scope.endTips = attrs.endTips || "结束日期：";
            }
        };
    })
    .directive('defLaydate', function () {
        return {
            require: '?ngModel',
            restrict: 'A',
            scope: {
                ngModel: '='
            },
            link: function (scope, element, attr, ngModel) {
                var _date = null, _config = {};

                // 初始化参数 
                _config = {
                    elem: '#' + attr.id,
                    format: attr.format != undefined && attr.format != '' ? attr.format : 'YYYY-MM-DD',
                    max: attr.hasOwnProperty('maxDate') ? attr.maxDate : '',
                    min: attr.hasOwnProperty('minDate') ? attr.minDate : '',
                    choose: function (data) {
                        scope.$apply(setViewValue);

                    },
                    clear: function () {
                        ngModel.$setViewValue(null);
                    }
                };
                // 初始化
                _date = laydate(_config);



                // 模型值同步到视图上
                ngModel.$render = function () {
                    element.val(ngModel.$viewValue || '');
                };

                // 监听元素上的事件
                element.on('blur keyup change', function () {
                    scope.$apply(setViewValue);
                });

                setViewValue();

                // 更新模型上的视图值
                function setViewValue() {
                    var val = element.val();
                    ngModel.$setViewValue(val);
                }
            }
        }
    })
    .directive('dateSelection', function (appRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                statDate: '='
            },
            templateUrl: appRoot + 'DateSelection.Tpl.html',
            transclude: true,
            link: function (scope, element, attrs) {
                scope.dateTips = attrs.dateTips || "日期：";
            }
        };
    });
   
angular.module('app.directives.file', [])
    .directive('dumpFileSelector', function(appRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                fileTitle: '@',
                tag: '@',
                fileType: '@'
            },
            templateUrl: appRoot + 'dump/FileSelector.html',
            transclude: true
        };
    })
    .directive('multipleFileSelector', function (appRoot) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                fileTitle: '@',
                tag: '@',
                fileType: '@'
            },
            templateUrl: appRoot + 'dump/MultipleSelector.html',
            transclude: true
        };
    });

angular.module('app.directives.district', ["ui.bootstrap", 'myApp.region'])
    .directive('cityDistrictSelection', function(appRoot, appRegionService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                city: '=',
                district: '='
            },
            templateUrl: appRoot + 'CityDistrictSelection.Tpl.html',
            link: function(scope) {
                appRegionService.initializeCities().then(function(cities) {
                    scope.city.options = cities;
                    scope.city.selected = cities[0];
                });
                scope.$watch("city.selected", function(city) {
                    if (city) {
                        appRegionService.queryDistricts(city).then(function(districts) {
                            scope.district.options = districts;
                            scope.district.selected = districts[0];
                        });
                    }
                });
            }
        };
    })
    .directive('citySelection', function (appRoot, appRegionService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                city: '='
            },
            templateUrl: appRoot + 'CitySelection.Tpl.html',
            transclude: true,
            link: function (scope) {
                appRegionService.initializeCities().then(function (cities) {
                    scope.city.options = cities;
                    scope.city.selected = cities[0];
                });
            }
        };
    })
    .directive('districtTownSelection', function(appRoot, appRegionService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                city: '=',
                district: '=',
                town: '='
            },
            templateUrl: appRoot + 'DistrictTownSelection.Tpl.html',
            transclude: true,
            link: function(scope) {
                scope.$watch("district.selected", function (district, oldDistrict) {
                    if (district && district !== oldDistrict) {
                        appRegionService.queryTowns(scope.city.selected, district).then(function(towns) {
                            scope.town.options = towns;
                            scope.town.selected = towns[0];
                        });
                    }
                });
            }
        };
    })
    .directive('districtTownPlain', function (appRoot, appRegionService) {
        return {
            restrict: 'ECMA',
            replace: true,
            scope: {
                city: '=',
                district: '=',
                town: '='
            },
            templateUrl: appRoot + 'DistrictTownPlain.Tpl.html',
            link: function (scope) {
                scope.$watch("district.selected", function (district, oldDistrict) {
                    if (district && district !== oldDistrict) {
                        appRegionService.queryTowns(scope.city.selected, district).then(function (towns) {
                            scope.town.options = towns;
                            scope.town.selected = towns[0];
                        });
                    }
                });
            }
        };
    });

angular.module('app.directives.form', [])
    .directive('formFieldError', function($compile) {
        return{
            restrict: 'A',
            require: 'ngModel',
            link: function(scope, element, attrs, ngModel) {
                var subScope = scope.$new(true);
                subScope.hasErrors = function() {
                    return ngModel.$invalid && ngModel.$dirty;
                };
                subScope.errors = function() {
                    var errors = ngModel.$error;
                    if (errors.parse !== undefined) {
                        errors.parse = false;
                    }
                    return errors;
                };
                subScope.customHints = scope.$eval(attrs.formFieldError);
                var hint = $compile(
                    '<ul class="text-danger" ng-if="hasErrors()">'
                    + '<small ng-repeat="(name, wrong) in errors()" ng-if="wrong">{{name | formError: customHints}}</small>'
                    + '</ul>'
                )(subScope);
                element.after(hint);
            }
        };
    })
    .directive('formAssertSameAs', function() {
        return {
            restrict: 'A',
            require: 'ngModel',
            link: function(scope, element, attrs, ngModel) {
                var isSame = function(value) {
                    var anotherValue = scope.$eval(attrs.formAssertSameAs);
                    return value === anotherValue;
                };
                ngModel.$parsers.push(function(value) {
                    ngModel.$setValidity('same', isSame(value));
                    return isSame(value) ? value : undefined;
                });
                scope.$watch(function() {
                    return scope.$eval(attrs.formAssertSameAs);
                }, function() {
                    ngModel.$setValidity('same', isSame(ngModel.$modelValue));
                });
            }
        };
    });

angular.module('app.directives.glyphicon', [])
    .directive('glyphiconEnhance', function() {
        return {
            restrict: 'A',
            compile: function(element, attrs) {
                element.addClass('glyphicon');
                element.addClass('glyphicon-l');
                if (attrs.type) {
                    element.addClass('glyphicon-' + attrs.type);
                }
            }
        }
    })
    .directive('glyphiconInline', function ($compile) {
        return {
            restrict: 'A',
            scope: {
                type: '='
            },
            link: function (scope, element, attrs) {
                scope.initialize = true;
                scope.$watch('type', function(type) {
                    if (type && scope.initialize) {
                        var dom = $compile('<em glyphicon-enhance type=' + type + '></em>')(scope);
                        element.append(dom);
                        scope.initialze = false;
                    }
                });

            }
        };
    })
    .directive('panelColor', function() {
        return {
            restrict: 'A',
            scope: {
                color: '='
            },
            link: function (scope, element, attrs) {
                scope.initialize = true;
                scope.$watch('color', function (color) {
                    if (color && scope.initialize) {
                        element.addClass('panel');
                        element.addClass('panel-widget');
                        element.addClass('panel-' + color);
                        scope.initialze = false;
                    }
                });

            }
        }
    })

    .directive('coverageLegend', function() {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                legendTitle: '=',
                criteria: '=',
                sign: '='
            },
            template: '<div class="map-w"> \
                <ul id="colorBar"> \
                    <li class="map-w-1">{{legendTitle}}</li> \
                    <li ng-repeat="criterion in criteria" class="map-w-i" \
                        ng-style="{\'background-color\': criterion.color, \'border\': criterion.color}" style="border: 1px solid"> \
                        {{sign !== false ? "&lt;" : "&gt;"}}{{criterion.threshold}} \
                    </li> \
                    <li class="map-w-i" style="background-color: #077f07; border: 1px solid #077f07"> \
                        {{sign !== false ? "&gt;=" : "&lt;="}}{{criteria[criteria.length-1].threshold}} \
                    </li> \
                </ul> \
            </div>'
        };
    })
    .directive('simpleLegend', function() {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                legendTitle: '=',
                criteria: '='
            },
            template: '<div class="map-w"> \
                <ul id="colorBar"> \
                    <li class="map-w-1">{{legendTitle}}</li> \
                    <li ng-repeat="criterion in criteria" class="map-w-i" \
                        ng-style="{\'background-color\': criterion.color, \'border\': criterion.color}" style="border: 1px solid"> \
                        {{criterion.threshold}} \
                    </li> \
                </ul> \
            </div>'
        };
    });
    