namespace Borentra.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class TestSignUp : IKey, IFacebookIdentity
    {
        #region Properties
        public string Name
        {
            get;
            set;
        }
        public MarketTest Type
        {
            get;
            set;
        }
        public DateTime CreatedOn
        {
            get;
            set;
        }
        public string DisplayName
        {
            get;
            set;
        }
        public long FacebookId
        {
            get;
            set;
        }
        public string Key
        {
            get;
            set;
        }
        public string Email
        {
            get;
            set;
        }
        public Guid? UserIdentifier
        {
            get;
            set;
        }
        #endregion
    }
}