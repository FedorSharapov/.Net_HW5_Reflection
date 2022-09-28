using Reflection.Serializers;
using System.Diagnostics;

namespace Reflection
{
    static class Tests
    {
        internal static void CompareSerializers(int numberIterations)
        {
            // инициализация сериализаторов
            var systemTextJS = new SystemTextJsonSerializer();
            var newtonsoftJS = new NewtonsoftJsonSerializer();
            var myJS = new MyJsonSerializer();

            // тесты сериализации
            var leadTimeSystemTextJson = TestSerialize(systemTextJS, numberIterations);
            var leadTimeNewtonsoftJson = TestSerialize(newtonsoftJS, numberIterations);
            var leadTimeMyJson = TestSerialize(myJS, numberIterations);

            // вывод разницы времени выполнения сериализаций
            var difference = Math.Abs(leadTimeMyJson - leadTimeSystemTextJson);
            var text = (leadTimeMyJson > leadTimeSystemTextJson) ? "медленее" : "быстрее";
            ConsoleHelper.WriteLine($"\"{myJS.GetType().Name}\" {text} \"{systemTextJS.GetType().Name}\" на [{difference:F3}]",
                ConsoleColor.Green);

            difference = Math.Abs(leadTimeMyJson - leadTimeNewtonsoftJson);
            text = (leadTimeMyJson > leadTimeNewtonsoftJson) ? "медленее" : "быстрее";
            ConsoleHelper.WriteLine($"\"{myJS.GetType().Name}\" {text} \"{newtonsoftJS.GetType().Name}\" на [{difference:F3}]",
                ConsoleColor.Green);

            Console.WriteLine();
        }

        private static double TestSerialize(ISerializer serializer, int numberIterations)
        {
            var testObject = new F();
            long sum = 0;

            ConsoleHelper.WriteLine($"[{serializer.GetType().Name}]", ConsoleColor.Blue);
            ConsoleHelper.WriteLine($"Json: {serializer.Serialize(testObject)}", ConsoleColor.Blue);
            ConsoleHelper.WriteLine($"Количество итераций: [{numberIterations}].", ConsoleColor.Yellow);

            for (int j = 0; j < 10; j++)
            {
                Console.Write($"{j + 1}. ");

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                for (int i = 0; i < numberIterations; i++)
                    serializer.Serialize(testObject);

                stopwatch.Stop();

                sum += stopwatch.ElapsedMilliseconds;
                ConsoleHelper.WriteLine($"Время выполнения: [{stopwatch.ElapsedMilliseconds}] мс", ConsoleColor.Yellow);
            }

            double meanVal = ((sum / 10.0) / numberIterations) * 1000.0;
            ConsoleHelper.WriteLine($"Среднее время выполнения на 1 итерацию: [{meanVal:F3}] нс\r\n", ConsoleColor.Green);

            return meanVal;
        }
    }
}
