using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CollectionsLab
{
    public static class TextHashSetTasks
    {
        public static void PrintConsonantsNotInExactlyOneWord(string filePath)
        {
            string text = File.ReadAllText(filePath, Encoding.UTF8).ToLower();

            char[] separators = { ' ', ',', '.', '!', '?', ';', ':', '\n', '\r', '\t', '(', ')', '[', ']', '{', '}', '"', '\'' };
            string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            HashSet<char> deafConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };

            Dictionary<char, int> charInWordCount = new Dictionary<char, int>();

            foreach (string word in words)
            {
                HashSet<char> uniqueChars = new HashSet<char>();
                foreach (char c in word)
                {
                    if (IsRussianLetter(c))
                    {
                        uniqueChars.Add(c);
                    }
                }

                foreach (char ch in uniqueChars)
                {
                    if (charInWordCount.ContainsKey(ch))
                    {
                        charInWordCount[ch]++;
                    }
                    else
                    {
                        charInWordCount[ch] = 1;
                    }
                }
            }

            List<char> result = new List<char>();
            foreach (char consonant in deafConsonants)
            {
                if (!charInWordCount.ContainsKey(consonant) || charInWordCount[consonant] != 1)
                {
                    result.Add(consonant);
                }
            }

            result.Sort();

            Console.Write("Глухие согласные, не входящие ровно в одно слово: ");
            if (result.Count == 0)
            {
                Console.WriteLine("нет таких букв");
            }
            else
            {
                for (int i = 0; i < result.Count; i++)
                {
                    Console.Write(result[i]);
                    if (i < result.Count - 1) Console.Write(", ");
                }
                Console.WriteLine($" (всего: {result.Count})");
            }
        }

        private static bool IsRussianLetter(char c)
        {
            return (c >= 'а' && c <= 'я') || c == 'ё';
        }
    }
}