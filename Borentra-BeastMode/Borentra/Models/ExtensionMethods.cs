namespace Borentra.Models
{
    using Borentra.Core;
    using System;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region Borentra.Models.IFacebookIdentity
        public static Uri Picture(this IFacebookIdentity identity)
        {
            return FacebookCore.Picture(identity.FacebookId);
        }
        #endregion

        #region Borentra.Models.IRental
        public static decimal Cost(this IRental rental, DateTime from, DateTime to)
        {
            switch (rental.PerUnit)
            {
                case RentalUnit.PerDay:
                    return rental.Price * from.Subtract(to).Days;
                default:
                    throw new ArgumentException("unknown unit");
            }
        }
        #endregion
    }
}