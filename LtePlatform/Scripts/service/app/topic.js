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
                drawCollegeArea: function(collegeId, callback) {
                    collegeService.queryRegion(collegeId).then(function(region) {
                        var center;
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
                        baiduMapService.setCellFocus(center.X, center.Y);
                        callback(center);
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
                showCheckingStations: function (stations, color, status, type) {
                    if (status === '已巡检') {
                        if (type === 'JZ') {
                            generalMapService
                                .showPointWithClusterer(stations, color, function (station) {
                                    parametersDialogService.showCheckingStationInfo(station);
                                });
                        } else {
                            generalMapService
                                .showPointWithClusterer(stations, color, function (station) {
                                    parametersDialogService.showCheckingIndoorInfo(station);
                                });
                        }
                    } else {
                        generalMapService
                            .showPointWithClusterer(stations, color, function (station) {
                                mapDialogService.showCommonStationInfo(station);
                            });
                    }
                    /*
                    generalMapService
                        .showPointWithClusterer(stations, color, function (station) {
                            if (status === '已巡检') {
                                if (type === 'JZ')
                                    parametersDialogService.showCheckingStationInfo(station);
                                else
                                    parametersDialogService.showCheckingIndoorInfo(station);
                            } else {
                                mapDialogService.showCommonStationInfo(station);
                            }
                        });*/
                },
                showFixingStations: function(stations, color) {
                    generalMapService
                        .showPointWithClusterer(stations, color, mapDialogService.showFixingStationInfo);
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
    .controller('map.neighbor.dialog',
        function($scope,
            $uibModalInstance,
            interFreqHoService,
            neighborDialogService,
            flowService,
            kpiChartCalculateService,
            neighbor,
            dialogTitle,
            beginDate,
            endDate) {
            $scope.neighbor = neighbor;
            $scope.eNodebId = neighbor.otherInfos.split(': ')[5];
            $scope.sectorId = neighbor.cellName.split('-')[1];
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.parameter = {
                options: [
                    '基本参数', '同频切换', '异频切换'
                ],
                selected: '基本参数'
            };

            $scope.dump = function() {
                neighborDialogService.dumpCellMongo({
                        eNodebId: $scope.eNodebId,
                        sectorId: $scope.sectorId,
                        pci: neighbor.pci,
                        name: neighbor.cellName.split('-')[0]
                    },
                    $scope.beginDate.value,
                    $scope.endDate.value);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.queryFlow = function() {
                flowService.queryCellFlowByDateSpan($scope.eNodebId,
                    $scope.sectorId,
                    $scope.beginDate.value,
                    $scope.endDate.value).then(function(results) {
                        $("#flowChart").highcharts(kpiChartCalculateService.generateMergeFlowOptions(results, neighbor.cellName));
                        $("#usersChart").highcharts(kpiChartCalculateService.generateMergeUsersOptions(results, neighbor.cellName));
                });
            };

            interFreqHoService.queryCellParameters($scope.eNodebId, $scope.sectorId).then(function(result) {
                $scope.interFreqHo = result;
                $scope.queryFlow();
            });
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
angular.module('topic.parameters.coverage', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('town.dt.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            item,
            kpiDisplayService,
            baiduMapService,
            parametersMapService,
            coverageService,
            collegeService) {
            $scope.dialogTitle = dialogTitle;

            $scope.includeAllFiles = false;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };
            $scope.dataFile = {
                options: [],
                selected: ''
            };
            $scope.data = [];
            $scope.coverageOverlays = [];

            $scope.query = function() {
                $scope.kpi = kpiDisplayService.queryKpiOptions($scope.network.selected);
                collegeService.queryTownRaster($scope.network.selected,
                    item.townName,
                    $scope.longBeginDate.value,
                    $scope.endDate.value).then(function(results) {
                    baiduMapService.initializeMap("all-map", 14);
                    baiduMapService.setCellFocus(item.longtitute, item.lattitute, 14);
                    if (results.length) {
                        $scope.dataFile.options = results;
                        $scope.dataFile.selected = results[0];
                    }
                });

            };

            $scope.$watch('network.selected',
                function() {
                    baiduMapService.switchSubMap();
                    $scope.query();
                });

            $scope.showDtPoints = function() {
                $scope.legend = kpiDisplayService.queryCoverageLegend($scope.kpi.selected);
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateCoveragePoints($scope.coveragePoints, $scope.data, $scope.kpi.selected);
                angular.forEach($scope.coverageOverlays,
                    function(overlay) {
                        baiduMapService.removeOverlay(overlay);
                    });
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.coverageOverlays);
            };

            var queryRasterInfo = function(index) {
                coverageService.queryByRasterInfo($scope.dataFile.options[index], $scope.network.selected)
                    .then(function(result) {
                        $scope.data.push.apply($scope.data, result);
                        if (index < $scope.dataFile.options.length - 1) {
                            queryRasterInfo(index + 1);
                        } else {
                            $scope.showDtPoints();
                        }
                    });
            };

            $scope.showStat = function() {
                $scope.data = [];
                if ($scope.includeAllFiles) {
                    queryRasterInfo(0);
                } else {
                    coverageService.queryByRasterInfo($scope.dataFile.selected, $scope.network.selected)
                        .then(function(result) {
                            $scope.data = result;
                            $scope.showDtPoints();
                        });
                }
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('csv.dt.dialog',
        function($scope,
            dialogTitle,
            beginDate,
            endDate,
            collegeService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };

            $scope.query = function() {
                collegeService.queryCsvFileNames($scope.beginDate.value, $scope.endDate.value).then(function(infos) {
                    $scope.fileInfos = infos;
                    angular.forEach(infos,
                        function(info) {
                            collegeService.queryCsvFileType(info.csvFileName.replace('.csv', '')).then(function(type) {
                                info.networkType = type;
                            });
                            collegeService.queryFileTownDtTestInfo(info.id).then(function(items) {
                                info.townInfos = items;
                            });
                            collegeService.queryFileRoadDtTestInfo(info.id).then(function(items) {
                                info.roadInfos = items;
                            });
                        });
                });
            };

            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('highway.dt.dialog',
        function($scope,
            dialogTitle,
            beginDate,
            endDate,
            name,
            collegeService,
            parametersChartService,
            $uibModalInstance) {
            $scope.dialogTitle = dialogTitle;
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.query = function() {
                collegeService.queryRoadDtFileInfos(name, $scope.beginDate.value, $scope.endDate.value)
                    .then(function(infos) {
                        $scope.fileInfos = infos;
                        angular.forEach(infos,
                            function(info) {
                                collegeService.queryCsvFileType(info.csvFileName.replace('.csv', ''))
                                    .then(function(type) {
                                        info.networkType = type;
                                    });
                            });
                        $("#distanceDistribution").highcharts(parametersChartService
                            .getHotSpotDtDistancePieOptions(name, infos));
                        $("#coverageRate").highcharts(parametersChartService
                            .getHotSpotDtCoverageRateOptions(name, infos));
                    });
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.bts);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query();
        })
    .controller('college.coverage.name',
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            coverageOverlays,
            baiduMapService,
            collegeQueryService,
            baiduQueryService,
            collegeService,
            collegeMapService,
            collegeDtService,
            coverageService,
            kpiDisplayService,
            parametersMapService) {
            $scope.dialogTitle = name + '覆盖情况评估';
            $scope.includeAllFiles = false;
            $scope.network = {
                options: ['2G', '3G', '4G'],
                selected: '2G'
            };
            $scope.dataFile = {
                options: [],
                selected: ''
            };
            $scope.data = [];
            $scope.coverageOverlays = coverageOverlays;

            $scope.query = function() {
                $scope.kpi = kpiDisplayService.queryKpiOptions($scope.network.selected);
                $scope.legend = kpiDisplayService.queryCoverageLegend($scope.kpi.selected);
                $scope.legend.title = $scope.kpi.selected;
                collegeDtService.queryRaster($scope.center,
                    $scope.network.selected,
                    beginDate.value,
                    endDate.value,
                    function(files) {
                        $scope.dataFile.options = files;
                        if (files.length) {
                            $scope.dataFile.selected = files[0];
                        }
                        $scope.dtList = files;
                        angular.forEach(files,
                            function(file) {
                                collegeService.queryCsvFileInfo(file.csvFileName).then(function(info) {
                                    angular.extend(file, info);
                                });
                            });
                    });
            };

            $scope.$watch('network.selected',
                function() {
                    if ($scope.center) {
                        $scope.query();
                    }
                });

            $scope.showDtPoints = function() {
                $scope.coveragePoints = kpiDisplayService.initializeCoveragePoints($scope.legend);
                kpiDisplayService.generateCoveragePoints($scope.coveragePoints, $scope.data, $scope.kpi.selected);
                angular.forEach($scope.coverageOverlays,
                    function(overlay) {
                        baiduMapService.removeOverlay(overlay);
                    });
                parametersMapService.showIntervalPoints($scope.coveragePoints.intervals, $scope.coverageOverlays);
            };

            var queryRasterInfo = function(index) {
                coverageService.queryByRasterInfo($scope.dataFile.options[index],
                        $scope.network.selected)
                    .then(function(result) {
                        $scope.data.push.apply($scope.data, result);
                        if (index < $scope.dataFile.options.length - 1) {
                            queryRasterInfo(index + 1);
                        } else {
                            $scope.showDtPoints();
                        }
                    });
            };

            $scope.showResults = function() {
                $scope.data = [];
                if ($scope.includeAllFiles) {
                    queryRasterInfo(0);
                } else {
                    coverageService.queryByRasterInfo($scope.dataFile.selected,
                            $scope.network.selected)
                        .then(function(result) {
                            $scope.data = result;
                            $scope.showDtPoints();
                        });
                }
            };

            $scope.showStat = function() {
                if ($scope.includeAllFiles) {
                    var combined = _.reduce($scope.dataFile.options,
                        function(memo, num) {
                            return {
                                count: memo.count + num.count,
                                coverageCount: memo.coverageCount + num.coverageCount
                            }
                        });
                    $scope.coverageRate = 100 * combined.coverageCount / combined.count;
                } else {
                    $scope.coverageRate = $scope.dataFile.selected.coverageRate;
                }
            };

            collegeMapService.queryCenterAndCallback(name,
                function(center) {
                    baiduQueryService.transformToBaidu(center.X, center.Y).then(function(coors) {
                        $scope.center = {
                            centerX: 2 * center.X - coors.x,
                            centerY: 2 * center.Y - coors.y
                        };
                        $scope.query();
                    });
                });

            $scope.ok = function() {
                $scope.showResults();
                $uibModalInstance.close($scope.legend);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('cluster.point.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            site,
            currentClusterList,
            alarmsService) {
            $scope.dialogTitle = dialogTitle;
            $scope.currentClusterList = currentClusterList;
            angular.forEach(currentClusterList,
                function(stat) {
                    alarmsService.queryDpiGridKpi(stat.x, stat.y).then(function(result) {
                        stat.firstPacketDelay = result.firstPacketDelay;
                        stat.pageOpenDelay = result.pageOpenDelay;
                        stat.firstPacketDelayClass = result.firstPacketDelayClass;
                        stat.pageOpenDelayClass = result.pageOpenDelayClass;
                    });
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.site);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
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
                showCellInfo: function(cell, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Map/NeighborMapInfoBox.html',
                        controller: 'map.neighbor.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return cell.cellName + "小区信息";
                                },
                                neighbor: function() {
                                    return cell;
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
                showTownDtInfo: function(item) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/College/Coverage/TownMap.html',
                            controller: 'town.dt.dialog',
                            resolve: {
                                dialogTitle: function() {
                                    return item.cityName + item.districtName + item.townName + "-" + "路测数据文件信息";
                                },
                                item: function() {
                                    return item;
                                }
                            }
                        },
                        function(info) {
                            baiduMapService.switchMainMap();
                        });
                },
                showCheckPlanList: function (type) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckPlanListDialog.html',
                        controller: 'map.checkPlanList.dialog',
                        resolve: {
                            dialogTitle: function () {
                                return "巡检计划列表";
                            },
                            type: function () {
                                return type;
                            }
                        }
                    });
                },
                addCheckPlanDialog: function (stationId,name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/AddCheckPlanDialog.html',
                        controller: 'map.addCheckPlan.dialog',
                        resolve: {
                            dialogTitle: function () {
                                return "添加巡检计划";
                            },
                            stationId: function () {
                                return stationId;
                            },
                            name: function () {
                                return name;
                            },
                        }
                    });
                },
                showCommonStationEdit: function(stationId) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/CommonStationEdit.html',
                        controller: 'map.common-stationEdit.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "编辑站点";
                            },
                            stationId: function() {
                                return stationId;
                            }
                        }
                    });
                },
                showCommonStationAdd: function(type) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/CommonStationAdd.html',
                        controller: 'map.common-stationAdd.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点添加";
                            },
                            type: function() {
                                return type;
                            }
                        }
                    });
                },
                showCheckingStationInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckDetails.html',
                        controller: 'map.checkingStation.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "巡检信息:" + station.name;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showCheckingResultsStationAdd: function (station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckResultsStationAdd.html',
                        controller: 'map.checkingResultsStationAdd.dialog',
                        resolve: {
                            dialogTitle: function () {
                                return "巡检结果录入:" + station.StationName + ' '+ station.id;
                            },
                            station: function () {
                                return station;
                            }
                        }
                    });
                },
                showCheckingIndoorInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CheckIndoorDetails.html',
                        controller: 'map.checkingStation.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "巡检信息:" + station.name;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showConstructionInfo: function(site) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/Construction.html',
                        controller: 'map.construction.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return site.eNodebName + "站点信息";
                            },
                            site: function() {
                                return site;
                            }
                        }
                    });
                },
                showClusterPointInfo: function(site, currentClusterList) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/ClusterPoint.html',
                        controller: 'cluster.point.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return site.theme + "主题" + site.clusterNumber + "编号簇规划选点信息";
                            },
                            site: function() {
                                return site;
                            },
                            currentClusterList: function() {
                                return currentClusterList;
                            }
                        }
                    });
                },
                showCollegeCoverage: function(name, beginDate, endDate, coverageOverlays, callback) {
                    menuItemService.showGeneralDialogWithAction({
                            templateUrl: '/appViews/College/Coverage/CollegeMap.html',
                            controller: 'college.coverage.name',
                            resolve: stationFormatService.dateSpanDateResolve({
                                    name: function() {
                                        return name;
                                    },
                                    coverageOverlays: function() {
                                        return coverageOverlays;
                                    }
                                },
                                beginDate,
                                endDate)
                        },
                        callback);
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
                },
                showHighwayDtInfos: function(beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/HotspotDtDialog.html',
                        controller: 'highway.dt.dialog',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return name + "路测数据信息查询";
                                },
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                }
            }
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
                showHotSpotCellSectors: function(hotSpotName, beginDate, endDate) {
                    collegeQueryService.queryHotSpotSectors(hotSpotName).then(function(sectors) {
                        baiduQueryService.transformToBaidu(sectors[0].longtitute, sectors[0].lattitute)
                            .then(function(coors) {
                                var xOffset = coors.x - sectors[0].longtitute;
                                var yOffset = coors.y - sectors[0].lattitute;
                                parametersDisplayMapService.showCellSectors(sectors, xOffset, yOffset, beginDate, endDate);
                            });
                    });
                },
                showCollegeENodebs: function(name, beginDate, endDate) {
                    collegeService.queryENodebs(name).then(function(eNodebs) {
                        parametersDisplayMapService.showENodebsElements(eNodebs, beginDate, endDate);
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
    .controller('online.sustain.dialog',
        function($scope,
            $uibModalInstance,
            items,
            dialogTitle,
            networkElementService,
            cellHuaweiMongoService,
            alarmImportService,
            intraFreqHoService,
            interFreqHoService,
            appFormatService) {
            $scope.dialogTitle = dialogTitle;
            $scope.itemGroups = [];

            angular.forEach(items,
                function(item) {
                    $scope.itemGroups.push(appFormatService.generateSustainGroups(item));
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodebGroups);
            };
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        })
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
        })
    .controller("workitem.city",
        function($scope,
            $uibModalInstance,
            endDate,
            preciseWorkItemService,
            workItemDialog) {
            $scope.dialogTitle = "精确覆盖优化工单一览";
            var lastSeason = new Date();
            lastSeason.setDate(lastSeason.getDate() - 100);
            $scope.seasonDate = {
                value: new Date(lastSeason.getFullYear(), lastSeason.getMonth(), lastSeason.getDate(), 8),
                opened: false
            };
            $scope.endDate = endDate;
            $scope.queryWorkItems = function() {
                preciseWorkItemService.queryByDateSpan($scope.seasonDate.value, $scope.endDate.value)
                    .then(function(views) {
                        angular.forEach(views,
                            function(view) {
                                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
                            });
                        $scope.viewItems = views;
                    });
            };
            $scope.showDetails = function(view) {
                workItemDialog.showDetails(view, $scope.queryWorkItems);
            };
            $scope.queryWorkItems();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("workitem.district",
        function($scope,
            $uibModalInstance,
            district,
            endDate,
            preciseWorkItemService,
            workItemDialog) {
            $scope.dialogTitle = district + "精确覆盖优化工单一览";
            var lastSeason = new Date();
            lastSeason.setDate(lastSeason.getDate() - 100);
            $scope.seasonDate = {
                value: new Date(lastSeason.getFullYear(), lastSeason.getMonth(), lastSeason.getDate(), 8),
                opened: false
            };
            $scope.endDate = endDate;
            $scope.queryWorkItems = function() {
                preciseWorkItemService.queryByDateSpanDistrict($scope.seasonDate.value, $scope.endDate.value, district)
                    .then(function(views) {
                        angular.forEach(views,
                            function(view) {
                                view.detailsPath = $scope.rootPath + "details/" + view.serialNumber;
                            });
                        $scope.viewItems = views;
                    });
            };
            $scope.showDetails = function(view) {
                workItemDialog.showDetails(view, $scope.queryWorkItems);
            };
            $scope.queryWorkItems();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("customer.index",
        function($scope,
            $uibModalInstance,
            complainService,
            appKpiService) {
            $scope.statDate = {
                value: new Date(),
                opened: false
            };
            $scope.monthObject = 498;
            $scope.query = function() {
                complainService.queryCurrentComplains($scope.statDate.value).then(function(count) {
                    $scope.count = count;
                    var objects = [];
                    complainService.queryMonthTrend($scope.statDate.value).then(function(stat) {
                        angular.forEach(stat.item1,
                            function(date, index) {
                                objects.push((index + 1) / stat.item1.length * $scope.monthObject);
                            });
                        var options = appKpiService.generateComplainTrendOptions(stat.item1, stat.item2, objects);
                        $('#line-chart').highcharts(options);
                    });
                });
            };
            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("customer.yesterday",
        function($scope,
            $uibModalInstance,
            city,
            complainService) {
            $scope.statDate = {
                value: new Date(),
                opened: false
            };
            $scope.city = city;
            $scope.data = {
                complainList: []
            };
            $scope.query = function() {
                complainService.queryLastDateComplainStats($scope.statDate.value).then(function(result) {
                    $scope.statDate.value = result.statDate;
                    $scope.districtStats = result.districtComplainViews;
                });
            };
            $scope.query();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
    })
    .controller("customer.recent",
        function ($scope,
            $uibModalInstance,
            city,
            beginDate,
            endDate,
            complainService) {
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.city = city;
            $scope.data = {
                complainList: []
            };
            $scope.query = function () {
                complainService.queryDateSpanComplainStats($scope.beginDate.value, $scope.endDate.value).then(function (result) {
                    $scope.districtStats = result;
                });
            };
            $scope.query();
            $scope.ok = function () {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("complain.details",
        function($scope, $uibModalInstance, item, appFormatService) {
            $scope.dialogTitle = item.serialNumber + "投诉工单详细信息";
            $scope.complainGroups = appFormatService.generateComplainItemGroups(item);
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('complain.adjust',
        function ($scope, $uibModalInstance, complainService) {
            $scope.dialogTitle = "抱怨量信息校正";
            $scope.items = [];

            $scope.query = function() {
                complainService.queryPositionList($scope.beginDate.value, $scope.endDate.value).then(function(list) {
                    $scope.items = list;
                });
            };

            $scope.ok = function () {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };
            $scope.query();
        });
angular.module('topic.dialog.parameters', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', 'topic.dialog', "ui.bootstrap"])
    .controller('town.eNodeb.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            city,
            district,
            town,
            networkElementService,
            appRegionService,
            parametersChartService,
            mapDialogService) {
            $scope.dialogTitle = dialogTitle;
            networkElementService.queryENodebsInOneTown(city, district, town).then(function(eNodebs) {
                $scope.eNodebList = eNodebs;
            });
            networkElementService.queryBtssInOneTown(city, district, town).then(function(btss) {
                $scope.btsList = btss;
            });
            appRegionService.queryTownLteCount(city, district, town, true).then(function(stat) {
                $("#indoorChart").highcharts(parametersChartService
                    .getLteCellCountOptions(city + district + town + '室内', stat));
            });
            appRegionService.queryTownLteCount(city, district, town, false).then(function(stat) {
                $("#outdoorChart").highcharts(parametersChartService
                    .getLteCellCountOptions(city + district + town + '室外', stat));
            });

            $scope.arrangeENodebs = function() {
                mapDialogService.arrangeTownENodebInfo(city, district, town);
            };
            $scope.arrangeBtss = function() {
                mapDialogService.arrangeTownBtsInfo(city, district, town);
            };

            $scope.ok = function() {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
    })
    .controller('arrange.eNodeb.dialog',
        function($scope,
            $uibModalInstance,
            dialogTitle,
            city,
            district,
            town,
            networkElementService) {
            $scope.dialogTitle = dialogTitle;

            $scope.query = function() {
                networkElementService.queryENodebsInOneTown(city, district, town).then(function(eNodebs) {
                    $scope.currentENodebList = eNodebs;
                });
                networkElementService.queryENodebsByTownArea(city, district, town).then(function(eNodebs) {
                    $scope.candidateENodebList = eNodebs;
                });
            };

            $scope.arrangeLte = function(index) {
                networkElementService.updateENodebTownInfo($scope.candidateENodebList[index]).then(function(result) {
                    if (index < $scope.candidateENodebList.length - 1) {
                        $scope.arrangeLte(index + 1);
                    } else {
                        $scope.query();
                    }
                });
            };

            $scope.ok = function () {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query();
        })
    .controller('arrange.bts.dialog',
        function ($scope,
            $uibModalInstance,
            dialogTitle,
            city,
            district,
            town,
            networkElementService) {
            $scope.dialogTitle = dialogTitle;

            $scope.query = function() {
                networkElementService.queryBtssInOneTown(city, district, town).then(function(btss) {
                    $scope.currentBtsList = btss;
                });
                networkElementService.queryBtssByTownArea(city, district, town).then(function(btss) {
                    $scope.candidateBtsList = btss;
                });
            };

            $scope.arrangeCdma = function (index) {
                networkElementService.updateBtsTownInfo($scope.candidateBtsList[index]).then(function (result) {
                    if (index < $scope.candidateBtsList.length - 1) {
                        $scope.arrangeCdma(index + 1);
                    } else {
                        $scope.query();
                    }
                });
            };

            $scope.ok = function () {
                $uibModalInstance.close($scope.eNodeb);
            };

            $scope.cancel = function () {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.query();
        })
    .controller('hot.spot.info.dialog',
        function($scope, $uibModalInstance, dialogTitle, hotSpotList) {
            $scope.dialogTitle = dialogTitle;
            $scope.hotSpotList = hotSpotList;

            $scope.ok = function() {
                $uibModalInstance.close($scope.neighbor);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        })
    .controller('map.sectors.dialog',
        function($scope, $uibModalInstance, sectors, dialogTitle, networkElementService) {
            $scope.sectors = sectors;
            $scope.dialogTitle = dialogTitle;
            if (sectors.length > 0) {
                networkElementService.queryCellInfo(sectors[0].eNodebId, sectors[0].sectorId).then(function(result) {
                    $scope.currentCell = result;
                });
            }
            $scope.ok = function() {
                $uibModalInstance.close($scope.sectors);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

        })
    .controller('map.site.dialog',
        function($scope, $uibModalInstance, site, dialogTitle, appFormatService, networkElementService) {
            $scope.itemGroups = appFormatService.generateSiteGroups(site);
            $scope.detailsGroups = appFormatService.generateSiteDetailsGroups(site);
            $scope.cellGroups = [];
            networkElementService.queryENodebByPlanNum(site.planNum).then(function(eNodeb) {
                if (eNodeb) {
                    $scope.eNodebGroups = appFormatService.generateENodebGroups(eNodeb);
                    $scope.eNodeb = eNodeb;
                } else {
                    networkElementService.queryLteRrusFromPlanNum(site.planNum).then(function(cells) {
                        angular.forEach(cells,
                            function(cell) {
                                $scope.cellGroups.push({
                                    cellName: cell.cellName,
                                    cellGroup: appFormatService.generateCellGroups(cell),
                                    rruGroup: appFormatService.generateRruGroups(cell)
                                });
                            });
                        if (cells.length) {
                            networkElementService.queryENodebInfo(cells[0].eNodebId).then(function(item) {
                                if (item) {
                                    $scope.eNodebGroups = appFormatService.generateENodebGroups(item);
                                    $scope.eNodeb = item;
                                    networkElementService.queryCellViewsInOneENodeb(item.eNodebId)
                                        .then(function(cellList) {
                                            $scope.cellList = cellList;
                                        });
                                }
                            });
                        }
                    });
                }
            });
            $scope.dialogTitle = dialogTitle;
            $scope.ok = function() {
                $uibModalInstance.close($scope.site);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.building.dialog',
        function($scope, $uibModalInstance, building, dialogTitle) {
            $scope.building = building;
            $scope.dialogTitle = dialogTitle;

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module("topic.dialog.college", ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .run(function($rootScope) {
        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };
    })
    .controller("college.flow.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            collegeService,
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
            collegeService.queryCells(name).then(function(cells) {
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
    .controller("college.feeling.name",
        function($scope,
            $uibModalInstance,
            name,
            beginDate,
            endDate,
            collegeService,
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
            collegeService.queryCells(name).then(function(cells) {
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
            collegeDtService,
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
        })
    .controller('college.flow.dump',
        function($scope,
            $uibModalInstance,
            beginDate,
            endDate,
            basicCalculationService,
            appRegionService,
            collegeQueryService,
            kpiDisplayService) {
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.dialogTitle = "校园网流量导入";
            $scope.query = function() {
                $scope.stats = basicCalculationService
                    .generateDateSpanSeries($scope.beginDate.value, $scope.endDate.value);
                angular.forEach($scope.stats,
                    function(stat) {
                        appRegionService.getCurrentDateTownFlowStats(stat.date, 'college').then(function(items) {
                            stat.items = items;
                            if (!items.length) {
                                collegeQueryService.retrieveDateCollegeFlowStats(stat.date).then(function(newItems) {
                                    stat.items = newItems;
                                    angular.forEach(newItems,
                                        function(item) {
                                            appRegionService.updateTownFlowStat(item).then(function(result) {});
                                        });
                                    $scope.updateCollegeNames(newItems);
                                });
                            } else {
                                $scope.updateCollegeNames(items);
                            }
                        });
                    });
            };
            $scope.updateCollegeNames = function(items) {
                angular.forEach(items,
                    function(item) {
                        collegeQueryService.queryCollegeById(item.townId).then(function(college) {
                            item.name = college.name;
                        });
                    });
            };
            $scope.showChart = function (items) {
                $("#collegeFlowChart").highcharts(kpiDisplayService.generateCollegeFlowBarOptions(items));
            };

            $scope.query();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('topic.dialog.kpi', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .run(function($rootScope, kpiPreciseService) {
        $rootScope.orderPolicy = {
            options: [],
            selected: ""
        };
        $rootScope.topCount = {
            options: [5, 10, 15, 20, 30],
            selected: 15
        };
        $rootScope.initializeOrderPolicy = function() {
            kpiPreciseService.getOrderSelection().then(function(result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[5];
            });
        };
        $rootScope.initializeDownSwitchOrderPolicy = function() {
            kpiPreciseService.getDownSwitchOrderSelection().then(function(result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[1];
            });
        };
        $rootScope.initializeCqiOrderPolicy = function () {
            kpiPreciseService.getCqiOrderSelection().then(function (result) {
                $rootScope.orderPolicy.options = result;
                $rootScope.orderPolicy.selected = result[1];
            });
        };

        $rootScope.closeAlert = function(messages, index) {
            messages.splice(index, 1);
        };

        $rootScope.overallStat = {
            currentDistrict: "",
            districtStats: [],
            townStats: [],
            cityStat: {},
            dateString: ""
        };
        var yesterday = new Date();
        yesterday.setDate(yesterday.getDate() - 1);
        $rootScope.statDate = {
            value: yesterday,
            opened: false
        };
    })
    .controller("rutrace.trend",
        function($scope,
            $uibModalInstance,
            mapDialogService,
            appKpiService,
            kpiPreciseService,
            appFormatService,
            workItemDialog,
            dialogTitle,
            city,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle +
                '-' +
                appFormatService.getDateString($scope.statDate.value, "yyyy年MM月dd日");
            $scope.kpiType = 'precise';
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.showKpi = function() {
                kpiPreciseService.getRecentPreciseRegionKpi(city.selected, $scope.statDate.value)
                    .then(function(result) {
                        $scope.statDate.value = appFormatService.getDate(result.statDate);
                        angular.forEach(result.districtViews,
                            function(view) {
                                view.objectRate = appKpiService.getPreciseObject(view.district);
                            });
                        $scope.overallStat.districtStats = result.districtViews;
                        $scope.overallStat.townStats = result.townViews;
                        $scope.overallStat.currentDistrict = result.districtViews[0].district;
                        $scope.overallStat.districtStats
                            .push(appKpiService.getCityStat($scope.overallStat.districtStats, city.selected));
                        $scope.overallStat.dateString = appFormatService
                            .getDateString($scope.statDate.value, "yyyy年MM月dd日");
                    });
            };
            $scope.showChart = function() {
                workItemDialog.showPreciseChart($scope.overallStat);
            };
            $scope.showWorkitemCity = function() {
                mapDialogService.showPreciseWorkItem($scope.endDate);
            };
            $scope.showTrend = function() {
                workItemDialog.showPreciseTrend(city, $scope.beginDate, $scope.endDate);
            };
            $scope.showTopKpi = function() {
                mapDialogService.showPreciseTop($scope.beginDate, $scope.endDate);
            };

            $scope.showKpi();

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('rrc.trend',
        function($scope,
            $uibModalInstance,
            kpiPreciseService,
            appFormatService,
            appKpiService,
            workItemDialog,
            dialogTitle,
            city,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle +
                '-' +
                appFormatService.getDateString($scope.statDate.value, "yyyy年MM月dd日");
            $scope.kpiType = 'rrc';

            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.showKpi = function() {
                kpiPreciseService.getRecentRrcRegionKpi(city.selected, $scope.statDate.value)
                    .then(function(result) {
                        $scope.statDate.value = appFormatService.getDate(result.statDate);
                        angular.forEach(result.districtViews,
                            function(view) {
                                view.objectRate = appKpiService.getRrcObject(view.district);
                            });
                        $scope.overallStat.districtStats = result.districtViews;
                        $scope.overallStat.townStats = result.townViews;
                        $scope.overallStat.currentDistrict = result.districtViews[0].district;
                        $scope.overallStat.districtStats
                            .push(appKpiService.getRrcCityStat($scope.overallStat.districtStats, city.selected));
                        $scope.overallStat.dateString = appFormatService
                            .getDateString($scope.statDate.value, "yyyy年MM月dd日");
                    });
            };
            $scope.showChart = function() {
                workItemDialog.showRrcChart($scope.overallStat);
            };
            $scope.showTrend = function() {
                workItemDialog.showRrcTrend(city, $scope.beginDate, $scope.endDate);
            };
            $scope.showKpi();
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("kpi.basic",
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            appRegionService,
            appFormatService,
            kpi2GService,
            workItemDialog) {
            $scope.dialogTitle = dialogTitle;
            $scope.views = {
                options: ['主要', '2G', '3G'],
                selected: '主要'
            };
            $scope.showKpi = function() {
                kpi2GService.queryDayStats(city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.statList = result.statViews;
                });
            };
            $scope.showTrend = function() {
                workItemDialog.showBasicTrend(city.selected, $scope.beginDate, $scope.endDate);
            };
            $scope.$watch('city.selected',
                function(item) {
                    if (item) {
                        $scope.showKpi();
                    }
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
angular.module('topic.dialog.top',
        ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', 'topic.dialog.kpi', "ui.bootstrap"])
    .controller("rutrace.top",
        function($scope,
            $uibModalInstance,
            dialogTitle,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = dialogTitle;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.kpiType = 'precise';

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopKpis(beginDate.value,
                    endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected).then(function(result) {
                    $scope.topCells = result;
                    angular.forEach(result,
                        function(cell) {
                            workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(items) {
                                if (items.length > 0) {
                                    for (var j = 0; j < $scope.topCells.length; j++) {
                                        if (items[0].eNodebId === $scope.topCells[j].cellId &&
                                            items[0].sectorId === $scope.topCells[j].sectorId) {
                                            $scope.topCells[j].hasWorkItems = true;
                                            break;
                                        }
                                    }
                                }
                            });
                        });
                });
            };
            $scope.initializeOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("rutrace.top.district",
        function($scope,
            $uibModalInstance,
            district,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = "TOP指标分析-" + district;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopKpisInDistrict($scope.beginDate.value,
                    $scope.endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected,
                    $scope.city.selected,
                    district).then(function(result) {
                    $scope.topCells = result;
                    angular.forEach(result,
                        function(cell) {
                            workitemService.queryByCellId(cell.cellId, cell.sectorId).then(function(items) {
                                if (items.length > 0) {
                                    for (var j = 0; j < $scope.topCells.length; j++) {
                                        if (items[0].eNodebId === $scope.topCells[j].cellId &&
                                            items[0].sectorId === $scope.topCells[j].sectorId) {
                                            $scope.topCells[j].hasWorkItems = true;
                                            break;
                                        }
                                    }
                                }
                            });
                        });
                });
            };
            $scope.initializeOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });

            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller("down.switch.top.district",
        function($scope,
            $uibModalInstance,
            district,
            preciseInterferenceService,
            kpiPreciseService,
            workitemService,
            beginDate,
            endDate) {
            $scope.dialogTitle = "TOP指标分析-" + district;
            $scope.topCells = [];
            $scope.updateMessages = [];
            $scope.beginDate = beginDate;
            $scope.endDate = endDate;
            $scope.kpiType = 'downSwitch';

            $scope.query = function() {
                $scope.topCells = [];
                kpiPreciseService.queryTopDownSwitchByPolicyInDistrict(beginDate.value,
                    endDate.value,
                    $scope.topCount.selected,
                    $scope.orderPolicy.selected,
                    $scope.city.selected,
                    district).then(function(result) {
                    $scope.topCells = result;
                });
            };
            $scope.initializeDownSwitchOrderPolicy();
            $scope.$watch('orderPolicy.selected',
                function(selection) {
                    if (selection) {
                        $scope.query();
                    }
                });


            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('kpi.topDrop2G',
        function($scope,
            $uibModalInstance,
            city,
            dialogTitle,
            appFormatService,
            drop2GService,
            connection3GService,
            workItemDialog) {
            $scope.dialogTitle = dialogTitle;
            $scope.topData = {
                drop2G: [],
                connection3G: []
            };
            $scope.showKpi = function() {
                drop2GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.topData.drop2G = result.statViews;
                });
                connection3GService.queryDayStats($scope.city.selected, $scope.statDate.value).then(function(result) {
                    $scope.statDate.value = appFormatService.getDate(result.statDate);
                    $scope.topData.connection3G = result.statViews;
                });
            };
            $scope.showDropTrend = function() {
                workItemDialog.showTopDropTrend(city.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.topCount);
            };
            $scope.showConnectionTrend = function() {
                workItemDialog.showTopConnectionTrend(city.selected,
                    $scope.beginDate,
                    $scope.endDate,
                    $scope.topCount);
            };
            $scope.$watch('city.selected',
                function(item) {
                    if (item) {
                        $scope.showKpi();
                    }
                });
            $scope.ok = function() {
                $uibModalInstance.close($scope.building);
            };

            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        });
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
angular.module('topic.dialog.alarm', ['myApp.url', 'myApp.region', 'myApp.kpi', 'topic.basic', "ui.bootstrap"])
    .controller('map.zero-voice.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateZeroVoiceGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.zero-flow.dialog',
        function($scope,
            $uibModalInstance,
            station,
            dialogTitle,
            appFormatService) {

            $scope.itemGroups = appFormatService.generateZeroFlowGroups(station);

            $scope.dialogTitle = dialogTitle;


            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };
        })
    .controller('map.resource.dialog',
        function($scope, $uibModalInstance, station, dialogTitle, downSwitchService) {
            $scope.station = station;
            $scope.dialogTitle = dialogTitle;
            $scope.cancel = function() {
                $uibModalInstance.dismiss('cancel');
            };

            $scope.tab = 1;

            $scope.selectTab = function(setTab) {
                $scope.tab = setTab;
                if (0 === setTab) {
                    downSwitchService.getResourceCounter(station.id).then(function(response) {
                        $scope.counter = response.result;
                    });
                } else if (1 === setTab) {
                    $scope.table = "bts";
                } else if (2 === setTab) {
                    $scope.table = "enodeb";
                } else if (3 === setTab) {
                    $scope.table = "rru";
                } else if (4 === setTab) {
                    $scope.table = "lrru";
                } else if (5 === setTab) {
                    $scope.table = "sfz";
                } else if (6 === setTab) {
                    $scope.table = "zfz";
                } else if (7 === setTab) {
                    $scope.table = "asset";
                }
                if (0 !== setTab) {
                    downSwitchService.getResource($scope.table, station.id).then(function(response) {
                        $scope.resourceList = response.result;
                    });
                }
            }

            $scope.isSelectTab = function(checkTab) {
                return $scope.tab === checkTab
            }
            $scope.selectTab(0);
        });
angular.module('topic.dialog',[ 'app.menu', 'app.core' ])
    .factory('mapDialogService',
        function (menuItemService, stationFormatService) {
            return {
                showTownENodebInfo: function(item, city, district) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/TownENodebInfo.html',
                        controller: 'town.eNodeb.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return city + district + item.town + "-" + "基站基本信息";
                            },
                            city: function() {
                                return city;
                            },
                            district: function() {
                                return district;
                            },
                            town: function() {
                                return item.town;
                            }
                        }
                    });
                },
                arrangeTownENodebInfo: function (city, district, town) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/ArrangeInfo.html',
                        controller: 'arrange.eNodeb.dialog',
                        resolve: {
                            dialogTitle: function () {
                                return city + district + town + "-" + "LTE基站镇区调整";
                            },
                            city: function () {
                                return city;
                            },
                            district: function () {
                                return district;
                            },
                            town: function () {
                                return town;
                            }
                        }
                    });
                },
                arrangeTownBtsInfo: function (city, district, town) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Region/ArrangeInfo.html',
                        controller: 'arrange.bts.dialog',
                        resolve: {
                            dialogTitle: function () {
                                return city + district + town + "-" + "CDMA基站镇区调整";
                            },
                            city: function () {
                                return city;
                            },
                            district: function () {
                                return district;
                            },
                            town: function () {
                                return town;
                            }
                        }
                    });
                },
                showHotSpotsInfo: function(hotSpotList) {
                    menuItemService.showGeneralDialogWithAction({
                        templateUrl: '/appViews/Parameters/Map/HotSpotInfoBox.html',
                        controller: 'hot.spot.info.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "热点信息列表";
                            },
                            hotSpotList: function() {
                                return hotSpotList;
                            }
                        }
                    });
                },
                showCellsInfo: function(sectors) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Parameters/Map/CellsMapInfoBox.html',
                        controller: 'map.sectors.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "小区信息列表";
                            },
                            sectors: function() {
                                return sectors;
                            }
                        }
                    });
                },
                showPlanningSitesInfo: function(site) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Home/PlanningDetails.html',
                        controller: 'map.site.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "规划站点信息:" + site.formalName;
                            },
                            site: function() {
                                return site;
                            }
                        }
                    });
                },
                showOnlineSustainInfos: function(items) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Complain/Online.html',
                        controller: 'online.sustain.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "在线支撑基本信息";
                            },
                            items: function() {
                                return items;
                            }
                        }
                    });
                },
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
                showCheckingStationInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.checkingStation.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "巡检信息:" + station.name;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showFixingStationInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.fixingStation.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "整治信息:" + station.name;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showCommonStationList: function(type) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/CommonStationListDialog.html',
                        controller: 'map.common-stationList.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "公共列表";
                            },
                            type: function() {
                                return type;
                            }
                        }
                    });
                },
                showSpecialStationInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.special-station.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点信息:" + station.enodebName;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showSpecialIndoorInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.special-indoor.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点信息:" + station.enodebName;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showFaultStationInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.fault-station.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点信息:" + station.enodebName;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showZeroFlowInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.zero-flow.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点信息:" + station.enodebName;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showZeroVoiceInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/SpecialStationDetails.html',
                        controller: 'map.zero-voice.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "站点信息:" + station.BTSName;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showResourceInfo: function(station) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Import/ResoureDetails.html',
                        controller: 'map.resource.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return "资源资产:" + station.name;
                            },
                            station: function() {
                                return station;
                            }
                        }
                    });
                },
                showBuildingInfo: function(building) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Evaluation/Dialog/BuildingInfoBox.html',
                        controller: 'map.building.dialog',
                        resolve: {
                            dialogTitle: function() {
                                return building.name + "楼宇信息";
                            },
                            building: function() {
                                return building;
                            }
                        }
                    });
                },
                showBasicKpiDialog: function (city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/Index.html',
                        controller: 'kpi.basic',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return city.selected + "CDMA整体分析";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showTopDrop2GDialog: function(city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/BasicKpi/TopDrop2G.html',
                        controller: 'kpi.topDrop2G',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return city.selected + "TOP掉话分析";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showRrcTrend: function(city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                            templateUrl: '/appViews/Rutrace/Trend.html',
                            controller: 'rrc.trend',
                            resolve: stationFormatService.dateSpanDateResolve({
                                    dialogTitle: {
                                        function() {
                                            return "RRC连接成功率变化趋势";
                                        },
                                        city: function() {
                                            return city;
                                        }
                                    }
                                },
                                beginDate,
                                endDate)
                    });
                },
                showCityCqiTrend: function (city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Trend.html',
                        controller: 'cqi.trend',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "CQI优良比变化趋势";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showDownSwitchTrendDialog: function (city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Trend.html',
                        controller: 'down.switch.trend',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "4G下切3G变化趋势";
                                },
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showPreciseWorkItem: function(endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/WorkItem/ForCity.html',
                        controller: 'workitem.city',
                        resolve: {
                            endDate: function() {
                                return endDate;
                            }
                        }
                    });
                },
                showPreciseWorkItemDistrict: function(district, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/WorkItem/ForCity.html',
                        controller: 'workitem.district',
                        resolve: {
                            district: function() {
                                return district;
                            },
                            endDate: function() {
                                return endDate;
                            }
                        }
                    });
                },
                showPreciseTop: function(beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Top.html',
                        controller: 'rutrace.top',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "全市精确覆盖率TOP统计";
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showDownSwitchTop: function (beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Top.html',
                        controller: 'down.switch.top',
                        resolve: stationFormatService.dateSpanDateResolve({
                                dialogTitle: function() {
                                    return "全市4G下切3GTOP统计";
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showPreciseTopDistrict: function(beginDate, endDate, district) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Top.html',
                        controller: 'rutrace.top.district',
                        resolve: stationFormatService.dateSpanDateResolve({
                                district: function() {
                                    return district;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showDownSwitchTopDistrict: function (beginDate, endDate, district) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Rutrace/Top.html',
                        controller: 'down.switch.top.district',
                        resolve: stationFormatService.dateSpanDateResolve({
                                district: function() {
                                    return district;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                showMonthComplainItems: function() {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Index.html',
                        controller: 'customer.index',
                        resolve: {}
                    });
                },
                showYesterdayComplainItems: function(city) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Dialog/Yesterday.html',
                        controller: 'customer.yesterday',
                        resolve: {
                            city: function() {
                                return city;
                            }
                        }
                    });
                },
                showRecentComplainItems: function (city, beginDate, endDate) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Dialog/Recent.html',
                        controller: 'customer.recent',
                        resolve: stationFormatService.dateSpanDateResolve({
                                city: function() {
                                    return city;
                                }
                            },
                            beginDate,
                            endDate)
                    });
                },
                adjustComplainItems: function () {
                    menuItemService.showGeneralDialog({
                        templateUrl: "/appViews/Customer/Complain/Adjust.html",
                        controller: 'complain.adjust',
                        resolve: {}
                    });
                },
                showComplainDetails: function(item) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/Customer/Complain/Details.html',
                        controller: 'complain.details',
                        resolve: {
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
                showCollegeFlowTrend: function(beginDate, endDate, name) {
                    menuItemService.showGeneralDialog({
                        templateUrl: '/appViews/College/Test/CollegeFlow.html',
                        controller: 'college.flow.name',
                        resolve: stationFormatService.dateSpanDateResolve({
                                name: function() {
                                    return name;
                                }
                            },
                            beginDate,
                            endDate)
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
    'topic.parameters.basic', 'topic.parameters.coverage', 'topic.parameters.station', "topic.parameters",
    'topic.dialog', 'topic.dialog.college', 'topic.dialog.kpi', 'topic.dialog.top',
    'topic.dialog.customer', 'topic.dialog.parameters', 'topic.dialog.station', 'topic.dialog.alarm'
]);