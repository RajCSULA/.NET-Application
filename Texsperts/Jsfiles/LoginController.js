//myapp.controller('LoginCtrl', function ($scope) {

//    $scope.message4="Login Page";
//});




myapp.controller('LoginCtrl', function ($scope, $http, $location, $rootScope) {
    $scope.message4 = "Login Page";

                    $scope.Isloginsuccess = function () {
                    var employeeData = {

                        FirstName: $scope.FirstName,
                      
                        Password:$scope.Password     
                    };
                    debugger;
                    $http.get("api/Registration/GetAllEmployee", employeeData).success(function (data) {
                        $location.path('/Home');
                    }).error(function (data) {
                        console.log(data);
                        $scope.error = "Something wrong when adding new employee " + data.ExceptionMessage;
                    });
                }
   
        });
































    