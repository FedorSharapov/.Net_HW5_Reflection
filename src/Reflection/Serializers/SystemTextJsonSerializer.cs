using System.Text.Json;

namespace Reflection.Serializers
{
    internal class SystemTextJsonSerializer : ISerializer
    {
        public string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value);
        }
    }
}
