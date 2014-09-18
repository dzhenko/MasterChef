recipesApp.factory('requester', function($q, $http, auth) {
    'use strict';

    var rootUrl = 'http://localhost:21185/';

    function register(username, password) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Account/Register',
            type:"POST",
            data:{
                Email: username,
                Password: password,
                ConfirmPassword: password
            },
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data)
            }
        });

        return deferred.promise;
    }

    function login(username, password) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'Token',
            type:"POST",
            data:{
                grant_type: 'password',
                Username: username,
                Password: password
            },
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function logout() {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Account/Logout',
            type:"POST",
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function createRecipe(recipe) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/Create',
            type:"POST",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            data: recipe,
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function allRecipes() {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/All',
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function recipeById(id) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/GetById/'+ id,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function recipeByName(name) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/GetByName/' + name,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function recipeByCategory(category){
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/GetByCategory/' + category,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function commentRecipe(recipe, comment) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/Comment/',
            type:"POST",
            data: {
                RecipeId : recipe,
                Text: comment
            },
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function likeRecipe(recipe) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/Like/' + recipe,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function allCategories() {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Categories/All',
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    return {
        account: {
            register: register,
            login: login,
            logout: logout
        },
        categories: allCategories,
        recipe: {
            create: createRecipe,
            all: allRecipes,
            byId: recipeById,
            byName: recipeByName,
            byCategory: recipeByCategory
        },
        actions: {
            like: likeRecipe,
            comment: commentRecipe
        }
    }
});