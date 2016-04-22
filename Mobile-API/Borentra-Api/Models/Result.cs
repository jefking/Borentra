namespace Borentra.API.Models
{
    using Borentra.Models;
    using System;

    public class Result
    {
        #region Properties
        public DateTime CreatedOn { get; set; }
        public string Description { get; set; }
        public string Key { get; set; }
        public string Location { get; set; }
        public string MemberName { get; set; }
        public string Thumbnail { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        #endregion

        #region Methdods
        public static Result Map(SearchResult r)
        {
            return r.Map<Result>();
        }
        #endregion
    }
}