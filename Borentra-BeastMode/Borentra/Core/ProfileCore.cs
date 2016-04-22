namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Profile Core
    /// </summary>
    public class ProfileCore
    {
        #region Members
        /// <summary>
        /// Administrator Identifier
        /// </summary>
        public static readonly Guid AdministratorIdentifier = Guid.Parse("BA8B47BD-63CE-40B7-B68E-03D2320AA279");

        /// <summary>
        /// Activity Core
        /// </summary>
        private readonly ActivityCore activityCore = new ActivityCore();
        #endregion

        #region Methods
        /// <summary>
        /// Save
        /// </summary>
        /// <param name="profile">Profile</param>
        /// <returns>Profile</returns>
        public Profile Save(Profile profile, bool publishActivity = true, string facebookAccessToken = null, MobileOS os = MobileOS.Unknown)
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }
            
            if (Guid.Empty == profile.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var original = this.SearchSingle(profile.Identifier, null, profile.Identifier);

            var sp = new UserSaveProfile()
            {
                UserIdentifier = profile.Identifier,
                Status = profile.Status.TrimIfNotNull(),
                DisplayName = profile.Name.TrimIfNotNull(),
                Email = profile.Email.TrimIfNotNull(),
                Location = profile.Location.TrimIfNotNull(),
                PrivacyLevel = (byte?)profile.PrivacyLevel,
                Longitude = profile.Longitude == 0 || profile.Latitude == 0? (double?)null : profile.Longitude,
                Latitude = profile.Longitude == 0 || profile.Latitude == 0 ? (double?)null : profile.Latitude,
                SearchRadius = profile.SearchRadius == 0 ? (int?)null : profile.SearchRadius,
                IpAddress = profile.IpAddress.TrimIfNotNull(),
                FacebookAccessToken = facebookAccessToken.TrimIfNotNull(),
            };

            var data = sp.Execute().LoadObject<Profile>();

            if (publishActivity)
            {
                if (!string.IsNullOrWhiteSpace(profile.Status)
                    && original.Status != data.Status)
                {
                    this.activityCore.StatusUpdate(data.Identifier, data.Status);
                }
                else
                {
                    this.activityCore.ProfileUpdate(data.Identifier, os);
                }
            }

            return data;
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="callerId"></param>
        /// <returns>Profile</returns>
        public Profile Search(Profile profile, Guid? callerId = null, bool isAdmin = false)
        {
            return this.Search<Profile>(profile, callerId, isAdmin);
        }

        /// <summary>
        /// Search
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="profile"></param>
        /// <param name="callerId"></param>
        /// <returns></returns>
        public T Search<T>(T profile, Guid? callerId = null, bool isAdmin = false)
            where T : Profile, new()
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }

            if (Guid.Empty == profile.Identifier
                && string.IsNullOrWhiteSpace(profile.Key))
            {
                throw new ArgumentException("Not unique");
            }

            callerId = !callerId.HasValue || callerId.Value == Guid.Empty ? (Guid?)null : callerId.Value;

            var sp = new UserSearchProfile()
            {
                Identifier = profile.Identifier == Guid.Empty ? (Guid?)null : profile.Identifier,
                Key = profile.Key.TrimIfNotNull(),
                Top = 1,
                CallerIdentifier = isAdmin ? AdministratorIdentifier : callerId,
            };

            return sp.Execute().LoadObject<T>();
        }

        public IEnumerable<Profile> Search(string searchTerm, Guid? userId = null, Guid? callerId = null, bool isAdmin = false, short top = 100, int? radius = null)
        {
            return this.Search<Profile>(searchTerm, userId, callerId, isAdmin, top, radius);
        }

        public IEnumerable<T> Search<T>(string searchTerm, Guid? userId = null, Guid? callerId = null, bool isAdmin = false, short top = 100, int? radius = null)
            where T : Profile, new()
        {
            callerId = !callerId.HasValue || callerId.Value == Guid.Empty ? (Guid?)null : callerId.Value;
            userId = !userId.HasValue || userId.Value == Guid.Empty ? (Guid?)null : userId.Value;

            var proc = new UserSearchProfile()
            {
                Keyword = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm,
                ConnectionIdentifier = userId,
                Top = top,
                CallerIdentifier = isAdmin ? AdministratorIdentifier : callerId,
                Radius = radius.HasValue && radius == 0 ? (int?)null : radius,
            };

            return proc.CallObjects<T>();
        }

        public Profile SearchSingle(Guid? id, string key = null, Guid? callerId = null, bool isAdmin = false)
        {
            return this.SearchSingle<Profile>(id, key, callerId, isAdmin);
        }

        public T SearchSingle<T>(Guid? id, string key = null, Guid? callerId = null, bool isAdmin = false)
            where T : Profile, new()
        {
            callerId = !callerId.HasValue || callerId.Value == Guid.Empty ? (Guid?)null : callerId.Value;
            id = !id.HasValue || id.Value == Guid.Empty ? (Guid?)null : id.Value;

            var proc = new UserGetProfile()
            {
                Identifier = id,
                CallerIdentifier = isAdmin ? AdministratorIdentifier : callerId,
                Key = string.IsNullOrWhiteSpace(key) ? null : key,
            };

            return proc.CallObject<T>();
        }

        public IEnumerable<Profile> Friends(Profile profile, short? top = null)
        {
            if (null == profile)
            {
                throw new ArgumentNullException("profile");
            }
            if (Guid.Empty == profile.Identifier)
            {
                throw new ArgumentException("Identifier");
            }

            var sp = new UserSearchProfile()
            {
                CallerIdentifier = profile.Identifier,
                ConnectionIdentifier = profile.Identifier,
                Top = top,
            };

            return sp.CallObjects<Profile>();
        }

        public DashboardInfo<T> Load<T>(Guid userIdentifier)
        {
            if (Guid.Empty == userIdentifier)
            {
                throw new ArgumentException("User Identifier");
            }

            var sproc = new StatsDashboard()
            {
                UserIdentifier = userIdentifier,
            };

            return sproc.CallObject<DashboardInfo<T>>();
        }

        public bool SaveTheme(Guid userId, string landingTheme)
        {
            if (Guid.Empty != userId && !string.IsNullOrWhiteSpace(landingTheme))
            {
                var sproc = new UserSaveProfileArchive()
                {
                    UserIdentifier = userId,
                    LandingTheme = landingTheme.TrimIfNotNull(),
                };

                try
                {
                    sproc.ExecuteNonQuery();
                    return true;
                }
                catch
                {
                }
            }

            return false;
        }

        /// <summary>
        /// Delete Profile
        /// </summary>
        /// <param name="userId"></param>
        public void Delete(Guid userId)
        {
            if (Guid.Empty == userId)
            {
                throw new ArgumentException("userId");
            }

            var sproc = new UserSaveProfile()
            {
                Delete = true,
                UserIdentifier = userId,
            };

            sproc.ExecuteNonQuery();
        }


        /// <summary>
        /// This should be broken into a set of Async Actions
        /// </summary>
        /// <param name="profile">Profile Full</param>
        public async Task Register(ProfileFull profile)
        {
            try
            {
                if (null != profile)
                {
                    var emailCore = new EmailCore();

                    emailCore.NewUserGreeting(profile);

                    if (!string.IsNullOrWhiteSpace(profile.IpAddress))
                    {
                        var geoCore = new GeoCore();
                        var location = await geoCore.GetGeoFromIp(profile.IpAddress);
                        if (null != location)
                        {
                            var p = new Profile()
                            {
                                Identifier = profile.Identifier,
                                Latitude = location.Latitude,
                                Longitude = location.Longitude,
                                Location = location.Location,
                                IpAddress = location.IPAddress.TrimIfNotNull(),
                            };

                            this.Save(p, false);
                            profile.Location = p.Location.TrimIfNotNull(); // So we don't get FB's if it is valid
                        }
                    }

                    activityCore.Joined(profile.Identifier, profile.Location);

                    if (!string.IsNullOrWhiteSpace(profile.FacebookAccessToken))
                    {
                        var facebookCore = new FacebookCore();
                        var emails = facebookCore.ImportFriends(profile);
                        foreach (var email in emails)
                        {
                            emailCore.FriendSignedUp(email);
                        }
                    }

                    //this.converstationCore.NewUserGreeting(profile.Identifier, profile.Name);
                }
            }
            catch
            {
                // Logging
            }
        }

        /// <summary>
        /// Profile Base Url
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Base Url</returns>
        public static string BaseUrl(string key)
        {
            return string.Format("/profile/{0}", key.TrimIfNotNull());
        }

        /// <summary>
        /// Profile Search Url
        /// </summary>
        /// <param name="term">Key</param>
        /// <returns>Search Url</returns>
        public static string SearchUrl(string term, string category = "organic")
        {
            return string.Format("/search/member?s={0}&c={1}", term.TrimIfNotNull(), category.TrimIfNotNull());
        }
        #endregion
    }
}