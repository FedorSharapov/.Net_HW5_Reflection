namespace Reflection
{
    internal static class CsvFile
    {
        /// <summary>
        /// Запись в файл
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <param name="data">данные</param>
        public static void Write(string path, string data)
        {
            using var streamWriter = new StreamWriter(path);
            streamWriter.Write(data);

            streamWriter.Close();
        }

        /// <summary>
        /// Чтения из файла
        /// </summary>
        /// <param name="path">путь к файлу</param>
        /// <returns>данные файла</returns>
        public static string Read(string path)
        {
            using var streamReader = new StreamReader(path);
            return streamReader.ReadToEnd();
        }
    }
}
