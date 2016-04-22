namespace Borentra.DataAccessLayer.Admin
{
    using System;

    public class CountByDate
    {
        #region Properties
        public virtual int Count
        {
            get;
            set;
        }

        public DateTime CreateDate
        {
            get;
            set;
        }
        #endregion
    }
}