namespace Reflection.Serializers
{
    internal class MyJsonSerializer : ISerializer
    {
        public string Serialize<T>(T value)
        {
            return MyJson.Serialize(value);
        }
    }
}
