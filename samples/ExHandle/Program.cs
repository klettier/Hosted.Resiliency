using ExHandle.HostedService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ExHandle
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await Host.CreateDefaultBuilder(args)
                        .ConfigureLogging(logging =>
                        {
                            logging.AddConsole();
                        })
                        .ConfigureServices(services =>
                        {
                            services.AddHostedService(MyServiceFactory.Build);
                        })
                        .UseConsoleLifetime()
                        .Build()
                        .RunAsync();
        }
    }
}
