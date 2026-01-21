using System;

namespace Lab6_CatAssignment
{
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