using EmployeeManagement;

class Program
{
    static EmployeeManager manager = new EmployeeManager();

    static void Main()
    {
        AddSampleData();

        bool running = true;
        while (running)
        {
            Console.WriteLine();
            Console.WriteLine("1 Add employee");
            Console.WriteLine("2 Show all employees");
            Console.WriteLine("3 Search by id or name");
            Console.WriteLine("4 Remove employee");
            Console.WriteLine("5 Payroll report");
            Console.WriteLine("0 Exit");
            Console.Write("> ");
            string? choice = Console.ReadLine();

            // Wrong input must not end the program: show the message and return to the menu
            try
            {
                if (choice == "1") AddEmployee();
                else if (choice == "2") ShowAll();
                else if (choice == "3") Search();
                else if (choice == "4") RemoveEmployee();
                else if (choice == "5") ShowPayroll();
                else if (choice == "0") running = false;
                else Console.WriteLine("Unknown option.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }

    // Example data so there is something to see at the start
    static void AddSampleData()
    {
        manager.AddFullTime(new FullTimeEmployee(1, "Anna", 6000m, 1000m, true));
        manager.AddFullTime(new FullTimeEmployee(2, "Ben", 6000m, 1000m, false));
        manager.AddPartTime(new PartTimeEmployee(3, "Clara", 40m, 80));
        manager.AddPartTime(new PartTimeEmployee(4, "David", 35.5m, 120));
        manager.AddIntern(new Intern(5, "Eva", 1200m, 100, 80));
    }

    // ----- Input helpers -----

    static string ReadText(string label)
    {
        Console.Write(label + ": ");
        string? text = Console.ReadLine();
        if (text == null) return "";
        return text;
    }

    static int ReadInt(string label)
    {
        Console.Write(label + ": ");
        if (int.TryParse(Console.ReadLine(), out int value)) return value;
        throw new ArgumentException("Please enter a whole number.");
    }

    static decimal ReadDecimal(string label)
    {
        Console.Write(label + ": ");
        if (decimal.TryParse(Console.ReadLine(), out decimal value)) return value;
        throw new ArgumentException("Please enter a number.");
    }

    static bool ReadYesNo(string label)
    {
        Console.Write(label + " (y/n): ");
        string? answer = Console.ReadLine();
        return answer == "y" || answer == "Y";
    }

    // ----- Menu actions -----

    static void AddEmployee()
    {
        Console.WriteLine("Category: 1 Full-time  2 Part-time  3 Intern");
        int category = ReadInt("Category");
        if (category < 1 || category > 3) throw new ArgumentException("Please choose 1, 2 or 3.");

        int id = ReadInt("Id");
        string name = ReadText("Name");

        if (category == 1)
        {
            decimal salary = ReadDecimal("Monthly salary");
            decimal bonus = ReadDecimal("Bonus amount");
            bool eligible = ReadYesNo("Bonus eligible");
            manager.AddFullTime(new FullTimeEmployee(id, name, salary, bonus, eligible));
        }
        else if (category == 2)
        {
            decimal rate = ReadDecimal("Hourly rate");
            int hours = ReadInt("Hours worked");
            manager.AddPartTime(new PartTimeEmployee(id, name, rate, hours));
        }
        else
        {
            decimal pay = ReadDecimal("Fixed monthly pay");
            int maxHours = ReadInt("Max hours");
            int hours = ReadInt("Hours worked");
            manager.AddIntern(new Intern(id, name, pay, maxHours, hours));
        }
        Console.WriteLine("Employee added.");
    }

    static void ShowAll()
    {
        Console.WriteLine("--- Full-time employees ---");
        foreach (FullTimeEmployee e in manager.FullTimeEmployees) PrintFullTime(e);

        Console.WriteLine("--- Part-time employees ---");
        foreach (PartTimeEmployee e in manager.PartTimeEmployees) PrintPartTime(e);

        Console.WriteLine("--- Interns ---");
        foreach (Intern e in manager.Interns) PrintIntern(e);
    }

    static void Search()
    {
        Console.WriteLine("Search by: 1 Id  2 Name");
        int mode = ReadInt("Search by");

        Employee? found;
        if (mode == 1) found = manager.FindById(ReadInt("Id"));
        else if (mode == 2) found = manager.FindByName(ReadText("Name"));
        else throw new ArgumentException("Please choose 1 or 2.");

        if (found == null)
        {
            Console.WriteLine("Nobody found.");
        }
        else if (found is FullTimeEmployee fullTime)
        {
            PrintFullTime(fullTime);
        }
        else if (found is PartTimeEmployee partTime)
        {
            PrintPartTime(partTime);
        }
        else if (found is Intern intern)
        {
            PrintIntern(intern);
        }
    }

    static void RemoveEmployee()
    {
        int id = ReadInt("Id to remove");
        if (manager.Remove(id)) Console.WriteLine("Employee removed.");
        else Console.WriteLine("Nobody found.");
    }

    static void ShowPayroll()
    {
        decimal fullTimeSum = 0;
        decimal partTimeSum = 0;
        decimal internSum = 0;

        Console.WriteLine("--- Full-time employees ---");
        foreach (FullTimeEmployee e in manager.FullTimeEmployees)
        {
            Console.WriteLine(e.Name + ": " + e.CalculateMonthlyPay().ToString("F2"));
            fullTimeSum += e.CalculateMonthlyPay();
        }
        Console.WriteLine("Sum: " + fullTimeSum.ToString("F2"));

        Console.WriteLine("--- Part-time employees ---");
        foreach (PartTimeEmployee e in manager.PartTimeEmployees)
        {
            Console.WriteLine(e.Name + ": " + e.CalculateMonthlyPay().ToString("F2"));
            partTimeSum += e.CalculateMonthlyPay();
        }
        Console.WriteLine("Sum: " + partTimeSum.ToString("F2"));

        Console.WriteLine("--- Interns ---");
        foreach (Intern e in manager.Interns)
        {
            Console.WriteLine(e.Name + ": " + e.CalculateMonthlyPay().ToString("F2"));
            internSum += e.CalculateMonthlyPay();
        }
        Console.WriteLine("Sum: " + internSum.ToString("F2"));

        Console.WriteLine("=== Total: " + (fullTimeSum + partTimeSum + internSum).ToString("F2") + " ===");
    }

    // ----- Output helpers -----

    static void PrintFullTime(FullTimeEmployee e)
    {
        Console.WriteLine("#" + e.Id + " " + e.Name + " | salary " + e.MonthlySalary.ToString("F2")
            + " | bonus " + e.BonusAmount.ToString("F2") + " | bonus eligible: " + e.BonusEligible);
    }

    static void PrintPartTime(PartTimeEmployee e)
    {
        Console.WriteLine("#" + e.Id + " " + e.Name + " | rate " + e.HourlyRate.ToString("F2")
            + " | hours " + e.HoursWorked);
    }

    static void PrintIntern(Intern e)
    {
        Console.WriteLine("#" + e.Id + " " + e.Name + " | fixed pay " + e.FixedMonthlyPay.ToString("F2")
            + " | hours " + e.HoursWorked + " of " + e.MaxHours);
    }
}
