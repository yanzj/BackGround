angular.module("myApp", ['app.common'])
    .config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix('');
            var viewDir = "/appViews/Parameters/Import/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "Index.html",
                    controller: "import.index"
                })
                .when('/eNodebInfos', {
                    templateUrl: viewDir + "ENodebInfos.html",
                    controller: "import.eNodebs"
                })
                .when('/eNodebLonLat', {
                    templateUrl: viewDir + "ENodebLonLatTable.html",
                    controller: "eNodeb.lonLat"
                })
                .when('/cellInfos', {
                    templateUrl: viewDir + "CellInfos.html",
                    controller: "import.cells"
                })
                .when('/cellLonLat', {
                    templateUrl: viewDir + "CellLonLatTable.html",
                    controller: "cell.lonLat"
                })
                .when('/vanishedENodebInfos', {
                    templateUrl: viewDir + "VanishedENodebs.html",
                    controller: "eNodeb.vanished"
                })
                .when('/vanishedCellInfos', {
                    templateUrl: viewDir + "VanishedCellInfos.html",
                    controller: "cell.vanished"
                })
                .when('/btsInfos', {
                    templateUrl: viewDir + "BtsInfos.html",
                    controller: "import.btss"
                })
                .when('/btsLonLat', {
                    templateUrl: viewDir + "BtsLonLatTable.html",
                    controller: "bts.lonLat"
                })
                .when('/cdmaCellInfos', {
                    templateUrl: viewDir + "CdmaCellInfos.html",
                    controller: "import.cdmaCells"
                })
                .when('/cdmaCellLonLat', {
                    templateUrl: viewDir + "CdmaCellLonLatTable.html",
                    controller: "cdmaCell.lonLat"
                })
                .when('/vanishedBtsInfos', {
                    templateUrl: viewDir + "VanishedBtss.html",
                    controller: "bts.vanished"
                })
                .when('/vanishedCdmaCellInfos', {
                    templateUrl: viewDir + "VanishedCdmaCellInfos.html",
                    controller: "cdmaCell.vanished"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope, basicImportService) {
        $rootScope.rootPath = "/Parameters/BasicImport#/";
        $rootScope.importData = {
            newENodebs: [],
            newCells: [],
            newBtss: [],
            newCdmaCells: [],
            vanishedENodebIds: [],
            vanishedCellIds: [],
            vanishedBtsIds: [],
            vanishedCdmaCellIds: [],
            updateMessages: []
        };

        $rootScope.closeAlert = function(index) {
            $rootScope.importData.updateMessages.splice(index, 1);
        };

        basicImportService.queryENodebExcels().then(function(data) {
            $rootScope.importData.newENodebs = data;
        });
        basicImportService.queryCellExcels().then(function(data) {
            $rootScope.importData.newCells = data;
        });
        basicImportService.queryBtsExcels().then(function(data) {
            $rootScope.importData.newBtss = data;
        });
        basicImportService.queryCdmaCellExcels().then(function(data) {
            $rootScope.importData.newCdmaCells = data;
        });
        basicImportService.queryCellCount().then(function(data) {
            $rootScope.importData.cellCount = data;
        });
        basicImportService.queryCdmaCellCount().then(function(data) {
            $rootScope.importData.cdmaCellCount = data;
        });
        basicImportService.queryVanishedENodebs().then(function(data) {
            $rootScope.importData.vanishedENodebIds = data;
        });
        basicImportService.queryVanishedCells().then(function(data) {
            $rootScope.importData.vanishedCellIds = data;
        });
        basicImportService.queryVanishedBtss().then(function(data) {
            $rootScope.importData.vanishedBtsIds = data;
        });
        basicImportService.queryVanishedCdmaCells().then(function(data) {
            $rootScope.importData.vanishedCdmaCellIds = data;
        });
    })
    .controller("import.index", function($scope, basicImportService) {
        $scope.newENodebsImport = true;
        $scope.newCellsImport = true;
        $scope.newBtssImport = true;
        $scope.newCdmaCellsImport = true;
        $scope.totalDumpRrus = 0;
        $scope.totalUpdateCells = 0;
        $scope.totalDumpCdmaRrus = 0;

        $scope.postAllENodebs = function() {
            if ($scope.importData.newENodebs.length > 0) {
                basicImportService.dumpMultipleENodebExcels($scope.importData.newENodebs).then(function(result) {
                    $scope.importData.updateMessages.push({
                        contents: "完成LTE基站导入" + result + "个",
                        type: 'success'
                    });
                    $scope.importData.newENodebs = [];
                });
            }
        };

        $scope.postAllBtss = function() {
            if ($scope.importData.newBtss.length > 0) {
                basicImportService.dumpMultipleBtsExcels($scope.importData.newBtss).then(function(result) {
                    $scope.importData.updateMessages.push({
                        contents: "完成CDMA基站导入" + result + "个",
                        type: 'success'
                    });
                    $scope.importData.newBtss = [];
                });
            }
        };

        $scope.postAllCells = function() {
            if ($scope.importData.newCells.length > 0) {
                basicImportService.dumpMultipleCellExcels($scope.importData.newCells).then(function(result) {
                    $scope.importData.updateMessages.push({
                        contents: "完成LTE小区导入" + result + "个",
                        type: 'success'
                    });
                    $scope.importData.newCells = [];
                });
            }
        };

        $scope.postAllCdmaCells = function() {
            if ($scope.importData.newCdmaCells.length > 0) {
                basicImportService.dumpMultipleCdmaCellExcels($scope.importData.newCdmaCells).then(function(result) {
                    $scope.importData.updateMessages.push({
                        contents: "完成CDMA小区导入" + result + "个",
                        type: 'success'
                    });
                    $scope.importData.newCdmaCells = [];
                });
            }
        };

        $scope.updateLteCells = function() {
            basicImportService.updateLteCells().then(function (result) {
                if (result > 0) {
                    $scope.totalUpdateCells = result;
                    $scope.updateLteCells();
                }
            });
        };

        $scope.importLteRrus = function() {
            basicImportService.dumpLteRrus().then(function (result) {
                if (result > 0) {
                    $scope.totalDumpRrus = result;
                    $scope.importLteRrus();
                }
            });
        };

        $scope.importCdmaRrus = function() {
            basicImportService.dumpCdmaRrus().then(function (result) {
                if (result > 0) {
                    $scope.totalDumpCdmaRrus = result;
                    $scope.importCdmaRrus();
                }
            });
        };

        $scope.vanishENodebs = function() {
            basicImportService.vanishENodebIds($scope.importData.vanishedENodebIds).then(function() {
                $scope.importData.updateMessages.push({
                    contents: "完成消亡LTE基站：" + $scope.importData.vanishedENodebIds.length,
                    type: 'success'
                });
                $scope.importData.vanishedENodebIds = [];
            });
        };

        $scope.vanishBtss = function() {
            basicImportService.vanishBtsIds($scope.importData.vanishedBtsIds).then(function() {
                $scope.importData.updateMessages.push({
                    contents: "完成消亡CDMA基站：" + $scope.importData.vanishedBtsIds.length,
                    type: 'success'
                });
                $scope.importData.vanishedBtsIds = [];
            });
        };

        $scope.vanishCells = function() {
            basicImportService.vanishCellIds($scope.importData.vanishedCellIds).then(function() {
                $scope.importData.updateMessages.push({
                    contents: "完成消亡LTE小区：" + $scope.importData.vanishedCellIds.length,
                    type: 'success'
                });
                $scope.importData.vanishedCellIds = [];
            });
        };

        $scope.vanishCdmaCells = function() {
            basicImportService.vanishCdmaCellIds($scope.importData.vanishedCdmaCellIds).then(function() {
                $scope.importData.updateMessages.push({
                    contents: "完成消亡CDMA小区：" + $scope.importData.vanishedCdmaCellIds.length,
                    type: 'success'
                });
                $scope.importData.vanishedCdmaCellIds = [];
            });
        };
    })
    .controller("import.eNodebs", function ($scope, $uibModal, $log, appFormatService, basicImportService) {
        $scope.editENodeb = function (item, index) {
            if (!item.dateTransefered) {
                item.openDate = appFormatService.getDate(item.openDate);
                item.dateTransefered = true;
            }

            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/Parameters/Import/EditENodebDialog.html',
                controller: 'import.eNodebs.dialog',
                size: 'lg',
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                $scope.postSingleENodeb(result, index);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        $scope.postSingleENodeb = function (item, index) {
            basicImportService.dumpOneENodebExcel(item).then(function (result) {
                if (result) {
                    $scope.importData.newENodebs.splice(index, 1);
                    $scope.importData.updateMessages.push({
                        contents: "完成一个LTE基站'" + item.name + "'导入数据库！",
                        type: "success"
                    });
                } else {
                    $scope.importData.updateMessages.push({
                        contents: "LTE基站'" + item.name + "'导入数据库失败！",
                        type: "error"
                    });
                }
            }, function (reason) {
                $scope.importData.updateMessages.push({
                    contents: "LTE基站'" + item.name + "'导入数据库失败！原因是：" + reason,
                    type: "error"
                });
            });

        };
    })
    .controller('import.eNodebs.dialog', function ($scope, $uibModalInstance, item) {
        $scope.item = item;
        $scope.dateOpened = false;

        $scope.ok = function () {
            $uibModalInstance.close($scope.item);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    }).controller("import.btss", function ($scope, $uibModal, $log, appFormatService, basicImportService) {
        $scope.editBts = function (item, index) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/Parameters/Import/EditBtsDialog.html',
                controller: 'import.btss.dialog',
                size: 'lg',
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                $scope.postSingleBts(result, index);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        $scope.postSingleBts = function (item, index) {
            basicImportService.dumpOneBtsExcel(item).then(function (result) {
                if (result) {
                    $scope.importData.newBtss.splice(index, 1);
                    $scope.importData.updateMessages.push({
                        contents: "完成一个CDMA基站'" + item.name + "'导入数据库！",
                        type: "success"
                    });
                } else {
                    $scope.importData.updateMessages.push({
                        contents: "CDMA基站'" + item.name + "'导入数据库失败！",
                        type: "error"
                    });
                }
            }, function (reason) {
                $scope.importData.updateMessages.push({
                    contents: "CDMA基站'" + item.name + "'导入数据库失败！原因是：" + reason,
                    type: "error"
                });
            });
        };
    })
    .controller('import.btss.dialog', function ($scope, $uibModalInstance, item) {
        $scope.item = item;
        $scope.dateOpened = false;

        $scope.ok = function () {
            $uibModalInstance.close($scope.item);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .controller("eNodeb.lonLat", function ($scope, neGeometryService) {
        $scope.newENodebLonLatEdits = neGeometryService.queryENodebLonLatEdits($scope.importData.newENodebs);

        $scope.postENodebLonLat = function () {
            neGeometryService.mapLonLatEdits($scope.importData.newENodebs, $scope.newENodebLonLatEdits);
        };

    })
    .controller("bts.lonLat", function ($scope, neGeometryService) {
        $scope.newBtsLonLatEdits = neGeometryService.queryGeneralLonLatEdits($scope.importData.newBtss);

        $scope.postBtsLonLat = function () {
            neGeometryService.mapLonLatEdits($scope.importData.newBtss, $scope.newBtsLonLatEdits);
        };

    })
    .controller("import.cells", function ($scope, $uibModal, $log, basicImportService) {
        $scope.editCell = function (item, index) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/Parameters/Import/EditCellDialog.html',
                controller: 'import.cells.dialog',
                size: 'lg',
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                $scope.postSingleCell(result, index);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        $scope.postSingleCell = function (item, index) {
            basicImportService.dumpOneCellExcel(item).then(function (result) {
                if (result) {
                    $scope.importData.newCells.splice(index, 1);
                    $scope.importData.updateMessages.push({
                        contents: "完成一个LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库！",
                        type: "success"
                    });
                } else {
                    $scope.importData.updateMessages.push({
                        contents: "LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库失败！",
                        type: "error"
                    });
                }
            }, function (reason) {
                $scope.importData.updateMessages.push({
                    contents: "LTE小区'" + item.eNodebId + "-" + item.sectorId + "'导入数据库失败！原因是：" + reason,
                    type: "error"
                });
            });

        };
    })
    .controller('import.cells.dialog', function ($scope, $uibModalInstance, item) {
        $scope.item = item;
        $scope.dateOpened = false;

        $scope.ok = function () {
            $uibModalInstance.close($scope.item);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .controller("cell.lonLat", function ($scope, neGeometryService) {
        $scope.newCellLonLatEdits = neGeometryService.queryGeneralLonLatEdits($scope.importData.newCells);

        $scope.postCellLonLat = function () {
            neGeometryService.mapLonLatEdits($scope.importData.newCells, $scope.newCellLonLatEdits);
        };

    })
    .controller("import.cdmaCells", function ($scope, $uibModal, $log, basicImportService) {
        $scope.editCdmaCell = function (item, index) {
            var modalInstance = $uibModal.open({
                animation: true,
                templateUrl: '/appViews/Parameters/Import/EditCdmaCellDialog.html',
                controller: 'import.cdmaCells.dialog',
                size: 'lg',
                resolve: {
                    item: function () {
                        return item;
                    }
                }
            });
            modalInstance.result.then(function (result) {
                $scope.postSingleCdmaCell(result, index);
            }, function () {
                $log.info('Modal dismissed at: ' + new Date());
            });
        };
        $scope.postSingleCdmaCell = function (item, index) {
            basicImportService.dumpOneCdmaCellExcel(item).then(function (result) {
                if (result) {
                    $scope.importData.newCdmaCells.splice(index, 1);
                    $scope.importData.updateMessages.push({
                        contents: "完成一个CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库！",
                        type: "success"
                    });
                } else {
                    $scope.importData.updateMessages.push({
                        contents: "CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库失败！",
                        type: "error"
                    });
                }
            }, function (reason) {
                $scope.importData.updateMessages.push({
                    contents: "CDMA小区'" + item.btsId + "-" + item.sectorId + "'导入数据库失败！原因是：" + reason,
                    type: "error"
                });
            });

        };
    })
    .controller('import.cdmaCells.dialog', function ($scope, $uibModalInstance, item) {
        $scope.item = item;
        $scope.dateOpened = false;

        $scope.ok = function () {
            $uibModalInstance.close($scope.item);
        };

        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .controller("cdmaCell.lonLat", function ($scope, neGeometryService) {
        $scope.newCdmaCellLonLatEdits
            = neGeometryService.queryGeneralLonLatEdits($scope.importData.newCdmaCells);

        $scope.postCdmaCellLonLat = function () {
            neGeometryService.mapLonLatEdits($scope.importData.newCdmaCells, $scope.newCdmaCellLonLatEdits);
        };

    })
    .controller('eNodeb.vanished', function ($scope, networkElementService) {
        $scope.vanishedENodebs = [];
        var data = $scope.importData.vanishedENodebIds;
        for (var i = 0; i < data.length; i++) {
            networkElementService.queryENodebInfo(data[i]).then(function (result) {
                $scope.vanishedENodebs.push(result);
            });
        }
    })
    .controller('bts.vanished', function ($scope, networkElementService) {
        $scope.vanishedBtss = [];
        var data = $scope.importData.vanishedBtsIds;
        for (var i = 0; i < data.length; i++) {
            networkElementService.queryBtsInfo(data[i]).then(function (result) {
                $scope.vanishedBtss.push(result);
            });
        }
    })
    .controller('cell.vanished', function ($scope, networkElementService) {
        $scope.vanishedCells = [];
        var data = $scope.importData.vanishedCellIds;
        for (var i = 0; i < data.length; i++) {
            networkElementService.queryCellInfo(data[i].cellId, data[i].sectorId).then(function (result) {
                $scope.vanishedCells.push(result);
            });
        }
    })
    .controller('cdmaCell.vanished', function ($scope, networkElementService) {
        $scope.vanishedCdmaCells = [];
        var data = $scope.importData.vanishedCdmaCellIds;
        for (var i = 0; i < data.length; i++) {
            networkElementService.queryCdmaCellInfoWithType(data[i].cellId, data[i].sectorId, data[i].cellType).then(function (result) {
                $scope.vanishedCdmaCells.push(result);
            });
        }
    });