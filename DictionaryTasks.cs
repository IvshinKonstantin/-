using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CollectionsLab
{
    public static class DictionaryTasks
    {
        public static void PrintFailedApplicants(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
                if (lines.Length == 0)
                {
                    Console.WriteLine("Файл пуст.");
                    return;
                }

                if (!int.TryParse(lines[0], out int n) || n <= 0)
                {
                    Console.WriteLine("Неверный формат первой строки.");
                    return;
                }

                List<string> failed = new List<string>();

                for (int i = 1; i <= n && i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length >= 4)
                    {
                        string lastName = parts[0];
                        string firstName = parts[1];

                        if (int.TryParse(parts[2], out int score1) &&
                            int.TryParse(parts[3], out int score2))
                        {
                            if (score1 < 30 || score2 < 30)
                            {
                                failed.Add($"{lastName} {firstName}");
                            }
                        }
                    }
                }

                Console.WriteLine("Не допущенные абитуриенты:");
                if (failed.Count == 0)
                {
                    Console.WriteLine("Таких абитуриентов нет");
                }
                else
                {
                    failed.Sort();
                    foreach (string person in failed)
                    {
                        Console.WriteLine(person);
                    }
                    Console.WriteLine($"Всего: {failed.Count} человек");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            }
        }
    }
}