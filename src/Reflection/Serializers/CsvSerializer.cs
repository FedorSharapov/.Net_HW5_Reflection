using System.Text;

namespace Reflection.Serializers
{
    internal static class CsvSerializer
    {
        /// <summary>
        /// Сериализовать значения свойств объекта в строку с 'tab' разделителем 
        /// </summary>
        /// <typeparam name="T">тип объекта</typeparam>
        /// <param name="value">объект</param>
        /// <param name="separator">разделитель</param>
        /// <returns>сериализованные значения свойств объекта в строку</returns>
        /// <exception cref="ArgumentNullException">объект не может быть null</exception>
        public static string Serialize<T>(T value, char separator = '\t') 
        {
            if (value == null)
                throw new ArgumentNullException();

            var properties = typeof(T).GetProperties();
            return new StringBuilder()
                .AppendJoin(separator, properties.Select(x => x.GetValue(value)))
                .ToString();
        }

        /// <summary>
        /// Сериализация имен свойств объекта
        /// </summary>
        /// <typeparam name="T">тип объекта</typeparam>
        /// <param name="value">объект</param>
        /// <param name="separator">разделитель</param>
        /// <returns>сериализованные имена свойств объекта в строку</returns>
        /// <exception cref="ArgumentNullException">объект не может быть null</exception>
        public static string SerializeHeader<T>(T value, char separator = '\t')
        {
            if (value == null)
                throw new ArgumentNullException();

            var properties = typeof(T).GetProperties();
            return new StringBuilder()
                .AppendJoin(separator, properties.Select(x => x.Name))
                .ToString();
        }

        /// <summary>
        /// Десериализация значений из строки с 'tab' разделителем
        /// </summary>
        /// <typeparam name="T">тип объекта десериализации</typeparam>
        /// <param name="value">сериализованный объект</param>
        /// <param name="separator">разделитель</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">value не может быть null</exception>
        /// <exception cref="ArgumentException">value не может быть пустым</exception>
        public static T? Deserialize<T>(string value, char separator = '\t') where T : class, new()
        {
            if (value == null)
                throw new ArgumentNullException();
            else if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.");

            var propertiesValues = value.Split(separator); 
            var properties = typeof(T).GetProperties();

            if (propertiesValues.Length != properties.Length)
                throw new ArgumentException("The number of values doesn't match the number of properties.");

            var obj = new T();

            int i = 0;
            foreach (var p in properties)
            {
                var pValue = Convert.ChangeType(propertiesValues[i++], p.PropertyType);
                p.SetValue(obj, pValue);
            }

            return obj;
        }
    }
}
