(function () {
    angular.module("app")
        .controller("appController", ["$scope", function ($scope) {
            $scope.test = "This is appController";
        }]);
})();