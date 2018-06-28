angular.module('myApp', ['app.common'])
.config([
    '$routeProvider', '$locationProvider',
    function ($routeProvider, $locationProvider) {
        $locationProvider.hashPrefix('');
            $routeProvider
                .when('/', {
                    templateUrl: '/appViews/Manage/CurrentUser.html',
                    controller: "manage.current"
                })
                .when('/all', {
                    templateUrl: '/appViews/Manage/AllUsers.html',
                    controller: 'manage.all'
                })
                .when('/roles', {
                    templateUrl: '/appViews/Manage/Roles.html',
                    controller: 'manage.roles'
                })
                .when('/roleUser/:name', {
                    templateUrl: '/appViews/Manage/RoleUser.html',
                    controller: 'manage.roleUser'
                })
                .when('/addPhoneNumber', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.signup"
                })
                .when('/modifyPhoneNumber/:number', {
                    templateUrl: '/appViews/Manage/AddPhoneNumber.html',
                    controller: "phoneNumber.modify"
                })
                .when('/changePassword', {
                    templateUrl: '/appViews/Manage/ChangePassword.html',
                    controller: "password.change"
                })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ])
    .run(function ($rootScope) {
        var rootUrl = "/Manage#";
        $rootScope.menuItems = [
            {
                displayName: "账号权限管理",
                isActive: true,
                subItems: [
                    {
                        displayName: "本账号信息管理",
                        url: rootUrl + "/"
                    }, {
                        displayName: "所有用户信息管理",
                        url: rootUrl + "/all"
                    }, {
                        displayName: "所有角色管理",
                        url: rootUrl + "/roles"
                    }
                ]
            }
            
        ];
        $rootScope.rootPath = rootUrl + "/";
        $rootScope.page = {
            title: "本账号信息管理",
            messages: []
        };
        $rootScope.closeAlert = function (index) {
            $rootScope.page.messages.splice(index, 1);
        };
    })
.controller('manage.all', function ($scope, authorizeService, coverageDialogService) {
    $scope.page.title = "所有用户信息管理";
    $scope.manageUsers = [];
    authorizeService.queryAllUsers().then(function (result) {
        $scope.manageUsers = result;
        for (var i = 0; i < result.length; i++) {
            authorizeService.queryEmailConfirmed(result[i].userName).then(function (confirmed) {
                for (var j = 0; j < result.length; j++) {
                    if (result[j].userName === confirmed.Name) {
                        $scope.manageUsers[j].emailHasBeenConfirmed = confirmed.Result;
                        break;
                    }
                }
            });
        }
    });
        $scope.showRoles = function(userName) {
            coverageDialogService.showUserRoles(userName);
        };
    })
