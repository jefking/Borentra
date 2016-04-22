namespace Borentra.Models
{
    /// <summary>
    /// Badge
    /// </summary>
    public class Badge : IContent
    {
        #region Properties
        public string Description
        {
            get;
            set;
        }

        public string IconName
        {
            get;
            set;
        }

        public byte Points
        {
            get;
            set;
        }
        public short MembersWithBadge
        {
            get;
            set;
        }
        public string Title
        {
            get;
            set;
        }
        #endregion
    }
}