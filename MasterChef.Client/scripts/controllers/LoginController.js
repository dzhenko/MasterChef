recipesApp.controller('LoginController', function HomeController($location, $rootScope, $scope, requester, auth) {
    'use strict';

    $scope.welcome = 'Welcome to MasterChef - our recipes site';
    $scope.start = 'Start by signing up or logging in';

    $scope.username = '';
    $scope.password = '';
    $scope.regUsername = '';
    $scope.regPassword = '';
    $scope.regConfirmPassword = '';

    $rootScope.currentUserIsLoggedIn = false;

    $scope.imgSrc = 'http://www-tc.pbs.org/food/wp-content/blogs.dir/2/files/2012/12/Year-in-Food-2012-Recipes-Feat-602x338.jpg';

    $scope.register = function(username, password){
        requester.account.register(username, password).then(function(data){
            console.log('Registered');
            $rootScope.currentUserIsLoggedIn = true;
            $scope.login(username, password);
        });
    };

    $scope.login = function(username, password){
        requester.account.login(username, password).then(function(data){
            console.log('Logged in');
            $rootScope.currentUserIsLoggedIn = true;
            auth.login(data.access_token);
            $location.path('/home');
        });
    };
});
