using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCI.Common.Serialization.Interfaces
{
    public interface ISerializer
    {
        string Serialize<T>(T target);
        T Deserializer<T>(string target);
    }
}
