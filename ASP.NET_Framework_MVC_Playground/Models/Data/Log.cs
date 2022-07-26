using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Models.Data
{
    public class Log
    {
        public int Log_ID { get; set; }
        public DateTime Log_DateTime { get; set; }
        public string Log_Thread { get; set; }
        public string Log_Level { get; set; }
        public string Log_Source { get; set; }
        public string Log_Message { get; set; }
        public string Log_Exception { get; set; }
    }
}