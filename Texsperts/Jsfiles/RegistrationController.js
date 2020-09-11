
myapp.controller('RegCtrl', function ($scope, $http, $location) {
    $scope.message2 = "Registration Page";

    //Add new employee
    $scope.IsRegistrationSuccessful = function () {
        var employeeData = {

            FirstName: $scope.FirstName,
            LastName: $scope.LastName,
            Email:$scope.Email,
        Password:$scope.Password,
        ConfirmPassword : $scope.ConfirmPassword,
           
          };
        debugger;
        $http.post("api/Registration/AddEmployee", employeeData).success(function (data) {
            $location.path('/Login');
        }).error(function (data) {
            console.log(data);
            $scope.error = "Something wrong when adding new employee " + data.ExceptionMessage;
        });
    }
   
});











////$document.ready(function () {
////    $("#btnSave").click(function () {

////        var Registration = {
////            FirstName: $("#tbxFirstName").val(),
////            LastName: $("#tbxLastName").val(),

////            Email: $("#tbxEmail:").val(),
////            Password: $("#tbxPassword").val(),

////            ConfirmPassword: $("#tbxConfirmPassword").val(),

////        };

////        var objReg = JSON.stringify(Registration);

////        $.ajax({
////            type: "POST",
////            url: "api/Registration" + "?Registrationinfo=" + objReg,
////            contentType: "application/json; charset=utf-8",
////            dataType: "json",
////            success: function (r) {
////                if (r == "true") {
////                    $("#msg").text("Record save");
////                }
////                else {
////                    $("msg").text("nt saved");
////                }
////            },
////            error: function (r) {
////                alert(r.responseText);
////            },
////            failure: function (r) {
////                alert(r.responseText);
////            }
////        });
////    });
////});
