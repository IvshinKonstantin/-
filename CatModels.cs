using System;
using System.Linq;

namespace Lab6_CatAssignment
{
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
}
