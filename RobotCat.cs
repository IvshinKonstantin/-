using System;

namespace Lab6_CatAssignment
{
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
}