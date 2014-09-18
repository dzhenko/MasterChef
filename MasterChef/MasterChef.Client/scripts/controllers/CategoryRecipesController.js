recipesApp.controller('CategoryRecipesController', function CategoryRecipesController($scope, $routeParams, requester) {
    'use strict';

    $scope.name = $routeParams.categoryName;
    $scope.information = '. . . loading';

    requester.recipe.byCategory($routeParams.categoryName).then(function(data){
        $scope.foundRecipes = data;
        if(data.length === 0) {
            $scope.information = 'None';
        }
        else {
            $scope.information = data.length;
        }
    });
});
