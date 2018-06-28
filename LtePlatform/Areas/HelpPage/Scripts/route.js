angular.module('myApp', ['app.common'])
    .config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix('');
            var viewDir = "/appViews/Test/Help/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "ApiGroup.html",
                    controller: "api.group"
                })
                .when('/method/:name', {
                    templateUrl: viewDir + "ApiMethod.html",
                    controller: "api.method"
                })
                .when('/api/:apiId/:method', {
                    templateUrl: viewDir + "Api.html",
                    controller: "api.details"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function($rootScope) {
        var rootUrl = "/Help#";
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "",
            introduction: ""
        };
    })
    .controller("api.details", function ($scope, generalHttpService, $routeParams) {
        $scope.page.title = $routeParams.apiId;
        $scope.page.introduction = "Provide the details of this API method.";
        $scope.method = $routeParams.method;
        generalHttpService.getMvcData('/Help/ApiActionDoc', {
            apiId: $routeParams.apiId
        }).then(function(result) {
            $scope.parameters = result.ParameterDescriptions;
            $scope.bodyModel = result.FromBodyModel;
            $scope.responseModel = result.ResponseModel;
        });
    })
    .controller("api.group", function ($scope, generalHttpService) {
        $scope.page.title = "Introduction";
        $scope.page.introduction = "Provide a general description of your APIs here.";
        generalHttpService.getMvcData('/Help/ApiDescriptions', {}).then(function(result) {
            $scope.apiDescription = result;
        });
    })
    .controller("api.method", function ($scope, generalHttpService, $routeParams) {
        $scope.page.title = $routeParams.name;
        $scope.page.introduction = "Provide the description of this API controller here.";
        generalHttpService.getMvcData('/Help/ApiMethod', {
            controllerName: $routeParams.name
        }).then(function(result) {
            $scope.methods = result.ActionList;
            $scope.page.introduction = $routeParams.name + '(' + result.FullPath + '): ' + result.Documentation;
        });
    });