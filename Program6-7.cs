using System;

namespace OperatorOverloading
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            
            while (!exit)
            {
                Console.Clear();
                Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №4 - ПЕРЕГРУЗКА ОПЕРАТОРОВ ===");
                Console.WriteLine("=== Вариант 4 ===\n");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Создать треугольники и показать информацию");
                Console.WriteLine("2. Тестирование методов (Задание 6)");
                Console.WriteLine("3. Тестирование операторов (Задание 7)");
                Console.WriteLine("4. Полное тестирование всех возможностей");
                Console.WriteLine("5. Выход");
                Console.Write("\nВаш выбор (1-5): ");
                
                string choice = Console.ReadLine();
                Console.WriteLine();
                
                switch (choice)
                {
                    case "1":
                        CreateTrianglesMenu();
                        break;
                    case "2":
                        TestMethodsMenu();
                        break;
                    case "3":
                        TestOperatorsMenu();
                        break;
                    case "4":
                        FullTestMenu();
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите любую клавишу...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        
        static void CreateTrianglesMenu()
        {
            Console.Clear();
            Console.WriteLine("=== СОЗДАНИЕ ТРЕУГОЛЬНИКОВ ===");
            
            RightTriangle triangle1 = null;
            RightTriangle triangle2 = null;
            RightTriangle triangle3 = null;
            
            try
            {
                Console.WriteLine("\n--- Треугольник 1 ---");
                triangle1 = CreateTriangleFromInput(1);
                
                Console.WriteLine("\n--- Треугольник 2 ---");
                Console.WriteLine("1. Ввести параметры вручную");
                Console.WriteLine("2. Создать автоматически");
                Console.Write("Ваш выбор (1-2): ");
                
                string choice = Console.ReadLine();
                if (choice == "1")
                {
                    triangle2 = CreateTriangleFromInput(2);
                }
                else
                {
                    triangle2 = new RightTriangle(5, 12);
                    Console.WriteLine("Создан треугольник со сторонами 5 и 12");
                }
                
                Console.WriteLine("\n--- Треугольник 3 ---");
                Console.WriteLine("1. Ввести параметры вручную");
                Console.WriteLine("2. Создать автоматически");
                Console.WriteLine("3. Не создавать");
                Console.Write("Ваш выбор (1-3): ");
                
                choice = Console.ReadLine();
                if (choice == "1")
                {
                    triangle3 = CreateTriangleFromInput(3);
                }
                else if (choice == "2")
                {
                    triangle3 = new RightTriangle(6, 8);
                    Console.WriteLine("Создан треугольник со сторонами 6 и 8");
                }
                
                Console.WriteLine("\n=== ИНФОРМАЦИЯ О ТРЕУГОЛЬНИКАХ ===");
                Console.WriteLine($"Треугольник 1: {triangle1}");
                Console.WriteLine($"Треугольник 2: {triangle2}");
                if (triangle3 != null)
                {
                    Console.WriteLine($"Треугольник 3: {triangle3}");
                }
                
                // Сохраняем треугольники для использования в других меню
                ProgramData.Triangle1 = triangle1;
                ProgramData.Triangle2 = triangle2;
                ProgramData.Triangle3 = triangle3;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        
        static RightTriangle CreateTriangleFromInput(int number)
        {
            Console.WriteLine($"Введите параметры для треугольника {number}:");
            
            double a = GetDoubleInput("Длина катета A: ", 0.1, 1000);
            double b = GetDoubleInput("Длина катета B: ", 0.1, 1000);
            
            return new RightTriangle(a, b);
        }
        
        static double GetDoubleInput(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                
                if (double.TryParse(input, out double result))
                {
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                    else
                    {
                        Console.WriteLine($"Введите число от {min} до {max}");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный формат числа. Попробуйте снова.");
                }
            }
        }
        
        static void TestMethodsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ТЕСТИРОВАНИЕ МЕТОДОВ (ЗАДАНИЕ 6) ===");
            
            if (ProgramData.Triangle1 == null || ProgramData.Triangle2 == null)
            {
                Console.WriteLine("Сначала создайте треугольники!");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("\nИспользуемые треугольники:");
            Console.WriteLine($"1. {ProgramData.Triangle1}");
            Console.WriteLine($"2. {ProgramData.Triangle2}");
            
            if (ProgramData.Triangle3 != null)
            {
                Console.WriteLine($"3. {ProgramData.Triangle3}");
            }
            
            Console.WriteLine("\n--- Результаты тестирования ---");
            
            Console.WriteLine("\n1. Метод Area():");
            Console.WriteLine($"   Площадь треугольника 1: {ProgramData.Triangle1.Area():F2}");
            Console.WriteLine($"   Площадь треугольника 2: {ProgramData.Triangle2.Area():F2}");
            if (ProgramData.Triangle3 != null)
            {
                Console.WriteLine($"   Площадь треугольника 3: {ProgramData.Triangle3.Area():F2}");
            }
            
            Console.WriteLine("\n2. Метод ToString():");
            Console.WriteLine($"   Triangle1.ToString(): {ProgramData.Triangle1}");
            Console.WriteLine($"   Triangle2.ToString(): {ProgramData.Triangle2}");
            
            Console.WriteLine("\n3. Проверка существования треугольников:");
            bool exists1 = (bool)ProgramData.Triangle1;
            bool exists2 = (bool)ProgramData.Triangle2;
            Console.WriteLine($"   Треугольник 1 существует: {exists1}");
            Console.WriteLine($"   Треугольник 2 существует: {exists2}");
            
            // Создаем невалидный треугольник для демонстрации
            RightTriangle invalidTriangle = new RightTriangle(0, 0);
            bool existsInvalid = (bool)invalidTriangle;
            Console.WriteLine($"   Треугольник (0,0) существует: {existsInvalid}");
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        
        static void TestOperatorsMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ТЕСТИРОВАНИЕ ОПЕРАТОРОВ (ЗАДАНИЕ 7) ===");
            
            if (ProgramData.Triangle1 == null || ProgramData.Triangle2 == null)
            {
                Console.WriteLine("Сначала создайте треугольники!");
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
                return;
            }
            
            Console.WriteLine("\nИспользуемые треугольники:");
            Console.WriteLine($"1. {ProgramData.Triangle1}");
            Console.WriteLine($"2. {ProgramData.Triangle2}");
            
            Console.WriteLine("\n=== ТЕСТЫ ОПЕРАТОРОВ ===");
            
            Console.WriteLine("\n1. Унарные операторы ++ и --");
            Console.WriteLine("   Исходный треугольник 1: " + ProgramData.Triangle1);
            
            RightTriangle incremented = ProgramData.Triangle1++;
            Console.WriteLine("   triangle1++ (новый объект): " + incremented);
            Console.WriteLine("   Исходный треугольник 1 после операции: " + ProgramData.Triangle1);
            
            RightTriangle decremented = ProgramData.Triangle1--;
            Console.WriteLine("   triangle1-- (новый объект): " + decremented);
            Console.WriteLine("   Исходный треугольник 1 после операции: " + ProgramData.Triangle1);
            
            Console.WriteLine("\n2. Операторы сравнения (по площади):");
            Console.WriteLine($"   triangle1 ({ProgramData.Triangle1.Area():F2}) <= triangle2 ({ProgramData.Triangle2.Area():F2}): {ProgramData.Triangle1 <= ProgramData.Triangle2}");
            Console.WriteLine($"   triangle1 ({ProgramData.Triangle1.Area():F2}) >= triangle2 ({ProgramData.Triangle2.Area():F2}): {ProgramData.Triangle1 >= ProgramData.Triangle2}");
            Console.WriteLine($"   triangle1 ({ProgramData.Triangle1.Area():F2}) < triangle2 ({ProgramData.Triangle2.Area():F2}): {ProgramData.Triangle1 < ProgramData.Triangle2}");
            Console.WriteLine($"   triangle1 ({ProgramData.Triangle1.Area():F2}) > triangle2 ({ProgramData.Triangle2.Area():F2}): {ProgramData.Triangle1 > ProgramData.Triangle2}");
            
            Console.WriteLine("\n3. Операторы равенства:");
            Console.WriteLine($"   triangle1 == triangle2: {ProgramData.Triangle1 == ProgramData.Triangle2}");
            Console.WriteLine($"   triangle1 != triangle2: {ProgramData.Triangle1 != ProgramData.Triangle2}");
            
            // Создаем копию triangle1
            RightTriangle triangle1Copy = new RightTriangle(ProgramData.Triangle1.A, ProgramData.Triangle1.B);
            Console.WriteLine($"\n   Создана копия triangle1: {triangle1Copy}");
            Console.WriteLine($"   triangle1 == triangle1Copy: {ProgramData.Triangle1 == triangle1Copy}");
            Console.WriteLine($"   triangle1.Equals(triangle1Copy): {ProgramData.Triangle1.Equals(triangle1Copy)}");
            
            Console.WriteLine("\n4. Приведение типов:");
            double area = (double)ProgramData.Triangle1;
            Console.WriteLine($"   (double)triangle1 (явное): {area:F2}");
            
            bool exists = ProgramData.Triangle1;
            Console.WriteLine($"   (bool)triangle1 (неявное): {exists}");
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
        
        static void FullTestMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ПОЛНОЕ ТЕСТИРОВАНИЕ ===");
            
            Console.WriteLine("\nВведите параметры для тестового треугольника:");
            RightTriangle testTriangle = CreateTriangleFromInput(0);
            
            Console.WriteLine("\n=== РЕЗУЛЬТАТЫ ТЕСТИРОВАНИЯ ===");
            
            // 1. Базовые методы
            Console.WriteLine("\n1. Базовые методы:");
            Console.WriteLine($"   ToString(): {testTriangle}");
            Console.WriteLine($"   Area(): {testTriangle.Area():F2}");
            Console.WriteLine($"   Существует? {(bool)testTriangle}");
            
            // 2. Унарные операторы
            Console.WriteLine("\n2. Унарные операторы:");
            Console.WriteLine($"   Исходный: {testTriangle}");
            
            testTriangle = ++testTriangle;
            Console.WriteLine($"   После ++: {testTriangle}");
            
            testTriangle = --testTriangle;
            Console.WriteLine($"   После --: {testTriangle}");
            
            // 3. Создаем второй треугольник для сравнения
            Console.WriteLine("\n3. Сравнение с другим треугольником (5, 12):");
            RightTriangle otherTriangle = new RightTriangle(5, 12);
            Console.WriteLine($"   Треугольник 1: {testTriangle}");
            Console.WriteLine($"   Треугольник 2: {otherTriangle}");
            
            Console.WriteLine($"\n   Сравнение площадей:");
            Console.WriteLine($"   triangle1 <= triangle2: {testTriangle <= otherTriangle}");
            Console.WriteLine($"   triangle1 >= triangle2: {testTriangle >= otherTriangle}");
            Console.WriteLine($"   triangle1 == triangle2: {testTriangle == otherTriangle}");
            Console.WriteLine($"   triangle1 != triangle2: {testTriangle != otherTriangle}");
            
            // 4. Приведение типов
            Console.WriteLine("\n4. Приведение типов:");
            Console.WriteLine($"   Явное к double: {(double)testTriangle:F2}");
            Console.WriteLine($"   Неявное к bool: {(bool)testTriangle}");
            
            // 5. Работа с невалидным треугольником
            Console.WriteLine("\n5. Тест с невалидным треугольником (0, 0):");
            RightTriangle invalid = new RightTriangle(0, 0);
            Console.WriteLine($"   Невалидный треугольник: {invalid}");
            Console.WriteLine($"   Существует? {(bool)invalid}");
            
            try
            {
                Console.WriteLine("\n6. Тест исключений:");
                Console.WriteLine("   Попытка создать треугольник с отрицательными сторонами...");
                RightTriangle negative = new RightTriangle(-1, 5);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"   Ошибка: {ex.Message}");
            }
            
            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
    
    // Класс для хранения данных между вызовами меню
    public static class ProgramData
    {
        public static RightTriangle Triangle1 { get; set; }
        public static RightTriangle Triangle2 { get; set; }
        public static RightTriangle Triangle3 { get; set; }
    }
}
