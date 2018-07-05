angular.module('topic.parameters.station', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap", 'angularFileUpload'])
    .controller('map.common-stationEdit.dialog', function ($scope, stationId, dialogTitle, $uibModalInstance, downSwitchService) {
	    $scope.dialogTitle = dialogTitle;
	    $scope.station = {};
        downSwitchService.getStationCommonById(stationId).then(function(result) {
            $scope.station = result.result[0];
            $scope.station.longtitute = result.result[0].longtitute*1;
            $scope.station.lattitute = result.result[0].lattitute*1;
        });
	    
	    $scope.ok = function() {
                downSwitchService.updateStationCommon({
                    "Station": JSON.stringify($scope.station)
                }).then(function(result) {
                    alert(result.description);
                });
            }
	    $scope.cancel = function () {
	        $uibModalInstance.dismiss('cancel');
	    }
    })
    .controller('map.common-stationAdd.dialog',
        function($scope,
            $http,
            dialogTitle,
            type,
            $uibModalInstance,
            downSwitchService,
            stationFactory) {
            $scope.dialogTitle = dialogTitle;
            $scope.station = {};
            $scope.distincts = stationFactory.stationDistincts;

            $scope.change = function() {
                downSwitchService.getCommonStationIdAdd($scope.selectedDistinct, type).then(function(result) {
                    $scope.station.id = result.result;
                });
            };
            
            $scope.ok = function() {
                downSwitchService.addCommonStation({
                    "Station": JSON.stringify($scope.station)
                }).then(function(result) {
                    alert(result.description);
                    $uibModalInstance.dismiss('cancel');
                });
            }

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            }
        })
    .controller('map.construction.dialog',
        function($scope, $uibModalInstance, dialogTitle, site, appFormatService, downSwitchService) {
            $scope.dialogTitle = dialogTitle;
            $scope.site = site;
            $scope.upload = {
                dwg: false
            };
            $scope.constructionGroups = appFormatService.generateConstructionGroups(site);
            $scope.uploadNewDwg = function() {
                $scope.upload.dwg = true;
                var $uploader = $("#btsInfo_upload_dwg");
                //配置上传控件
                $uploader.fileinput({
                    language: "zh", //本地化语言
                    uploadUrl: "/api/DwgView?directory=Common&btsId=" + site.fslNumber,
                    uploadAsync: true,
                    minFileCount: 1,
                    maxFileCount: 6, //一次最多上传数量
                    overwriteInitial: false,
                    allowedFileExtensions: ["pdf", "vsd", "vsdx"],
                    previewSettings: {
                        image: { width: "120px", height: "80px" }
                    },
                    initialPreviewAsData: true // identify if you are sending preview data only and not the markup
                }).on('fileuploaded',
                    function(event, data, id, index) {
                        $scope.upload.dwg = false;
                        $scope.getDwgList();
                    }).on('filebatchuploaderror',
                    function(event, data, previewId, index) {
                        $scope.upload.dwg = false;
                    });

                //清空已选
                $uploader.fileinput('clear');
            };

            $scope.getDwgList = function() {
                downSwitchService.queryDwgList(site.fslNumber).then(function(list) {
                    $scope.dwgList = list;
                });
            };
            $scope.download = function(fileName) {
                downSwitchService.queryDwgUrl(site.fslNumber, fileName).then(function(result) {
                    if (result.error) {
                        console.log(error);
                    } else {
                        $scope.downloadUrl = "http://" +
                            window.location.hostname +
                            ":2015/BTSDWG/Common/" +
                            site.fslNumber +
                            "/" +
                            encodeURIComponent(result.file);
                    }
                });
            };

            $scope.getDwgList();

            $scope.ok = function() {
                $uibModalInstance.close($scope.site);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });