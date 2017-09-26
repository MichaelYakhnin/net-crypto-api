using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace TickerMonitor
{
    public class RestApi
    {
        private string data;

        public string Send(string path)
        {
            using (WebClient wc = new WebClient())
            {
               
                try
                {
                    data = wc.DownloadString(path);
                }
                catch (WebException e)
                {
                    
                   Console.Write( e);
                }
                return data;
            }
        }
       
    }
}
