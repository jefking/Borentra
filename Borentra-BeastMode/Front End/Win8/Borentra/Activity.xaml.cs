namespace Borentra
{
    using Borentra.Common;
    using Borentra.Core;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;
    using System.Linq;

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Activity : Page
    {
        #region Members
        /// <summary>
        /// API Core
        /// </summary>
        private readonly ApiCore api = new ApiCore();
        /// <summary>
        /// Navigation Helper
        /// </summary>
        private NavigationHelper navigationHelper;
        /// <summary>
        /// Data
        /// </summary>
        private readonly ObservableDictionary data = new ObservableDictionary();
        #endregion

        #region Constructor
        public Activity()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
        }
        #endregion

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary Data
        {
            get { return this.data; }
        }

        #region Methods
        #endregion

        #region NavigationHelper registration
        private async void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            this.data["Activities"] = await api.Activity();
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