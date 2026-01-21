using System;
using System.Linq;

namespace Lab6_CatAssignment
{
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
}