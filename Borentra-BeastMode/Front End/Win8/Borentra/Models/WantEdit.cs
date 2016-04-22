namespace Borentra.Models
{
    public class WantEdit : Want
    {
        #region Properties
        public string AccessToken
        {
            get;
            set;
        }
        public bool Delete
        {
            get;
            set;
        }

        public bool ForFree { get; set; }
        public bool ForRent { get; set; }
        public bool ForShare { get; set; }
        public bool ForTrade { get; set; }
        public string ImageUrl
        {
            get;
            set;
        }
        #endregion
    }
}