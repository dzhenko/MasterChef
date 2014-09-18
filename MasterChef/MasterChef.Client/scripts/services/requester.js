recipesApp.factory('requester', function($q, $http, auth) {
    'use strict';

    var rootUrl = 'http://masterchef-1.apphb.com/';

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
            url:rootUrl + 'api/Recipes',
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
            url:rootUrl + 'api/Recipes',
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
            url:rootUrl + 'api/Recipes/'+ id,
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
            url:rootUrl + 'api/Recipes/?param=name&value=' + name,
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
            url:rootUrl + 'api/Recipes/?param=category&value=' + category,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function recipeByUserId(id){
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/?param=user&value=' + id,
            type:"GET",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function ownRecipes(){
        return recipeByUserId();
    }

    function recipeDelete(id) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/' + id,
            type:"DELETE",
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function commentRecipe(id, comment) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/?id=' + id + '&comment=' + comment,
            type:"PUT",
            data: comment,
            beforeSend: function(xhr){xhr.setRequestHeader('Authorization', 'bearer ' + auth.token())},
            contentType:"application/x-www-form-urlencoded",
            success: function(data){
                deferred.resolve(data);
            }
        });

        return deferred.promise;
    }

    function likeRecipe(id) {
        var deferred = $q.defer();

        $.ajax({
            url:rootUrl + 'api/Recipes/' + id,
            type:"PUT",
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
            url:rootUrl + 'api/Categories',
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
            byCategory: recipeByCategory,
            byUser:recipeByUserId,
            own: ownRecipes,
            delete: recipeDelete
        },
        actions: {
            like: likeRecipe,
            comment: commentRecipe
        }
    }
});