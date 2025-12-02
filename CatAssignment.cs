using System;
using System.Linq;

namespace Lab6_CatAssignment
{
    // ========== ИНТЕРФЕЙСЫ ==========
    
    /// <summary>
    /// Интерфейс для объектов, способных мяукать
    /// </summary>
    public interface IMeowable
    {
        /// <summary>
        /// Издать мяуканье
        /// </summary>
        void Meow();
        
        /// <summary>
        /// Получить количество произведенных мяуканий
        /// </summary>
        int MeowCount { get; }
    }

    // ========== КЛАССЫ МОДЕЛЕЙ ==========
    
    /// <summary>
    /// Класс, представляющий кота
    /// </summary>
    public class Cat : IMeowable
    {
        private string _name;
        private int _meowCount;
        
        /// <summary>
        /// Имя кота
        /// </summary>
        public string Name 
        { 
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Имя не может быть пустым");
                _name = value;
            }
        }
        
        /// <summary>
        /// Количество произведенных мяуканий
        /// </summary>
        public int MeowCount => _meowCount;
        
        /// <summary>
        /// Создает нового кота
        /// </summary>
        /// <param name="name">Имя кота</param>
        public Cat(string name)
        {
            Name = name;
            _meowCount = 0;
        }
        
        /// <summary>
        /// Приведение к текстовой форме
        /// </summary>
        /// <returns>Строковое представление кота</returns>
        public override string ToString() => $"кот: {Name}";
        
        /// <summary>
        /// Мяукнуть один раз
        /// </summary>
        public void Meow()
        {
            Console.WriteLine($"{Name}: мяу!");
            _meowCount++;
        }
        
