namespace Borentra.Models
{
    using System.Collections.Generic;

    public class LeaderBoard
    {
        #region Properties
        public IEnumerable<Leader> Community
        {
            get;
            set;
        }
        public IEnumerable<Leader> World
        {
            get;
            set;
        }
        #endregion
    }
}