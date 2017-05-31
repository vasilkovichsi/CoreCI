using System.Reflection;

namespace CoreCI.Common.Modularity.Interfaces
{
    public interface IAssemblyLoader
    {
        /// <summary>
        /// Loads the assemblies for module.
        /// </summary>
        /// <param name="moduleFolder">The module folder.</param>
        /// <param name="assemblyFile">The assembly file.</param>
        void LoadAssembliesForModule(string moduleFolder, string assemblyFile);
    }
}