        /// <summary>
        /// Мяукнуть несколько раз
        /// </summary>
        /// <param name="n">Количество мяуканий</param>
        public void Meow(int n)
        {
            if (n <= 0)
                throw new ArgumentException("Количество мяуканий должно быть положительным");
                
            string meows = string.Join("-", new string[n].Select(_ => "мяу"));
            Console.WriteLine($"{Name}: {meows}!");
            _meowCount += n;
        }
    }
    
    /// <summary>
    /// Пример другого класса, который может мяукать
    /// </summary>
    public class RobotCat : IMeowable
    {
        private string _model;
        private int _meowCount;
        
        /// <summary>
        /// Модель робокота
        /// </summary>
        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Модель не может быть пустой");
                _model = value;
            }
        }
        
        /// <summary>
        /// Количество произведенных мяуканий
        /// </summary>
        public int MeowCount => _meowCount;
        
        /// <summary>
        /// Создает нового робокота
        /// </summary>
        /// <param name="model">Модель робокота</param>
        public RobotCat(string model)
        {
            Model = model;
            _meowCount = 0;
        }
        
        /// <summary>
        /// Издать мяуканье (роботизированное)
        /// </summary>
        public void Meow()
        {
            Console.WriteLine($"{Model}: бип-мяу!");
            _meowCount++;
        }
        
        /// <summary>
        /// Приведение к текстовой форме
        /// </summary>
        /// <returns>Строковое представление робокота</returns>
        public override string ToString() => $"робокот: {Model}";
    }
    
    /// <summary>
    /// Еще один пример мяукающего объекта
    /// </summary>
    public class ToyCat : IMeowable
    {
        private string _color;
        private int _meowCount;
        
        /// <summary>
        /// Цвет игрушечного кота
        /// </summary>
        public string Color
        {
            get => _color;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Цвет не может быть пустым");
                _color = value;
            }
        }
        
        /// <summary>
        /// Количество произведенных мяуканий
        /// </summary>
        public int MeowCount => _meowCount;
        
        /// <summary>
        /// Создает нового игрушечного кота
        /// </summary>
        /// <param name="color">Цвет кота</param>
        public ToyCat(string color)
        {
            Color = color;
            _meowCount = 0;
        }
        
        /// <summary>
        /// Издать мяуканье (игрушечное)
        /// </summary>
        public void Meow()
        {
            Console.WriteLine($"{Color} игрушечный кот: пи-мяу!");
            _meowCount++;
        }
        
        /// <summary>
        /// Приведение к текстовой форме
        /// </summary>
        public override string ToString() => $"игрушечный кот: {Color}";
    }

    // ========== СЕРВИСНЫЕ КЛАССЫ ==========
    
    /// <summary>
    /// Статический класс с функциями для работы с мяукающими объектами
    /// </summary>
    public static class Funs
    {
        /// <summary>
        /// Вызывает мяуканье у каждого объекта
        /// </summary>
        /// <param name="meowables">Коллекция мяукающих объектов</param>
        public static void MeowsCare(params IMeowable[] meowables)
        {
            if (meowables == null)
                throw new ArgumentNullException(nameof(meowables));
                
            foreach (var meowable in meowables)
            {
                meowable.Meow();
            }
        }
        
        /// <summary>
        /// Вызывает мяуканье у каждого объекта несколько раз
        /// (метод для демонстрации, не требуется по заданию)
        /// </summary>
        public static void MeowsCare(int times, params IMeowable[] meowables)
        {
            if (meowables == null)
                throw new ArgumentNullException(nameof(meowables));
            if (times <= 0)
                throw new ArgumentException("Количество раз должно быть положительным");
                
            foreach (var meowable in meowables)
            {
                for (int i = 0; i < times; i++)
                {
                    meowable.Meow();
                }
            }
        }
        
        /// <summary>
        /// Метод из задания 1.3 - вызывает мяуканье 5 раз
        /// </summary>
        /// <param name="meowable">Мяукающий объект</param>
        public static void MeowsCare(IMeowable meowable)
        {
            if (meowable == null)
                throw new ArgumentNullException(nameof(meowable));
            
            // Вызываем мяуканье 5 раз как в примере задания
            for (int i = 0; i < 5; i++)
            {
                meowable.Meow();
            }
        }
    }
    
    /// <summary>
    /// Дополнительный сервис для работы с мяукающими объектами
    /// </summary>
    public static class MeowService
    {
        /// <summary>
        /// Вызывает мяуканье у всех объектов в коллекции
        /// </summary>
        /// <param name="meowables">Коллекция мяукающих объектов</param>
        public static void MakeAllMeow(params IMeowable[] meowables)
        {
            Funs.MeowsCare(meowables);
        }
    }

    // ========== ОСНОВНАЯ ПРОГРАММА ==========
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №6 - ЗАДАНИЕ 1: КОТЫ ===\n");
            
            // ===== ЗАДАНИЕ 1.1 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.1: Кот мяукает =======");
            
            // Создаем кота по имени "Барсик"
            Cat barsik = new Cat("Барсик");
            Console.WriteLine($"Создан: {barsik}");
            
            // Мяукаем один раз
            Console.Write("Мяуканье 1 раз: ");
            barsik.Meow();
            
            // Мяукаем три раза
            Console.Write("Мяуканье 3 раза: ");
            barsik.Meow(3);
            
            Console.WriteLine($"Всего мяуканий: {barsik.MeowCount}\n");
            
            // ===== ЗАДАНИЕ 1.2 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.2: Интерфейс Мяуканье =======");
            
            // Создаем несколько котов
            Cat murzik = new Cat("Мурзик");
            Cat vaska = new Cat("Васька");
            
            // Создаем другие объекты, которые могут мяукать
            RobotCat robotCat = new RobotCat("RC-3000");
            ToyCat toyCat = new ToyCat("рыжий");
            
            Console.WriteLine("Вызываем мяуканье у всех объектов:");
            
            // Используем метод MeowsCare с интерфейсом IMeowable
            Funs.MeowsCare(barsik, murzik, vaska, robotCat, toyCat);
            
            Console.WriteLine($"\nСтатистика мяуканий:");
            Console.WriteLine($"{barsik}: {barsik.MeowCount} раз");
            Console.WriteLine($"{murzik}: {murzik.MeowCount} раз");
            Console.WriteLine($"{vaska}: {vaska.MeowCount} раз");
            Console.WriteLine($"{robotCat}: {robotCat.MeowCount} раз");
            Console.WriteLine($"{toyCat}: {toyCat.MeowCount} раз\n");
            
            // ===== ЗАДАНИЕ 1.3 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.3: Количество мяуканий =======");
            
            // Создаем нового кота для теста
            Cat testCat = new Cat("Тестовый");
            Console.WriteLine($"Создан: {testCat}");
            Console.WriteLine($"Начальное количество мяуканий: {testCat.MeowCount}");
            
            // Сохраняем ссылку на кота
            IMeowable meowableCat = testCat;
            
            // Вызываем метод MeowsCare (кот будет мяукать 5 раз)
            Console.WriteLine("\nВызываем Funs.MeowsCare(testCat)...");
            Funs.MeowsCare(meowableCat);
            
            // Проверяем количество мяуканий после вызова метода
            Console.WriteLine($"\nПосле вызова метода:");
            Console.WriteLine($"{testCat.Name} мяукал {testCat.MeowCount} раз");
            
            // Демонстрация с разными объектами
            Console.WriteLine("\n--- Дополнительная демонстрация ---");
            
            // Сбрасываем счетчик
            testCat = new Cat("Счетчик");
            Console.WriteLine($"\nНовый кот: {testCat}");
            Console.WriteLine($"До вызова: {testCat.MeowCount} мяуканий");
            
            // Передаем ссылку в метод
            Funs.MeowsCare(testCat);
            
            // Выводим результат
            Console.WriteLine($"После вызова: {testCat.MeowCount} мяуканий");
            Console.WriteLine($"ИТОГ: кот мяукал {testCat.MeowCount} раз");
            
            // ===== ДОПОЛНИТЕЛЬНАЯ ПРОВЕРКА =====
            Console.WriteLine("\n======= ДОПОЛНИТЕЛЬНАЯ ПРОВЕРКА =======");
            
            // Проверка с массивом объектов
            IMeowable[] meowArray = new IMeowable[]
            {
                new Cat("Рыжик"),
                new RobotCat("AI-Cat"),
                new ToyCat("синий")
            };
            
            Console.WriteLine("\nМассив мяукающих объектов:");
            foreach (var obj in meowArray)
            {
                Console.WriteLine($"  - {obj}");
            }
            
            Console.WriteLine("\nВызываем мяуканье у всех объектов массива:");
            Funs.MeowsCare(meowArray);
            
            Console.WriteLine("\nСтатистика по массиву:");
            foreach (var obj in meowArray)
            {
                if (obj is Cat cat)
                    Console.WriteLine($"  {cat.Name}: {cat.MeowCount} мяуканий");
                else if (obj is RobotCat rc)
                    Console.WriteLine($"  {rc.Model}: {rc.MeowCount} мяуканий");
                else if (obj is ToyCat tc)
                    Console.WriteLine($"  {tc.Color} игрушка: {tc.MeowCount} мяуканий");
            }
            
            // ===== ИТОГИ =====
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("ВСЕ ЗАДАНИЯ ВЫПОЛНЕНЫ:");
            Console.WriteLine("1.1 ✓ Создание кота и мяуканье");
            Console.WriteLine("1.2 ✓ Интерфейс IMeowable и метод для всех мяукающих");
            Console.WriteLine("1.3 ✓ Подсчет количества мяуканий");
            Console.WriteLine(new string('=', 50));
            
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
