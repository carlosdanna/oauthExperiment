(function () {
    angular.module("app")
        .controller("signupController", ["SignUpServices", function (SignUpServices) {
            
            vm = this;
            vm.login = {};
            vm.signUp = function () {

                console.log("came here");
                console.log(vm.login);
                SignUpServices.SignUp(vm.login).then(function (data) {
                    console.log(data);
                });
            }
        }])
})();