using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ASP.NET_Framework_MVC_Playground.Data_Access
{
    public static class JSON_Access
    {
        public static JObject LoadJSON(String fileName)
        {
            var path = System.Web.Hosting.HostingEnvironment.MapPath($"~/{fileName}");
            JObject json = JObject.Parse(File.ReadAllText(path));
            return json;
        }
    }
}