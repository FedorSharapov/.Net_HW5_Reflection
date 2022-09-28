using Newtonsoft.Json;

namespace Reflection.Serializers
{
    internal class NewtonsoftJsonSerializer : ISerializer
    {
        public string Serialize<T>(T value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }
}
