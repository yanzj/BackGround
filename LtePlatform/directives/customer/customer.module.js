angular.module('customer.module',
        ['customer.emergency.module', 'customer.vip.module', 'customer.complain.module', 'customer.complain.module']);

angular.module('customer.emergency.module', ['myApp.region', 'myApp.kpi'])
    .controller('EmergencyCommunicationController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'projectName', name: '项目名称' },
                    { field: 'beginDate', name: '开始日期', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'endDate', name: '结束日期', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'demandLevelDescription', name: '需求等级' },
                    { field: 'vehicularTypeDescription', name: '通信车类型' },
                    { field: 'expectedPeople', name: '预计人数' },
                    {
                        name: '工单处理',
                        cellTemplate:
                            '<a ng-href="{{grid.appScope.rootPath}}emergency/process/{{row.entity.id}}" class="btn btn-sm btn-success">{{row.entity.currentStateDescription}}</a>'
                    }
                ],
                data: []
            };
        })
    .directive('emergencyCommunicationList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controller: 'EmergencyCommunicationController',
                    scope: {
                        items: '=',
                        rootPath: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('BranchListController',
        function($scope) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'serialNumber', name: '序列号' },
                    { field: 'beginDate', name: '受理时间', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'subscriberInfo', name: '用户信息' },
                    { field: 'managerInfo', name: '客户经理' },
                    { field: 'complainContents', name: '投诉内容' },
                    { field: 'solveFunctionDescription', name: '处理措施' }
                ],
                data: []
            };
        })
    .directive('branchList',
        function($compile, calculateService) {
            return calculateService.generatePagingGridDirective({
                    controllerName: 'BranchListController',
                    scope: {
                        items: '=',
                        rootPath: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('HotSpotController',
        function($scope, customerDialogService, workItemDialog) {
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
                        <button class="btn btn-sm btn-success" ng-click="grid.appScope.editInfo(row.entity)">编辑</button> \
                        <button class="btn btn-sm btn-primary" ng-click="grid.appScope.showCells(row.entity.hotspotName)">小区</button> \
                        <button class="btn btn-sm btn-default" ng-click="grid.appScope.showFlow(row.entity)">流量</button> \
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
            $scope.showFlow = function(hotSpot) {
                workItemDialog.showHotSpotCellFlow(hotSpot, $scope.beginDate, $scope.endDate);
            };
            $scope.showCells = function(name) {
                workItemDialog.showHotSpotCells(name);
            };
        })
    .directive('hotSpotList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'HotSpotController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('OnlineListController',
        function($scope) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'serialNumber', name: '序列号' },
                    { field: 'beginDate', name: '受理时间', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'address', name: '投诉地址' },
                    { field: 'complainCategoryDescription', name: '投诉类型' },
                    { field: 'phenomenon', name: '投诉现象' },
                    { field: 'followInfo', name: '跟进信息' },
                    { field: 'dutyStaff', name: '值班人员' }
                ],
                data: []
            };
        })
    .directive('onlineList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'OnlineListController',
                    scope: {
                        items: '=',
                        rootPath: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('EmergencyProcessController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'processTime', name: '处理时间', cellFilter: 'date: "yyyy-MM-dd HH:mm:ss"' },
                    { field: 'processPerson', name: '处理人' },
                    { field: 'processStateDescription', name: '处理步骤' },
                    { field: 'processInfo', name: '反馈信息' }
                ],
                data: []
            };
        })
    .directive('emergencyProcessList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'EmergencyProcessController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('VipProcessController',
        function($scope, emergencyService, collegeDialogService, customerDialogService) {
            $scope.updateProcess = function(item) {
                if ($scope.collegeName && item.vipStateDescription === '现场测试') {
                    collegeDialogService.processTest($scope.collegeName,
                        function(info) {
                            item.processInfo = info;
                            emergencyService.finishVipProcess(item).then(function() {
                                $scope.query();
                            });
                        });
                } else if ($scope.collegeName && item.vipStateDescription === '通信车需求') {
                    customerDialogService.constructEmergencyCollege(item.serialNumber,
                        $scope.collegeName,
                        function() {
                            emergencyService.finishVipProcess(item).then(function() {
                                $scope.query();
                            });
                        });
                } else if ($scope.collegeName && item.vipStateDescription === '测试评估') {
                    collegeDialogService.tracePlanning($scope.collegeName,
                        function(info) {
                            item.processInfo = info;
                            emergencyService.finishVipProcess(item).then(function() {
                                $scope.query();
                            });
                        });
                } else {
                    emergencyService.finishVipProcess(item).then(function() {
                        $scope.query();
                    });
                }
            };
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'beginTime', name: '开始时间', cellFilter: 'date: "yyyy-MM-dd HH:mm:ss"' },
                    { field: 'contactPerson', name: '发起人' },
                    { field: 'vipStateDescription', name: '处理步骤' },
                    { field: 'beginInfo', name: '建单信息' },
                    { field: 'processPerson', name: '处理人' },
                    { field: 'processInfo', name: '处理信息' },
                    {
                        name: '处理',
                        cellTemplate:
                            '<button ng-disabled="row.entity.processInfo" class="btn btn-sm btn-success" ng-click="grid.appScope.updateProcess(row.entity)">处理信息</button>'
                    }
                ],
                data: []
            };
        })
    .directive('vipProcessList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'VipProcessController',
                    scope: {
                        items: '=',
                        collegeName: '=',
                        query: '&'
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('customer.process.module', ['myApp.region', 'myApp.kpi'])
    .controller('ComplainProcessController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'beginTime', name: '开始时间', cellFilter: 'date: "yyyy-MM-dd HH:mm:ss"' },
                    { field: 'contactPerson', name: '发起人' },
                    { field: 'complainStateDescription', name: '处理步骤' },
                    { field: 'beginInfo', name: '建单信息' },
                    { field: 'processPerson', name: '处理人' },
                    { field: 'processInfo', name: '处理信息' }
                ],
                data: []
            };
        })
    .directive('complainProcessList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'ComplainProcessController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('FiberItemController',
        function($scope, emergencyService) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'beginDate', name: '建单时间', cellFilter: 'date: "yyyy-MM-dd HH:mm:ss"' },
                    { field: 'finishDate', name: '完成时间', cellFilter: 'date: "yyyy-MM-dd HH:mm:ss"' },
                    { field: 'person', name: '联系人' },
                    { field: 'workItemNumber', name: '工单编号' },
                    {
                        name: '处理',
                        cellTemplate:
                            '<button ng-if="!grid.appScope.finishDate" ng-click="grid.appScope.finish(row.entity)" class="btn btn-success">完成</button>'
                    }
                ],
                data: []
            };
            $scope.finish = function(item) {
                emergencyService.finishFiberItem(item).then(function() {
                    item.finishDate = new Date();
                });
            };
        })
    .directive('fiberItemList',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'FiberItemController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .value('processTypeDictionay',
    {
        "通信车申请": 'default',
        "光纤起单": 'danger',
        "电源准备": 'primary',
        "光纤调通": 'warning',
        "通信车就位": 'info',
        "通信车开通": 'primary',
        "优化测试": 'info',
        "完成": 'success'
    })
    .directive('emergencyProcessState',
        function(processTypeDictionay) {
            return {
                restrict: 'A',
                scope: {
                    state: '='
                },
                template: '<span>{{state}}</span>',
                link: function(scope, element, attrs) {
                    element.addClass('label');

                    scope.$watch("state",
                        function(state, oldState) {
                            if (oldState) {
                                element.removeClass('label-' + processTypeDictionay[oldState]);
                            }
                            if (state) {
                                var type = processTypeDictionay[state] || 'primary';
                                element.addClass('label-' + type);
                            }
                        });
                }
            };
        })
    .value('vipTypeDictionay',
    {
        "预处理": 'default',
        "现场测试": 'danger',
        "测试评估": 'primary',
        "优化调整": 'warning',
        "新增资源": 'info',
        "通信车需求": 'primary',
        "保障结论": 'info'
    })
    .directive('vipProcessState',
        function(vipTypeDictionay) {
            return {
                restrict: 'A',
                scope: {
                    state: '='
                },
                template: '<span>{{state}}</span>',
                link: function(scope, element, attrs) {
                    element.addClass('label');

                    scope.$watch("state",
                        function(state, oldState) {
                            if (oldState) {
                                element.removeClass('label-' + vipTypeDictionay[oldState]);
                            }
                            if (state) {
                                var type = vipTypeDictionay[state] || 'primary';
                                element.addClass('label-' + type);
                            }
                        });
                }
            };
        })
    .value('complainTypeDictionay',
    {
        "生成工单": 'default',
        "预处理": 'danger',
        "预约测试": 'primary',
        "现场测试": 'warning',
        "问题处理": 'info',
        "回访用户": 'primary',
        "工单归档": 'success'
    })
    .directive('complainProcessState',
        function(complainTypeDictionay) {
            return {
                restrict: 'A',
                scope: {
                    state: '='
                },
                template: '<span>{{state}}</span>',
                link: function(scope, element, attrs) {
                    element.addClass('label');

                    scope.$watch("state",
                        function(state, oldState) {
                            if (oldState) {
                                element.removeClass('label-' + complainTypeDictionay[oldState]);
                            }
                            if (state) {
                                var type = complainTypeDictionay[state] || 'primary';
                                element.addClass('label-' + type);
                            }
                        });
                }
            };
        });

