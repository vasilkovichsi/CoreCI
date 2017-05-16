using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCI.Common.Processors
{
    public class ProcessorsConfigModel
    {
        public ProcessorsConfigModel() { }
        public Processors Processors { get; set; }
    }

    public class Processor
    {
        public string Class { get; set; }
        public string Assembly { get; set; }
    }

    public class Processors
    {
        public Processor[] ProcessorsList { get; set; }
    }
}
