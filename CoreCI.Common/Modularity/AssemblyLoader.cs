using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using CoreCI.Common.Extensions;
using CoreCI.Common.IoC.Interfaces;
using CoreCI.Common.Modularity.Interfaces;
using Microsoft.Extensions.DependencyModel;

namespace CoreCI.Common.Modularity
{
    internal class AssemblyLoader : AssemblyLoadContext, IAssemblyLoader
    {
        private readonly IContainer _container;

        public AssemblyLoader(IContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Loads the specified assembly name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns></returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
           // return Assembly.Load(assemblyName);

            DependencyContext deps = DependencyContext.Default;
            IEnumerable<CompilationLibrary> res = deps.CompileLibraries;//.Where(d => d.Name.Contains(assemblyName.Name));
            IEnumerable<CompilationLibrary> assemblies = deps.CompileLibraries.ForEach(x =>
            {
                try
                {
                    Assembly.Load(new AssemblyName(x.Name));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });

            Assembly assembly = Assembly.Load(new AssemblyName(res.FirstOrDefault()?.Name));
            return assembly;
        }

        ///// <summary>
        ///// Loads the assembly.
        ///// </summary>
        ///// <param name="assemblyName">Name of the assembly.</param>
        ///// <returns></returns>
        //public Assembly LoadAssembly(AssemblyName assemblyName)
        //{
        //    return Load(assemblyName);
        //}

        public void LoadAssembliesForModule(string folder, string assemblyName)
        {
            string path = Path.Combine(folder, assemblyName);
            Assembly asm = Default.LoadFromAssemblyPath(path);
            DependencyContext deps = DependencyContext.Load(asm);

            IEnumerable<CompilationLibrary> compilationLibraries = deps.CompileLibraries?.Where(x => x.Name.Equals("websocketmanager", StringComparison.CurrentCultureIgnoreCase));

            foreach (var compilationLibrary in compilationLibraries)
            {
                Console.WriteLine($"\tPackage {compilationLibrary.Name} {compilationLibrary.Version}");

                foreach (Dependency dependency in compilationLibrary.Dependencies)
                {
                    try
                    {
                        //Default.LoadFromAssemblyName(new AssemblyName(dependency.Name));
                        Default.LoadFromAssemblyPath(Path.Combine(folder, $"{dependency.Name}.dll"));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                }
            }
        }
    }
}