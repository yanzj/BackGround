module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            "foo": {
                "files": {
                    'app/url.js': [
                        'url/core.js', 'url/menu.js', 'url/format.js', 'url/chart.js',
                        'url/geometry.js', 'url/calculation.js',
                        'url/app.url.js'
                    ],
                    'app/region.js': [
                        'region/basic.js', 'region/mongo.js', 'region/kpi.js', 'region/import.js',
                        'region/authorize.js', 'region/college.js', 'region/precise.js',
                        'region/network.js', 'region/app.region.js'
                    ],
                    'app/kpi.js': [
                        'kpi/college.infrastructure.js', 'kpi/college.basic.js', 'kpi/college.maintain.js',
                        'kpi/college.js', 
                        'kpi/customer.js', 'kpi/customer.complain.js', 'kpi/customer.sustain.js',
                        'kpi/parameter.dump.js', 'kpi/work.dialog.js', 'kpi/work.js', 'kpi/app.kpi.js'
                    ],
                    'app/topic.js': [
                        'topic/basic.js', 'topic/college.js',
                        'topic/parameters.basic.js', 'topic/parameters.js',
                        'topic/dialog.customer.js', "topic/dialog.college.js",
                        'topic/dialog.js', 'topic/baidu.map.js'
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
