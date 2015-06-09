using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace hduhelp.Helper
{
    class HttpHelper
    {
        private string _url = string.Empty;
        private List<KeyValuePair<string, string>> _param = null;

        public HttpHelper(string url, List<KeyValuePair<string, string>> param)
        {
            this._url = url;
            this._param = param;
        }

        public async Task<string> PostWebRequest()
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, new Uri(_url));
                var httpClient = new HttpClient();
                request.Content = new HttpFormUrlEncodedContent(_param);
                var response = await httpClient.SendRequestAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();
                return responseString;
            }
            catch (Exception)
            {
                throw new Exception("请检查网络设置。");
            }
        }
    }
}
