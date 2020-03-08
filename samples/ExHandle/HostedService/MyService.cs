using ExHandle.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace ExHandle.HostedService
{
    class MyService : DefaultMyService
    {
        public MyService(
            Models.Configuration configuration)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
        }
    }

    class DefaultMyService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }

    static class MyServiceFactory
    {
        public static DefaultMyService Build(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger(nameof(Build));

            try
            {
                var config = BuildConfiguration();

                if (config == null)
                {
                    logger.LogError("Config is null");

                    StopApplication(serviceProvider);

                    return new DefaultMyService();
                }

                return new MyService(config);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error occurred");
                StopApplication(serviceProvider);
            }

            return new DefaultMyService();
        }

        static void StopApplication(IServiceProvider serviceProvider)
        {
            var hostApplicationLifetime = serviceProvider.GetService<IHostApplicationLifetime>();
            hostApplicationLifetime.StopApplication();
        }

        static Configuration? BuildConfiguration()
        {
            throw new Exception();

            //return new Configuration();
        }
    }
}
