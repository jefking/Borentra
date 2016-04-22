namespace Borentra.DataAccessLayer.Admin
{

    public class DeviceCount : CountByDate
    {
        #region Properties
        public override int Count
        {
            get
            {
                return this.AppleCount + this.WindowsCount + this.UnknownCount;
            }
        }
        public int AppleCount
        {
            get;
            set;
        }
        public int WindowsCount
        {
            get;
            set;
        }
        public int UnknownCount
        {
            get;
            set;
        }
        #endregion
    }
}