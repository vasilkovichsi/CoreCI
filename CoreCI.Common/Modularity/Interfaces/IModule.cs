using CoreCI.Common.IoC.Interfaces;

namespace CoreCI.Common.Modularity.Interfaces
{
    public interface IModule
    {
        void InitializeModule(IContainer container);
    }
}