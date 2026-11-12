namespace EmployeeManagement;

// Keeps one list per category. The lists are private.
public class EmployeeManager
{
    private readonly List<FullTimeEmployee> _fullTimeEmployees = new();
    private readonly List<PartTimeEmployee> _partTimeEmployees = new();
    private readonly List<Intern> _interns = new();

    public IReadOnlyList<FullTimeEmployee> FullTimeEmployees { get { return _fullTimeEmployees; } }
    public IReadOnlyList<PartTimeEmployee> PartTimeEmployees { get { return _partTimeEmployees; } }
    public IReadOnlyList<Intern> Interns { get { return _interns; } }

    public void AddFullTime(FullTimeEmployee employee)
    {
        CheckIdIsFree(employee.Id);
        _fullTimeEmployees.Add(employee);
    }

    public void AddPartTime(PartTimeEmployee employee)
    {
        CheckIdIsFree(employee.Id);
        _partTimeEmployees.Add(employee);
    }

    public void AddIntern(Intern employee)
    {
        CheckIdIsFree(employee.Id);
        _interns.Add(employee);
    }

    // Returns the employee or null if nobody has this id
    public Employee? FindById(int id)
    {
        foreach (FullTimeEmployee e in _fullTimeEmployees)
            if (e.Id == id) return e;
        foreach (PartTimeEmployee e in _partTimeEmployees)
            if (e.Id == id) return e;
        foreach (Intern e in _interns)
            if (e.Id == id) return e;
        return null;
    }

    // Returns the first employee with this name (upper/lower case is ignored) or null
    public Employee? FindByName(string name)
    {
        foreach (FullTimeEmployee e in _fullTimeEmployees)
            if (SameName(e.Name, name)) return e;
        foreach (PartTimeEmployee e in _partTimeEmployees)
            if (SameName(e.Name, name)) return e;
        foreach (Intern e in _interns)
            if (SameName(e.Name, name)) return e;
        return null;
    }

    // Returns true if an employee was removed
    public bool Remove(int id)
    {
        foreach (FullTimeEmployee e in _fullTimeEmployees)
        {
            if (e.Id == id)
            {
                _fullTimeEmployees.Remove(e);
                return true;
            }
        }
        foreach (PartTimeEmployee e in _partTimeEmployees)
        {
            if (e.Id == id)
            {
                _partTimeEmployees.Remove(e);
                return true;
            }
        }
        foreach (Intern e in _interns)
        {
            if (e.Id == id)
            {
                _interns.Remove(e);
                return true;
            }
        }
        return false;
    }

    private void CheckIdIsFree(int id)
    {
        if (FindById(id) != null) throw new ArgumentException("An employee with this id already exists.");
    }

    private bool SameName(string a, string b)
    {
        return a.ToLower() == b.ToLower();
    }
}
