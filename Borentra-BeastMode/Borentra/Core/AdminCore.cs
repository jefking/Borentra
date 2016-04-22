namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.DataAccessLayer.Admin;
    using Borentra.Models;
    using System.Collections.Generic;

    public class AdminCore
    {
        #region Methods
        public void Archive()
        {
            new AdminArchive().ExecuteNonQuery();
        }

        public IEnumerable<TestSignUp> TestSignUps()
        {
            return new TestSearchSignUp().CallObjects<TestSignUp>();
        }

        public IEnumerable<ProfileReport> Profiles(short top = 10)
        {
            var sproc = new AdminFindProfile()
            {
                Top = top,
            };

            return sproc.CallObjects<ProfileReport>();
        }

        public IEnumerable<Item> Items(short top = 10)
        {
            var sproc = new AdminFindItem()
            {
                Top = top,
            };

            return sproc.CallObjects<Item>();
        }

        public IEnumerable<ItemRequest> ItemRequests(short top = 10)
        {
            var sproc = new AdminFindItemRequest()
            {
                Top = top,
            };

            return sproc.CallObjects<ItemRequest>();
        }

        public void GenerateBadges()
        {
            new AdminGenerateBadges().ExecuteNonQuery();
        }

        public void GenerateForProfile()
        {
            new AdminGenerateForProfile().ExecuteNonQuery();
        }
        #endregion
    }
}