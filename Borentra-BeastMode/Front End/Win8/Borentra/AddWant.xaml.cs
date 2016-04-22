namespace Borentra
{
    using Borentra.Common;
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    /// <summary>
    /// Add Want Flyout
    /// </summary>
    public sealed partial class AddWant : SettingsFlyout
    {
        #region Members
        /// <summary>
        /// API
        /// </summary>
        private readonly ApiCore api;

        /// <summary>
        /// Want Created Event
        /// </summary>
        public event EventHandler<Want> WantCreated;

        /// <summary>
        /// View Model
        /// </summary>
        private readonly ObservableDictionary defaultViewModel = new ObservableDictionary();
        #endregion

        #region Constructors
        public AddWant(ApiCore api)
        {
            this.api = api;

            this.InitializeComponent();
        }
        #endregion

        #region Properties
        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Save Click
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Event Arguments</param>
        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            this.Save.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

            var img = this.images.SelectedItem as Borentra.Models.Image;
            var imageUrl = null != img && !string.IsNullOrWhiteSpace(img.Url) ? img.Url : null;
            var want = new WantEdit()
            {
                Identifier = Guid.NewGuid(),
                Title = this.StringFromRichTextBox(this.Title),
                Description = this.StringFromRichTextBox(this.Description),
                ForFree = true,
                ImageUrl = imageUrl,
            };

            var saved = await this.api.SaveWant(want);

            var handle = this.WantCreated;
            if (null != handle)
            {
                handle(this, saved);
            }

            if (null != saved)
            {
                this.Title.Document.SetText(Windows.UI.Text.TextSetOptions.None, string.Empty);
                this.Description.Document.SetText(Windows.UI.Text.TextSetOptions.None, string.Empty);
                this.defaultViewModel["Images"] = null;
            }

            this.Save.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private async void Title_LostFocus(object sender, RoutedEventArgs e)
        {
            var term = this.StringFromRichTextBox(this.Title);
            if (!string.IsNullOrWhiteSpace(term) && 5 < term.Length)
            {
                this.defaultViewModel["Images"] = await this.api.Images(term);
            }
        }
        string StringFromRichTextBox(RichEditBox rtb)
        {
            string str;
            rtb.Document.GetText(Windows.UI.Text.TextGetOptions.None, out str);

            return string.IsNullOrWhiteSpace(str) ? null : str.TrimEnd('\r', '\n');
        }
        #endregion
    }
}