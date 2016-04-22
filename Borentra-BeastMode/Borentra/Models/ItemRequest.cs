namespace Borentra.Models
{
    /// <summary>
    /// Item Request
    /// </summary>
    public class ItemRequest : Thing
    {
        #region Properties
        public override bool IsPublic
        {
            get
            {
                return true;
            }
        }

        public bool ForTrade
        {
            get;
            set;
        }

        public bool ForFree
        {
            get;
            set;
        }

        public bool ForRent
        {
            get;
            set;
        }

        public bool ForShare
        {
            get;
            set;
        }
        #endregion
    }
}