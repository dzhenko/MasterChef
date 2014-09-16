(function () {
    var pubnub;

    initializeRoom();
    subscribe(function (m) {
        console.log(m);
    });

    function initializeRoom() {
        pubnub = PUBNUB.init({
            publish_key: 'demo',
            subscribe_key: 'demo'
        });
    }

    function subscribe(printFunction) {
        pubnub.subscribe({
            channel: 'my-notifications',
            message: function (m) {
                console.log(m);
            }
        });
    }
}());