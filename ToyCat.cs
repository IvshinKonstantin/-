using System;

namespace Lab6_CatAssignment
{
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
}