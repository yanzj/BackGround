angular.module('myApp', ['test.angular.root', 'test.angular.index', 'test.angular.chap10']);

angular.module('test.angular.index', ['app.common'])
    .controller("root.property", function($scope) {
        $scope.page.title = "Root Property";
    })
    .controller('ParentController', function($scope) {
        // 使用.controller访问`ng-controller`内部的属性
        // 在DOM忽略的$scope中，根据当前控制器进行推断
        $scope.parentProperty = 'parent scope';
    })
    .controller('ChildController', function($scope) {
        $scope.childProperty = 'child scope';
        // 同在DOM中一样，我们可以通过当前$scope直接访问原型中的任意属性
        $scope.fullSentenceFromChild = 'Same $scope: We can access: ' +
            $scope.rootProperty + ' and ' +
            $scope.parentProperty + ' and ' +
            $scope.childProperty;
    })
    .directive('myDirective', function() {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                myUrl: '@', //绑定策略
                myLinkText: '@' //绑定策略
            },
            template: '<a href="{{myUrl}}">' +
                '{{myLinkText}}</a>'
        };
    })
    .directive('theirDirective', function() {
        return {
            restrict: 'A',
            replace: true,
            scope: {
                myUrl: '=someAttr', // 经过了修改
                myLinkText: '@'
            },
            template: '\
            <div>\
                <label>My Url Field:</label>\
                <input type="text"\
                    ng-model="myUrl" />\
                <a href="{{myUrl}}">{{myLinkText}}</a>\
            </div>'
        };
    }).directive('ngFocus', [
        function() {
            var FOCUS_CLASS = "ng-focused";
            return {
                restrict: 'A',
                require: 'ngModel',
                link: function(scope, element, attrs, ctrl) {
                    ctrl.$focused = false;
                    element.bind('focus', function(evt) {
                        element.addClass(FOCUS_CLASS);
                        scope.$apply(function() {
                            ctrl.$focused = true;
                        });
                    }).bind('blur', function(evt) {
                        element.removeClass(FOCUS_CLASS);
                        scope.$apply(function() {
                            ctrl.$focused = false;
                        });
                    });
                }
            };
        }
    ])
    .controller("submit.form", function($scope) {
        $scope.page.title = "Submit Form";
    })
    .controller('signupController', [
        '$scope', function($scope) {
            $scope.submitted = false;
            $scope.signupForm = function() {
                if ($scope.signup_form.$valid) {
                    // Submit as normal
                    alert("The form is submit!");
                } else {
                    $scope.signup_form.submitted = true;
                }
            };
        }
    ])
    .controller("SimpleTypeController", function($scope) {
        $scope.page.title = "Simple Type Test";
        $scope.section.title = "Simple";
        $scope.simpleA = 1;
        $scope.simpleB = 2;
    })
    .controller("ClockController", function($scope, $timeout) {
        $scope.section.title = "Clock";
        $scope.page.title = "Simple Type Test";
        var updateClock = function() {
            $scope.clock = new Date();
            $timeout(function() {
                updateClock();
            }, 1000);
        };
        updateClock();
    })
    .controller("AddController", function($scope) {
        $scope.page.title = "Simple Type Test";
        $scope.section.title = "Add";
        $scope.counter = 0;
        $scope.add = function(amount) { $scope.counter += amount; };
        $scope.subtract = function(amount) { $scope.counter -= amount; };
    })
    .controller("ParseController", function($scope, $parse) {
        $scope.page.title = "Simple Type Test";
        $scope.section.title = "Parse";
        $scope.$watch('expr', function(newVal, oldVal, scope) {
            if (newVal !== oldVal) {
                var parseFun = $parse(newVal);
                $scope.parsedValue = parseFun(scope);
            }
        });
    })
    .controller("InterpolateController", function($scope, $interpolate) {
        $scope.page.title = "Simple Type Test";
        $scope.section.title = "Interpolate";
        $scope.$watch('emailBody', function(body) {
            if (body) {
                var template = $interpolate(body);
                $scope.previewText = template({ to: $scope.to });
            }
        });
    })
    .controller('Chap9Controller', function($scope) {
        $scope.page.title = "第 9 章　内置指令";
    })
    .controller('SomeController', function($scope) {
        // 反模式，裸值
        $scope.someBareValue = 'hello computer';
        $scope.someModel = {
            someValue: 'hello computer 2'
        }
        // 设置$scope本身的操作，这样没问题
        $scope.someAction = function() {
            // 在SomeController和ChildController内部设置{{ someBareValue }}
            $scope.someBareValue = 'hello human, from parent';
            $scope.someModel.someValue = 'hello human, from parent 2';
        };
    })
    .controller('ChildController', function($scope) {
        $scope.childAction = function() {
            // 在ChildController内部设置{{ someBareValue }}
            $scope.someBareValue = 'hello human, from child';
            $scope.someModel.someValue = 'hello human, from child 2';
        };
    })
    .controller('PeopleController', function($scope) {
        $scope.people = [
            { name: "Ari", city: "San Francisco" },
            { name: "Erik", city: "Seattle" }
        ];
    })
    .controller('FormController', function($scope) {
        $scope.fields = [
            { placeholder: 'Username', isRequired: true },
            { placeholder: 'Password', isRequired: true },
            { placeholder: 'Email (optional)', isRequired: false }
        ];
        $scope.submitForm = function() {
            alert("it works!");
        };
    })
    .controller('OtherController', function($scope) {
        // 最佳实践，永远使用一个模式
        $scope.someModel = {
            someValue: 'hello computer'
        }
        $scope.someAction = function() {
            $scope.someModel.someValue = 'hello human, from parent';
        };
    })
    .controller('OtherChildController', function($scope) {
        $scope.childAction = function() {
            $scope.someModel.someValue = 'hello human, from child';
        };
    })
    .directive('sidebox', function() {
        return {
            restrict: 'EA',
            scope: {
                title: '@'
            },
            transclude: true,
            template: '<div class="sidebox">\
            <div class="content">\
                <h2 class="header">{{ title }}</h2>\
                <span class="content" ng-transclude>\
                </span>\
            </div>\
        </div>'
        };
    });