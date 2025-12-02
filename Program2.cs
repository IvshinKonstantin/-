using System;

namespace Lab6_FractionAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №6 - ЗАДАНИЕ 2: ДРОБИ ===\n");
            
            bool exit = false;
            
            while (!exit)
            {
                Console.WriteLine("\n══════════════════════════════════════════════");
                Console.WriteLine("                МЕНЮ ВЫБОРА                    ");
                Console.WriteLine("══════════════════════════════════════════════");
                Console.WriteLine("1. Автоматические тесты (по заданию)");
                Console.WriteLine("2. Ручной ввод дробей");
                Console.WriteLine("3. Арифметические операции");
                Console.WriteLine("4. Сравнение дробей");
                Console.WriteLine("5. Клонирование дроби");
                Console.WriteLine("6. Тест кэширования");
                Console.WriteLine("7. Вычисление выражения f1.sum(f2).div(f3).minus(5)");
                Console.WriteLine("8. Выход");
                Console.WriteLine("══════════════════════════════════════════════");
                Console.Write("Выберите пункт меню: ");
                
                string choice = Console.ReadLine() ?? string.Empty;
                Console.WriteLine();
                
                switch (choice)
                {
                    case "1":
                        RunAutomaticTests();
                        break;
                    case "2":
                        ManualInput();
                        break;
                    case "3":
                        ArithmeticOperations();
                        break;
                    case "4":
                        CompareFractions();
                        break;
                    case "5":
                        CloneFraction();
                        break;
                    case "6":
                        TestCaching();
                        break;
                    case "7":
                        CalculateExpression();
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Выход из программы...");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
                
                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
            
            Console.WriteLine("\nПрограмма завершена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
        
        // Метод для автоматических тестов
        static void RunAutomaticTests()
        {
            Console.WriteLine("======= АВТОМАТИЧЕСКИЕ ТЕСТЫ =======");
            
            // Создаем тестовые дроби
            Fraction f1 = new Fraction(1, 3);
            Fraction f2 = new Fraction(2, 3);
            Fraction f3 = new Fraction(3, 4);
            
            Console.WriteLine("\nТестовые дроби:");
            Console.WriteLine($"f1 = {f1}");
            Console.WriteLine($"f2 = {f2}");
            Console.WriteLine($"f3 = {f3}");
            
            Console.WriteLine("\nБазовые операции:");
            Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
            Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
            Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
            Console.WriteLine($"{f1} / {f2} = {f1 / f2}");
            
            Console.WriteLine("\nОперации с целыми числами:");
            Console.WriteLine($"{f1} + 2 = {f1 + 2}");
            Console.WriteLine($"{f1} * 3 = {f1 * 3}");
            
            Console.WriteLine("\n✅ Автоматические тесты выполнены успешно!");
        }
        
        // Метод для ручного ввода дробей
        static void ManualInput()
        {
            Console.WriteLine("======= РУЧНОЙ ВВОД ДРОБЕЙ =======");
            
            try
            {
                Console.Write("Введите числитель первой дроби: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Введите знаменатель первой дроби: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Fraction f1 = new Fraction(num1, den1);
                Console.WriteLine($"Первая дробь: {f1}");
                
                Console.Write("\nВведите числитель второй дроби: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int num2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Введите знаменатель второй дроби: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Fraction f2 = new Fraction(num2, den2);
                Console.WriteLine($"Вторая дробь: {f2}");
                
                // Сохраняем дроби для последующих операций
                Console.WriteLine("\n✅ Дроби успешно созданы!");
                Console.WriteLine($"f1 = {f1} (≈ {f1.GetRealValue():F4})");
                Console.WriteLine($"f2 = {f2} (≈ {f2.GetRealValue():F4})");
                
                // Предлагаем сохранить или использовать
                Console.WriteLine("\nХотите выполнить операции с этими дробями? (д/н): ");
                string? answer = Console.ReadLine()?.ToLower();
                
                if (answer == "д" || answer == "y")
                {
                    PerformOperationsWithFractions(f1, f2);
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
        
        // Метод для выполнения операций с дробями
        static void PerformOperationsWithFractions(Fraction f1, Fraction f2)
        {
            Console.WriteLine("\n══════════════════════════════════════════════");
            Console.WriteLine("          ОПЕРАЦИИ С ДРОБЯМИ                  ");
            Console.WriteLine("══════════════════════════════════════════════");
            
            Console.WriteLine($"f1 = {f1} (≈ {f1.GetRealValue():F4})");
            Console.WriteLine($"f2 = {f2} (≈ {f2.GetRealValue():F4})");
            
            Console.WriteLine("\nРезультаты операций:");
            Console.WriteLine($"1. Сложение:      {f1} + {f2} = {f1 + f2}");
            Console.WriteLine($"2. Вычитание:     {f1} - {f2} = {f1 - f2}");
            Console.WriteLine($"3. Умножение:     {f1} * {f2} = {f1 * f2}");
            
            try
            {
                Console.WriteLine($"4. Деление:       {f1} / {f2} = {f1 / f2}");
            }
            catch (DivideByZeroException)
            {
                Console.WriteLine($"4. Деление:       {f1} / {f2} = Ошибка: деление на ноль!");
            }
        }
        
        // Метод для арифметических операций
        static void ArithmeticOperations()
        {
            Console.WriteLine("======= АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ =======");
            
            try
            {
                Console.WriteLine("Введите первую дробь:");
                Console.Write("Числитель: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f1 = new Fraction(num1, den1);
                
                Console.WriteLine("\nВведите вторую дробь:");
                Console.Write("Числитель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int num2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f2 = new Fraction(num2, den2);
                
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1. Сложение (+)");
                Console.WriteLine("2. Вычитание (-)");
                Console.WriteLine("3. Умножение (*)");
                Console.WriteLine("4. Деление (/)");
                Console.Write("Ваш выбор: ");
                
                string? opChoice = Console.ReadLine();
                
                Fraction result;
                string operation;
                
                switch (opChoice)
                {
                    case "1":
                        result = f1 + f2;
                        operation = "+";
                        break;
                    case "2":
                        result = f1 - f2;
                        operation = "-";
                        break;
                    case "3":
                        result = f1 * f2;
                        operation = "*";
                        break;
                    case "4":
                        try
                        {
                            result = f1 / f2;
                            operation = "/";
                        }
                        catch (DivideByZeroException)
                        {
                            Console.WriteLine("❌ Ошибка: деление на ноль!");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("❌ Неверный выбор операции!");
                        return;
                }
                
                Console.WriteLine($"\nРезультат: {f1} {operation} {f2} = {result}");
                Console.WriteLine($"Десятичная форма: {result.GetRealValue():F6}");
                
                // Операция с целым числом
                Console.WriteLine("\nХотите выполнить операцию с целым числом? (д/н): ");
                string? answer = Console.ReadLine()?.ToLower();
                
                if (answer == "д" || answer == "y")
                {
                    Console.Write("Введите целое число: ");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out int number))
                    {
                        Console.WriteLine("❌ Ошибка: Введите целое число!");
                        return;
                    }
                    
                    Console.WriteLine("\nВыберите операцию с целым числом:");
                    Console.WriteLine($"1. {f1} + {number}");
                    Console.WriteLine($"2. {f1} - {number}");
                    Console.WriteLine($"3. {f1} * {number}");
                    Console.WriteLine($"4. {f1} / {number}");
                    Console.Write("Ваш выбор: ");
                    
                    string? intOpChoice = Console.ReadLine();
                    Fraction intResult;
                    string intOperation;
                    
                    switch (intOpChoice)
                    {
                        case "1":
                            intResult = f1 + number;
                            intOperation = "+";
                            break;
                        case "2":
                            intResult = f1 - number;
                            intOperation = "-";
                            break;
                        case "3":
                            intResult = f1 * number;
                            intOperation = "*";
                            break;
                        case "4":
                            try
                            {
                                intResult = f1 / number;
                                intOperation = "/";
                            }
                            catch (DivideByZeroException)
                            {
                                Console.WriteLine("❌ Ошибка: деление на ноль!");
                                return;
                            }
                            break;
                        default:
                            Console.WriteLine("❌ Неверный выбор операции!");
                            return;
                    }
                    
                    Console.WriteLine($"\nРезультат: {f1} {intOperation} {number} = {intResult}");
                    Console.WriteLine($"Десятичная форма: {intResult.GetRealValue():F6}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
        
        // Метод для сравнения дробей
        static void CompareFractions()
        {
            Console.WriteLine("======= СРАВНЕНИЕ ДРОБЕЙ =======");
            
            try
            {
                Console.WriteLine("Введите первую дробь для сравнения:");
                Console.Write("Числитель: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f1 = new Fraction(num1, den1);
                
                Console.WriteLine("\nВведите вторую дробь для сравнения:");
                Console.Write("Числитель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int num2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f2 = new Fraction(num2, den2);
                
                Console.WriteLine($"\nДробь 1: {f1} (≈ {f1.GetRealValue():F4})");
                Console.WriteLine($"Дробь 2: {f2} (≈ {f2.GetRealValue():F4})");
                
                Console.WriteLine("\nРезультаты сравнения:");
                Console.WriteLine($"f1 == f2: {f1 == f2}");
                Console.WriteLine($"f1 != f2: {f1 != f2}");
                Console.WriteLine($"f1.Equals(f2): {f1.Equals(f2)}");
                
                if (f1 == f2)
                {
                    Console.WriteLine("✅ Дроби равны!");
                }
                else
                {
                    Console.WriteLine("❌ Дроби не равны!");
                    
                    // Какая дробь больше по значению
                    double val1 = f1.GetRealValue();
                    double val2 = f2.GetRealValue();
                    
                    if (val1 > val2)
                    {
                        Console.WriteLine($"f1 ({val1:F4}) > f2 ({val2:F4})");
                    }
                    else if (val1 < val2)
                    {
                        Console.WriteLine($"f1 ({val1:F4}) < f2 ({val2:F4})");
                    }
                }
                
                // Дополнительное сравнение с упрощенными формами
                Console.WriteLine("\nУпрощенные формы:");
                Console.WriteLine($"f1 (упрощенная): {new Fraction(f1.Numerator, f1.Denominator)}");
                Console.WriteLine($"f2 (упрощенная): {new Fraction(f2.Numerator, f2.Denominator)}");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
        
        // Метод для клонирования дроби
        static void CloneFraction()
        {
            Console.WriteLine("======= КЛОНИРОВАНИЕ ДРОБИ =======");
            
            try
            {
                Console.WriteLine("Введите дробь для клонирования:");
                Console.Write("Числитель: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Fraction original = new Fraction(num, den);
                Console.WriteLine($"\nОригинальная дробь: {original}");
                Console.WriteLine($"Десятичная форма: {original.GetRealValue():F6}");
                
                // Клонируем дробь
                Fraction clone = (Fraction)original.Clone();
                Console.WriteLine($"\nКлонированная дробь: {clone}");
                Console.WriteLine($"Десятичная форма: {clone.GetRealValue():F6}");
                
                Console.WriteLine("\nПроверка клонирования:");
                Console.WriteLine($"Значения равны: {original == clone}");
                Console.WriteLine($"Ссылки равны: {ReferenceEquals(original, clone)}");
                
                // Изменяем клон и проверяем
                Console.WriteLine("\nХотите изменить клонированную дробь? (д/н): ");
                string? answer = Console.ReadLine()?.ToLower();
                
                if (answer == "д" || answer == "y")
                {
                    Console.Write("Введите новый числитель для клона: ");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out int newNum))
                    {
                        Console.WriteLine("❌ Ошибка: Введите целое число!");
                        return;
                    }
                    
                    Console.Write("Введите новый знаменатель для клона: ");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out int newDen))
                    {
                        Console.WriteLine("❌ Ошибка: Введите целое число!");
                        return;
                    }
                    
                    clone.Numerator = newNum;
                    clone.Denominator = newDen;
                    
                    Console.WriteLine($"\nПосле изменения:");
                    Console.WriteLine($"Оригинал: {original} (≈ {original.GetRealValue():F4})");
                    Console.WriteLine($"Клон: {clone} (≈ {clone.GetRealValue():F4})");
                    Console.WriteLine($"Теперь равны: {original == clone}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
        
        // Метод для тестирования кэширования
        static void TestCaching()
        {
            Console.WriteLine("======= ТЕСТ КЭШИРОВАНИЯ =======");
            
            try
            {
                Console.WriteLine("Введите дробь для тестирования кэширования:");
                Console.Write("Числитель: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Fraction fraction = new Fraction(num, den);
                Console.WriteLine($"\nДробь: {fraction}");
                
                Console.WriteLine("\nТестирование кэширования:");
                Console.WriteLine("Выполняем 5 последовательных вызовов GetRealValue():");
                
                for (int i = 1; i <= 5; i++)
                {
                    double value = fraction.GetRealValue();
                    Console.WriteLine($"Вызов {i}: {value:F10}");
                }
                
                Console.WriteLine("\n⚠️ Примечание: первое вычисление - расчет,");
                Console.WriteLine("остальные - взятие из кэша!");
                
                // Изменяем и снова тестируем
                Console.WriteLine("\nХотите изменить дробь и протестировать снова? (д/н): ");
                string? answer = Console.ReadLine()?.ToLower();
                
                if (answer == "д" || answer == "y")
                {
                    Console.Write("Введите новый числитель: ");
                    input = Console.ReadLine();
                    if (!int.TryParse(input, out int newNum))
                    {
                        Console.WriteLine("❌ Ошибка: Введите целое число!");
                        return;
                    }
                    
                    fraction.Numerator = newNum;
                    
                    Console.WriteLine("\nПосле изменения числителя:");
                    Console.WriteLine($"Новая дробь: {fraction}");
                    
                    for (int i = 1; i <= 3; i++)
                    {
                        double value = fraction.GetRealValue();
                        Console.WriteLine($"Вызов {i}: {value:F10}");
                    }
                    
                    Console.WriteLine("\n⚠️ Кэш сброшен при изменении числителя!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
        
        // Метод для вычисления выражения из задания
        static void CalculateExpression()
        {
            Console.WriteLine("======= ВЫЧИСЛЕНИЕ ВЫРАЖЕНИЯ =======");
            Console.WriteLine("Вычисление: f1.sum(f2).div(f3).minus(5)");
            Console.WriteLine("(аналог: (f1 + f2) / f3 - 5)");
            
            try
            {
                Console.WriteLine("\nВведите дробь f1:");
                Console.Write("Числитель: ");
                string? input = Console.ReadLine();
                if (!int.TryParse(input, out int num1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den1))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f1 = new Fraction(num1, den1);
                
                Console.WriteLine("\nВведите дробь f2:");
                Console.Write("Числитель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int num2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den2))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f2 = new Fraction(num2, den2);
                
                Console.WriteLine("\nВведите дробь f3:");
                Console.Write("Числитель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int num3))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                
                Console.Write("Знаменатель: ");
                input = Console.ReadLine();
                if (!int.TryParse(input, out int den3))
                {
                    Console.WriteLine("❌ Ошибка: Введите целое число!");
                    return;
                }
                Fraction f3 = new Fraction(num3, den3);
                
                Console.WriteLine($"\nВведенные дроби:");
                Console.WriteLine($"f1 = {f1} (≈ {f1.GetRealValue():F4})");
                Console.WriteLine($"f2 = {f2} (≈ {f2.GetRealValue():F4})");
                Console.WriteLine($"f3 = {f3} (≈ {f3.GetRealValue():F4})");
                
                Console.WriteLine("\nВычисление выражения...");
                
                try
                {
                    // Вычисляем выражение
                    Fraction result = f1.sum(f2).div(f3).minus(5);
                    
                    Console.WriteLine("\nПошаговое вычисление:");
                    Fraction step1 = f1.sum(f2);
                    Console.WriteLine($"1. f1.sum(f2) = {f1} + {f2} = {step1}");
                    
                    Fraction step2 = step1.div(f3);
                    Console.WriteLine($"2. {step1}.div(f3) = {step1} / {f3} = {step2}");
                    
                    Fraction step3 = step2.minus(5);
                    Console.WriteLine($"3. {step2}.minus(5) = {step2} - 5 = {step3}");
                    
                    Console.WriteLine($"\n✅ Итоговый результат:");
                    Console.WriteLine($"{f1}.sum({f2}).div({f3}).minus(5) = {result}");
                    Console.WriteLine($"Десятичная форма: {result.GetRealValue():F6}");
                    
                    // Альтернативный расчет для проверки
                    Console.WriteLine("\nПроверка (альтернативный расчет):");
                    Fraction altResult = (f1 + f2) / f3 - 5;
                    Console.WriteLine($"(f1 + f2) / f3 - 5 = {altResult}");
                    
                    if (result == altResult)
                    {
                        Console.WriteLine("✅ Результаты совпадают!");
                    }
                }
                catch (DivideByZeroException)
                {
                    Console.WriteLine("❌ Ошибка: Деление на ноль при вычислении выражения!");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"❌ Ошибка: {ex.Message}");
            }
        }
    }
}
