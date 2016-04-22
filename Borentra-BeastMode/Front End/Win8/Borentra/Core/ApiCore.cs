namespace Borentra.Core
{
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ApiCore
    {
        #region Members
        /// <summary>
        /// HTTP Core
        /// </summary>
        private readonly HttpCore http = new HttpCore();

        /// <summary>
        /// Local Data Store Core
        /// </summary>
        private readonly LocalDataStoreCore localData = new LocalDataStoreCore();

        /// <summary>
        /// Api URL Format
        /// </summary>
        private const string apiUrlFormat = "http://api.borentra.com{0}";

        /// <summary>
        /// Invalid Token
        /// </summary>
        public event EventHandler InvalidToken;

        /// <summary>
        /// No Internet Connection
        /// </summary>
        public event EventHandler CannotConnectToServers;
        #endregion

        #region Methods
        private void SessionEnded()
        {
            var handle = this.InvalidToken;
            if (null != handle)
            {
                handle(this, EventArgs.Empty);
            }
        }
        private void NoInternetConnection()
        {
            var handle = this.CannotConnectToServers;
            if (null != handle)
            {
                handle(this, EventArgs.Empty);
            }
        }
        public async Task<Token> Login(Registration registration)
        {
            Token t = null;
            if (null != registration)
            {
                registration.DeviceIdentifier = localData.DeviceIdentifier();

                try
                {
                    t = await http.Post<Token>(string.Format(apiUrlFormat, "/account/login"), registration);
                }
                catch (UnauthorizedAccessException)
                {
                    this.SessionEnded();
                }
                catch
                {
                    this.NoInternetConnection();
                }

                if (null != t)
                {
                    this.localData.AccessToken(t);
                }
            }

            return t;
        }

        public async Task<Profile> SaveProfile(double latitude, double longitude)
        {
            var token = this.localData.AccessToken();
            var p = new ProfileEdit()
            {
                Latitude = latitude,
                Longitude = longitude,
                AccessToken = token.AccessToken,
            };

            var url = string.Format(apiUrlFormat, "/profile/save");
            Profile profile = null;
            try
            {
                profile = await this.http.Post<Profile>(url, p);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return profile;
        }

        public void Logout()
        {
            var token = this.localData.AccessToken();

            if (null != token)
            {
                try
                {
                    http.Post(string.Format(apiUrlFormat, "/account/logout"), token);
                    this.localData.AccessToken(null);
                }
                catch (UnauthorizedAccessException)
                {
                    this.SessionEnded();
                }
                catch
                {
                    this.NoInternetConnection();
                }
            }
        }

        public async Task<Profile> MyProfile()
        {
            var token = this.localData.AccessToken();
            var url = string.Format(apiUrlFormat, string.Format("/profile/My?token={0}", token.AccessToken));
            try
            {
                return await this.http.Get<Profile>(url);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return null;
        }

        public async Task<IEnumerable<Want>> MyWants()
        {
            var token = this.localData.AccessToken();
            var url = string.Format(apiUrlFormat, string.Format("/want/My?token={0}", token.AccessToken));

            try
            {
                return await this.http.Get<List<Want>>(url);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return null;
        }
        public async Task<bool> TokenIsValid()
        {
            var token = this.localData.AccessToken();
            if (null != token && !string.IsNullOrWhiteSpace(token.AccessToken))
            {
                var url = string.Format(apiUrlFormat, string.Format("/account/valid?token={0}", token.AccessToken));

                try
                {
                    await this.http.Get(url);
                    return true;
                }
                catch
                {
                    this.SessionEnded();
                }
            }

            return false;
        }
        public async Task<IEnumerable<Activity>> Activity()
        {
            var token = this.localData.AccessToken();
            var url = string.Format(apiUrlFormat, string.Format("/social/SearchActivity?token={0}", token.AccessToken));
            
            try
            {
                return await this.http.Get<List<Activity>>(url);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return null;
        }
        public async void DeleteWant(Guid identifer)
        {
            var want = new WantEdit()
            {
                Identifier = identifer,
                Delete = true,
            };

            try
            {
                await this.SaveWant(want);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }
        }
        public async Task<Want> SaveWant(WantEdit want)
        {
            want.AccessToken = this.localData.AccessToken().AccessToken;
            var url = string.Format(apiUrlFormat, "/want/save");

            try
            {
                return await this.http.Post<Want>(url, want);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return null;
        }
        public async Task<IEnumerable<Image>> Images(string term)
        {
            var token = this.localData.AccessToken().AccessToken;
            var url = string.Format(apiUrlFormat, string.Format("/search/publicimages?token={0}&s={1}&limit=2", token, term));

            try
            {
                return await this.http.Get<List<Image>>(url);
            }
            catch (UnauthorizedAccessException)
            {
                this.SessionEnded();
            }
            catch
            {
                this.NoInternetConnection();
            }

            return null;
        }
        #endregion
    }
}