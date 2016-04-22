namespace Borentra.API.Models
{
    using Borentra.Core;
    using Borentra.Models;
    using System;

    public class ApiActivity
    {
        #region Properties
        public bool CallerFavorited { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
        public string Thumbnail
        {
            get;
            set;
        }

        public string LargeImage
        {
            get;
            set;
        }
        public DateTime ModifiedOn { get; set; }
        public Guid? ReferenceIdentifier { get; set; }
        public string ReferenceKey { get; set; }
        public string Text { get; set; }
        public int Type { get; set; }
        public int UserContext { get; set; }
        public string UserDisplayName { get; set; }
        public string UserKey { get; set; }
        public string UserPicture
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static ApiActivity Map(Activity r)
        {
            var activity = r.Map<ApiActivity>();
            activity.LargeImage = ImageCore.LargeCdn(r.ImagePathFormat);
            activity.Thumbnail = ImageCore.ThumbnailCdn(r.ImagePathFormat);
            activity.UserPicture = FacebookCore.Picture(r.UserFacebookId).ToString();
            return activity;
        }
        #endregion
    }
}