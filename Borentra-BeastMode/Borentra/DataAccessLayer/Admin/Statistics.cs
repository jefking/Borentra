namespace Borentra.DataAccessLayer.Admin
{
    using System.Collections.Generic;

    public class Statistics<T>
    {
        #region Properties
        public IEnumerable<T> Daily
        {
            get;
            set;
        }

        public IEnumerable<T> Monthly
        {
            get;
            set;
        }
        #endregion
    }
}