using Heating;

var controller = new Controller();
controller.Add(new Radiator("Living room"));
controller.Add(new Radiator("Bedroom"));
controller.Add(new Radiator("Bathroom"));

while (true)
{
    Console.WriteLine("\n1 Show  2 Set temperature  3 Next time step  0 Exit");
    Console.Write("> ");
    switch (Console.ReadLine())
    {
        case "1":
            controller.Radiators.ToList().ForEach(Console.WriteLine);
            break;
        case "2":
            Console.Write("Room: ");
            var room = Console.ReadLine() ?? "";
            Console.Write("Target (5-30): ");
            double.TryParse(Console.ReadLine(), out var temperature);
            Console.WriteLine(controller.SetTemperature(room, temperature) ? "OK" : "Failed");
            break;
        case "3":
            controller.Tick();
            controller.Radiators.ToList().ForEach(Console.WriteLine);
            break;
        case "0":
            return;
    }
}
