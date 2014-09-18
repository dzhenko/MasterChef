recipesApp.controller('AllCategoriesController', function AllCategoriesController($scope, requester) {
    'use strict';

    requester.categories().then(function(data){
        $scope.foundCategories = data;
    })
});
