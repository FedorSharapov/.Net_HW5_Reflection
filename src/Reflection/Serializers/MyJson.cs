using System.Reflection;
using System.Text;

namespace Reflection.Serializers
{
    static class MyJson
    {
        static private StringBuilder _result = new StringBuilder(64);

        /// <summary>
        /// Сериализация свойств объекта в Json формат
        /// </summary>
        /// <typeparam name="T">тип объекта сериализации</typeparam>
        /// <param name="value">объект сериализации</param>
        /// <returns>сериализованный объект в Json формате</returns>
        /// <exception cref="ArgumentNullException">объект сериализации = null</exception>
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
                AddJsonValue(properties[i].GetValue(value), properties[i].PropertyType);

                if (i != lastI)
                    _result.Append(",");
            }
            _result.Append("}");

            return _result.ToString();
        }

        /// <summary>
        /// Добавление значения в сериализуемый объект
        /// </summary>
        /// <param name="value">значение</param>
        /// <param name="type">тип значения</param>
        /// <exception cref="Exception">Неизвестный тип значения</exception>
        static void AddJsonValue(object value, Type type)
        {
            if (type == typeof(int))
                _result.Append(value.ToString());
            else if (type == typeof(string))
            {
                _result.Append("\"");
                _result.Append(value);
                _result.Append("\"");
            }
            else
                throw new Exception("Unknown type.");
        }
    }
}
