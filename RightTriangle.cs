using System;

namespace OperatorOverloading
{
    public class RightTriangle
    {
        private double a;
        private double b;

        public double A
        {
            get { return a; }
            set
            {
                if (value >= 0)
                    a = value;
                else
                    throw new ArgumentException("Длина катета не может быть отрицательной");
            }
        }

        public double B
        {
            get { return b; }
            set
            {
                if (value >= 0)
                    b = value;
                else
                    throw new ArgumentException("Длина катета не может быть отрицательной");
            }
        }

        public RightTriangle(double a, double b)
        {
            A = a;
            B = b;
        }

        // === ЗАДАНИЕ 6 ===
        // Метод: вычисление площади
        public double Area()
        {
            return A * B / 2.0;
        }

        // Переопределение ToString
        public override string ToString()
        {
            return $"Прямоугольный треугольник: катеты {A:F2} и {B:F2}, площадь = {Area():F2}";
        }

        // === ЗАДАНИЕ 7 ===
        
        // Унарные операторы ++ и --
        public static RightTriangle operator ++(RightTriangle t)
        {
            return new RightTriangle(t.A * 2.0, t.B * 2.0);
        }

        public static RightTriangle operator --(RightTriangle t)
        {
            if (t.A / 2.0 < 0 || t.B / 2.0 < 0)
                throw new InvalidOperationException("Нельзя уменьшить треугольник до отрицательных размеров");
            return new RightTriangle(t.A / 2.0, t.B / 2.0);
        }

        // Явное приведение к double (площадь)
        public static explicit operator double(RightTriangle t)
        {
            return t.Area();
        }

        // Неявное приведение к bool (существует ли треугольник)
        public static implicit operator bool(RightTriangle t)
        {
            return t.A > 0 && t.B > 0;
        }

        // Операторы сравнения площадей
        public static bool operator <=(RightTriangle t1, RightTriangle t2)
        {
            double area1 = t1.Area();
            double area2 = t2.Area();
            return area1 <= area2;
        }

        public static bool operator >=(RightTriangle t1, RightTriangle t2)
        {
            double area1 = t1.Area();
            double area2 = t2.Area();
            return area1 >= area2;
        }

        public static bool operator <(RightTriangle t1, RightTriangle t2)
        {
            double area1 = t1.Area();
            double area2 = t2.Area();
            return area1 < area2;
        }

        public static bool operator >(RightTriangle t1, RightTriangle t2)
        {
            double area1 = t1.Area();
            double area2 = t2.Area();
            return area1 > area2;
        }

        // Операторы равенства
        public static bool operator ==(RightTriangle t1, RightTriangle t2)
        {
            if (ReferenceEquals(t1, t2)) return true;
            if (ReferenceEquals(t1, null) || ReferenceEquals(t2, null)) return false;
            return Math.Abs(t1.A - t2.A) < 0.001 && Math.Abs(t1.B - t2.B) < 0.001;
        }

        public static bool operator !=(RightTriangle t1, RightTriangle t2)
        {
            return !(t1 == t2);
        }

        // Переопределение Equals и GetHashCode
        public override bool Equals(object obj)
        {
            if (obj is RightTriangle other)
                return Math.Abs(A - other.A) < 0.001 && Math.Abs(B - other.B) < 0.001;
            return false;
        }

        public override int GetHashCode()
        {
            return (A.GetHashCode() * 397) ^ B.GetHashCode();
        }
    }
}
