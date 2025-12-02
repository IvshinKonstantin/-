using System;

namespace Lab6_FractionAssignment
{
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
        private static int GCD(int a, int b)
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
            if (other is null)
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
            if (other is null)
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
            if (other is null)
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
            if (other is null)
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
        public bool Equals(Fraction? other)
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
        public override bool Equals(object? obj)
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
        public static bool operator ==(Fraction? left, Fraction? right)
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
        public static bool operator !=(Fraction? left, Fraction? right)
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
    }
}
