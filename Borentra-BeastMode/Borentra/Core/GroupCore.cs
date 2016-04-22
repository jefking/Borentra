namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using System;

    public class GroupCore
    {
        #region Methods
        public void Queue(string email, string name, Guid? userId)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("email");
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name");
            }

            var sproc = new TestSaveGroupSignUp()
            {
                Email = email,
                Name = name,
                UserIdentifier = userId,
            };

            sproc.ExecuteNonQuery();
        }
        #endregion
    }
}
