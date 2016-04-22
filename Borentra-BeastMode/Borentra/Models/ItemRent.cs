namespace Borentra.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ItemRent : IIdentifier, IRental
    {
        #region Properties
        public Guid Identifier
        {
            get;
            set;
        }

        public Guid ItemIdentifier
        {
            get;
            set;
        }

        public decimal Price
        {
            get;
            set;
        }

        public RentalUnit PerUnit
        {
            get;
            set;
        }
        #endregion
    }
}