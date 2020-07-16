using System;
using BLL.Enums;

namespace CourseManagerConsole
{
    internal static class Utility
    {
        public static bool GetPaidInfo()
        {
            Console.WriteLine("Is this a paid course?: ");
            Console.WriteLine();
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");

            return GetPaidInfoChoice();
        }

        private static bool GetPaidInfoChoice()
        {
            var choice = new ConsoleKeyInfo();

            do
            {
                choice = Console.ReadKey(true);
                switch (choice.Key)
                {
                    case ConsoleKey.D1:
                        return true;

                    case ConsoleKey.D2:
                        return false;

                    default:
                        Console.WriteLine("\nUnsupported input, try again...\n");
                        continue;
                }
            } while (choice.Key != ConsoleKey.D1 || choice.Key != ConsoleKey.D2);

            return false;
        }

        public static TimeSpan GetCourseDuration(string courseDurationHhMmSs)
        {
            string value;
            TimeSpan time;

            Console.WriteLine("\nEnter course duration [hh:mm:ss]\n");
            do
            {
                value = Console.ReadLine();
            } while (!ValidateTimeValue(value, out time));

            return time;
        }

        private static bool ValidateTimeValue(string value, out TimeSpan time)
        {
            var values = value.Split(':');
            var convertedValues = new int[3];

            if (values.Length != 3)
            {
                Console.WriteLine("\nFormat error, all 3 values are required [hh:mm:ss], try again...\n");
                time = new TimeSpan();
                return false;
            }

            for (var i = 0; i < 3; i++)
            {
                if (!int.TryParse(values[i], out convertedValues[i]) || convertedValues[i] < 0)
                {
                    Console.WriteLine("\nValue error, one of the values provided was not a positive integer number [hh:mm:ss], try again...\n");
                    time = new TimeSpan();
                    return false;
                }
            }

            time = new TimeSpan(convertedValues[0], convertedValues[1], convertedValues[2]);
            return true;
        }

        public static Level GetDifficulty()
        {
            Console.WriteLine("Choose course difficulty level:");
            Console.WriteLine();
            Console.WriteLine("1. Beginner");
            Console.WriteLine("2. Intermediate");
            Console.WriteLine("3. Advanced");
            Console.WriteLine();

            return (Level)int.Parse(Console.ReadKey(true).KeyChar.ToString());
        }

        public static string GetGenericData(string message = null)
        {
            if (message != null)
            {
                Console.Write(message);
            }

            return Console.ReadLine();
        }
    }
}