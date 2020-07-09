using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new NServiceBus.EndpointConfiguration("ProductApi");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();
                    transport.Routing().RouteToEndpoint(
                        assembly: typeof(IsItDown).Assembly,
                        destination: "Feed");

                    endpointConfiguration.SendOnly();

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(c => c.UseStartup<Startup>())
                .Build();

            host.Run();
        }
    }
}
