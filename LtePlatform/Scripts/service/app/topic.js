angular.module('topic.basic', ['myApp.url', 'myApp.region'])
    .value('drawingStyleOptions', {
        strokeColor: "red", //边线颜色。
        fillColor: "red", //填充颜色。当参数为空时，圆形将没有填充效果。
        strokeWeight: 3, //边线的宽度，以像素为单位。
        strokeOpacity: 0.8, //边线透明度，取值范围0 - 1。
        fillOpacity: 0.6, //填充的透明度，取值范围0 - 1。
        strokeStyle: 'solid' //边线的样式，solid或dashed。
    })
    .value('baiduMapOptions', {
        myKey: 'LlMnTd7NcCWI1ibhDAdKeVlG',
        baiduApiUrl: '//api.map.baidu.com/geoconv/v1/',
        baiduPlaceUrl: '//api.map.baidu.com/place/v2/suggestion'
    })
    .factory('baiduQueryService', function($sce, generalHttpService, appUrlService, baiduMapOptions) {
        return {
            transformToBaidu: function (longtitute, lattitute) {
                var trustedUrl = $sce.trustAsResourceUrl(baiduMapOptions.baiduApiUrl +
                    '?coords=' +
                    longtitute +
                    ',' +
                    lattitute +
                    '&from=1&to=5&ak=' +
                    baiduMapOptions.myKey);
                return generalHttpService.getJsonpData(trustedUrl, function(result) {
                        return result.result[0];
                    });
            },
            transformBaiduCoors: function (coors) {
                var trustedUrl = $sce.trustAsResourceUrl(baiduMapOptions.baiduApiUrl +
                    '?coords=' +
                    coors.longtitute +
                    ',' +
                    coors.lattitute +
                    '&from=1&to=5&ak=' +
                    baiduMapOptions.myKey);
                return generalHttpService.getJsonpData(trustedUrl, function(result) {
                        return {
                            longtitute: result.result[0].x,
                            lattitute: result.result[0].y
                        }
                    });
            },
            queryBaiduPlace: function (name) {
                var trustedUrl = $sce.trustAsResourceUrl(baiduMapOptions.baiduPlaceUrl +
                    '?query=' +
                    name +
                    '&region=佛山市&output=json&ak=' +
                    baiduMapOptions.myKey);
                return generalHttpService.getJsonpData(trustedUrl, function(result) {
                        return result.result;
                    });
            },
            queryWandonglouyu: function() {
                return generalHttpService.getUrlData(appUrlService.getBuildingUrlHost() + 'phpApi/wandonglouyu.php', {});
            }
        }
    })
    .factory('baiduMapService', function(geometryService, networkElementService, drawingStyleOptions) {
        var mapStructure = {
            mainMap: {},
            subMap: {},
            currentCityBounday: {}
        };
        var map = mapStructure.mainMap;

        var getCellCenter = function(cell, rCell) {
            return geometryService.getPositionLonLat(cell, rCell, cell.azimuth);
        };
        var drawingManager = {};
        var markerClusterer = {};
        return {
            getMap: function() {
                return map;
            },
            initializeMap: function(tag, zoomLevel) {
                if (map === mapStructure.mainMap) {
                    mapStructure.mainMap = new BMap.Map(tag);
                    map = mapStructure.mainMap;
                } else {
                    mapStructure.subMap = new BMap.Map(tag);
                    map = mapStructure.subMap;
                }

                map.centerAndZoom(new BMap.Point(113.01, 23.02), zoomLevel);
                map.setMinZoom(8); //设置地图最小级别
                map.setMaxZoom(18); //设置地图最大级别

                map.enableScrollWheelZoom(); //启用滚轮放大缩小
                map.enableDragging();
                map.disableDoubleClickZoom();

                var topLeftControl = new BMap.ScaleControl({ anchor: BMAP_ANCHOR_TOP_LEFT }); // 左上角，添加比例尺
                var topLeftNavigation = new BMap.NavigationControl(); //左上角，添加默认缩放平移控件

                map.addControl(topLeftControl);
                map.addControl(topLeftNavigation);
                markerClusterer = new BMapLib.MarkerClusterer(map, { minClusterSize:5});
            },
            switchSubMap: function() {
                map = mapStructure.subMap;
            },
            switchMainMap: function() {
                map = mapStructure.mainMap;
            },
            addClickListener: function(callback) {
                map.addEventListener('click', callback);
            },
            getRange: function() {
                var bounds = map.getBounds();
                var sw = bounds.getSouthWest();
                var ne = bounds.getNorthEast();
                return {
                    west: sw.lng,
                    east: ne.lng,
                    south: sw.lat,
                    north: ne.lat
                };
            },
            setCenter: function(distinctIndex) {
                var lonDictionary = [113.30, 113.15, 113.12, 112.87, 112.88, 113.01];
                var latDictionay = [22.80, 23.03, 23.02, 23.17, 22.90, 23.02];
                var index = parseInt(distinctIndex) - 1;
                index = (index > 4 || index < 0) ? 5 : index;
                map.centerAndZoom(new BMap.Point(lonDictionary[index], latDictionay[index]), 12);
            },
            initializeDrawingManager: function() {
                drawingManager = new BMapLib.DrawingManager(map, {
                    isOpen: true, //是否开启绘制模式
                    enableDrawingTool: true, //是否显示工具栏
                    drawingToolOptions: {
                        anchor: BMAP_ANCHOR_TOP_RIGHT, //位置
                        offset: new BMap.Size(5, 5), //偏离值
                        drawingTypes: [
                            BMAP_DRAWING_CIRCLE,
                            BMAP_DRAWING_POLYGON,
                            BMAP_DRAWING_RECTANGLE
                        ]
                    },
                    circleOptions: drawingStyleOptions, //圆的样式
                    polygonOptions: drawingStyleOptions, //多边形的样式
                    rectangleOptions: drawingStyleOptions //矩形的样式
                });
            },
            addDrawingEventListener: function(event, callback) {
                drawingManager.addEventListener(event, callback);
            },
            addCityBoundary: function(city) {
                var bdary = new BMap.Boundary();
                bdary.get(city, function(rs) { //获取行政区域
                    var count = rs.boundaries.length; //行政区域的点有多少个
                    if (count === 0) {
                        return;
                    }
                    for (var i = 0; i < count; i++) {
                        var ply = new BMap.Polygon(rs.boundaries[i], {
                            strokeWeight: 3,
                            strokeColor: "#ff0000",
                            fillOpacity: 0.1
                        }); //建立多边形覆盖物
                        mapStructure.currentCityBounday = ply;
                        map.addOverlay(ply); //添加覆盖物
                    }
                });
            },
            addBoundary: function(coors, color, xOffset, yOffset) {
                var points = [];
                angular.forEach(coors, function(coor) {
                    points.push(new BMap.Point(coor.longtitute + xOffset, coor.lattitute + yOffset));
                });
                var polygon = new BMap.Polygon(points,
                { strokeColor: color, strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(polygon);
            },
            addFilledBoundary: function (coors, color, xOffset, yOffset) {
                var points = [];
                angular.forEach(coors, function (coor) {
                    points.push(new BMap.Point(coor.longtitute + xOffset, coor.lattitute + yOffset));
                });
                var polygon = new BMap.Polygon(points,
                    { strokeColor: color, fillColor: color, strokeWeight: 0 });
                map.addOverlay(polygon);
            },
            addDistrictBoundary: function(district, color) {
                var bdary = new BMap.Boundary();
                bdary.get(district, function(rs) { //获取行政区域
                    var count = rs.boundaries.length; //行政区域的点有多少个
                    if (count === 0) {
                        return;
                    }
                    for (var i = 0; i < count; i++) {
                        var ply = new BMap.Polygon(rs.boundaries[i], {
                            strokeWeight: 2,
                            strokeColor: color || "#00ee22",
                            fillOpacity: 0.1
                        }); //建立多边形覆盖物
                        map.addOverlay(ply); //添加覆盖物
                    }
                });
            },
            removeOverlay: function(overlay) {
                map.removeOverlay(overlay);
            },
            removeOverlays: function(overlays) {
                angular.forEach(overlays, function(overlay) {
                    map.removeOverlay(overlay);
                });
            },
            addOverlays: function(overlays) {
                angular.forEach(overlays, function(overlay) {
                    map.addOverlay(overlay);
                });
            },
            clearOverlays: function () {
                markerClusterer.clearMarkers();
                map.clearOverlays();
            },
            generateNeighborLines: function(lines, settings) {
                var zoom = map.getZoom();
                var rSector = geometryService.getRadius(zoom).rSector;
                var centerCell = getCellCenter(settings.cell, rSector / 2);
                angular.forEach(settings.neighbors, function(neighbor) {
                    networkElementService.queryCellInfo(neighbor.neighborCellId, neighbor.neighborSectorId).then(function(neighborCell) {
                        if (neighborCell) {
                            var neighborCenter = getCellCenter(neighborCell, rSector / 2);
                            var line = geometryService.getArrowLine(centerCell.longtitute + settings.xOffset,
                                centerCell.lattitute + settings.yOffset,
                                neighborCenter.longtitute + settings.xOffset, neighborCenter.lattitute + settings.yOffset, rSector / 2);
                            lines.push(line);
                        }

                    });
                });
            },
            generateReverseNeighborLines: function(lines, settings) {
                var zoom = map.getZoom();
                var rSector = geometryService.getRadius(zoom).rSector;
                var centerCell = getCellCenter(settings.cell, rSector / 2);
                angular.forEach(settings.neighbors, function(neighbor) {
                    networkElementService.queryCellInfo(neighbor.cellId, neighbor.sectorId).then(function(neighborCell) {
                        if (neighborCell) {
                            var neighborCenter = getCellCenter(neighborCell, rSector / 2);
                            var line = geometryService.getLine(centerCell.longtitute + settings.xOffset,
                                centerCell.lattitute + settings.yOffset,
                                neighborCenter.longtitute + settings.xOffset,
                                neighborCenter.lattitute + settings.yOffset, "red");
                            lines.push(line);
                        }

                    });
                });
            },
            generateInterferenceComponents: function(lines, circles, cell, neighbors, xOffset, yOffset, color, callback) {
                var zoom = map.getZoom();
                var rSector = geometryService.getRadius(zoom).rSector;
                var centerCell = getCellCenter(cell, rSector / 2);
                angular.forEach(neighbors, function(neighbor) {
                    if (neighbor.destENodebId > 0) {
                        networkElementService.queryCellInfo(neighbor.destENodebId, neighbor.destSectorId).then(function(neighborCell) {
                            var neighborCenter = getCellCenter(neighborCell, rSector / 2);
                            var line = geometryService.getLine(centerCell.longtitute + xOffset, centerCell.lattitute + yOffset,
                                neighborCenter.longtitute + xOffset, neighborCenter.lattitute + yOffset, color);
                            lines.push(line);
                            var circle = geometryService.getCircle(
                                neighborCenter.longtitute + xOffset,
                                neighborCenter.lattitute + yOffset,
                                50, color,
                                callback, neighbor);
                            circles.push(circle);
                        });
                    }
                });
            },
            generateVictimComponents: function(lines, circles, cell, neighbors, xOffset, yOffset, color, callback) {
                var zoom = map.getZoom();
                var rSector = geometryService.getRadius(zoom).rSector;
                var centerCell = getCellCenter(cell, rSector / 2);
                angular.forEach(neighbors, function(neighbor) {
                    if (neighbor.victimENodebId > 0) {
                        networkElementService.queryCellInfo(neighbor.victimENodebId, neighbor.victimSectorId).then(function(neighborCell) {
                            var neighborCenter = getCellCenter(neighborCell, rSector / 2);
                            var line = geometryService.getLine(centerCell.longtitute + xOffset, centerCell.lattitute + yOffset,
                                neighborCenter.longtitute + xOffset, neighborCenter.lattitute + yOffset, color);
                            lines.push(line);
                            var circle = geometryService.getCircle(
                                neighborCenter.longtitute + xOffset, neighborCenter.lattitute + yOffset, 50, color,
                                callback, neighbor);
                            circles.push(circle);
                        });
                    }
                });
            },
            addOneMarker: function(marker) {
                map.addOverlay(marker);
            },
            addOneMarkerToScope: function(marker, callback, data) {
                map.addOverlay(marker);
                marker.addEventListener("click", function() {
                    callback(data);
                });
            },
            addOneSector: function(sector, html, boxHeight) {
                boxHeight = boxHeight || "300px";
                map.addOverlay(sector);
                var infoBox = new BMapLib.InfoBox(map, html, {
                    boxStyle: {
                        background: "url('/Content/themes/baidu/tipbox.jpg') no-repeat center top",
                        width: "270px",
                        height: boxHeight
                    },
                    closeIconUrl: "/Content/themes/baidu/close.png",
                    closeIconMargin: "1px 1px 0 0",
                    enableAutoPan: true,
                    align: INFOBOX_AT_TOP
                });
                sector.addEventListener("click", function() {
                    infoBox.open(this.getPath()[2]);
                });
            },
            addOneSectorToScope: function(sector, callBack, neighbor) {
                sector.addEventListener("click", function() {
                    callBack(neighbor);
                });
                map.addOverlay(sector);
            },
            setCellFocus: function(longtitute, lattitute, zoomLevel) {
                zoomLevel = zoomLevel || 15;
                map.centerAndZoom(new BMap.Point(longtitute, lattitute), zoomLevel);
            },
            generateSector: function(data, sectorColor, scalor) {
                var center = { lng: data.longtitute, lat: data.lattitute };
                var iangle = 65;
                var irotation = data.azimuth - iangle / 2;
                var zoom = map.getZoom();
                var points = geometryService.generateSectorPolygonPoints(center, irotation, iangle, zoom, scalor);
                sectorColor = sectorColor || "blue";
                var sector = new BMap.Polygon(points, {
                    strokeWeight: 2,
                    strokeColor: sectorColor,
                    fillColor: sectorColor,
                    fillOpacity: 0.5
                });
                return sector;
            },
            getCurrentMapRange: function(xOffset, yOffset) {
                return {
                    west: map.getBounds().getSouthWest().lng + xOffset,
                    south: map.getBounds().getSouthWest().lat + yOffset,
                    east: map.getBounds().getNorthEast().lng + xOffset,
                    north: map.getBounds().getNorthEast().lat + yOffset
                };
            },
            generateIconMarker: function(longtitute, lattitute, iconUrl) {
                var icon = new BMap.Icon(iconUrl, new BMap.Size(20, 30));
                return new BMap.Marker(new BMap.Point(longtitute, lattitute), {
                    icon: icon
                });
            },
            generateMarker: function(longtitute, lattitute) {
                return new BMap.Marker(new BMap.Point(longtitute, lattitute));
            },
            generateCircleMarker: function(longtitute, lattitute, color) {
                return new BMap.Circle(new BMap.Point(longtitute, lattitute), 5,
                {
                    strokeColor: color,
                    strokeWeight: 1,
                    strokeOpacity: 0.8,
                    fillColor: color
                });
            },
            drawPolygonAndGetCenter: function(coors) {
                var centerx = 0;
                var centery = 0;
                var points = [];
                for (var p = 0; p < coors.length / 2; p++) {
                    points.push(new BMap.Point(parseFloat(coors[2 * p]), parseFloat(coors[2 * p + 1])));
                    centerx += parseFloat(coors[2 * p]);
                    centery += parseFloat(coors[2 * p + 1]);
                }
                centerx /= coors.length / 2;
                centery /= coors.length / 2;
                var polygon = new BMap.Polygon(points,
                { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(polygon);
                return {
                    X: centerx,
                    Y: centery,
                    points: points
                };
            },
            drawPolygon: function(coors) {
                var points = [];
                for (var p = 0; p < coors.length / 2; p++) {
                    points.push(new BMap.Point(parseFloat(coors[2 * p]), parseFloat(coors[2 * p + 1])));
                }
                var polygon = new BMap.Polygon(points,
                { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(polygon);
            },
            drawPolygonWithColor: function(coorsString, color, xoffset, yoffset) {
                var points = [];
                var coors = coorsString.split(';');
                if (coors[coors.length - 2].trim() === coors[0].trim()) {
                    coors = coors.slice(0, coors.length - 2);
                }
                angular.forEach(coors, function(coor) {
                    if (coor.length > 1) {
                        var fields = coor.split(',');
                        points.push(new BMap.Point(parseFloat(fields[0]) - xoffset, parseFloat(fields[1]) - yoffset));
                    }
                });
                var polygon = new BMap.Polygon(points,
                { strokeColor: color, strokeWeight: 1, strokeOpacity: 0.2, fillColor: color });
                map.addOverlay(polygon);
                return polygon.getPath();
            },
            drawRectangleAndGetCenter: function(coors) {
                var centerx = (parseFloat(coors[0]) + parseFloat(coors[2])) / 2;
                var centery = (parseFloat(coors[1]) + parseFloat(coors[3])) / 2;
                var points = [
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[3])),
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[3]))
                ];
                var rectangle = new BMap.Polygon(points, { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(rectangle);
                return {
                    X: centerx,
                    Y: centery,
                    points: points
                };
            },
            drawRectangle: function(coors) {
                var rectangle = new BMap.Polygon([
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[3])),
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[3]))
                ], { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(rectangle);
            },
            drawRectangleWithColor: function (coors, color) {
                var rectangle = new BMap.Polygon([
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[1])),
                    new BMap.Point(parseFloat(coors[2]), parseFloat(coors[3])),
                    new BMap.Point(parseFloat(coors[0]), parseFloat(coors[3]))
                ], { strokeColor: color, strokeWeight: 1, strokeOpacity: 0.2, fillColor: color });
                map.addOverlay(rectangle);
                return rectangle.getPath();
            },
            drawCircleAndGetCenter: function(coors) {
                var centerx = parseFloat(coors[0]);
                var centery = parseFloat(coors[1]);
                var circle = new BMap.Circle(new BMap.Point(centerx, centery),
                    parseFloat(coors[2]),
                    { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(circle);
                return {
                    X: centerx,
                    Y: centery,
                    points: points
                };
            },
            drawCircle: function(coors) {
                var circle = new BMap.Circle(new BMap.Point(parseFloat(coors[0]), parseFloat(coors[1])),
                    parseFloat(coors[2]),
                    { strokeColor: "blue", strokeWeight: 2, strokeOpacity: 0.2 });
                map.addOverlay(circle);
            },
            drawLabel: function(name, longtitute, lattitute) {
                var opts = {
                    position: new BMap.Point(longtitute, lattitute), // 指定文本标注所在的地理位置
                    offset: new BMap.Size(10, -20) //设置文本偏移量
                };
                var label = new BMap.Label(name, opts); // 创建文本标注对象
                label.setStyle({
                    color: "red",
                    fontSize: "12px",
                    height: "20px",
                    lineHeight: "20px",
                    fontFamily: "微软雅黑"
                });
                map.addOverlay(label);
            },
            drawCustomizeLabel: function(lon, lat, text, details, lines) {
                var myCompOverlay = new ComplexCustomOverlay(new BMap.Point(lon, lat), text, details, lines);

                map.addOverlay(myCompOverlay);
            },
            drawMultiPoints: function(coors, color, xoffset, yoffset, callback) {
                var points = []; // 添加海量点数据
                angular.forEach(coors, function(data) {
                    points.push(new BMap.Point(data.longtitute - xoffset, data.lattitute - yoffset));
                });
                var options = {
                    size: BMAP_POINT_SIZE_SMALL,
                    color: color
                }
                var pointCollection = new BMap.PointCollection(points, options); // 初始化PointCollection
                if (callback)
                    pointCollection.addEventListener('click', callback);
                map.addOverlay(pointCollection); // 添加Overlay
                return pointCollection;
            },
            drawPointsUsual: function(coors, xoffset, yoffset, callback) {
                angular.forEach(coors, function(data) {
                    var myIcon = new BMap.Icon("/Content/Images/Hotmap/markers.png", new BMap.Size(23, 25), {
                        offset: new BMap.Size(10, 25),
                        imageOffset: new BMap.Size(0, (0 - data.index + 1) * 25)
                    });
                    var point = new BMap.Point(data.longtitute - xoffset, data.lattitute - yoffset);
                    var marker = new BMap.Marker(point, { icon: myIcon });
                    marker.data = data;
                    marker.addEventListener("click", callback);
                    map.addOverlay(marker);
                });
                return;
            },
            drawPointCollection: function(coors, color, xoffset, yoffset, callback, pointSize) {

                var points = []; // 添加海量点数据
                var size = pointSize || BMAP_POINT_SIZE_BIG;
                angular.forEach(coors, function(data) {
                    var p = new BMap.Point(data.longtitute - xoffset, data.lattitute - yoffset);
                    p.data = data;
                    points.push(p);
                });
                var options = {
                    size: size,
                    shape: BMAP_POINT_SHAPE_CIRCLE,
                    color: color
                }
                var pointCollection = new BMap.PointCollection(points, options); // 初始化PointCollection
                if (callback)
                    pointCollection.addEventListener('click', callback);
                map.addOverlay(pointCollection); // 添加Overlay
                return pointCollection;
            },
            drawPointWithClusterer: function (coors, index, xoffset, yoffset, callback) {
                var MAX = 10;
                var markers = [];
                
                var iconStr = '/Content/Images/BtsIcons/btsicon_' + index + '.png';
                //alert(iconStr);
                angular.forEach(coors, function (data) {

                    var myIcon = new BMap.Icon(iconStr, new BMap.Size(22, 28), {
                        anchor: new BMap.Size(10, 30)
                    });

                    var point = new BMap.Point(data.longtitute - xoffset, data.lattitute - yoffset);
                    var marker = new BMap.Marker(point, { icon: myIcon });
                    marker.data = data;
                    marker.addEventListener("click", callback);
                    markers.push(marker);
                    //map.addOverlay(marker);
                });

                markerClusterer.addMarkers(markers);
                return;
            },
            isPointInCurrentCity: function(longtitute, lattitute) {
                return BMapLib.GeoUtils.isPointInPolygon(new BMap.Point(longtitute, lattitute), mapStructure.currentCityBounday);
            }
        };
    });
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
            parametersDialogService) {
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
                        var center = geometryService.queryRegionCenter(college);
                        callback(center);
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
                }
            };
        });
