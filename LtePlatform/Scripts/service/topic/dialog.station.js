angular.module('topic.dialog.station', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap",'angularFileUpload'])
    .controller('map.special-station.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateSpecialStationGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.special-indoor.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateSpecialIndoorGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.fault-station.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateFaultStationGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.checkingStation.dialog', function ($scope, $uibModalInstance, station, dialogTitle,
        appFormatService, networkElementService, downSwitchService) {
        $scope.station;
        downSwitchService.getCheckDetailsById(station.id).then(function (response) {
            $scope.station = response.result[0];
        });
        $scope.dialogTitle = dialogTitle;
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
    })
    .controller('map.checkingResultsStationAdd.dialog', function ($scope, $uibModalInstance, station, dialogTitle,
        appFormatService, networkElementService, downSwitchService) {
        $scope.station = {};
        $scope.station.stationId = station.StationId;
        $scope.station.WYMC003 = station.StationName;
        $scope.station.WYBH002 = station.StationId;
        $scope.station.XJLSH001 = station.id;
        $scope.station.KSSJ008 = station.starttime;
        $scope.station.JSSJ009 = station.endtime;
        $scope.station.XJDW005 = station.service;
        $scope.station.WGMC007 = 'FS'+station.AreaName;
        $scope.tab = 1;
        $scope.dialogTitle = dialogTitle;
        $scope.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
        $scope.ok = function () {
            downSwitchService.addCheckResults({
                "Station": JSON.stringify($scope.station)
            }).then(function (result) {
                alert(result.description);
                $uibModalInstance.dismiss('cancel');
            });
        }
        $scope.selectTab = function (setTab) {
            $scope.tab = setTab;
        }
        $scope.isSelectTab = function (checkTab) {
            return $scope.tab === checkTab
        }
        $scope.selectTab(0);
    })
    .controller('map.fixingStation.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService,
            downSwitchService) {

            downSwitchService.getFixingStationById(station.id).then(function (response) {
                $scope.fixingStations = response.result[0];
                $scope.fixingStations.longtitute = station.longtitute;
                $scope.fixingStations.lattitute = station.lattitute;
                $scope.itemGroups = appFormatService.generateFixingStationGroups($scope.fixingStations);
            });
            $scope.dialogTitle = dialogTitle;

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.common-stationList.dialog',
        function($scope,
            $http,
            dialogTitle,
            type,
            $uibModalInstance,
            parametersDialogService,
            downSwitchService,
            mapDialogService,
            appUrlService,
            $upload) {
            $scope.dialogTitle = dialogTitle;
            $scope.distincts = new Array('FS', 'SD', 'NH', 'CC', 'SS', 'GM');
            $scope.stationList = [];
            $scope.page = 1;
            $scope.stationName = '';
            $scope.totolPage = 1;
            
            $scope.data = {
                file: null
            };
            $scope.onFileSelect = function ($files) {
                $scope.data.file = $files[0];
            }
            $scope.upload = function () {
                if (!$scope.data.file) {
                    return;
                }
                var url = appUrlService.getPhpHost() + 'LtePlatForm/lte/index.php/StationCommon/upload/';  //params是model传的参数，图片上传接口的url
                var data = angular.copy($scope.data || {}); // 接口需要的额外参数，比如指定所上传的图片属于哪个用户: { UserId: 78 }
                data.file = $scope.data.file;

                $upload.upload({
                    url: url,
                    data: data
                }).success(function (data) {
                    alert('success');
                }).error(function () {
                    alert('error');
                    });
            };
            
            downSwitchService.getAllCommonStations(type, 0, 10).then(function(response) {
                $scope.stationList = response.result.rows;
                $scope.totolPage = response.result.total_pages;
                $scope.page = response.result.curr_page;
            });
            $scope.details = function(stationId) {
                downSwitchService.getCommonStationById(stationId).then(function(result) {
                    mapDialogService.showCommonStationInfo(result.result[0]);
                });
            }

            $scope.delete = function(stationId) {
                if (confirm("你确定删除该站点？")) {
                    downSwitchService.deleteCommonStation(stationId).then(function(response) {
                        alert(response.description);
                        $scope.jumpPage($scope.page);
                    });
                }
            }
            $scope.edit = function(stationId) {
                parametersDialogService.showCommonStationEdit(stationId);
            }
            $scope.addStation = function() {
                parametersDialogService.showCommonStationAdd(type);
            }
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            }
            $scope.search = function() {
                $scope.page = 1;
                $scope.jumpPage($scope.page);
            }
            $scope.firstPage = function() {
                $scope.page = 1;
                $scope.jumpPage($scope.page);
            }
            $scope.lastPage = function() {
                $scope.page = $scope.totolPage;
                $scope.jumpPage($scope.page);
            }
            $scope.prevPage = function() {
                if ($scope.page !== 1)
                    $scope.page--;
                $scope.jumpPage($scope.page);
            }
            $scope.nextPage = function() {
                if ($scope.page !== $scope.totolPage)
                    $scope.page++;
                $scope.jumpPage($scope.page);
            }
            $scope.jumpPage = function(page) {
                if (page >= $scope.totolPage)
                    page = $scope.totolPage;
                downSwitchService.getCommonStationByName($scope.selectDistinct, $scope.stationName, type, page, 10)
                    .then(function(response) {
                        $scope.stationList = response.result.rows;
                        $scope.totolPage = response.result.total_pages;
                        $scope.page = response.result.curr_page;
                        $scope.records = response.result.records;
                    });
            }
        });