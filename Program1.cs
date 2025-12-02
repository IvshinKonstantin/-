using System;

namespace Lab6_CatAssignment
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ЛАБОРАТОРНАЯ РАБОТА №6 - ЗАДАНИЕ 1: КОТЫ ===\n");
            
            // ===== ЗАДАНИЕ 1.1 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.1: Кот мяукает =======");
            
            // Создаем кота по имени "Барсик"
            Cat barsik = new Cat("Барсик");
            Console.WriteLine($"Создан: {barsik}");
            
            // Мяукаем один раз
            Console.Write("Мяуканье 1 раз: ");
            barsik.Meow();
            
            // Мяукаем три раза
            Console.Write("Мяуканье 3 раза: ");
            barsik.Meow(3);
            
            Console.WriteLine($"Всего мяуканий: {barsik.MeowCount}\n");
            
            // ===== ЗАДАНИЕ 1.2 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.2: Интерфейс Мяуканье =======");
            
            // Создаем несколько котов
            Cat murzik = new Cat("Мурзик");
            Cat vaska = new Cat("Васька");
            
            // Создаем другие объекты, которые могут мяукать
            RobotCat robotCat = new RobotCat("RC-3000");
            ToyCat toyCat = new ToyCat("рыжий");
            
            Console.WriteLine("Вызываем мяуканье у всех объектов:");
            
            // Используем метод MeowsCare с интерфейсом IMeowable
            Funs.MeowsCare(barsik, murzik, vaska, robotCat, toyCat);
            
            Console.WriteLine($"\nСтатистика мяуканий:");
            Console.WriteLine($"{barsik}: {barsik.MeowCount} раз");
            Console.WriteLine($"{murzik}: {murzik.MeowCount} раз");
            Console.WriteLine($"{vaska}: {vaska.MeowCount} раз");
            Console.WriteLine($"{robotCat}: {robotCat.MeowCount} раз");
            Console.WriteLine($"{toyCat}: {toyCat.MeowCount} раз\n");
            
            // ===== ЗАДАНИЕ 1.3 =====
            Console.WriteLine("======= ЗАДАНИЕ 1.3: Количество мяуканий =======");
            
            // Создаем нового кота для теста
            Cat testCat = new Cat("Тестовый");
            Console.WriteLine($"Создан: {testCat}");
            Console.WriteLine($"Начальное количество мяуканий: {testCat.MeowCount}");
            
            // Сохраняем ссылку на кота
            IMeowable meowableCat = testCat;
            
            // Вызываем метод MeowsCare (кот будет мяукать 5 раз)
            Console.WriteLine("\nВызываем Funs.MeowsCare(testCat)...");
            Funs.MeowsCare(meowableCat);
            
            // Проверяем количество мяуканий после вызова метода
            Console.WriteLine($"\nПосле вызова метода:");
            Console.WriteLine($"{testCat.Name} мяукал {testCat.MeowCount} раз");
            
            // Дополнительная демонстрация
            Console.WriteLine("\n--- Дополнительная демонстрация ---");
            Cat counterCat = new Cat("Счетчик");
            Console.WriteLine($"\nНовый кот: {counterCat}");
            Console.WriteLine($"До вызова: {counterCat.MeowCount} мяуканий");
            
            Funs.MeowsCare(counterCat);
            Console.WriteLine($"После вызова: {counterCat.MeowCount} мяуканий");
            
            // ===== ИТОГИ =====
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("ВСЕ ЗАДАНИЯ ВЫПОЛНЕНЫ:");
            Console.WriteLine("1.1 ✓ Создание кота и мяуканье");
            Console.WriteLine("1.2 ✓ Интерфейс IMeowable и метод для всех мяукающих");
            Console.WriteLine("1.3 ✓ Подсчет количества мяуканий");
            Console.WriteLine(new string('=', 50));
            
            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
