angular.module('directiveMyElement', [])

    .directive('myElement', function ($compile, $templateCache, $rootScope) {
        return {
            restrict: 'EA',
            replace: false,
            transclude: false,
            templateUrl: 'Scripts/app/directives/templates/myElement.html',
            scope: {
                owner: '=',
                color: '='
            },
            require: '?myElement',
            controller: function ($scope, $element, $attrs, $transclude) {
                $scope.show = function () {
                    $rootScope.$emit('show_clicked');
                }

                $scope.hide = function () {
                    $rootScope.$emit('hide_clicked');
                }
            },
            compile: function (element, attrs, transclude) {

                console.log('compile called');
                console.log(element.html());

                return {
                    pre: function (scope, element, attrs, controller) {

                        console.log('pre-link called');
                        console.log(element.html());

                    },
                    post: function (scope, element, attrs, controller) {

                        (function () {
                            console.log('post-link called');
                            console.log(element.html());

                            var animEndEnv = "webkitAnimationEnd animationend";
                            var $animationType = "animated bounceInDown";
                            var $this = element;

                            $this.addClass($animationType).one(animEndEnv, function () {
                                $this.removeClass($animationType);
                            });
                        })();

                        $rootScope.$on('show_clicked', function (event) {
                            console.log('btnShow clicked');
                            $("#content-row").animate({
                                opacity: '1'
                            });
                        });

                        $rootScope.$on('hide_clicked', function (event) {
                            $("#content-row").animate({
                                opacity: '0'
                            });
                        });

                    }
                };
            }
        };
    });