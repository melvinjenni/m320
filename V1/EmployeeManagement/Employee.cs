namespace EmployeeManagement;

// Base class: data that every employee has
public class Employee
{
    private int _id;
    private string _name = "";

    public int Id
    {
        get { return _id; }
        set
        {
            if (value <= 0) throw new ArgumentException("Id must be greater than 0.");
            _id = value;
        }
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Name must not be empty.");
            _name = value;
        }
    }

    // The constructor uses the properties, so the checks are written only once
    public Employee(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
