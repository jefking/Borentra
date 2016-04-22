namespace Borentra.Core
{
    using Newtonsoft.Json.Linq;
    using System.IO;
    using System.Net;
    using System.Text;

    public class GoodReadsCore
    {
        #region Members
        /// <summary>
        /// URL Format
        /// </summary>
        private const string urlFormat = "http://www.goodreads.com/book/title?format=json&key=u5hHHsAN0ENbDfW7N5neA&title={0}";
        #endregion

        #region Methods
        /// <summary>
        /// Get Review Token
        /// </summary>
        /// <param name="search"></param>
        /// <param name="categories"></param>
        /// <returns></returns>
        public string GetReviewToken(string search)
        {
            var token = string.Empty;

            var url = string.Format(urlFormat, search);

            try
            {
                var request = WebRequest.Create(url);

                request.Method = "GET";

                using (var response = request.GetResponse())
                {
                    using (var receiveStream = response.GetResponseStream())
                    {
                        // Pipes the stream to a higher level stream reader with the required encoding format. 
                        using (var readStream = new StreamReader(receiveStream, Encoding.UTF8))
                        {
                            var body = readStream.ReadToEnd();

                            var o = JObject.Parse(body);

                            token = (string)o.SelectToken("reviews_widget");
                        }
                    }
                }
            }
            catch
            {
                // I guess we can't get reviews for this book.
            }

            return token;
        }
        #endregion
    }
}