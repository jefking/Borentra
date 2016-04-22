namespace Borentra.Email
{
    using RestSharp;
    using System;

    /// <summary>
    /// Mail Gun class for sending email through third party.
    /// </summary>
    public class MailGun
    {
        #region Members
        /// <summary>
        /// Access Key
        /// </summary>
        private const string AccessKey = "key-3z5te2mh1upro6t63mf697egwgj26ba9";

        /// <summary>
        /// API Url
        /// </summary>
        private static readonly string APIUrl = "https://api.mailgun.net/v2";
        #endregion

        #region Methods
        public IRestResponse Send(string fromEmail, string fromName, string toEmail, string subject, string message)
        {
            if (string.IsNullOrWhiteSpace(fromEmail))
            {
                throw new ArgumentException("fromEmail");
            }
            if (string.IsNullOrWhiteSpace(toEmail))
            {
                throw new ArgumentException("toEmail");
            }
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentException("subject");
            }
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException("message");
            }

            var request = new RestRequest()
            {
                Resource = "{domain}/messages",
                Method = Method.POST,
            };

            request.AddParameter("domain", "borentra.com", ParameterType.UrlSegment);
            if (string.IsNullOrWhiteSpace(fromName))
            {
                request.AddParameter("from", fromEmail);
            }
            else
            {
                request.AddParameter("from", string.Format("{0} <{1}>", fromName, fromEmail));
            }

            request.AddParameter("to", toEmail);
            request.AddParameter("subject", subject);
            request.AddParameter("html", message);

            var client = new RestClient()
            {
                BaseUrl = APIUrl,
                Authenticator = new HttpBasicAuthenticator("api", AccessKey),
            };

            return client.Execute(request);
        }
        #endregion
    }
}