function GeneralChart() {
    this.title = {
        text: ''
    };
    this.series = [];

    this.legend = {
        layout: 'vertical',
        align: 'left',
        x: 100,
        verticalAlign: 'top',
        y: 30,
        floating: true,
        backgroundColor: (Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF',
        enabled: true
    };
    this.tooltip = {
        headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
        pointFormat: '<b>{point.y:.2f}</b>'
    };
    this.options = {
        title: this.title,
        tooltip: this.tooltip,
        legend: this.legend,
        series: this.series
    };
    this.putSeries = function(series, type) {
        series.type = type;
        if (this.series.length === 0) {
            this.series.push(series);
        } else {
            this.series[0] = series;
        }
        this.options.series = this.series;
    };
    this.defaultYAxis = {
        labels: {
            format: '{value}',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        },
        title: {
            text: 'YLabel',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        }
    };
    this.defaultXAxis = {
        categories: [],
        title: {
            text: 'xLabel',
            style: {
                color: Highcharts.getOptions().colors[0]
            }
        }
    };
    this.setDefaultYAxis = function(settings) {
        if (settings.title) {
            this.defaultYAxis.title.text = settings.title;
        }
        if (settings.min) {
            this.defaultYAxis.min = settings.min;
        }
        if (settings.max) {
            this.defaultYAxis.max = settings.max;
        }
    };
    this.setDefaultXAxis = function(settings) {
        if (settings.title) {
            this.defaultXAxis.title.text = settings.title;
        }
        if (settings.categories) {
            this.defaultXAxis.categories = settings.categories;
        }
    };
    this.addSeries = function(series, type) {
        series.type = type;
        this.series.push(series);
        this.options.series = this.series;
    };
    return this;
};
 
function ComboChart() {
    this.options.series = this.series = [];
    this.options.xAxis = this.xAxis = [this.defaultXAxis];
    this.options.yAxis = this.yAxis = [this.defaultYAxis];
    this.options.chart = {
        zoomType: 'xy'
    };
}

ComboChart.prototype = new GeneralChart();
angular.extend(ComboChart.prototype.options, GeneralChart.prototype.options, {
    xAxis: []
});
angular.extend(ComboChart.prototype.options, GeneralChart.prototype.options, {
    yAxis: []
});
ComboChart.prototype.pushOneYAxis = function (yLabel) {
    this.yAxis.push({
        labels: {
            format: '{value}'
        },
        title: {
            text: yLabel
        },
        opposite: true
    });
};
ComboChart.prototype.initialize=function(settings) {
    this.title.text = settings.title;

    this.yAxis[0].title.text = settings.yTitle;
    this.xAxis[0].title.text = settings.xTitle;
}

var SingleAxisChart = function () {
    GeneralChart.call(this);
    this.options.series = this.series = [];
    this.options.xAxis = this.xAxis = this.defaultXAxis;
    this.options.yAxis = this.yAxis = this.defaultYAxis;
};

var GradientPie = function () {
    GeneralChart.call(this);
    this.tooltip = {
        pointFormat: '{series.name}: <b>{point.y: .1f}, 占比{point.percentage:.1f}%</b>'
    };
    this.putSeries({
        name: 'Brands',
        data: []
    });
    this.options.chart = this.chart = {
        plotBackgroundColor: null,
        plotBorderWidth: null,
        plotShadow: false,
        type: 'pie'
    };
    this.options.plotOptions = this.plotOptions = {
        pie: {
            allowPointSelect: true,
            cursor: 'pointer',
            dataLabels: {
                enabled: true,
                format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                style: {
                    color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || 'black'
                },
                connectorColor: 'silver'
            }
        }
    };
};

function BarChart() {
    SingleAxisChart.call(this);
    this.options.chart = this.chart = {
        type: 'bar'
    };
    this.options.plotOptions = this.plotOptions = {
        bar: {
            dataLabels: {
                enabled: true,
                align: "center",
                color: 'red',
                formatter: function() {
                    return parseInt(this.y * 100) / 100;
                }
            }
        }
    };
    this.options.credits = this.credits = {
        enabled: false
    };
    this.asignSeries = function(series) {
        this.putSeries(series, 'bar');
    };
};

function StackColumnChart() {
    SingleAxisChart.call(this);
    this.options.chart = this.chart = {
        type: 'column'
    };
    this.options.plotOptions = this.plotOptions = {
        column: {
            stacking: 'normal',
            dataLabels: {
                enabled: true,
                color: (Highcharts.theme && Highcharts.theme.dataLabelsColor) || 'white',
                style: {
                    textShadow: '0 0 3px black'
                }
            }
        }
    };
    angular.extend(this.yAxis, {
        stackLabels: {
            enabled: true,
            style: {
                fontWeight: 'bold',
                color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'
            }
        }
    });
}

function Column3d() {
    SingleAxisChart.call(this);
    this.options.chart = this.chart = {
        type: 'column',
        options3d: {
            enabled: true,
            alpha: 10,
            beta: 25,
            depth: 70
        }
    };
    this.options.legend.enabled = false;
    this.options.plotOptions = this.plotOptions = {
        column: {
            depth: 25
        }
    };
}

var TimeSeriesLine = function () {
    this.xAxis.type = 'datetime';
};

TimeSeriesLine.prototype = new SingleAxisChart();
TimeSeriesLine.prototype.plotOptions = {
    area: {
        marker: {
            radius: 2
        },
        lineWidth: 1,
        states: {
            hover: {
                lineWidth: 1
            }
        },
        threshold: null
    }
};
angular.extend(TimeSeriesLine.prototype.options, SingleAxisChart.prototype.options, {
    plotOptions: TimeSeriesLine.prototype.plotOptions
});
TimeSeriesLine.prototype.insertSeries = function(series) {
    this.addSeries(series, 'area');
};

function GaugeMeter() {
    var self = {};
    self.chart = {
        type: 'gauge',
        plotBackgroundColor: null,
        plotBackgroundImage: null,
        plotBorderWidth: 0,
        plotShadow: false
    };
    self.title = {
        text: 'Speedometer'
    };
    self.pane = {
        startAngle: -150,
        endAngle: 150,
        background: [{
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#FFF'],
                    [1, '#333']
                ]
            },
            borderWidth: 0,
            outerRadius: '109%'
        }, {
            backgroundColor: {
                linearGradient: { x1: 0, y1: 0, x2: 0, y2: 1 },
                stops: [
                    [0, '#333'],
                    [1, '#FFF']
                ]
            },
            borderWidth: 1,
            outerRadius: '107%'
        }, {
            // default background
        }, {
            backgroundColor: '#DDD',
            borderWidth: 0,
            outerRadius: '105%',
            innerRadius: '103%'
        }]
    };
    self.yAxis = {
        min: 0,
        max: 200,

        minorTickInterval: 'auto',
        minorTickWidth: 1,
        minorTickLength: 10,
        minorTickPosition: 'inside',
        minorTickColor: '#666',

        tickPixelInterval: 30,
        tickWidth: 2,
        tickPosition: 'inside',
        tickLength: 10,
        tickColor: '#666',
        labels: {
            step: 2,
            rotation: 'auto'
        },
        title: {
            text: 'km/h'
        },
        plotBands: [{
            from: 0,
            to: 120,
            color: '#DF5353' // red
        }, {
            from: 120,
            to: 160,
            color: '#DDDF0D' // yellow
        }, {
            from: 160,
            to: 200,
            color: '#55BF3B' // green
        }]
    };
    self.series = [{
        name: 'Speed',
        data: [80]
    }];
    self.options = {
        chart: self.chart,
        title: self.title,
        pane: self.pane,
        yAxis: self.yAxis,
        series: self.series
    };
    return self;
}
