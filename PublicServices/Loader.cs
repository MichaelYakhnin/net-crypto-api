using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TickerMonitor
{
    public class Loader
    {
        public static T Load<T>(string config)
        {
            return JsonConvert.DeserializeObject<T>(config);
        }
    }
}
