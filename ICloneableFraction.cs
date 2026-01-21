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
}