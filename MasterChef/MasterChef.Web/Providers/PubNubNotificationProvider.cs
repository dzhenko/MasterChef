namespace MasterChef.Web.Providers
{
    public class PubNubNotificationProvider : INotificationProvider
    {
        private static PubNubNotificationProvider instance;

        private PubNubNotificationProvider()
        {
            // initialize 
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

        public void Notify(string notification)
        {
            // TODO: write the string in the room
        }
    }
}