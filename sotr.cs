using System;

public class Department
{
    public string Name { get; set; }
    public Employee Manager { get; set; }

    public Department(string name)
    {
        Name = name;
        Manager = null;
    }

    public override string ToString()
    {
        if (Manager == null)
            return $"Отдел {Name} (начальник не назначен)";
        return $"Отдел {Name}, начальник: {Manager.Name}";
    }
}

public class Employee
{
    public string Name { get; set; }
    public Department Department { get; set; }

    public Employee(string name, Department department)
    {
        Name = name;
        Department = department;
    }

    public override string ToString()
    {
        if (Department != null && Department.Manager == this)
        {
            return $"{Name} начальник отдела {Department.Name}";
        }
        else if (Department != null && Department.Manager != null)
        {
            return $"{Name} работает в отделе {Department.Name}, начальник которого {Department.Manager.Name}";
        }
        else if (Department != null)
        {
            return $"{Name} работает в отделе {Department.Name} (начальник не назначен)";
        }
        else
        {
            return $"{Name} (не привязан к отделу)";
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== Сотрудники и отделы ===\n");

        Department itDepartment = new Department("IT");

        Employee petrov = new Employee("Петров", itDepartment);
        Employee kozlov = new Employee("Козлов", itDepartment);
        Employee sidorov = new Employee("Сидоров", itDepartment);

        itDepartment.Manager = kozlov;

        Console.WriteLine("Информация об отделе:");
        Console.WriteLine(itDepartment);

        Console.WriteLine("\nИнформация о сотрудниках:");
        Console.WriteLine(petrov);
        Console.WriteLine(kozlov);
        Console.WriteLine(sidorov);

        Console.WriteLine("\n=== Проверка связей ===");
        Console.WriteLine($"Одинаковый ли отдел у всех сотрудников? " +
            $"{(petrov.Department == kozlov.Department && kozlov.Department == sidorov.Department)}");

        Console.WriteLine($"Одинаковый ли начальник у всех сотрудников? " +
            $"{(petrov.Department.Manager == kozlov.Department.Manager && kozlov.Department.Manager == sidorov.Department.Manager)}");

        Console.WriteLine($"Козлов является начальником? {itDepartment.Manager == kozlov}");
    }
}