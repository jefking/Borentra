namespace Borentra.DataAccessLayer
{
    using Borentra.Models;
    using System;

    public class Device : IUserIdentifier, IFacebookEntity, IIdentifier
    {
        #region Constructors
        public Device()
        {
            var random = new Random();
            this.Amplitude = random.Next();
            this.VerticalOffset = random.Next();
            this.AngularFrequency = random.Next();
            this.PhaseShift = random.Next();

            this.KeyExpiresOn = DateTime.Now.AddHours(3);
        }
        #endregion

        #region Properties
        public DateTime KeyExpiresOn
        {
            get;
            set;
        }
        public int Amplitude
        {
            get;
            set;
        }
        public int VerticalOffset
        {
            get;
            set;
        }
        public int AngularFrequency
        {
            get;
            set;
        }
        public int PhaseShift
        {
            get;
            set;
        }
        public Guid UserIdentifier
        {
            get;
            set;
        }
        public Guid DeviceIdentifier
        {
            get;
            set;
        }
        public string FacebookAccessToken
        {
            get;
            set;
        }

        public DateTime FacebookTokenExpiration
        {
            get;
            set;
        }
        public DateTime LastValidatedOn
        {
            get;
            set;
        }
        public long FacebookId
        {
            get;
            set;
        }
        public bool FacebookIsValidated
        {
            get;
            set;
        }
        public MobileOS OperatingSystem
        {
            get;
            set;
        }
        public string IpAddress
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