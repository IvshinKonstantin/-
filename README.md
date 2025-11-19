markdown
# Лабораторная работа №4 - Вариант 4
## Коллекции и перегрузка операторов

## СОДЕРЖАНИЕ
- [Задание 1: List](#задание-1-list)
- [Задание 2: LinkedList](#задание-2-linkedlist)
- [Задание 3: HashSet](#задание-3-hashset)
- [Задание 4: HashSet с текстом](#задание-4-hashset-с-текстом)
- [Задание 5: Dictionary](#задание-5-dictionary)
- [Задание 6: Класс RightTriangle](#задание-6-класс-righttriangle)
- [Задание 7: Перегрузка операторов](#задание-7-перегрузка-операторов)

---

## ОБЩЕЕ ОПИСАНИЕ

Лабораторная работа состоит из двух частей:
- **Задания 1-5**: Работа с коллекциями (List, LinkedList, HashSet, Dictionary)
- **Задания 6-7**: Создание класса с перегрузкой операторов

---

## ЗАДАНИЕ 1: LIST

### Описание задачи
**Составить программу, которая удаляет из списка L за каждым вхождением элемента E один элемент, если такой есть, и он отличен от E.**

### Алгоритм решения
1. Создать новый список для результата
2. Пройти по всем элементам исходного списка
3. Добавлять каждый текущий элемент в результат
4. Если текущий элемент равен E и следующий элемент существует и не равен E - пропустить следующий элемент
5. Вернуть результирующий список

### Код метода
```csharp
public static List<T> RemoveAfterElement<T>(List<T> list, T element)
{
    var result = new List<T>();
    for (int i = 0; i < list.Count; i++)
    {
        result.Add(list[i]);
        if (EqualityComparer<T>.Default.Equals(list[i], element) && 
            i + 1 < list.Count && 
            !EqualityComparer<T>.Default.Equals(list[i + 1], element))
        {
            i++;
        }
    }
    return result;
}
Пример использования
text
Входные данные: [1, 2, 3, 2, 4, 5, 2, 6], E = 2
Результат: [1, 2, 3, 2, 5, 2]
```
ЗАДАНИЕ 2: LINKEDLIST
Описание задачи
Определить, есть ли в списке L хотя бы один элемент, который равен следующему за ним (по кругу) элементу (первый элемент считать следующим для последнего).

Алгоритм решения
Пройти по всем элементам связного списка

Для каждого элемента найти следующий элемент (если текущий последний, то следующий - первый)

Сравнить текущий элемент со следующим

Если найдены равные соседи - вернуть true

Если не найдены - вернуть false

Код метода
```csharp
public static bool HasEqualNeighborsCircular<T>(LinkedList<T> list)
{
    var current = list.First;
    while (current != null)
    {
        var next = current.Next ?? list.First;
        if (EqualityComparer<T>.Default.Equals(current.Value, next.Value))
            return true;
        current = current.Next;
    }
    return false;
}
Пример использования
text
Входные данные: [1, 2, 3, 4, 1]
Результат: True (1 == 1 по кругу)
```
ЗАДАНИЕ 3: HASHSET
Описание задачи
Есть перечень стран, популярных у туристов. Определить для каждой страны, какие из них посетили все n туристов, какие — некоторые из туристов, и какие — никто из туристов.

Алгоритм решения
Для каждой страны из общего перечня:

Проверить, посещена ли она всеми туристами (All)

Проверить, посещена ли она хотя бы одним туристом (Any)

Определить категорию страны и вывести результат

Код метода
```csharp
public static void AnalyzeCountryVisits(HashSet<string> allCountries, 
                                      List<HashSet<string>> touristVisits)
{
    foreach (var country in allCountries)
    {
        var visitedByAll = touristVisits.All(visits => visits.Contains(country));
        var visitedBySome = touristVisits.Any(visits => visits.Contains(country));
        
        if (visitedByAll)
            Console.WriteLine($"{country}: посещена всеми туристами");
        else if (visitedBySome)
            Console.WriteLine($"{country}: посещена некоторыми туристами");
        else
            Console.WriteLine($"{country}: не посещена никем");
    }
}
Пример использования
text
Страны: Франция, Италия, Испания
Турист 1: Франция, Италия
Турист 2: Италия, Испания

Результат:
Франция: посещена некоторыми туристами
Италия: посещена всеми туристами  
Испания: посещена некоторыми туристами
```
ЗАДАНИЕ 4: HASHSET С ТЕКСТОМ
Описание задачи
Файл содержит текст на русском языке. Напечатать в алфавитном порядке все глухие согласные буквы, которые не входят ровно в одно слово.

Алгоритм решения
Определить множество глухих согласных букв

Разбить текст на слова

Для каждой согласной подсчитать, в скольких словах она встречается

Отфильтровать согласные, которые встречаются не ровно в одном слове

Отсортировать и вывести результат

Код метода
```csharp
public static void PrintVoicelessConsonants(string filePath)
{
    var voicelessConsonants = new HashSet<char> { 'п', 'ф', 'к', 'т', 'ш', 'с', 'х', 'ц', 'ч', 'щ' };
    var words = File.ReadAllText(filePath).ToLower().Split(...);
    
    var consonantCount = new Dictionary<char, int>();
    foreach (var consonant in voicelessConsonants)
    {
        consonantCount[consonant] = words.Count(word => word.Contains(consonant));
    }
    
    var result = consonantCount
        .Where(pair => pair.Value != 1)
        .Select(pair => pair.Key)
        .OrderBy(c => c);
    
    foreach (var consonant in result)
    {
        Console.Write(consonant + " ");
    }
}
Пример использования
text
Текст: "пример текста для анализа согласных"
Результат: к п с т х ш
```
ЗАДАНИЕ 5: DICTIONARY
Описание задачи
На вход программы подаются сведения о пассажирах, сдавших свой багаж в камеру хранения. Вывести список пассажиров, которые в ближайшие 2 часа должны освободить ячейки.

Алгоритм решения
Получить текущее время

Вычислить время через 2 часа

Отфильтровать пассажиров с временем освобождения в интервале [текущее время, текущее время + 2 часа]

Отсортировать по времени освобождения

Вывести результат

Код метода
```csharp
public static void PrintPassengersWithSoonExpiry(string currentTimeStr, List<Passenger> passengers)
{
    var currentTime = TimeSpan.Parse(currentTimeStr);
    var twoHoursLater = currentTime.Add(TimeSpan.FromHours(2));
    
    var soonExpiring = passengers
        .Where(p => p.ExpiryTime >= currentTime && p.ExpiryTime <= twoHoursLater)
        .OrderBy(p => p.ExpiryTime)
        .ToList();
    
    foreach (var passenger in soonExpiring)
    {
        Console.WriteLine($"{passenger.Name} - {passenger.ExpiryTime:hh\\:mm}");
    }
}
```
Пример использования
text
Текущее время: 10:00
Пассажиры:
- Иванов 10:30
- Петров 11:45  
- Сидоров 12:15

Результат:
Иванов - 10:30
Петров - 11:45
ЗАДАНИЕ 6: КЛАСС RIGHTTRIANGLE
Описание задачи
Создать класс RightTriangle для работы с прямоугольными треугольниками.

Поля: double a, double b (длины катетов)

Конструкторы (по умолчанию, с параметрами, копирования)

Свойства с валидацией

Метод CalculateArea() - вычисление площади

Перегрузка ToString()

Математические формулы
Площадь: S = (a × b) / 2

Гипотенуза: c = √(a² + b²)

Существование: a > 0 && b > 0

Основной код класса
```csharp
public class RightTriangle
{
    private double _a;
    private double _b;

    public double A
    {
        get => _a;
        set
        {
            if (value <= 0) throw new ArgumentException("Длина катета должна быть положительной");
            _a = value;
        }
    }

    public double B
    {
        get => _b;
        set
        {
            if (value <= 0) throw new ArgumentException("Длина катета должна быть положительной");
            _b = value;
        }
    }

    // Конструкторы
    public RightTriangle() : this(1.0, 1.0) { }
    public RightTriangle(double a, double b)
    {
        if (a <= 0 || b <= 0) throw new ArgumentException("Длины катетов должны быть положительными");
        _a = a;
        _b = b;
    }

    // Методы
    public double CalculateArea() => (_a * _b) / 2.0;
    public double CalculateHypotenuse() => Math.Sqrt(_a * _a + _b * _b);
    public bool Exists() => _a > 0 && _b > 0;

    public override string ToString() => 
        $"RightTriangle [a={_a:F2}, b={_b:F2}, площадь={CalculateArea():F2}]";
}
```
Пример использования
```csharp
var triangle = new RightTriangle(3.0, 4.0);
Console.WriteLine(triangle); // RightTriangle [a=3.00, b=4.00, площадь=6.00]
Console.WriteLine(triangle.CalculateArea()); // 6.00
Console.WriteLine(triangle.CalculateHypotenuse()); // 5.00
```
ЗАДАНИЕ 7: ПЕРЕГРУЗКА ОПЕРАТОРОВ
Описание задачи
Добавить перегрузку операторов для класса RightTriangle:

Унарные операции:

++ - увеличение сторон в 2 раза

-- - уменьшение сторон в 2 раза

Операции приведения типа:

explicit double - площадь треугольника

implicit bool - существование треугольника

Бинарные операции:

<= - сравнение площадей

>= - сравнение площадей

Код перегрузки операторов
```csharp
// Унарные операции
public static RightTriangle operator ++(RightTriangle triangle)
{
    double newA = triangle._a * 2;
    double newB = triangle._b * 2;
    return new RightTriangle(newA, newB);
}

public static RightTriangle operator --(RightTriangle triangle)
{
    double newA = triangle._a / 2;
    double newB = triangle._b / 2;
    return new RightTriangle(newA, newB);
}

// Приведение типов
public static explicit operator double(RightTriangle triangle) => 
    triangle.Exists() ? triangle.CalculateArea() : -1;

public static implicit operator bool(RightTriangle triangle) => 
    triangle?.Exists() ?? false;

// Бинарные операции сравнения
public static bool operator <=(RightTriangle t1, RightTriangle t2) => 
    t1.CalculateArea() <= t2.CalculateArea();

public static bool operator >=(RightTriangle t1, RightTriangle t2) => 
    t1.CalculateArea() >= t2.CalculateArea();
Примеры использования операторов
csharp
var triangle1 = new RightTriangle(3.0, 4.0);
var triangle2 = new RightTriangle(6.0, 8.0);

// Унарные операции
var doubled = triangle1++; // 6×8 (площадь 24)
var halved = triangle1--;  // 1.5×2 (площадь 1.5)

// Приведение типов
double area = (double)triangle1; // 6.00
bool exists = triangle1;         // true

// Сравнение
bool lessOrEqual = triangle1 <= triangle2; // true
bool greaterOrEqual = triangle1 >= triangle2; // false
```
ТЕСТИРОВАНИЕ
Тесты для заданий 1-5:
Тест 1:

text
Вход: [1, 2, 3, 2, 4, 5, 2, 6], E=2
Выход: [1, 2, 3, 2, 5, 2]
Тест 2:

text
Вход: [1, 2, 3, 4, 1]
Выход: True (равные соседи по кругу)
Тест 3:

text
Страны: Франция, Италия, Испания
Туристы: 
  - [Франция, Италия]
  - [Италия, Испания]
Выход:
  Франция: некоторые
  Италия: все
  Испания: некоторые
Тест 4:

text
Текст: "пример текста для анализа"
Выход: к п с т х ш
Тест 5:

text
Время: 10:00
Пассажиры: Иванов 10:30, Петров 11:45, Сидоров 12:15
Выход: Иванов, Петров
Тесты для заданий 6-7:
Тест 6:

text
Треугольник 3×4:
Площадь: 6.00
Гипотенуза: 5.00
Существует: True
Тест 7:

text
Исходный: 3×4 (площадь 6)
После ++: 6×8 (площадь 24)
После --: 1.5×2 (площадь 1.5)
Сравнение: 3×4 <= 6×8: True
ИНСТРУКЦИЯ ПО ЗАПУСКУ
Для заданий 1-5:
Запустите проект Lab4_Tasks1_5

Выберите номер задания от 1 до 5

Следуйте инструкциям для ввода данных

Для заданий 6-7:
Запустите проект Lab4_Tasks6_7

Выберите действие:

"1" - создание и тестирование треугольника

"2" - демонстрация операций перегрузки

Примеры ввода данных:
Задание 1: 1 2 3 2 4 5 и 2

Задание 2: 1 2 3 4 1

Задание 3: Франция, Италия, Испания и данные туристов

Задание 4: произвольный русский текст

Задание 5: 10:00 и данные пассажиров

Задания 6-7: 3 и 4 для катетов
