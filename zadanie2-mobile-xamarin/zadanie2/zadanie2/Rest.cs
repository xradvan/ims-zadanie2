using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace zadanie2
{
    class Rest
    {
        public static void Consume<T>(string url, ref List<T> list) where T: class 
        {
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(response.StatusCode.ToString());

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    var content = reader.ReadToEnd();
                    list = JsonConvert.DeserializeObject<List<T>>(content);
                }
            }
        }
    }
}
