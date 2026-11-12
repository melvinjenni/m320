namespace Heating;

public class Controller
{
    private readonly List<Radiator> _radiators = new();

    public IReadOnlyList<Radiator> Radiators => _radiators;

    public void Add(Radiator radiator) => _radiators.Add(radiator);

    public bool SetTemperature(string room, double temperature)
    {
        var radiator = _radiators.FirstOrDefault(r => r.Room.Equals(room, StringComparison.OrdinalIgnoreCase));
        return radiator != null && radiator.SetTarget(temperature);
    }

    public void Tick() => _radiators.ForEach(r => r.Tick());
}
