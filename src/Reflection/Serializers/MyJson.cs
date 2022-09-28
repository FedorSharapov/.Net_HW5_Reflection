using System.Reflection;
using System.Text;

namespace Reflection.Serializers
{
    static class MyJson
    {
        static private StringBuilder _result = new StringBuilder(64);

        public static string Serialize<T>(T value)
        {
            if (value == null)
                throw new ArgumentNullException();

            var properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

            _result.Length = 0;
            _result.Append("{");
            var lastI = properties.Length - 1;
            for (int i = 0; i < properties.Length; i++)
            {
                _result.Append("\"");
                _result.Append(properties[i].Name);
                _result.Append("\":");
                GetJsonValue(properties[i].GetValue(value), properties[i].PropertyType);

                if (i != lastI)
                    _result.Append(",");
            }
            _result.Append("}");

            return _result.ToString();
        }

        static void GetJsonValue(object obj, Type type)
        {
            if (type == typeof(int))
                _result.Append(obj.ToString());
            else if (type == typeof(string))
            {
                _result.Append("\"");
                _result.Append(obj);
                _result.Append("\"");
            }
            else
                throw new Exception("Unknown type.");
        }
    }
}
