var app = angular.module("app", ['ngAnimate', 'toastr', 'dataGrid', 'pagination', 'angular-loading-bar']).config(['cfpLoadingBarProvider', function (cfpLoadingBarProvider) {
    cfpLoadingBarProvider.includeSpinner = true;
}]);
