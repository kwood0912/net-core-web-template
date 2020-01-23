using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SolutionName.Services.Common
{
    public class HttpWebService
    {
        protected T Get<T>(string url, Dictionary<string, string> queryValues = null)
        {
            if (queryValues != null)
            {
                url = QueryHelpers.AddQueryString(url, queryValues);
            }
            return ExecuteApiRequest<T>("GET", url);
        }

        protected T Post<T>(string url, object body)
        {
            //stub method
            return ExecuteApiRequest<T>("POST", url, body);
        }

        protected T Put<T>(string url, object body)
        {
            //stub method
            return ExecuteApiRequest<T>("PUT", url, body);
        }

        private T ExecuteApiRequest<T>(string verb, string url, object body = null)
        {
            T result = default(T);
            //build the http request
            var webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = verb;
            //stream the http request body if there is one
            if (body != null)
            {
                var bodyStr = JsonConvert.SerializeObject(body);
                var bodyBytes = Encoding.ASCII.GetBytes(bodyStr);
                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(bodyBytes, 0, bodyBytes.Length);
                    stream.Flush();
                }
            }

            HttpWebResponse webResponse = null;
            try
            {
                webResponse = webRequest.GetResponse() as HttpWebResponse;
            }
            catch (WebException wex)
            {
                webResponse = wex.Response as HttpWebResponse;
            }

            //parse and deserialize the returned payload
            using (var responseStream = webResponse?.GetResponseStream())
            {
                if (responseStream != null)
                {
                    string streamData = new StreamReader(responseStream).ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(streamData);
                }
            }
            return result;
        }
    }
}
