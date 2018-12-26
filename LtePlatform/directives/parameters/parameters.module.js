angular.module('parameters.module',
[
    'parameters.eNodeb.module', 'parameters.bts.module',
    'parameters.cell.module', 'parameters.cell.rru.module', 'parameters.cdma.cell.module'
]);

angular.module('parameters.eNodeb.module', ['ui.grid', 'myApp.region', 'myApp.url', 'myApp.kpi'])
    .controller('LteENodebController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'eNodebId', name: 'LTE基站编号' },
                    { field: 'name', name: '基站名称', width: 150 },
                    { field: 'planNum', name: '规划编号' },
                    { field: 'openDate', name: '入网日期', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'address', name: '地址', width: 300, enableColumnResizing: false },
                    { field: 'factory', name: '厂家' },
                    { field: 'isInUse', name: '是否在用', cellFilter: 'yesNoChinese' }
                ],
                data: []
            };
        })
    .directive('eNodebTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'LteENodebController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('ENodebPlainController',
        function($scope, workItemDialog) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'eNodebId', name: 'LTE基站编号' },
                    { field: 'name', name: '基站名称', width: 120 },
                    { field: 'planNum', name: '规划编号' },
                    { field: 'openDate', name: '入网日期', cellFilter: 'date: "yyyy-MM-dd"' },
                    { field: 'address', name: '地址', width: 300, enableColumnResizing: false },
                    { field: 'factory', name: '厂家' },
                    {
                        name: 'IP',
                        cellTemplate: '<span class="text-primary">{{row.entity.ip.addressString}}</span>',
                        width: 100
                    },
                    {
                        name: '是否在用',
                        cellTemplate:
                            '<span ng-class="{\'btn-default\': !row.entity.isInUse, \'btn-success\': row.entity.isInUse}">\
						{{row.entity.isInUse}}</span>'
                    },
                    {
                        name: '查询',
                        cellTemplate:
                            '<a ng-click="grid.appScope.showFlow(row.entity)" class="btn btn-sm btn-default">流量查询</a>'
                    }
                ],
                data: []
            };
            $scope.showFlow = function(eNodeb) {
                workItemDialog.showENodebFlow(eNodeb, $scope.beginDate, $scope.endDate);
            };
        })
    .directive('eNodebPlainTable',
        function($compile, calculateService) {
            return calculateService.generatePagingGridDirective({
                    controllerName: 'ENodebPlainController',
                    scope: {
                        items: '=',
                        beginDate: '=',
                        endDate: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('ENodebSelectionController',
        function($scope) {
            $scope.gridOptions = {
                enableRowSelection: true,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                showGridFooter: true
            };
            $scope.gridOptions.multiSelect = true;
            $scope.gridOptions.columnDefs = [
                { field: 'eNodebId', name: 'LTE基站编号' },
                { field: 'name', name: '基站名称', width: 120 },
                { field: 'planNum', name: '规划编号' },
                { field: 'openDate', name: '入网日期', cellFilter: 'date: "yyyy-MM-dd"' },
                { field: 'address', name: '地址', width: 300, enableColumnResizing: false },
                { field: 'factory', name: '厂家' },
                {
                    name: 'IP',
                    cellTemplate: '<span class="text-primary">{{row.entity.ip.addressString}}</span>',
                    width: 100
                },
                { name: '与中心距离', field: 'distance', cellFilter: 'number: 2' }
            ];

            $scope.gridOptions.data = [];
            $scope.gridOptions.onRegisterApi = function(gridApi) {
                $scope.gridApi = gridApi;
            };
        })
    .directive('eNodebSelectionTable',
        function($compile, calculateService) {
            return calculateService.generateSelectionGridDirective({
                    controllerName: 'ENodebSelectionController',
                    scope: {
                        items: '=',
                        gridApi: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('parameters.bts.module', ['ui.grid', 'myApp.region', 'myApp.url', 'myApp.kpi'])
    .controller('CdmaBtsController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    { field: 'btsId', name: 'CDMA基站编号' },
                    { field: 'name', name: '基站名称', width: 150 },
                    { field: 'bscId', name: 'BSC编号' },
                    { field: 'districtName', name: '区域' },
                    { field: 'townName', name: '镇区' },
                    {
                        field: 'address',
                        name: '地址',
                        width: 300,
                        enableColumnResizing: false,
                        cellTooltip: function(row) {
                            return row.entity.address;
                        }
                    },
                    { field: 'isInUse', name: '是否在用', cellFilter: 'yesNoChinese' }
                ],
                data: []
            };
        })
    .directive('btsTable',
        function($compile, calculateService) {
            return calculateService.generateSelectionGridDirective({
                    controllerName: 'CdmaBtsController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('BtsPlainController',
        function($scope) {
            $scope.gridOptions = {
                paginationPageSizes: [20, 40, 60],
                paginationPageSize: 20,
                columnDefs: [
                    { field: 'btsId', name: 'CDMA基站编号' },
                    { field: 'name', name: '基站名称', width: 120 },
                    { field: 'btsId', name: 'BSC编号' },
                    { field: 'longtitute', name: '经度' },
                    { field: 'lattitute', name: '纬度' },
                    { field: 'address', name: '地址', width: 300, enableColumnResizing: false },
                    { field: 'isInUse', name: '是否在用', cellFilter: 'yesNoChinese' }
                ],
                data: []
            };
        })
    .directive('btsPlainTable',
        function($compile, calculateService) {
            return calculateService.generatePagingGridDirective({
                    controllerName: 'BtsPlainController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('BtsSelectionController',
        function($scope) {
            $scope.gridOptions = {
                enableRowSelection: true,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                showGridFooter: true
            };
            $scope.gridOptions.multiSelect = true;
            $scope.gridOptions.columnDefs = [
                { field: 'btsId', name: 'CDMA基站编号' },
                { field: 'name', name: '基站名称', width: 120 },
                { field: 'btsId', name: 'BSC编号' },
                { field: 'longtitute', name: '经度' },
                { field: 'lattitute', name: '纬度' },
                { field: 'address', name: '地址', width: 300, enableColumnResizing: false },
                { field: 'isInUse', name: '是否在用', cellFilter: 'yesNoChinese' },
                { name: '与中心距离', field: 'distance', cellFilter: 'number: 2' }
            ];

            $scope.gridOptions.data = [];
            $scope.gridOptions.onRegisterApi = function(gridApi) {
                $scope.gridApi = gridApi;
            };
        })
    .directive('btsSelectionTable',
        function($compile, calculateService) {
            return calculateService.generateSelectionGridDirective({
                    controllerName: 'BtsSelectionController',
                    scope: {
                        items: '=',
                        gridApi: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('parameters.cell.module', ['ui.grid', 'myApp.region', 'myApp.url', 'myApp.kpi'])
    .controller('CellDialogController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    {
                        name: '小区名称',
                        cellTemplate:
                            '<span class="text-primary">{{row.entity.eNodebName}}-{{row.entity.sectorId}}</span>'
                    },
                    { field: 'frequency', name: '频点' },
                    { field: 'pci', name: 'PCI' },
                    { field: 'prach', name: 'PRACH' },
                    { field: 'rsPower', name: 'RS功率' },
                    { field: 'indoor', name: '室内外' },
                    { field: 'height', name: '高度' },
                    { field: 'azimuth', name: '方位角' },
                    { field: 'downTilt', name: '下倾' },
                    { field: 'antennaGain', name: '天线增益(dB)' }
                ],
                data: []
            };
        })
    .directive('cellDialogTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CellDialogController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('CellSelectionController',
        function($scope) {
            $scope.gridOptions = {
                enableRowSelection: true,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                showGridFooter: true
            };
            $scope.gridOptions.multiSelect = true;
            $scope.gridOptions.columnDefs = [
                {
                    name: '小区名称',
                    cellTemplate: '<span class="text-primary">{{row.entity.eNodebName}}-{{row.entity.sectorId}}</span>'
                },
                { field: 'frequency', name: '频点' },
                { field: 'pci', name: 'PCI' },
                { field: 'tac', name: 'TAC' },
                { field: 'prach', name: 'PRACH' },
                { field: 'rsPower', name: 'RS功率' },
                { field: 'indoor', name: '室内外' },
                { field: 'height', name: '高度' },
                { field: 'azimuth', name: '方位角' },
                { field: 'downTilt', name: '下倾' },
                { field: 'antennaGain', name: '天线增益(dB)' }
            ];
            $scope.gridOptions.data = [];
            $scope.gridOptions.onRegisterApi = function(gridApi) {
                $scope.gridApi = gridApi;
            };
        })
    .directive('cellSelectionTable',
        function($compile, calculateService) {
            return calculateService.generateSelectionGridDirective({
                    controllerName: 'CellSelectionController',
                    scope: {
                        items: '=',
                        gridApi: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('parameters.cell.rru.module', ['ui.grid', 'myApp.region', 'myApp.url', 'myApp.kpi'])
    .controller('CellRruController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    {
                        name: '小区名称',
                        cellTemplate:
                            '<span class="text-primary">{{row.entity.eNodebName}}-{{row.entity.sectorId}}</span>'
                    },
                    { field: 'frequency', name: '频点' },
                    {
                        name: 'RRU名称',
                        cellTemplate: '<span class="text-primary">{{row.entity.rruName}}</span>'
                    },
                    { field: 'eNodebId', name: '基站编号' },
                    { field: 'antennaFactoryDescription', name: '天线厂家' },
                    { field: 'indoor', name: '室内外' },
                    { field: 'antennaInfo', name: '天线信息' },
                    { field: 'antennaModel', name: '天线型号' },
                    { field: 'downTilt', name: '下倾' },
                    { field: 'antennaGain', name: '天线增益(dB)' }
                ],
                data: []
            };
        })
    .directive('cellRruTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CellRruController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('CellRruSelectionController',
        function($scope) {
            $scope.gridOptions = {
                enableRowSelection: true,
                enableSelectAll: true,
                selectionRowHeaderWidth: 35,
                rowHeight: 35,
                showGridFooter: true
            };
            $scope.gridOptions.multiSelect = true;
            $scope.gridOptions.columnDefs = [
                {
                    name: '小区名称',
                    cellTemplate: '<span class="text-primary">{{row.entity.cellName}}</span>'
                },
                { name: '方位角', field: 'azimuth' },
                { name: '下倾角', field: 'downTilt' },
                { name: '频点', field: 'frequency' },
                { name: '天线挂高', field: 'height' },
                { name: '室内外', field: 'indoor' },
                {
                    name: 'RRU名称',
                    cellTemplate: '<span class="text-primary">{{row.entity.rruName}}</span>'
                },
                { name: '与中心点距离', field: 'distance' }
            ];

            $scope.gridOptions.data = [];
            $scope.gridOptions.onRegisterApi = function(gridApi) {
                $scope.gridApi = gridApi;
            };
        })
    .directive('cellRruSelectionTable',
        function($compile, calculateService) {
            return calculateService.generateSelectionGridDirective({
                    controllerName: 'CellRruSelectionController',
                    scope: {
                        items: '=',
                        gridApi: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('CellsBasicInfoController',
        function($scope, networkElementService) {
            $scope.gridOptions = {
                columnDefs: [
                    {
                        name: '小区名称',
                        field: 'cellName'
                    },
                    {
                        field: 'frequency',
                        name: '频点',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        }
                    },
                    {
                        field: 'indoor',
                        name: '室内外',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        }
                    },
                    {
                        field: 'height',
                        name: '天线挂高',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        }
                    },
                    {
                        field: 'azimuth',
                        name: '方位角',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        }
                    },
                    {
                        field: 'downTilt',
                        name: '下倾',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        }
                    },
                    {
                        field: 'antennaGain',
                        name: '天线增益(dB)',
                        cellTooltip: function(row, col) {
                            return row.entity.otherInfos;
                        },
                        headerTooltip: function(col) {
                            return col.displayName;
                        }
                    }, {
                        name: '查询',
                        cellTemplate:
                            '<button class="btn btn-sm btn-primary" ng-click="grid.appScope.showCurrent(row.entity)"> \
							<span class="glyphicon glyphicon-search"></span>详细信息 \
						</button>'
                    }
                ],
                data: []
            };
            $scope.showCurrent = function(cell) {
                networkElementService.queryCellInfo(cell.eNodebId, cell.sectorId).then(function(result) {
                    $scope.currentCell = result;
                });
            };
        })
    .directive('cellsBasicInfoTable',
        function($compile, calculateService) {
            return calculateService.generateShortGridDirective({
                    controllerName: 'CellsBasicInfoController',
                    scope: {
                        items: '=',
                        currentCell: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        });

angular.module('parameters.cdma.cell.module', ['ui.grid', 'myApp.region', 'myApp.url', 'myApp.kpi'])
    .controller('CdmaCellController',
        function($scope) {
            $scope.gridOptions = {
                columnDefs: [
                    {
                        name: '小区名称',
                        cellTemplate: '<span class="text-primary">{{row.entity.btsName}}-{{row.entity.sectorId}}</span>'
                    },
                    { field: 'frequency', name: '频点' },
                    { field: 'cellType', name: '小区类别' },
                    { field: 'frequencyList', name: '频点列表' },
                    { field: 'cellId', name: '小区编号' },
                    { field: 'lac', name: 'LAC' },
                    { field: 'pn', name: 'PN' },
                    { field: 'height', name: '高度' },
                    { field: 'azimuth', name: '方位角' },
                    { field: 'downTilt', name: '下倾' },
                    { field: 'antennaGain', name: '天线增益(dB)' }
                ],
                data: []
            };
        })
    .directive('cdmaCellTable',
        function($compile, calculateService) {
            return calculateService.generateGridDirective({
                    controllerName: 'CdmaCellController',
                    scope: {
                        items: '='
                    },
                    argumentName: 'items'
                },
                $compile);
        })
    .controller('CellSectorDetailsController',
        function($scope, calculateService) {
            $scope.$watch('cell',
                function(cell) {
                    if (cell) {
                        $scope.itemGroups = calculateService.generateCellDetailsGroups($scope.cell);
                    }
                });

        })
    .directive('cellSectorDetails',
        function() {
            return {
                controller: 'CellSectorDetailsController',
                restrict: 'EA',
                replace: true,
                scope: {
                    cell: '='
                },
                templateUrl: '/appViews/Home/GeneralTableDetails.html'
            };
        });
