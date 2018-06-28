module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            "foo": {
                "files": {
                    'app/url.js': [
                        'url/core.js', 'url/menu.js', 'url/format.js', 'url/chart.js', 'url/kpi.chart.js',
                        'url/geometry.js', 'url/calculation.js',
                        'url/app.url.js'
                    ],
                    'app/region.js': [
                        'region/basic.js', 'region/mongo.js', 'region/kpi.js', 'region/import.js',
                        'region/authorize.js', 'region/college.js', 'region/precise.js',
                        'region/network.js', 'region/app.region.js'
                    ],
                    'app/kpi.js': [
                        'kpi/core.js', 'kpi/college.infrastructure.js', 'kpi/college.basic.js', 'kpi/college.maintain.js',
                        'kpi/college.work.js', 'kpi/college.flow.js', 'kpi/college.js', 
                        'kpi/coverage.interference.js', 'kpi/coverage.mr.js', 'kpi/coverage.stats.js', 'kpi/coverage.flow.js',
                        'kpi/coverage.js', 'kpi/customer.js', 'kpi/customer.complain.js', 'kpi/customer.sustain.js',
                        'kpi/parameter.dump.js', 'kpi/parameter.rutrace.js', 'kpi/parameter.query.js', 'kpi/parameter.js',
                        'kpi/work.dialog.js', 'kpi/work.flow.js', 'kpi/work.chart.js', 'kpi/work.trend.js',
                        'kpi/work.js', 'kpi/app.kpi.js'
                    ],
                    'app/topic.js': [
                        'topic/basic.js', 'topic/college.js',
                        'topic/parameters.basic.js', 'topic/parameters.coverage.js', 'topic/parameters.station.js', 'topic/parameters.js',
                        'topic/dialog.customer.js', 'topic/dialog.parameters.js', "topic/dialog.college.js",
                        'topic/dialog.kpi.js', 'topic/dialog.top.js',
                        'topic/dialog.station.js', 'topic/dialog.alarm.js', 'topic/dialog.js', 'topic/baidu.map.js'
                    ],
                    'app/filters.js': [
                        'filters/basic.js', 'filters/cell.js', 'filters/handoff.js', 'filters/combined.js'
                    ]
                }
            }
        }
    });
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.registerTask('default', ['concat']);
};
