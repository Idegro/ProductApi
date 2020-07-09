using Messages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
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
                    var endpointConfiguration = new NServiceBus.EndpointConfiguration("Samples.ASPNETCore.Sender");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();
                    transport.Routing().RouteToEndpoint(
                        assembly: typeof(IsItDown).Assembly,
                        destination: "Samples.ASPNETCore.Endpoint");

                    endpointConfiguration.SendOnly();

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(c => c.UseStartup<Startup>())
                .Build();

            host.Run();
        }
    }
}
