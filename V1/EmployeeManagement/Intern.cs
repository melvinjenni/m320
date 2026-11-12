namespace EmployeeManagement;

public class Intern : Employee
{
    private decimal _fixedMonthlyPay;
    private int _maxHours;
    private int _hoursWorked;

    public decimal FixedMonthlyPay
    {
        get { return _fixedMonthlyPay; }
        set
        {
            if (value < 0) throw new ArgumentException("Fixed monthly pay must not be negative.");
            _fixedMonthlyPay = value;
        }
    }

    public int MaxHours
    {
        get { return _maxHours; }
        set
        {
            if (value <= 0) throw new ArgumentException("Max hours must be greater than 0.");
            if (value < _hoursWorked)
                throw new ArgumentException("Max hours cannot be lower than hours already worked (" + _hoursWorked + ").");
            _maxHours = value;
        }
    }

    public int HoursWorked
    {
        get { return _hoursWorked; }
        set
        {
            if (value < 0 || value > MaxHours)
                throw new ArgumentException("Hours worked must be between 0 and " + MaxHours + ".");
            _hoursWorked = value;
        }
    }

    // MaxHours must be set before HoursWorked, because HoursWorked is checked against it
    public Intern(int id, string name, decimal fixedMonthlyPay, int maxHours, int hoursWorked)
        : base(id, name)
    {
        FixedMonthlyPay = fixedMonthlyPay;
        MaxHours = maxHours;
        HoursWorked = hoursWorked;
    }

    public decimal CalculateMonthlyPay()
    {
        return FixedMonthlyPay;
    }
}
