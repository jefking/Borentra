namespace Borentra.Core
{
    using Borentra.Models;
    using System;
    using System.Threading.Tasks;

    public class FacebookCore
    {
        #region Members
        private readonly LocalDataStoreCore data = new LocalDataStoreCore();
        #endregion

        public async Task<string> Post(Want want)
        {
            var fb = this.data.GetFacebookInfo();
            var facebookClient = new Facebook.FacebookClient(fb.AccessToken);

            var postParams = new
            {
                name = string.Format("In Search Of: {0}", want.Title),
                caption = string.Format("Can anyone help me find: '{0}'?", want.Title),
                description = want.Description,
                link = string.Format("http://www.borentra.com/wanted/{0}", want.Key),
                picture = want.LargeImage,
            };

            string shareMessage = null;

            try
            {
                await facebookClient.PostTaskAsync("/me/feed", postParams);
                
                shareMessage = string.Format("Successfully shared: '{0}'.", want.Title);
            }
            catch (Exception ex)
            {
                shareMessage = string.Format("Unable to post at this time: '{0}'", ex.Message);
            }

            return shareMessage;
        }
    }
}