angular.module('app.geometry', [])
    .factory('geometryCalculateService', function() {
        var getDistanceFunc = function(p1Lat, p1Lng, p2Lat, p2Lng) {
            var earthRadiusKm = 6378.137;
            var dLat1InRad = p1Lat * (Math.PI / 180);
            var dLong1InRad = p1Lng * (Math.PI / 180);
            var dLat2InRad = p2Lat * (Math.PI / 180);
            var dLong2InRad = p2Lng * (Math.PI / 180);
            var dLongitude = dLong2InRad - dLong1InRad;
            var dLatitude = dLat2InRad - dLat1InRad;
            var a = Math.pow(Math.sin(dLatitude / 2), 2) + Math.cos(dLat1InRad) * Math.cos(dLat2InRad) * Math.pow(Math.sin(dLongitude / 2), 2);
            var c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
            var dDistance = earthRadiusKm * c;
            return dDistance;
        };
        var getLonLatFunc = function(centre, x, y) {
            var lat = centre.lat + y / getDistanceFunc(centre.lat, centre.lng, centre.lat + 1, centre.lng);
            var lng = centre.lng + x / getDistanceFunc(centre.lat, centre.lng, centre.lat, centre.lng + 1);
            return new BMap.Point(lng, lat);
        };
        return {
            getDistanceFunc: function(p1Lat, p1Lng, p2Lat, p2Lng) {
                return getDistanceFunc(p1Lat, p1Lng, p2Lat, p2Lng);
            },
            getLonLatFunc: function(centre, x, y) {
                return getLonLatFunc(centre, x, y);
            },
            getPositionFunc: function(centre, r, angle) {
                var x = r * Math.cos(angle * Math.PI / 180);
                var y = r * Math.sin(angle * Math.PI / 180);
                return getLonLatFunc(centre, x, y);
            },
            getPositionRadius: function(centre, r, rad) {
                var x = r * Math.cos(rad);
                var y = r * Math.sin(rad);
                return getLonLatFunc(centre, x, y);
            },
            getPolygonCenter: function (coors) {
                var centerx = 0;
                var centery = 0;
                for (var p = 0; p < coors.length / 2; p++) {
                    centerx += parseFloat(coors[2 * p]);
                    centery += parseFloat(coors[2 * p + 1]);
                }
                centerx /= coors.length / 2;
                centery /= coors.length / 2;
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getRectangleCenter: function (coors) {
                var centerx = (parseFloat(coors[0]) + parseFloat(coors[2])) / 2;
                var centery = (parseFloat(coors[1]) + parseFloat(coors[3])) / 2;
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getCircleCenter: function (coors) {
                var centerx = parseFloat(coors[0]);
                var centery = parseFloat(coors[1]);
                return {
                    X: centerx,
                    Y: centery
                };
            },
            getRadiusFunc: function(zoom) {
                var rSation = 70;
                var rSector = 0.2;
                switch (zoom) {
                case 15:
                    rSector *= 0.75;
                    rSation *= 0.75;
                    break;
                case 16:
                    rSector /= 2.5;
                    rSation /= 2.5;
                    break;
                case 17:
                    rSector /= 5;
                    rSation /= 5;
                    break;
                default:
                    break;
                }

                return { rSector: rSector, rStation: rSation };
            }

        };
    })
    .factory('geometryService', function(geometryCalculateService) {
        return {
            getDistance: function(p1Lat, p1Lng, p2Lat, p2Lng) {
                return geometryCalculateService.getDistanceFunc(p1Lat, p1Lng, p2Lat, p2Lng);
            },
            getLonLat: function(centre, x, y) {
                return geometryCalculateService.getLonLatFunc(centre, x, y);
            },
            getPosition: function(centre, r, angle) {
                return geometryCalculateService.getPositionFunc(centre, r, angle);
            },
            getPositionLonLat: function(centre, r, angle) {
                var x = r * Math.cos(angle * Math.PI / 180);
                var y = r * Math.sin(angle * Math.PI / 180);
                var lat = centre.lattitute + y / geometryCalculateService.getDistanceFunc(centre.lattitute, centre.longtitute, centre.lattitute + 1, centre.longtitute);
                var lng = centre.longtitute + x / geometryCalculateService.getDistanceFunc(centre.lattitute, centre.longtitute, centre.lattitute, centre.longtitute + 1);
                return {
                    longtitute: lng,
                    lattitute: lat
                };
            },
            generateSectorPolygonPoints: function(centre, irotation, iangle, zoom, scalor) {
                var assemble = [];
                var dot;
                var i;
                var r = geometryCalculateService.getRadiusFunc(zoom).rSector * (scalor || 1);

                for (i = 0; i <= r; i += r / 2) {
                    dot = geometryCalculateService.getPositionFunc(centre, i, irotation);
                    assemble.push(dot);
                }

                for (i = 0; i <= iangle; i += iangle / 5) {
                    dot = geometryCalculateService.getPositionFunc(centre, r, i + irotation);
                    assemble.push(dot);
                }

                return assemble;
            },
            getRadius: function(zoom) {
                return geometryCalculateService.getRadiusFunc(zoom);
            },
            getDtPointRadius: function(zoom) {
                var radius = 17;
                switch (zoom) {
                case 15:
                    radius *= 0.9;
                    break;
                case 16:
                    radius *= 0.8;
                    break;
                case 17:
                    radius *= 0.75;
                    break;
                default:
                    break;
                }
                return radius;
            },
            getArrowLine: function(x1, y1, x2, y2, r) {
                var rad = Math.atan2(y2 - y1, x2 - x1);
                var centre = {
                    lng: x2,
                    lat: y2
                };
                var point1 = geometryCalculateService.getPositionRadius(centre, -r, rad - 0.2);
                var point2 = geometryCalculateService.getPositionRadius(centre, -r, rad + 0.2);
                return new BMap.Polyline([
                    new BMap.Point(x2, y2),
                    point1,
                    point2,
                    new BMap.Point(x2, y2),
                    new BMap.Point(x1, y1)
                ], { strokeColor: "blue", strokeWeight: 1 });
            },
            getLine: function(x1, y1, x2, y2, color) {
                color = color || "orange";
                return new BMap.Polyline([
                    new BMap.Point(x2, y2),
                    new BMap.Point(x1, y1)
                ], { strokeColor: color, strokeWeight: 1 });
            },
            getCircle: function(x, y, r, color, callback, neighbor) {
                color = color || "orange";
                var circle = new BMap.Circle(new BMap.Point(x, y), r, { strokeColor: color, fillColor: color });
                circle.addEventListener("click", function() {
                    callback(neighbor);
                });
                return circle;
            },
            getPathInfo: function(path) {
                var result = '';
                angular.forEach(path, function(point, $index) {
                    if ($index > 0) result += ';';
                    result += point.lng + ';' + point.lat;
                });
                return result;
            },
            queryRegionCenter: function (region) {
                switch (region.regionType) {
                    case 2:
                        return geometryCalculateService.getPolygonCenter(region.info.split(';'));
                    case 1:
                        return geometryCalculateService.getRectangleCenter(region.info.split(';'));
                    default:
                        return geometryCalculateService.getCircleCenter(region.info.split(';'));
                }
            },
            queryMapColors: function() {
                return [
                    '#CC3333',
                    '#3366D0',
                    '#CC9966',
                    '#003300',
                    '#99CC00',
                    '#FF9966',
                    '#CC6699',
                    '#9933FF',
                    '#333300'
                ];
            },
            queryConstructionIcon: function(status) {
                var urlDictionary = {
                    '审计会审': "/Content/Images/BtsIcons/alarm_lightyellow.png",
                    '天馈施工': "/Content/Images/BtsIcons/alarm_yellow.png",
                    '整体完工': "/Content/Images/BtsIcons/alarm_lightblue.png",
                    '基站开通': "/Content/Images/BtsIcons/alarm_green.png",
                    '其他': "/Content/Images/BtsIcons/alarm_red.png"
                }
                var url = urlDictionary[status];
                if (!url) url = "/Content/Images/BtsIcons/alarm_purple.png";
                return new BMap.Icon(url, new BMap.Size(22, 28));
            },
            constructionStateOptions: ['全部', '审计会审', '天馈施工', '整体完工', '基站开通', '其他']
        };
    });