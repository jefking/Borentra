namespace Borentra.Models
{
    public class DashboardInfo<T> : DashboardStats
    {
        #region Properties
        public T Info
        {
            get;
            set;
        }
        #endregion
    }
}