namespace Bank;

public class Account
{
    private decimal _balance;               // private: only changed through methods
    private readonly List<string> _history = new();

    public string Number { get; }
    public string Owner { get; }
    public decimal Balance => _balance;     // read-only from outside
    public IReadOnlyList<string> History => _history;

    public Account(string number, string owner, decimal startBalance = 0)
    {
        Number = number;
        Owner = owner;
        Deposit(startBalance);
    }

    // Optional text describes the booking in the history
    public bool Deposit(decimal amount, string text = "deposit")
    {
        if (amount <= 0) return false;
        _balance += amount;
        _history.Add($"+{amount:F2} {text}");
        return true;
    }

    public bool Withdraw(decimal amount, string text = "withdrawal")
    {
        if (amount <= 0 || amount > _balance) return false;
        _balance -= amount;
        _history.Add($"-{amount:F2} {text}");
        return true;
    }

    // One object calls a method of another and passes a value
    public bool Transfer(Account target, decimal amount)
    {
        if (target == this) return false;   // no transfer to itself
        if (!Withdraw(amount, $"transfer to {target.Number}")) return false;
        target.Deposit(amount, $"transfer from {Number}");
        return true;
    }

    public override string ToString() => $"{Number} ({Owner}): {_balance:F2} CHF";
}
