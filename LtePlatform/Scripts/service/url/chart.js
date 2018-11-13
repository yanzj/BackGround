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