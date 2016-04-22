namespace Borentra.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Weighted Dictionary
    /// </summary>
    /// <typeparam name="T">Key</typeparam>
    /// <typeparam name="Y">Value</typeparam>
    public class WeightedDictionary<T, Y>
    {
        #region Members
        /// <summary>
        /// Dictionary
        /// </summary>
        private readonly IDictionary<T, Weight<Y>> dic = new Dictionary<T, Weight<Y>>();
        #endregion

        #region Methods
        /// <summary>
        /// Add Item
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="item">Item</param>
        public void Add(T key, Y item)
        {
            if (dic.ContainsKey(key))
            {
                dic[key].Count++;
            }
            else
            {
                var weight = new Weight<Y>()
                {
                    Item = item,
                };

                dic.Add(key, weight);
            }
        }

        /// <summary>
        /// Values
        /// </summary>
        public IEnumerable<Y> Values
        {
            get
            {
                return from v in this.dic.Values
                       orderby v.Count descending
                       select v.Item;
            }
        }
        #endregion
    }
}