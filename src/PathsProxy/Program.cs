using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ProxyKit.Recipe.PathsProxy
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var proxyHost = WebHost.CreateDefaultBuilder(args)
                .UseStartup<ProxyStartup>()
                .Build();

            proxyHost.Run();
        }
    }
}