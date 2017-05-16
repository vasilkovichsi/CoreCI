using System.Collections.Generic;
using CoreCI.Common.Processors;

namespace CoreCI.Common.Modularity.Interfaces
{
    public interface IProcessorsLoader
    {
        /// <summary>
        /// Initializes the processors.
        /// </summary>
        /// <returns></returns>
        IList<IProcessor> InitializeProcessors();
    }
}