using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProxyKit.Recipe.PathsUpstreamAppTwo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var upstreamHost2 = WebHost.CreateDefaultBuilder(args)
                .UseStartup<UpstreamHost2Startup>()
                .UseUrls("http://localhost:55002")
                .Build();

            //await upstreamHost2.StartAsync();

            upstreamHost2.Run();
        }
    }
}