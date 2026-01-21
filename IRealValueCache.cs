namespace Lab6_FractionAssignment
{
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
}