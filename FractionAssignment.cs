using System;

namespace Lab6_FractionAssignment
{
    // ========== ИНТЕРФЕЙСЫ ==========
    
    /// <summary>
    /// Интерфейс для клонирования объектов
    /// </summary>
    public interface ICloneableFraction
    {
        /// <summary>
        /// Создает копию объекта
        /// </summary>
        /// <returns>Новый объект с такими же значениями</returns>
        object Clone();
    }
    
    /// <summary>
    /// Интерфейс для кэширования вещественного значения
    /// </summary>
    public interface IRealValueCache
    {
        /// <summary>
        /// Получает вещественное значение с кэшированием
        /// </summary>
        /// <returns>Вещественное значение</returns>
        double GetRealValue();
    }

    // ========== КЛАССЫ МОДЕЛЕЙ ==========
    
    /// <summary>
    /// Класс, представляющий дробь
    /// </summary>
    public class Fraction : ICloneableFraction, IRealValueCache, IEquatable<Fraction>
    {
        private int _numerator;
        private int _denominator;
        private double? _cachedRealValue;
        
        /// <summary>
        /// Числитель дроби
        /// </summary>
        public int Numerator 
        { 
            get => _numerator;
            set
            {
                _numerator = value;
                _cachedRealValue = null; // Сброс кэша
                Simplify();
            }
        }
        
        /// <summary>
        /// Знаменатель дроби
        /// </summary>
        public int Denominator 
        { 
            get => _denominator;
            set
            {
                if (value == 0)
                    throw new ArgumentException("Знаменатель не может быть равен нулю");
                _denominator = Math.Abs(value); // Знаменатель всегда положительный
                _cachedRealValue = null; // Сброс кэша
                
                // Если знаменатель был отрицательным, меняем знак числителя
                if (value < 0)
                {
                    _numerator = -_numerator;
                }
                Simplify();
            }
        }
        
        /// <summary>
        /// Создает новую дробь
        /// </summary>
        /// <param name="numerator">Числитель</param>
        /// <param name="denominator">Знаменатель</param>
        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");
                
            _numerator = numerator;
            _denominator = Math.Abs(denominator);
            
            // Если знаменатель отрицательный, меняем знак числителя
            if (denominator < 0)
            {
                _numerator = -_numerator;
            }
            
            Simplify();
        }
        
        /// <summary>
        /// Упрощает дробь (приводит к несократимому виду)
        /// </summary>
        private void Simplify()
        {
            if (_numerator == 0)
            {
                _denominator = 1;
                return;
            }
            
            int gcd = GCD(Math.Abs(_numerator), _denominator);
            _numerator /= gcd;
            _denominator /= gcd;
        }
        
        /// <summary>
        /// Находит наибольший общий делитель (алгоритм Евклида)
        /// </summary>
        /// <param name="a">Первое число</param>
        /// <param name="b">Второе число</param>
        /// <returns>Наибольший общий делитель</returns>
        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        
        /// <summary>
        /// Строковое представление дроби
        /// </summary>
        /// <returns>Строка в формате "числитель/знаменатель"</returns>
        public override string ToString() => $"{Numerator}/{Denominator}";
        
        // ===== ЗАДАНИЕ 2.1: АРИФМЕТИЧЕСКИЕ ОПЕРАЦИИ =====
        
        /// <summary>
        /// Сложение с другой дробью
        /// </summary>
        /// <param name="other">Другая дробь</param>
        /// <returns>Новая дробь - результат сложения</returns>
        public Fraction Add(Fraction other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
                
            int newNumerator = _numerator * other._denominator + other._numerator * _denominator;
            int newDenominator = _denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        
        /// <summary>
        /// Сложение с целым числом
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <returns>Новая дробь - результат сложения</returns>
        public Fraction Add(int number)
        {
            return Add(new Fraction(number, 1));
        }
        
        /// <summary>
        /// Вычитание другой дроби
        /// </summary>
        /// <param name="other">Другая дробь</param>
        /// <returns>Новая дробь - результат вычитания</returns>
        public Fraction Subtract(Fraction other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
                
            int newNumerator = _numerator * other._denominator - other._numerator * _denominator;
            int newDenominator = _denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        
        /// <summary>
        /// Вычитание целого числа
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <returns>Новая дробь - результат вычитания</returns>
        public Fraction Subtract(int number)
        {
            return Subtract(new Fraction(number, 1));
        }
        
        /// <summary>
        /// Умножение на другую дробь
        /// </summary>
        /// <param name="other">Другая дробь</param>
        /// <returns>Новая дробь - результат умножения</returns>
        public Fraction Multiply(Fraction other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
                
            int newNumerator = _numerator * other._numerator;
            int newDenominator = _denominator * other._denominator;
            return new Fraction(newNumerator, newDenominator);
        }
        
        /// <summary>
        /// Умножение на целое число
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <returns>Новая дробь - результат умножения</returns>
        public Fraction Multiply(int number)
        {
            return Multiply(new Fraction(number, 1));
        }
        
        /// <summary>
        /// Деление на другую дробь
        /// </summary>
        /// <param name="other">Другая дробь</param>
        /// <returns>Новая дробь - результат деления</returns>
        public Fraction Divide(Fraction other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));
            if (other._numerator == 0)
                throw new DivideByZeroException("Деление на ноль");
                
            return Multiply(new Fraction(other._denominator, other._numerator));
        }
        
