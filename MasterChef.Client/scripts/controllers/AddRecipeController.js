recipesApp.controller('AddRecipeController', function AddRecipeController($scope, $compile, $location, requester) {
    'use strict';

    $scope.categories = ['Soup', 'Salad', 'Main Dish', 'Dessert'];

    var minutesArr = [];
    for (var i = 1; i <= 36; i++) {
        minutesArr.push(i*5);
    }
    $scope.minutes=minutesArr;

    $scope.products = [''];

    $scope.preparationSteps = [{
        Minutes:0,
        StepNumber: 1,
        Text: ''
    }];

    $scope.addProduct = function() {
        $scope.products.push('');
        angular.element('#add-recepie-input-products')
            .append( $compile('<input type="text" class="form-control" ng-model="products['+ ($scope.products.length - 1) +']"/>')($scope) );
    };

    $scope.addDirection = function() {
        $scope.preparationSteps.push({
            Minutes:0,
            StepNumber: 1,
            Text: ''
        });
        angular.element('#add-recepie-input-directions')
            .append( $compile('<input type="text" class="form-control myCarrier" ng-model="preparationSteps['+ ($scope.preparationSteps.length - 1) +'].Text"/> <input type="number" class="form-control-feedback myTimeInput" ng-model="preparationSteps['+ ($scope.preparationSteps.length - 1) +'].Minutes"/> ')($scope) );
    };

    $scope.saveRecipe = function(recipe, invalidForm) {
        var i;

        if (invalidForm) {
            alert('Invalid recipe info!');
            return;
        }

        // check for empty products or steps
        for (i = 0; i < $scope.products.length; i+=1) {
            if ($scope.products[i] === '') {
                $scope.products.splice(i,1);
            }
        }
        for (i = 0; i < $scope.preparationSteps.length; i+=1) {
            if ($scope.preparationSteps[i] === '') {
                $scope.preparationSteps.splice(i,1);
            }

            $scope.preparationSteps[i].StepNumber = i + 1;
        }

        recipe.Products = $scope.products;
        recipe.PreparationSteps = $scope.preparationSteps;

        requester.recipe.create(recipe).then(function(data){
            $location.path('/single-recipe/' + data);
        });
    };

    $scope.cancelEdit = function() {
        $location.path("/home" );
    }
});
