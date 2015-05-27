(function () {
    'use strict';

    angular.module('BootstrapMaterialController', [])

        .controller('firstController', function ($scope, $routeParams, $location) {

            $scope.jumbotronHeader = "Bootstrap Material Design";
            $scope.jombotronContent = "This is a simple hero unit, a simple jumbotron-style component for calling extra attention to featured content or information.";

        });

})();