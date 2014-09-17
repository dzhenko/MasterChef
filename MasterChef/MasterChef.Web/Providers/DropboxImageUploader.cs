namespace MasterChef.Web.Providers
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;

    using Spring.IO;
    using Spring.Social.Dropbox.Api;
    using Spring.Social.Dropbox.Connect;
    using Spring.Social.OAuth1;

    public class DropboxImageUploader : IDropboxImageUploader
    {
        // https://www.dropbox.com/home Email: zarichepower@gmail.com Password: ta_peach
        // https://www.dropbox.com/developers/apps/info/jkgqqefhutacq4n
        private const string DropboxAppKey = "jkgqqefhutacq4n";
        private const string DropboxAppSecret = "21kf0wc1d5kn38j";
        private const string OAuthTokenFileName = @"..\..\..\OAuthTokenFileName.txt";
        private const string OauthAccessTokenValue = "ra72amhofx2x56vp";
        private const string OauthAccessTokenSecret = "cf87kxku32j17mg";

        private static IDropboxImageUploader dropboxDataProvider;
        private DropboxServiceProvider dropboxServiceProvider;
        private OAuthToken oauthAccessToken;
        private WebClient client;
        private IDropbox dropbox;

        private DropboxImageUploader()
        {
            this.dropboxServiceProvider = new DropboxServiceProvider(DropboxAppKey, DropboxAppSecret, AccessLevel.Full);
            this.oauthAccessToken = new OAuthToken(OauthAccessTokenValue, OauthAccessTokenSecret); // this.oauthAccessToken = this.LoadOAuthToken();
            this.client = new WebClient();
            this.dropbox = this.dropboxServiceProvider.GetApi(this.oauthAccessToken.Value, this.oauthAccessToken.Secret);
        }

        public static IDropboxImageUploader Instance
        {
            get
            {
                if (dropboxDataProvider == null)
                {
                    dropboxDataProvider = new DropboxImageUploader();
                }

                return dropboxDataProvider;
            }
        }

        /// <summary>
        /// Uploads the image to dropbox and then gets the new url.
        /// </summary>
        /// <param name="url">Used to download the image.</param>
        /// <param name="fileName">Used as name for the image.</param>
        /// <returns>The link from dropbox</returns>
        public string UploadImageToDropbox(string url, string fileName)
        {
            this.client.DownloadFile(url, fileName);
            Entry uploadFileEntry = dropbox.UploadFileAsync(new FileResource(fileName), string.Format("/images/{0}.jpg", fileName)).Result; // TODO: check for file extensions?
            DropboxLink sharedUrl = dropbox.GetMediaLinkAsync(uploadFileEntry.Path).Result;
            File.Delete(fileName);

            return sharedUrl.Url.ToString();
        }

        #region UsedToCreateOathToken
        /*private OAuthToken LoadOAuthToken()
        {
            if (!File.Exists(OAuthTokenFileName))
            {
                this.AuthorizeAppOAuth(this.dropboxServiceProvider);
            }

            string[] lines = File.ReadAllLines(OAuthTokenFileName);
            OAuthToken oauthAccessToken = new OAuthToken(lines[0], lines[1]);

            return oauthAccessToken;
        }

        private void AuthorizeAppOAuth(DropboxServiceProvider dropboxServiceProvider)
        {
            // Authorization without callback url
            Console.Write("Getting request token...");
            OAuthToken oauthToken = dropboxServiceProvider.OAuthOperations.FetchRequestTokenAsync(null, null).Result;
            Console.WriteLine("Done.");

            OAuth1Parameters parameters = new OAuth1Parameters();
            string authenticateUrl = dropboxServiceProvider.OAuthOperations.BuildAuthorizeUrl(oauthToken.Value, parameters);
            Console.WriteLine("Redirect the user for authorization to {0}", authenticateUrl);
            Process.Start(authenticateUrl);
            Console.Write("Press [Enter] when authorization attempt has succeeded.");
            Console.ReadLine();

            Console.Write("Getting access token...");
            AuthorizedRequestToken requestToken = new AuthorizedRequestToken(oauthToken, null);
            OAuthToken oauthAccessToken = dropboxServiceProvider.OAuthOperations.ExchangeForAccessTokenAsync(requestToken, null).Result;
            Console.WriteLine("Done.");

            string[] oauthData = new string[] { oauthAccessToken.Value, oauthAccessToken.Secret };
            File.WriteAllLines(OAuthTokenFileName, oauthData);
        }*/
        #endregion
    }
}