angular.module('topic.parameters.basic', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('map.eNodeb.dialog',
        function($scope,
            $uibModalInstance,
            eNodeb,
            dialogTitle,
            networkElementService,
            cellHuaweiMongoService,
            alarmImportService,
            intraFreqHoService,
            interFreqHoService,
            appFormatService,
            downSwitchService,
            alarmsService,
            appRegionService) {
            $scope.dialogTitle = dialogTitle;
            $scope.alarmLevel = {
                options: ["严重告警", "重要以上告警", "所有告警"],
                selected: "重要以上告警"
            };
            $scope.alarms = [];
            $scope.searchAlarms = function() {
                alarmsService.queryENodebAlarmsByDateSpanAndLevel(eNodeb.eNodebId,
                    $scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.alarmLevel.selected).then(function(result) {
                    $scope.alarms = result;
                });
            };

            $scope.searchAlarms();

            networkElementService.queryENodebInfo(eNodeb.eNodebId).then(function(result) {
                appRegionService.isInTownBoundary(result.longtitute,
                    result.lattitute,
                    result.cityName,
                    result.districtName,
                    result.townName).then(function(conclusion) {
                    var color = conclusion ? 'green' : 'red';
                    $scope.eNodebGroups = appFormatService.generateENodebGroups(result, color);
                });
                networkElementService.queryStationByENodeb(eNodeb.eNodebId, eNodeb.planNum).then(function(dict) {
                    if (dict) {
                        downSwitchService.getStationByStationId(dict.stationNum).then(function(stations) {
                            stations.result[0].Town = result.townName;
                            $scope.stationGroups = appFormatService.generateStationGroups(stations.result[0]);
                        });
                    }

                });
                if (result.factory === '华为') {
                    cellHuaweiMongoService.queryLocalCellDef(result.eNodebId).then(function(cellDef) {
                        alarmImportService.updateHuaweiAlarmInfos(cellDef).then(function() {});
                    });
                }
            });

            //查询该基站下带的小区列表
            networkElementService.queryCellViewsInOneENodeb(eNodeb.eNodebId).then(function(result) {
                $scope.cellList = result;
            });

            //查询基站同频切换参数
            intraFreqHoService.queryENodebParameters(eNodeb.eNodebId).then(function(result) {
                $scope.intraFreqHo = result;
            });

            //查询基站异频切换参数
            interFreqHoService.queryENodebParameters(eNodeb.eNodebId).then(function(result) {
                $scope.interFreqHo = result;
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.cdma.cell.dialog',
        function($scope,
            $uibModalInstance,
            neighbor,
            dialogTitle,
            networkElementService) {
            $scope.cdmaCellDetails = neighbor;
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };
            networkElementService.queryCdmaCellInfo(neighbor.btsId, neighbor.sectorId).then(function(result) {
                angular.extend($scope.cdmaCellDetails, result);
            });
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.bts.dialog',
        function($scope, $uibModalInstance, bts, dialogTitle, networkElementService) {
            $scope.bts = bts;
            $scope.dialogTitle = dialogTitle;

            networkElementService.queryBtsInfo(bts.btsId).then(function(result) {
                $scope.btsDetails = result;
            });
            networkElementService.queryCdmaCellViews(bts.name).then(function(result) {
                $scope.cdmaCellList = result;
            });
            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('topic.parameters', ['app.menu', 'app.core', 'topic.basic'])
    .factory('parametersDialogService',
        function(menuItemService, baiduMapService, stationFormatService) {
            return {
                showENodebInfo: function(eNodeb, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Map/ENodebMapInfoBox.html',
                        controller: 'map.eNodeb.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return eNodeb.name + "-" + "基站基本信息";
                                },
                                eNodeb: function() {
                                    return eNodeb;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showBtsInfo: function(bts) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Map/BtsMapInfoBox.html',
                        controller: 'map.bts.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return bts.name + "-" + "基站基本信息";
                            },
                            bts: function() {
                                return bts;
                            }
                        }
                    });
                },
                showCdmaCellInfo: function(cell) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Map/CdmaCellInfoBox.html',
                        controller: 'map.cdma.cell.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return cell.cellName + "小区信息";
                            },
                            neighbor: function() {
                                return cell;
                            }
                        }
                    });
                },
                showCheckPlanList: function(type) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckPlanListDialog.html',
                        controller: 'map.checkPlanList.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "巡检计划列表";
                            },
                            type: function() {
                                return type;
                            }
                        }
                    });
                },
                addCheckPlanDialog: function(stationId, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/AddCheckPlanDialog.html',
                        controller: 'map.addCheckPlan.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "添加巡检计划";
                            },
                            stationId: function() {
                                return stationId;
                            },
                            name: function() {
                                return name;
                            },
                        }
                    });
                },
                showCheckingResultsStationAdd: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckResultsStationAdd.html',
                        controller: 'map.checkingResultsStationAdd.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "巡检结果录入:" + station.StationName + ' ' + station.id;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                manageCsvDtInfos: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/CsvDtDialog.html',
                        controller: 'csv.dt.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "全网路测数据信息管理";
                                }
                            },
                            beginDate,
                            endDate)
                    });
                }
            };
        })
    .factory('parametersDisplayMapService',
        function(baiduMapService, parametersDialogService, baiduQueryService, networkElementService) {
            var showCellSectors = function(cells, xOffset, yOffset, beginDate, endDate, cellOverlays) {
                angular.forEach(cells,
                    function(cell) {
                        cell.longtitute += xOffset;
                        cell.lattitute += yOffset;
                        var cellSector = baiduMapService.generateSector(cell);
                        if (cellOverlays) {
                            cellOverlays.push(cellSector);
                        }
                        baiduMapService.addOneSectorToScope(cellSector,
                            function(item) {
                                parametersDialogService.showCellInfo(item, beginDate, endDate);
                            },
                            cell);
                    });
            };
            return {
                showENodebsElements:
                    function(eNodebs, beginDate, endDate, shouldShowCells, siteOverlays, cellOverlays) {
                        baiduQueryService.transformToBaidu(eNodebs[0].longtitute, eNodebs[0].lattitute)
                            .then(function(coors) {
                                var xOffset = coors.x - eNodebs[0].longtitute;
                                var yOffset = coors.y - eNodebs[0].lattitute;
                                angular.forEach(eNodebs,
                                    function(eNodeb) {
                                        eNodeb.longtitute += xOffset;
                                        eNodeb.lattitute += yOffset;
                                        var marker = baiduMapService.generateIconMarker(eNodeb.longtitute,
                                            eNodeb.lattitute,
                                            "/Content/Images/Hotmap/site_or.png");
                                        if (siteOverlays) {
                                            siteOverlays.push(marker);
                                        }
                                        baiduMapService.addOneMarkerToScope(marker,
                                            function(item) {
                                                parametersDialogService.showENodebInfo(item, beginDate, endDate);
                                            },
                                            eNodeb);
                                        if (shouldShowCells) {
                                            networkElementService.queryCellInfosInOneENodebUse(eNodeb.eNodebId)
                                                .then(function(cells) {
                                                    showCellSectors(cells,
                                                        xOffset,
                                                        yOffset,
                                                        beginDate,
                                                        endDate,
                                                        cellOverlays);
                                                });
                                        }

                                    });
                            });
                    },
                showCdmaElements: function(btss) {
                    baiduQueryService.transformToBaidu(btss[0].longtitute, btss[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - btss[0].longtitute;
                        var yOffset = coors.y - btss[0].lattitute;
                        angular.forEach(btss,
                            function(bts) {
                                bts.longtitute += xOffset;
                                bts.lattitute += yOffset;
                                var marker = baiduMapService.generateIconMarker(bts.longtitute,
                                    bts.lattitute,
                                    "/Content/Images/Hotmap/site_bl.png");
                                baiduMapService.addOneMarkerToScope(marker,
                                    function(item) {
                                        parametersDialogService.showBtsInfo(item);
                                    },
                                    bts);
                                networkElementService.queryCdmaCellInfosInOneBts(bts.btsId).then(function(cells) {
                                    angular.forEach(cells,
                                        function(cell) {
                                            cell.longtitute += xOffset;
                                            cell.lattitute += yOffset;
                                            cell.btsId = bts.btsId;
                                            var cellSector = baiduMapService.generateSector(cell);
                                            baiduMapService.addOneSectorToScope(cellSector,
                                                function(item) {
                                                    parametersDialogService.showCdmaCellInfo(item);
                                                },
                                                cell);
                                        });
                                });
                            });
                    });
                },
                showCellSectors: function(cells, xOffset, yOffset, beginDate, endDate, cellOverlays) {
                    showCellSectors(cells, xOffset, yOffset, beginDate, endDate, cellOverlays);
                }
            };
        })
    .factory('parametersMapService',
        function(baiduMapService,
            networkElementService,
            baiduQueryService,
            workItemDialog,
            neGeometryService,
            collegeQueryService,
            appRegionService,
            parametersDialogService,
            collegeService,
            alarmsService,
            parametersDisplayMapService,
            stationFactory) {

            return {
                showElementsInOneTown: function(city, district, town, beginDate, endDate, siteOverlays, cellOverlays) {
                    networkElementService.queryENodebsInOneTownUse(city, district, town).then(function(eNodebs) {
                        parametersDisplayMapService
                            .showENodebsElements(eNodebs, beginDate, endDate, true, siteOverlays, cellOverlays);
                    });
                },
                showElementsInRange:
                    function(west, east, south, north, beginDate, endDate, siteOverlays, cellOverlays) {
                        networkElementService.queryInRangeENodebs(west, east, south, north).then(function(eNodebs) {
                            parametersDisplayMapService
                                .showENodebsElements(eNodebs, beginDate, endDate, true, siteOverlays, cellOverlays);
                        });
                    },
                showElementsWithGeneralName: function(name, beginDate, endDate) {
                    networkElementService.queryENodebsByGeneralNameInUse(name).then(function(eNodebs) {
                        if (eNodebs.length === 0) return;
                        parametersDisplayMapService.showENodebsElements(eNodebs, beginDate, endDate, true);
                    });
                },
                showCdmaInOneTown: function(city, district, town) {
                    networkElementService.queryBtssInOneTown(city, district, town).then(function(btss) {
                        parametersDisplayMapService.showCdmaElements(btss);
                    });
                },
                showCdmaWithGeneralName: function(name) {
                    networkElementService.queryBtssByGeneralName(name).then(function(btss) {
                        if (btss.length === 0) return;
                        parametersDisplayMapService.showCdmaElements(btss);
                    });
                },
                showENodebs: function(eNodebs, beginDate, endDate) {
                    parametersDisplayMapService.showENodebsElements(eNodebs, beginDate, endDate, false);
                },
                showBtssElements: function(btss) {
                    return parametersDisplayMapService.showCdmaElements(btss);
                },
                showCellSectors: function(cells, showCellInfo) {
                    baiduQueryService.transformToBaidu(cells[0].longtitute, cells[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - cells[0].longtitute;
                        var yOffset = coors.y - cells[0].lattitute;
                        baiduMapService.setCellFocus(coors.x, coors.y, 16);
                        angular.forEach(cells,
                            function(cell) {
                                cell.longtitute += xOffset;
                                cell.lattitute += yOffset;
                                var cellSector = baiduMapService.generateSector(cell);
                                baiduMapService.addOneSectorToScope(cellSector,
                                    function(item) {
                                        showCellInfo(item);
                                    },
                                    cell);
                            });
                    });
                },
                showPhpElements: function(elements, showElementInfo) {
                    baiduQueryService.transformToBaidu(elements[0].longtitute, elements[0].lattitute)
                        .then(function(coors) {
                            var xOffset = coors.x - parseFloat(elements[0].longtitute);
                            var yOffset = coors.y - parseFloat(elements[0].lattitute);
                            angular.forEach(elements,
                                function(element) {
                                    element.longtitute = xOffset + parseFloat(element.longtitute);
                                    element.lattitute = yOffset + parseFloat(element.lattitute);
                                    var marker = baiduMapService.generateIconMarker(element.longtitute,
                                        element.lattitute,
                                        "/Content/Images/Hotmap/site_or.png");
                                    baiduMapService.addOneMarkerToScope(marker, showElementInfo, element);
                                });
                        });
                },
                showIntervalPoints: function(intervals, coverageOverlays) {
                    angular.forEach(intervals,
                        function(interval) {
                            var coors = interval.coors;
                            var index;
                            if (coors.length === 0) {
                                return;
                            } else
                                index = parseInt(coors.length / 2);
                            baiduQueryService.transformBaiduCoors(coors[index]).then(function(newCoor) {
                                var xoffset = coors[index].longtitute - newCoor.longtitute;
                                var yoffset = coors[index].lattitute - newCoor.lattitute;
                                var points = baiduMapService.drawMultiPoints(coors, interval.color, xoffset, yoffset);
                                if (coverageOverlays)
                                    coverageOverlays.push(points);
                            });
                        });
                },
                showIntervalGrids: function(intervals, coverageOverlays) {
                    angular.forEach(intervals,
                        function(interval) {
                            var coors = interval.coors;
                            var index;
                            if (coors.length === 0) {
                                return;
                            } else
                                index = parseInt(coors.length / 2);
                            baiduQueryService.transformBaiduCoors(coors[index]).then(function(newCoor) {
                                var xoffset = coors[index].longtitute - newCoor.longtitute;
                                var yoffset = coors[index].lattitute - newCoor.lattitute;
                                angular.forEach(coors,
                                    function(coor) {
                                        var polygon = baiduMapService.drawRectangleWithColor([
                                                coor.longtitute - xoffset,
                                                coor.lattitute - yoffset,
                                                coor.longtitute + 0.00049 - xoffset,
                                                coor.lattitute + 0.00045 - yoffset
                                            ],
                                            interval.color);
                                        if (coverageOverlays)
                                            coverageOverlays.push(polygon);
                                    });

                            });
                        });
                },
                displaySourceDistributions: function(sites, filters, colors) {
                    baiduQueryService.transformToBaidu(sites[0].longtitute, sites[0].lattitute).then(function(coors) {
                        var xOffset = coors.x - sites[0].longtitute;
                        var yOffset = coors.y - sites[0].lattitute;
                        angular.forEach(filters,
                            function(filter, $index) {
                                var stats = _.filter(sites, filter);
                                baiduMapService.drawMultiPoints(stats,
                                    colors[$index],
                                    -xOffset,
                                    -yOffset,
                                    function(e) {
                                        var xCenter = e.point.lng - xOffset;
                                        var yCenter = e.point.lat - yOffset;
                                        var container = neGeometryService.queryNearestRange(xCenter, yCenter);
                                        networkElementService.queryRangeDistributions(container).then(function(items) {
                                            if (items.length) {
                                                workItemDialog.showDistributionInfo(items[0]);
                                            }
                                        });
                                    });
                            });
                    });
                },
                showTownBoundaries: function(city, district, town, color) {
                    appRegionService.queryTownBoundaries(city, district, town).then(function(boundaries) {
                        angular.forEach(boundaries,
                            function(boundary) {
                                baiduQueryService
                                    .transformToBaidu(boundary.boundaryGeoPoints[0].longtitute,
                                        boundary.boundaryGeoPoints[0].lattitute).then(function(coors) {
                                        var xOffset = coors.x - boundary.boundaryGeoPoints[0].longtitute;
                                        var yOffset = coors.y - boundary.boundaryGeoPoints[0].lattitute;
                                        baiduMapService
                                            .addBoundary(boundary.boundaryGeoPoints, color, xOffset, yOffset);
                                    });
                            });
                    });
                },
                showAreaBoundaries: function() {
                    appRegionService.queryAreaBoundaries().then(function(boundaries) {
                        angular.forEach(boundaries,
                            function(boundary) {
                                var color = stationFactory.getAreaTypeColor(boundary.areaType);
                                baiduQueryService
                                    .transformToBaidu(boundary.boundaryGeoPoints[0].longtitute,
                                    boundary.boundaryGeoPoints[0].lattitute).then(function (coors) {
                                        var xOffset = coors.x - boundary.boundaryGeoPoints[0].longtitute;
                                        var yOffset = coors.y - boundary.boundaryGeoPoints[0].lattitute;
                                        baiduMapService
                                            .addFilledBoundary(boundary.boundaryGeoPoints, color, xOffset, yOffset);
                                        baiduMapService.drawLabel(boundary.areaName,
                                            boundary.boundaryGeoPoints[0].longtitute + xOffset,
                                            boundary.boundaryGeoPoints[0].lattitute + yOffset);
                                    });
                            });
                    });
                },
                displayClusterPoints: function(clusterList, overlays, threshold, baseCoor) {
                    var baseX = baseCoor ? baseCoor.longtitute : clusterList[0].longtitute;
                    var baseY = baseCoor ? baseCoor.lattitute : clusterList[0].lattitute;
                    baiduQueryService.transformToBaidu(baseX, baseY).then(function(coors) {
                        var xOffset = coors.x - baseX;
                        var yOffset = coors.y - baseY;
                        angular.forEach(clusterList,
                            function(stat) {
                                var centerX = stat.bestLongtitute + xOffset + 0.000245;
                                var centerY = stat.bestLattitute + yOffset + 0.000225;
                                if (baiduMapService.isPointInCurrentCity(centerX, centerY) &&
                                    stat.gridPoints.length > threshold) {
                                    var marker = baiduMapService.generateIconMarker(centerX,
                                        centerY,
                                        "/Content/Images/BtsIcons/m_8_end.png");
                                    overlays.push(marker);
                                    baiduMapService.addOneMarkerToScope(marker,
                                        function(data) {
                                            alarmsService.queryClusterGridKpis(stat.gridPoints).then(function(list) {
                                                parametersDialogService.showClusterPointInfo(data, list);
                                            });
                                        },
                                        stat);
                                }

                            });
                    });
                },
                displayClusterPoint: function(stat, currentClusterList) {
                    baiduQueryService.transformToBaidu(stat.bestLongtitute, stat.bestLattitute).then(function(coors) {
                        var marker = baiduMapService
                            .generateIconMarker(coors.x + 0.000245,
                                coors.y + 0.000225,
                                "/Content/Images/BtsIcons/m_2_end.png");
                        baiduMapService.addOneMarkerToScope(marker,
                            function(data) {
                                parametersDialogService.showClusterPointInfo(data, currentClusterList);
                            },
                            stat);
                    });
                },
                clearOverlaySites: function(sites) {
                    angular.forEach(sites,
                        function(site) {
                            baiduMapService.removeOverlay(site);
                        });
                    sites = [];
                }
            }
        });

