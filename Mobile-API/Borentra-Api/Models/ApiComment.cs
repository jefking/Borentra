namespace Borentra.API.Models
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ApiComment
    {
        #region Properties
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsMine { get; set; }
        public string OwnerKey { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPicture
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static ApiComment Map(SocialComment c)
        {
            var comment = c.Map<ApiComment>();
            comment.OwnerPicture = FacebookCore.Picture(c.OwnerFacebookId).ToString();
            return comment;
        }
        #endregion
    }
}