angular.module('myApp', ['app.common'])
    .config([
        '$routeProvider', function($routeProvider) {
            var viewDir = "/appViews/Test/Simple/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "Index.html",
                    controller: "qunit.index"
                })
                .when('/main', {
                    templateUrl: viewDir + "Main.html",
                    controller: "qunit.main"
                })
                .when('/legacy', {
                    templateUrl: viewDir + "LegacyMarkup.html",
                    controller: "legacy.markup"
                })
                .when('/filter', {
                    templateUrl: viewDir + "GridFilter.html",
                    controller: "grid.filter"
                })
                .when('/footer', {
                    templateUrl: viewDir + "GridFooter.html",
                    controller: "grid.footer"
                })
                .when('/binding', {
                    templateUrl: viewDir + "GridBinding.html",
                    controller: "grid.binding"
                })
                .when('/cellClass', {
                    templateUrl: viewDir + "CellClass.html",
                    controller: "cell.class"
                })
                .when('/tooltip', {
                    templateUrl: viewDir + "GridTooltip.html",
                    controller: "grid.tooltip"
                })
                .when('/menu', {
                    templateUrl: viewDir + "GridMenu.html",
                    controller: "grid.menu"
                })
                .when('/template', {
                    templateUrl: viewDir + "GridBinding.html",
                    controller: "grid.template"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/TestPage/QUnitTest#";
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "",
            introduction: ""
        };
        $rootScope.menuItems = [
            {
                displayName: "QUnit and Ui-Grid测试",
                isActive: true,
                subItems: [
                    {
                        displayName: "QUnit例子",
                        url: rootUrl + "/",
                        tooltip: "使用QUNnit进行测试的代码。"
                    }, {
                        displayName: "第一个ui-grid",
                        url: rootUrl + "/main",
                        tooltip: "第一个ui-grid"
                    }, {
                        displayName: "ui-grid的排序功能",
                        url: rootUrl + "/legacy"
                    }, {
                        displayName: "ui-grid的过滤功能",
                        url: rootUrl + "/filter"
                    }, {
                        displayName: "ui-grid的表脚",
                        url: rootUrl + "/footer"
                    }, {
                        displayName: "ui-grid的绑定",
                        url: rootUrl + "/binding"
                    }, {
                        displayName: "单元格格式",
                        url: rootUrl + '/cellClass'
                    }, {
                        displayName: "ui-grid智能提示",
                        url: rootUrl + '/tooltip'
                    }, {
                        displayName: "ui-grid输出菜单",
                        url: rootUrl + '/menu'
                    }, {
                        displayName: "ui-grid模板",
                        url: rootUrl + '/template'
                    }
                ]
            }
        ];
        require.config({
            paths: {
                echarts: 'http://echarts.baidu.com/build/dist'
            }
        });
    })
    .controller('cell.class', [
        '$scope', '$http', function($scope, $http) {
            $scope.page.title = '单元格格式';
            $scope.gridOptions = {
                enableSorting: true,
                columnDefs: [
                    { field: 'name', cellClass: 'red' },
                    {
                        field: 'company',
                        cellClass: function(grid, row, col, rowRenderIndex, colRenderIndex) {
                            if (grid.getCellValue(row, col) === 'Velity') {
                                return 'blue';
                            }
                        }
                    }
                ]
            };

            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100.json')
                .success(function(data) {
                    $scope.gridOptions.data = data;
                });
        }
    ])
    .controller('grid.binding', [
        '$scope', function($scope) {

            $scope.page.title = 'ui-grid的绑定';
            $scope.gridOptions = {
                enableSorting: true,
                columnDefs: [
                    { name: 'firstName', field: 'first-name' },
                    { name: '1stFriend', field: 'friends[0]' },
                    { name: 'city', field: 'address.city' },
                    { name: 'getZip', field: 'getZip()', enableCellEdit: false }
                ],
                data: [
                    {
                        "first-name": "Cox",
                        "friends": ["friend0"],
                        "address": { street: "301 Dove Ave", city: "Laurel", zip: "39565" },
                        "getZip": function() { return this.address.zip; }
                    }
                ]
            };

        }
    ])
    .controller('grid.filter', [
        '$scope', '$http', 'uiGridConstants', function($scope, $http, uiGridConstants) {

            $scope.page.title = 'ui-grid的过滤功能';
            var today = new Date();
            var nextWeek = new Date();
            nextWeek.setDate(nextWeek.getDate() + 7);

            $scope.highlightFilteredHeader = function(row, rowRenderIndex, col, colRenderIndex) {
                if (col.filters[0].term) {
                    return 'header-filtered';
                } else {
                    return '';
                }
            };

            $scope.gridOptions = {
                enableFiltering: true,
                onRegisterApi: function(gridApi) {
                    $scope.gridApi = gridApi;
                },
                columnDefs: [
                    // default
                    { field: 'name', headerCellClass: $scope.highlightFilteredHeader },
                    // pre-populated search field
                    {
                        field: 'gender',
                        filter: {
                            term: '1',
                            type: uiGridConstants.filter.SELECT,
                            selectOptions: [{ value: '1', label: 'male' }, { value: '2', label: 'female' }, { value: '3', label: 'unknown' }, { value: '4', label: 'not stated' }, { value: '5', label: 'a really long value that extends things' }]
                        },
                        cellFilter: 'mapGender',
                        headerCellClass: $scope.highlightFilteredHeader
                    },
                    // no filter input
                    {
                        field: 'company',
                        enableFiltering: false,
                        filter: {
                            noTerm: true,
                            condition: function(searchTerm, cellValue) {
                                return cellValue.match(/a/);
                            }
                        }
                    },
                    // specifies one of the built-in conditions
                    // and a placeholder for the input
                    {
                        field: 'email',
                        filter: {
                            condition: uiGridConstants.filter.ENDS_WITH,
                            placeholder: 'ends with'
                        },
                        headerCellClass: $scope.highlightFilteredHeader
                    },
                    // custom condition function
                    {
                        field: 'phone',
                        filter: {
                            condition: function(searchTerm, cellValue) {
                                var strippedValue = (cellValue + '').replace(/[^\d]/g, '');
                                return strippedValue.indexOf(searchTerm) >= 0;
                            }
                        },
                        headerCellClass: $scope.highlightFilteredHeader
                    },
                    // multiple filters
                    {
                        field: 'age',
                        filters: [
                            {
                                condition: uiGridConstants.filter.GREATER_THAN,
                                placeholder: 'greater than'
                            },
                            {
                                condition: uiGridConstants.filter.LESS_THAN,
                                placeholder: 'less than'
                            }
                        ],
                        headerCellClass: $scope.highlightFilteredHeader
                    },
                    // date filter
                    {
                        field: 'mixedDate',
                        cellFilter: 'date',
                        width: '15%',
                        filter: {
                            condition: uiGridConstants.filter.LESS_THAN,
                            placeholder: 'less than',
                            term: nextWeek
                        },
                        headerCellClass: $scope.highlightFilteredHeader
                    },
                    {
                        field: 'mixedDate',
                        displayName: "Long Date",
                        cellFilter: 'date:"longDate"',
                        filterCellFiltered: true,
                        width: '15%',
                    }
                ]
            };

            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/500_complex.json')
                .success(function(data) {
                    $scope.gridOptions.data = data;
                    $scope.gridOptions.data[0].age = -5;

                    data.forEach(function addDates(row, index) {
                        row.mixedDate = new Date();
                        row.mixedDate.setDate(today.getDate() + (index % 14));
                        row.gender = row.gender === 'male' ? '1' : '2';
                    });
                });

            $scope.toggleFiltering = function() {
                $scope.gridOptions.enableFiltering = !$scope.gridOptions.enableFiltering;
                $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
            };
        }
    ])
    .filter('mapGender', function() {
        var genderHash = {
            1: 'male',
            2: 'female'
        };

        return function(input) {
            if (!input) {
                return '';
            } else {
                return genderHash[input];
            }
        };
    })
    .controller("grid.footer", function($scope, uiGridConstants, $http) {
        $scope.page.title = "ui-grid的表脚";

        $scope.gridOptions = {
            showGridFooter: true,
            showColumnFooter: true,
            enableFiltering: true,
            columnDefs: [
                { field: 'name', width: '13%' },
                { field: 'address.street', aggregationType: uiGridConstants.aggregationTypes.sum, width: '13%' },
                { field: 'age', aggregationType: uiGridConstants.aggregationTypes.avg, aggregationHideLabel: true, width: '13%' },
                { name: 'ageMin', field: 'age', aggregationType: uiGridConstants.aggregationTypes.min, width: '13%', displayName: 'Age for min' },
                { name: 'ageMax', field: 'age', aggregationType: uiGridConstants.aggregationTypes.max, width: '13%', displayName: 'Age for max' },
                { name: 'customCellTemplate', field: 'age', width: '14%', footerCellTemplate: '<div class="ui-grid-cell-contents" style="background-color: Red;color: White">custom template</div>' },
                { name: 'registered', field: 'registered', width: '20%', cellFilter: 'date', footerCellFilter: 'date', aggregationType: uiGridConstants.aggregationTypes.max }
            ],
            data: [],
            onRegisterApi: function(gridApi) {
                $scope.gridApi = gridApi;
            }
        };

        $scope.toggleFooter = function() {
            $scope.gridOptions.showGridFooter = !$scope.gridOptions.showGridFooter;
            $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.OPTIONS);
        };

        $scope.toggleColumnFooter = function() {
            $scope.gridOptions.showColumnFooter = !$scope.gridOptions.showColumnFooter;
            $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.OPTIONS);
        };

        $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/500_complex.json')
            .success(function(data) {
                data.forEach(function(row) {
                    row.registered = Date.parse(row.registered);
                });
                $scope.gridOptions.data = data;
            });
    })
    .controller('grid.menu', [
        '$scope', '$http', '$interval', '$q', function($scope, $http, $interval, $q) {
            $scope.page.title = 'ui-grid输出菜单';
            var fakeI18n = function(title) {
                var deferred = $q.defer();
                $interval(function() {
                    deferred.resolve('col: ' + title);
                }, 1000, 1);
                return deferred.promise;
            };

            $scope.gridOptions = {
                exporterMenuCsv: true,
                enableGridMenu: true,
                gridMenuTitleFilter: fakeI18n,
                columnDefs: [
                    { name: 'name' },
                    { name: 'gender', enableHiding: false },
                    { name: 'company' }
                ],
                gridMenuCustomItems: [
                    {
                        title: 'Rotate Grid',
                        action: function($event) {
                            this.grid.element.toggleClass('rotated');
                        },
                        order: 210
                    }
                ],
                onRegisterApi: function(gridApi) {
                    $scope.gridApi = gridApi;

                    // interval of zero just to allow the directive to have initialized
                    $interval(function() {
                        gridApi.core.addToGridMenu(gridApi.grid, [{ title: 'Dynamic item', order: 100 }]);
                    }, 0, 1);

                    gridApi.core.on.columnVisibilityChanged($scope, function(changedColumn) {
                        $scope.columnChanged = { name: changedColumn.colDef.name, visible: changedColumn.colDef.visible };
                    });
                }
            };

            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100.json')
                .success(function(data) {
                    $scope.gridOptions.data = data;
                });
        }
    ])
    .controller('grid.template', [
        '$scope', '$log', '$http', function($scope, $log, $http) {

            $scope.page.title = 'ui-grid模板';
            $scope.someProp = 'abc',
                $scope.showMe = function(val) {
                    alert(val);
                };

            $scope.gridOptions = {};

            //you can override the default assignment if you wish
            //$scope.gridOptions.appScopeProvider = someOtherReference;

            $scope.gridOptions.columnDefs = [
                { field: 'name', name: '姓名' },
                { name: 'gender' },
                {
                    name: 'ShowScope',
                    cellTemplate: '<button class="btn primary" ng-click="grid.appScope.showMe(row.entity.gender)">Click Me</button>'
                },
                {
                    name: 'ShowGender',
                    cellTemplate: '<a href="">{{row.entity.gender}}</a>'
                }
            ];
            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100.json')
                .success(function(data) {
                    $scope.gridOptions.data = data;
                });

        }
    ])
    .controller('grid.tooltip', [
        '$scope', '$http', 'uiGridConstants', function($scope, $http, uiGridConstants) {
            $scope.page.title = 'ui-grid智能提示';
            $scope.gridOptions = {
                enableSorting: true,
                columnDefs: [
                    { field: 'name', cellTooltip: 'Custom string', headerTooltip: 'Custom header string' },
                    {
                        field: 'company',
                        cellTooltip:
                            function(row, col) {
                                return 'Name: ' + row.entity.name + ' Company: ' + row.entity.company;
                            },
                        headerTooltip:
                            function(col) {
                                return 'Header: ' + col.displayName;
                            }
                    },
                    { field: 'gender', cellTooltip: true, headerTooltip: true, cellFilter: 'mapGender' },
                ],
                onRegisterApi: function(gridApi) {
                    $scope.gridApi = gridApi;
                    $scope.gridApi.core.on.sortChanged($scope, function(grid, sort) {
                        $scope.gridApi.core.notifyDataChange(uiGridConstants.dataChange.COLUMN);
                    });
                }
            };

            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100.json')
                .success(function(data) {
                    data.forEach(function setGender(row, index) {
                        row.gender = row.gender === 'male' ? '1' : '2';
                    });

                    $scope.gridOptions.data = data;
                });
        }
    ])
    .filter('mapGender', function() {
        var genderHash = {
            1: 'male',
            2: 'female'
        };

        return function(input) {
            if (!input) {
                return '';
            } else {
                return genderHash[input];
            }
        };
    })
    .controller('legacy.markup', [
        '$scope', '$http', 'uiGridConstants', function($scope, $http, uiGridConstants) {
            $scope.page.title = "ui-grid的排序功能";
            $scope.gridOptions1 = {
                enableSorting: true,
                columnDefs: [
                    { field: 'name' },
                    { field: 'gender' },
                    { field: 'company', enableSorting: false }
                ],
                onRegisterApi: function(gridApi) {
                    $scope.grid1Api = gridApi;
                }
            };

            $scope.toggleGender = function() {
                if ($scope.gridOptions1.data[64].gender === 'male') {
                    $scope.gridOptions1.data[64].gender = 'female';
                } else {
                    $scope.gridOptions1.data[64].gender = 'male';
                };
                $scope.grid1Api.core.notifyDataChange(uiGridConstants.dataChange.EDIT);
            };

            $scope.gridOptions2 = {
                enableSorting: true,
                onRegisterApi: function(gridApi) {
                    $scope.grid2Api = gridApi;
                },
                columnDefs: [
                    {
                        field: 'name',
                        sort: {
                            direction: uiGridConstants.DESC,
                            priority: 1
                        }
                    },
                    {
                        field: 'gender',
                        sort: {
                            direction: uiGridConstants.ASC,
                            priority: 0,
                        },
                        suppressRemoveSort: true,
                        sortingAlgorithm: function(a, b, rowA, rowB, direction) {
                            var nulls = $scope.grid2Api.core.sortHandleNulls(a, b);
                            if (nulls !== null) {
                                return nulls;
                            } else {
                                if (a === b) {
                                    return 0;
                                }
                                if (a === 'male') {
                                    return 1;
                                }
                                if (b === 'male') {
                                    return -1;
                                }
                                if (a == 'female') {
                                    return 1;
                                }
                                if (b === 'female') {
                                    return -1;
                                }
                                return 0;
                            }
                        }
                    },
                    { field: 'company', enableSorting: false }
                ]
            };

            $http.get('https://cdn.rawgit.com/angular-ui/ui-grid.info/gh-pages/data/100.json')
                .success(function(data) {
                    $scope.gridOptions1.data = data;
                    $scope.gridOptions2.data = data;
                });
        }
    ])
    .controller("qunit.index", function($scope) {
        $scope.page.title = "QUnit-ECharts例子";
        require(
           [
               'echarts',
               'echarts/chart/bar' // 使用柱状图就加载bar模块，按需加载
           ],
           function (ec) {
               // 基于准备好的dom，初始化echarts图表
               var myChart = ec.init(document.getElementById('main'));

               var option = {
                   tooltip: {
                       show: true
                   },
                   legend: {
                       data: ['销量']
                   },
                   xAxis: [
                       {
                           type: 'category',
                           data: ["衬衫", "羊毛衫", "雪纺衫", "裤子", "高跟鞋", "袜子"]
                       }
                   ],
                   yAxis: [
                       {
                           type: 'value'
                       }
                   ],
                   series: [
                       {
                           "name": "销量",
                           "type": "bar",
                           "data": [5, 20, 40, 10, 10, 20]
                       }
                   ]
               };

               // 为echarts对象加载数据 
               myChart.setOption(option);
           }
       );
    })
    .controller("qunit.main", function($scope) {
        $scope.page.title = "第一个ui-grid";
        $scope.myData = [
            {
                "firstName": "Cox",
                "lastName": "Carney",
                "company": "Enormo",
                "employed": true
            },
            {
                "firstName": "Lorraine",
                "lastName": "Wise",
                "company": "Comveyer",
                "employed": false
            },
            {
                "firstName": "Nancy",
                "lastName": "Waters",
                "company": "Fuelton",
                "employed": false
            }
        ];
    });