
function ComplexCustomOverlay(point, text, mouseoverText, lines) {
    this._point = point;
    this._text = text;
    this._overText = mouseoverText;
    this._lines = lines;
}
ComplexCustomOverlay.prototype = new BMap.Overlay();
ComplexCustomOverlay.prototype.initialize = function (map) {
    this._map = map;
    var div = this._div = document.createElement("div");
    div.className = 'baidu-label-body baidu-label-red';
    div.style.zIndex = BMap.Overlay.getZIndex(this._point.lat);
    var span = this._span = document.createElement("span");
    div.appendChild(span);
    span.appendChild(document.createTextNode(this._text));
    var that = this;

    var arrow = this._arrow = document.createElement("div");
    arrow.className = 'baidu-label-arrow';
    div.appendChild(arrow);

    div.onmouseover = function () {
        div.className = 'baidu-label-body baidu-label-blue';
        this.style.height = 18*that._lines + "px";
        this.style.top = that._defaultTop - 18*(that._lines-1) + "px";
        this.getElementsByTagName("span")[0].innerHTML = that._overText;
        arrow.style.backgroundPosition = "0px -20px";
        arrow.style.top = 18 * that._lines - 1 + "px";
    }

    div.onmouseout = function () {
        div.className = 'baidu-label-body baidu-label-red';
        this.style.height = "18px";
        this.style.top = that._defaultTop + "px";
        this.getElementsByTagName("span")[0].innerHTML = that._text;
        arrow.style.backgroundPosition = "0px 0px";
        arrow.style.top = "17px";
    }

    map.getPanes().labelPane.appendChild(div);

    return div;
}
ComplexCustomOverlay.prototype.draw = function () {
    var map = this._map;
    var pixel = map.pointToOverlayPixel(this._point);
    this._div.style.left = pixel.x - 10 + "px";
    this._div.style.top = pixel.y - 26 + "px";
    this._defaultTop = pixel.y - 26;
}