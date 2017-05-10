using System;
using System.Collections.Generic;
using System.Text;
using CoreCI.Common.Serialization.Interfaces;
using Newtonsoft.Json;

namespace CoreCI.Common.Serialization
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T target)
        {
            return JsonConvert.SerializeObject(target);
        }

        public T Deserializer<T>(string target)
        {
            return JsonConvert.DeserializeObject<T>(target);
        }
    }
}
