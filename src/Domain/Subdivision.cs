namespace Domain;

/// <summary>
/// Подразделение (дирекция, отдел, группа и т.п.)
/// </summary>
public class Subdivision
{
    public Guid Id { get; set; }
    public User Manager { get; set; }
    public required Guid ManagerId { get; set; }
    public List<User> Employees { get; set; }
    public required List<Guid> EmployeeIds { get; set; }
}
