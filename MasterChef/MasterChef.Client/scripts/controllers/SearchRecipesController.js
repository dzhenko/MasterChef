'use strict';

recipesApp.controller('SearchRecipesController', function SearchRecipesController($scope, $routeParams, requester) {
    $scope.information = '. . . searching';

    requester.recipe.byName($routeParams.searchText).then(function(data){
        console.log(data);
        $scope.foundRecipes = data;
        if(data.length === 0) {
            $scope.information = 'None';
        }
        else {
            $scope.information = data.length;
        }
    });
});