.controller("manage.current", function ($scope, authorizeService) {
    $scope.page.title = "本账号信息管理";
    authorizeService.queryCurrentUserInfo().then(function (result) {
        $scope.currentUser = result;
    });
    $scope.removePhoneNumber = function () {
        authorizeService.removePhoneNumber().then(function (result) {
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
            $scope.currentUser.phoneNumber = null;
        });
    };
    $scope.confirmEmail = function () {
        authorizeService.confirmEmail($scope.currentUser).then(function (result) {
            $scope.page.messages.push({
                contents: result.Message,
                type: result.Type
            });
        });
    };
})
.controller('manage.roleUser', function ($scope, $routeParams, $http) {
    $scope.panelTitle = $routeParams.name + "角色用户列表管理";
})
.controller('manage.roles', function ($scope, authorizeService) {
    $scope.page.title = "所有角色管理";
    $scope.manageRoles = [];
    $scope.inputRole = {
        name: ""
    };
    $scope.updateRoleList = function () {
        authorizeService.updateRoleList().then(function (result) {
            $scope.manageRoles = result;
            $scope.inputRole.name = "New role " + result.length;
        });
    };
    $scope.addRole = function () {
        authorizeService.addRole($scope.inputRole.name).then(function (result) {
            $scope.updateRoleList();
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
        }, function (reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'warning'
            });
        });
    };
    $scope.deleteRole = function (name) {
        authorizeService.deleteRole(name).then(function (result) {
            $scope.updateRoleList();
            $scope.page.messages.push({
                contents: result,
                type: 'success'
            });
        }, function (reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'warning'
            });
        });
    };

    $scope.updateRoleList();
})
.controller('password.change', function ($scope, authorizeService) {
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            authorizeService.changePassword($scope.signup).then(function (result) {
                $scope.page.messages.push({
                    contents: result.Message,
                    type: result.Type
                });
            }, function (reason) {
                $scope.page.messages.push({
                    contents: reason,
                    type: 'warning'
                });
            });
        } else {
            $scope.page.messages.push({
                contents: '输入密码有误！请检查。',
                type: 'warning'
            });
        }
    };
})
.controller('password.forgot', function ($scope, authorizeService) {
    $scope.page = {
        messages: []
    };
    $scope.signup = {
        userName: "",
        email: ""
    };
    $scope.signupForm = function () {
        authorizeService.forgotPassword($scope.signup).then(function (result) {
            $scope.page.messages.push({
                contents: result.Message,
                type: result.Type
            });
        }, function (reason) {
            $scope.page.messages.push({
                contents: reason,
                type: 'warning'
            });
        });
    };

    $scope.closeAlert = function (index) {
        $scope.page.messages.splice(index, 1);
    };
})
.controller('password.reset', function ($scope, $window, authorizeService, appUrlService) {
    $scope.signup = {
        userName: "",
        email: "",
        password: "",
        confirmPassword: "",
        code: appUrlService.parseQueryString($window.location.href).code
    };
    $scope.page = {
        messages: []
    };
    $scope.signupForm = function () {
        $scope.signupForm = function () {
            authorizeService.resetPassword($scope.signup).then(function (result) {
                $scope.page.messages.push({
                    contents: result.Message,
                    type: result.Type
                });
                if (result.Type === 'success') {
                    $window.location.href = "/Account/Login";
                }
            }, function (reason) {
                $scope.page.messages.push({
                    contents: reason,
                    type: 'warning'
                });
            });
        };
    };
    $scope.closeAlert = function (index) {
        $scope.page.messages.splice(index, 1);
    };
})
.controller('phoneNumber.signup', function ($scope, authorizeService, $window) {
    $scope.action = "添加";
    $scope.signup = {
        phoneNumber: "",
        code: "123"
    };
    $scope.verify = false;
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            if ($scope.verify) {
                authorizeService.verifyPhoneNumber($scope.signup).then(function (result) {
                    $scope.page.messages.push({
                        contents: result,
                        type: 'success'
                    });
                    $window.location.href = $scope.rootPath;
                });
            } else {
                authorizeService.addPhoneNumber($scope.signup).then(function (result) {
                    $scope.signup = {
                        code: result.Code,
                        phoneNumber: result.PhoneNumber
                    };
                    $scope.verify = true;
                });
            }
        } else {
            $scope.page.messages.push({
                contents: '输入电话号码有误！请检查。',
                type: 'warning'
            });
        }
    };
})
.controller('phoneNumber.modify', function ($scope, authorizeService, $routeParams, $window) {
    $scope.action = "修改";
    $scope.signup = {
        phoneNumber: $routeParams.number,
        code: "123"
    };
    $scope.verify = false;
    $scope.signupForm = function () {
        if ($scope.signup_form.$valid) {
            if ($scope.verify) {
                authorizeService.verifyPhoneNumber($scope.signup).then(function (result) {
                    $scope.page.messages.push({
                        contents: result,
                        type: 'success'
                    });
                    $window.location.href = $scope.rootPath;
                });
            } else {
                authorizeService.addPhoneNumber($scope.signup).then(function (result) {
                    $scope.signup = {
                        code: result.Code,
                        phoneNumber: result.PhoneNumber
                    };
                    $scope.verify = true;
                });
            }
        } else {
            $scope.page.messages.push({
                contents: '输入电话号码有误！请检查。',
                type: 'warning'
            });
        }
    };
});
