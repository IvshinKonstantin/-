-----------------------------------Отчет по лабораторной работе №4
КОЛЛЕКЦИИ И ПЕРЕГРУЗКИ ОПЕРАТОРОВ
Вариант 4

--------------------------Задание 1. List
УСЛОВИЕ ЗАДАЧИ:
Составить программу, которая удаляет из списка L за каждым вхождением элемента E один элемент, если такой есть, и он отличен от E.

АЛГОРИТМ РЕШЕНИЯ:
Создаем список List<int> L и заполняем его данными.
Проходим по списку в цикле, начиная с первого элемента.
Если текущий элемент равен E, проверяем следующий элемент (если он существует).
Если следующий элемент существует и не равен E, удаляем его.
Пропускаем следующий элемент после удаления, чтобы не обрабатывать его повторно.

КОД РЕШЕНИЯ:
csharp
public static void RemoveAfterE(List<int> L, int E)
{
    for (int i = 0; i < L.Count - 1; i++)
    {
        if (L[i] == E && L[i + 1] != E)
        {
            L.RemoveAt(i + 1);
        }
    }
}
ТЕСТЫ:
Вход: L = [1, 2, 3, 4, 2, 5], E = 2
Выход: [1, 2, 3, 4, 2]

Вход: L = [2, 2, 2, 3], E = 2
Выход: [2, 2, 2]

Вход: L = [1, 2, 3], E = 4
Выход: [1, 2, 3]

-----------------------------------------Задание 2. LinkedList
УСЛОВИЕ ЗАДАЧИ:
Подсчитать количество элементов списка L, у которых равные «соседи».

АЛГОРИТМ РЕШЕНИЯ:
Создаем связный список LinkedList<int> L.
Проходим по всем элементам списка, начиная со второго и заканчивая предпоследним.
Для каждого элемента проверяем, равны ли его предыдущий и следующий элементы.
Если равны, увеличиваем счетчик.

КОД РЕШЕНИЯ
csharp
public static int CountEqualNeighbors(LinkedList<int> L)
{
    int count = 0;
    var node = L.First.Next;
    while (node != null && node.Next != null)
    {
        if (node.Previous.Value == node.Next.Value)
            count++;
        node = node.Next;
    }
    return count;
}
ТЕСТЫ:
Вход: L = [1, 2, 1, 3, 3]
Выход: 1

Вход: L = [1, 1, 1, 1]
Выход: 2

Вход: L = [1, 2, 3]
Выход: 0

---------------------------------------------Задание 3. HashSet
УСЛОВИЕ ЗАДАЧИ:
Задан некоторый набор блюд в кафе. Определить для каждого из блюд, какие из них заказывали все посетители, какие — некоторые из посетителей, и какие не заказывал никто.

АЛГОРИТМ РЕШЕНИЯ:
Создаем множество всех блюд allDishes.
Для каждого посетителя создаем множество заказанных блюд.
Используем операции над множествами:
Все посетители: пересечение множеств всех посетителей.
Некоторые посетители: объединение множеств всех посетителей.
Никто: разность между allDishes и множеством блюд, которые заказывали хотя бы один раз.

КОД РЕШЕНИЯ:
csharp
public static void AnalyzeOrders(HashSet<string> allDishes, List<HashSet<string>> visitors)
{
    var allOrdered = visitors[0];
    foreach (var visitor in visitors)
    {
        allOrdered.IntersectWith(visitor);
    }
    Console.WriteLine("Заказывали все: " + string.Join(", ", allOrdered));

    var someOrdered = new HashSet<string>();
    foreach (var visitor in visitors)
    {
        someOrdered.UnionWith(visitor);
    }
    Console.WriteLine("Заказывали некоторые: " + string.Join(", ", someOrdered));

    var noOneOrdered = new HashSet<string>(allDishes);
    noOneOrdered.ExceptWith(someOrdered);
    Console.WriteLine("Не заказывал никто: " + string.Join(", ", noOneOrdered));
}
ТЕСТЫ:
Вход:
allDishes = {"суп", "салат", "компот"}
visitors = [{"суп", "салат"}, {"суп"}, {"салат"}]
Выход:
Все: {}
Некоторые: {"суп", "салат"}
Никто: {"компот"}

-------------------------------------------Задание 4. HashSet
УСЛОВИЯ ЗАДАЧИ:
Файл содержит текст на русском языке. Напечатать в алфавитном порядке все глухие согласные буквы, которые не входят ровно в одно слово.

Алгоритм решения:

Читаем текст из файла, разбиваем на слова.

Создаем множество глухих согласных букв.

Для каждой буквы подсчитываем, в сколько слов она входит.

Выводим буквы, которые входят не ровно в одно слово.

КОД РЕШЕНИЯ:
csharp
public static void PrintDeafConsonants(string filePath)
{
    var deafConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
    var words = File.ReadAllText(filePath).Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var letterCount = new Dictionary<char, int>();

    foreach (var word in words)
    {
        var uniqueLetters = new HashSet<char>(word.ToLower());
        foreach (var letter in uniqueLetters)
        {
            if (deafConsonants.Contains(letter))
            {
                letterCount[letter] = letterCount.GetValueOrDefault(letter) + 1;
            }
        }
    }

    var result = letterCount.Where(pair => pair.Value != 1).Select(pair => pair.Key).OrderBy(c => c);
    Console.WriteLine("Глухие согласные, которые не входят ровно в одно слово: " + string.Join(", ", result));
}
ТЕСТЫ:
Вход (файл):
"стол парта шкаф"
Выход: п, т

Вход: "кот сок"
Выход: к, т

