namespace Borentra
{
    using Borentra.Common;
    using Borentra.Core;
    using Borentra.Models;
    using Facebook.Client.Controls;
    using System;
    using Windows.Devices.Geolocation;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Members
        /// <summary>
        /// API Core
        /// </summary>
        private readonly ApiCore api = new ApiCore();

        /// <summary>
        /// Local Data Store
        /// </summary>
        private readonly LocalDataStoreCore data = new LocalDataStoreCore();

        /// <summary>
        /// Navigation Helper
        /// </summary>
        private NavigationHelper navigationHelper;

        /// <summary>
        /// Geo Locator
        /// </summary>
        private readonly Geolocator locator = new Geolocator();

        /// <summary>
        /// Profile Core
        /// </summary>
        private readonly ProfileCore profileCore = new ProfileCore();
        #endregion

        #region Constructors
        public MainPage()
        {
            this.InitializeComponent();
            this.api.CannotConnectToServers += api_CannotConnectToServers;
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }
        #endregion

        #region Methods
        private void CheckConnection_Click(object sender, RoutedEventArgs e)
        {
            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                this.checkConnection.Visibility =
                    this.Loading.Visibility = Visibility.Collapsed;
                this.loginButton.Visibility = Visibility.Visible;
            }
        }

        private async void api_CannotConnectToServers(object sender, System.EventArgs e)
        {
            var dialog = new MessageDialog("No internet connection is available");
            dialog.Commands.Add(new UICommand("Ok", null, 0));
            await dialog.ShowAsync();
            
            this.Loading.Visibility = Visibility.Collapsed;
            this.loginButton.Visibility = Visibility.Collapsed;
            this.checkConnection.Visibility = Visibility.Visible;
        }

        private async void OnSessionStateChanged(object sender, SessionStateChangedEventArgs e)
        {
            switch (e.SessionState)
            {
                case FacebookSessionState.Opened:
                    this.DisplayLoading(true);

                    try
                    {
                        var registration = new Registration()
                        {
                            FacebookAccessToken = this.loginButton.CurrentSession.AccessToken,
                            FacebookTokenExpiration = this.loginButton.CurrentSession.Expires,
                        };
                        
                        var token = await this.api.Login(registration);

                        if (null != token)
                        {
                            var fb = new FacebookInfo()
                            {
                                AccessToken = this.loginButton.CurrentSession.AccessToken,
                                ProfileId = this.loginButton.CurrentSession.FacebookId,
                            };

                            this.data.Set(fb);

                            this.GeoLocation();

                            this.Frame.Navigate(typeof(WantSection));
                        }
                    }
                    catch
                    {
                        this.api_CannotConnectToServers(this, EventArgs.Empty);
                    }
                    break;
                case FacebookSessionState.Closed:
                    this.api.Logout();
                    break;
            }

            this.DisplayLoading(false);
        }

        public void loginButton_AuthenticationError(object sender, AuthenticationErrorEventArgs e)
        {
            this.Loading.Visibility = 
                this.loginButton.Visibility = Visibility.Collapsed;
            this.checkConnection.Visibility = Visibility.Visible;
        }

        private void DisplayLoading(bool loading)
        {
            this.loginButton.Visibility = loading ? Visibility.Collapsed : Visibility.Visible;
            this.Loading.Visibility = loading ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion

        #region NavigationHelper registration
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.DisplayLoading(true);

            if (System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable())
            {
                var tokenIsValid = await this.api.TokenIsValid();
                if (tokenIsValid)
                {
                    this.Frame.Navigate(typeof(WantSection));
                }
                else
                {
                    this.DisplayLoading(false);
                }
            }
            else
            {
                this.api_CannotConnectToServers(this, EventArgs.Empty);
            }
        }

        private async void GeoLocation()
        {
            try
            {
                var pos = await locator.GetGeopositionAsync();

                this.profileCore.Update(pos);
            }
            catch
            {
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        #endregion
    }
}