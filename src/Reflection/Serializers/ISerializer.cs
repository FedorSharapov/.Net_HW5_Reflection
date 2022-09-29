namespace Reflection.Serializers
{
    internal interface ISerializer
    {
        /// <summary>
        /// сериализовать 
        /// </summary>
        /// <typeparam name="T">тип сериализуемого объекта</typeparam>
        /// <param name="value">сериализуемый объект</param>
        /// <returns>сериализованный объект в строку</returns>
        public string Serialize<T>(T value);
    }
}
