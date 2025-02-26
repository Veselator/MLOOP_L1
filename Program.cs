using System.Formats.Tar;
using System.Text; // Для utf-8

namespace MLOOP_L1
{
    internal class Program
    {
        static int WINDOW_WIDTH = 210, WINDOW_HEIGHT = 50;
        // Кольори
        static string NORMAL = Console.IsOutputRedirected ? "" : "\x1b[39m";
        static string RED = Console.IsOutputRedirected ? "" : "\x1b[91m";
        static string GREEN = Console.IsOutputRedirected ? "" : "\x1b[92m";
        static string YELLOW = Console.IsOutputRedirected ? "" : "\x1b[93m";
        static string BLUE = Console.IsOutputRedirected ? "" : "\x1b[94m";
        static string MAGENTA = Console.IsOutputRedirected ? "" : "\x1b[95m";
        static string CYAN = Console.IsOutputRedirected ? "" : "\x1b[96m";
        static string GREY = Console.IsOutputRedirected ? "" : "\x1b[97m";
        static string BOLD = Console.IsOutputRedirected ? "" : "\x1b[1m";
        static string NOBOLD = Console.IsOutputRedirected ? "" : "\x1b[22m";
        static string UNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[4m";
        static string NOUNDERLINE = Console.IsOutputRedirected ? "" : "\x1b[24m";
        static string REVERSE = Console.IsOutputRedirected ? "" : "\x1b[7m";
        static string NOREVERSE = Console.IsOutputRedirected ? "" : "\x1b[27m";

        static void PressAnyKeyToContinue()
        {
            Console.WriteLine(" Натисніть будь-яку клавішу для продовження");
            Console.ReadKey();
        }

        static void PrintResult(double result)
        {
            Console.WriteLine($" Результат виконання програми: {result,6:f}");
            PressAnyKeyToContinue();
        }

        static void GetMark(int score)
        {
            switch (score)
            {
                case 7:
                    Console.WriteLine(" Геній");
                    break;
                case 6:
                    Console.WriteLine(" Ерудит");
                    break;
                case 5:
                    Console.WriteLine(" Нормальний");
                    break;
                case 4:
                    Console.WriteLine(" Здібності середні");
                    break;
                case 3:
                    Console.WriteLine(" Здібності нижче середнього");
                    break;
                default:
                    Console.WriteLine(" Вам треба відпочити!");
                    break;
            }
        }

        static void PrintTextFile(string FileName)
        {
            using (StreamReader readtext = new StreamReader(FileName))
            {
                while (!readtext.EndOfStream)
                {
                    Console.WriteLine(readtext.ReadLine());
                }
            }
        }

        static void Task1()
        {
            double x, a;
            Console.WriteLine();
            Console.Write(" Введіть значення x (за умовами x = 4,92)\n > ");
            if (!double.TryParse(Console.ReadLine(), out x)) { x = 4.92; }
            Console.WriteLine();

            Console.Write(" Введіть значення a (за умовами a = 0,88)\n > ");
            if (!double.TryParse(Console.ReadLine(), out a)) { a = 0.88; }
            Console.WriteLine();

            double result = Math.Log(Math.Abs(Math.Atan(x) - Math.Sin(x) * a)) + Math.Pow(3 * a * x, 1.0 / 3);
            PrintResult(result);
        }

        static double GetBorysNumber(double x)
        {
            return Math.Sqrt(Math.PI) * x * Math.Atan(x);
        }

        static void Task2()
        {
            Console.WriteLine("\n Маємо функцію:");

            double testValue = 1, xMin = 2, xMax = 5;
            using (StreamReader readtext = new StreamReader("LAB2.txt"))
            {
                try
                {
                    xMin = double.Parse(readtext.ReadLine());
                    xMax = double.Parse(readtext.ReadLine());
                }
                catch
                {
                    Console.WriteLine(" Щось пішло не так із зчитуванням даних, але розумний програміст передбавич це, і тому змінні отримують стандартні значення: xMin = 2, xMax = 5");
                }
            }
            double step = Math.Abs((xMax - xMin) / 8);
            double targetValue, targetResult;

            PrintTextFile("formula.txt");
            Console.WriteLine($"\n x = {testValue,6:f}: y = {GetBorysNumber(testValue),6:f}\n");
            using (StreamWriter writeText = new StreamWriter("LAB2.RES"))
            {
                writeText.WriteLine("Отримано");
                for (int i = 0; i < 8; i++)
                {
                    targetValue = xMin + step * i;
                    targetResult = GetBorysNumber(targetValue);
                    Console.WriteLine($" x = {targetValue,6:f}: y = {targetResult,6:f}");
                    writeText.WriteLine($"для заданої Y({targetValue,6:f}) = {targetResult,6:f}");
                }
                writeText.WriteLine("Розрахував студент Соломка Борис Олегович");
            }

            PressAnyKeyToContinue();
        }

        static void Task3()
        {
            int numOfQuestions = 7;
            int[] answers = { 1, 50, 2, 11, 30, 1, 1 };
            string[] questions = { "Професор ліг спати о 8 годині, а встав о 9 годині. Скільки годин проспав професор?",
            "На двох руках десять пальців. Скільки пальців на 10?",
            "Скільки цифр у дюжині?",
            "Скільки потрібно зробити розпилів, щоб розпиляти колоду на 12 частин?",
            "Лікар зробив три уколи в інтервалі 30 хвилин. Скільки часу він витратив?",
            "Скільки цифр 9 в інтервалі 1100?",
            "Пастух мав 30 овець. Усі, окрім однієї, розбіглися. Скільки овець лишилося?"
            };
            int currentAnswer;
            int numOfCorrectAnswers = 0;
            int topScore = 0;

            Console.WriteLine("\n Перевір свої можливості!\n");

            for (int i = 0; i < numOfQuestions; i++)
            {
                Console.Write($" {i + 1}) {questions[i]}\n > ");
                if (!int.TryParse(Console.ReadLine(), out currentAnswer) || currentAnswer != answers[i])
                {
                    Console.WriteLine($" {RED}Неправильно!{NORMAL}");
                    continue;
                }

                numOfCorrectAnswers++;
                Console.WriteLine($" {GREEN}Правильно!{NORMAL}");
            }

            Console.WriteLine($"\n Твій результат: {numOfCorrectAnswers}");
            GetMark(numOfCorrectAnswers);

            // Система рекордів
            try
            {
                using (StreamReader strReader = new StreamReader("topscore.txt"))
                {
                    if (!int.TryParse(strReader.ReadLine(), out topScore))
                    {
                        topScore = numOfCorrectAnswers;
                    }
                    else
                    {
                        topScore = topScore > numOfCorrectAnswers ? topScore : numOfCorrectAnswers;
                    }
                }
            }
            catch
            {
                topScore = numOfCorrectAnswers;
            }
            finally
            {
                using (StreamWriter strWriter = new StreamWriter("topscore.txt", false))
                {
                    strWriter.WriteLine(topScore);
                }
                Console.WriteLine($" Рекорд: {topScore}");
            }

            PressAnyKeyToContinue();
        }

        static void Task4()
        {
            double maxFuelCapacity, distAB, distBC, mass, consumptionPerKM;
            using (StreamReader strReader = new StreamReader("LAB4.txt"))
            {
                if (!double.TryParse(strReader.ReadLine(), out maxFuelCapacity)) { maxFuelCapacity = 4; }
                strReader.ReadLine();

                if (!double.TryParse(strReader.ReadLine(), out distAB)) { distAB = 4; }
                strReader.ReadLine();

                if (!double.TryParse(strReader.ReadLine(), out distBC)) { distBC = 4; }
                strReader.ReadLine();

                if (!double.TryParse(strReader.ReadLine(), out mass)) { mass = 4; }
            }

            consumptionPerKM = mass < 500.0 ? 1 : (mass < 1000.0 ? 4 : (mass < 1500.0 ? 7 : (mass < 2000.0 ? 9 : 0)));
            double fuelA2B = distAB * consumptionPerKM;
            double fuelB2C = distBC * consumptionPerKM;

            PrintTextFile("airplane.txt");
            int cursorX = Console.CursorLeft, cursorY = Console.CursorTop;

            Console.SetCursorPosition(57, 29);
            Console.Write($"{GREEN}A -> B = {distAB} км");
            Console.SetCursorPosition(57, 30);
            Console.Write($"Палива необхідно: {fuelA2B} л");

            Console.SetCursorPosition(132, 26);
            Console.Write($"B -> C = {distBC} км");
            Console.SetCursorPosition(132, 27);
            Console.Write($"Палива необхідно: {fuelB2C} л");

            Console.SetCursorPosition(14, 26);
            Console.Write($"Вага вантажу: {mass} кг");
            Console.SetCursorPosition(14, 27);
            Console.Write($"Макс. місткість палива: {maxFuelCapacity} л");
            Console.SetCursorPosition(14, 28);
            Console.Write($"Споживання палива: {consumptionPerKM} л/км{NORMAL}");

            Console.SetCursorPosition(cursorX, cursorY);

            if (consumptionPerKM == 0)
            {
                Console.WriteLine($" {RED}Занадто великий вантаж!\n Літак не взлетить!{NORMAL}");
                PressAnyKeyToContinue();
                return;
            }

            if (fuelA2B > maxFuelCapacity)
            {
                Console.WriteLine($" {RED}Максимального палива не вистачіть для переліту із пункту A в пункт B!{NORMAL}");
                PressAnyKeyToContinue();
                return;
            }

            double fuelLeft = maxFuelCapacity - fuelA2B;
            if (fuelLeft > fuelB2C)
            {
                Console.WriteLine($" {GREEN}Павила достатньо для того, щоб долетіти із пункту A в пункт C без дозаправки в пункті B!{NORMAL}");
                PressAnyKeyToContinue();
                return;
            }

            if (fuelB2C > maxFuelCapacity)
            {
                Console.WriteLine($" {RED}Максимального палива не вистачіть для переліту із пункту B в пункт C!{NORMAL}");
                PressAnyKeyToContinue();
                return;
            }

            double fuelNeedToFill = fuelB2C - fuelLeft;

            Console.WriteLine($" Необхідно дозаправитись в пункті B на {GREEN}{fuelNeedToFill}{NORMAL} літрів для того, щоб долетіти із пункта A в пункт C.");
            PressAnyKeyToContinue();
        }

        static void Setup()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(NORMAL);

            Console.OutputEncoding = Encoding.UTF8;

            Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);
        }

