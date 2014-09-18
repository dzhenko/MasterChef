recipesApp.factory('auth', function() {
    'use strict';
    return {
        login: function(token){
            localStorage.setItem("recipesBearerToken", token);
        },
        token: function(){
            return localStorage.getItem("recipesBearerToken");
        },
        logout: function(){
            localStorage.removeItem("recipesBearerToken");
        },
        isAuthenticated: function() {
            return localStorage.getItem("recipesBearerToken") != undefined;
        }
    }
});