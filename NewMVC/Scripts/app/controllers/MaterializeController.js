(function () {
    'use strict';

    angular.module('MaterializeController', [])

        .controller('firstController', function ($scope, $routeParams, $location) {

            $scope.messageHeading = "Learn how to easily start using Materialize in your website."

            $scope.messageBody = 'Materialize comes in two different forms. You can select which version you want depending on your preference and expertise.';

            $scope.color = "red";

        });

})();