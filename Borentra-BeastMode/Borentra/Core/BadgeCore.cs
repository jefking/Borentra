namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Badge Core
    /// </summary>
    public class BadgeCore
    {
        #region Methods
        public IEnumerable<Badge> Search(Guid userId)
        {
            var sproc = new SocialSearchBadges()
            {
                UserIdentifier = userId,
            };

            return sproc.CallObjects<Badge>();
        }

        /// <summary>
        /// Search for Badges
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Badge> Search()
        {
            return new SocialSearchBadgeInformation().CallObjects<Badge>();
        }
        #endregion
    }
}