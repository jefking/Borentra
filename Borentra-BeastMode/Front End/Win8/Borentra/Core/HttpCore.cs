namespace Borentra.Core
{
    using Newtonsoft.Json;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading.Tasks;

    public class HttpCore
    {
        #region Methods
        public async Task<T> Get<T>(string url)
        {
            var content = await this.Request(url, "GET");
            return JsonConvert.DeserializeObject<T>(content);
        }
        public async Task Get(string url)
        {
            await this.RequestStream(url, "GET");
        }
        public async Task<T> Post<T>(string url, object data)
        {
            byte[] postData = null;
            if (null != data)
            {
                var json = await JsonConvert.SerializeObjectAsync(data);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    postData = UTF8Encoding.UTF8.GetBytes(json);
                }
            }

            var content = await this.Request(url, "POST", postData);
            return JsonConvert.DeserializeObject<T>(content);
        }
        public async void Post(string url, object data)
        {
            byte[] postData = null;
            if (null != data)
            {
                var json = await JsonConvert.SerializeObjectAsync(data);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    postData = UTF8Encoding.UTF8.GetBytes(json);
                }
            }

            await this.RequestStream(url, "POST", postData);
        }

        private async Task<string> Request(string url, string method, byte[] postData = null)
        {
            Stream stream = await this.RequestStream(url, method, postData);
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        private async Task<Stream> RequestStream(string url, string method, byte[] postData = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = method;
            request.ContentType = "application/json";
            if (postData != null)
            {
                using (var dataStream = await request.GetRequestStreamAsync())
                {
                    await dataStream.WriteAsync(postData, 0, postData.Length);
                }
            }

            WebResponse response = await request.GetResponseAsync();
            return response.GetResponseStream();
        }
        #endregion
    }
}