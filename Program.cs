using System;
using System.Linq;

namespace AutoParkSystem
{
    class Program
    {
        static DatabaseManager dbManager;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Система управления автопарком";

            // Путь к файлу Excel
            string filePath = "LR5-var4.xlsx"; // Используйте .xlsx формат

            // Проверка существования файла
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Файл {filePath} не найден!");
                Console.WriteLine("Пожалуйста, выполните следующие шаги:");
                Console.WriteLine("1. Убедитесь, что файл находится в папке с программой");
                Console.WriteLine("2. Если у вас файл .xls, откройте его в Excel и сохраните как .xlsx");
                Console.WriteLine("3. Переименуйте файл в LR5-var4.xlsx");
                Console.Write("\nВведите путь к файлу вручную (или нажмите Enter для выхода): ");

                filePath = Console.ReadLine();
                if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
                {
                    Console.WriteLine("Выход из программы...");
                    Console.ReadKey();
                    return;
                }
            }

            // Инициализация менеджера базы данных
            dbManager = new DatabaseManager(filePath);

            // Загрузка данных
            Console.WriteLine("Загрузка данных из файла...");
            dbManager.LoadData();

            if (!dbManager.IsDataLoaded())
            {
                Console.WriteLine("\nНе удалось загрузить данные. Проверьте файл Excel.");
                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
                return;
            }

            bool exit = false;

            while (!exit)
            {
                ShowMenu();
                Console.Write("\nВыберите действие (1-6): ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            ViewData();
                            break;
                        case 2:
                            DeleteItem();
                            break;
                        case 3:
                            AddItem();
                            break;
                        case 4:
                            ExecuteQueries();
                            break;
                        case 5:
                            SaveData();
                            break;
                        case 6:
                            exit = true;
                            Console.WriteLine("Выход из программы...");
                            break;
                        default:
                            Console.WriteLine("Неверный выбор! Введите число от 1 до 6.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Ошибка ввода! Введите число от 1 до 6.");
                }

                if (!exit)
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("       СИСТЕМА УПРАВЛЕНИЯ АВТОПАРКОМ");
            Console.WriteLine(new string('=', 50));
            Console.WriteLine("1. Просмотр данных");
            Console.WriteLine("2. Удаление элемента");
            Console.WriteLine("3. Добавление элемента");
            Console.WriteLine("4. Выполнение запросов");
            Console.WriteLine("5. Сохранение данных");
            Console.WriteLine("6. Выход");
            Console.WriteLine(new string('-', 50));
        }

        static void ViewData()
        {
            Console.Clear();
            Console.WriteLine("=== ПРОСМОТР ДАННЫХ ===");
            Console.WriteLine("1. Просмотреть все автомобили");
            Console.WriteLine("2. Просмотреть всех водителей");
            Console.WriteLine("3. Просмотреть все рейсы");
            Console.WriteLine("4. Просмотреть статистику");
            Console.Write("\nВыберите вариант (1-4): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        dbManager.ViewAllCars();
                        break;
                    case 2:
                        dbManager.ViewAllDrivers();
                        break;
                    case 3:
                        dbManager.ViewAllTrips();
                        break;
                    case 4:
                        ShowStatistics();
                        break;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }

        static void ShowStatistics()
        {
            var cars = dbManager.GetCars();
            var drivers = dbManager.GetDrivers();
            var trips = dbManager.GetTrips();

            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.WriteLine($"Всего автомобилей: {cars.Count}");
            Console.WriteLine($"Всего водителей: {drivers.Count}");
            Console.WriteLine($"Всего рейсов: {trips.Count}");

            // Статистика по автомобилям
            var carBrands = cars.GroupBy(c => c.Brand)
                               .Select(g => new { Brand = g.Key, Count = g.Count() })
                               .OrderByDescending(x => x.Count);

            Console.WriteLine("\nАвтомобили по маркам:");
            foreach (var item in carBrands.Take(5))
            {
                Console.WriteLine($"  {item.Brand}: {item.Count} авто");
            }

            // Статистика по водителям
            var avgAge = drivers.Average(d => d.Age);
            var avgExp = drivers.Average(d => d.Experience);
            Console.WriteLine($"\nСредний возраст водителей: {avgAge:F1} лет");
            Console.WriteLine($"Средний стаж вождения: {avgExp:F1} лет");
        }

        static void DeleteItem()
        {
            Console.Clear();
            Console.WriteLine("=== УДАЛЕНИЕ ЭЛЕМЕНТА ===");
            Console.WriteLine("1. Удалить автомобиль");
            Console.WriteLine("2. Удалить водителя");
            Console.WriteLine("3. Удалить рейс");
            Console.Write("\nВыберите тип элемента (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    Console.Write("Введите ID для удаления: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        bool success = false;

                        switch (choice)
                        {
                            case 1:
                                success = dbManager.DeleteCar(id);
                                Console.WriteLine(success ? "Автомобиль успешно удален!" : "Автомобиль не найден!");
                                break;
                            case 2:
                                success = dbManager.DeleteDriver(id);
                                Console.WriteLine(success ? "Водитель успешно удален!" : "Водитель не найден!");
                                break;
                            case 3:
                                success = dbManager.DeleteTrip(id);
                                Console.WriteLine(success ? "Рейс успешно удален!" : "Рейс не найден!");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ID!");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при удалении: {ex.Message}");
                }
            }
        }

