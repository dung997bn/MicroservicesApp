using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Ordering.API.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.API.Extentions
{
    public static class ApplicationBuilderExtentions
    {
        public static EventBusRabbitMQConsumer Listener { get; set; }
        public static IApplicationBuilder UseRabbitMQListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);
            return app;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
