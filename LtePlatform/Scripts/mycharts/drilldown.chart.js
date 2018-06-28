function DrilldownChart() {
    var self = this;

    self.title = {
        text: 'Drill-down pie chart'
    };
    self.series = [
    {
        name: "Drill-down pie chart",
        colorByPoint: true,
        data: []
    }];
    self.drilldown = {
        series: []
    };

    self.options = {
        chart: {
            type: 'column'
        },
        title: self.title,
        subtitle: {
            text: 'Click the slices to view versions.'
        },
        plotOptions: {
            series: {
                dataLabels: {
                    enabled: true,
                    format: '{point.name}: {point.y:.2f}'
                }
            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y:.2f}</b><br/>'
        },

        series: self.series,

        drilldown: self.drilldown,

        yAxis: {}
    };
}

DrilldownChart.prototype.addOneSeries = function (series) {
    var self = this;
    self.series[0].data.push({
        name: series.name,
        y: series.value,
        drilldown: series.name
    });
    self.drilldown.series.push({
        name: series.name,
        id: series.name,
        data: series.subData
    });
};

DrilldownChart.prototype.initialize = function(options) {
    this.title.text = options.title;
    this.series[0].data = [];
    this.drilldown.series = [];
    this.series[0].name = options.seriesName;
    if (options.yMin) {
        this.options.yAxis = {
            min: options.yMin,
            max: options.yMax
        };
    }
};

var DrilldownColumn = function () {

};

DrilldownColumn.prototype = new DrilldownChart();

DrilldownColumn.prototype.options.chart = {
    type: 'column'
};

DrilldownColumn.prototype.options.plotOptions.series.dataLabels.format = '{point.y:.2f}';

DrilldownColumn.prototype.options.xAxis = {
    type: 'category'
};

var DrilldownPie = function() {

};

DrilldownPie.prototype = new DrilldownChart();

DrilldownPie.prototype.options.chart = {
    type: 'pie'
};