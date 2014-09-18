recipesApp.controller('HomeController', function HomeController($scope, $compile) {
    'use strict';

    $scope.welcome = 'Recent events:';

    $scope.imgSrc = 'http://www-tc.pbs.org/food/wp-content/blogs.dir/2/files/2012/12/Year-in-Food-2012-Recipes-Feat-602x338.jpg';

    function onMessageRecieved(message) {
        var hrefLink = message.substr(0, 40);
        message = message.substr(43);

        angular.element('#newEventsHolder')
            .append( $compile('<a href="'+hrefLink+'" class="list-group-item">' + message + '</a>')($scope) );
    }

    var subscribeKey = 'testSubscribeKeyValue';
    var chanelName = 'testSubscribeChanelName';

    var pubnub = PUBNUB.init({
        //publish_key: publishKey,
        subscribe_key: subscribeKey
    });

    pubnub.subscribe({
        channel: chanelName,
        message: onMessageRecieved
    });
});