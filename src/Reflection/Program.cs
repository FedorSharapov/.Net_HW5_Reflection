using Reflection.Serializers;

namespace Reflection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tests.CompareSerializers(10000);
            Tests.CompareSerializers(100000);

            Console.ReadKey();
        }
    }
}