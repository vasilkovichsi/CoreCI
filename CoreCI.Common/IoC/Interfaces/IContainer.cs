using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CoreCI.Common.IoC.Interfaces
{
    /// <summary>
    /// Container interface.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Register class in IoC container.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        IContainer Register<TInterface, TType>(LifeTimeManager manager = LifeTimeManager.PerResolve)
            where TType : class, TInterface
            where TInterface : class;

        /// <summary>
        /// Registers the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="manager">The manager.</param>
        /// <returns></returns>
        IContainer Register(Type type, LifeTimeManager manager = LifeTimeManager.PerResolve);

        /// <summary>
        /// Build provider.
        /// </summary>
        /// <returns></returns>
        IContainer BuildServiceProvider();

        /// <summary>
        /// resolve service for interface.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns></returns>
        TInterface GetService<TInterface>();

        /// <summary>
        /// Resolve provided type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        object GetService(Type type);

        /// <summary>
        /// Register coniguration.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        IContainer AddOptions(IConfigurationRoot builder);

        ///// <summary>
        ///// Adds the configuration section.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="section">The section.</param>
        ///// <returns></returns>
        // IContainer Configure<T>() where T : class;

        /// <summary>
        /// Configures the specified configuration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns></returns>
        IContainer Configure<T>(IConfigurationRoot config) where T : class;

        /// <summary>
        /// Resolve services for interface.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <returns></returns>
        IEnumerable<TInterface> GetServices<TInterface>();
    }
}