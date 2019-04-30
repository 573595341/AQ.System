using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace AQ.WebMain
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //NLog.Web.NLogBuilder.ConfigureNLog("nlog.config");  //假如没有用默认的名字，多写了一个1
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)//.UseNLog()
            .UseStartup<Startup>();

    }
}