angular.module('topic.dialog.customer', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('micro.dialog',
        function($scope, $uibModalInstance, dialogTitle, item, appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.detailsGroups = appFormatService.generateMicroAddressGroups(item);
            $scope.microGroups = [];
            angular.forEach(item.microItems,
                function(micro) {
                    $scope.microGroups.push(appFormatService.generateMicroItemGroups(micro));
                });
        });
angular.module("topic.dialog.college", ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .run(function($rootScope) {
        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };
    })
    .controller("hotSpot.flow.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "流量分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFlowStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFlowCharts($scope.flowStats, name, $scope.mergeStats);
            };
            complainService.queryHotSpotCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("hotSpot.feeling.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            complainService,
            appKpiService,
            kpiChartService) {
            $scope.dialogTitle = name + "感知速率分析";
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.flowStats = [];
            $scope.mergeStats = [];
            $scope.query = function() {
                appKpiService.calculateFeelingStats($scope.cellList,
                    $scope.flowStats,
                    $scope.mergeStats,
                    $scope.beginDate,
                    $scope.endDate);
            };
            $scope.showCharts = function() {
                kpiChartService.showFeelingCharts($scope.flowStats, name, $scope.mergeStats);
            };
            complainService.queryHotSpotCells(name).then(function(cells) {
                $scope.cellList = cells;
                $scope.query();
            });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('college.coverage.all',
        function($scope,
            beginDate,
            endDate,
            $uibModalInstance,
            collegeMapService) {
            $scope.dialogTitle = "校园网路测数据查询";
            $scope.dtInfos = [];
            $scope.query = function() {
                collegeMapService.showDtInfos($scope.dtInfos, beginDate.value, endDate.value);
            };
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('topic.dialog',[ 'app.menu', 'app.core' ])
    .factory('mapDialogService',
        function (menuItemService, stationFormatService) {
            return {
                showMicroAmpliferInfos: function(item) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Dialog/Micro.html',
                        controller: 'micro.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return item.addressNumber + "手机伴侣基本信息";
                            },
                            item: function() {
                                return item;
                            }
                        }
                    });
                },
                showCollegeCoverageList: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Coverage/All.html',
                        controller: 'college.coverage.all',
                        resolve: stationFormatService.dateSpanDateResolve({}, beginDate, endDate)
                    });
                },
                showHotSpotFlowTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'hotSpot.flow.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showCollegeFeelingTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'college.feeling.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showHotSpotFeelingTrend: function (beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'hotSpot.feeling.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showCollegeFlowDumpDialog: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Table/DumpCollegeFlowTable.html',
                        controller: 'college.flow.dump',
                        resolve: stationFormatService.dateSpanDateResolve({},
                            beginDate,
                            endDate)
                    });
                }
            };
        });
angular.module('baidu.map',
[
    'topic.basic', 'topic.college',
    'topic.parameters.basic', "topic.parameters",
    'topic.dialog', 'topic.dialog.college', 'topic.dialog.customer'
]);