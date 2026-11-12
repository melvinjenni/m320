using Bank;

var accounts = new List<Account>
{
    new("CH01", "Anna", 1000m),
    new("CH02", "Ben", 500m),
    new("CH03", "Clara", 250m),
};

while (true)
{
    Console.WriteLine("\n1 List  2 Deposit  3 Withdraw  4 Transfer  5 History  0 Exit");
    Console.Write("> ");
    switch (Console.ReadLine())
    {
        case "1":
            accounts.ForEach(Console.WriteLine);
            break;
        case "2":
        {
            var account = Find("Account");
            if (account != null)
            {
                var ok = account.Deposit(ReadAmount());
                Console.WriteLine(ok ? "OK" : "Failed");
            }
            break;
        }
        case "3":
        {
            var account = Find("Account");
            if (account != null)
            {
                var ok = account.Withdraw(ReadAmount());
                Console.WriteLine(ok ? "OK" : "Failed");
            }
            break;
        }
        case "4":
        {
            var from = Find("From");
            var to = Find("To");
            if (from != null && to != null)
            {
                var ok = from.Transfer(to, ReadAmount());
                Console.WriteLine(ok ? "OK" : "Failed");
            }
            break;
        }
        case "5":
            Find("Account")?.History.ToList().ForEach(Console.WriteLine);
            break;
        case "0":
            return;
    }
}

Account? Find(string label)
{
    Console.Write($"{label} number (e.g. CH01): ");
    var number = Console.ReadLine();
    var account = accounts.FirstOrDefault(a => a.Number == number);
    if (account == null) Console.WriteLine("Account not found");
    return account;
}

decimal ReadAmount()
{
    Console.Write("Amount: ");
    return decimal.TryParse(Console.ReadLine(), out var amount) ? amount : 0;
}