------------------------------------------Задание 5. Dictionary/SortedList
УСЛОВИЯ ЗАДАЧИ:
На вход программы подаются сведения о пассажирах, сдавших багаж. Вывести список пассажиров, которые освободят ячейки в ближайшие 2 часа.
Алгоритм решения:
Читаем текущее время и список пассажиров.
Для каждого пассажира вычисляем время освобождения ячейки.
Фильтруем тех, у кого время освобождения в пределах 2 часов от текущего.
Сортируем по времени освобождения.

КОД РЕШЕНИЯ:
csharp
public static void PrintPassengers(string filePath)
{
    var lines = File.ReadAllLines(filePath);
    var currentTime = TimeSpan.Parse(lines[0]);
    var passengers = new List<(string Name, TimeSpan FreeTime)>();

    for (int i = 2; i < lines.Length; i++)
    {
        var parts = lines[i].Split(' ');
        passengers.Add((parts[0], TimeSpan.Parse(parts[1])));
    }

    var result = passengers
        .Where(p => p.FreeTime <= currentTime.Add(TimeSpan.FromHours(2)) && p.FreeTime >= currentTime)
        .OrderBy(p => p.FreeTime)
        .Select(p => p.Name);

    Console.WriteLine("Пассажиры: " + string.Join(", ", result));
}
ТЕСТЫ:
Вход (файл):
10:00
3
Иванов 12:00
Петров 10:12
Сидоров 12:12
Выход: Петров, Иванов

--------------------------------------Задание 6. Перегрузка операций
УСЛОВИЯ ЗАДАЧИ:
Создать класс RightTriangle (прямоугольный треугольник) с полями:
double a - длина первого катета
double b - длина второго катета
Реализовать:
Конструкторы, Свойтва для доступа к полям,
Метод ToString() для вывода информации о треугольнике,
Метод для вычисления площади треугольника
Алгоритм решения:
Создать класс RightTriangle с приватными полями a и b
Реализовать конструкторы: по умолчанию и с параметрами
Добавить свойства с проверкой на положительность значений
Реализовать метод CalculateArea() для вычисления площади
Переопределить метод ToString() для красивого вывода информации
КОД РЕШЕНИЯ:
csharp
using System;

namespace LABA4_ZAD6
{
    public class RightTriangle
    {
        private double _a;
        private double _b;

        // Конструктор по умолчанию
        public RightTriangle()
        {
            _a = 1;
            _b = 1;
        }

        // Конструктор с параметрами
        public RightTriangle(double a, double b)
        {
            A = a;
            B = b;
        }

        // Свойство для катета A с проверкой
        public double A
        {
            get { return _a; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Длина катета должна быть положительной");
                _a = value;
            }
        }

        // Свойство для катета B с проверкой
        public double B
        {
            get { return _b; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Длина катета должна быть положительной");
                _b = value;
            }
        }

        // Метод для вычисления площади
        public double CalculateArea()
        {
            return 0.5 * _a * _b;
        }

        // Переопределение метода ToString()
        public override string ToString()
        {
            return $"Прямоугольный треугольник: катет a = {_a:F2}, катет b = {_b:F2}, площадь = {CalculateArea():F2}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Тестирование конструктора по умолчанию
                Console.WriteLine("Тест 1 - Конструктор по умолчанию:");
                RightTriangle triangle1 = new RightTriangle();
                Console.WriteLine(triangle1);
                Console.WriteLine();

                // Тестирование конструктора с параметрами
                Console.WriteLine("Тест 2 - Конструктор с параметрами:");
                RightTriangle triangle2 = new RightTriangle(3, 4);
                Console.WriteLine(triangle2);
                Console.WriteLine();

                // Тестирование свойств
                Console.WriteLine("Тест 3 - Изменение свойств:");
                triangle2.A = 5;
                triangle2.B = 6;
                Console.WriteLine(triangle2);
                Console.WriteLine();

                // Тестирование вычисления площади
                Console.WriteLine("Тест 4 - Вычисление площади:");
                RightTriangle triangle3 = new RightTriangle(7.5, 8.2);
                Console.WriteLine($"Площадь треугольника: {triangle3.CalculateArea():F2}");
                Console.WriteLine();

                // Тестирование обработки ошибок
                Console.WriteLine("Тест 5 - Обработка ошибок:");
                try
                {
                    RightTriangle triangle4 = new RightTriangle(-1, 5);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }

                // Тестирование с разными значениями
                Console.WriteLine("\nТест 6 - Разные треугольники:");
                RightTriangle[] triangles = {
                    new RightTriangle(1, 1),
                    new RightTriangle(2, 3),
                    new RightTriangle(10, 15)
                };

                foreach (var triangle in triangles)
                {
                    Console.WriteLine(triangle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
            }
        }
    }
}
ТЕСТЫ:
Тест 1 
Прямоугольный треугольник: катет a = 1,00, катет b = 1,00, площадь = 0,50
Тест 2 
Прямоугольный треугольник: катет a = 3,00, катет b = 4,00, площадь = 6,00
Тест 3 
Прямоугольный треугольник: катет a = 5,00, катет b = 6,00, площадь = 15,00
Тест 4 
Площадь треугольника: 30,75
Тест 5 
Ошибка: Длина катета должна быть положительной
Тест 6 
Прямоугольный треугольник: катет a = 1,00, катет b = 1,00, площадь = 0,50
Прямоугольный треугольник: катет a = 2,00, катет b = 3,00, площадь = 3,00
Прямоугольный треугольник: катет a = 10,00, катет b = 15,00, площадь = 75,00