        static void PrintTitle(string date, int numOfLaboratory, string title)
        {
            Console.WriteLine($"\n " + date);
            Console.WriteLine($" Лабораторна робота №{numOfLaboratory}");
            Console.WriteLine($" Тема: {title}");
            Console.WriteLine($" Виконав Соломка Борис");
        }

        static void Main(string[] args)
        {
            Setup();
            PrintTitle($"25.02.2025", 1, $"Програмування лінійних алгоритмів та алгоритмів, що розгалуджуються");

            bool isRunning = true;
            while (isRunning)
            {
                Console.Write($"\n Введіть відповідний номер:\n {UNDERLINE}0){NOUNDERLINE} Вихід;\n " +
                    $"{UNDERLINE}1){NOUNDERLINE} Завдання №1.1\n " +
                    $"{UNDERLINE}2){NOUNDERLINE} Завдання №1.2\n " +
                    $"{UNDERLINE}3){NOUNDERLINE} Завдання №1.3\n " +
                    $"{UNDERLINE}4){NOUNDERLINE} Завдання №1.4\n > ");
                int userChoice;
                if (!int.TryParse(Console.ReadLine(), out userChoice)) { break; }
                Console.Clear();

                switch (userChoice)
                {
                    case 0:
                        isRunning = false;
                        break;
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    case 4:
                        Task4();
                        break;
                    default:
                        Console.WriteLine(" Введено некоректне число.");
                        break;
                }
                Console.Clear();
            }
        }
    }
}
