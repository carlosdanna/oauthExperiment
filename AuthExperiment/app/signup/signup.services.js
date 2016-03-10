(function () {
    angular.module("app")
        .factory("SignUpServices", signUpServices);

    function signUpServices($http, $q) {
        var SignUpServices = {};
        var signUp = function (data) {
            var deferred = $q.defer();
            $http.post('http://localhost:62756/Api/User/CreateUser', data)
                .success(function (data) {
                    deferred.resolve(data);
                })
                .error(function (err) {
                    deferred.reject(err);
                });
            return deferred.promise;
        }

        SignUpServices.SignUp = signUp;
       
        return SignUpServices;
        
    }
})();