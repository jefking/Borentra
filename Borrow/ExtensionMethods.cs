namespace Borentra
{
    using Borentra.Core;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Principal;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;

    /// <summary>
    /// Extension Methods
    /// </summary>
    public static class ExtensionMethods
    {
        #region System.Web.Mvc.UrlHelper
        /// <summary>
        /// Content cached on CDN
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="resource">content</param>
        /// <returns>Url</returns>
        public static string ContentCdn(this UrlHelper url, string resource, bool userContent = false)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;
            if (userContent & requestUrl.IsLoopback)
            {
                return resource.Replace("~", "http://cdn.borentra.com");
            }
            else
            {
                return requestUrl.IsLoopback & !userContent ? url.Content(resource) : resource.Replace("~", "http://cdn.borentra.com");
            }
        }
        /// <summary>
        /// Content straight from Blob, to avoid caching
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="resource">content</param>
        /// <returns>Url</returns>
        public static string ContentBlob(this UrlHelper url, string resource, bool userContent = false)
        {
            var requestUrl = url.RequestContext.HttpContext.Request.Url;
            if (userContent & requestUrl.IsLoopback)
            {
                return resource.Replace("~", "//content.borentra.com");
            }
            else
            {
                return requestUrl.IsLoopback & !userContent ? url.Content(resource) : resource.Replace("~", "//content.borentra.com");
            }
        }
        #endregion

        #region System.Web.Mvc.HtmlHelper
        public static IHtmlString SelectOption(this HtmlHelper helper, string text, int value, int trueValue)
        {
            return helper.Raw(string.Format("<option value='{1}'{2}>{0}</option>", text, value, value == trueValue ? " selected='selected'" : string.Empty));
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

        #region System.Security.Principal.IPrincipal
        /// <summary>
        /// Gets The Membership User
        /// </summary>
        /// <param name="principal">Principal</param>
        /// <returns>Membership User</returns>
        public static MembershipUser User(this IPrincipal principal)
        {
            return Membership.GetUser(principal.Identity.NameIdentifier());
        }

        /// <summary>
        /// User Identifier, when you don't know if the user is logged in
        /// </summary>
        /// <param name="principal">Principal</param>
        /// <returns>User Identifier</returns>
        public static Guid? IdentifierSafe(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated ? principal.User().Identifier() : (Guid?)null;
        }

        /// <summary>
        /// User Identifier
        /// </summary>
        /// <param name="principal">Principal</param>
        /// <returns>User Identifier</returns>
        public static Guid Identifier(this IPrincipal principal)
        {
            return principal.Identity.IsAuthenticated ? principal.User().Identifier() : Guid.Empty;
        }
        #endregion

        #region System.Security.Principal.IIdentity
        /// <summary>
        /// Get Identities Email Address
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Email Address</returns>
        public static string EmailAddress(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;

            return identity == null ? null : claimIdentity.ClaimValue(ClaimTypes.Email);
        }

        /// <summary>
        /// Get Identities Name
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Name</returns>
        public static string Name(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;

            return identity == null ? null : claimIdentity.ClaimValue(ClaimTypes.Name);
        }

        /// <summary>
        /// Get Identities Facebook Access Token
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Facebook Access Token</returns>
        public static string FacebookAccessToken(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;

            return identity == null ? null : claimIdentity.ClaimValue("http://www.facebook.com/claims/AccessToken");
        }

        /// <summary>
        /// Is Manager
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Is Manager</returns>
        public static bool IsManager(this IIdentity identity)
        {
            var claim = identity as ClaimsIdentity;
            bool isManager = false;
            if (claim != null)
            {
                var role = claim.ClaimValue(ClaimTypes.Role);
                if (!string.IsNullOrWhiteSpace(role))
                {
                    isManager = role.ToUpperInvariant() == "staff".ToUpperInvariant();
                }
            }

            return isManager;
        }

        /// <summary>
        /// Get Identities Identity Provider
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Identity Provider</returns>
        public static string IdentityProvider(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;

            return identity == null ? null : claimIdentity.ClaimValue("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider");
        }

        /// <summary>
        /// Get Identities Expiration
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Expiration</returns>
        public static DateTime? Expiration(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;
            if (identity == null)
            {
                return null;
            }

            DateTime expiration;
            return DateTime.TryParse(claimIdentity.ClaimValue(ClaimTypes.Expiration), out expiration) ? (DateTime?)expiration : (DateTime?)null;
        }

        /// <summary>
        /// Get Identities Email Address
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <returns>Email Address</returns>
        public static string NameIdentifier(this IIdentity identity)
        {
            var claimIdentity = identity as ClaimsIdentity;

            return identity == null ? null : claimIdentity.ClaimValue(ClaimTypes.NameIdentifier);
        }

        /// <summary>
        /// Claim Value
        /// </summary>
        /// <param name="identity">Identity</param>
        /// <param name="claimType">Claim Type</param>
        /// <returns>Value of specified Claim</returns>
        public static string ClaimValue(this ClaimsIdentity identity, string claimType)
        {
            return identity.Claims == null ? null : (from data in identity.Claims
                                                     where data.Type.ToUpperInvariant() == claimType.ToUpperInvariant()
                                                     select data.Value).FirstOrDefault();
        }
        #endregion

        #region System.String
        /// <summary>
        /// Parse Urls
        /// </summary>
        /// <param name="withUrls">With Urls</param>
        /// <returns>Urls</returns>
        public static string[] ParseUrls(this string withUrls)
        {
            const string pattern = @"(?<Url>((https?):((//)|(\\\\))+[\w\d:#@%/;$()~_?\+-=\\\.&]*))";
            if (!string.IsNullOrWhiteSpace(withUrls))
            {
                var urls = new List<string>();

                foreach (Match match in Regex.Matches(withUrls, pattern))
                {
                    urls.Add(match.Groups["Url"].Value);
                }

                return urls.ToArray();
            }

            return null;
        }

        /// <summary>
        /// Parse Categories
        /// </summary>
        /// <param name="content">Content</param>
        /// <returns>Categories</returns>
        public static string[] ParseCategories(this string content)
        {
            string[] categories = null;
            if (!string.IsNullOrWhiteSpace(content))
            {
                var cats = new List<string>();

                foreach (Match match in Regex.Matches(content, "[#](?<Category>[a-zA-Z0-9]*)[ $]{0,1}"))
                {
                    cats.Add(match.Groups["Category"].Value);
                }

                categories = cats.ToArray();
            }

            return categories;
        }
        #endregion

        #region IDescriptive
        /// <summary>
        /// Set Categories
        /// </summary>
        /// <param name="item">Item</param>
        public static void SetCategories(this IDescriptive item)
        {
            var descriptionCategories = item.Description.ParseCategories();
            var categories = new List<string>();
            if (null != descriptionCategories)
            {
                categories.AddRange(descriptionCategories);
            }
            var titleCategories = item.Title.ParseCategories();
            if (null != titleCategories)
            {
                categories.AddRange(titleCategories);
            }
            var tagCategories = item.Tags.ParseCategories();
            if (null != tagCategories)
            {
                categories.AddRange(tagCategories);
            }
            
            if (0 < categories.Count)
            {
                item.Categories = categories.Distinct();
            }
        }

        /// <summary>
        /// Render HTML for Display
        /// </summary>
        /// <returns>HTML Fragment (minimal markup)</returns>
        public static string RenderDisplayHtml(this IDescriptive thing, bool isOffer = true)
        {
            var display = new StringBuilder(thing.Description);
            display.Replace("\n", "<br/>");

            var urls = thing.Description.ParseUrls();
            if (null != urls)
            {
                foreach (var url in urls)
                {
                    display.Replace(url, string.Format("<a class='user-link' href='{0}' target='_blank' onclick='_gaq.push(['_trackEvent', 'Offsite', 'Link', 'User-Item-Request'])'>External Link</a>", url));
                }
            }

            if (null == thing.Categories)
            {
                thing.SetCategories();
            }

            if (null != thing.Categories)
            {
                var page = isOffer ? "offer" : "wanted";
                var isReferenceType = thing is IReferenceType;
                var temp = display.ToString();
                foreach (var category in thing.Categories)
                {
                    string url = null;
                    if (isReferenceType)
                    {
                        url = ((IReferenceType)thing).RelativeSearch(category);
                    }
                    else
                    {
                        url = string.Format("/search/{0}?s={1}", page, category);
                    }

                    temp = Regex.Replace(temp, "([#]" + category + "\\b{1})", string.Format("<a href='{0}&c=tag_embeded'>#{1}</a>", url, category));
                }

                display = new StringBuilder(temp);
            }

            return display.ToString();
        }
        public static string RenderDisplayHtml(this IContent content)
        {
            var display = new StringBuilder(content.Description);
            display.Replace("\n", "<br/>");

            var urls = content.Description.ParseUrls();
            if (null != urls)
            {
                foreach (var url in urls)
                {
                    display.Replace(url, string.Format("<a class='user-link' href='{0}' target='_blank' onclick='_gaq.push(['_trackEvent', 'Offsite', 'Link', 'User-Item-Request'])'>External Link</a>", url));
                }
            }

            var categories = new List<string>();
            categories.AddRange(content.Description.ParseCategories());
            categories.AddRange(content.Title.ParseCategories());

            if (null != categories && 0 < categories.Count())
            {
                var isReferenceType = content is IReferenceType;
                var temp = display.ToString();
                foreach (var category in categories)
                {
                    string url = null;
                    if (isReferenceType)
                    {
                        url = ((IReferenceType)content).RelativeSearch(category);
                    }

                    temp = Regex.Replace(temp, "([#]" + category + "\\b{1})", string.Format("<a href='{0}&c=tag_embeded'>#{1}</a>", url, category));
                }

                display = new StringBuilder(temp);
            }

            return display.ToString();
        }
        #endregion

        #region SearchResult
        public static string RelativeLink(this SearchResult result)
        {
            switch (result.Type)
            {
                case Reference.ItemRequest:
                    return ItemRequestCore.BaseUrl(result.Key);
                case Reference.Item:
                    return ItemCore.BaseUrl(result.Key);
                case Reference.User:
                    return ProfileCore.BaseUrl(result.Key);
                case Reference.Company:
                    return CompanyCore.BaseUrl(result.Key);
                default:
                    throw new InvalidOperationException("unkown type");
            }
        }
        #endregion

        #region IReferenceType
        public static string RelativeSearch(this IReferenceType reference, string query)
        {
            switch (reference.Type)
            {
                case Reference.ItemRequest:
                    return ItemRequestCore.SearchUrl(query);
                case Reference.Item:
                    return ItemCore.SearchUrl(query);
                case Reference.User:
                    return ProfileCore.SearchUrl(query);
                case Reference.Company:
                    return CompanyCore.SearchUrl(query);
                default:
                    return null;
            }
        }
        #endregion
    }
}