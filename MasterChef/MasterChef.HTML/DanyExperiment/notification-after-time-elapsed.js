(function () {
    var seconds = 1;
    var pubnub = PUBNUB.init({
        publish_key: 'demo',
        subscribe_key: 'demo'
    });
    var channelName = "my-notifications";
    var printFunction = function (m) {
        console.log(m);
    };

    countdown(seconds);

    function countdown(timeInSecconds) {
        var counter = setInterval(timer, 1000); //1000 will  run it every 1 second

        function timer() {

            timeInSecconds = timeInSecconds - 1;
            if (timeInSecconds <= 0) {
                clearInterval(counter);

                pubnub.publish({
                    channel: channelName,
                    message: 'Hello PubNub, love the JavaScript SDK!'
                });

                console.log("finished");
                return;
            }
        }
    }

    //You do not need to subscribe to publish
    /*pubnub.subscribe({
        channel: channelName,
        message: printFunction
    });*/

    /*pubnub.unsubscribe({
        channel: channelName,
        message: printFunction
    });*/
}());