(function () {
    'use strict';

    angular.module('DirectCard', [])

        .directive('directiveCard', function ($compile, $templateCache, $rootScope) {

            return {
                restrict: 'EA',
                replace: false,
                transclude: false,
                templateUrl: 'Scripts/app/directives/templates/tmplCard.html',
                compile: function (element, attrs, transclude) {
                    return {
                        pre: function (scope, element, attrs) {

                        },
                        post: function (scope, element, attrs) {

                            (function () {

                            })();

                        }
                    };
                }
            };

        });


})();