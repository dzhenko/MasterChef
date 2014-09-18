recipesApp.controller('SingleRecipeController', function SingleRecipeController($scope, $routeParams, requester, $location) {
    'use strict';

    $scope.like = function() {
        if ($scope.likeText == 'Dislike') {
            $scope.likeText = 'Like';
            $scope.recipe.RecipeLikes--;
        }
        else {
            $scope.likeText = 'Dislike';
            $scope.recipe.RecipeLikes++;
        }

        requester.actions.like($scope.recipe.Id).then(function(){console.log('liked')});
    };

    function refresh(){
        requester.recipe.byId($routeParams.id).then(function(data){
            $scope.recipe = data;
            if ($scope.recipe.Liked == true) {
                $scope.likeText = 'Dislike';
            }
            else {
                $scope.likeText = 'Like';
            }
        });
    }

    refresh();

    $scope.addComment = function(commentText) {
        requester.actions.comment($scope.recipe.Id, commentText).then(function() {
            $scope.commentText = '';
            refresh();
        })
    }
});
