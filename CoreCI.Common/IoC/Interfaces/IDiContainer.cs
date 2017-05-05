using System;
using System.Collections.Generic;

namespace CoreCI.Common.IoC.Interfaces
{
    public interface IDiContainer
    {
        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <typeparam name="TInterface">The type of the interface.</typeparam>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="lifetimeManager">The lifetime manager.</param>
        /// <returns></returns>
        IDiContainer RegisterType<TInterface, TType>(string name = "", LifeTimeManager lifetimeManager = 0)
            where TType : class, TInterface;

        /// <summary>
        /// Registers the type.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="name">The name.</param>
        /// <param name="lifetimeManager">The lifetime manager.</param>
        /// <returns></returns>
        IDiContainer RegisterType<TType>(string name = "", LifeTimeManager lifetimeManager = 0)
            where TType : class;

        /// <summary>
        /// Registers the instance.
        /// </summary>
        /// <param name="type">Type of instance</param>
        /// <param name="instance">The instance.</param>
        /// <param name="lifetimeManager">The lifetime manager.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        IDiContainer RegisterInstance(Type type, object instance, LifeTimeManager lifetimeManager = 0, string name = "");

        /// <summary>
        /// Resolves the specified name.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        TType Resolve<TType>(string name = "");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serveceType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        object Resolve(Type serveceType, string name = "");

        /// <summary>
        /// Resolves all.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <returns></returns>
        IEnumerable<object> ResolveAll<TType>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceTyp"></param>
        /// <returns></returns>
        IEnumerable<object> ResolveAll(Type serviceTyp);

        /// <summary>
        /// Determines whether the specified name is registered.
        /// </summary>
        /// <typeparam name="TType">The type of the type.</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <c>true</c> if the specified name is registered; otherwise, <c>false</c>.
        /// </returns>
        bool IsRegistered<TType>(string name = "");

        /// <summary>
        /// Creates the child container.
        /// </summary>
        /// <returns></returns>
        IDiContainer CreateChildContainer();
    }
}