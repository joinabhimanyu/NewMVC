(function () {
    'use strict';

    var app = angular.module('CustomerAngularMod', ['ngRoute', 'ngResource', 'ngAnimate', 'ngMaterial', 'CustomerAngularService', 'MaterialService', 'CustomerAngularFactory', 'CustomerAngularController', 'directiveHelloWorld', 'directiveMyElement']);

    //Defining Routing
    app.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {

        $routeProvider.when('/', {
            templateUrl: 'Views/Flat/templates/Material.html',
            controller: 'MaterialController'
        });

        $routeProvider.when('/allFactoryCustomers',
        {
            templateUrl: 'Views/Flat/templates/AllCustomers.html',
            controller: 'FactoryCustomers'
        });

        $routeProvider.when('/customer/:customerID',
        {
            templateUrl: 'Views/Flat/templates/CustomerByID.html',
            controller: 'CustomerByIDFactory'
        });

        $routeProvider.when('/allServiceCustomers',
        {
            templateUrl: 'Views/Flat/templates/AllCustomers.html',
            controller: 'AllCustomers'
        });

        $routeProvider.when('/:customerID',
        {
            templateUrl: 'Views/Flat/templates/CustomerByID.html',
            controller: 'CustomerByID'
        });

        $routeProvider.otherwise(
        {
            redirectTo: '/'
        });

        // $locationProvider.html5Mode(true);
        //$locationProvider.html5Mode(true).hashPrefix('!')
    }]);

})();






