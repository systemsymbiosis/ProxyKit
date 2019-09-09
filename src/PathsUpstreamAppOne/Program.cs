using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProxyKit.Recipe.PathsUpstreamAppOne
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var upstreamHost1 = WebHost.CreateDefaultBuilder(args)
                .UseStartup<UpstreamHost1Startup>()
                .UseUrls("http://localhost:55001")
                .Build();

          //await upstreamHost1.StartAsync();

            upstreamHost1.Run();

        }
    }
}