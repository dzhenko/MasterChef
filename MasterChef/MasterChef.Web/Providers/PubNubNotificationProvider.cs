namespace MasterChef.Web.Providers
{
    using System;

    using PubNubMessaging.Core;

    public class PubNubNotificationProvider : INotificationProvider
    {
        private const string Channel = "MasterChef";
        private const string publishKey = "pub-c-15409870-ef42-4da1-bc2d-4211862362f3";
        private const string subscribekey = "sub-c-e6269c5c-3d90-11e4-87bf-02ee2ddab7fe";

        private static PubNubNotificationProvider instance;

        private static Pubnub pubnub;

        private PubNubNotificationProvider()
        {
            pubnub = new Pubnub(publishKey, subscribekey);
        }

        public static PubNubNotificationProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PubNubNotificationProvider();
                }

                return instance;
            }
        }

        public static string LastMessage { get; private set; }

        public static string LastError { get; private set; }

        public void Notify(string notification)
        {
            pubnub.Publish(Channel, notification, (x) => LastMessage = x.ToString(), (e) => LastError = e.ToString());
        }
    }
}