angular.module('home.route', ['app.common'])
    .config(function($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
        var viewDir = "/appViews/Home/";
        $stateProvider
            .state('list',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Network.html",
                            controller: "home.network"
                        }
                    },
                    url: "/cells"
                })
            .state('query',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Query.html",
                            controller: "home.query"
                        }
                    },
                    url: "/query"
                })
            .state('building',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Query.html",
                            controller: "evaluation.home"
                        }
                    },
                    url: "/building"
                })
            .state('common-station',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/Home/StationSearchMenu.html",
                            controller: "menu.common-station"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Common.html",
                            controller: "common-station.network"
                        }
                    },
                    url: "/common-station"
                })
            .state('common-indoor',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/Home/StationSearchMenu.html",
                            controller: "menu.common-indoor"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Common.html",
                            controller: "common-indoor.network"
                        }
                    },
                    url: "/common-indoor"
                })
            .state('plan',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.plan"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Plan.html",
                            controller: "home.plan"
                        }
                    },
                    url: "/plan"
                })
            .state('cdma',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.cdma"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Cdma.html",
                            controller: "home.cdma"
                        }
                    },
                    url: "/cdma"
                })
            .state('interference',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.cdma"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Menu/Plan.html",
                            controller: "home.interference"
                        }
                    },
                    url: "/interference"
                })
            .state('micro',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.complain"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Micro.html",
                            controller: "complain.micro"
                        }
                    },
                    url: "/micro"
                })
            .state('college',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.college"
                        },
                        "contents": {
                            templateUrl: "/appViews/College/Root.html",
                            controller: "home.college"
                        }
                    },
                    url: "/college"
                })
            .state('college-coverage',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.college"
                        },
                        "contents": {
                            templateUrl: "/appViews/College/Coverage/Index.html",
                            controller: "college.coverage"
                        }
                    },
                    url: "/college-coverage"
                })
            .state('mr',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Mr.html",
                            controller: "home.mr"
                        }
                    },
                    url: "/mr"
                })
            .state('grid',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: viewDir + "MrGrid.html",
                            controller: "mr.grid"
                        }
                    },
                    url: "/grid"
                })
            .state('app',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.mr"
                        },
                        "contents": {
                            templateUrl: "/appViews/BasicKpi/GridApp.html",
                            controller: "mr.app"
                        }
                    },
                    url: "/app"
                })
            .state('analysis',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: viewDir + "Analysis.html",
                            controller: "network.analysis"
                        }
                    },
                    url: "/analysis"
                })
            .state('highway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Highway.html',
                            controller: "analysis.highway"
                        }
                    },
                    url: "/highway"
                })
            .state('railway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Highway.html',
                            controller: "analysis.railway"
                        }
                    },
                    url: "/railway"
                })
            .state('subway',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/Subway.html',
                            controller: "analysis.subway"
                        }
                    },
                    url: "/subway"
                })
            .state('highvalue',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.analysis"
                        },
                        "contents": {
                            templateUrl: '/appViews/Evaluation/HighValue.html',
                            controller: "analysis.highvalue"
                        }
                    },
                    url: "/highvalue"
                });
        $urlRouterProvider.otherwise('/cells');
    });