using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCI.Common.Modularity
{
    public class ConfigModel
    {
        public ConfigModel() { }
        public Modules Modules { get; set; }
    }

    public class Module
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }
        public string Initializer { get; set; }
        public string Folder { get; set; }
    }
    public class Modules
    {
        public Module[] ModuleList { get; set; }
    }
}
