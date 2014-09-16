(function () {
    var $wrapper = $('body');
    var chanelName;
    var printingFunction = function (m) {
        console.log(m);
    };
    var pubnub;

    $wrapper.on('click', '#start-recipie', function () {
        // GET subscribeKey and chanelName from server

        // Start listening
        //TODO skip PublishKey for recipie notification
        //var publishKey = 'demo';
        var subscribeKey = 'demo';
        chanelName = 'my-notifications';

        pubnub = PUBNUB.init({
            //publish_key: publishKey,
            subscribe_key: subscribeKey
        });

        pubnub.subscribe({
            channel: chanelName,
            message: printingFunction
        });
    });

    $wrapper.on('click', '#finish-recipie', function () {
        if (chanelName != null) {
            pubnub.unsubscribe({
                channel: chanelName
            });
        }

        //Send termination request to server
    });
}());
