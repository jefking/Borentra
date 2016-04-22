namespace Borentra.WorkerRole
{
    using Borentra.Core;
    using Borentra.DataAccessLayer;
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class FacebookFriendGatherer : ScheduledManager
    {
        #region Members
        /// <summary>
        /// Reoccurrence
        /// </summary>
        private static readonly TimeSpan reoccurrence = TimeSpan.FromDays(7);
        #endregion

        #region Constructor
        public FacebookFriendGatherer()
            : base(reoccurrence.TotalSeconds)
        {
        }
        #endregion

        #region Methods
        public override void Execute()
        {
            var facebook = new FacebookCore();
            var profileCore = new ProfileCore();
            var emailCore = new EmailCore();

            var profiles = profileCore.Search(null, null, null, true, short.MaxValue, int.MaxValue);

            foreach (var profile in profiles)
            {
                try
                {
                    var user = profileCore.SearchSingle<ProfileFull>(profile.Identifier, profile.Key, null, true);
                    if (!string.IsNullOrWhiteSpace(user.FacebookAccessToken) && Guid.Empty != user.Identifier)
                    {
                        var emails = facebook.ImportFriends(user);
                        foreach (var email in emails)
                        {
                            emailCore.NewFriend(email);
                        }
                    }

                    Thread.Sleep(500);
                }
                catch (Exception ex)
                {
                    Trace.WriteLine(ex.ToString());
                }
            }
        }
        #endregion
    }
}
