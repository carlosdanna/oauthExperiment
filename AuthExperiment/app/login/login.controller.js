(function () {
    angular.module("app")
        .controller("loginController", ["$scope", function ($scope) {
            $scope.name = "loginController";
            vm = this;
            vm.login = {};
        }])
})();