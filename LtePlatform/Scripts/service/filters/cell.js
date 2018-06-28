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