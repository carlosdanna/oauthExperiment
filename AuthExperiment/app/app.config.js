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
                .state('login', {
                    url: "/login",
                    templateUrl: "app/login/login.tmpl.html",
                    controller: "loginController",
                    controllerAs: "vm"
                })
              
                .state('signup', {
                    url: "/signup",
                    templateUrl: "app/signup/signup.tmpl.html",
                    controller: "signupController",
                    controllerAs: "vm"
                })
                .state('dashboard', {
                    url: "/dashboard",
                    templateUrl: "app/dashboard/dashboard.tmpl.html",
                    controller: "dashboardController",
                    controllerAs: "vm"
                });
        });
})();