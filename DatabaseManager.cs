using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;
using System.Globalization;
using System.Data;

namespace AutoParkSystem
{
    // ======================= КЛАСС ДЛЯ РАБОТЫ С БАЗОЙ ДАННЫХ =======================

    public class DatabaseManager
    {
        private List<Car> cars;
        private List<Driver> drivers;
        private List<Trip> trips;
        private string filePath;

        public DatabaseManager(string filePath)
        {
            this.filePath = filePath;
            cars = new List<Car>();
            drivers = new List<Driver>();
            trips = new List<Trip>();

            // Для EPPlus 4.5.3.3 не требуется установка лицензии
        }

        // 1. Чтение данных из Excel
        public void LoadData()
        {
            try
            {
                using (var package = new ExcelPackage(new FileInfo(filePath)))
                {
                    // Чтение таблицы Автомобили
                    var carsSheet = package.Workbook.Worksheets["Автомобили"];
                    if (carsSheet != null)
                    {
                        int rowCount = carsSheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++) // Пропускаем заголовок
                        {
                            if (carsSheet.Cells[row, 1].Value != null)
                            {
                                int carId = Convert.ToInt32(carsSheet.Cells[row, 1].Value);
                                string brand = carsSheet.Cells[row, 2].Value?.ToString() ?? "";
                                string model = carsSheet.Cells[row, 3].Value?.ToString() ?? "";
                                int year = Convert.ToInt32(carsSheet.Cells[row, 4].Value);

                                cars.Add(new Car(carId, brand, model, year));
                            }
                        }
                        Console.WriteLine($"Загружено {cars.Count} автомобилей");
                    }
                    else
                    {
                        Console.WriteLine("Лист 'Автомобили' не найден в файле!");
                    }

                    // Чтение таблицы Водители
                    var driversSheet = package.Workbook.Worksheets["Водители"];
                    if (driversSheet != null)
                    {
                        int rowCount = driversSheet.Dimension.Rows;
                        for (int row = 2; row <= rowCount; row++)
                        {
                            if (driversSheet.Cells[row, 1].Value != null)
                            {
                                int driverId = Convert.ToInt32(driversSheet.Cells[row, 1].Value);
                                string name = driversSheet.Cells[row, 2].Value?.ToString() ?? "";
                                int age = Convert.ToInt32(driversSheet.Cells[row, 3].Value);
                                int experience = Convert.ToInt32(driversSheet.Cells[row, 4].Value);

                                drivers.Add(new Driver(driverId, name, age, experience));
                            }
                        }
                        Console.WriteLine($"Загружено {drivers.Count} водителей");
                    }
                    else
                    {
                        Console.WriteLine("Лист 'Водители' не найден в файле!");
                    }

                    // Чтение таблицы Рейсы (создадим тестовые данные)
                    if (cars.Count > 0 && drivers.Count > 0)
                    {
                        CreateSampleTrips();
                        Console.WriteLine($"Создано {trips.Count} тестовых рейсов");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось создать тестовые рейсы: недостаточно данных об автомобилях или водителях");
                    }
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Файл не найден: {filePath}");
                Console.WriteLine("Поместите файл LR5-var4.xls в папку с исполняемым файлом");
                Console.WriteLine("Или укажите полный путь к файлу");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Ошибка при чтении Excel файла: {ex.Message}");
                Console.WriteLine("Возможно, файл имеет неверный формат или поврежден");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // Создание тестовых рейсов
        private void CreateSampleTrips()
        {
            var random = new Random();
            var startDate = new DateTime(2023, 1, 1);

            for (int i = 1; i <= 100; i++)
            {
                int tripId = i;
                int carId = random.Next(1, cars.Count + 1);
                int driverId = random.Next(1, drivers.Count + 1);

                DateTime tripStart = startDate.AddDays(random.Next(0, 365))
                                            .AddHours(random.Next(0, 24))
                                            .AddMinutes(random.Next(0, 60));
                DateTime tripEnd = tripStart.AddHours(random.Next(1, 12));

                string[] routes = { "Москва-СПб", "Москва-Казань", "Москва-Нижний Новгород",
                                    "Москва-Ростов", "Москва-Владивосток" };
                string route = routes[random.Next(routes.Length)];

                trips.Add(new Trip(tripId, carId, driverId, tripStart, tripEnd, route));
            }
        }

        // 2. Просмотр данных
        public void ViewAllCars()
        {
            Console.WriteLine("\n=== АВТОМОБИЛИ ===");
            if (cars.Count == 0)
            {
                Console.WriteLine("Нет данных об автомобилях");
                return;
            }

            foreach (var car in cars)
            {
                Console.WriteLine(car);
            }
        }

        public void ViewAllDrivers()
        {
            Console.WriteLine("\n=== ВОДИТЕЛИ ===");
            if (drivers.Count == 0)
            {
                Console.WriteLine("Нет данных о водителях");
                return;
            }

            foreach (var driver in drivers)
            {
                Console.WriteLine(driver);
            }
        }

        public void ViewAllTrips()
        {
            Console.WriteLine("\n=== РЕЙСЫ ===");
            if (trips.Count == 0)
            {
                Console.WriteLine("Нет данных о рейсах");
                return;
            }

            foreach (var trip in trips)
            {
                Console.WriteLine(trip);
            }
        }

        // 3. Удаление элементов
        public bool DeleteCar(int carId)
        {
            var car = cars.FirstOrDefault(c => c.CarId == carId);
            if (car != null)
            {
                cars.Remove(car);
                // Также удаляем связанные рейсы
                trips.RemoveAll(t => t.CarId == carId);
                return true;
            }
            return false;
        }

        public bool DeleteDriver(int driverId)
        {
            var driver = drivers.FirstOrDefault(d => d.DriverId == driverId);
            if (driver != null)
            {
                drivers.Remove(driver);
                // Удаляем связанные рейсы
                trips.RemoveAll(t => t.DriverId == driverId);
                return true;
            }
            return false;
        }

        public bool DeleteTrip(int tripId)
        {
            var trip = trips.FirstOrDefault(t => t.TripId == tripId);
            if (trip != null)
            {
                trips.Remove(trip);
                return true;
            }
            return false;
        }

        // 4. Добавление элементов
        public void AddCar(Car car)
        {
            cars.Add(car);
        }

        public void AddDriver(Driver driver)
        {
            drivers.Add(driver);
        }

        public void AddTrip(Trip trip)
        {
            trips.Add(trip);
        }

        // 5. ЗАПРОСЫ
        // средний стаж водителий которые проезжают больше 2000 за рейс.

        // 5.1 Запрос к одной таблице: Получить список автомобилей определенной марки
        public List<Car> GetCarsByBrand(string brand)
        {
            return cars.Where(c => c.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase))
                       .ToList();
        }

        // 5.2 Запрос к двум таблицам: Получить список рейсов с информацией об автомобилях
        public List<dynamic> GetTripsWithCarInfo()
        {
            var query = from trip in trips
                        join car in cars on trip.CarId equals car.CarId
                        select new
                        {
                            TripId = trip.TripId,
                            CarBrand = car.Brand,
                            CarModel = car.Model,
                            StartDate = trip.StartDate,
                            EndDate = trip.EndDate,
                            Route = trip.Route
                        };

            return query.ToList<dynamic>();
        }

        // 5.3 Запрос к трем таблицам (возвращает перечень): 
        // Получить полную информацию о всех рейсах
        public List<dynamic> GetFullTripInfo()
        {
            var query = from trip in trips
                        join car in cars on trip.CarId equals car.CarId
                        join driver in drivers on trip.DriverId equals driver.DriverId
                        select new
                        {
                            TripId = trip.TripId,
                            CarInfo = $"{car.Brand} {car.Model} ({car.Year})",
                            DriverInfo = $"{driver.Name} (Стаж: {driver.Experience} лет)",
                            StartDate = trip.StartDate.ToString("dd.MM.yyyy HH:mm"),
                            EndDate = trip.EndDate.ToString("dd.MM.yyyy HH:mm"),
                            Route = trip.Route,
                            DurationHours = (trip.EndDate - trip.StartDate).TotalHours
                        };

            return query.ToList<dynamic>();
        }

        // 5.3 Запрос к трем таблицам (возвращает одно значение): 
        // Подсчитать общее количество рейсов, выполненных водителями старше 40 лет
        // на автомобилях молоде 10 лет в 2023 году
        public int GetCountOfTripsByOlderDriversOnNewCars()
        {
            var currentYear = DateTime.Now.Year;

            var query = (from trip in trips
                         where trip.StartDate.Year == 2023
                         join car in cars on trip.CarId equals car.CarId
                         join driver in drivers on trip.DriverId equals driver.DriverId
                         where driver.Age > 40 && (currentYear - car.Year) < 10
                         select trip).Count();

            return query;
        }
        // средний стаж водителий которые проезжают больше 2000 за рейс.
        public double GetAverageExperienceOfDriversWithLongTrips2()
        {
            var query = (from d in drivers
                         join t in trips on d.DriverId equals t.DriverId
                         where t.Route > 2000
                         group d by d.DriverId into g
                         select g.First().Experience) 
                        .Average();

            return query;
        }

        // Дополнительный запрос: Пример из задания
        public int GetExampleQueryCount()
        {
            // Пример запроса: 
            // Определите, какое количество рейсов было совершено (началось и закончилось) 
            // в 2023 году на автомобилях марки «Toyota», выпущенных после 2005 года?

            var query = (from trip in trips
                         where trip.StartDate.Year == 2023 && trip.EndDate.Year == 2023
                         join car in cars on trip.CarId equals car.CarId
                         where car.Brand.Equals("Toyota", StringComparison.OrdinalIgnoreCase)
                               && car.Year > 2005
                         select trip).Count();

            return query;
        }

        // 6. Сохранение в Excel
        public void SaveToExcel(string outputPath)
        {
            try
            {
                using (var package = new ExcelPackage())
                {
                    // Сохраняем автомобили
                    var carsSheet = package.Workbook.Worksheets.Add("Автомобили");
                    carsSheet.Cells[1, 1].Value = "ID автомобиля";
                    carsSheet.Cells[1, 2].Value = "Марка";
                    carsSheet.Cells[1, 3].Value = "Модель";
                    carsSheet.Cells[1, 4].Value = "Год выпуска";

                    for (int i = 0; i < cars.Count; i++)
                    {
                        carsSheet.Cells[i + 2, 1].Value = cars[i].CarId;
                        carsSheet.Cells[i + 2, 2].Value = cars[i].Brand;
                        carsSheet.Cells[i + 2, 3].Value = cars[i].Model;
                        carsSheet.Cells[i + 2, 4].Value = cars[i].Year;
                    }

                    // Сохраняем водителей
                    var driversSheet = package.Workbook.Worksheets.Add("Водители");
                    driversSheet.Cells[1, 1].Value = "ID водителя";
                    driversSheet.Cells[1, 2].Value = "Имя";
                    driversSheet.Cells[1, 3].Value = "Возраст";
                    driversSheet.Cells[1, 4].Value = "Стаж вождения";

                    for (int i = 0; i < drivers.Count; i++)
                    {
                        driversSheet.Cells[i + 2, 1].Value = drivers[i].DriverId;
                        driversSheet.Cells[i + 2, 2].Value = drivers[i].Name;
                        driversSheet.Cells[i + 2, 3].Value = drivers[i].Age;
                        driversSheet.Cells[i + 2, 4].Value = drivers[i].Experience;
                    }

                    // Сохраняем рейсы
                    var tripsSheet = package.Workbook.Worksheets.Add("Рейсы");
                    tripsSheet.Cells[1, 1].Value = "ID рейса";
                    tripsSheet.Cells[1, 2].Value = "ID автомобиля";
                    tripsSheet.Cells[1, 3].Value = "ID водителя";
                    tripsSheet.Cells[1, 4].Value = "Дата начала";
                    tripsSheet.Cells[1, 5].Value = "Дата окончания";
                    tripsSheet.Cells[1, 6].Value = "Маршрут";

                    for (int i = 0; i < trips.Count; i++)
                    {
                        tripsSheet.Cells[i + 2, 1].Value = trips[i].TripId;
                        tripsSheet.Cells[i + 2, 2].Value = trips[i].CarId;
                        tripsSheet.Cells[i + 2, 3].Value = trips[i].DriverId;
                        tripsSheet.Cells[i + 2, 4].Value = trips[i].StartDate;
                        tripsSheet.Cells[i + 2, 5].Value = trips[i].EndDate;
                        tripsSheet.Cells[i + 2, 6].Value = trips[i].Route;
                    }

                    // Сохраняем файл
                    var fileInfo = new FileInfo(outputPath);
                    package.SaveAs(fileInfo);
                    Console.WriteLine($"\nДанные сохранены в файл: {outputPath}");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"Ошибка: Нет прав для записи в файл {outputPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        // Геттеры для доступа к данным
        public List<Car> GetCars() => cars;
        public List<Driver> GetDrivers() => drivers;
        public List<Trip> GetTrips() => trips;

        // Метод для проверки, загружены ли данные
        public bool IsDataLoaded()
        {
            return cars.Count > 0 && drivers.Count > 0;
        }
    }
}