angular.module('customer.vip.module', ['myApp.region', 'kpi.customer'])
    .controller('VipDemandController', function($scope, customerDialogService) {
        $scope.supplement = function(view) {
            customerDialogService.supplementVipDemandInfo(view, $scope.city, $scope.district, $scope.messages,
                function() {
                    view.isInfoComplete = true;
                });
        };
    })
    .directive('vipDemandList', function() {
        return {
            controller: 'VipDemandController',
            restrict: 'EA',
            replace: true,
            scope: {
                items: '=',
                city: '=',
                district: '=',
                messages: '=',
                rootPath: '='
            },
            templateUrl: '/directives/customer/vip/DemandList.html'
        };
    })
    .directive('vipDemandInfo', function () {
        return {
            restrict: 'EA',
            replace: true,
            scope: {
                item: '='
            },
            templateUrl: '/directives/customer/vip/VipInfo.html'
        };
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
        })
    .directive('complainList',
        function($compile, calculateService) {
            return calculateService.generatePagingGridDirective({
                    controllerName: 'ComplainListController',
                    scope: {
                        items: '=',
                        rootPath: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('ComplainCountController',
        function($scope, complainService) {
            $scope.showComplainItems = function(district) {
                complainService.queryLastDateDistrictComplains($scope.statDate.value, district).then(function(result) {
                    $scope.data.complainList = result;
                });
            };
        })
    .directive('complainCountTable',
        function() {
            return {
                controller: 'ComplainCountController',
                restrict: 'EA',
                replace: true,
                scope: {
                    stats: '=',
                    data: '=',
                    statDate: '='
                },
                templateUrl: '/directives/customer/Complain.Tpl.html'
            };
        })
    .controller('ComplainSpanCountController',
        function($scope, complainService) {
            $scope.showComplainItems = function(district) {
                complainService.queryDateSpanDistrictComplains($scope.beginDate.value, $scope.endDate.value, district)
                    .then(function(result) {
                        $scope.data.complainList = result;
                    });
            };
        })
    .directive('complainSpanCountTable',
        function() {
            return {
                controller: 'ComplainSpanCountController',
                restrict: 'EA',
                replace: true,
                scope: {
                    stats: '=',
                    data: '=',
                    beginDate: '=',
                    endDate: '='
                },
                templateUrl: '/directives/customer/Complain.Tpl.html'
            };
        });
    