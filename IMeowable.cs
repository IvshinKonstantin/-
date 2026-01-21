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
}