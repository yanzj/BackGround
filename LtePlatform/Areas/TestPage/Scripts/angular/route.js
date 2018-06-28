angular.module('test.angular.root', ['app.common'])
    .run(function($rootScope, $timeout) { // 使用.run访问$rootScope
        $timeout(function() {
            $rootScope.myHref = 'http://google.com';
        }, 2000);
        $rootScope.rootProperty = 'root scope';
        var indexPath = '/TestPage/AngularTest#/';
        $rootScope.menuItems = [
            {
                displayName: "AngularJS测试",
                isActive: true,
                subItems: [
                    {
                        name: "Simple",
                        displayName: "Simple Type Test",
                        url: indexPath + 'simple'
                    }, {
                        name: "SubmitForm",
                        displayName: "Submit Form",
                        url: indexPath
                    }, {
                        name: "RootProperty",
                        displayName: "Root Property",
                        url: indexPath + "root"
                    }, {
                        name: "Chapter9Ari",
                        displayName: "第 9 章　内置指令",
                        url: indexPath + "Chapter9Ari"
                    }, {
                        name: "Chapter10Ari",
                        displayName: "第 10 章　指令详解",
                        url: indexPath + "links"
                    }
                ]
            }, {
                displayName: "CoffeeScript脚本测试",
                isActive: false,
                subItems: [
                    {
                        displayName: "Hotseat 5x5",
                        url: "/TestPage/CoffeeScript/Hotseat",
                        tooltip: "5×5棋盘"
                    }
                ]
            }, {
                displayName: "WebApi测试",
                isActive: false,
                subItems: [
                    {
                        displayName: "工参上传测试",
                        url: "/TestPage/WebApiTest/BasicPost",
                        tooltip: "全网LTE和CDMA基站、小区列表和地理化显示、对全网的基站按照基站名称、地址等信息进行查询，并进行个别基站小区的增删、修改信息的操作"
                    }, {
                        displayName: "简单类型测试",
                        url: "/TestPage/WebApiTest/SimpleType",
                        tooltip: "对传统指标（主要是2G和3G）的监控、分析和地理化呈现"
                    }, {
                        displayName: "Html5csv测试",
                        url: "/TestPage/WebApiTest/Html5Test",
                        tooltip: "对接本部优化部4G网优平台，实现对日常工单的监控和分析"
                    }, {
                        displayName: "Html5csv测试-2",
                        url: "/TestPage/WebApiTest/Html5PostTest",
                        tooltip: "校园网专项优化，包括数据管理、指标分析、支撑工作管理和校园网覆盖呈现"
                    }
                ]
            }
        ];

        $rootScope.sectionItems = [
            {
                name: "Simple",
                displayName: "简单类型测试",
                url: indexPath + 'simple'
            }, {
                name: "Add",
                displayName: "简单加减法",
                url: indexPath + "add"
            }, {
                name: "Clock",
                displayName: "时钟控制器",
                url: indexPath + "clock"
            }, {
                name: "Interpolate",
                displayName: "插值字符串测试",
                url: indexPath + "interpolate"
            }, {
                name: "Parse",
                displayName: "表达式解析测试",
                url: indexPath + "parse"
            },
            {
                name: "Links",
                displayName: "指令测试",
                url: indexPath + 'links'
            }, {
                displayName: "日期控件",
                items: [
                    {
                        url: indexPath + "dateparser",
                        displayName: "日期解析"
                    }, {
                        url: indexPath + "datepicker",
                        displayName: "复杂控件"
                    }, {
                        url: indexPath + "timepicker",
                        displayName: "时间控件"
                    }
                ]
            }, {
                displayName: "控件实例I",
                items: [
                    {
                        url: indexPath + "accordion",
                        displayName: "手风琴"
                    }, {
                        url: indexPath + "alert",
                        displayName: "警告框"
                    }, {
                        url: indexPath + "buttons",
                        displayName: "按钮"
                    }, {
                        url: indexPath + "collapse",
                        displayName: "折叠"
                    }, {
                        url: indexPath + "progress",
                        displayName: "进度条"
                    }
                ]
            }, {
                displayName: "控件实例II",
                items: [
                    {
                        url: indexPath + "carousel",
                        displayName: "轮播"
                    }, {
                        url: indexPath + "dropdown",
                        displayName: "下拉列表"
                    }, {
                        url: indexPath + "pager",
                        displayName: "分页符"
                    }, {
                        url: indexPath + "pagination",
                        displayName: "分页"
                    }, {
                        url: indexPath + "tabs",
                        displayName: "标签"
                    }
                ]
            }, {
                displayName: "控件实例III",
                items: [
                    {
                        url: indexPath + "modal",
                        displayName: "模态框"
                    }, {
                        url: indexPath + "popover",
                        displayName: "弹出"
                    }, {
                        url: indexPath + "rating",
                        displayName: "星级"
                    }, {
                        url: indexPath + "tooltip",
                        displayName: "提示"
                    }, {
                        url: indexPath + "typeahead",
                        displayName: "自动补全"
                    }
                ]
            }
        ];
        $rootScope.section = {
            title: "Simple"
        };
        $rootScope.page = {
            title: "RootProperty"
        };
    })
    .config([
        '$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
            $locationProvider.hashPrefix('');
            var viewDir = "/appViews/Test/Angular/";
            var simpleDir = "/appViews/Test/Simple/";
            var chap10Dir = "/appViews/Test/Chapter10/";
            $routeProvider
                .when('/', {
                    templateUrl: viewDir + "SubmitForm.html",
                    controller: "submit.form"
                })
                .when('/root', {
                    templateUrl: viewDir + "RootProperty.html",
                    controller: "root.property"
                })
                .when('/legacy', {
                    templateUrl: viewDir + "LegacyMarkup.html",
                    controller: "legacy.markup"
                })
                .when('/simple', {
                    templateUrl: simpleDir + "SimpleType.html",
                    controller: "SimpleTypeController"
                })
                .when('/add', {
                    templateUrl: simpleDir + "Add.html",
                    controller: "AddController"
                })
                .when('/clock', {
                    templateUrl: simpleDir + "Clock.html",
                    controller: "ClockController"
                })
                .when('/interpolate', {
                    templateUrl: simpleDir + "Interpolate.html",
                    controller: "InterpolateController"
                })
                .when('/parse', {
                    templateUrl: simpleDir + "Parse.html",
                    controller: "ParseController"
                })
                .when('/Chapter9Ari', {
                    templateUrl: viewDir + "Chapter9Ari.html",
                    controller: "Chap9Controller"
                })

            .when('/links', {
                templateUrl: chap10Dir + "Links.html",
                controller: "LinksController"
            })
            .when('/dateparser', {
                templateUrl: chap10Dir + "demo.dateparser.html",
                controller: "DateParserDemoCtrl"
            })
            .when('/datepicker', {
                templateUrl: chap10Dir + "demo.datepicker.html",
                controller: "DatepickerPopupDemoCtrl"
            })
            .when('/accordion', {
                templateUrl: chap10Dir + "demo.accordion.html",
                controller: "AccordionDemoCtrl"
            })
            .when('/alert', {
                templateUrl: chap10Dir + "demo.alert.html",
                controller: "AlertDemoCtrl"
            })
            .when('/buttons', {
                templateUrl: chap10Dir + "demo.buttons.html",
                controller: "ButtonsCtrl"
            })
            .when('/carousel', {
                templateUrl: chap10Dir + "demo.carousel.html",
                controller: "CarouselDemoCtrl"
            })
            .when('/collapse', {
                templateUrl: chap10Dir + "demo.collapse.html",
                controller: "CollapseDemoCtrl"
            })
            .when('/dropdown', {
                templateUrl: chap10Dir + "demo.dropdown.html",
                controller: "DropdownCtrl"
            })
            .when('/modal', {
                templateUrl: chap10Dir + "demo.modal.html",
                controller: "ModalDemoCtrl"
            })
            .when('/pager', {
                templateUrl: chap10Dir + "demo.pager.html",
                controller: "PagerDemoCtrl"
            })
            .when('/pagination', {
                templateUrl: chap10Dir + "demo.pagination.html",
                controller: "PaginationDemoCtrl"
            })
            .when('/popover', {
                templateUrl: chap10Dir + "demo.popover.html",
                controller: "PopoverDemoCtrl"
            })
            .when('/progress', {
                templateUrl: chap10Dir + "demo.progress.html",
                controller: "ProgressDemoCtrl"
            })
            .when('/rating', {
                templateUrl: chap10Dir + "demo.rating.html",
                controller: "RatingDemoCtrl"
            })
            .when('/tabs', {
                templateUrl: chap10Dir + "demo.tabs.html",
                controller: "TabsDemoCtrl"
            })
            .when('/timepicker', {
                templateUrl: chap10Dir + "demo.timepicker.html",
                controller: "TimepickerDemoCtrl"
            })
            .when('/tooltip', {
                templateUrl: chap10Dir + "demo.tooltip.html",
                controller: "TooltipDemoCtrl"
            })
            .when('/typeahead', {
                templateUrl: chap10Dir + "demo.typeahead.html",
                controller: "TypeaheadCtrl"
            })
                .otherwise({
                    redirectTo: '/'
                });
        }
    ]);