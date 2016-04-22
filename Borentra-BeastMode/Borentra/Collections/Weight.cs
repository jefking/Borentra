namespace Borentra.Collections
{
    /// <summary>
    /// Dictionary Weight Class
    /// </summary>
    /// <typeparam name="T">Type</typeparam>
    public class Weight<T>
    {
        #region Properties
        /// <summary>
        /// Count
        /// </summary>
        public byte Count
        {
            get;
            set;
        }

        /// <summary>
        /// Item
        /// </summary>
        public T Item
        {
            get;
            set;
        }
        #endregion
    }
}