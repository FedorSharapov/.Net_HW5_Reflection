using Reflection.Serializers;
using System.Diagnostics;
using System.Text;

namespace Reflection
{
    static class Tests
    {
        /// <summary>
        /// Сравнение MyJson c NewtonsoftJsonSerializer и SystemTextJsonSerializer
        /// </summary>
        /// <param name="numberIterations">количество итераций сериализаций</param>
        internal static void CompareSerializers(int numberIterations)
        {
            // инициализация сериализаторов
            var systemTextJS = new SystemTextJsonSerializer();
            var newtonsoftJS = new NewtonsoftJsonSerializer();
            var myJS = new MyJsonSerializer();

            // тесты сериализации
            var leadTimeMyJson = TestSerialize(myJS, numberIterations);
            var leadTimeSystemTextJson = TestSerialize(systemTextJS, numberIterations);
            var leadTimeNewtonsoftJson = TestSerialize(newtonsoftJS, numberIterations);

            // вывод разницы времени выполнения сериализаций
            var difference = Math.Abs(leadTimeMyJson - leadTimeSystemTextJson);
            var text = (leadTimeMyJson > leadTimeSystemTextJson) ? "медленее" : "быстрее";
            ConsoleHelper.WriteLine($"\"{myJS.GetType().Name}\" {text} \"{systemTextJS.GetType().Name}\" на [{difference:F3}] нс",
                ConsoleColor.Green);

            difference = Math.Abs(leadTimeMyJson - leadTimeNewtonsoftJson);
            text = (leadTimeMyJson > leadTimeNewtonsoftJson) ? "медленее" : "быстрее";
            ConsoleHelper.WriteLine($"\"{myJS.GetType().Name}\" {text} \"{newtonsoftJS.GetType().Name}\" на [{difference:F3}] нс",
                ConsoleColor.Green);

            Console.WriteLine();
        }

        /// <summary>
        /// Тест быстродействия сериализации
        /// </summary>
        /// <param name="serializer">сериализатор</param>
        /// <param name="numberIterations">количество сериализаций</param>
        /// <returns>Среднее время 1 итерации сериализации в наносекундах</returns>
        private static double TestSerialize(ISerializer serializer, int numberIterations)
        {
            var testObject = new F();
            long sum = 0;

            ConsoleHelper.WriteLine($"Сериализатор: [{serializer.GetType().Name}]", ConsoleColor.Blue);
            Console.WriteLine($"Json: {serializer.Serialize(testObject)}");
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

        /// <summary>
        /// Тест быстродействия сериализации с выводом в Console
        /// </summary>
        /// <param name="serializer">сериализатор</param>
        /// <param name="numberIterations">количество сериализаций</param>
        public static void TestConsoleWriteSerializedObjects(ISerializer serializer, int numberIterations)
        {
            var testObject = new F();

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < numberIterations; i++)
                Console.WriteLine(serializer.Serialize(testObject));

            stopwatch.Stop();

            ConsoleHelper.WriteLine($"Вывод в Console.\r\nСериализатор: [{serializer.GetType().Name}]", ConsoleColor.Blue);
            ConsoleHelper.WriteLine($"Количество итераций: [{numberIterations}].", ConsoleColor.Yellow);
            ConsoleHelper.WriteLine($"Время выполнения: [{stopwatch.ElapsedMilliseconds}] мс", ConsoleColor.Yellow);

            double meanVal = (stopwatch.ElapsedMilliseconds / (double)numberIterations) * 1000.0;
            ConsoleHelper.WriteLine($"Среднее время выполнения на 1 итерацию: [{meanVal:F1}] нс\r\n", ConsoleColor.Green);
        }
    }
}
