namespace Reflection.Serializers
{
    internal interface ISerializer
    {
        public string Serialize<T>(T value);
    }
}
