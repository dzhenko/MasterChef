recipesApp.controller('UserRecipesController', function AllRecipesController($scope, requester) {
    'use strict';

    requester.recipe.own().then(function(data){
        $scope.recipes = data;
        $scope.information = data.length + " recipes";
    });

    $scope.information = ". . . loading";

    $scope.sortBy = 'Id';
    $scope.sortTypeText = 'Ascending';
    $scope.sortByReverse = false;

    $scope.sortTypeClicked = function() {
        if ($scope.sortTypeText === 'Ascending') {
            $scope.sortTypeText = 'Descending';
        }
        else {
            $scope.sortTypeText = 'Ascending';
        }
        $scope.sortByReverse = !$scope.sortByReverse;
    };

    $scope.deleteRecipe = function(id){
        requester.recipe.delete(id).then(function(){
            requester.recipe.own().then(function(data){
                $scope.recipes = data;
                $scope.information = data.length + " recipes";
            });
        })
    }
});
