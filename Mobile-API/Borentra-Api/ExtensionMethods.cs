namespace Borentra
{
    using Borentra.API.Models;
    using Borentra.DataAccessLayer;
    using Borentra.Security;
    using System;
    using System.Web.Security;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region System.String
        public static IToken ToToken(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("value");
            }

            if (112 != value.Length)
            {
                throw new ArgumentException("value length");
            }

            return new Token()
            {
                AccessToken = value,
            };
        }
        #endregion

        #region System.Web.Security.MembershipUser
        /// <summary>
        /// Identifier
        /// </summary>
        /// <param name="user">Membership User</param>
        /// <returns>Identifier</returns>
        public static Guid Identifier(this MembershipUser user)
        {
            return (Guid)user.ProviderUserKey;
        }
        #endregion

        #region Borentra.DataAccessLayer.Device
        public static IToken ToToken(this Device device)
        {
            var st = new SecurityToken()
            {
                Id = device.Identifier,
                Key = Key.CreateKey(device.Amplitude, device.VerticalOffset, device.AngularFrequency, device.PhaseShift),
            };

            return st.ToToken();
        }
        #endregion

        #region Borentra.API.Models.ISecuredToken
        public static IToken ToToken(this ISecuredToken token)
        {
            return new Token()
            {
                AccessToken = TokenCreator.Create(token.Id, token.Key),
            };
        }
        #endregion

        #region Borentra.API.Models.IToken
        public static ISecuredToken ToSecuredToken(this IToken token)
        {
            if (null == token)
            {
                throw new ArgumentNullException("token");
            }
            if (string.IsNullOrWhiteSpace(token.AccessToken))
            {
                throw new ArgumentException("Access Token");
            }
            if (112 != token.AccessToken.Length)
            {
                throw new ArgumentException("Access Token Length");
            }

            const int guidLength = 36;
            var value = token.AccessToken.FromBase64<string>();
            var id = Guid.Parse(value.Substring(0, guidLength));
            return new SecurityToken()
            {
                Id = id,
                Key = value.Substring(guidLength),
            };
        }
        #endregion
    }
}