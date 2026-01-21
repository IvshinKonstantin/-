using System;

namespace AutoParkSystem
{
    // Класс для рейса
    public class Trip
    {
        public int TripId { get; set; }
        public int CarId { get; set; }
        public int DriverId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Route { get; set; }

        public Trip(int tripId, int carId, int driverId, DateTime startDate, DateTime endDate, string route)
        {
            TripId = tripId;
            CarId = carId;
            DriverId = driverId;
            StartDate = startDate;
            EndDate = endDate;
            Route = route;
        }

        public override string ToString()
        {
            return $"ID рейса: {TripId}, ID авто: {CarId}, ID водителя: {DriverId}, " +
                   $"Начало: {StartDate:dd.MM.yyyy HH:mm}, Окончание: {EndDate:dd.MM.yyyy HH:mm}, Маршрут: {Route}";
        }
    }
}