        /// <summary>
        /// Деление на целое число
        /// </summary>
        /// <param name="number">Целое число</param>
        /// <returns>Новая дробь - результат деления</returns>
        public Fraction Divide(int number)
        {
            if (number == 0)
                throw new DivideByZeroException("Деление на ноль");
                
            return Divide(new Fraction(number, 1));
        }
        
        // Перегрузки операторов для удобства
        public static Fraction operator +(Fraction a, Fraction b) => a.Add(b);
        public static Fraction operator +(Fraction a, int b) => a.Add(b);
        public static Fraction operator -(Fraction a, Fraction b) => a.Subtract(b);
        public static Fraction operator -(Fraction a, int b) => a.Subtract(b);
        public static Fraction operator *(Fraction a, Fraction b) => a.Multiply(b);
        public static Fraction operator *(Fraction a, int b) => a.Multiply(b);
        public static Fraction operator /(Fraction a, Fraction b) => a.Divide(b);
        public static Fraction operator /(Fraction a, int b) => a.Divide(b);
        
        // Синонимы методов из задания (для использования в выражении)
        public Fraction sum(Fraction other) => Add(other);
        public Fraction div(Fraction other) => Divide(other);
        public Fraction minus(int number) => Subtract(number);
        
        // ===== ЗАДАНИЕ 2.2: СРАВНЕНИЕ ДРОБЕЙ =====
        
        /// <summary>
        /// Сравнивает текущую дробь с другой дробью
        /// </summary>
        /// <param name="other">Другая дробь</param>
        /// <returns>True, если дроби равны</returns>
        public bool Equals(Fraction other)
        {
            if (other is null) 
                return false;
            
            // Приводим обе дроби к простейшей форме и сравниваем
            var simplifiedThis = new Fraction(_numerator, _denominator);
            var simplifiedOther = new Fraction(other._numerator, other._denominator);
            
            return simplifiedThis._numerator == simplifiedOther._numerator &&
                   simplifiedThis._denominator == simplifiedOther._denominator;
        }
        
