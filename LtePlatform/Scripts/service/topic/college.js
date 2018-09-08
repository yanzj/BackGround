angular.module('topic.college',
        ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', 'topic.dialog', 'topic.parameters'])
    .factory('generalMapService',
        function(baiduMapService,
            baiduQueryService,
            networkElementService,
            neGeometryService,
            geometryCalculateService,
            dumpPreciseService) {
            return {
                showGeneralPointCollection: function (stations, color, callback) {
                    alert(color);
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointCollection(stations,
                                color,
                                -xOffset,
                                -yOffset,
                                function(e) {
                                    callback(e.point.data);
                                });
                        });
                },               
                showPointWithClusterer: function (stations, color, callback) {     
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function (coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.drawPointWithClusterer(stations,
                                color.slice(1,7),
                                -xOffset,
                                -yOffset,
                                function () {
                                    callback(this.data);
                                });
                        });
                },
                showGeneralMultiPoints: function(sites, color, callback) {
                    baiduQueryService.transformToBaidu(sites[0].longtitute, sites[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - sites[0].longtitute;
                        var yOffset = coors.y - sites[0].lattitute;
                        baiduMapService.drawMultiPoints(sites,
                            color,
                            -xOffset,
                            -yOffset,
                            function(e) {
                                var xCenter = e.point.lng - xOffset;
                                var yCenter = e.point.lat - yOffset;
                                networkElementService.queryRangeSectors(
                                    neGeometryService.queryNearestRange(xCenter, yCenter),
                                    []).then(function(sectors) {
                                    callback(sectors);
                                });
                            });
                    });
                },
                showContainerSites: function(sites, color, callback) {
                    baiduQueryService.transformToBaidu(sites[0].longtitute, sites[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - sites[0].longtitute;
                        var yOffset = coors.y - sites[0].lattitute;
                        baiduMapService.drawMultiPoints(sites,
                            color,
                            -xOffset,
                            -yOffset,
                            function(e) {
                                var xCenter = e.point.lng - xOffset;
                                var yCenter = e.point.lat - yOffset;
                                var container = neGeometryService.queryNearestRange(xCenter, yCenter);
                                container.excludedIds = [];
                                callback(container);
                            });
                    });
                },
                showGeneralSector: function(cell, item, color, size, callback, data) {
                    baiduQueryService.transformToBaidu(cell.longtitute, cell.lattitute).then(function(coors) {
                        item = angular.extend(item, cell);
                        cell.longtitute = coors.x;
                        cell.lattitute = coors.y;
                        var sectorTriangle = baiduMapService.generateSector(cell, color, size);
                        baiduMapService.addOneSectorToScope(sectorTriangle, callback, data);
                    });
                },
                calculateRoadDistance: function(dtPoints) {
                    var xOrigin = 0;
                    var yOrigin = 0;
                    var distance = 0;
                    angular.forEach(dtPoints,
                        function(point) {
                            if (point.longtitute > 112 &&
                                point.longtitute < 114 &&
                                point.lattitute > 22 &&
                                point.lattitute < 24) {
                                if (xOrigin > 112 && xOrigin < 114 && yOrigin > 22 && yOrigin < 24) {
                                    distance += geometryCalculateService
                                        .getDistanceFunc(yOrigin, xOrigin, point.lattitute, point.longtitute) *
                                        1000;
                                }
                                xOrigin = point.longtitute;
                                yOrigin = point.lattitute;
                            }
                        });
                    return distance;
                },
                generateUsersDistrictsAndDistincts: function(city, districts, distincts, areaNames, callback) {
                    dumpPreciseService.generateUsersDistrict(city,
                        districts,
                        function(district, $index) {
                            areaNames.push('FS' + district);
                            distincts.push(district + '区');
                            callback(district, $index);
                        });
                },
                generateUsersDistrictsOnly: function (city, districts, callback) {
                    dumpPreciseService.generateUsersDistrict(city,
                        districts,
                        function(district, $index) {
                            callback(district, $index);
                        });
                },
                showGeneralCells: function(cells, xOffset, yOffset, callback) {
                    angular.forEach(cells,
                        function (cell) {
                            cell.longtitute += xOffset;
                            cell.lattitute += yOffset;
                            var sectorTriangle = baiduMapService.generateSector(cell, "blue", 5);
                            baiduMapService
                                .addOneSectorToScope(sectorTriangle,
                                callback,
                                cell);
                        });
                }
            };
        })
    .factory('collegeMapService',
        function(generalMapService,
            baiduMapService,
            collegeService,
            collegeQueryService,
            collegeDtService,
            baiduQueryService,
            workItemDialog,
            geometryService,
            networkElementService,
            mapDialogService,
            parametersDialogService,
            neighborDialogService) {
            return {
                showCollegeInfos: function(showCollegeDialogs, year) {
                    collegeService.queryStats(year).then(function(colleges) {
                        angular.forEach(colleges,
                            function(college) {
                                var center;
                                collegeService.queryRegion(college.id).then(function(region) {
                                    switch (region.regionType) {
                                    case 2:
                                        center = baiduMapService.drawPolygonAndGetCenter(region.info.split(';'));
                                        break;
                                    case 1:
                                        center = baiduMapService.drawRectangleAndGetCenter(region.info.split(';'));
                                        break;
                                    default:
                                        center = baiduMapService.drawCircleAndGetCenter(region.info.split(';'));
                                        break;
                                    }
                                    var marker = baiduMapService.generateMarker(center.X, center.Y);
                                    baiduMapService.addOneMarkerToScope(marker, showCollegeDialogs, college);
                                    baiduMapService.drawLabel(college.name, center.X, center.Y);
                                });
                            });
                    });
                },
                showDtInfos: function(infos, begin, end) {
                    collegeQueryService.queryAll().then(function(colleges) {
                        angular.forEach(colleges,
                            function(college) {

                                collegeService.queryRegion(college.id).then(function(region) {
                                    var center = geometryService.queryRegionCenter(region);
                                    var info = {
                                        name: college.name,
                                        centerX: center.X,
                                        centerY: center.Y,
                                        area: region.area,
                                        file2Gs: 0,
                                        file3Gs: 0,
                                        file4Gs: 0
                                    };
                                    infos.push(info);
                                    collegeDtService.updateFileInfo(info, begin, end);
                                });
                            });
                    });
                },
                queryCenterAndCallback: function(collegeName, callback) {
                    collegeQueryService.queryByName(collegeName).then(function(college) {
                        collegeService.queryRegion(college.id).then(function(region) {
                            var center = geometryService.queryRegionCenter(region);
                            callback(center);
                        });
                    });
                },
                showRsrpMrGrid: function(result, longtitute, lattitute, areaStats, colorDictionary) {
                    baiduQueryService.transformToBaidu(longtitute, lattitute).then(function(coors) {
                        var xOffset = coors.x - longtitute;
                        var yOffset = coors.y - lattitute;
                        baiduMapService.setCellFocus(coors.x, coors.y, 14);
                        angular.forEach(result,
                            function(item) {
                                var gridColor = colorDictionary[item.rsrpLevelDescription];
                                var polygon = baiduMapService
                                    .drawPolygonWithColor(item.coordinates, gridColor, -xOffset, -yOffset);
                                var area = BMapLib.GeoUtils.getPolygonArea(polygon);

                                if (area > 0) {
                                    areaStats[item.rsrpLevelDescription] += area;
                                }
                            });
                    });
                },
                showCommonStations: function (stations, color) {
                    generalMapService.showPointWithClusterer(stations, color,
                        function (station) {
                            mapDialogService.showCommonStationInfo(station);
                        });
                },
                showConstructionSites: function(stations, status, callback) {
                    baiduQueryService.transformToBaidu(stations[0].longtitute, stations[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - stations[0].longtitute;
                            var yOffset = coors.y - stations[0].lattitute;
                            baiduMapService.setCellFocus(coors.x, coors.y, 15);
                            angular.forEach(stations,
                                function(item) {
                                    var marker = new BMap.Marker(new BMap
                                        .Point(item.longtitute + xOffset, item.lattitute + yOffset),
                                        {
                                            icon: geometryService.queryConstructionIcon(status)
                                        });
                                    baiduMapService.addOneMarkerToScope(marker, callback, item);
                                });
                        });
                },
                showOutdoorCellSites: function(sites, color) {
                    generalMapService.showGeneralMultiPoints(sites, color, mapDialogService.showCellsInfo);
                },
                showIndoorCellSites: function(sites, color) {
                    generalMapService.showGeneralMultiPoints(sites, color, mapDialogService.showCellsInfo);
                },
                showENodebSites: function(sites, color, beginDate, endDate) {
                    generalMapService.showContainerSites(sites,
                        color,
                        function(container) {
                            networkElementService.queryRangeENodebs(container).then(function(items) {
                                if (items.length) {
                                    parametersDialogService.showENodebInfo(items[0], beginDate, endDate);
                                }
                            });
                        });
                },
                showComplainItems: function(sites, color) {
                    generalMapService.showContainerSites(_.filter(sites, function(item) {
                            return item.longtitute && item.lattitute;
                        }),
                        color,
                        function(container) {
                            networkElementService.queryRangeComplains(container).then(function(items) {
                                if (items.length) {
                                    mapDialogService.showOnlineSustainInfos(items);
                                }
                            });
                        });
                },
                showRrcCellSector: function(cell, item, beginDate, endDate) {
                    generalMapService.showGeneralSector(cell,
                        item,
                        "blue",
                        5,
                        neighborDialogService.showRrcCell,
                        {
                            item: item,
                            beginDate: beginDate,
                            endDate: endDate
                        });
                }
            };
        });