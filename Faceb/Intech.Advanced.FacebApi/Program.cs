using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Intech.Advanced.FacebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://localhost:5000", "http://192.168.25.34:5000")
                //.UseUrls("http://localhost:5000", "http://10.10.170.25:5000")
                .UseUrls("http://localhost:5000", "http://10.10.172.176:5000", "http://189.23.118.181:9102")
                .UseStartup<Startup>();
    }
}
