namespace CoreCI.Common.Modularity.Interfaces
{
    public interface IModulesLoader
    {
        /// <summary>
        /// Initialize modules from config by provided application type.
        /// </summary>
        /// <param name="appType"></param>
        void InitializeModules(string appType);
    }
}