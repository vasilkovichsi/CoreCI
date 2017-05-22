using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CoreCI.Common.Communication;
using CoreCI.Common.IoC.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebSocketManager;

namespace CoreCI.Modules.WebSockets
{
    internal class WebSocketMessenger : IMessenger
    {
        private CancellationTokenSource _cancelationToken = new CancellationTokenSource();
        public WebSocketMessenger()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>().Build();
            host.Run(_cancelationToken.Token);
        }

        public Task<bool> SendMessage<TType>(TType obj)
        {
            throw new NotImplementedException();
        }

        public Task<TType> RecieveMessage<TType>()
        {
            throw new NotImplementedException();
        }

        public void StopMessenger()
        {
            _cancelationToken.Cancel();
        }
    }

    internal class Startup
    {
        public void Configure(IApplicationBuilder app, IContainer container)
        {
            app.UseWebSockets();
            app.MapWebSocketManager("/messenging", container.GetService<WebSocketMessageHandler>());

            app.UseStaticFiles();
        }

        public void ConfigureServices(IContainer container)
        {
            container.AddWebSocketManager();
        }
    }
}
