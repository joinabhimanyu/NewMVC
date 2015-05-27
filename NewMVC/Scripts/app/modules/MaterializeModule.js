(function () {

    'use strict';

    angular.module('MaterializeModule', ['ngRoute', 'ngResource', 'ngAnimate', 'ngMaterial', 'ui.router', 'MaterializeController', 'DirectDropDown', 'DirectCard'])

        .config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {

            $urlRouterProvider.otherwise('/home');

            $stateProvider.state('home', {
                url: '/home',
                templateUrl: 'Views/Flat/templates/firstMaterialView.html',
                controller: 'firstController'
            });

        }]);

})();