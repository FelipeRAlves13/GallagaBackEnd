﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseKestrel();

                    webBuilder.UseUrls("http://localhost:5000", "http://172.17.192.1:5000");
                      
                    webBuilder.UseStartup<Startup>();
                });
    }
}