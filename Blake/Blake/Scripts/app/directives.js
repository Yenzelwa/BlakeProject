var app = angular.module("App", []);
app.directive('datePicker', function () {
    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {

            if (!ngModel) {
                return;
            }

            element.datetimepicker({
                weekStart: 0,
                autoclose: 1,
                todayHighlight: 0,
                startView: 2,
                forceParse: 0,
                showMeridian: 0,
                minuteStep: 1,
                pickerPosition: 'bottom-left',
                format: 'dd MM yyyy',
                minView: 2
            });

            element.bind('change', function () {
                scope.$apply(read);
            });

            if (ngModel != null) {
                ngModel.$render = function () {
                    var dt = new Date(ngModel.$viewValue);
                    element.datetimepicker("setDate", dt);
                    return ngModel.$viewValue;
                };
            }

            function read() {
                ngModel.$setViewValue(element.val());
            }
        }
    }
});
