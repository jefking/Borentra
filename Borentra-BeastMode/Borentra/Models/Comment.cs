namespace Borentra.Models
{
    using System;

    public class Comment : IIdentifier
    {
        #region Properties
        public string Body
        {
            get;
            set;
        }

        public DateTime On
        {
            get;
            set;
        }

        public bool Read
        {
            get;
            set;
        }

        public Guid ParentConversationIdentifier
        {
            get;
            set;
        }

        public Guid FromUserIdentifier
        {
            get;
            set;
        }

        public string FromDisplayName
        {
            get;
            set;
        }

        public long FromFacebookId
        {
            get;
            set;
        }

        public string FromKey
        {
            get;
            set;
        }

        public Guid ToUserIdentifier
        {
            get;
            set;
        }

        public string ToDisplayName
        {
            get;
            set;
        }

        public long ToFacebookId
        {
            get;
            set;
        }

        public string ToKey
        {
            get;
            set;
        }

        public Guid Identifier
        {
            get;
            set;
        }
        #endregion
    }
}