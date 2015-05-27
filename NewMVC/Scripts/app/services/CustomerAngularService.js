(function () {
    'use strict';

    angular.module('CustomerAngularService', [])

.service('CustomerService', function ($http) {

    this.getAllCustomers = function () {
        return $http.get('customers/CustomersApi');
    };

    this.getCustomerByID = function (customerID) {
        return $http.get('customers/CustomersApi/', { params: { customerID: customerID } });
    };

});

})();

