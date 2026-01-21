using System;
using System.Collections.Generic;

namespace CollectionsLab
{
    public static class HashSetTasks
    {
        public static void AnalyzeTouristCountries()
        {
            HashSet<string> countries = new HashSet<string> { "Франция", "Испания", "Италия", "Германия" };
            List<HashSet<string>> tourists = new List<HashSet<string>>
            {
                new HashSet<string> { "Франция", "Испания" },
                new HashSet<string> { "Испания", "Италия" },
                new HashSet<string> { "Италия", "Германия" }
            };

            AnalyzeCustomTouristCountries(countries, tourists);
        }

        public static void AnalyzeCustomTouristCountries(HashSet<string> countries, List<HashSet<string>> tourists)
        {
            int allTourists = tourists.Count;

            Console.WriteLine("Результаты анализа:");
            Console.WriteLine($"Всего туристов: {allTourists}");
            Console.WriteLine($"Всего стран в перечне: {countries.Count}");
            Console.WriteLine();

            List<string> sortedCountries = new List<string>(countries);
            sortedCountries.Sort();

            foreach (string country in sortedCountries)
            {
                int visitedByAll = 0;

                foreach (HashSet<string> tourist in tourists)
                {
                    if (tourist.Contains(country))
                    {
                        visitedByAll++;
                    }
                }

                int visitedBySome = visitedByAll > 0 ? visitedByAll : 0;
                int visitedByNone = allTourists - visitedByAll;

                Console.WriteLine($"Страна: {country}");
                Console.WriteLine($"  Посетили все: {visitedByAll}");
                Console.WriteLine($"  Посетили некоторые: {visitedBySome}");
                Console.WriteLine($"  Не посетил никто: {visitedByNone}");
                Console.WriteLine();
            }
        }
    }
}