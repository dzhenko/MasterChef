recipesApp.controller('HomeController', function HomeController($scope, $compile) {
    'use strict';

    $scope.welcome = 'Recent events:';

    $scope.imgSrc = 'http://www-tc.pbs.org/food/wp-content/blogs.dir/2/files/2012/12/Year-in-Food-2012-Recipes-Feat-602x338.jpg';

    function onMessageReceived(message) {
        var hrefLink = message.substr(0, 36);
        message = message.substr(39);

        angular.element('#newEventsHolder')
            .append( $compile('<a href="#/single-recipe/'+hrefLink+'" class="list-group-item">' + message + '</a>')($scope) );
    }
    var subscribeKey = 'sub-c-e6269c5c-3d90-11e4-87bf-02ee2ddab7fe';
    var chanelName = 'MasterChef';

    var pubnub = PUBNUB.init({
        subscribe_key: subscribeKey
    });

    pubnub.subscribe({
        channel: chanelName,
        message: onMessageReceived
    });
});