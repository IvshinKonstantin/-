using System;
using System.Collections.Generic;
using System.Linq;

// Класс Отдел
public class Department
{
    public string Name { get; set; }
    public Employee Manager { get; set; }
    private List<Employee> employees; // Список сотрудников отдела

    public Department(string name)
    {
        Name = name;
        Manager = null;
        employees = new List<Employee>();
    }

    // Метод для добавления сотрудника в отдел
    public void AddEmployee(Employee employee)
    {
        if (!employees.Contains(employee))
        {
            employees.Add(employee);
            employee.Department = this; // Устанавливаем связь с отделом
        }
    }

    // Метод для удаления сотрудника из отдела
    public void RemoveEmployee(Employee employee)
    {
        if (employees.Contains(employee))
        {
            employees.Remove(employee);
            employee.Department = null;
        }
    }

    // Свойство для получения списка сотрудников
    public List<Employee> Employees => employees.ToList(); // Возвращаем копию списка

    // Метод для получения количества сотрудников
    public int EmployeeCount => employees.Count;

    public override string ToString()
    {
        if (Manager == null)
            return $"Отдел {Name} (сотрудников: {EmployeeCount}, начальник не назначен)";
        return $"Отдел {Name} (сотрудников: {EmployeeCount}), начальник: {Manager.Name}";
    }

    // Метод для получения всех сотрудников, кроме начальника
    public List<Employee> GetRegularEmployees()
    {
        return employees.Where(e => e != Manager).ToList();
    }
}

// Класс Сотрудник
public class Employee
{
    public string Name { get; set; }
    public Department Department { get; set; }

    public Employee(string name)
    {
        Name = name;
        Department = null;
    }

    // Метод для присоединения к отделу
    public void JoinDepartment(Department department)
    {
        if (Department != null)
        {
            Department.RemoveEmployee(this);
        }

        department.AddEmployee(this);
    }

    // Метод для получения списка всех коллег по отделу
    public List<Employee> GetDepartmentColleagues()
    {
        if (Department == null)
            return new List<Employee>();

        return Department.Employees.Where(e => e != this).ToList();
    }

    // Метод для получения всех подчиненных (если начальник)
    public List<Employee> GetSubordinates()
    {
        if (Department == null || Department.Manager != this)
            return new List<Employee>();

        return Department.GetRegularEmployees();
    }

    public override string ToString()
    {
        if (Department != null && Department.Manager == this)
        {
            return $"{Name} начальник отдела {Department.Name} (подчиненных: {GetSubordinates().Count})";
        }
        else if (Department != null && Department.Manager != null)
        {
            var colleagues = GetDepartmentColleagues();
            return $"{Name} работает в отделе {Department.Name}, " +
                   $"начальник {Department.Manager.Name}, " +
                   $"коллег в отделе: {colleagues.Count}";
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
        Console.WriteLine("=== Сотрудники и отделы (расширенная версия) ===\n");

        // 1. Создаем отдел IT
        Department itDepartment = new Department("IT");

        // 2. Создаем сотрудников
        Employee petrov = new Employee("Петров");
        Employee kozlov = new Employee("Козлов");
        Employee sidorov = new Employee("Сидоров");
        Employee ivanov = new Employee("Иванов"); // Добавляем еще одного сотрудника

        // 3. Добавляем сотрудников в отдел
        petrov.JoinDepartment(itDepartment);
        kozlov.JoinDepartment(itDepartment);
        sidorov.JoinDepartment(itDepartment);
        ivanov.JoinDepartment(itDepartment);

        // 4. Делаем Козлова начальником IT отдела
        itDepartment.Manager = kozlov;

        Console.WriteLine("Информация об отделе:");
        Console.WriteLine(itDepartment);

        Console.WriteLine("\nИнформация о сотрудниках:");
        Console.WriteLine(petrov);
        Console.WriteLine(kozlov);
        Console.WriteLine(sidorov);
        Console.WriteLine(ivanov);

        Console.WriteLine("\n=== Использование новой функциональности ===");

        // 5. Используем ссылку на сотрудника для получения информации об отделе
        Console.WriteLine("\nИспользуя ссылку на Петрова:");
        Console.WriteLine($"Все сотрудники отдела Петрова ({petrov.Department.Name}):");
        foreach (var colleague in petrov.GetDepartmentColleagues())
        {
            Console.WriteLine($"  - {colleague.Name}");
        }

        Console.WriteLine($"Всего сотрудников в отделе: {petrov.Department.EmployeeCount}");

        Console.WriteLine("\nИспользуя ссылку на Козлова (начальника):");
        Console.WriteLine($"Подчиненные Козлова:");
        var subordinates = kozlov.GetSubordinates();
        if (subordinates.Count > 0)
        {
            foreach (var subordinate in subordinates)
            {
                Console.WriteLine($"  - {subordinate.Name}");
            }
        }
        else
        {
            Console.WriteLine("  (нет подчиненных)");
        }

        Console.WriteLine($"\nСписок всех сотрудников отдела IT:");
        foreach (var emp in itDepartment.Employees)
        {
            string role = (emp == itDepartment.Manager) ? " (начальник)" : "";
            Console.WriteLine($"  - {emp.Name}{role}");
        }

        // 6. Демонстрация динамического изменения
        Console.WriteLine("\n=== Динамическое изменение ===");

        Employee newEmployee = new Employee("Новиков");
        newEmployee.JoinDepartment(itDepartment);

        Console.WriteLine($"После добавления нового сотрудника:");
        Console.WriteLine(itDepartment);
        Console.WriteLine($"Сотрудников в отделе: {itDepartment.EmployeeCount}");

        Console.WriteLine($"\nКоллеги Сидорова:");
        foreach (var colleague in sidorov.GetDepartmentColleagues())
        {
            Console.WriteLine($"  - {colleague.Name}");
        }
    }
}