        /// <summary>
        /// Сравнивает текущий объект с другим объектом
        /// </summary>
        /// <param name="obj">Объект для сравнения</param>
        /// <returns>True, если объекты равны</returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as Fraction);
        }
        
        /// <summary>
        /// Получает хэш-код дроби
        /// </summary>
        /// <returns>Хэш-код</returns>
        public override int GetHashCode()
        {
            var simplified = new Fraction(_numerator, _denominator);
            return HashCode.Combine(simplified._numerator, simplified._denominator);
        }
        
        /// <summary>
        /// Оператор равенства для двух дробей
        /// </summary>
        public static bool operator ==(Fraction left, Fraction right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }
        
        /// <summary>
        /// Оператор неравенства для двух дробей
        /// </summary>
        public static bool operator !=(Fraction left, Fraction right)
        {
            return !(left == right);
        }
        
        // ===== ЗАДАНИЕ 2.3: КЛОНИРОВАНИЕ =====
        
        /// <summary>
        /// Создает копию дроби
        /// </summary>
        /// <returns>Новая дробь с такими же значениями</returns>
        public object Clone()
        {
            return new Fraction(_numerator, _denominator);
        }
        
        // ===== ЗАДАНИЕ 2.4: ВЕЩЕСТВЕННОЕ ЗНАЧЕНИЕ С КЭШИРОВАНИЕМ =====
        
        /// <summary>
        /// Получает вещественное значение дроби с кэшированием
        /// </summary>
        /// <returns>Вещественное значение</returns>
        public double GetRealValue()
        {
            // Используем кэширование: если значение уже вычислено, возвращаем его
            if (!_cachedRealValue.HasValue)
            {
                _cachedRealValue = (double)_numerator / _denominator;
            }
            return _cachedRealValue.Value;
        }
        
        /// <summary>
        /// Устанавливает числитель и знаменатель одновременно
        /// (дополнительный метод по заданию)
        /// </summary>
        /// <param name="numerator">Новый числитель</param>
        /// <param name="denominator">Новый знаменатель</param>
        public void SetValues(int numerator, int denominator)
        {
            if (denominator == 0)
                throw new ArgumentException("Знаменатель не может быть равен нулю");
                
            _numerator = numerator;
            _denominator = Math.Abs(denominator);
            
            if (denominator < 0)
            {
                _numerator = -_numerator;
            }
            
            _cachedRealValue = null; // Сброс кэша
            Simplify();
        }
    }

    // ========== СЕРВИСНЫЕ КЛАССЫ ==========
    
    /// <summary>
    /// Сервис для работы с дробями
    /// </summary>
    public static class FractionService
    {
        /// <summary>
        /// Выводит результат операции с дробями в заданном формате
        /// </summary>
        /// <param name="a">Первая дробь</param>
        /// <param name="operation">Операция (+, -, *, /)</param>
        /// <param name="b">Вторая дробь</param>
        /// <param name="result">Результат операции</param>
        public static void PrintOperation(Fraction a, string operation, Fraction b, Fraction result)
        {
            Console.WriteLine($"{a} {operation} {b} = {result}");
        }
        
        /// <summary>
        /// Выводит результат операции дроби с целым числом
        /// </summary>
        /// <param name="a">Дробь</param>
        /// <param name="operation">Операция</param>
        /// <param name="b">Целое число</param>
        /// <param name="result">Результат</param>
        public static void PrintOperation(Fraction a, string operation, int b, Fraction result)
        {
            Console.WriteLine($"{a} {operation} {b} = {result}");
        }
        
        /// <summary>
        /// Создает и возвращает массив тестовых дробей
        /// </summary>
        /// <returns>Массив дробей для тестирования</returns>
        public static Fraction[] CreateTestFractions()
        {
            return new Fraction[]
            {
                new Fraction(1, 3),
                new Fraction(2, 3),
                new Fraction(3, 4),
                new Fraction(-2, 5),
                new Fraction(4, 6),  // 2/3 после упрощения
                new Fraction(0, 1),
                new Fraction(5, 1)   // целое число как дробь
            };
        }
    }

    // ========== ОСНОВНАЯ ПРОГРАММА ==========
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №6 - ЗАДАНИЕ 2: ДРОБИ ===\n");
            
            // ===== ЗАДАНИЕ 2.1: ДРОБИ И ОПЕРАЦИИ =====
            Console.WriteLine("======= ЗАДАНИЕ 2.1: Дроби и операции =======");
            
            // 1. Создаем несколько экземпляров дробей
            Fraction f1 = new Fraction(1, 3);
            Fraction f2 = new Fraction(2, 3);
            Fraction f3 = new Fraction(3, 4);
            Fraction f4 = new Fraction(-2, 5);
            Fraction f5 = new Fraction(4, -6);  // Проверка отрицательного знаменателя
            
            Console.WriteLine("Созданные дроби:");
            Console.WriteLine($"f1 = {f1}");
            Console.WriteLine($"f2 = {f2}");
            Console.WriteLine($"f3 = {f3}");
            Console.WriteLine($"f4 = {f4}");
            Console.WriteLine($"f5 = {f5} (знаменатель автоматически стал положительным)");
            
            // 2. Примеры использования каждого метода
            Console.WriteLine("\nПримеры операций:");
            
            // Сложение дробей
            Fraction sumResult = f1.Add(f2);
            FractionService.PrintOperation(f1, "+", f2, sumResult);
            
            // Вычитание дробей
            Fraction subResult = f1.Subtract(f2);
            FractionService.PrintOperation(f1, "-", f2, subResult);
            
            // Умножение дробей
            Fraction mulResult = f1.Multiply(f2);
            FractionService.PrintOperation(f1, "*", f2, mulResult);
            
            // Деление дробей
            Fraction divResult = f1.Divide(f2);
            FractionService.PrintOperation(f1, "/", f2, divResult);
            
            // Операции с целыми числами
            Console.WriteLine("\nОперации с целыми числами:");
            FractionService.PrintOperation(f1, "+", 2, f1.Add(2));
            FractionService.PrintOperation(f1, "-", 1, f1.Subtract(1));
            FractionService.PrintOperation(f1, "*", 3, f1.Multiply(3));
            FractionService.PrintOperation(f1, "/", 2, f1.Divide(2));
            
            // 3. Вывод примеров в формате «1/3 * 2/3 = 2/9»
            Console.WriteLine("\nФорматированный вывод:");
            Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
            Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
            Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
            Console.WriteLine($"{f1} / {f2} = {f1 / f2}");
            
            // 4. Вычисление выражения: f1.sum(f2).div(f3).minus(5)
            Console.WriteLine("\nВычисление выражения: f1.sum(f2).div(f3).minus(5)");
            Console.WriteLine($"f1 = {f1}, f2 = {f2}, f3 = {f3}");
            
            Fraction expressionResult = f1.sum(f2).div(f3).minus(5);
            Console.WriteLine($"Результат: {expressionResult}");
            Console.WriteLine($"Вещественное значение: {expressionResult.GetRealValue():F4}");
            
            Console.WriteLine("\nПошаговое вычисление:");
            Fraction step1 = f1.sum(f2);
            Console.WriteLine($"f1.sum(f2) = {step1}");
            
            Fraction step2 = step1.div(f3);
            Console.WriteLine($"{step1}.div(f3) = {step2}");
            
            Fraction step3 = step2.minus(5);
            Console.WriteLine($"{step2}.minus(5) = {step3}");
            
            // ===== ЗАДАНИЕ 2.2: СРАВНЕНИЕ ДРОБЕЙ =====
            Console.WriteLine("\n\n======= ЗАДАНИЕ 2.2: Сравнение дробей =======");
            
            Fraction f6 = new Fraction(2, 4);  // 1/2 после упрощения
            Fraction f7 = new Fraction(1, 2);
            Fraction f8 = new Fraction(3, 6);  // 1/2 после упрощения
            Fraction f9 = new Fraction(2, 3);
            
            Console.WriteLine("Тестовые дроби:");
            Console.WriteLine($"f6 = {f6}");
            Console.WriteLine($"f7 = {f7}");
            Console.WriteLine($"f8 = {f8}");
            Console.WriteLine($"f9 = {f9}");
            
            Console.WriteLine("\nСравнение дробей:");
            Console.WriteLine($"f6 == f7: {f6 == f7} (ожидается: True)");
            Console.WriteLine($"f6.Equals(f7): {f6.Equals(f7)} (ожидается: True)");
            Console.WriteLine($"f7 == f8: {f7 == f8} (ожидается: True)");
            Console.WriteLine($"f6 == f8: {f6 == f8} (ожидается: True)");
            Console.WriteLine($"f6 == f9: {f6 == f9} (ожидается: False)");
            
            // Проверка с разными представлениями одной дроби
            Console.WriteLine("\nПроверка разных представлений одной дроби:");
            Fraction f10 = new Fraction(4, 8);   // 1/2
            Fraction f11 = new Fraction(3, 6);   // 1/2
            Fraction f12 = new Fraction(-2, -4); // 1/2
            Fraction f13 = new Fraction(2, -4);  // -1/2
            
            Console.WriteLine($"f10 = {f10}");
            Console.WriteLine($"f11 = {f11}");
            Console.WriteLine($"f12 = {f12}");
            Console.WriteLine($"f13 = {f13}");
            Console.WriteLine($"f10 == f11: {f10 == f11}");
            Console.WriteLine($"f10 == f12: {f10 == f12}");
            Console.WriteLine($"f10 == f13: {f10 == f13}");
            
            // ===== ЗАДАНИЕ 2.3: КЛОНИРОВАНИЕ =====
            Console.WriteLine("\n\n======= ЗАДАНИЕ 2.3: Клонирование дроби =======");
            
            Fraction original = new Fraction(3, 7);
            Console.WriteLine($"Оригинальная дробь: {original}");
            
            // Клонируем дробь
            Fraction cloned = (Fraction)original.Clone();
            Console.WriteLine($"Клонированная дробь: {cloned}");
            
            // Проверяем равенство
            Console.WriteLine($"original == cloned: {original == cloned}");
            Console.WriteLine($"original.Equals(cloned): {original.Equals(cloned)}");
            Console.WriteLine($"ReferenceEquals(original, cloned): {ReferenceEquals(original, cloned)}");
            
            // Изменяем клон и проверяем, что оригинал не изменился
            Console.WriteLine("\nИзменяем клонированную дробь...");
            cloned.Numerator = 5;
            cloned.Denominator = 9;
            
            Console.WriteLine($"После изменения клона:");
            Console.WriteLine($"Оригинал: {original}");
            Console.WriteLine($"Клон: {cloned}");
            Console.WriteLine($"original == cloned: {original == cloned}");
            
            // Глубокое клонирование с помощью метода SetValues
            Console.WriteLine("\nГлубокое клонирование через SetValues:");
            Fraction original2 = new Fraction(5, 8);
            Fraction clone2 = new Fraction(1, 1);
            clone2.SetValues(original2.Numerator, original2.Denominator);
            
            Console.WriteLine($"Оригинал: {original2}");
            Console.WriteLine($"Клон: {clone2}");
            
            // ===== ЗАДАНИЕ 2.4: ВЕЩЕСТВЕННОЕ ЗНАЧЕНИЕ С КЭШИРОВАНИЕМ =====
            Console.WriteLine("\n\n======= ЗАДАНИЕ 2.4: Вещественное значение с кэшированием =======");
            
            Fraction testFraction = new Fraction(1, 3);
            Console.WriteLine($"Тестовая дробь: {testFraction}");
            
            // Первый вызов GetRealValue - вычисление
            double value1 = testFraction.GetRealValue();
            Console.WriteLine($"Первое получение вещественного значения: {value1:F6}");
            
            // Второй вызов - должно быть взято из кэша
            double value2 = testFraction.GetRealValue();
            Console.WriteLine($"Второе получение вещественного значения: {value2:F6}");
            
            // Изменяем дробь - кэш должен сброситься
            Console.WriteLine("\nИзменяем числитель дроби...");
            testFraction.Numerator = 2;
            double value3 = testFraction.GetRealValue();
            Console.WriteLine($"После изменения числителя: {value3:F6}");
            
            // Еще одно изменение
            Console.WriteLine("\nИзменяем знаменатель дроби...");
            testFraction.Denominator = 5;
            double value4 = testFraction.GetRealValue();
            Console.WriteLine($"После изменения знаменателя: {value4:F6}");
            
            // Проверка кэширования при повторных вызовах
            Console.WriteLine("\nПроверка кэширования (3 вызова подряд):");
            Fraction cachedFraction = new Fraction(22, 7); // Приближение числа π
            for (int i = 1; i <= 3; i++)
            {
                double val = cachedFraction.GetRealValue();
                Console.WriteLine($"Вызов #{i}: {val:F10}");
            }
            
            // ===== ДОПОЛНИТЕЛЬНЫЕ ПРОВЕРКИ =====
            Console.WriteLine("\n\n======= ДОПОЛНИТЕЛЬНЫЕ ПРОВЕРКИ =======");
            
            // Проверка обработки отрицательных значений
            Console.WriteLine("Проверка отрицательных дробей:");
            Fraction neg1 = new Fraction(-3, 4);
            Fraction neg2 = new Fraction(2, -5);
            Fraction neg3 = new Fraction(-1, -2);
            
            Console.WriteLine($"neg1 = {neg1}, вещественное: {neg1.GetRealValue():F4}");
            Console.WriteLine($"neg2 = {neg2}, вещественное: {neg2.GetRealValue():F4}");
            Console.WriteLine($"neg3 = {neg3}, вещественное: {neg3.GetRealValue():F4}");
            
            Console.WriteLine($"\nneg1 + neg2 = {neg1 + neg2}");
            Console.WriteLine($"neg1 * neg2 = {neg1 * neg2}");
            
            // Проверка перегрузки операторов
            Console.WriteLine("\nПроверка перегрузки операторов:");
            Fraction a = new Fraction(1, 2);
            Fraction b = new Fraction(1, 3);
            
            Console.WriteLine($"a = {a}, b = {b}");
            Console.WriteLine($"a + b = {a + b}");
            Console.WriteLine($"a - b = {a - b}");
            Console.WriteLine($"a * b = {a * b}");
            Console.WriteLine($"a / b = {a / b}");
            Console.WriteLine($"a + 2 = {a + 2}");
            Console.WriteLine($"a * 3 = {a * 3}");
            
            // ===== ИТОГИ =====
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("ВСЕ ЗАДАНИЯ ВЫПОЛНЕНЫ:");
            Console.WriteLine("2.1 ✓ Создание дробей и арифметические операции");
            Console.WriteLine("2.2 ✓ Сравнение дробей по состоянию");
            Console.WriteLine("2.3 ✓ Клонирование дробей (интерфейс ICloneable)");
            Console.WriteLine("2.4 ✓ Вещественное значение с кэшированием");
            Console.WriteLine(new string('=', 60));
            
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
