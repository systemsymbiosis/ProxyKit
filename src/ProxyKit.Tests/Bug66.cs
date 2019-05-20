using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;
using Xunit.Abstractions;

namespace ProxyKit
{
    // https://github.com/damianh/ProxyKit/issues/66
    public class Bug66
    {
        private readonly ITestOutputHelper _testOutputHelper;

        private static readonly Dictionary<string, string> s_urlMaps = new Dictionary<string, string>()
        {
            {"/sysa", "http://localhost:5001/sysatest/"},
            {"/sysb", "http://localhost:5001/sysbtest/"},
            {"/sysc", "http://localhost:5001/sysctest/"},
        };

        public Bug66(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Blah()
        {
            var server = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:0")
                .UseStartup<HostStartup>()
                .Build();

            await server.StartAsync();
            var port = server.GetServerPort();

            // Build Proxy TestServer
            var proxyWebHostBuilder = new WebHostBuilder()
                .UseStartup<ProxyStartup>()
                .UseSetting("port", port.ToString());
            var proxyTestServer = new TestServer(proxyWebHostBuilder);

            var httpClient = proxyTestServer.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5000/sysa/");
            var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            var body = await response.Content.ReadAsStringAsync();
            _testOutputHelper.WriteLine(body);
            response.StatusCode.ShouldBe(HttpStatusCode.OK);
        }

        public class ProxyStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddProxy();
            }

            public void Configure(IApplicationBuilder app, IConfiguration config)
            {
                var port = config.GetValue("Port", 0);
                app.MapWhen(context => s_urlMaps.ContainsKey(context.Request.Path), appInner =>
                {
                    appInner.RunProxy(async context =>
                    {
                        var targetUrl = s_urlMaps[context.Request.Path];
                        targetUrl = targetUrl.Replace("5001", port.ToString());

                        var response = await context
                            .ForwardTo(targetUrl)
                            .AddXForwardedHeaders()
                            .Send();

                        return response;
                    });
                });

                /*app.Map("/sysa", appInner =>
                {
                    appInner.RunProxy(async context =>
                    {
                        var targetUrl = $"http://localhost:{port}/sysatest/";

                        var response = await context
                            .ForwardTo(targetUrl)
                            .AddXForwardedHeaders()
                            .Send();

                        return response;
                    });
                });*/
            }
        }

        public class HostStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {}

            public void Configure(IApplicationBuilder app)
            {
                app.UseXForwardedHeaders(new ForwardedHeadersOptions
                {
                    AllowedHosts = new List<string> { "localhost" },
                    ForwardedHeaders = ForwardedHeaders.All
                });
                app.Run(async context =>
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.StatusCode = 200;
                    await context.Response.WriteAsync(context.Request.Path);
                });
            }
        }
    }
}
