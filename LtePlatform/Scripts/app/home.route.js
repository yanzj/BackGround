angular.module('home.route', ['app.common'])
    .config(function($stateProvider, $urlRouterProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
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
            .state('micro',
                {
                    views: {
                        'menu': {
                            templateUrl: "/appViews/DropDownMenu.html",
                            controller: "menu.root"
                        },
                        "contents": {
                            templateUrl: "/appViews/Home/Micro.html",
                            controller: "complain.micro"
                        }
                    },
                    url: "/micro"
                });
        $urlRouterProvider.otherwise('/cells');
    });