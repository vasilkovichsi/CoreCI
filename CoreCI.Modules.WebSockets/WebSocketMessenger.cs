using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CoreCI.Common.Communication;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using WebSocketManager;

namespace CoreCI.Modules.WebSockets
{
    internal class WebSocketMessenger : IMessenger
    {
        private CancellationTokenSource _cancelationToken = new CancellationTokenSource();
        
        /// <summary>
        /// Initializes a new instance of the <see cref="WebSocketMessenger"/> class.
        /// </summary>
        public WebSocketMessenger(IContainer container, IAssemblyLoader loader)
        {
            IServiceCollection collection = container.GetService<IServiceCollection>();
            
            var host = new WebHostBuilder()
                .UseKestrel()
                .ConfigureServices(serviceCollection =>
                    {
                        serviceCollection.AddSingleton<IServiceCollection>(collection);
                    })
                .UseContentRoot(Directory.GetCurrentDirectory())
                
                .UseStartup<Startup>().Build();
            host.Run(_cancelationToken.Token);
        }

        /// <summary>
        /// Sends the message.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<bool> SendMessage<TType>(TType obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Recieves the message.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Task<TType> RecieveMessage<TType>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops the messenger.
        /// </summary>
        public void StopMessenger()
        {
            _cancelationToken.Cancel();
        }
    }

    internal class Startup
    {
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="collection">The container.</param>
        public void Configure(IApplicationBuilder app, IServiceCollection collection)
        {
            app.UseWebSockets();
            IContainer container = collection.BuildServiceProvider().GetService<IContainer>();

            app.MapWebSocketManager("/messenging", (WebSocketHandler)container.GetService(typeof(WebSocketMessageHandler)));

            app.UseStaticFiles();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void ConfigureServices(IServiceCollection collection)
        {
            collection.AddWebSocketManager();
        }
    }
}
