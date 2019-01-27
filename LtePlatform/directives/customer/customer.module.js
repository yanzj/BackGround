angular.module('customer.module',
        ['customer.emergency.module', 'customer.complain.module', 'customer.complain.module']);

angular.module('customer.emergency.module', ['myApp.region', 'myApp.kpi'])
    .controller('HotSpotController',
        function($scope, customerDialogService) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'hotspotName', name: '热点名称' },
                    { field: 'address', name: '地址' },
                    { field: 'typeDescription', name: '热点类型' },
                    { field: 'sourceName', name: '热点描述' },
                    { field: 'longtitute', name: '经度' },
                    { field: 'lattitute', name: '纬度' },
                    {
                        name: '信息',
                        cellTemplate: '<div class="btn-group-sm"> \
                        <button class="btn btn-sm btn-success" ng-click="grid.appScope.editInfo(row.entity)">编辑小区</button> \
                        <button class="btn btn-sm btn-primary" ng-click="grid.appScope.modifyBasic(row.entity)">基本信息</button> \
                        </div>',
                        width: 200
                    }
                ],
                data: []
            };
            $scope.editInfo = function(spot) {
                customerDialogService.manageHotSpotCells(spot,
                    function() {

                    });
            };
            $scope.modifyBasic = function(stat) {
                customerDialogService.modifyHotSpot(stat,
                    function() {
                    });
            };
        })
    .directive('hotSpotList',
        function($compile, calculateService) {
            return calculateService.generatePagingGridDirective({
                    controllerName: 'HotSpotController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('customer.complain.module', ['myApp.region'])
    .controller('ComplainPositionController',
        function($scope, customerDialogService) {
            $scope.gridOptions = {
                paginationPageSizes: [25, 50, 75],
                paginationPageSize: 25,
                columnDefs: [
                    { field: 'serialNumber', name: '工单编号' },
                    { field: 'city', name: '城市' },
                    { field: 'district', name: '区域' },
                    { field: 'buildingName', name: '楼宇名称' },
                    { field: 'roadName', name: '路名' },
                    { field: 'longtitute', name: '经度', cellFilter: 'number: 4' },
                    { field: 'lattitute', name: '纬度', cellFilter: 'number: 4' },
                    { field: 'sitePosition', name: '附近站点' },
                    {
                        name: '匹配位置',
                        cellTemplate:
                            '<button class="btn btn-default" ng-click="grid.appScope.match(row.entity)">匹配</button>'
                    }
                ],
                data: []
            };
            $scope.match = function(item) {
                customerDialogService.supplementComplainInfo(item,
                    function() {
                        $scope.messages.push({
                            type: 'success',
                            contents: '完成抱怨量工单' + item.serialNumber + '的信息补充！'
                        });
                        $scope.query();
                    });
            };
        })
    .directive('complainPositionList',
        function($compile) {
            return {
                controller: 'ComplainPositionController',
                restrict: 'EA',
                replace: true,
                scope: {
                    items: '=',
                    messages: '=',
                    query: '&'
                },
                template: '<div></div>',
                link: function(scope, element, attrs) {
                    scope.initialize = false;
                    scope.$watch('items',
                        function(items) {
                            scope.gridOptions.data = items;
                            if (!scope.initialize) {
                                var linkDom =
                                    $compile('<div ui-grid="gridOptions" ui-grid-pagination style="height: 800px"></div>')(scope);
                                element.append(linkDom);
                                scope.initialize = true;
                            }
                        });
                }
            };
        })
    .controller('ComplainListController',
        function($scope, mapDialogService) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'serialNumber', name: '序列号' },
                    { field: 'beginTime', name: '受理时间', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'buildingName', name: '楼宇名称' },
                    { field: 'roadName', name: '道路名称' },
                    { field: 'complainSourceDescription', name: '投诉来源' },
                    { field: 'networkTypeDescription', name: '网络类型' },
                    {
                        name: '详细信息',
                        cellTemplate:
                            '<a href="" ng-click="grid.appScope.showDetails(row.entity)" class="btn btn-sm btn-success">详细信息</a>'
                    },
                    {
                        name: '工单处理',
                        cellTemplate:
                            '<a href="" ng-click="grid.appScope.complainProcess(row.entity.serialNumber)" class="btn btn-sm btn-success">\
                        {{row.entity.currentStateDescription}}</a>'
                    }
                ],
                data: []
            };
            $scope.showDetails = function(item) {
                mapDialogService.showComplainDetails(item);
            };
        });
    