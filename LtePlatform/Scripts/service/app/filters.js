angular.module('basic.filters', [])
    .filter("percentage", function() {
        return function(input) {
            return angular.isNumber(input)
                ? (input > 1 || input < -1) ? input : input * 100
                : parseFloat(input) * 100;
        };
    })
    .constant('formErrorDefs', {
        email: '不是有效的邮件地址',
        required: '此项不能为空',
        same: '此项必须与上一项相同',
        minlength: '必须至少包含8 个字符'
    })
    .filter('formError', function(formErrorDefs) {
        return function (name, customMessages) {
            var errors = angular.extend({}, formErrorDefs, customMessages);
            return errors[name] || name;
        };
    })
    .filter('yesNoChinese', function() {
        return function(input) {
            return input ? '是' : '否';
        };
    })
    .filter('bitToByte', function() {
        return function(input) {
            return angular.isNumber(input) ? input / 8 : parseFloat(input) / 8;
        }
    });
angular.module('cell.filters', [])
    .filter("dbStep32", function() {
        var dict = [
            -24, -22, -20, -18, -16, -14, -12, -10, -8, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 32 ? dict[input] : input;
        };
    })
    .filter("powerStep8", function() {
        var dict = [
            -6, -4.77, -3, -1.77, 0, 1, 2, 3
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 8 ? dict[input] : input;
        };
    })
    .filter("pathLoss", function() {
        var dict = [
            0, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 8 ? dict[input] : input;
        };
    })
    .filter("deltaFPucch", function() {
        var dict;
        return function(input, format) {
            switch (format) {
            case '1':
                dict = [-2, 0, 2];
                return angular.isNumber(input) && input >= 0 && input < 3 ? dict[input] : input;
            case '1b':
                dict = [1, 3, 5];
                return angular.isNumber(input) && input >= 0 && input < 3 ? dict[input] : input;
            case '2':
                dict = [-2, 0, 1, 2];
                return angular.isNumber(input) && input >= 0 && input < 4 ? dict[input] : input;
            case '2a':
            case '2b':
                dict = [-2, 0, 2];
                return angular.isNumber(input) && input >= 0 && input < 3 ? dict[input] : input;
            default:
                return 'undefined';
            }
        }
    });
angular.module('handoff.filters', [])
    .filter("timeToTrigger", function() {
        var durations = [
            0, 40, 64, 80, 100, 128, 160, 256, 320, 480, 512, 640, 1024, 1280, 2560, 5120
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 16 ? durations[input] : input;
        }
    })
    .filter("reportInterval", function() {
        var durations = [
            '120ms', '240ms', '480ms', '640ms', '1024ms', '2048ms', '5120ms', '10240ms', '1min', '6min', '12min', '30min', '60min'
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 13 ? durations[input] : 'illegal';
        }
    })
    .filter("reportAmount", function() {
        var amounts = [
            "1", "2", "4", "8", "16", "32", "64", "无限次"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 8 ? amounts[input] : input;
        }
    })
    .filter("huaweiEvent", function() {
        var amounts = [
            "A3", "A4", "A5"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 3 ? amounts[input] : input;
        }
    })
    .filter("zteIntraRatEvent", function() {
        var amounts = [
            "A1", "A2", "A3", "A4", "A5", "A6"
        ];
        return function(input) {
            return angular.isNumber(input) && input >= 0 && input < 6 ? amounts[input] : input;
        }
    })
    .filter("halfDb", function() {
        return function(input) {
            return angular.isNumber(input) ? input / 2 : input;
        };
    })
    .filter("doubleDb", function() {
        return function(input) {
            return angular.isNumber(input) ? input * 2 : input;
        };
    })
    .filter("triggerQuantity", function() {
        var types = ["RSRP", "RSRQ"];
        return function(input) {
            return input === 0 || input === 1 ? types[input] : input;
        };
    })
    .filter("reportQuantity", function() {
        var types = ["与触发量相同", "全部发送"];
        return function(input) {
            return input === 0 || input === 1 ? types[input] : input;
        };
    });

angular.module('app.filters', ['basic.filters', 'cell.filters', 'handoff.filters']);