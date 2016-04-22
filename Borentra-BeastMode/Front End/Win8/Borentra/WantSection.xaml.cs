namespace Borentra
{
    using Borentra.Common;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Windows.UI.Popups;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    /// <summary>
    /// Want Section Page
    /// </summary>
    public sealed partial class WantSection : Page
    {
        #region Members
        /// <summary>
        /// Navigation Helper
        /// </summary>
        private readonly NavigationHelper navigationHelper;

        /// <summary>
        /// View Model
        /// </summary>
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// Borentra API
        /// </summary>
        private readonly ApiCore api = new ApiCore();

        /// <summary>
        /// Local Data Source
        /// </summary>
        private readonly LocalDataStoreCore data = new LocalDataStoreCore();

        /// <summary>
        /// Add Want, Flyout
        /// </summary>
        private readonly AddWant flyout;

        /// <summary>
        /// Facebook Core
        /// </summary>
        private readonly FacebookCore facebook = new FacebookCore();
        #endregion

        #region Properties
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        #endregion

        #region Constructors
        public WantSection()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += NavigationHelper_LoadState;

            this.itemListView.SelectionChanged += itemListView_SelectionChanged;

            Window.Current.SizeChanged += Window_SizeChanged;

            this.api.InvalidToken += this.SessionEnded;
            this.api.CannotConnectToServers += this.SessionEnded;

            this.flyout = new AddWant(this.api);
            this.flyout.WantCreated += this.WantCreated;
            
            this.InvalidateVisualState();
        }
        #endregion

        #region Methods
        private async void Logout_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure you would like to log out?");
            dialog.Commands.Add(new UICommand("Yes", null, 0));
            dialog.Commands.Add(new UICommand("No", null, 1));
            var result = await dialog.ShowAsync();
            if (((int)result.Id) == 0)
            {
                this.data.Set(null);
                this.api.Logout();

                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            this.RefreshWants();

            this.bottomAppBar.IsOpen = false;
        }
        private void WantCreated(object sender, Want want)
        {
            this.flyout.Hide();

            var items = this.DefaultViewModel["Items"] as IList<Want>;
            this.DefaultViewModel["Items"] = null;
            items.Add(want);
            this.DefaultViewModel["Items"] = items;
        }
        /// <summary>
        /// Session Ended, Re-Login
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Args</param>
        private void SessionEnded(object sender, EventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
        /// <summary>
        /// App Bar Click (Add, Remove)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.flyout.ShowIndependent();

            this.bottomAppBar.IsOpen = false;
        }
        private void Share_Click(object sender, RoutedEventArgs e)
        {
            if (null != this.itemListView.SelectedItem)
            {
                this.PublishStory(this.itemListView.SelectedItem as Want);
            }
        }
        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            var want = this.itemListView.SelectedItem as Want;
            if (null != want)
            {
                var content = string.Format("Are you sure you would like to remove: '{0}'?", want.Title);
                var dialog = new MessageDialog(content);
                dialog.Commands.Add(new UICommand("Yes", null, 0));
                dialog.Commands.Add(new UICommand("No", null, 1));
                var result = await dialog.ShowAsync();
                if (((int)result.Id) == 0)
                {
                    if (null != want)
                    {
                        this.api.DeleteWant(want.Identifier);
                        var items = this.DefaultViewModel["Items"] as List<Want>;
                        items.RemoveAt(this.itemListView.SelectedIndex);
                        this.DefaultViewModel["Items"] = null;
                        this.DefaultViewModel["Items"] = items;
                    }
                }
            }

            this.bottomAppBar.IsOpen = false;
        }
        void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.UsingLogicalPageNavigation())
            {
                this.InvalidateVisualState();
                this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
            }
        }
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.RefreshWants();

            var facebook = this.data.GetFacebookInfo();
            if (null != facebook)
            {
                this.ProfilePicture.AccessToken = facebook.AccessToken;
                this.ProfilePicture.ProfileId = facebook.ProfileId;
            }
        }
        private async void RefreshWants()
        {
            var data = await api.MyWants();
            this.DefaultViewModel["Items"] = data;

            if (null == data || 0 == data.Count())
            {
                this.flyout.ShowIndependent();
            }
        }
        #endregion

        #region Logical page navigation

        // The split page isdesigned so that when the Window does have enough space to show
        // both the list and the dteails, only one pane will be shown at at time.
        //
        // This is all implemented with a single physical page that can represent two logical
        // pages.  The code below achieves this goal without making the user aware of the
        // distinction.

        private const int MinimumWidthForSupportingTwoPanes = 768;

        /// <summary>
        /// Invoked to determine whether the page should act as one logical page or two.
        /// </summary>
        /// <returns>True if the window should show act as one logical page, false
        /// otherwise.</returns>
        private bool UsingLogicalPageNavigation()
        {
            return Window.Current.Bounds.Width < MinimumWidthForSupportingTwoPanes;
        }

        /// <summary>
        /// Invoked with the Window changes size
        /// </summary>
        /// <param name="sender">The current Window</param>
        /// <param name="e">Event data that describes the new size of the Window</param>
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            this.InvalidateVisualState();
        }
        private void InvalidateVisualState()
        {
            var visualState = DetermineVisualState();
            VisualStateManager.GoToState(this, visualState, false);
            this.navigationHelper.GoBackCommand.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Invoked to determine the name of the visual state that corresponds to an application
        /// view state.
        /// </summary>
        /// <returns>The name of the desired visual state.  This is the same as the name of the
        /// view state except when there is a selected item in portrait and snapped views where
        /// this additional logical page is represented by adding a suffix of _Detail.</returns>
        private string DetermineVisualState()
        {
            if (!UsingLogicalPageNavigation())
            {
                return "PrimaryView";
            }

            // Update the back button's enabled state when the view state changes
            var logicalPageBack = this.UsingLogicalPageNavigation() && this.itemListView.SelectedItem != null;

            return logicalPageBack ? "SinglePane_Detail" : "SinglePane";
        }

        #endregion

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        #region Temp
        private async void PublishStory(Want want)
        {
            await this.loginButton.RequestNewPermissions("publish_stream");

            var shareMessage = await this.facebook.Post(want);

            var diag = new MessageDialog(shareMessage);
            await diag.ShowAsync();
        }
        #endregion
    }
}