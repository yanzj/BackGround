﻿module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
        concat: {
            "foo": {
                "files": {
                    'home.viewmodel.js': [
                        'home.root.js', 'home.route.js', 'home.menu.js',
                        'home.network.js', 'home.complain.js', 'home.combined.js'
                    ]
                }
            }
        }
    });
    grunt.loadNpmTasks('grunt-contrib-concat');
    grunt.registerTask('default', ['concat']);
};
