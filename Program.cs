using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CollectionsLab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool exit = false;

            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №4 - КОЛЛЕКЦИИ ===");
                Console.WriteLine("=== Вариант 4 ===\n");
                Console.WriteLine("Выберите задание для выполнения:");
                Console.WriteLine("1. List - Удаление элементов после E");
                Console.WriteLine("2. LinkedList - Поиск равных соседей");
                Console.WriteLine("3. HashSet - Анализ посещения стран");
                Console.WriteLine("4. HashSet - Анализ текста");
                Console.WriteLine("5. Dictionary - Анализ абитуриентов");
                Console.WriteLine("6. Выход");
                Console.Write("\nВаш выбор (1-6): ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ListTasksMenu();
                        break;
                    case "2":
                        LinkedListTasksMenu();
                        break;
                    case "3":
                        HashSetTasksMenu();
                        break;
                    case "4":
                        TextHashSetTasksMenu();
                        break;
                    case "5":
                        DictionaryTasksMenu();
                        break;
                    case "6":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ListTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАНИЕ 1: List ===");
            Console.WriteLine("Удалить из списка за каждым вхождением элемента E один элемент,\nесли такой есть, и он отличен от E\n");

            Console.WriteLine("1. Использовать тестовые данные");
            Console.WriteLine("2. Ввести данные вручную");
            Console.Write("\nВаш выбор (1-2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Тестовые данные
                List<int> list = new List<int> { 1, 2, 2, 3, 2, 4, 2, 5 };
                int E = 2;

                Console.WriteLine("\nТестовые данные:");
                Console.Write("Список: ");
                PrintList(list);
                Console.WriteLine($"Элемент E: {E}");

                List<int> result = ListTasks.RemoveAfterEachE(list, E);

                Console.Write("\nРезультат: ");
                PrintList(result);
            }
            else if (choice == "2")
            {
                Console.WriteLine("\n=== Ввод данных ===");

                // Ввод списка
                List<int> list = new List<int>();
                Console.WriteLine("Введите элементы списка через пробел (целые числа):");
                Console.Write("> ");
                string input = Console.ReadLine();

                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int num))
                    {
                        list.Add(num);
                    }
                }

                if (list.Count == 0)
                {
                    Console.WriteLine("Список пуст. Используем тестовые данные.");
                    list = new List<int> { 1, 2, 2, 3, 2, 4, 2, 5 };
                }

                // Ввод элемента E
                Console.Write("\nВведите элемент E (целое число): ");
                if (!int.TryParse(Console.ReadLine(), out int E))
                {
                    Console.WriteLine("Неверный ввод. Используем E = 2.");
                    E = 2;
                }

                Console.WriteLine("\nИсходные данные:");
                Console.Write("Список: ");
                PrintList(list);
                Console.WriteLine($"Элемент E: {E}");

                List<int> result = ListTasks.RemoveAfterEachE(list, E);

                Console.Write("\nРезультат: ");
                PrintList(result);
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void LinkedListTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАНИЕ 2: LinkedList ===");
            Console.WriteLine("Определить, есть ли в списке хотя бы один элемент,\nкоторый равен следующему за ним (по кругу)\n");

            Console.WriteLine("1. Использовать тестовые данные");
            Console.WriteLine("2. Ввести данные вручную");
            Console.Write("\nВаш выбор (1-2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                // Тестовые данные
                LinkedList<int> list = new LinkedList<int>();
                list.AddLast(1);
                list.AddLast(2);
                list.AddLast(3);
                list.AddLast(4);
                list.AddLast(1);

                Console.WriteLine("\nТестовые данные:");
                Console.Write("Список: ");
                PrintLinkedList(list);

                bool result = LinkedListTasks.HasEqualNeighbors(list);
                Console.WriteLine($"Есть ли соседние равные элементы (по кругу)? {result}");
            }
            else if (choice == "2")
            {
                Console.WriteLine("\n=== Ввод данных ===");

                LinkedList<int> list = new LinkedList<int>();
                Console.WriteLine("Введите элементы списка через пробел (целые числа):");
                Console.Write("> ");
                string input = Console.ReadLine();

                string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int num))
                    {
                        list.AddLast(num);
                    }
                }

                if (list.Count == 0)
                {
                    Console.WriteLine("Список пуст. Используем тестовые данные.");
                    list.AddLast(1);
                    list.AddLast(2);
                    list.AddLast(3);
                    list.AddLast(4);
                    list.AddLast(1);
                }

                Console.WriteLine("\nИсходные данные:");
                Console.Write("Список: ");
                PrintLinkedList(list);

                bool result = LinkedListTasks.HasEqualNeighbors(list);
                Console.WriteLine($"Есть ли соседние равные элементы (по кругу)? {result}");
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void HashSetTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАНИЕ 3: HashSet ===");
            Console.WriteLine("Анализ посещения стран туристами\n");

            Console.WriteLine("1. Использовать тестовые данные");
            Console.WriteLine("2. Ввести данные вручную");
            Console.Write("\nВаш выбор (1-2): ");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.WriteLine("\nТестовые данные:");
                Console.WriteLine("Страны: Франция, Испания, Италия, Германия");
                Console.WriteLine("Турист 1: Франция, Испания");
                Console.WriteLine("Турист 2: Испания, Италия");
                Console.WriteLine("Турист 3: Италия, Германия");
                Console.WriteLine();

                HashSetTasks.AnalyzeTouristCountries();
            }
            else if (choice == "2")
            {
                Console.WriteLine("\n=== Ввод данных ===");

                // Ввод стран
                HashSet<string> countries = new HashSet<string>();
                Console.WriteLine("Введите названия стран через запятую:");
                Console.Write("> ");
                string countriesInput = Console.ReadLine();

                string[] countryParts = countriesInput.Split(',', ';', StringSplitOptions.RemoveEmptyEntries);
                foreach (string country in countryParts)
                {
                    string trimmed = country.Trim();
                    if (!string.IsNullOrEmpty(trimmed))
                    {
                        countries.Add(trimmed);
                    }
                }

                if (countries.Count == 0)
                {
                    Console.WriteLine("Список стран пуст. Используем тестовые данные.");
                    countries = new HashSet<string> { "Франция", "Испания", "Италия", "Германия" };
                }

                // Ввод данных о туристах
                List<HashSet<string>> tourists = new List<HashSet<string>>();
                Console.WriteLine("\nСколько туристов? (введите число):");
                Console.Write("> ");
                if (int.TryParse(Console.ReadLine(), out int touristCount) && touristCount > 0)
                {
                    for (int i = 0; i < touristCount; i++)
                    {
                        Console.WriteLine($"\nТурист {i + 1}. Введите страны через запятую:");
                        Console.Write("> ");
                        string touristInput = Console.ReadLine();

                        HashSet<string> touristCountries = new HashSet<string>();
                        string[] touristParts = touristInput.Split(',', ';', StringSplitOptions.RemoveEmptyEntries);
                        foreach (string country in touristParts)
                        {
                            string trimmed = country.Trim();
                            if (!string.IsNullOrEmpty(trimmed))
                            {
                                touristCountries.Add(trimmed);
                            }
                        }

                        tourists.Add(touristCountries);
                    }
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Используем тестовые данные.");
                    tourists = new List<HashSet<string>>
                    {
                        new HashSet<string> { "Франция", "Испания" },
                        new HashSet<string> { "Испания", "Италия" },
                        new HashSet<string> { "Италия", "Германия" }
                    };
                }

                // Анализ
                Console.WriteLine("\nРезультат анализа:");
                HashSetTasks.AnalyzeCustomTouristCountries(countries, tourists);
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void TextHashSetTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАНИЕ 4: HashSet с текстом ===");
            Console.WriteLine("Напечатать в алфавитном порядке все глухие согласные буквы,\nкоторые не входят ровно в одно слово\n");

            Console.WriteLine("1. Использовать тестовый текст");
            Console.WriteLine("2. Ввести текст вручную");
            Console.WriteLine("3. Загрузить текст из файла");
            Console.Write("\nВаш выбор (1-3): ");

            string choice = Console.ReadLine();
            string text = "";

            if (choice == "1")
            {
                text = "Пример текста с разными словами. Текст содержит разные буквы!";
                Console.WriteLine("\nТестовый текст:");
                Console.WriteLine(text);
                Console.WriteLine();

                // Сохраняем в файл для обработки
                File.WriteAllText("temp_text.txt", text, Encoding.UTF8);
                TextHashSetTasks.PrintConsonantsNotInExactlyOneWord("temp_text.txt");
                File.Delete("temp_text.txt");
            }
            else if (choice == "2")
            {
                Console.WriteLine("\nВведите текст (русский язык):");
                Console.WriteLine("(Для завершения ввода введите пустую строку)");
                Console.WriteLine("Начинайте ввод:");

                StringBuilder sb = new StringBuilder();
                string line;
                while (!string.IsNullOrEmpty(line = Console.ReadLine()))
                {
                    sb.AppendLine(line);
                }

                text = sb.ToString();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("Текст не введен. Используем тестовый текст.");
                    text = "Пример текста с разными словами. Текст содержит разные буквы!";
                }

                Console.WriteLine("\nВведенный текст:");
                Console.WriteLine(text);
                Console.WriteLine();

                // Сохраняем в файл для обработки
                File.WriteAllText("temp_text.txt", text, Encoding.UTF8);
                TextHashSetTasks.PrintConsonantsNotInExactlyOneWord("temp_text.txt");
                File.Delete("temp_text.txt");
            }
            else if (choice == "3")
            {
                Console.WriteLine("\nВведите путь к файлу:");
                Console.Write("> ");
                string filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    text = File.ReadAllText(filePath, Encoding.UTF8);
                    Console.WriteLine("\nСодержимое файла:");
                    Console.WriteLine(text);
                    Console.WriteLine();

                    TextHashSetTasks.PrintConsonantsNotInExactlyOneWord(filePath);
                }
                else
                {
                    Console.WriteLine("Файл не найден. Используем тестовый текст.");
                    text = "Пример текста с разными словами. Текст содержит разные буквы!";
                    File.WriteAllText("temp_text.txt", text, Encoding.UTF8);
                    TextHashSetTasks.PrintConsonantsNotInExactlyOneWord("temp_text.txt");
                    File.Delete("temp_text.txt");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void DictionaryTasksMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАДАНИЕ 5: Dictionary ===");
            Console.WriteLine("Вывести фамилии абитуриентов, не допущенных к экзаменам\n(оба балла <30), в алфавитном порядке\n");

            Console.WriteLine("1. Использовать тестовые данные");
            Console.WriteLine("2. Ввести данные вручную");
            Console.WriteLine("3. Загрузить данные из файла");
            Console.Write("\nВаш выбор (1-3): ");

            string choice = Console.ReadLine();
            string[] lines = null;

            if (choice == "1")
            {
                lines = new string[]
                {
                    "4",
                    "Иванов Петр 25 35",
                    "Петрова Анна 29 40",
                    "Сидоров Иван 20 25",
                    "Козлова Мария 45 28"
                };

                Console.WriteLine("\nТестовые данные:");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                }
                Console.WriteLine();

                File.WriteAllLines("temp_applicants.txt", lines, Encoding.UTF8);
                DictionaryTasks.PrintFailedApplicants("temp_applicants.txt");
                File.Delete("temp_applicants.txt");
            }
            else if (choice == "2")
            {
                Console.WriteLine("\n=== Ввод данных ===");
                Console.WriteLine("Введите количество абитуриентов:");
                Console.Write("> ");

                if (int.TryParse(Console.ReadLine(), out int n) && n > 0)
                {
                    List<string> data = new List<string> { n.ToString() };

                    for (int i = 0; i < n; i++)
                    {
                        Console.WriteLine($"\nАбитуриент {i + 1}:");
                        Console.Write("Фамилия: ");
                        string lastName = Console.ReadLine();

                        Console.Write("Имя: ");
                        string firstName = Console.ReadLine();

                        Console.Write("Балл по предмету 1: ");
                        string score1 = Console.ReadLine();

                        Console.Write("Балл по предмету 2: ");
                        string score2 = Console.ReadLine();

                        data.Add($"{lastName} {firstName} {score1} {score2}");
                    }

                    lines = data.ToArray();

                    Console.WriteLine("\nВведенные данные:");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine();

                    File.WriteAllLines("temp_applicants.txt", lines, Encoding.UTF8);
                    DictionaryTasks.PrintFailedApplicants("temp_applicants.txt");
                    File.Delete("temp_applicants.txt");
                }
                else
                {
                    Console.WriteLine("Неверный ввод. Используем тестовые данные.");
                    lines = new string[]
                    {
                        "4",
                        "Иванов Петр 25 35",
                        "Петрова Анна 29 40",
                        "Сидоров Иван 20 25",
                        "Козлова Мария 45 28"
                    };

                    File.WriteAllLines("temp_applicants.txt", lines, Encoding.UTF8);
                    DictionaryTasks.PrintFailedApplicants("temp_applicants.txt");
                    File.Delete("temp_applicants.txt");
                }
            }
            else if (choice == "3")
            {
                Console.WriteLine("\nВведите путь к файлу:");
                Console.Write("> ");
                string filePath = Console.ReadLine();

                if (File.Exists(filePath))
                {
                    lines = File.ReadAllLines(filePath, Encoding.UTF8);
                    Console.WriteLine("\nСодержимое файла:");
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                    Console.WriteLine();

                    DictionaryTasks.PrintFailedApplicants(filePath);
                }
                else
                {
                    Console.WriteLine("Файл не найден. Используем тестовые данные.");
                    lines = new string[]
                    {
                        "4",
                        "Иванов Петр 25 35",
                        "Петрова Анна 29 40",
                        "Сидоров Иван 20 25",
                        "Козлова Мария 45 28"
                    };

                    File.WriteAllLines("temp_applicants.txt", lines, Encoding.UTF8);
                    DictionaryTasks.PrintFailedApplicants("temp_applicants.txt");
                    File.Delete("temp_applicants.txt");
                }
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void PrintList<T>(List<T> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                Console.Write(list[i]);
                if (i < list.Count - 1) Console.Write(", ");
            }
            Console.WriteLine();
        }

        static void PrintLinkedList<T>(LinkedList<T> list)
        {
            LinkedListNode<T> node = list.First;
            while (node != null)
            {
                Console.Write(node.Value);
                if (node.Next != null) Console.Write(", ");
                node = node.Next;
            }
            Console.WriteLine();
        }
    }
}