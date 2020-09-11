myapp.controller('AboutCtrl', function ($scope,$http) {
   $scope.Aboutmsg = "This is about page";
    $http.get("http://localhost:61464/api/Registration")
      .then(function (response) {
          $scope.myWelcome = response.data;
      });

});