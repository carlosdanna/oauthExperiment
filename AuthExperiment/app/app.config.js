(function () {
    angular.module("app")
        .config(function ($stateProvider, $urlRouterProvider) 
        {
            $urlRouterProvider.otherwise("/");

            $stateProvider
                .state('home', {
                    url: "/",
                    templateUrl: "app/app.tmpl.html",
                    controller: "appController",
                    controllerAs: "vm"
                })
                .state('home.login', {
                    url: "home/login",
                    templateUrl: "app/login/login.tmpl.html",
                    controller: "loginController",
                    controllerAs: "vm"
                })
              
                .state('home.signup', {
                    url: "home/signup",
                    templateUrl: "app/signup/signup.tmpl.html",
                    controller: "signupController",
                    controllerAs: "vm"
                })
                .state('home.dashboard', {
                    url: "home/dashboard",
                    templateUrl: "app/dashboard/dashboard.tmpl.html",
                    controller: "dashboardController",
                    controllerAs: "vm"
                });
        });
})();