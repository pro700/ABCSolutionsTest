using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ABCSolutionsTest.DAL;

namespace ABCSolutionsTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ABCTestDBConext dbcontext = new ABCTestDBConext())
            {
                //dbcontext.Database.EnsureDeleted();
                ABCTestDBInitializer.Initialize(dbcontext);
            }

            var host = BuildWebHost(args);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
