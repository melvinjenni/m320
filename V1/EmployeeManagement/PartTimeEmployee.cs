namespace EmployeeManagement;

public class PartTimeEmployee : Employee
{
    private decimal _hourlyRate;
    private int _hoursWorked;

    public decimal HourlyRate
    {
        get { return _hourlyRate; }
        set
        {
            if (value <= 0) throw new ArgumentException("Hourly rate must be greater than 0.");
            _hourlyRate = value;
        }
    }

    public int HoursWorked
    {
        get { return _hoursWorked; }
        set
        {
            if (value < 0 || value > 180) throw new ArgumentException("Hours worked must be between 0 and 180.");
            _hoursWorked = value;
        }
    }

    public PartTimeEmployee(int id, string name, decimal hourlyRate, int hoursWorked)
        : base(id, name)
    {
        HourlyRate = hourlyRate;
        HoursWorked = hoursWorked;
    }

    public decimal CalculateMonthlyPay()
    {
        return HourlyRate * HoursWorked;
    }
}
