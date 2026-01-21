namespace AutoParkSystem
{
    // Класс для водителя
    public class Driver
    {
        public int DriverId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Experience { get; set; }

        public Driver(int driverId, string name, int age, int experience)
        {
            DriverId = driverId;
            Name = name;
            Age = age;
            Experience = experience;
        }

        public override string ToString()
        {
            return $"ID: {DriverId}, Имя: {Name}, Возраст: {Age}, Стаж: {Experience} лет";
        }
    }
}