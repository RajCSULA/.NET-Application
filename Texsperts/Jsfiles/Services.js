myapp.factory('webDataService', function ($http) {

    webDataService.SaveRegistration = function (registrationInfo) {


        var dataUrl = "api/Registration" + '?registrationinfo=' + registrationInfo;

        return $http({ method: "POST", url: dataUrl }).then(function (result) {

            return result.data;

        });



    };





});