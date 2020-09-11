var myapp = angular.module('myApp', ['ngRoute']);
myapp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
   .when('/', {
       controller: 'AboutCtrl',
       templateUrl: '../Html/About.html'

   })
   .when('/Home', {
       controller: 'HomeCtrl',
       templateUrl: '../Html/Home.html'
      })
     .when('/Login',
     {
         controller: 'LoginCtrl',
         templateUrl: '../Html/Login.html'
     })
          .when('/Registration',
     {
         controller: 'RegCtrl',
         templateUrl: '../Html/Registration.html'
     })
     .otherwise({
         redirectTo: '/'
     });
    $locationProvider.html5Mode(true);
}]);

