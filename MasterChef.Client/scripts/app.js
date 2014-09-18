'use strict';

var routeUserChecks = {
    authenticated: {
        authenticate: function(auth, $location) {
            var result = auth.isAuthenticated();
            if (!result) $location.path('/login');
            return result;
        }
    },
    logout: {
        logout: function(auth, $location) {
            auth.logout();
            return true;
        }
    }
};

var recipesApp = angular
    .module('recipesApp', ['ngRoute'])
    .config(function($routeProvider, $sceDelegateProvider){
//        $sceDelegateProvider.resourceUrlWhitelist([
//            'self',
//            '*'
//        ]);

        $routeProvider
            .when('/login', {
                templateUrl: 'templates/login.html'
            })
            .when('/logout', {
                templateUrl: 'templates/login.html',
                resolve: routeUserChecks.logout
            })
            .when('/home', {
                templateUrl: 'templates/home.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/all-recipes', {
                templateUrl: 'templates/all-recipes.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/all-categories', {
                templateUrl: 'templates/all-categories.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/category-recipes/:categoryName', {
                templateUrl: 'templates/category-recipes.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/single-recipe/:id', {
                templateUrl: 'templates/single-recipe.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/search-recipes/:searchText', {
                templateUrl: 'templates/search-recipes.html',
                resolve: routeUserChecks.authenticated
            })
            .when('/add-recipe', {
                templateUrl: 'templates/add-recipe.html',
                resolve: routeUserChecks.authenticated
            })
            .otherwise({redirectTo: '/login'});
    });