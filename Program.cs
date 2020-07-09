using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ProductApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(c => c.UseStartup<Startup>())
                .Build();

            host.Run();
        }
    }
}
