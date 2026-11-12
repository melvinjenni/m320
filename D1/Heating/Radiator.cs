namespace Heating;

public class Radiator
{
    private double _target = 20;
    private double _current = 18;

    public string Room { get; }
    public bool IsOn { get; private set; }  // readable outside, set only inside
    public double Target => _target;
    public double Current => _current;

    public Radiator(string room)
    {
        Room = room;
        IsOn = _current < _target;  // correct state from the start
    }

    public bool SetTarget(double temperature)
    {
        if (temperature < 5 || temperature > 30) return false;
        _target = temperature;
        IsOn = _current < _target;
        return true;
    }

    // One time step: the state changes depending on the target
    public void Tick()
    {
        _current += IsOn ? 0.5 : -0.2;
        IsOn = _current < _target;
    }

    public override string ToString() =>
        $"{Room,-12} current {_current:F1} C  target {_target:F1} C  {(IsOn ? "on" : "off")}";
}
