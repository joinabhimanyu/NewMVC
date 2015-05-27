(function myfunction() {
    'use strict';

    angular.module('BootstrapMaterialModule', ['ngRoute', 'ngResource', 'ngAnimate', 'ngMaterial', 'BootstrapMaterialController'])

        .config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

            $routeProvider.when('/', {
                templateUrl: 'Views/Flat/templates/firstBootstrapTemplate.html',
                controller: 'firstController'
            });

            $routeProvider.otherwise({
                redirectTo: '/'
            });

        }]);

})();