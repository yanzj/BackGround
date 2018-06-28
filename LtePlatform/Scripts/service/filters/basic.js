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