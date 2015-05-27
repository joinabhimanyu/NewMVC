(function () {
    'use strict';

    angular.module('CustomerAngularFactory', [])

    .factory('CustomerFactory', function ($resource) {

        return $resource("customers/CustomersApi/:customerID", { customerID: '@customerID' });

    });

})();



