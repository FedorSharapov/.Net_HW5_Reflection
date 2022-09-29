using Reflection.Serializers;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tests.TestConsoleWriteSerializedObjects(new SystemTextJsonSerializer(),100000);

            Tests.CompareSerializers(100000);

            Tests.CsvSerializationToFile("test.csv", 100000);
            Tests.CsvDeserializationFromFile("test.csv");

            Console.ReadKey();
        }
    }
}