namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Facebook;
    using Borentra.Models;
    using Facebook;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Facebook Actions
    /// </summary>
    public class FacebookCore
    {
        #region Members
        /// <summary>
        /// Application Identifier
        /// </summary>
        public const string ApplicationId = "315854161864272";

        /// <summary>
        /// Application Secret
        /// </summary>
        public const string ApplicationSecret = "c998e3cc5a9c250f19ea593c3e9f42a6";

        /// <summary>
        /// Facebook Picture Format
        /// </summary>
        public const string FacebookPictureFormat = "http://graph.facebook.com/{0}/picture";

        /// <summary>
        /// Facebook User Format
        /// </summary>
        public const string FacebookUserFormat = "http://graph.facebook.com/me";

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();
        #endregion

        #region Methods
        public MeInfo UserInfoSafe(IFacebookAccess fbAccess)
        {
            MeInfo result = null;
            try
            {
                result = this.UserInfo(fbAccess);
            }
            catch (FacebookOAuthException)
            {
                //Logging Needed
            }
            catch
            {
                //Logging Needed
            }

            return result;
        }

        public MeInfo UserInfo(IFacebookAccess fbAccess)
        {
            if (null == fbAccess)
            {
                throw new ArgumentNullException("facebook access");
            }

            if (string.IsNullOrWhiteSpace(fbAccess.FacebookAccessToken))
            {
                throw new ArgumentException("access token");
            }

            var client = this.Client(fbAccess.FacebookAccessToken);
            dynamic result = client.Get("me?fields=name,id,email");
            return new MeInfo()
            {
                FacebookId = long.Parse(result.id),
                Name = result.name,
                Email = result.email,
                FacebookAccessToken = fbAccess.FacebookAccessToken,
                FacebookTokenExpiration = fbAccess.FacebookTokenExpiration,
            };
        }

        /// <summary>
        /// Load Facebook Client
        /// </summary>
        /// <param name="accessToken">User Access Token</param>
        /// <returns>Facebook Client</returns>
        public FacebookClient Client(string accessToken)
        {
            return new FacebookClient(accessToken)
            {
                AppId = FacebookCore.ApplicationId,
                AppSecret = FacebookCore.ApplicationSecret,
            };
        }

        /// <summary>
        /// Users Location
        /// </summary>
        /// <param name="accessToken">User Access Token</param>
        /// <returns>Location</returns>
        public string Location(string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                try
                {
                    var client = this.Client(accessToken);
                    dynamic me = client.Get("me", null);
                    return me.location.name.ToString();
                }
                catch (FacebookOAuthException)
                {
                    //Logging Needed
                }
                catch
                {
                    // We need logging here.
                }
            }
            return null;
        }

        //public DateTime TokenExpiresOn(string accessToken)
        //{
        //    var client = this.Client(accessToken);
        //    dynamic result = client.Get("debug_token", new
        //    {
        //        input_token = "{input-token} ",
        //        access_token = "{access-token}"
        //    });
        //    return DateTime.Now;
        //}

        /// <summary>
        /// Users Friends
        /// </summary>
        /// <param name="accessToken">User Access Token</param>
        /// <returns>Friends</returns>
        public List<Friend> Friends(string accessToken)
        {
            var friends = new List<Friend>();

            try
            {
                var client = this.Client(accessToken);
                dynamic data = client.Get("me/friends", null);
                foreach (dynamic friend in data.data)
                {
                    var id = 0;
                    if (int.TryParse(friend.id, out id))
                    {
                        friends.Add(new Friend() { Identifier = id });
                    }
                }
            }
            catch (FacebookOAuthException)
            {
                //Logging Needed
            }
            catch
            {
                // We need logging here.
            }

            return friends;
        }

        /// <summary>
        /// Save Facebook Connection
        /// </summary>
        /// <param name="connection">Connection</param>
        /// <returns>Friend Email</returns>
        public FriendEmail Save(Connection connection)
        {
            if (null == connection)
            {
                throw new ArgumentNullException("connection");
            }
            if (0 >= connection.FacebookId)
            {
                throw new ArgumentException("facebook identifier");
            }
            if (Guid.Empty == connection.OwnerIdentifier)
            {
                throw new ArgumentException("owner identifier");
            }

            var sproc = new SocialSaveFacebookConnection()
            {
                FacebookId = connection.FacebookId,
                OwnerIdentifier = connection.OwnerIdentifier,
            };

            try
            {
                return sproc.CallObject<FriendEmail>();
            }
            catch
            {
                // We need logging
            }

            return null;
        }

        /// <summary>
        /// Import Friends
        /// </summary>
        /// <param name="profile">Profile</param>
        public IList<FriendEmail> ImportFriends(ProfileFull profile)
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }
            if (string.IsNullOrWhiteSpace(profile.FacebookAccessToken))
            {
                throw new ArgumentException("facebook access token");
            }
            if (Guid.Empty == profile.Identifier)
            {
                throw new ArgumentException("profile identifier");
            }

            var emails = new List<FriendEmail>();
            var friends = this.Friends(profile.FacebookAccessToken);
            foreach (var friend in friends)
            {
                var connection = new Connection()
                {
                    FacebookId = friend.Identifier,
                    OwnerIdentifier = profile.Identifier,
                };

                var email = this.Save(connection);
                if (null != email)
                {
                    activityCore.GotAFriend(email);
                    emails.Add(email);
                }
            }

            return emails;
        }

        /// <summary>
        /// Facebook Image
        /// </summary>
        /// <param name="facebookIdentifier">Facebook Identifier</param>
        /// <returns></returns>
        public static Uri Picture(long facebookIdentifier)
        {
            return new Uri(string.Format(FacebookCore.FacebookPictureFormat, facebookIdentifier));
        }
        #endregion
    }
}