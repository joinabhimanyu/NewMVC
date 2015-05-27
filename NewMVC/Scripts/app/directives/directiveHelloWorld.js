angular.module('directiveHelloWorld', [])

    .directive('helloWorld', function ($compile, $templateCache, $rootScope) {

        return {
            restrict: 'EA',
            replace: false,
            transclude: false,
            template: $templateCache.get('helloWorld.html'),
            //templateUrl: 'Scripts/app/directives/templates/helloWorld.html',
            require: '?helloWorld',
            scope: {
                color: '=',
                shown: '=',
                jumbo: '='
            },

            controller: function ($scope, $element, $attrs, $transclude) {
                this.id = "The ID: hello-world, this is set from the controller";

                this.showId = function () {
                    return this.id;
                }

                $scope.id = "The ID: hello-world, this is set from the controller";

                $scope.new_text = '';

                $scope.changeText = function () {
                    $scope.new_text = 'The new ID: Jumbo, this is set from the controller and updated by $watch.';

                    var new_changed_text = 'This is the new text set from the $emit event on the $rootScope.';
                    $rootScope.$emit('textChanged', new_changed_text);
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
                        console.log('post-link called');
                        console.log('html before post-link');
                        console.log(element.html());

                        (function () {

                            console.log('html after post-link');
                            console.log(element.html());
                            console.log("---" + controller.id);

                            console.log(controller.showId());
                            $("#directive-header").html(controller.showId());

                            var animEndEnv = "webkitAnimationEnd animationend";
                            var $animationType = "animated bounceInDown";
                            var $this = element;

                            $this.addClass($animationType).one(animEndEnv, function () {
                                $this.removeClass($animationType);
                            });

                        })();

                        $rootScope.$on('textChanged', function (event, data) {
                            var html = '<p>' + data + '</p>';
                            $("#first-jumbo").append(html);
                        });

                        $rootScope.$on('jumbo_clicked', function (event) {
                            var animEndEnv = "webkitAnimationEnd animationend";
                            var $animationType = "animated shake";
                            var $this = element;

                            if ($this.hasClass('animated bounceInRight')) {
                                $this.removeClass('animated bounceInRight');
                            }
                            else {
                                if ($this.hasClass('animated bounceOutLeft')) {
                                    $this.removeClass('animated bounceOutLeft');
                                }
                            }

                            $this.addClass($animationType).one(animEndEnv, function () {
                                $this.removeClass($animationType);
                            });
                        });

                        scope.$watch('new_text', function (new_value, old_value) {
                            scope.id = new_value;
                        });

                        scope.$watch('jumbo', function (new_value, old_value) {

                            if (new_value == 'true') {
                                //var markup = "<div class='jumbotron'><h1>Heading</h1><p>This is jumbotron." + controller.showId() + "</p></div>";

                                var markup = $templateCache.get("jumbo.html");

                                element.html($(markup)).show();
                                $compile(element.contents())(scope);
                            }
                            else {
                                if (new_value == 'false') {

                                    var markup = $templateCache.get('helloWorld.html');
                                    element.html($(markup)).show();
                                    $compile(element.contents())(scope);

                                    $("#directive-header").html(controller.showId());
                                }
                            }
                            
                        });

                        scope.$watch('color', function (new_value, old_value) {

                            element.animate({
                                color: new_value
                            });

                        });

                        scope.$watch('shown', function (new_value, old_value) {

                            if (new_value == 'true') {
                                var animEndEnv = "webkitAnimationEnd animationend";
                                var $animationType = "animated bounceInRight";

                                if (element.hasClass('animated bounceOutLeft')) {
                                    element.removeClass('animated bounceOutLeft');
                                }

                                element.addClass($animationType);
                            }
                            else {
                                if (new_value == 'false') {
                                    var animEndEnv = "webkitAnimationEnd animationend";
                                    var $animationType = "animated bounceOutLeft";

                                    if (element.hasClass('animated bounceInRight')) {
                                        element.removeClass('animated bounceInRight');
                                    }

                                    element.addClass('animated bounceOutLeft');
                                }
                            }
                        });

                        element.on('click', function () {

                            $rootScope.$emit('jumbo_clicked');               

                        });

                    }
                };
            }

        };

    });