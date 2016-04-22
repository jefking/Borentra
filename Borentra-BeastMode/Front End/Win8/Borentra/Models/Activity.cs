namespace Borentra.Models
{
    using System;

    public class Activity
    {
        #region Properties
        public bool CallerFavorited { get; set; }
        public int CommentCount { get; set; }
        public int FavoriteCount { get; set; }
        public DateTime ModifiedOn { get; set; }
        public Guid? ReferenceIdentifier { get; set; }
        public string ReferenceKey { get; set; }
        public int Type { get; set; }
        public int UserContext { get; set; }
     
        public string UserKey { get; set; }
        public string Image
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.Thumbnail))
                {
                    return string.Format("{0}?height=100&width=100", this.UserPicture);
                }
                else
                {
                    return this.Thumbnail;
                }
            }
        }
        public string Thumbnail
        {
            get;
            set;
        }
        public string Text { get; set; }
        public string UserDisplayName { get; set; }
        public string UserPicture
        {
            get;
            set;
        }
        #endregion
    }
}