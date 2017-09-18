using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExternalServices
{
    public class RestApi
    {
        public JObject Send(string path)
        {
            using (WebClient wc = new WebClient())
            {
                string ticker = wc.DownloadString(path);
                JObject jo = JObject.Parse(ticker);
                return jo;
            }
        }
       
    }
}
