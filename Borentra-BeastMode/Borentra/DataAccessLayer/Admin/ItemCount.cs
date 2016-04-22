namespace Borentra.DataAccessLayer.Admin
{
    public class ItemCount : CountByDate
    {
        #region Properties
        public override int Count
        {
            get
            {
                return this.RequestCount + this.OfferCount;
            }
        }
        public int RequestCount
        {
            get;
            set;
        }
        public int OfferCount
        {
            get;
            set;
        }
        #endregion
    }
}