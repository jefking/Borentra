namespace Borentra.API.Models
{
    using Borentra.Models;
    using System;

    public class Member
    {
        #region Properties
        public string Location
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }
        public string Status
        {
            get;
            set;
        }
        public Uri Picture
        {
            get;
            set;
        }
        
        public double Latitude
        {
            get;
            set;
        }
        public double Longitude
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public static Member Map(Profile profile)
        {
            var member = profile.Map<Member>();
            member.Picture = profile.Picture();
            return member;
        }
        #endregion
    }
}