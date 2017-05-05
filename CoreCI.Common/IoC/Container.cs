using System;
using System.Collections.Generic;
using CoreCI.Common.IoC.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreCI.Common.IoC
{
    /// <summary>
    /// Wrapper IServiceCollection and IServiceProvider.
    /// </summary>
    public class Container : IContainer
    {
        private readonly IServiceCollection _collection;
        private IServiceProvider _provider;
        
        /// <summary>
        /// Default constructor.
        /// </summary>
        public Container()
        {
            _collection = new ServiceCollection();
            _collection.AddSingleton<IContainer>(this);
        }

        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        public IContainer Register(Type type, LifeTimeManager manager = LifeTimeManager.PerResolve)
        {
            switch (manager)
            {
                case LifeTimeManager.PerResolve:
                    _collection.AddScoped(type);
                    break;
                case LifeTimeManager.ContainerControlled:
                    _collection.AddSingleton(type);
                    break;
                case LifeTimeManager.Transient:
                    _collection.AddTransient(type);
                    break;
            }
            return this;
        }

        /// <summary>
        /// Adds the configuration section.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public IContainer ConfigureSection<T>(IConfigurationRoot config, string sectionName) where T : class
        {
            _collection.Configure<T>(config.GetSection(sectionName));
            return this;
        }

        /// <summary>
        /// Configures the specified configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public IContainer Configure<T>(IConfigurationRoot config) where T : class
        {
            _collection.Configure<T>(config);
            return this;
        }

        /// <summary>
        /// Adds the options.
        /// </summary>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        public IContainer AddOptions(IConfigurationRoot config)
        {
            //_configurationRoot = config;
            _collection.AddOptions();
            return this;
        }

        /// <summary>
        /// Register class in IoC container.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TType"></typeparam>
        /// <param name="manager"></param>
        /// <returns></returns>
        public IContainer Register<TInterface, TType>(LifeTimeManager manager = LifeTimeManager.PerResolve) 
            where TType: class, TInterface 
            where TInterface : class
        {
            switch (manager)
            {
                case LifeTimeManager.PerResolve:
                    _collection.AddScoped<TInterface, TType>();
                    break;
                case LifeTimeManager.ContainerControlled:
                    _collection.AddSingleton<TInterface, TType>();
                    break;
                case LifeTimeManager.Transient:
                    _collection.AddTransient<TInterface, TType>();
                    break;
            }
            return this;
        }

        /// <summary>
        /// Build provider.
        /// </summary>
        /// <returns></returns>
        public IContainer BuildServiceProvider()
        {
            _provider = _collection.BuildServiceProvider();
            return this;
        }

        /// <summary>
        /// Resolve provided type.0.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object GetService(Type type)
        {
            return _provider?.GetService(type);
        }

        /// <summary>
        /// Resolve service for interface.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public TInterface GetService<TInterface>()
        {
            if (_provider == null)
            {
                return default(TInterface);
            }
            return _provider.GetService<TInterface>();
        }

        /// <summary>
        /// Resolve services for interface.
        /// </summary>
        /// <typeparam name="TInterface"></typeparam>
        /// <returns></returns>
        public IEnumerable<TInterface> GetServices<TInterface>()
        {
            return _provider?.GetServices<TInterface>();
        }
    }
}
