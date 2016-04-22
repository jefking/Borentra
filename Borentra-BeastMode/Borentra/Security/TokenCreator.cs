namespace Borentra.Security
{
    using System;

    public static class TokenCreator
    {
        #region Methods
        public static string Create(Guid identifier, string key)
        {
            if (Guid.Empty == identifier)
            {
                throw new ArgumentException("identifier");
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("key");
            }

            key = key.TrimIfNotNull();

            return string.Format("{0}{1}", identifier, key).ToBase64();
        }
        #endregion
    }
}