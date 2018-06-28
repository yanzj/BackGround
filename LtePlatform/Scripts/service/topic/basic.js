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