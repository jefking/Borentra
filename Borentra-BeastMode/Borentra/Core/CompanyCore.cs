namespace Borentra.Core
{
    using Borentra.DataAccessLayer;
    using Borentra.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Company Core
    /// </summary>
    public class CompanyCore
    {
        #region Methods
        /// <summary>
        /// Queue to waitlist
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="name">Name</param>
        /// <param name="userId">User Identifier</param>
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

            var sproc = new TestSaveCompanySignUp()
            {
                Email = email,
                Name = name,
                UserIdentifier = userId,
            };

            sproc.ExecuteNonQuery();
        }

        /// <summary>
        /// Get Company
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="identifier">Identifier</param>
        /// <returns>Company</returns>
        public Company Get(string key = null, Guid? identifier = null)
        {
            var id = identifier.HasValue && Guid.Empty != identifier.Value ? identifier.Value : (Guid?)null;

            var sproc = new CompanySearchCompany()
            {
                Identifier = id,
                Key = key.TrimIfNotNull(),
            };

            return sproc.CallObject<Company>();
        }

        /// <summary>
        /// Search Companies
        /// </summary>
        /// <returns>Companies</returns>
        public IEnumerable<Company> Search()
        {
            return new CompanySearchCompany().CallObjects<Company>();
        }

        /// <summary>
        /// Save Company
        /// </summary>
        /// <param name="company">Company</param>
        /// <returns>Company</returns>
        public Company Save(Company company)
        {
            var sproc = new CompanySaveCompany()
            {
                Description = company.Description,
                Identifier = company.Identifier.ToNullable(),
                PhoneNumber = company.PhoneNumber,
                WebsiteUrl = company.WebsiteUrl,
                BannerPath = company.BannerPath,
                LogoPath = company.LogoPath,
            };

            return sproc.CallObject<Company>();
        }

        /// <summary>
        /// Company Base Url
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>Base Url</returns>
        public static string BaseUrl(string key)
        {
            return string.Format("/company/{0}", key);
        }

        /// <summary>
        /// Company Search Url
        /// </summary>
        /// <param name="term">Term</param>
        /// <returns>Search Url</returns>
        public static string SearchUrl(string term, string category = "organic")
        {
            return string.Format("/search/company?s={0}&c={1}", term.TrimIfNotNull(), category.TrimIfNotNull());
        }
        #endregion
    }
}