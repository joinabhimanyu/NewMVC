(function () {
    'use strict';

    angular.module('CustomerAngularController', [])

        .controller('MaterialController', function ($scope, $routeParams, $location, $mdDialog, mtService) {

            $scope.results = [];

            $scope.isSearching = false;

            $scope.toolbarText = "Material Design";

            $scope.search = function () {

                $scope.isSearching = true;
                var search_term = $scope.searchTerm;

                var request = mtService.getSearchResult(search_term);

                request.then(function (data) {

                    $scope.results = data;
                    $scope.isSearching = false;

                }, function (error) {
                    console.error(error);
                });

            }

            $scope.showAdvanced = function (ev) {

                $mdDialog.show({
                    controller: DialogController,
                    templateUrl: 'dialog1.tmpl.html',
                    targetEvent: ev,
                })
    .then(function (answer) {
        $scope.alert = 'You said the information was "' + answer + '".';
    }, function () {
        $scope.alert = 'You cancelled the dialog.';
    });

            }

        })

.controller('FactoryCustomers', function ($scope, $location, $routeParams, $mdDialog, CustomerFactory) {

    var customers = CustomerFactory.query();
    $scope.customers = customers;

    $scope.color_directive = "#333";

    $scope.selectRow = function (row) {
        $scope.selectedRow = row;
    }

    $scope.showRow = '';

    $scope.showHide = function () {
        if ($scope.showRow == 'true') {
            $scope.showRow = 'false';
        }
        else {
            if ($scope.showRow == 'false') {
                $scope.showRow = 'true';
            }
            else {
                $scope.showRow = 'false';
            }
        }
    }

    //md-checkbox model
    $scope.data = {
        cb1: true
    }

    //mdDialog model
    $scope.showDialog = function (event) {

        // Appending dialog to document.body to cover sidenav in docs app
        // Modal dialogs should fully cover application
        // to prevent interaction outside of dialog
        console.log('dialog clicked');

        var myAlert = $mdDialog.alert({
            title: 'Attention',
            content: 'This is an example how easy dialogs can be!',
            ok: 'Close'
        });

        $mdDialog
            .show(myAlert)
            .finally(function () {
                myAlert = undefined;
            });

    };

    $scope.makeJumbotron = '';

    $scope.showJumbotron = function () {
        if ($scope.makeJumbotron == 'true') {
            $scope.makeJumbotron = 'false';
        }
        else {
            if ($scope.makeJumbotron == 'false') {
                $scope.makeJumbotron = 'true';
            }
            else {
                $scope.makeJumbotron = 'true';
            }
        }
    }

})

.controller('CustomerByIDFactory', function ($scope, $location, $routeParams, CustomerFactory) {

    var customerID = $routeParams.customerID;
    CustomerFactory.get({ customerID: customerID }, function (customer) {
        $scope.customer = customer;
    })

})

.controller('AllCustomers', function ($scope, $location, $routeParams, CustomerService) {

    var request = CustomerService.getAllCustomers();
    request.then(function (p1) {
        $scope.customers = p1.data;
    }, function (err) {
        console.log('ERR: ' + err);
    });

    $scope.selectRow = function (row) {
        $scope.selectedRow = row;
    }

})

.controller('CustomerByID', function ($scope, $routeParams, CustomerService) {
    var customerID = $routeParams.customerID;
    var request = CustomerService.getCustomerByID(customerID);
    request.then(function (p1) {
        $scope.customer = p1.data;
    }, function (err) {
        console.log('ERR: ' + err);
    });
});

    function DialogController($scope, $mdDialog) {

        $scope.hide = function () {
            $mdDialog.hide();
        };
        $scope.cancel = function () {
            $mdDialog.cancel();
        };
        $scope.answer = function (answer) {
            $mdDialog.hide(answer);
        };

    }

})();

