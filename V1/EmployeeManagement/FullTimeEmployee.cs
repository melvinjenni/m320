namespace EmployeeManagement;

public class FullTimeEmployee : Employee
{
    private decimal _monthlySalary;
    private decimal _bonusAmount;

    public decimal MonthlySalary
    {
        get { return _monthlySalary; }
        set
        {
            if (value <= 0) throw new ArgumentException("Monthly salary must be greater than 0.");
            _monthlySalary = value;
        }
    }

    public decimal BonusAmount
    {
        get { return _bonusAmount; }
        set
        {
            if (value < 0) throw new ArgumentException("Bonus amount must not be negative.");
            _bonusAmount = value;
        }
    }

    public bool BonusEligible { get; set; }

    public FullTimeEmployee(int id, string name, decimal monthlySalary, decimal bonusAmount, bool bonusEligible)
        : base(id, name)
    {
        MonthlySalary = monthlySalary;
        BonusAmount = bonusAmount;
        BonusEligible = bonusEligible;
    }

    public decimal CalculateMonthlyPay()
    {
        if (BonusEligible) return MonthlySalary + BonusAmount;
        return MonthlySalary;
    }
}
