angular.module('kpi.customer.complain', ['myApp.url', 'myApp.region', "ui.bootstrap"])
    .controller('emergency.new.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            dialogTitle,
            city,
            district,
            vehicularType) {
            $scope.dialogTitle = dialogTitle;
            $scope.message = "";
            $scope.city = city;
            $scope.district = district;
            $scope.vehicularType = vehicularType;

            var firstDay = new Date();
            firstDay.setDate(firstDay.getDate() + 7);
            var nextDay = new Date();
            nextDay.setDate(nextDay.getDate() + 14);
            $scope.itemBeginDate = {
                value: firstDay,
                opened: false
            };
            $scope.itemEndDate = {
                value: nextDay,
                opened: false
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            $scope.dto = {
                projectName: "和顺梦里水乡百合花文化节",
                expectedPeople: 500000,
                vehicles: 1,
                area: "万顷洋园艺世界",
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "此次活动为佛山市南海区政府组织的一次大型文化活动，是宣传天翼品牌的重要场合。",
                townId: 1
            };

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('emergency.college.dialog',
        function($scope,
            $uibModalInstance,
            serialNumber,
            collegeName,
            collegeQueryService,
            appFormatService,
            customerQueryService,
            appRegionService) {
            $scope.dialogTitle = collegeName + "应急通信车申请-" + serialNumber;
            $scope.dto = {
                projectName: collegeName + "应急通信车申请",
                expectedPeople: 500000,
                vehicles: 1,
                area: collegeName,
                department: "南海区分公司客响维护部",
                person: "刘文清",
                phone: "13392293722",
                vehicleLocation: "门口东边100米处",
                otherDescription: "应急通信车申请。",
                townId: 1
            };
            customerQueryService.queryDemandLevelOptions().then(function(options) {
                $scope.demandLevel = {
                    options: options,
                    selected: options[0]
                };
            });
            customerQueryService.queryVehicleTypeOptions().then(function(options) {
                $scope.vehicularType = {
                    options: options,
                    selected: options[17]
                };
            });
            var transmitOptions = customerQueryService.queryTransmitFunctionOptions();
            $scope.transmitFunction = {
                options: transmitOptions,
                selected: transmitOptions[0]
            };
            var electrictOptions = customerQueryService.queryElectricSupplyOptions();
            $scope.electricSupply = {
                options: electrictOptions,
                selected: electrictOptions[0]
            };
            collegeQueryService.queryByNameAndYear(collegeName, $scope.collegeInfo.year.selected).then(function(item) {
                $scope.itemBeginDate = {
                    value: appFormatService.getDate(item.oldOpenDate),
                    opened: false
                };
                $scope.itemEndDate = {
                    value: appFormatService.getDate(item.newOpenDate),
                    opened: false
                };
                $scope.dto.expectedPeople = item.expectedSubscribers;
            });
            customerQueryService.queryOneVip(serialNumber).then(function(item) {
                angular.forEach($scope.district.options,
                    function(district) {
                        if (district === item.district) {
                            $scope.district.selected = item.district;
                        }
                    });
                appRegionService.queryTowns($scope.city.selected, $scope.district.selected).then(function(towns) {
                    $scope.town.options = towns;
                    $scope.town.selected = towns[0];
                    angular.forEach(towns,
                        function(town) {
                            if (town === item.town) {
                                $scope.town.selected = town;
                            }
                        });
                });
            });

            $scope.ok = function() {
                $scope.dto.demandLevelDescription = $scope.demandLevel.selected;
                $scope.dto.beginDate = $scope.itemBeginDate.value;
                $scope.dto.endDate = $scope.itemEndDate.value;
                $scope.dto.vehicularTypeDescription = $scope.vehicularType.selected;
                $scope.dto.transmitFunction = $scope.transmitFunction.selected;
                $scope.dto.district = $scope.district.selected;
                $scope.dto.town = $scope.town.selected;
                $scope.dto.electricSupply = $scope.electricSupply.selected;
                $uibModalInstance.close($scope.dto);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('vip.supplement.dialog',
        function($scope,
            $uibModalInstance,
            customerQueryService,
            appFormatService,
            dialogTitle,
            view,
            city,
            district) {
            $scope.dialogTitle = dialogTitle;
            $scope.view = view;
            $scope.city = city;
            $scope.district = district;
            $scope.matchFunction = function(text) {
                return $scope.view.projectName.indexOf(text) >= 0 || $scope.view.projectContents.indexOf(text) >= 0;
            };
            $scope.matchDistrictTown = function() {
                var districtOption = appFormatService.searchText($scope.district.options, $scope.matchFunction);
                if (districtOption) {
                    $scope.district.selected = districtOption;
                }
            };
            $scope.$watch('town.selected',
                function() {
                    var townOption = appFormatService.searchText($scope.town.options, $scope.matchFunction);
                    if (townOption) {
                        $scope.town.selected = townOption;
                    }
                });

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.ok = function() {
                $scope.view.district = $scope.district.selected;
                $scope.view.town = $scope.town.selected;
                $uibModalInstance.close($scope.view);
            };
        })
    .controller('fiber.new.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            id,
            num) {
            $scope.dialogTitle = dialogTitle;

            $scope.item = {
                id: 0,
                emergencyId: id,
                workItemNumber: "FS-Fiber-" +
                    new Date().getYear() +
                    "-" +
                    new Date().getMonth() +
                    "-" +
                    new Date().getDate() +
                    "-" +
                    num,
                person: "",
                beginDate: new Date(),
                finishDate: null
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.item);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('complain.supplement.dialog',
        function($scope,
            $uibModalInstance,
            appRegionService,
            appFormatService,
            baiduMapService,
            parametersMapService,
            parametersDialogService,
            item) {
            $scope.dialogTitle = item.serialNumber + "工单信息补充";

            $scope.itemGroups = appFormatService.generateComplainPositionGroups(item);
            appRegionService.initializeCities().then(function(cities) {
                $scope.city.options = cities;
                $scope.city.selected = cities[0];
                appRegionService.queryDistricts($scope.city.selected).then(function(districts) {
                    $scope.district.options = districts;
                    $scope.district.selected = (item.district) ? item.district.replace('区', '') : districts[0];
                    baiduMapService.initializeMap("map", 11);
                    baiduMapService.addCityBoundary("佛山");
                    if (item.longtitute && item.lattitute) {
                        var marker = baiduMapService.generateMarker(item.longtitute, item.lattitute);
                        baiduMapService.addOneMarker(marker);
                        baiduMapService.setCellFocus(item.longtitute, item.lattitute, 15);
                    }
                    if (item.sitePosition) {
                        parametersMapService.showElementsWithGeneralName(item.sitePosition,
                            parametersDialogService.showENodebInfo,
                            parametersDialogService.showCellInfo);
                    }
                });
            });

            $scope.matchTown = function() {
                var town = appFormatService.searchPattern($scope.town.options, item.sitePosition);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.buildingName);
                if (town) {
                    $scope.town.selected = town;
                    return;
                }
                town = appFormatService.searchPattern($scope.town.options, item.roadName);
                if (town) {
                    $scope.town.selected = town;
                }
            };

            $scope.ok = function() {
                $scope.item.district = $scope.district.selected;
                $scope.item.town = $scope.town.selected;
                $uibModalInstance.close($scope.item);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });