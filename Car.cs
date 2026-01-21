namespace AutoParkSystem
{
    // Класс для автомобиля
    public class Car
    {
        public int CarId { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public Car(int carId, string brand, string model, int year)
        {
            CarId = carId;
            Brand = brand;
            Model = model;
            Year = year;
        }

        public override string ToString()
        {
            return $"ID: {CarId}, Марка: {Brand}, Модель: {Model}, Год выпуска: {Year}";
        }
    }
}