        static void AddItem()
        {
            Console.Clear();
            Console.WriteLine("=== ДОБАВЛЕНИЕ ЭЛЕМЕНТА ===");
            Console.WriteLine("1. Добавить автомобиль");
            Console.WriteLine("2. Добавить водителя");
            Console.WriteLine("3. Добавить рейс");
            Console.Write("\nВыберите тип элемента (1-3): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nВведите ID автомобиля: ");
                            int carId = int.Parse(Console.ReadLine());
                            Console.Write("Введите марку: ");
                            string brand = Console.ReadLine();
                            Console.Write("Введите модель: ");
                            string model = Console.ReadLine();
                            Console.Write("Введите год выпуска: ");
                            int year = int.Parse(Console.ReadLine());

                            dbManager.AddCar(new Car(carId, brand, model, year));
                            Console.WriteLine("\nАвтомобиль успешно добавлен!");
                            break;

                        case 2:
                            Console.Write("\nВведите ID водителя: ");
                            int driverId = int.Parse(Console.ReadLine());
                            Console.Write("Введите имя: ");
                            string name = Console.ReadLine();
                            Console.Write("Введите возраст: ");
                            int age = int.Parse(Console.ReadLine());
                            Console.Write("Введите стаж: ");
                            int experience = int.Parse(Console.ReadLine());

                            dbManager.AddDriver(new Driver(driverId, name, age, experience));
                            Console.WriteLine("\nВодитель успешно добавлен!");
                            break;

                        case 3:
                            Console.Write("\nВведите ID рейса: ");
                            int tripId = int.Parse(Console.ReadLine());
                            Console.Write("Введите ID автомобиля: ");
                            int tripCarId = int.Parse(Console.ReadLine());
                            Console.Write("Введите ID водителя: ");
                            int tripDriverId = int.Parse(Console.ReadLine());
                            Console.Write("Введите дату начала (гггг-мм-дд чч:мм): ");
                            DateTime startDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Введите дату окончания (гггг-мм-дд чч:мм): ");
                            DateTime endDate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Введите маршрут: ");
                            string route = Console.ReadLine();

                            dbManager.AddTrip(new Trip(tripId, tripCarId, tripDriverId, startDate, endDate, route));
                            Console.WriteLine("\nРейс успешно добавлен!");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка формата данных! Проверьте введенные значения.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при добавлении: {ex.Message}");
                }
            }
        }

        static void ExecuteQueries()
        {
            Console.Clear();
            Console.WriteLine("=== ВЫПОЛНЕНИЕ ЗАПРОСОВ ===");
            Console.WriteLine("1. Найти автомобили по марке");
            Console.WriteLine("2. Получить рейсы с информацией об автомобилях");
            Console.WriteLine("3. Получить полную информацию о рейсах");
            Console.WriteLine("4. Подсчитать рейсы водителей >40 лет на авто <10 лет");
            Console.WriteLine("5. Пример запроса из задания (Toyota после 2005)");
            Console.Write("\nВыберите запрос (1-5): ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                try
                {
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nВведите марку автомобиля: ");
                            string brand = Console.ReadLine();
                            var cars = dbManager.GetCarsByBrand(brand);
                            Console.WriteLine($"\nНайдено {cars.Count} автомобилей марки '{brand}':");
                            foreach (var car in cars)
                            {
                                Console.WriteLine(car);
                            }
                            break;

                        case 2:
                            var tripsWithCars = dbManager.GetTripsWithCarInfo();
                            Console.WriteLine($"\nНайдено {tripsWithCars.Count} рейсов:");
                            foreach (dynamic trip in tripsWithCars.Take(20)) // Показываем первые 20
                            {
                                Console.WriteLine($"Рейс {trip.TripId}: {trip.CarBrand} {trip.CarModel}, " +
                                                $"с {trip.StartDate:dd.MM.yyyy} по {trip.EndDate:dd.MM.yyyy}, " +
                                                $"маршрут: {trip.Route}");
                            }
                            if (tripsWithCars.Count > 20)
                            {
                                Console.WriteLine($"... и еще {tripsWithCars.Count - 20} рейсов");
                            }
                            break;

                        case 3:
                            var fullInfo = dbManager.GetFullTripInfo();
                            Console.WriteLine($"\nПолная информация о рейсах (первые 10):");
                            foreach (dynamic info in fullInfo.Take(10))
                            {
                                Console.WriteLine($"Рейс {info.TripId}:");
                                Console.WriteLine($"  Автомобиль: {info.CarInfo}");
                                Console.WriteLine($"  Водитель: {info.DriverInfo}");
                                Console.WriteLine($"  Период: {info.StartDate} - {info.EndDate}");
                                Console.WriteLine($"  Маршрут: {info.Route}");
                                Console.WriteLine($"  Длительность: {info.DurationHours:F1} часов");
                                Console.WriteLine();
                            }
                            if (fullInfo.Count > 10)
                            {
                                Console.WriteLine($"... и еще {fullInfo.Count - 10} рейсов");
                            }
                            break;

                        case 4:
                            int count = dbManager.GetCountOfTripsByOlderDriversOnNewCars();
                            Console.WriteLine($"\nКоличество рейсов в 2023 году, выполненных водителями " +
                                            $"старше 40 лет на автомобилях моложе 10 лет: {count}");
                            break;

                        case 5:
                            int exampleCount = dbManager.GetExampleQueryCount();
                            Console.WriteLine($"\nПример запроса из задания:");
                            Console.WriteLine("Определите, какое количество рейсов было совершено");
                            Console.WriteLine("(началось и закончилось) в 2023 году на автомобилях");
                            Console.WriteLine("марки «Toyota», выпущенных после 2005 года?");
                            Console.WriteLine($"\nОтвет: {exampleCount}");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при выполнении запроса: {ex.Message}");
                }
            }
        }

        static void SaveData()
        {
            Console.Clear();
            Console.WriteLine("=== СОХРАНЕНИЕ ДАННЫХ ===");
            Console.WriteLine("\nВнимание: Будет создан новый файл Excel со всеми данными.");
            Console.Write("\nВведите имя файла для сохранения (например: output.xlsx): ");
            string fileName = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                // Добавляем расширение .xlsx, если его нет
                if (!fileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase))
                {
                    fileName += ".xlsx";
                }

                dbManager.SaveToExcel(fileName);
            }
            else
            {
                Console.WriteLine("Имя файла не может быть пустым!");
            }
        }
    }
}